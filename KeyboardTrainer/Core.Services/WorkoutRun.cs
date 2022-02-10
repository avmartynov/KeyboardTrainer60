using Twidlle.KeyboardTrainer.Core.Model;
using Twidlle.Library.Utility;
using Twidlle.Library.VirtualTime;

namespace Twidlle.KeyboardTrainer.Core.Services;

public class WorkoutRun : IWorkoutRun
{
    private readonly IEnumerable<WorkoutType> _workoutTypes;
    private readonly IEnumerable<WorkoutLanguage> _workoutLanguages;
    private readonly ITimeProvider _timeProvider;
    private readonly IExerciseRun _exerciseRun;

    private WorkoutType _workoutType;
    private WorkoutLanguage _localLanguage;
    private WorkoutState _workoutState;

    public event Action? NewExercise;
    public event Action? NextExerciseStep;
    public event Action? WorkoutSaved;

    public WorkoutRun(IEnumerable<WorkoutType> workoutTypes,
                      IEnumerable<WorkoutLanguage> workoutLanguages,
                      ITimeProvider timeProvider,
                      IExerciseRun exerciseRun)
    {
        ThrowIfNull(workoutTypes);
        ThrowIfNull(workoutLanguages);
        ThrowIfNull(timeProvider);
        ThrowIfNull(exerciseRun);

        _workoutTypes = workoutTypes;
        _workoutLanguages = workoutLanguages;
        _timeProvider = timeProvider;
        _exerciseRun = exerciseRun;

        Init(out _workoutType, 
             out _localLanguage, 
             out _workoutState, 
             new WorkoutType().Code, 
             new WorkoutLanguage().Code);
    }

    public WorkoutType WorkoutType => _workoutType;
    public WorkoutLanguage LocalLanguage => _localLanguage;
    public WorkoutState WorkoutState => _workoutState;
    public IExerciseRun ExerciseRun => _exerciseRun;

    public void Initialize(string workoutTypeCode, string localLanguageCode)
    {
        Init(out _workoutType, 
             out _localLanguage, 
             out _workoutState, 
             workoutTypeCode,
             localLanguageCode);
    }

    public void Load(string filePath)
    {
        var workoutFile = XmlExtensions.ReadFile<WorkoutFile>(filePath);

        Init(out _workoutType, 
             out _localLanguage, 
             out _workoutState, 
             workoutFile.WorkoutTypeCode, 
             workoutFile.LocalLanguageCode, 
             workoutFile.WorkoutState);

        WorkoutState.FilePath = filePath;
    }

    private void Init(
        out WorkoutType workoutType, 
        out WorkoutLanguage localLanguage,
        out WorkoutState workoutState,
        string workoutTypeCode, 
        string localLanguageCode, 
        WorkoutState? state = null)
    {
        workoutType = _workoutTypes.SingleOrDefault(i => i.Code == workoutTypeCode) ?? new WorkoutType();
        localLanguage = _workoutLanguages.SingleOrDefault(i => i.Code == localLanguageCode) ?? new WorkoutLanguage();

        workoutState = state ?? new WorkoutState()
        {
            StartDateTime = _timeProvider.Now,
            LastPerformedDateTime = _timeProvider.Now
        };
        workoutState.Changed = false;

        _exerciseRun.Reset(workoutType, localLanguage);
    }

    /// <summary> Saving with current file path. </summary>
    public void Save()
    {
        if (WorkoutState.FilePath == null)
        {
            throw new InvalidOperationException("There is no current file path.");
        }

        Save(WorkoutState.FilePath);
    }

    /// <summary> Saving with new file path. </summary>
    public void Save(string filePath)
    {
        var workoutFile = new WorkoutFile
        {
            LocalLanguageCode = LocalLanguage.Code, 
            WorkoutTypeCode   = WorkoutType.Code, 
            WorkoutState      = WorkoutState
        };

        XmlExtensions.WriteFile(workoutFile, filePath);

        WorkoutState.FilePath = filePath;
        WorkoutState.Changed  = false;

        ResetExercise();
        WorkoutSaved?.Invoke();
    }

    public void ResetExercise(bool withoutNotifyView = false)
    {
        _exerciseRun.Reset(WorkoutType, LocalLanguage);

        if (!withoutNotifyView)
        {
            NextExerciseStep?.Invoke();
        }
    }

    public void ResetState()
    {
        var filePath = WorkoutState.FilePath;

        _workoutState = new WorkoutState
        {
            FilePath = filePath,
            StartDateTime = _timeProvider.Now,
            Changed = true
        };
        ResetExercise();
        NewExercise?.Invoke();
    }

    public bool OnKeyPressed(KeyCode keyPressed)
    {
        if (ExerciseRun.OnKeyPressed(keyPressed))
        {
            OnExerciseStep();
            return true;
        }
        return false;
    }

    public void OnCharPressed(char charPressed)
    {
        ExerciseRun.OnCharPressed(charPressed);

        OnExerciseStep();
    }

    //----------------------

    private void OnExerciseStep()
    {
        NextExerciseStep?.Invoke();

        if (ExerciseRun.CheckFinished(out var charPerMinute, out var errorCount))
        {
            SetExerciseFinished(WorkoutState, charPerMinute, (double)errorCount / WorkoutType.ExerciseLength);
            ResetExercise();
            NewExercise?.Invoke();
        }
    }

    private static void SetExerciseFinished(WorkoutState workoutState, int charPerMinute, double errorFraction)
    {
        workoutState.LastCharPerMinute = charPerMinute;
        workoutState.LastErrorFraction = errorFraction;

        if (workoutState.BestExerciseCharPerMinute < workoutState.LastCharPerMinute)
        {
            workoutState.BestExerciseCharPerMinute = workoutState.LastCharPerMinute;
            workoutState.BestExerciseErrorFraction = workoutState.LastErrorFraction;
        }

        workoutState.AverageCharPerMinute = (workoutState.AverageCharPerMinute * workoutState.ExerciseCount + charPerMinute) / (workoutState.ExerciseCount + 1);
        workoutState.AverageErrorFraction = (workoutState.AverageErrorFraction * workoutState.ExerciseCount + errorFraction) / (workoutState.ExerciseCount + 1);

        workoutState.ExerciseCount++;

        workoutState.Changed = true;
    }
}

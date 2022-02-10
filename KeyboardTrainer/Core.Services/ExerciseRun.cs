using System.Diagnostics;
using Twidlle.KeyboardTrainer.Core.Model;
using Twidlle.Library.VirtualTime;

namespace Twidlle.KeyboardTrainer.Core.Services;

public class ExerciseRun : IExerciseRun
{
    private readonly ITimeProvider _timeProvider;
    private readonly IExerciseGenerator _exerciseGenerator;

    public IReadOnlyList<ExerciseItem> ExerciseString { get; private set; }

    private DateTimeOffset? _startDate;
    private DateTimeOffset? _finishDate;
    private int _errorCount;

    public int CurrentPosition { get; private set; }
    public bool WrongTyping { get; private set; }

    public ExerciseRun(ITimeProvider timeProvider,
                       IExerciseGenerator exerciseGenerator)
    {
        ThrowIfNull(timeProvider);
        ThrowIfNull(exerciseGenerator);

        _timeProvider = timeProvider;
        _exerciseGenerator = exerciseGenerator;

        ExerciseString = _exerciseGenerator.CreateExercise(new WorkoutType(), new WorkoutLanguage());
    }

    public bool Running => _startDate.HasValue;
    public bool Finished => _finishDate.HasValue;

    public void Reset(WorkoutType workoutType, WorkoutLanguage localLanguage)
    {
        ThrowIfNull(localLanguage);
        ThrowIfNull(workoutType);

        ExerciseString = _exerciseGenerator.CreateExercise(workoutType, localLanguage);

        CurrentPosition = 0;
        WrongTyping = false;
        _startDate = null;
        _finishDate = null;
        _errorCount = 0;
    }

    public bool CheckFinished(out int charPerMinute, out int errorCount)
    {
        if (_finishDate == null)
        {
            charPerMinute = 0;
            errorCount = 0;
            return false;
        }

        charPerMinute = (int)(ExerciseString.Count / (_finishDate.Value - _startDate!.Value).TotalMinutes);
        errorCount = _errorCount;
        return true;
    }

    public bool OnKeyPressed(KeyCode keyPressed)
    {
        _startDate ??= _timeProvider.Now;
        Debug.Assert(Running);

        var currentKeyItem = ExerciseString[CurrentPosition];
        if (currentKeyItem is not FuncKeyItem keyItem)
            return false;

        WrongTyping = keyItem.Key != keyPressed;

        if (WrongTyping)
            _errorCount++;
        else
        {
            CurrentPosition++;
            if (CurrentPosition == ExerciseString.Count)
            {
                _finishDate = _timeProvider.Now;
                Debug.Assert(Finished);
            }
        }
        return true;
    }

    public void OnCharPressed(char charPressed)
    {
        _startDate ??= _timeProvider.Now;
        Debug.Assert(Running);

        WrongTyping = (ExerciseString[CurrentPosition] as CharacterItem)?.Character != charPressed;

        if (WrongTyping)
            _errorCount++;
        else
        {
            CurrentPosition++;
            if (CurrentPosition == ExerciseString.Count)
            {
                _finishDate = _timeProvider.Now;
                Debug.Assert(Finished);
            }
        }
    }
}


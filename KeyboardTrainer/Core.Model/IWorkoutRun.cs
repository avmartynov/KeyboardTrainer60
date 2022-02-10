namespace Twidlle.KeyboardTrainer.Core.Model;

public interface IWorkoutRun
{
    WorkoutType     WorkoutType   { get; }
    WorkoutLanguage LocalLanguage { get; }
    WorkoutState    WorkoutState  { get; }

    IExerciseRun ExerciseRun { get; }

    /// <summary> Notification about new exercise </summary>
    event Action NewExercise;

    /// <summary> Notification about exercise execution </summary>
    event Action NextExerciseStep;

    /// <summary> Notification about workout have been saved </summary>
    event Action WorkoutSaved;


    void Initialize(string workoutTypeCode, string localLanguageCode);
    void Load(string filePath);

    bool OnKeyPressed(KeyCode keyPressed);
    void OnCharPressed(char charPressed);

    void Save(string filePath);
    void Save();

    void ResetExercise(bool withoutNotifyView = false);
    void ResetState();
}

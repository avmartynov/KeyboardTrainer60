namespace Twidlle.KeyboardTrainer.Core.Model;

public interface IExerciseRun
{
    public IReadOnlyList<ExerciseItem> ExerciseString { get; }

    public int CurrentPosition { get; }

    public bool WrongTyping { get;  }

    public bool Running { get; }

    public bool Finished { get; }


    public void Reset(WorkoutType workoutType, WorkoutLanguage localLanguage);

    public bool CheckFinished(out int charPerMinute, out int errorCount);

    public bool OnKeyPressed(KeyCode keyPressed);

    public void OnCharPressed(char charPressed);
}
namespace Twidlle.KeyboardTrainer.Core.Model;

public interface IExerciseGenerator  
{
    IReadOnlyList<ExerciseItem> CreateExercise(WorkoutType workoutType, WorkoutLanguage localLanguage);
}
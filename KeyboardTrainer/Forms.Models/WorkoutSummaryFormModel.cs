using Twidlle.KeyboardTrainer.Core.Model;
using Twidlle.Library.WinForms.Mvp.Dialogs;

namespace Twidlle.KeyboardTrainer.Forms.Models;

public class WorkoutSummaryFormModel
{
    public string? BestCharPerMinute { get; set; }
    public string? BestErrors { get; set; }

    public string? AverageCharPerMinute { get; set; }
    public string? AverageErrors { get; set; }

    public string? LastCharPerMinute { get; set; }
    public string? LastErrors { get; set; }

    public string? WorkoutType { get; set; }
    public string? Language { get; set; }
    public string? ExerciseCount { get; set; }
}

public interface IWorkoutSummaryForm : IDialogView<WorkoutSummaryFormModel>
{
    event Action CopyWorkoutSummary;
}

public interface IWorkoutSummaryFormPresenter 
{
    /// <summary> Отображает диалоговое окно и ожидает его закрытия. </summary>
    void ShowDialog(IWorkoutRun workoutRun);
}

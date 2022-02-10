using Twidlle.KeyboardTrainer.Core.Model;
using Twidlle.Library.WinForms.Mvp.Dialogs;

namespace Twidlle.KeyboardTrainer.Forms.Models;

public class WorkoutFormModel
{
    public string SelectedWorkoutTypeCode { get; set; } = default!;
    public string SelectedLanguageCode { get; set; } = default!;

    public WorkoutType[] WorkoutTypes { get; set; } = default!;
    public WorkoutLanguage[] Languages { get; set; } = default!;
}

public interface IWorkoutForm : IDialogView<WorkoutFormModel>
{
    event Action Accept;
}

public interface IWorkoutFormPresenter 
{
    /// <summary> Отображает диалоговое окно и ожидает его закрытия. </summary>
    void ShowDialog(IWorkoutRun workoutRun);
}

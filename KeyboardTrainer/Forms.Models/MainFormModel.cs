using System.Drawing;
using Twidlle.Library.WinForms.Mvp.Dialogs;

namespace Twidlle.KeyboardTrainer.Forms.Models;

public class MainFormModel
{
    public string? Title { get; set; }
    public string? LastResult { get; set; }
    public string? BestResult { get; set; }
    public string? ExerciseCount { get; set; }
}

public interface IMainForm: IDialogView<MainFormModel>
{
    event Action FormShown;
    event Func<bool> CloseRequest;
    event Action<char> KeyPressed;
    event Func<int, bool> KeyDownSuppressRequest;
    event Action<Graphics, Rectangle> PaintExercise;
    event Action NewWorkoutFile;
    event Action LoadWorkoutFile;
    event Action SaveWorkoutFile;
    event Action SaveWorkoutFileAs;
    event Action ShowWorkoutProperties;
    event Action NewExercise;
    event Action ResetWorkout;
    event Action EditOptions;
    event Action NavigateToHomePage;
    event Action ShowAboutForm;
}

public interface IMainFormPresenter : IDialogPresenter
{
}

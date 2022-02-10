using Twidlle.KeyboardTrainer.Core.Model;
using Twidlle.KeyboardTrainer.Forms.Models;
using Twidlle.KeyboardTrainer.WinFormsApp.Properties;
using Twidlle.Library.WinForms;

namespace Twidlle.KeyboardTrainer.WinFormsApp.Forms
{
    public partial class MainForm : Form, IMainForm
    {
        public MainFormModel Model { get; } = new();

        public void ShowDialogForm() =>
            Application.Run(this);

        public event Action? FormShown;
        public event Func<bool>? CloseRequest;
        public event Action<char>? KeyPressed;
        public event Func<int, bool>? KeyDownSuppressRequest;
        public event Action<Graphics, Rectangle>? PaintExercise;
        public event Action? NewWorkoutFile;
        public event Action? LoadWorkoutFile;
        public event Action? SaveWorkoutFile;
        public event Action? SaveWorkoutFileAs;
        public event Action? ShowWorkoutProperties;
        public event Action? NewExercise;
        public event Action? ResetWorkout;
        public event Action? EditOptions;
        public event Action? NavigateToHomePage;
        public event Action? ShowAboutForm;

        public MainForm()
        {
            this.RestoreFormLocation(Settings.Default, x => x.MainForm);

            InitializeComponent();

            this.Shown       += (_, _) => FormShown?.Invoke();
            this.FormClosing += (_, e) => e.Cancel = !(CloseRequest?.Invoke() ?? true);
            this.KeyDown     += (_, e) => e.SuppressKeyPress = KeyDownSuppressRequest?.Invoke((int)e.KeyCode) ?? false;
            this.KeyPress    += (_, e) => KeyPressed?.Invoke(e.KeyChar);

            this._testPanel.Paint            += (_, e) => PaintExercise?.Invoke(e.Graphics, _testPanel.ClientRectangle);
            this.newWorkoutMenuItem.Click    += (_, _) => NewWorkoutFile?.Invoke();
            this.openWorkoutMenuItem.Click   += (_, _) => LoadWorkoutFile?.Invoke();
            this.saveWorkoutMenuItem.Click   += (_, _) => SaveWorkoutFile?.Invoke();
            this.saveWorkoutAsMenuItem.Click += (_, _) => SaveWorkoutFileAs?.Invoke();
            this.workoutPropsMenuItem.Click  += (_, _) => ShowWorkoutProperties?.Invoke();
            this.exitMenuItem.Click          += (_, _) => Close();
            this.nextExerciseMenuItem.Click  += (_, _) => NewExercise?.Invoke();
            this.resetWorkoutMenuItem.Click  += (_, _) => ResetWorkout?.Invoke();
            this.preferencesMenuItem.Click   += (_, _) => EditOptions?.Invoke();
            this.homePageMenuItem.Click      += (_, _) => NavigateToHomePage?.Invoke();
            this.aboutProgramMenuItem.Click  += (_, _) => ShowAboutForm?.Invoke();
        }

        public void RedrawModel()
        {
            Text = Model.Title;

            lastResultToolStripStatusLabel.Text = Model.LastResult;
            bestResultToolStripStatusLabel.Text = Model.BestResult;
            exerciseCountToolStripStatusLabel.Text = Model.ExerciseCount;

            _testPanel.Invalidate();
        }
    }
}




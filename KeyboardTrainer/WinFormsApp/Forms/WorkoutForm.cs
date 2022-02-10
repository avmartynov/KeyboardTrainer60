using Twidlle.KeyboardTrainer.Forms.Models;

namespace Twidlle.KeyboardTrainer.WinFormsApp.Forms
{
    public partial class WorkoutForm : Form, IWorkoutForm
    {
        public WorkoutFormModel Model { get; } = new();

        public void ShowDialogForm() =>
            ShowDialog();

        public event Action? Accept;

        public WorkoutForm()
        {
            InitializeComponent();

            modelBindingSource.DataSource = Model;

            this.okButton.Click += (_, _) => Accept?.Invoke();
        }

        public void RedrawModel() =>
            modelBindingSource.ResetBindings(metadataChanged: false);
    }
}

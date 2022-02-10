using Twidlle.KeyboardTrainer.Forms.Models;

namespace Twidlle.KeyboardTrainer.WinFormsApp.Forms
{
    public partial class WorkoutSummaryForm : Form, IWorkoutSummaryForm
    {
        public WorkoutSummaryFormModel Model { get; } = new();

        public void ShowDialogForm() =>
            ShowDialog();

        public event Action? CopyWorkoutSummary;

        public WorkoutSummaryForm()
        {
            InitializeComponent();

            modelBindingSource.DataSource = Model;

            this.copyButton.Click += (_, _) => CopyWorkoutSummary?.Invoke();
        }

        public void RedrawModel() =>
            modelBindingSource.ResetBindings(metadataChanged: false);
    }
}

using Twidlle.KeyboardTrainer.Forms.Models;

namespace Twidlle.KeyboardTrainer.WinFormsApp.Forms
{
    public partial class PreferencesForm : Form, IPreferencesForm
    {
        public PreferencesFormModel Model { get; } = new();

        public void ShowDialogForm() =>
            ShowDialog();

        public event Action? Accept;

        public PreferencesForm()
        {
            InitializeComponent();

            modelBindingSource.DataSource = Model;

            this.okButton.Click += (_,_) => Accept?.Invoke();
        }

        public void RedrawModel() =>
            modelBindingSource.ResetBindings(metadataChanged: false);
    }
}

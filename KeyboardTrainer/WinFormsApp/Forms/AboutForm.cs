using Twidlle.KeyboardTrainer.Forms.Models;
using Twidlle.KeyboardTrainer.WinFormsApp.Properties;

namespace Twidlle.KeyboardTrainer.WinFormsApp.Forms
{
    public sealed partial class AboutForm : Form, IAboutForm
    {
        public AboutFormModel Model { get; } = new();

        public void ShowDialogForm() =>
            ShowDialog();

        public event Action? OpenConfigDirectory;
        public event Action? CopyDiagnosticsInfo;

        public AboutForm()
        {
            InitializeComponent();

            _pictureBox.Image = Resources.AppIcon.ToBitmap();
            _pictureBox.BackColor = Color.Transparent;
        }

        public void RedrawModel()
        {
            Text = string.Format(Text, Model.Title); 
            _productLabel.Text = Model.Product;
            _versionLabel.Text = string.Format(_versionLabel.Text, Model.Version);
            _copyrightLabel.Text = string.Format(_copyrightLabel.Text, Model.CopyrightYear, Model.CompanyName);
        }

        private void AboutForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.D)
                OpenConfigDirectory?.Invoke();

            if (e.Control && e.KeyCode == Keys.C)
                CopyDiagnosticsInfo?.Invoke();
        }
    }
}

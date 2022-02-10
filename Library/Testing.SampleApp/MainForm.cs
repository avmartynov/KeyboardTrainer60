namespace Testing.SampleApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Environment.GetCommandLineArgs().Length > 1)
                MessageBox.Show("Test");
        }
    }
}
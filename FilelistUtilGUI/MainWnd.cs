namespace FilelistUtilGUI
{
    public partial class MainWnd : Form
    {
        public MainWnd()
        {
            InitializeComponent();
        }

        private void MainWnd_Load(object sender, EventArgs e)
        {
            InitExtractPanel();
            InitCreatePanel();
        }
    }
}

namespace FilelistUtilGUI
{
    public partial class MainWnd : Form
    {
        private BinInfoListColumnSorter _columnSorter;

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

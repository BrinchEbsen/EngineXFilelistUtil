using FilelistUtilities.Common;
using FilelistUtilities.Filelist;

namespace FilelistUtilGUI
{
    public partial class MainWnd : Form
    {
        private string? _selectedBinFile = null;
        private Filelist? _currentFileListBin = null;
        private string? _outputScrFile = null;

        public MainWnd()
        {
            InitializeComponent();
        }

        private void MainWnd_Load(object sender, EventArgs e)
        {
            GroupBox_FilelistStats.Visible = false;
        }

        private void Btn_BrowseReadBinFile_Click(object sender, EventArgs e)
        {
            var res = OpenReadBinDialogue.ShowDialog(this);

            if (res == DialogResult.OK)
            {
                if (OpenReadBinDialogue.FileName != string.Empty)
                    OpenBinFile(OpenReadBinDialogue.FileName);
            }
        }

        private void OpenBinFile(string binFile)
        {
            try
            {
                if (!binFile.EndsWith(".bin", StringComparison.InvariantCultureIgnoreCase))
                    throw new Exception($"{binFile} is not a .bin filelist file.");

                Filelist filelist = Filelist.Read(binFile);

                _selectedBinFile = binFile;
                _currentFileListBin = filelist;

                PopulateBinFileInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed to open .bin file.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateBinFileInfo()
        {
            if (_currentFileListBin is null || _selectedBinFile is null) return;

            Lbl_OpenedBinFile.Text = _selectedBinFile;

            //Populate the table

            //foreach (ListViewItem row in ListView_FilelistBinList.Items)
            //    row.Dispose();

            ListView_FilelistBinList.Items.Clear();

            for (int i = 0; i < _currentFileListBin.FileInfo.Length; i++)
            {
                FileInfoElement info = _currentFileListBin.FileInfo[i];

                ListViewItem row = new(i.ToString());

                string filelocNumStr = "1";
                string filelocStr = string.Empty;

                if (_currentFileListBin.Version < 5)
                {
                    filelocStr = info.FileLoc.ToString("X");
                }
                else
                {
                    filelocNumStr = info.NumFileLoc.ToString();

                    if (info.FileLocInfo.Length == 0)
                    {
                        filelocStr = "(None)";
                    }
                    else
                    {
                        bool first = true;
                        foreach (var loc in info.FileLocInfo)
                        {
                            if (!first)
                                filelocStr += ", ";
                            first = false;

                            filelocStr += loc.ToString();
                        }
                    }
                }

                row.SubItems.AddRange(
                    new ListViewItem.ListViewSubItem() { Text = info.Path },
                    new ListViewItem.ListViewSubItem() { Text = info.Length.ToString() },
                    new ListViewItem.ListViewSubItem() { Text = info.HashCode.ToString("X") },
                    new ListViewItem.ListViewSubItem() { Text = info.Version.ToString() },
                    new ListViewItem.ListViewSubItem() { Text = info.Flags.ToString("X") },
                    new ListViewItem.ListViewSubItem() { Text = filelocNumStr },
                    new ListViewItem.ListViewSubItem() { Text = filelocStr }
                    );

                ListView_FilelistBinList.Items.Add(row);
            }

            //Populate the stats window

            Lbl_FilelistVersion.Text = _currentFileListBin.Version.ToString();
            Lbl_BinSize.Text = _currentFileListBin.FileSize.ToString();
            Lbl_NumberOfFiles.Text = _currentFileListBin.NumFiles.ToString();
            Lbl_BuildType.Text = _currentFileListBin.Version < 5 ? "N/A" : _currentFileListBin.BuildType.ToString();
            Lbl_NumberOfArchives.Text = _currentFileListBin.Version < 5 ? "1" : (_currentFileListBin.NumFileLists + 1).ToString();

            //Populate the "Archives" panel

            foreach (Control ctrl in FlowPanel_Archives.Controls)
                ctrl.Dispose();

            FlowPanel_Archives.Controls.Clear();

            string[] arvhicePaths = FindBinAssociatedArchives();

            foreach (string path in arvhicePaths)
            {
                FlowPanel_Archives.Controls.Add(new Label
                {
                    Text = Path.GetFileName(path),
                });
            }

            //Finally make stats panel visible to the user.

            GroupBox_FilelistStats.Visible = true;
            GroupBox_ExtractFiles.Visible = true;
        }

        private string[] FindBinAssociatedArchives()
        {
            if (_currentFileListBin is null || _selectedBinFile is null) return [];

            try
            {
                string dir = Path.GetDirectoryName(_selectedBinFile)!;
                string name = Path.GetFileNameWithoutExtension(_selectedBinFile)!;

                //Versions before v5 use a single .dat archive.
                if (_currentFileListBin.Version < 5)
                {
                    string datPath = Path.Combine(dir, name + ".dat");
                    if (!File.Exists(datPath))
                    {
                        MessageBox.Show(
                            $"The expected archive {datPath} could not be found.",
                            "Error finding filelist archive.",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return [];
                    }
                    return [datPath];
                }
                else
                //Version 5+ use archives with incrementing extensions (.000, .001, .002 etc.)
                {
                    List<string> paths = new(_currentFileListBin.NumFileLists + 1);

                    for (int i = 0; i < _currentFileListBin.NumFileLists + 1; i++)
                    {
                        string datPath = Path.Combine(dir, name + "." + i.ToString().PadLeft(3, '0'));
                        if (!File.Exists(datPath))
                        {
                            MessageBox.Show(
                                $"The expected archive {datPath} could not be found.",
                                "Error finding filelist archive.",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return [];
                        }
                        paths.Add(datPath);
                    }

                    return paths.ToArray();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                            ex.Message,
                            "Error finding filelist archive.",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                return [];
            }
        }

        private void Btn_ExtractFiles_Click(object sender, EventArgs e)
        {
            var res = ExtractFilesDirectoryDialog.ShowDialog(this);

            if (res == DialogResult.OK)
            {
                string path = ExtractFilesDirectoryDialog.SelectedPath;
                if (Directory.Exists(path))
                {
                    ExtractFiles(path);
                }
            }
        }

        private void Btn_BrowseOutputScr_Click(object sender, EventArgs e)
        {
            var res = OutputScrFileDialog.ShowDialog(this);

            if (res == DialogResult.OK)
            {
                if (OutputScrFileDialog.FileName != string.Empty)
                {
                    _outputScrFile = OutputScrFileDialog.FileName;
                    Lbl_ExportScrFilePath.Text = _outputScrFile;
                }
            }
        }

        private void Check_OutputScr_CheckedChanged(object sender, EventArgs e)
        {
            Btn_BrowseOutputScr.Enabled = Check_OutputScr.Checked;
            Lbl_ExportScrFilePath.Enabled = Check_OutputScr.Checked;
        }

        private void ExtractFiles(string path)
        {
            if (_currentFileListBin is null) return;

            if (Check_OutputScr.Checked)
            {
                if (_outputScrFile is null)
                {
                    MessageBox.Show(
                        "No output .scr file was specified.",
                        "Cannot extract files.",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }

                try
                {
                    _currentFileListBin.ExportDescriptor(_outputScrFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        ex.Message,
                        "Error outputting .scr file.",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    return;
                }
            }

            try
            {
                void proc()
                {
                    _currentFileListBin.ExportFiles(path);
                }

                ProcessBar procBar = new(proc, "Extracting files")
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                procBar.ShowDialog(this);
                procBar.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                        ex.Message,
                        "Error extracting files.",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }
    }
}

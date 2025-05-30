using FilelistUtilities.Common;
using FilelistUtilities.Filelist;

namespace FilelistUtilGUI
{
    public partial class MainWnd
    {
        private string? _inputScrPath = null;
        private string? _inputCreateFilelistDir = null;

        private Dictionary<string, GamePlatform> PlatformDict = new()
        {
            { "GameCube", GamePlatform.GameCube },
            { "Xbox", GamePlatform.Xbox },
            { "PlayStation 2", GamePlatform.PlayStation2 }
        };

        private void InitCreatePanel()
        {
            foreach (var key in PlatformDict.Keys)
                ComboBox_Platform.Items.Add(key);

            ComboBox_Platform.SelectedIndex = 0;

            NumUpDown_Version.Minimum = Filelist.SupportedVersions.Min();
            NumUpDown_Version.Maximum = Filelist.SupportedVersions.Max();
            NumUpDown_Version.Value = NumUpDown_Version.Maximum;

            NumUpDown_SplitSize.Value = Filelist.FILE_SIZE_SPLIT_MINIMUM;
            NumUpDown_SplitSize.Minimum = Filelist.FILE_SIZE_SPLIT_MINIMUM;
        }

        private void Check_UseSplitSize_CheckedChanged(object sender, EventArgs e)
        {
            Check_UseXboxDefaultSplitSize.Enabled = Check_UseSplitSize.Checked;
            NumUpDown_SplitSize.Enabled = (!Check_UseXboxDefaultSplitSize.Checked) && Check_UseSplitSize.Checked;
        }

        private void Check_UseScrFile_CheckedChanged(object sender, EventArgs e)
        {
            Btn_BrowseInputScr.Enabled = Check_UseScrFile.Checked;
            Lbl_InputScrFilePath.Enabled = Check_UseScrFile.Checked;
        }

        private void Btn_BrowseInputScr_Click(object sender, EventArgs e)
        {
            var res = InputScrFileDialog.ShowDialog(this);

            if (res == DialogResult.OK)
            {
                if (InputScrFileDialog.FileName != string.Empty)
                {
                    _inputScrPath = InputScrFileDialog.FileName;
                    Lbl_InputScrFilePath.Text = _inputScrPath;
                }
            }
        }

        private void Btn_BrowseInputDirectory_Click(object sender, EventArgs e)
        {
            var res = CreateFilelistInputDirDialog.ShowDialog(this);

            if (res == DialogResult.OK)
            {
                if (CreateFilelistInputDirDialog.SelectedPath != string.Empty)
                {
                    _inputCreateFilelistDir = CreateFilelistInputDirDialog.SelectedPath;
                    Lbl_CreateFilelistInputDir.Text = _inputCreateFilelistDir;
                }
            }
        }

        private void Btn_CreateFileList_Click(object sender, EventArgs e)
        {
            var res = CreateFilelistOutputDirDialog.ShowDialog(this);

            if (res == DialogResult.OK)
            {
                if (CreateFilelistOutputDirDialog.SelectedPath != string.Empty)
                {
                    string path = CreateFilelistOutputDirDialog.SelectedPath;
                    CreateFileList(path);
                }
            }
        }

        private void CreateFileList(string path)
        {
            if (_inputCreateFilelistDir is null)
            {
                MessageBox.Show("No input directory specified.", "Cannot create filelist.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string inputPath = _inputCreateFilelistDir;

            if (!GetCreateFilelistSetting(out GamePlatform platform, out FileListExportSettings settings))
                return;

            try
            {
                void proc()
                {
                    Filelist.ExportFileList(inputPath, path, platform, settings);
                }

                ProcessBar procBar = new(proc, "Creating filelist")
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                procBar.ShowDialog(this);
                procBar.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cannot create filelist.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private bool GetCreateFilelistSetting(out GamePlatform platform, out FileListExportSettings settings)
        {
            platform = GamePlatform.PlayStation2;
            settings = new FileListExportSettings();

            // PLATFORM

            string? selectedPlatform = (string?)ComboBox_Platform.Items[ComboBox_Platform.SelectedIndex];

            if (selectedPlatform is null)
            {
                MessageBox.Show("No platform selected.", "Invalid settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!PlatformDict.TryGetValue(selectedPlatform, out platform))
            {
                MessageBox.Show("Invalid platform selected.", "Invalid settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // VERSION

            uint version = (uint)NumUpDown_Version.Value;

            if (!Filelist.SupportedVersions.Contains(version))
            {
                MessageBox.Show($"Unsupported version ({version}) selected.", "Invalid settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            settings.Version = version;

            // FILELIST NAME

            string filelistName = TextBox_FilelistName.Text;

            if (filelistName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                MessageBox.Show($"Filelist name {filelistName} is an invalid file name.", "Invalid settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            settings.FileListName = filelistName;

            // DRIVE LETTER

            string driveLetter = TextBox_DriveLetter.Text;

            if (driveLetter.Length == 0)
            {
                var res = MessageBox.Show("No drive letter was specified, use default ('x')?", "Use default drive letter", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (res == DialogResult.Cancel)
                    return false;
            }

            if (Path.GetInvalidFileNameChars().Contains(driveLetter[0]))
            {
                MessageBox.Show($"Drive letter {driveLetter[0]} is invalid.", "Invalid settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            settings.RootName = driveLetter[0];

            // SPLIT SIZE

            if (Check_UseSplitSize.Checked)
            {
                if (Check_UseXboxDefaultSplitSize.Checked)
                {
                    settings.FileSizeSplit = Filelist.FILE_SIZE_SPLIT_XBOX_DEFAULT;
                } else
                {
                    settings.FileSizeSplit = (uint)NumUpDown_SplitSize.Value;
                }
            } else
            {
                settings.FileSizeSplit = -1;
            }

            // SCR FILE

            if (Check_UseScrFile.Checked)
            {
                string? scrPath = _inputScrPath;

                if (scrPath is null)
                {
                    MessageBox.Show($".scr file not specified.", "Invalid settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                settings.UseDescriptor = true;
                settings.DescriptorPath = scrPath;
            }

            return true;
        }

        // Code derived from:
        // https://learn.microsoft.com/da-dk/troubleshoot/developer/visualstudio/csharp/language-compilers/sort-listview-by-column
        private void ListView_FilelistBinList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == _columnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (_columnSorter.Order == SortOrder.Ascending)
                {
                    _columnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    _columnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                _columnSorter.SortColumn = e.Column;
                _columnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.ListView_FilelistBinList.Sort();
        }
    }
}

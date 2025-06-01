
namespace FilelistUtilGUI
{
    partial class MainWnd
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tabControl1 = new TabControl();
            TabPage_Extract = new TabPage();
            GroupBox_ExtractFiles = new GroupBox();
            Btn_ExtractFiles = new Button();
            Btn_BrowseOutputScr = new Button();
            Lbl_ExportScrFilePath = new Label();
            Check_OutputScr = new CheckBox();
            ListView_FilelistBinList = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            GroupBox_FilelistStats = new GroupBox();
            Btn_TransferToCreateTab = new Button();
            FlowPanel_Archives = new FlowLayoutPanel();
            label7 = new Label();
            Lbl_NumberOfArchives = new Label();
            Lbl_BuildType = new Label();
            Lbl_NumberOfFiles = new Label();
            Lbl_BinSize = new Label();
            Lbl_FilelistVersion = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            Lbl_OpenedBinFile = new Label();
            Btn_BrowseReadBinFile = new Button();
            label1 = new Label();
            TabPage_Create = new TabPage();
            Btn_CreateFileList = new Button();
            Lbl_CreateFilelistInputDir = new Label();
            Btn_BrowseInputDirectory = new Button();
            label14 = new Label();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            Btn_BrowseInputScr = new Button();
            Lbl_InputScrFilePath = new Label();
            Check_UseScrFile = new CheckBox();
            NumUpDown_SplitSize = new NumericUpDown();
            Check_UseXboxDefaultSplitSize = new CheckBox();
            TextBox_DriveLetter = new TextBox();
            TextBox_FilelistName = new TextBox();
            NumUpDown_Version = new NumericUpDown();
            label8 = new Label();
            ComboBox_Platform = new ComboBox();
            Check_UseSplitSize = new CheckBox();
            OpenReadBinDialogue = new OpenFileDialog();
            OutputScrFileDialog = new SaveFileDialog();
            ExtractFilesDirectoryDialog = new FolderBrowserDialog();
            InputScrFileDialog = new OpenFileDialog();
            CreateFilelistInputDirDialog = new FolderBrowserDialog();
            CreateFilelistOutputDirDialog = new FolderBrowserDialog();
            MainToolTiP = new ToolTip(components);
            tabControl1.SuspendLayout();
            TabPage_Extract.SuspendLayout();
            GroupBox_ExtractFiles.SuspendLayout();
            GroupBox_FilelistStats.SuspendLayout();
            TabPage_Create.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumUpDown_SplitSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumUpDown_Version).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(TabPage_Extract);
            tabControl1.Controls.Add(TabPage_Create);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(887, 838);
            tabControl1.TabIndex = 0;
            // 
            // TabPage_Extract
            // 
            TabPage_Extract.Controls.Add(GroupBox_ExtractFiles);
            TabPage_Extract.Controls.Add(ListView_FilelistBinList);
            TabPage_Extract.Controls.Add(GroupBox_FilelistStats);
            TabPage_Extract.Controls.Add(Lbl_OpenedBinFile);
            TabPage_Extract.Controls.Add(Btn_BrowseReadBinFile);
            TabPage_Extract.Controls.Add(label1);
            TabPage_Extract.Location = new Point(4, 24);
            TabPage_Extract.Name = "TabPage_Extract";
            TabPage_Extract.Padding = new Padding(3);
            TabPage_Extract.Size = new Size(879, 810);
            TabPage_Extract.TabIndex = 0;
            TabPage_Extract.Text = "Read/Extract";
            TabPage_Extract.UseVisualStyleBackColor = true;
            // 
            // GroupBox_ExtractFiles
            // 
            GroupBox_ExtractFiles.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox_ExtractFiles.Controls.Add(Btn_ExtractFiles);
            GroupBox_ExtractFiles.Controls.Add(Btn_BrowseOutputScr);
            GroupBox_ExtractFiles.Controls.Add(Lbl_ExportScrFilePath);
            GroupBox_ExtractFiles.Controls.Add(Check_OutputScr);
            GroupBox_ExtractFiles.Location = new Point(6, 696);
            GroupBox_ExtractFiles.Name = "GroupBox_ExtractFiles";
            GroupBox_ExtractFiles.Size = new Size(867, 108);
            GroupBox_ExtractFiles.TabIndex = 11;
            GroupBox_ExtractFiles.TabStop = false;
            GroupBox_ExtractFiles.Text = "Extract";
            GroupBox_ExtractFiles.Visible = false;
            // 
            // Btn_ExtractFiles
            // 
            Btn_ExtractFiles.Location = new Point(6, 22);
            Btn_ExtractFiles.Name = "Btn_ExtractFiles";
            Btn_ExtractFiles.Size = new Size(120, 23);
            Btn_ExtractFiles.TabIndex = 7;
            Btn_ExtractFiles.Text = "Extract Files...";
            MainToolTiP.SetToolTip(Btn_ExtractFiles, "Extract the files to a specified folder.");
            Btn_ExtractFiles.UseVisualStyleBackColor = true;
            Btn_ExtractFiles.Click += Btn_ExtractFiles_Click;
            // 
            // Btn_BrowseOutputScr
            // 
            Btn_BrowseOutputScr.Enabled = false;
            Btn_BrowseOutputScr.Location = new Point(6, 76);
            Btn_BrowseOutputScr.Name = "Btn_BrowseOutputScr";
            Btn_BrowseOutputScr.Size = new Size(75, 23);
            Btn_BrowseOutputScr.TabIndex = 10;
            Btn_BrowseOutputScr.Text = "Browse";
            MainToolTiP.SetToolTip(Btn_BrowseOutputScr, "Select the destination for the outputted .scr file.");
            Btn_BrowseOutputScr.UseVisualStyleBackColor = true;
            Btn_BrowseOutputScr.Click += Btn_BrowseOutputScr_Click;
            // 
            // Lbl_ExportScrFilePath
            // 
            Lbl_ExportScrFilePath.AutoSize = true;
            Lbl_ExportScrFilePath.Enabled = false;
            Lbl_ExportScrFilePath.Location = new Point(87, 80);
            Lbl_ExportScrFilePath.Name = "Lbl_ExportScrFilePath";
            Lbl_ExportScrFilePath.Size = new Size(155, 15);
            Lbl_ExportScrFilePath.TabIndex = 9;
            Lbl_ExportScrFilePath.Text = "No .scr destination selected.";
            // 
            // Check_OutputScr
            // 
            Check_OutputScr.AutoSize = true;
            Check_OutputScr.Location = new Point(6, 51);
            Check_OutputScr.Name = "Check_OutputScr";
            Check_OutputScr.Size = new Size(104, 19);
            Check_OutputScr.TabIndex = 8;
            Check_OutputScr.Text = "Output .scr file";
            MainToolTiP.SetToolTip(Check_OutputScr, "Output a .scr file to document the included files and their order.\r\n");
            Check_OutputScr.UseVisualStyleBackColor = true;
            Check_OutputScr.CheckedChanged += Check_OutputScr_CheckedChanged;
            // 
            // ListView_FilelistBinList
            // 
            ListView_FilelistBinList.AllowColumnReorder = true;
            ListView_FilelistBinList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ListView_FilelistBinList.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5, columnHeader6, columnHeader7, columnHeader8 });
            ListView_FilelistBinList.Location = new Point(6, 50);
            ListView_FilelistBinList.MultiSelect = false;
            ListView_FilelistBinList.Name = "ListView_FilelistBinList";
            ListView_FilelistBinList.Size = new Size(867, 509);
            ListView_FilelistBinList.TabIndex = 6;
            ListView_FilelistBinList.UseCompatibleStateImageBehavior = false;
            ListView_FilelistBinList.View = View.Details;
            ListView_FilelistBinList.ColumnClick += ListView_FilelistBinList_ColumnClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Index";
            columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Path";
            columnHeader2.Width = 300;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Size";
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "HashCode";
            columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Version";
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Flags";
            columnHeader6.Width = 70;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "Number of locations";
            columnHeader7.Width = 130;
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "Location(s)";
            columnHeader8.Width = 80;
            // 
            // GroupBox_FilelistStats
            // 
            GroupBox_FilelistStats.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            GroupBox_FilelistStats.Controls.Add(Btn_TransferToCreateTab);
            GroupBox_FilelistStats.Controls.Add(FlowPanel_Archives);
            GroupBox_FilelistStats.Controls.Add(label7);
            GroupBox_FilelistStats.Controls.Add(Lbl_NumberOfArchives);
            GroupBox_FilelistStats.Controls.Add(Lbl_BuildType);
            GroupBox_FilelistStats.Controls.Add(Lbl_NumberOfFiles);
            GroupBox_FilelistStats.Controls.Add(Lbl_BinSize);
            GroupBox_FilelistStats.Controls.Add(Lbl_FilelistVersion);
            GroupBox_FilelistStats.Controls.Add(label6);
            GroupBox_FilelistStats.Controls.Add(label5);
            GroupBox_FilelistStats.Controls.Add(label4);
            GroupBox_FilelistStats.Controls.Add(label3);
            GroupBox_FilelistStats.Controls.Add(label2);
            GroupBox_FilelistStats.Location = new Point(6, 565);
            GroupBox_FilelistStats.Name = "GroupBox_FilelistStats";
            GroupBox_FilelistStats.Size = new Size(867, 125);
            GroupBox_FilelistStats.TabIndex = 5;
            GroupBox_FilelistStats.TabStop = false;
            GroupBox_FilelistStats.Text = "Filelist Stats";
            // 
            // Btn_TransferToCreateTab
            // 
            Btn_TransferToCreateTab.Location = new Point(6, 97);
            Btn_TransferToCreateTab.Name = "Btn_TransferToCreateTab";
            Btn_TransferToCreateTab.Size = new Size(166, 23);
            Btn_TransferToCreateTab.TabIndex = 12;
            Btn_TransferToCreateTab.Text = "Transfer to create tab";
            MainToolTiP.SetToolTip(Btn_TransferToCreateTab, "Transfer the information from this filelist to the \"Create\" tab.\r\n");
            Btn_TransferToCreateTab.UseVisualStyleBackColor = true;
            Btn_TransferToCreateTab.Click += Btn_TransferToCreateTab_Click;
            // 
            // FlowPanel_Archives
            // 
            FlowPanel_Archives.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            FlowPanel_Archives.AutoScroll = true;
            FlowPanel_Archives.BorderStyle = BorderStyle.FixedSingle;
            FlowPanel_Archives.FlowDirection = FlowDirection.TopDown;
            FlowPanel_Archives.Location = new Point(318, 19);
            FlowPanel_Archives.Name = "FlowPanel_Archives";
            FlowPanel_Archives.Size = new Size(543, 100);
            FlowPanel_Archives.TabIndex = 11;
            MainToolTiP.SetToolTip(FlowPanel_Archives, "The archives referenced by this filelist, containing the actual binary data of the files.");
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(257, 64);
            label7.Name = "label7";
            label7.Size = new Size(55, 15);
            label7.TabIndex = 10;
            label7.Text = "Archives:";
            // 
            // Lbl_NumberOfArchives
            // 
            Lbl_NumberOfArchives.AutoSize = true;
            Lbl_NumberOfArchives.Location = new Point(124, 79);
            Lbl_NumberOfArchives.Name = "Lbl_NumberOfArchives";
            Lbl_NumberOfArchives.Size = new Size(29, 15);
            Lbl_NumberOfArchives.TabIndex = 9;
            Lbl_NumberOfArchives.Text = "N/A";
            MainToolTiP.SetToolTip(Lbl_NumberOfArchives, "Number of archives used by this filelist. Only version 5 or above uses more than 1.");
            // 
            // Lbl_BuildType
            // 
            Lbl_BuildType.AutoSize = true;
            Lbl_BuildType.Location = new Point(124, 64);
            Lbl_BuildType.Name = "Lbl_BuildType";
            Lbl_BuildType.Size = new Size(29, 15);
            Lbl_BuildType.TabIndex = 8;
            Lbl_BuildType.Text = "N/A";
            MainToolTiP.SetToolTip(Lbl_BuildType, "Only version 5 and above: Type of this filelist. 0 for loosely stored files, 1 for files stored in archives.");
            // 
            // Lbl_NumberOfFiles
            // 
            Lbl_NumberOfFiles.AutoSize = true;
            Lbl_NumberOfFiles.Location = new Point(124, 49);
            Lbl_NumberOfFiles.Name = "Lbl_NumberOfFiles";
            Lbl_NumberOfFiles.Size = new Size(29, 15);
            Lbl_NumberOfFiles.TabIndex = 7;
            Lbl_NumberOfFiles.Text = "N/A";
            MainToolTiP.SetToolTip(Lbl_NumberOfFiles, "Amount of files in the file system.");
            // 
            // Lbl_BinSize
            // 
            Lbl_BinSize.AutoSize = true;
            Lbl_BinSize.Location = new Point(124, 34);
            Lbl_BinSize.Name = "Lbl_BinSize";
            Lbl_BinSize.Size = new Size(29, 15);
            Lbl_BinSize.TabIndex = 6;
            Lbl_BinSize.Text = "N/A";
            MainToolTiP.SetToolTip(Lbl_BinSize, "The size of the .bin filelist file in bytes.");
            // 
            // Lbl_FilelistVersion
            // 
            Lbl_FilelistVersion.AutoSize = true;
            Lbl_FilelistVersion.Location = new Point(124, 19);
            Lbl_FilelistVersion.Name = "Lbl_FilelistVersion";
            Lbl_FilelistVersion.Size = new Size(29, 15);
            Lbl_FilelistVersion.TabIndex = 5;
            Lbl_FilelistVersion.Text = "N/A";
            MainToolTiP.SetToolTip(Lbl_FilelistVersion, "The version of the filelist.");
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 79);
            label6.Name = "label6";
            label6.Size = new Size(114, 15);
            label6.TabIndex = 4;
            label6.Text = "Number of archives:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 64);
            label5.Name = "label5";
            label5.Size = new Size(63, 15);
            label5.TabIndex = 3;
            label5.Text = "Build type:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 49);
            label4.Name = "label4";
            label4.Size = new Size(92, 15);
            label4.TabIndex = 2;
            label4.Text = "Number of files:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 34);
            label3.Name = "label3";
            label3.Size = new Size(52, 15);
            label3.TabIndex = 1;
            label3.Text = ".bin size:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 19);
            label2.Name = "label2";
            label2.Size = new Size(48, 15);
            label2.TabIndex = 0;
            label2.Text = "Version:";
            // 
            // Lbl_OpenedBinFile
            // 
            Lbl_OpenedBinFile.AutoSize = true;
            Lbl_OpenedBinFile.Location = new Point(87, 25);
            Lbl_OpenedBinFile.Name = "Lbl_OpenedBinFile";
            Lbl_OpenedBinFile.Size = new Size(91, 15);
            Lbl_OpenedBinFile.TabIndex = 4;
            Lbl_OpenedBinFile.Text = "No file selected.";
            // 
            // Btn_BrowseReadBinFile
            // 
            Btn_BrowseReadBinFile.Location = new Point(6, 21);
            Btn_BrowseReadBinFile.Name = "Btn_BrowseReadBinFile";
            Btn_BrowseReadBinFile.Size = new Size(75, 23);
            Btn_BrowseReadBinFile.TabIndex = 2;
            Btn_BrowseReadBinFile.Text = "Browse";
            MainToolTiP.SetToolTip(Btn_BrowseReadBinFile, "Open a .bin filelist file.");
            Btn_BrowseReadBinFile.UseVisualStyleBackColor = true;
            Btn_BrowseReadBinFile.Click += Btn_BrowseReadBinFile_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 3);
            label1.Name = "label1";
            label1.Size = new Size(49, 15);
            label1.TabIndex = 1;
            label1.Text = ".bin file:";
            // 
            // TabPage_Create
            // 
            TabPage_Create.Controls.Add(Btn_CreateFileList);
            TabPage_Create.Controls.Add(Lbl_CreateFilelistInputDir);
            TabPage_Create.Controls.Add(Btn_BrowseInputDirectory);
            TabPage_Create.Controls.Add(label14);
            TabPage_Create.Controls.Add(label13);
            TabPage_Create.Controls.Add(label12);
            TabPage_Create.Controls.Add(label11);
            TabPage_Create.Controls.Add(label10);
            TabPage_Create.Controls.Add(label9);
            TabPage_Create.Controls.Add(Btn_BrowseInputScr);
            TabPage_Create.Controls.Add(Lbl_InputScrFilePath);
            TabPage_Create.Controls.Add(Check_UseScrFile);
            TabPage_Create.Controls.Add(NumUpDown_SplitSize);
            TabPage_Create.Controls.Add(Check_UseXboxDefaultSplitSize);
            TabPage_Create.Controls.Add(TextBox_DriveLetter);
            TabPage_Create.Controls.Add(TextBox_FilelistName);
            TabPage_Create.Controls.Add(NumUpDown_Version);
            TabPage_Create.Controls.Add(label8);
            TabPage_Create.Controls.Add(ComboBox_Platform);
            TabPage_Create.Controls.Add(Check_UseSplitSize);
            TabPage_Create.Location = new Point(4, 24);
            TabPage_Create.Name = "TabPage_Create";
            TabPage_Create.Padding = new Padding(3);
            TabPage_Create.Size = new Size(879, 810);
            TabPage_Create.TabIndex = 1;
            TabPage_Create.Text = "Create";
            TabPage_Create.UseVisualStyleBackColor = true;
            // 
            // Btn_CreateFileList
            // 
            Btn_CreateFileList.Location = new Point(47, 309);
            Btn_CreateFileList.Name = "Btn_CreateFileList";
            Btn_CreateFileList.Size = new Size(107, 34);
            Btn_CreateFileList.TabIndex = 22;
            Btn_CreateFileList.Text = "Create Filelist...";
            MainToolTiP.SetToolTip(Btn_CreateFileList, "Output the new filelist to a specified folder.");
            Btn_CreateFileList.UseVisualStyleBackColor = true;
            Btn_CreateFileList.Click += Btn_CreateFileList_Click;
            // 
            // Lbl_CreateFilelistInputDir
            // 
            Lbl_CreateFilelistInputDir.AutoSize = true;
            Lbl_CreateFilelistInputDir.Location = new Point(179, 270);
            Lbl_CreateFilelistInputDir.Name = "Lbl_CreateFilelistInputDir";
            Lbl_CreateFilelistInputDir.Size = new Size(122, 15);
            Lbl_CreateFilelistInputDir.TabIndex = 21;
            Lbl_CreateFilelistInputDir.Text = "No directory selected.";
            // 
            // Btn_BrowseInputDirectory
            // 
            Btn_BrowseInputDirectory.Location = new Point(98, 266);
            Btn_BrowseInputDirectory.Name = "Btn_BrowseInputDirectory";
            Btn_BrowseInputDirectory.Size = new Size(75, 23);
            Btn_BrowseInputDirectory.TabIndex = 20;
            Btn_BrowseInputDirectory.Text = "Browse";
            MainToolTiP.SetToolTip(Btn_BrowseInputDirectory, "Specify the input directory containing the files to include in the filelist.");
            Btn_BrowseInputDirectory.UseVisualStyleBackColor = true;
            Btn_BrowseInputDirectory.Click += Btn_BrowseInputDirectory_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(6, 270);
            label14.Name = "label14";
            label14.Size = new Size(88, 15);
            label14.TabIndex = 19;
            label14.Text = "Input directory:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(6, 216);
            label13.Name = "label13";
            label13.Size = new Size(47, 15);
            label13.TabIndex = 18;
            label13.Text = ".scr file:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(6, 134);
            label12.Name = "label12";
            label12.Size = new Size(74, 15);
            label12.TabIndex = 17;
            label12.Text = "Split file size:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 107);
            label11.Name = "label11";
            label11.Size = new Size(67, 15);
            label11.TabIndex = 16;
            label11.Text = "Drive letter:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(6, 78);
            label10.Name = "label10";
            label10.Size = new Size(76, 15);
            label10.TabIndex = 15;
            label10.Text = "Filelist name:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(6, 48);
            label9.Name = "label9";
            label9.Size = new Size(48, 15);
            label9.TabIndex = 14;
            label9.Text = "Version:";
            // 
            // Btn_BrowseInputScr
            // 
            Btn_BrowseInputScr.Enabled = false;
            Btn_BrowseInputScr.Location = new Point(119, 237);
            Btn_BrowseInputScr.Name = "Btn_BrowseInputScr";
            Btn_BrowseInputScr.Size = new Size(75, 23);
            Btn_BrowseInputScr.TabIndex = 13;
            Btn_BrowseInputScr.Text = "Browse";
            MainToolTiP.SetToolTip(Btn_BrowseInputScr, "Specify the .scr file to use.");
            Btn_BrowseInputScr.UseVisualStyleBackColor = true;
            Btn_BrowseInputScr.Click += Btn_BrowseInputScr_Click;
            // 
            // Lbl_InputScrFilePath
            // 
            Lbl_InputScrFilePath.AutoSize = true;
            Lbl_InputScrFilePath.Enabled = false;
            Lbl_InputScrFilePath.Location = new Point(200, 241);
            Lbl_InputScrFilePath.Name = "Lbl_InputScrFilePath";
            Lbl_InputScrFilePath.Size = new Size(112, 15);
            Lbl_InputScrFilePath.TabIndex = 12;
            Lbl_InputScrFilePath.Text = "No .scr file selected.";
            // 
            // Check_UseScrFile
            // 
            Check_UseScrFile.AutoSize = true;
            Check_UseScrFile.Location = new Point(98, 212);
            Check_UseScrFile.Name = "Check_UseScrFile";
            Check_UseScrFile.Size = new Size(85, 19);
            Check_UseScrFile.TabIndex = 11;
            Check_UseScrFile.Text = "Use .scr file";
            MainToolTiP.SetToolTip(Check_UseScrFile, "Use a .scr file to specify which files to include and in what order.");
            Check_UseScrFile.UseVisualStyleBackColor = true;
            Check_UseScrFile.CheckedChanged += Check_UseScrFile_CheckedChanged;
            // 
            // NumUpDown_SplitSize
            // 
            NumUpDown_SplitSize.Enabled = false;
            NumUpDown_SplitSize.Location = new Point(119, 183);
            NumUpDown_SplitSize.Maximum = new decimal(new int[] { 2000000000, 0, 0, 0 });
            NumUpDown_SplitSize.Name = "NumUpDown_SplitSize";
            NumUpDown_SplitSize.Size = new Size(120, 23);
            NumUpDown_SplitSize.TabIndex = 7;
            MainToolTiP.SetToolTip(NumUpDown_SplitSize, "The maximum size of an archive in bytes before it's split into another archive.");
            // 
            // Check_UseXboxDefaultSplitSize
            // 
            Check_UseXboxDefaultSplitSize.AutoSize = true;
            Check_UseXboxDefaultSplitSize.Enabled = false;
            Check_UseXboxDefaultSplitSize.Location = new Point(119, 158);
            Check_UseXboxDefaultSplitSize.Name = "Check_UseXboxDefaultSplitSize";
            Check_UseXboxDefaultSplitSize.Size = new Size(115, 19);
            Check_UseXboxDefaultSplitSize.TabIndex = 6;
            Check_UseXboxDefaultSplitSize.Text = "Use Xbox Default";
            MainToolTiP.SetToolTip(Check_UseXboxDefaultSplitSize, "Use the usual split size limit for Eurocom's Xbox ports.");
            Check_UseXboxDefaultSplitSize.UseVisualStyleBackColor = true;
            Check_UseXboxDefaultSplitSize.CheckedChanged += Check_UseSplitSize_CheckedChanged;
            // 
            // TextBox_DriveLetter
            // 
            TextBox_DriveLetter.Location = new Point(98, 104);
            TextBox_DriveLetter.MaxLength = 1;
            TextBox_DriveLetter.Name = "TextBox_DriveLetter";
            TextBox_DriveLetter.Size = new Size(120, 23);
            TextBox_DriveLetter.TabIndex = 5;
            TextBox_DriveLetter.Text = "x";
            MainToolTiP.SetToolTip(TextBox_DriveLetter, "The root drive letter for the virtual file system.");
            // 
            // TextBox_FilelistName
            // 
            TextBox_FilelistName.Location = new Point(98, 75);
            TextBox_FilelistName.MaxLength = 256;
            TextBox_FilelistName.Name = "TextBox_FilelistName";
            TextBox_FilelistName.Size = new Size(120, 23);
            TextBox_FilelistName.TabIndex = 3;
            TextBox_FilelistName.Text = "Filelist";
            MainToolTiP.SetToolTip(TextBox_FilelistName, "The name of the filelist (with no extension).");
            // 
            // NumUpDown_Version
            // 
            NumUpDown_Version.Location = new Point(98, 46);
            NumUpDown_Version.Name = "NumUpDown_Version";
            NumUpDown_Version.Size = new Size(120, 23);
            NumUpDown_Version.TabIndex = 2;
            MainToolTiP.SetToolTip(NumUpDown_Version, "The version of the filelist.");
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 20);
            label8.Name = "label8";
            label8.Size = new Size(56, 15);
            label8.TabIndex = 1;
            label8.Text = "Platform:";
            // 
            // ComboBox_Platform
            // 
            ComboBox_Platform.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox_Platform.FormattingEnabled = true;
            ComboBox_Platform.Location = new Point(98, 17);
            ComboBox_Platform.Name = "ComboBox_Platform";
            ComboBox_Platform.Size = new Size(121, 23);
            ComboBox_Platform.TabIndex = 0;
            MainToolTiP.SetToolTip(ComboBox_Platform, "The platform to output the filelist for.");
            // 
            // Check_UseSplitSize
            // 
            Check_UseSplitSize.AutoSize = true;
            Check_UseSplitSize.Location = new Point(98, 133);
            Check_UseSplitSize.Name = "Check_UseSplitSize";
            Check_UseSplitSize.Size = new Size(124, 19);
            Check_UseSplitSize.TabIndex = 4;
            Check_UseSplitSize.Text = "Use Split Size Limit";
            MainToolTiP.SetToolTip(Check_UseSplitSize, "Split the archive into multiple files when they exceed a specified size in bytes.");
            Check_UseSplitSize.UseVisualStyleBackColor = true;
            Check_UseSplitSize.CheckedChanged += Check_UseSplitSize_CheckedChanged;
            // 
            // OpenReadBinDialogue
            // 
            OpenReadBinDialogue.Title = "Open .bin filelist file";
            // 
            // OutputScrFileDialog
            // 
            OutputScrFileDialog.DefaultExt = "scr";
            OutputScrFileDialog.FileName = "Filelist";
            OutputScrFileDialog.Filter = "Descriptor files|*.scr";
            OutputScrFileDialog.Title = "Save output .scr file";
            // 
            // InputScrFileDialog
            // 
            InputScrFileDialog.Filter = "Descriptor files|*.scr";
            // 
            // MainWnd
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(911, 862);
            Controls.Add(tabControl1);
            Name = "MainWnd";
            ShowIcon = false;
            Text = "Filelist Utility";
            Load += MainWnd_Load;
            tabControl1.ResumeLayout(false);
            TabPage_Extract.ResumeLayout(false);
            TabPage_Extract.PerformLayout();
            GroupBox_ExtractFiles.ResumeLayout(false);
            GroupBox_ExtractFiles.PerformLayout();
            GroupBox_FilelistStats.ResumeLayout(false);
            GroupBox_FilelistStats.PerformLayout();
            TabPage_Create.ResumeLayout(false);
            TabPage_Create.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NumUpDown_SplitSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumUpDown_Version).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage TabPage_Extract;
        private TabPage TabPage_Create;
        private OpenFileDialog OpenReadBinDialogue;
        private Label label1;
        private Button Btn_BrowseReadBinFile;
        private Label Lbl_OpenedBinFile;
        private GroupBox GroupBox_FilelistStats;
        private Label label3;
        private Label label2;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label Lbl_NumberOfArchives;
        private Label Lbl_BuildType;
        private Label Lbl_NumberOfFiles;
        private Label Lbl_BinSize;
        private Label Lbl_FilelistVersion;
        private Label label7;
        private FlowLayoutPanel FlowPanel_Archives;
        private ListView ListView_FilelistBinList;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private Button Btn_ExtractFiles;
        private CheckBox Check_OutputScr;
        private Button Btn_BrowseOutputScr;
        private Label Lbl_ExportScrFilePath;
        private SaveFileDialog OutputScrFileDialog;
        private GroupBox GroupBox_ExtractFiles;
        private FolderBrowserDialog ExtractFilesDirectoryDialog;
        private Label label8;
        private ComboBox ComboBox_Platform;
        private NumericUpDown NumUpDown_Version;
        private TextBox TextBox_FilelistName;
        private CheckBox Check_UseSplitSize;
        private TextBox TextBox_DriveLetter;
        private CheckBox Check_UseXboxDefaultSplitSize;
        private NumericUpDown NumUpDown_SplitSize;
        private Button Btn_BrowseInputScr;
        private Label Lbl_InputScrFilePath;
        private CheckBox Check_UseScrFile;
        private OpenFileDialog InputScrFileDialog;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label14;
        private Label Lbl_CreateFilelistInputDir;
        private Button Btn_BrowseInputDirectory;
        private Button Btn_CreateFileList;
        private FolderBrowserDialog CreateFilelistInputDirDialog;
        private FolderBrowserDialog CreateFilelistOutputDirDialog;
        private Button Btn_TransferToCreateTab;
        private ToolTip MainToolTiP;
    }
}

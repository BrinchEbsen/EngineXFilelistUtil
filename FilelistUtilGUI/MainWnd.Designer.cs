
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
            OpenReadBinDialogue = new OpenFileDialog();
            OutputScrFileDialog = new SaveFileDialog();
            ExtractFilesDirectoryDialog = new FolderBrowserDialog();
            tabControl1.SuspendLayout();
            TabPage_Extract.SuspendLayout();
            GroupBox_ExtractFiles.SuspendLayout();
            GroupBox_FilelistStats.SuspendLayout();
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
            Check_OutputScr.UseVisualStyleBackColor = true;
            Check_OutputScr.CheckedChanged += Check_OutputScr_CheckedChanged;
            // 
            // ListView_FilelistBinList
            // 
            ListView_FilelistBinList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ListView_FilelistBinList.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5, columnHeader6, columnHeader7, columnHeader8 });
            ListView_FilelistBinList.Location = new Point(6, 50);
            ListView_FilelistBinList.MultiSelect = false;
            ListView_FilelistBinList.Name = "ListView_FilelistBinList";
            ListView_FilelistBinList.Size = new Size(867, 532);
            ListView_FilelistBinList.TabIndex = 6;
            ListView_FilelistBinList.UseCompatibleStateImageBehavior = false;
            ListView_FilelistBinList.View = View.Details;
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
            GroupBox_FilelistStats.Location = new Point(6, 588);
            GroupBox_FilelistStats.Name = "GroupBox_FilelistStats";
            GroupBox_FilelistStats.Size = new Size(867, 102);
            GroupBox_FilelistStats.TabIndex = 5;
            GroupBox_FilelistStats.TabStop = false;
            GroupBox_FilelistStats.Text = "Filelist Stats";
            // 
            // FlowPanel_Archives
            // 
            FlowPanel_Archives.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            FlowPanel_Archives.AutoScroll = true;
            FlowPanel_Archives.BorderStyle = BorderStyle.FixedSingle;
            FlowPanel_Archives.FlowDirection = FlowDirection.TopDown;
            FlowPanel_Archives.Location = new Point(305, 19);
            FlowPanel_Archives.Name = "FlowPanel_Archives";
            FlowPanel_Archives.Size = new Size(556, 77);
            FlowPanel_Archives.TabIndex = 11;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(244, 19);
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
            // 
            // Lbl_BuildType
            // 
            Lbl_BuildType.AutoSize = true;
            Lbl_BuildType.Location = new Point(124, 64);
            Lbl_BuildType.Name = "Lbl_BuildType";
            Lbl_BuildType.Size = new Size(29, 15);
            Lbl_BuildType.TabIndex = 8;
            Lbl_BuildType.Text = "N/A";
            // 
            // Lbl_NumberOfFiles
            // 
            Lbl_NumberOfFiles.AutoSize = true;
            Lbl_NumberOfFiles.Location = new Point(124, 49);
            Lbl_NumberOfFiles.Name = "Lbl_NumberOfFiles";
            Lbl_NumberOfFiles.Size = new Size(29, 15);
            Lbl_NumberOfFiles.TabIndex = 7;
            Lbl_NumberOfFiles.Text = "N/A";
            // 
            // Lbl_BinSize
            // 
            Lbl_BinSize.AutoSize = true;
            Lbl_BinSize.Location = new Point(124, 34);
            Lbl_BinSize.Name = "Lbl_BinSize";
            Lbl_BinSize.Size = new Size(29, 15);
            Lbl_BinSize.TabIndex = 6;
            Lbl_BinSize.Text = "N/A";
            // 
            // Lbl_FilelistVersion
            // 
            Lbl_FilelistVersion.AutoSize = true;
            Lbl_FilelistVersion.Location = new Point(124, 19);
            Lbl_FilelistVersion.Name = "Lbl_FilelistVersion";
            Lbl_FilelistVersion.Size = new Size(29, 15);
            Lbl_FilelistVersion.TabIndex = 5;
            Lbl_FilelistVersion.Text = "N/A";
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
            TabPage_Create.Location = new Point(4, 24);
            TabPage_Create.Name = "TabPage_Create";
            TabPage_Create.Padding = new Padding(3);
            TabPage_Create.Size = new Size(879, 810);
            TabPage_Create.TabIndex = 1;
            TabPage_Create.Text = "Create";
            TabPage_Create.UseVisualStyleBackColor = true;
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
    }
}

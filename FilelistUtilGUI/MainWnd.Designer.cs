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
            TabPage_Create = new TabPage();
            tabControl1.SuspendLayout();
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
            tabControl1.Size = new Size(811, 584);
            tabControl1.TabIndex = 0;
            // 
            // TabPage_Extract
            // 
            TabPage_Extract.Location = new Point(4, 24);
            TabPage_Extract.Name = "TabPage_Extract";
            TabPage_Extract.Padding = new Padding(3);
            TabPage_Extract.Size = new Size(803, 556);
            TabPage_Extract.TabIndex = 0;
            TabPage_Extract.Text = "Read/Extract";
            TabPage_Extract.UseVisualStyleBackColor = true;
            // 
            // TabPage_Create
            // 
            TabPage_Create.Location = new Point(4, 24);
            TabPage_Create.Name = "TabPage_Create";
            TabPage_Create.Padding = new Padding(3);
            TabPage_Create.Size = new Size(645, 585);
            TabPage_Create.TabIndex = 1;
            TabPage_Create.Text = "Create";
            TabPage_Create.UseVisualStyleBackColor = true;
            // 
            // MainWnd
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(835, 608);
            Controls.Add(tabControl1);
            Name = "MainWnd";
            ShowIcon = false;
            Text = "Filelist Utility";
            Load += this.MainWnd_Load;
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage TabPage_Extract;
        private TabPage TabPage_Create;
    }
}

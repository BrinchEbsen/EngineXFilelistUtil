namespace FilelistUtilGUI
{
    partial class ProcessBar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ProgressBar_ProcProgress = new ProgressBar();
            Lbl_Msg = new Label();
            SuspendLayout();
            // 
            // ProgressBar_ProcProgress
            // 
            ProgressBar_ProcProgress.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ProgressBar_ProcProgress.Location = new Point(12, 34);
            ProgressBar_ProcProgress.Name = "ProgressBar_ProcProgress";
            ProgressBar_ProcProgress.Size = new Size(684, 23);
            ProgressBar_ProcProgress.TabIndex = 0;
            // 
            // Lbl_Msg
            // 
            Lbl_Msg.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Lbl_Msg.AutoSize = true;
            Lbl_Msg.Location = new Point(12, 16);
            Lbl_Msg.Name = "Lbl_Msg";
            Lbl_Msg.Size = new Size(57, 15);
            Lbl_Msg.TabIndex = 1;
            Lbl_Msg.Text = "Starting...";
            // 
            // ProcessBar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(708, 69);
            Controls.Add(Lbl_Msg);
            Controls.Add(ProgressBar_ProcProgress);
            Name = "ProcessBar";
            ShowIcon = false;
            Text = "ProcessBar";
            Load += ProcessBar_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar ProgressBar_ProcProgress;
        private Label Lbl_Msg;
    }
}
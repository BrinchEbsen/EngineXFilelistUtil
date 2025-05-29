using FilelistUtilities.Filelist;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilelistUtilGUI
{
    public partial class ProcessBar : Form
    {
        private Action _action;

        public ProcessBar(Action action, string title)
        {
            InitializeComponent();
            _action = action;
            Text = title;
        }

        private void ProcessBar_Load(object sender, EventArgs e)
        {
            FormClosed += new FormClosedEventHandler((object? sender, FormClosedEventArgs e) =>
            {
                Filelist.ClearCallBacks();
            });

            Thread procThread = new(() => {
                try
                {
                    _action();
                } catch (Exception ex)
                {
                    Invoke(ShowErrorAndClose, ex.Message);
                }
                Invoke(Close);
            });

            Filelist.ClearCallBacks();
            Filelist.LogCallBack = (string msg, double progress) =>
            {
                Invoke(UpdateVisual, msg, progress);
            };
            Filelist.ErrorCallBack = (string msg) =>
            {
                Invoke(ShowErrorAndClose, msg);
            };

            procThread.Start();
        }

        public void ShowErrorAndClose(string msg)
        {
            MessageBox.Show(msg, "Error extracting files.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Close();
        }

        public void UpdateVisual(string msg, double progress)
        {
            Lbl_Msg.Text = msg;

            if (progress == 0)
                ProgressBar_ProcProgress.Value = 0;
            else
            {
                double max = (double)ProgressBar_ProcProgress.Maximum;
                ProgressBar_ProcProgress.Value = (int)(max * progress);
            }
        }
    }
}

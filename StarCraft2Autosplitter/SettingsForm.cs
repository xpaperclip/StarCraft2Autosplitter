using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace StarCraft2Autosplitter
{
    public partial class SettingsForm : UserControl
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        public AutosplitterComponent Component { get; set; }


        // status

        public void SetCampaign(ICampaign campaign)
        {
            lblCampaign.Text = campaign.Name;
        }
        public void SetBankPath(string filename)
        {
            txtBankPath.Text = filename;
        }
        private void lnkShowBankFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var filename = txtBankPath.Text;
            Process.Start("explorer.exe", $"/select,\"{filename}\"");
        }

        public void Update_IgtUpdate(bool value)
        {
            chkIgtUpdate.Checked = value;
        }
        private void chkIgtUpdate_CheckedChanged(object sender, EventArgs e)
        {
            Component.IgtUpdate = chkIgtUpdate.Checked;
        }

        public void Update_WriteIgtFile(bool value)
        {
            chkWriteIgtFile.Checked = value;
        }
        private void chkWriteIgtFile_CheckedChanged(object sender, EventArgs e)
        {
            Component.WriteIgtFile = chkWriteIgtFile.Checked;
        }

        public void Update_IgtFile(string value)
        {
            txtIgtFile.Text = value;
        }
        private void txtIgtFile_TextChanged(object sender, EventArgs e)
        {
            Component.IgtFile = txtIgtFile.Text;
        }

        private void btnIgtFileBrowse_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.FileName = txtIgtFile.Text;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtIgtFile.Text = dlg.FileName;
                }
            }
        }

    }
}


namespace StarCraft2Autosplitter
{
    partial class SettingsForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtBankPath = new System.Windows.Forms.TextBox();
            this.lnkShowBankFile = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCampaign = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIgtFile = new System.Windows.Forms.TextBox();
            this.chkWriteIgtFile = new System.Windows.Forms.CheckBox();
            this.btnIgtFileBrowse = new System.Windows.Forms.Button();
            this.chkIgtUpdate = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bank path";
            // 
            // txtBankPath
            // 
            this.txtBankPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBankPath.Location = new System.Drawing.Point(90, 53);
            this.txtBankPath.Name = "txtBankPath";
            this.txtBankPath.ReadOnly = true;
            this.txtBankPath.Size = new System.Drawing.Size(346, 20);
            this.txtBankPath.TabIndex = 0;
            // 
            // lnkShowBankFile
            // 
            this.lnkShowBankFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkShowBankFile.AutoSize = true;
            this.lnkShowBankFile.Location = new System.Drawing.Point(350, 76);
            this.lnkShowBankFile.Name = "lnkShowBankFile";
            this.lnkShowBankFile.Size = new System.Drawing.Size(86, 13);
            this.lnkShowBankFile.TabIndex = 1;
            this.lnkShowBankFile.TabStop = true;
            this.lnkShowBankFile.Text = "Show in Explorer";
            this.lnkShowBankFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkShowBankFile_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lblCampaign);
            this.groupBox1.Controls.Add(this.txtBankPath);
            this.groupBox1.Controls.Add(this.lnkShowBankFile);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 115);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(449, 125);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current status";
            // 
            // lblCampaign
            // 
            this.lblCampaign.AutoSize = true;
            this.lblCampaign.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCampaign.Location = new System.Drawing.Point(87, 26);
            this.lblCampaign.Name = "lblCampaign";
            this.lblCampaign.Size = new System.Drawing.Size(60, 13);
            this.lblCampaign.TabIndex = 3;
            this.lblCampaign.Text = "Unknown";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Campaign";
            // 
            // txtIgtFile
            // 
            this.txtIgtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIgtFile.Location = new System.Drawing.Point(12, 74);
            this.txtIgtFile.Name = "txtIgtFile";
            this.txtIgtFile.Size = new System.Drawing.Size(368, 20);
            this.txtIgtFile.TabIndex = 2;
            this.txtIgtFile.TextChanged += new System.EventHandler(this.txtIgtFile_TextChanged);
            // 
            // chkWriteIgtFile
            // 
            this.chkWriteIgtFile.AutoSize = true;
            this.chkWriteIgtFile.Checked = true;
            this.chkWriteIgtFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWriteIgtFile.Location = new System.Drawing.Point(12, 52);
            this.chkWriteIgtFile.Name = "chkWriteIgtFile";
            this.chkWriteIgtFile.Size = new System.Drawing.Size(164, 17);
            this.chkWriteIgtFile.TabIndex = 1;
            this.chkWriteIgtFile.Text = "Write IGT mission times to file";
            this.chkWriteIgtFile.UseVisualStyleBackColor = true;
            this.chkWriteIgtFile.CheckedChanged += new System.EventHandler(this.chkWriteIgtFile_CheckedChanged);
            // 
            // btnIgtFileBrowse
            // 
            this.btnIgtFileBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIgtFileBrowse.Location = new System.Drawing.Point(386, 72);
            this.btnIgtFileBrowse.Name = "btnIgtFileBrowse";
            this.btnIgtFileBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnIgtFileBrowse.TabIndex = 3;
            this.btnIgtFileBrowse.Text = "Browse";
            this.btnIgtFileBrowse.UseVisualStyleBackColor = true;
            this.btnIgtFileBrowse.Click += new System.EventHandler(this.btnIgtFileBrowse_Click);
            // 
            // chkIgtUpdate
            // 
            this.chkIgtUpdate.AutoSize = true;
            this.chkIgtUpdate.Checked = true;
            this.chkIgtUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIgtUpdate.Location = new System.Drawing.Point(12, 19);
            this.chkIgtUpdate.Name = "chkIgtUpdate";
            this.chkIgtUpdate.Size = new System.Drawing.Size(116, 17);
            this.chkIgtUpdate.TabIndex = 0;
            this.chkIgtUpdate.Text = "Enable IGT update";
            this.chkIgtUpdate.UseVisualStyleBackColor = true;
            this.chkIgtUpdate.CheckedChanged += new System.EventHandler(this.chkIgtUpdate_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkIgtUpdate);
            this.Controls.Add(this.btnIgtFileBrowse);
            this.Controls.Add(this.chkWriteIgtFile);
            this.Controls.Add(this.txtIgtFile);
            this.Controls.Add(this.groupBox1);
            this.Name = "SettingsForm";
            this.Size = new System.Drawing.Size(476, 294);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBankPath;
        private System.Windows.Forms.LinkLabel lnkShowBankFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtIgtFile;
        private System.Windows.Forms.CheckBox chkWriteIgtFile;
        private System.Windows.Forms.Button btnIgtFileBrowse;
        private System.Windows.Forms.CheckBox chkIgtUpdate;
        private System.Windows.Forms.Label lblCampaign;
        private System.Windows.Forms.Label label2;
    }
}

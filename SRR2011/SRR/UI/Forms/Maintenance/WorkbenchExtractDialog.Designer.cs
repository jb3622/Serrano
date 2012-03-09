namespace Disney.iDash.SRR.UI.Forms.Maintenance
{
    partial class WorkbenchExtractDialog
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
			this.grpExtractType = new DevExpress.XtraEditors.GroupControl();
			this.rgExtract = new DevExpress.XtraEditors.RadioGroup();
			this.btnStart = new DevExpress.XtraEditors.SimpleButton();
			this.btnClose = new DevExpress.XtraEditors.SimpleButton();
			this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
			((System.ComponentModel.ISupportInitialize)(this.ClientPanel)).BeginInit();
			this.ClientPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.BoxStyles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grpExtractType)).BeginInit();
			this.grpExtractType.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rgExtract.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
			this.SuspendLayout();
			// 
			// ClientPanel
			// 
			this.ClientPanel.Controls.Add(this.btnClose);
			this.ClientPanel.Controls.Add(this.btnStart);
			this.ClientPanel.Controls.Add(this.grpExtractType);
			this.ClientPanel.Size = new System.Drawing.Size(500, 152);
			this.ClientPanel.TabIndex = 0;
			// 
			// BoxStyles
			// 
			this.BoxStyles.Appearance.BackColor = System.Drawing.Color.White;
			this.BoxStyles.Appearance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
			this.BoxStyles.Appearance.ForeColor = System.Drawing.Color.Black;
			this.BoxStyles.Appearance.Options.UseBackColor = true;
			this.BoxStyles.Appearance.Options.UseFont = true;
			this.BoxStyles.Appearance.Options.UseForeColor = true;
			this.BoxStyles.AppearanceDisabled.BackColor = System.Drawing.Color.White;
			this.BoxStyles.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
			this.BoxStyles.AppearanceDisabled.ForeColor = System.Drawing.Color.Silver;
			this.BoxStyles.AppearanceDisabled.Options.UseBackColor = true;
			this.BoxStyles.AppearanceDisabled.Options.UseFont = true;
			this.BoxStyles.AppearanceDisabled.Options.UseForeColor = true;
			this.BoxStyles.AppearanceDropDown.BackColor = System.Drawing.Color.White;
			this.BoxStyles.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BoxStyles.AppearanceDropDown.ForeColor = System.Drawing.Color.Black;
			this.BoxStyles.AppearanceDropDown.Options.UseBackColor = true;
			this.BoxStyles.AppearanceDropDown.Options.UseFont = true;
			this.BoxStyles.AppearanceDropDown.Options.UseForeColor = true;
			this.BoxStyles.AppearanceDropDownHeader.BackColor = System.Drawing.Color.White;
			this.BoxStyles.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
			this.BoxStyles.AppearanceDropDownHeader.ForeColor = System.Drawing.Color.Blue;
			this.BoxStyles.AppearanceDropDownHeader.Options.UseBackColor = true;
			this.BoxStyles.AppearanceDropDownHeader.Options.UseFont = true;
			this.BoxStyles.AppearanceDropDownHeader.Options.UseForeColor = true;
			this.BoxStyles.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan;
			this.BoxStyles.AppearanceFocused.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
			this.BoxStyles.AppearanceFocused.ForeColor = System.Drawing.Color.Black;
			this.BoxStyles.AppearanceFocused.Options.UseBackColor = true;
			this.BoxStyles.AppearanceFocused.Options.UseFont = true;
			this.BoxStyles.AppearanceFocused.Options.UseForeColor = true;
			this.BoxStyles.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
			this.BoxStyles.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
			this.BoxStyles.AppearanceReadOnly.ForeColor = System.Drawing.Color.Red;
			this.BoxStyles.AppearanceReadOnly.Options.UseBackColor = true;
			this.BoxStyles.AppearanceReadOnly.Options.UseFont = true;
			this.BoxStyles.AppearanceReadOnly.Options.UseForeColor = true;
			// 
			// grpExtractType
			// 
			this.grpExtractType.AppearanceCaption.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.grpExtractType.AppearanceCaption.Options.UseFont = true;
			this.grpExtractType.Controls.Add(this.rgExtract);
			this.grpExtractType.Location = new System.Drawing.Point(23, 18);
			this.grpExtractType.Name = "grpExtractType";
			this.grpExtractType.Size = new System.Drawing.Size(200, 100);
			this.grpExtractType.TabIndex = 0;
			this.grpExtractType.Text = "Extract Type";
			// 
			// rgExtract
			// 
			this.rgExtract.EditValue = "Daily";
			this.rgExtract.Location = new System.Drawing.Point(5, 27);
			this.rgExtract.Name = "rgExtract";
			this.rgExtract.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.rgExtract.Properties.Appearance.Options.UseBackColor = true;
			this.rgExtract.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Daily", "Daily"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Weekly", "Weekly"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Weekend", "Weekend")});
			this.rgExtract.Size = new System.Drawing.Size(82, 68);
			this.rgExtract.TabIndex = 3;
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(332, 95);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 23);
			this.btnStart.TabIndex = 1;
			this.btnStart.Text = "&Start";
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(413, 95);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// repositoryItemProgressBar1
			// 
			this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
			this.repositoryItemProgressBar1.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
			// 
			// WorkbenchExtractDialog
			// 
			this.Appearance.Font = new System.Drawing.Font("Arial", 8.25F);
			this.Appearance.Options.UseFont = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(500, 206);
			this.ControlBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WorkbenchExtractDialog";
			this.Text = "Planning Workbench Extract";
			((System.ComponentModel.ISupportInitialize)(this.ClientPanel)).EndInit();
			this.ClientPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.BoxStyles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grpExtractType)).EndInit();
			this.grpExtractType.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.rgExtract.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraEditors.GroupControl grpExtractType;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnStart;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
        private DevExpress.XtraEditors.RadioGroup rgExtract;
    }
}

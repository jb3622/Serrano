namespace Disney.iDash.SRR.Controls
{
	partial class LookupCheckedComboControl
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
			this.cboLookup = new DevExpress.XtraEditors.CheckedComboBoxEdit();
			((System.ComponentModel.ISupportInitialize)(this.cboLookup.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// cboLookup
			// 
			this.cboLookup.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cboLookup.Location = new System.Drawing.Point(0, 0);
			this.cboLookup.Name = "cboLookup";
			this.cboLookup.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
			this.cboLookup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
			this.cboLookup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
			this.cboLookup.Size = new System.Drawing.Size(228, 20);
			this.cboLookup.TabIndex = 0;
			this.cboLookup.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cboLookup_ButtonClick);
			this.cboLookup.EditValueChanged += new System.EventHandler(this.cboLookup_EditValueChanged);
			this.cboLookup.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboLookup_KeyPress);
			// 
			// LookupCheckedComboControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.cboLookup);
			this.Name = "LookupCheckedComboControl";
			this.Size = new System.Drawing.Size(228, 20);
			((System.ComponentModel.ISupportInitialize)(this.cboLookup.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraEditors.CheckedComboBoxEdit cboLookup;

	}
}

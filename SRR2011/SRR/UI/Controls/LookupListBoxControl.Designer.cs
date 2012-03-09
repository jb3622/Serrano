namespace Disney.iDash.SRR.Controls
{
    partial class LookupListBoxControl
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
            this.components = new System.ComponentModel.Container();
            this.lstLookup = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.mnuOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuClear = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.lstLookup)).BeginInit();
            this.mnuOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstLookup
            // 
            this.lstLookup.CheckOnClick = true;
            this.lstLookup.ContextMenuStrip = this.mnuOptions;
            this.lstLookup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLookup.Location = new System.Drawing.Point(0, 0);
            this.lstLookup.Name = "lstLookup";
            this.lstLookup.Size = new System.Drawing.Size(248, 187);
            this.lstLookup.TabIndex = 0;
            this.lstLookup.ItemChecking += new DevExpress.XtraEditors.Controls.ItemCheckingEventHandler(this.lstLookup_ItemChecking);
            this.lstLookup.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.lstLookup_ItemCheck);
            // 
            // mnuOptions
            // 
            this.mnuOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClear});
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(153, 48);
            // 
            // mnuClear
            // 
            this.mnuClear.Name = "mnuClear";
            this.mnuClear.Size = new System.Drawing.Size(152, 22);
            this.mnuClear.Text = "&Clear";
            this.mnuClear.Click += new System.EventHandler(this.mnuClear_Click);
            // 
            // LookupListBoxControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstLookup);
            this.Name = "LookupListBoxControl";
            this.Size = new System.Drawing.Size(248, 187);
            ((System.ComponentModel.ISupportInitialize)(this.lstLookup)).EndInit();
            this.mnuOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.CheckedListBoxControl lstLookup;
        private System.Windows.Forms.ContextMenuStrip mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuClear;
    }
}

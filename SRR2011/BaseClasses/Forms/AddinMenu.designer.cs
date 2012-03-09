namespace Disney.iDash.BaseClasses.Forms
{
    partial class AddinMenu
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddinMenu));
			this.addinRibbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.addinRibbon)).BeginInit();
			this.SuspendLayout();
			// 
			// addinRibbon
			// 
			this.addinRibbon.ApplicationButtonText = null;
			// 
			// 
			// 
			this.addinRibbon.ExpandCollapseItem.Id = 0;
			this.addinRibbon.ExpandCollapseItem.Name = "";
			this.addinRibbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.addinRibbon.ExpandCollapseItem});
			this.addinRibbon.Location = new System.Drawing.Point(0, 0);
			this.addinRibbon.MaxItemId = 1;
			this.addinRibbon.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.OnlyWhenMaximized;
			this.addinRibbon.Name = "addinRibbon";
			this.addinRibbon.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010;
			this.addinRibbon.Size = new System.Drawing.Size(554, 54);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(12, 200);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(292, 220);
			this.label1.TabIndex = 5;
			this.label1.Text = resources.GetString("label1.Text");
			this.label1.Visible = false;
			// 
			// AddinMenu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(554, 429);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.addinRibbon);
			this.Name = "AddinMenu";
			this.Ribbon = this.addinRibbon;
			this.Text = "AddinMenu";
			((System.ComponentModel.ISupportInitialize)(this.addinRibbon)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraBars.Ribbon.RibbonControl addinRibbon;
        private System.Windows.Forms.Label label1;
    }
}

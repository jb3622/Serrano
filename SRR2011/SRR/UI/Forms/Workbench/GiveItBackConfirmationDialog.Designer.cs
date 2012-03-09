namespace Disney.iDash.SRR.UI.Forms.Workbench
{
	partial class GiveItBackConfirmationDialog
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.bandFixed = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colItem = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colDescription = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colGrade = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colMarket = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colStore = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandQuantities = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colCurrentTSR = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colNewTSR = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colRingFenced = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colGiveItBack = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ClientPanel)).BeginInit();
            this.ClientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BoxStyles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ClientPanel
            // 
            this.ClientPanel.Controls.Add(this.btnOk);
            this.ClientPanel.Controls.Add(this.gridControl1);
            this.ClientPanel.Size = new System.Drawing.Size(843, 283);
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
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridControl1.Location = new System.Drawing.Point(12, 16);
            this.gridControl1.MainView = this.bandedGridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(819, 221);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.UseEmbeddedNavigator = true;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridView1});
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.Appearance.BandPanel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.bandedGridView1.Appearance.BandPanel.Options.UseFont = true;
            this.bandedGridView1.Appearance.BandPanelBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.bandedGridView1.Appearance.BandPanelBackground.Options.UseBackColor = true;
            this.bandedGridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.bandedGridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.bandedGridView1.BandPanelRowHeight = 20;
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.bandFixed,
            this.bandQuantities});
            this.bandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colItem,
            this.colDescription,
            this.colGrade,
            this.colMarket,
            this.colStore,
            this.colCurrentTSR,
            this.colNewTSR,
            this.colRingFenced,
            this.colGiveItBack});
            this.bandedGridView1.GridControl = this.gridControl1;
            this.bandedGridView1.Name = "bandedGridView1";
            this.bandedGridView1.OptionsBehavior.Editable = false;
            this.bandedGridView1.OptionsCustomization.AllowColumnMoving = false;
            this.bandedGridView1.OptionsCustomization.AllowFilter = false;
            this.bandedGridView1.OptionsCustomization.AllowGroup = false;
            this.bandedGridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.bandedGridView1.OptionsCustomization.AllowSort = false;
            this.bandedGridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.bandedGridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.bandedGridView1.OptionsView.EnableAppearanceOddRow = true;
            this.bandedGridView1.OptionsView.ShowGroupPanel = false;
            // 
            // bandFixed
            // 
            this.bandFixed.Columns.Add(this.colItem);
            this.bandFixed.Columns.Add(this.colDescription);
            this.bandFixed.Columns.Add(this.colGrade);
            this.bandFixed.Columns.Add(this.colMarket);
            this.bandFixed.Columns.Add(this.colStore);
            this.bandFixed.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.bandFixed.Name = "bandFixed";
            this.bandFixed.Width = 373;
            // 
            // colItem
            // 
            this.colItem.AppearanceHeader.Options.UseTextOptions = true;
            this.colItem.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colItem.Caption = "Item";
            this.colItem.FieldName = "Item";
            this.colItem.Name = "colItem";
            this.colItem.Visible = true;
            this.colItem.Width = 138;
            // 
            // colDescription
            // 
            this.colDescription.Caption = "Description";
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Visible = true;
            this.colDescription.Width = 235;
            // 
            // colGrade
            // 
            this.colGrade.AppearanceHeader.Options.UseTextOptions = true;
            this.colGrade.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGrade.Caption = "Grade";
            this.colGrade.FieldName = "Grade";
            this.colGrade.Name = "colGrade";
            this.colGrade.Width = 85;
            // 
            // colMarket
            // 
            this.colMarket.Caption = "Market";
            this.colMarket.FieldName = "Market";
            this.colMarket.Name = "colMarket";
            // 
            // colStore
            // 
            this.colStore.Caption = "Store";
            this.colStore.FieldName = "Store";
            this.colStore.Name = "colStore";
            // 
            // bandQuantities
            // 
            this.bandQuantities.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.bandQuantities.AppearanceHeader.Options.UseFont = true;
            this.bandQuantities.AppearanceHeader.Options.UseTextOptions = true;
            this.bandQuantities.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandQuantities.Caption = "Total Stock Required";
            this.bandQuantities.Columns.Add(this.colCurrentTSR);
            this.bandQuantities.Columns.Add(this.colNewTSR);
            this.bandQuantities.Columns.Add(this.colRingFenced);
            this.bandQuantities.Columns.Add(this.colGiveItBack);
            this.bandQuantities.Name = "bandQuantities";
            this.bandQuantities.Width = 374;
            // 
            // colCurrentTSR
            // 
            this.colCurrentTSR.AppearanceCell.Options.UseTextOptions = true;
            this.colCurrentTSR.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colCurrentTSR.AppearanceHeader.Options.UseTextOptions = true;
            this.colCurrentTSR.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCurrentTSR.Caption = "Current TSR";
            this.colCurrentTSR.DisplayFormat.FormatString = "#,##0";
            this.colCurrentTSR.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colCurrentTSR.FieldName = "CurrentTSR";
            this.colCurrentTSR.Name = "colCurrentTSR";
            this.colCurrentTSR.Visible = true;
            this.colCurrentTSR.Width = 85;
            // 
            // colNewTSR
            // 
            this.colNewTSR.AppearanceCell.Options.UseTextOptions = true;
            this.colNewTSR.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colNewTSR.AppearanceHeader.Options.UseTextOptions = true;
            this.colNewTSR.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNewTSR.Caption = "New TSR";
            this.colNewTSR.DisplayFormat.FormatString = "#,##0";
            this.colNewTSR.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNewTSR.FieldName = "NewTSR";
            this.colNewTSR.Name = "colNewTSR";
            this.colNewTSR.Visible = true;
            this.colNewTSR.Width = 82;
            // 
            // colRingFenced
            // 
            this.colRingFenced.AppearanceHeader.Options.UseTextOptions = true;
            this.colRingFenced.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRingFenced.Caption = "Current Ring Fenced";
            this.colRingFenced.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colRingFenced.FieldName = "RingFenced";
            this.colRingFenced.Name = "colRingFenced";
            this.colRingFenced.Visible = true;
            this.colRingFenced.Width = 125;
            // 
            // colGiveItBack
            // 
            this.colGiveItBack.AppearanceCell.Options.UseTextOptions = true;
            this.colGiveItBack.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colGiveItBack.AppearanceHeader.Options.UseTextOptions = true;
            this.colGiveItBack.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGiveItBack.Caption = "Give It Back";
            this.colGiveItBack.DisplayFormat.FormatString = "#,##0";
            this.colGiveItBack.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGiveItBack.FieldName = "GiveItBackValue";
            this.colGiveItBack.Name = "colGiveItBack";
            this.colGiveItBack.Visible = true;
            this.colGiveItBack.Width = 82;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(383, 249);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(77, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "&Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // GiveItBackConfirmationDialog
            // 
            this.Appearance.Font = new System.Drawing.Font("Arial", 8.25F);
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.ClientSize = new System.Drawing.Size(843, 341);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GiveItBackConfirmationDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Give It Back Confirmation";
            ((System.ComponentModel.ISupportInitialize)(this.ClientPanel)).EndInit();
            this.ClientPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BoxStyles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl1;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colItem;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colGrade;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCurrentTSR;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNewTSR;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colGiveItBack;
        private DevExpress.XtraEditors.SimpleButton btnOk;
		private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colMarket;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colStore;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colDescription;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colRingFenced;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandFixed;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand bandQuantities;
	}
}

namespace Disney.iDash.SRR.UI.Forms.Maintenance
{
    partial class StoreLeadTimeForm
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
			DevExpress.XtraGrid.StyleFormatCondition styleFormatCondition1 = new DevExpress.XtraGrid.StyleFormatCondition();
			this.colLeadTime = new DevExpress.XtraGrid.Columns.GridColumn();
			this.riLeadTime = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.grpSelection = new DevExpress.XtraEditors.GroupControl();
			this.lstMarkets = new Disney.iDash.SRR.Controls.LookupCheckedComboControl();
			this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
			this.gridItems = new DevExpress.XtraGrid.GridControl();
			this.viewItems = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colCountryId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCountry = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colStoreId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colStoreName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.btnClose = new DevExpress.XtraEditors.SimpleButton();
			this.btnSave = new DevExpress.XtraEditors.SimpleButton();
			this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
			this.lblOver21 = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.ClientPanel)).BeginInit();
			this.ClientPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.BoxStyles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.riLeadTime)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grpSelection)).BeginInit();
			this.grpSelection.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.viewItems)).BeginInit();
			this.SuspendLayout();
			// 
			// ClientPanel
			// 
			this.ClientPanel.Controls.Add(this.lblOver21);
			this.ClientPanel.Controls.Add(this.btnCancel);
			this.ClientPanel.Controls.Add(this.btnSave);
			this.ClientPanel.Controls.Add(this.btnClose);
			this.ClientPanel.Controls.Add(this.gridItems);
			this.ClientPanel.Controls.Add(this.grpSelection);
			this.ClientPanel.Size = new System.Drawing.Size(849, 578);
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
			// colLeadTime
			// 
			this.colLeadTime.AppearanceHeader.Options.UseTextOptions = true;
			this.colLeadTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colLeadTime.Caption = "Lead Time";
			this.colLeadTime.ColumnEdit = this.riLeadTime;
			this.colLeadTime.FieldName = "LEADTIME";
			this.colLeadTime.Name = "colLeadTime";
			this.colLeadTime.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
			this.colLeadTime.Visible = true;
			this.colLeadTime.VisibleIndex = 3;
			// 
			// riLeadTime
			// 
			this.riLeadTime.AutoHeight = false;
			this.riLeadTime.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.riLeadTime.IsFloatValue = false;
			this.riLeadTime.Mask.EditMask = "N0";
			this.riLeadTime.Mask.UseMaskAsDisplayFormat = true;
			this.riLeadTime.MaxValue = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.riLeadTime.Name = "riLeadTime";
			this.riLeadTime.Spin += new DevExpress.XtraEditors.Controls.SpinEventHandler(this.riLeadTime_Spin);
			// 
			// grpSelection
			// 
			this.grpSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpSelection.Appearance.Font = new System.Drawing.Font("Arial", 8.25F);
			this.grpSelection.Appearance.Options.UseFont = true;
			this.grpSelection.AppearanceCaption.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.grpSelection.AppearanceCaption.Options.UseFont = true;
			this.grpSelection.Controls.Add(this.lstMarkets);
			this.grpSelection.Controls.Add(this.btnSearch);
			this.grpSelection.Location = new System.Drawing.Point(12, 18);
			this.grpSelection.Name = "grpSelection";
			this.grpSelection.Size = new System.Drawing.Size(825, 58);
			this.grpSelection.TabIndex = 0;
			this.grpSelection.Text = "Select Markets";
			// 
			// lstMarkets
			// 
			this.lstMarkets.AllowedCharacters = "";
			this.lstMarkets.AutoSize = true;
			this.lstMarkets.EditValue = "";
			this.lstMarkets.Location = new System.Drawing.Point(15, 29);
			this.lstMarkets.LookupType = Disney.iDash.SRR.BusinessLayer.LookupSource.LookupTypes.Markets;
			this.lstMarkets.Name = "lstMarkets";
			this.lstMarkets.Size = new System.Drawing.Size(327, 20);
			this.lstMarkets.StyleController = this.BoxStyles;
			this.lstMarkets.TabIndex = 3;
			this.lstMarkets.WhereClause = "";
			this.lstMarkets.EditValueChanged += new System.EventHandler(this.lstMarkets_EditValueChanged);
			// 
			// btnSearch
			// 
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.Enabled = false;
			this.btnSearch.Location = new System.Drawing.Point(757, 27);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(63, 23);
			this.btnSearch.TabIndex = 2;
			this.btnSearch.Text = "Search";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// gridItems
			// 
			this.gridItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridItems.EmbeddedNavigator.Buttons.Append.Visible = false;
			this.gridItems.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
			this.gridItems.EmbeddedNavigator.Buttons.Edit.Visible = false;
			this.gridItems.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
			this.gridItems.EmbeddedNavigator.Buttons.NextPage.Visible = false;
			this.gridItems.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
			this.gridItems.EmbeddedNavigator.Buttons.Remove.Visible = false;
			this.gridItems.Location = new System.Drawing.Point(12, 82);
			this.gridItems.MainView = this.viewItems;
			this.gridItems.Name = "gridItems";
			this.gridItems.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riLeadTime});
			this.gridItems.Size = new System.Drawing.Size(825, 461);
			this.gridItems.TabIndex = 1;
			this.gridItems.UseEmbeddedNavigator = true;
			this.gridItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewItems});
			this.gridItems.Visible = false;
			// 
			// viewItems
			// 
			this.viewItems.Appearance.EvenRow.BackColor = System.Drawing.Color.LightCyan;
			this.viewItems.Appearance.EvenRow.Options.UseBackColor = true;
			this.viewItems.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.viewItems.Appearance.HeaderPanel.Options.UseFont = true;
			this.viewItems.Appearance.OddRow.BackColor = System.Drawing.Color.White;
			this.viewItems.Appearance.OddRow.Options.UseBackColor = true;
			this.viewItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCountryId,
            this.colCountry,
            this.colStoreId,
            this.colStoreName,
            this.colLeadTime});
			styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.Gold;
			styleFormatCondition1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			styleFormatCondition1.Appearance.ForeColor = System.Drawing.Color.Black;
			styleFormatCondition1.Appearance.Options.UseBackColor = true;
			styleFormatCondition1.Appearance.Options.UseFont = true;
			styleFormatCondition1.Appearance.Options.UseForeColor = true;
			styleFormatCondition1.Column = this.colLeadTime;
			styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Greater;
			styleFormatCondition1.Value1 = 21;
			this.viewItems.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
			this.viewItems.GridControl = this.gridItems;
			this.viewItems.Name = "viewItems";
			this.viewItems.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
			this.viewItems.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
			this.viewItems.OptionsView.EnableAppearanceEvenRow = true;
			this.viewItems.OptionsView.EnableAppearanceOddRow = true;
			this.viewItems.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.viewItems_RowCellStyle);
			this.viewItems.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.viewItems_CellValueChanged);
			// 
			// colCountryId
			// 
			this.colCountryId.Caption = "CountryId";
			this.colCountryId.FieldName = "COUNTRYID";
			this.colCountryId.Name = "colCountryId";
			this.colCountryId.OptionsColumn.AllowEdit = false;
			this.colCountryId.OptionsColumn.ShowInCustomizationForm = false;
			// 
			// colCountry
			// 
			this.colCountry.AppearanceHeader.Options.UseTextOptions = true;
			this.colCountry.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCountry.Caption = "Country";
			this.colCountry.FieldName = "COUNTRY";
			this.colCountry.Name = "colCountry";
			this.colCountry.OptionsColumn.AllowEdit = false;
			this.colCountry.Visible = true;
			this.colCountry.VisibleIndex = 0;
			// 
			// colStoreId
			// 
			this.colStoreId.AppearanceHeader.Options.UseTextOptions = true;
			this.colStoreId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colStoreId.Caption = "Store";
			this.colStoreId.FieldName = "STOREID";
			this.colStoreId.Name = "colStoreId";
			this.colStoreId.OptionsColumn.AllowEdit = false;
			this.colStoreId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
			this.colStoreId.Visible = true;
			this.colStoreId.VisibleIndex = 1;
			// 
			// colStoreName
			// 
			this.colStoreName.AppearanceHeader.Options.UseTextOptions = true;
			this.colStoreName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colStoreName.Caption = "Store Name";
			this.colStoreName.FieldName = "STORENAME";
			this.colStoreName.Name = "colStoreName";
			this.colStoreName.OptionsColumn.AllowEdit = false;
			this.colStoreName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
			this.colStoreName.Visible = true;
			this.colStoreName.VisibleIndex = 2;
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(774, 549);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(63, 23);
			this.btnClose.TabIndex = 4;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(636, 549);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(63, 23);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(705, 549);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(63, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "C&ancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lblOver21
			// 
			this.lblOver21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblOver21.Appearance.BackColor = System.Drawing.Color.Gold;
			this.lblOver21.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.lblOver21.Location = new System.Drawing.Point(12, 552);
			this.lblOver21.Name = "lblOver21";
			this.lblOver21.Padding = new System.Windows.Forms.Padding(3);
			this.lblOver21.Size = new System.Drawing.Size(299, 19);
			this.lblOver21.TabIndex = 3;
			this.lblOver21.Text = "Lead times greater than 21 are shown in this colour.";
			// 
			// StoreLeadTimeForm
			// 
			this.Appearance.Font = new System.Drawing.Font("Arial", 8.25F);
			this.Appearance.Options.UseFont = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(849, 632);
			this.Name = "StoreLeadTimeForm";
			this.Text = "Store Lead Time";
			((System.ComponentModel.ISupportInitialize)(this.ClientPanel)).EndInit();
			this.ClientPanel.ResumeLayout(false);
			this.ClientPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.BoxStyles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.riLeadTime)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grpSelection)).EndInit();
			this.grpSelection.ResumeLayout(false);
			this.grpSelection.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.viewItems)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpSelection;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraGrid.GridControl gridItems;
		private DevExpress.XtraGrid.Views.Grid.GridView viewItems;
        private DevExpress.XtraGrid.Columns.GridColumn colCountryId;
        private DevExpress.XtraGrid.Columns.GridColumn colCountry;
        private DevExpress.XtraGrid.Columns.GridColumn colStoreId;
        private DevExpress.XtraGrid.Columns.GridColumn colStoreName;
        private DevExpress.XtraGrid.Columns.GridColumn colLeadTime;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit riLeadTime;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl lblOver21;
		private Controls.LookupCheckedComboControl lstMarkets;
    }
}

namespace Disney.iDash.SRR.UI.Forms.Workbench
{
    partial class DetailedParametersForm
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
			this.riProgress = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
			this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
			this.btnExit = new DevExpress.XtraEditors.SimpleButton();
			this.grpSummaryLevel = new DevExpress.XtraEditors.GroupControl();
			this.rgParameter = new DevExpress.XtraEditors.RadioGroup();
			this.grpItemDetails = new DevExpress.XtraEditors.GroupControl();
			this.lblCheckedCount = new DevExpress.XtraEditors.LabelControl();
			this.txtUPC = new DevExpress.XtraEditors.TextEdit();
			this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
			this.cboEventCode = new Disney.iDash.SRR.Controls.LookupComboControl();
			this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
			this.lstPromotions = new Disney.iDash.SRR.Controls.LookupListBoxControl();
			this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
			this.txtCoordinateGroup = new DevExpress.XtraEditors.TextEdit();
			this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
			this.cboFranchise = new Disney.iDash.SRR.Controls.LookupComboControl();
			this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
			this.ctrlItem = new Disney.iDash.SRR.Controls.StockItemControl();
			this.grpLocation = new DevExpress.XtraEditors.GroupControl();
			this.cboStoreGroup = new Disney.iDash.SRR.Controls.LookupComboControl();
			this.cboMarket = new Disney.iDash.SRR.Controls.LookupComboControl();
			this.labelControl13 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
			this.cboStoreGrade = new Disney.iDash.SRR.Controls.LookupComboControl();
			this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
			this.cboStore = new Disney.iDash.SRR.Controls.LookupComboControl();
			this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
			this.grpOther = new DevExpress.XtraEditors.GroupControl();
			this.chkExcludeItemsWithZeroStock = new DevExpress.XtraEditors.CheckEdit();
			this.chkShowProposedMarkedDownLines = new DevExpress.XtraEditors.CheckEdit();
			this.chkShowMarkDownLines = new DevExpress.XtraEditors.CheckEdit();
			this.chkShowFullPriceLines = new DevExpress.XtraEditors.CheckEdit();
			this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
			this.DepartmentSelector = new Disney.iDash.SRR.Controls.DepartmentSelectorControl();
			this.btnView = new DevExpress.XtraEditors.SimpleButton();
			this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
			this.btnReset = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.ClientPanel)).BeginInit();
			this.ClientPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.BoxStyles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.riProgress)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grpSummaryLevel)).BeginInit();
			this.grpSummaryLevel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.rgParameter.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grpItemDetails)).BeginInit();
			this.grpItemDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtUPC.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCoordinateGroup.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grpLocation)).BeginInit();
			this.grpLocation.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grpOther)).BeginInit();
			this.grpOther.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.chkExcludeItemsWithZeroStock.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chkShowProposedMarkedDownLines.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chkShowMarkDownLines.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chkShowFullPriceLines.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
			this.groupControl1.SuspendLayout();
			this.SuspendLayout();
			// 
			// ClientPanel
			// 
			this.ClientPanel.Controls.Add(this.btnReset);
			this.ClientPanel.Controls.Add(this.btnView);
			this.ClientPanel.Controls.Add(this.btnEdit);
			this.ClientPanel.Controls.Add(this.groupControl1);
			this.ClientPanel.Controls.Add(this.grpOther);
			this.ClientPanel.Controls.Add(this.grpLocation);
			this.ClientPanel.Controls.Add(this.grpItemDetails);
			this.ClientPanel.Controls.Add(this.grpSummaryLevel);
			this.ClientPanel.Controls.Add(this.btnExit);
			this.ClientPanel.Location = new System.Drawing.Point(0, 31);
			this.ClientPanel.Size = new System.Drawing.Size(699, 591);
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
			// barStatus
			// 
			this.barStatus.Appearance.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
			this.barStatus.Appearance.Options.UseFont = true;
			// 
			// ribbonStatusBar
			// 
			this.ribbonStatusBar.Location = new System.Drawing.Point(0, 622);
			this.ribbonStatusBar.Size = new System.Drawing.Size(699, 23);
			// 
			// riProgress
			// 
			this.riProgress.AutoHeight = true;
			this.riProgress.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
			this.riProgress.Name = "riProgress";
			this.riProgress.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
			this.riProgress.ShowTitle = true;
			// 
			// ribbon
			// 
			this.ribbon.ApplicationButtonText = null;
			// 
			// 
			// 
			this.ribbon.ExpandCollapseItem.Id = 0;
			this.ribbon.ExpandCollapseItem.Name = "";
			this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem});
			this.ribbon.Location = new System.Drawing.Point(0, 0);
			this.ribbon.MaxItemId = 4;
			this.ribbon.Name = "ribbon";
			this.ribbon.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riProgress});
			this.ribbon.Size = new System.Drawing.Size(699, 25);
			this.ribbon.StatusBar = this.ribbonStatusBar;
			// 
			// btnExit
			// 
			this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnExit.Location = new System.Drawing.Point(626, 551);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(56, 23);
			this.btnExit.TabIndex = 8;
			this.btnExit.Text = "E&xit";
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// grpSummaryLevel
			// 
			this.grpSummaryLevel.AppearanceCaption.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.grpSummaryLevel.AppearanceCaption.Options.UseFont = true;
			this.grpSummaryLevel.Controls.Add(this.rgParameter);
			this.grpSummaryLevel.Location = new System.Drawing.Point(12, 111);
			this.grpSummaryLevel.Name = "grpSummaryLevel";
			this.grpSummaryLevel.Size = new System.Drawing.Size(341, 145);
			this.grpSummaryLevel.TabIndex = 1;
			this.grpSummaryLevel.Text = "Summary Level";
			// 
			// rgParameter
			// 
			this.rgParameter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rgParameter.Location = new System.Drawing.Point(2, 24);
			this.rgParameter.Name = "rgParameter";
			this.rgParameter.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.rgParameter.Properties.Appearance.Options.UseBackColor = true;
			this.rgParameter.Properties.Columns = 2;
			this.rgParameter.Size = new System.Drawing.Size(337, 119);
			this.rgParameter.TabIndex = 0;
			this.rgParameter.EditValueChanged += new System.EventHandler(this.rgSummaryLevel_EditValueChanged);
			// 
			// grpItemDetails
			// 
			this.grpItemDetails.AppearanceCaption.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.grpItemDetails.AppearanceCaption.Options.UseFont = true;
			this.grpItemDetails.Controls.Add(this.lblCheckedCount);
			this.grpItemDetails.Controls.Add(this.txtUPC);
			this.grpItemDetails.Controls.Add(this.labelControl9);
			this.grpItemDetails.Controls.Add(this.cboEventCode);
			this.grpItemDetails.Controls.Add(this.labelControl8);
			this.grpItemDetails.Controls.Add(this.lstPromotions);
			this.grpItemDetails.Controls.Add(this.labelControl7);
			this.grpItemDetails.Controls.Add(this.txtCoordinateGroup);
			this.grpItemDetails.Controls.Add(this.labelControl6);
			this.grpItemDetails.Controls.Add(this.cboFranchise);
			this.grpItemDetails.Controls.Add(this.labelControl5);
			this.grpItemDetails.Controls.Add(this.labelControl4);
			this.grpItemDetails.Controls.Add(this.ctrlItem);
			this.grpItemDetails.Location = new System.Drawing.Point(12, 271);
			this.grpItemDetails.Name = "grpItemDetails";
			this.grpItemDetails.Size = new System.Drawing.Size(424, 303);
			this.grpItemDetails.TabIndex = 3;
			this.grpItemDetails.Text = "Item Details";
			// 
			// lblCheckedCount
			// 
			this.lblCheckedCount.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.lblCheckedCount.Appearance.Font = new System.Drawing.Font("Arial", 8.25F);
			this.lblCheckedCount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.lblCheckedCount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.lblCheckedCount.Location = new System.Drawing.Point(277, 84);
			this.lblCheckedCount.Name = "lblCheckedCount";
			this.lblCheckedCount.Size = new System.Drawing.Size(136, 14);
			this.lblCheckedCount.TabIndex = 12;
			this.lblCheckedCount.Text = "0 selected";
			// 
			// txtUPC
			// 
			this.txtUPC.Location = new System.Drawing.Point(103, 275);
			this.txtUPC.Name = "txtUPC";
			this.txtUPC.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.txtUPC.Properties.Appearance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
			this.txtUPC.Properties.Appearance.Options.UseBackColor = true;
			this.txtUPC.Properties.Appearance.Options.UseFont = true;
			this.txtUPC.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtUPC.Properties.MaxLength = 12;
			this.txtUPC.Size = new System.Drawing.Size(87, 20);
			this.txtUPC.StyleController = this.BoxStyles;
			this.txtUPC.TabIndex = 11;
			// 
			// labelControl9
			// 
			this.labelControl9.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControl9.Location = new System.Drawing.Point(13, 278);
			this.labelControl9.Name = "labelControl9";
			this.labelControl9.Size = new System.Drawing.Size(20, 13);
			this.labelControl9.TabIndex = 10;
			this.labelControl9.Text = "UPC";
			// 
			// cboEventCode
			// 
			this.cboEventCode.AllowedCharacters = "";

			this.cboEventCode.AutoSize = true;
			this.cboEventCode.EditValue = null;

			this.cboEventCode.Location = new System.Drawing.Point(103, 253);
			this.cboEventCode.LookupType = Disney.iDash.SRR.BusinessLayer.LookupSource.LookupTypes.EventCodes;
			this.cboEventCode.MaxLength = 0;
			this.cboEventCode.Name = "cboEventCode";
			this.cboEventCode.Size = new System.Drawing.Size(310, 20);
			this.cboEventCode.StyleController = this.BoxStyles;
			this.cboEventCode.TabIndex = 9;
			this.cboEventCode.WhereClause = "";
			// 
			// labelControl8
			// 
			this.labelControl8.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControl8.Location = new System.Drawing.Point(13, 253);
			this.labelControl8.Name = "labelControl8";
			this.labelControl8.Size = new System.Drawing.Size(56, 13);
			this.labelControl8.TabIndex = 8;
			this.labelControl8.Text = "Event Code";
			// 
			// lstPromotions
			// 
			this.lstPromotions.Location = new System.Drawing.Point(103, 101);
			this.lstPromotions.LookupType = Disney.iDash.SRR.BusinessLayer.LookupSource.LookupTypes.Promotions;
			this.lstPromotions.MaxSelections = 10;
			this.lstPromotions.Name = "lstPromotions";
			this.lstPromotions.SelectedIndex = -1;
			this.lstPromotions.SelectedItem = null;
			this.lstPromotions.SelectedValue = null;
			this.lstPromotions.Size = new System.Drawing.Size(310, 150);
			this.lstPromotions.StyleController = this.BoxStyles;
			this.lstPromotions.TabIndex = 7;
			this.lstPromotions.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.lstPromotions_ItemCheck);
			// 
			// labelControl7
			// 
			this.labelControl7.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControl7.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
			this.labelControl7.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControl7.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControl7.Location = new System.Drawing.Point(13, 104);
			this.labelControl7.Name = "labelControl7";
			this.labelControl7.Size = new System.Drawing.Size(85, 33);
			this.labelControl7.TabIndex = 6;
			this.labelControl7.Text = "Promotions\r\n(10 max)";
			// 
			// txtCoordinateGroup
			// 
			this.txtCoordinateGroup.Location = new System.Drawing.Point(103, 79);
			this.txtCoordinateGroup.Name = "txtCoordinateGroup";
			this.txtCoordinateGroup.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.txtCoordinateGroup.Properties.Appearance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
			this.txtCoordinateGroup.Properties.Appearance.Options.UseBackColor = true;
			this.txtCoordinateGroup.Properties.Appearance.Options.UseFont = true;
			this.txtCoordinateGroup.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtCoordinateGroup.Properties.MaxLength = 4;
			this.txtCoordinateGroup.Size = new System.Drawing.Size(59, 20);
			this.txtCoordinateGroup.StyleController = this.BoxStyles;
			this.txtCoordinateGroup.TabIndex = 5;
			// 
			// labelControl6
			// 
			this.labelControl6.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControl6.Location = new System.Drawing.Point(13, 85);
			this.labelControl6.Name = "labelControl6";
			this.labelControl6.Size = new System.Drawing.Size(85, 13);
			this.labelControl6.TabIndex = 4;
			this.labelControl6.Text = "Coordinate Group";
			// 
			// cboFranchise
			// 
			this.cboFranchise.AllowedCharacters = "";

			this.cboFranchise.AutoSize = true;
			this.cboFranchise.EditValue = null;

			this.cboFranchise.Location = new System.Drawing.Point(103, 58);
			this.cboFranchise.LookupType = Disney.iDash.SRR.BusinessLayer.LookupSource.LookupTypes.Franchises;
			this.cboFranchise.MaxLength = 0;
			this.cboFranchise.Name = "cboFranchise";
			this.cboFranchise.Size = new System.Drawing.Size(310, 20);
			this.cboFranchise.StyleController = this.BoxStyles;
			this.cboFranchise.TabIndex = 3;
			this.cboFranchise.WhereClause = "";
			// 
			// labelControl5
			// 
			this.labelControl5.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControl5.Location = new System.Drawing.Point(13, 60);
			this.labelControl5.Name = "labelControl5";
			this.labelControl5.Size = new System.Drawing.Size(46, 13);
			this.labelControl5.TabIndex = 2;
			this.labelControl5.Text = "Franchise";
			// 
			// labelControl4
			// 
			this.labelControl4.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControl4.Location = new System.Drawing.Point(13, 38);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(22, 13);
			this.labelControl4.TabIndex = 0;
			this.labelControl4.Text = "Item";
			// 
			// ctrlItem
			// 
			this.ctrlItem.AutoSize = true;
			this.ctrlItem.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ctrlItem.DepartmentId = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.ctrlItem.ItemClass = null;
			this.ctrlItem.ItemColour = null;
			this.ctrlItem.ItemColourVisible = true;
			this.ctrlItem.ItemSize = null;
			this.ctrlItem.ItemSizeVisible = true;
			this.ctrlItem.ItemStyle = null;
			this.ctrlItem.ItemStyleVisible = true;
			this.ctrlItem.ItemVendor = null;
			this.ctrlItem.Location = new System.Drawing.Point(103, 34);
			this.ctrlItem.Margin = new System.Windows.Forms.Padding(0);
			this.ctrlItem.MaximumSize = new System.Drawing.Size(280, 20);
			this.ctrlItem.MinimumSize = new System.Drawing.Size(280, 20);
			this.ctrlItem.Name = "ctrlItem";
			this.ctrlItem.ShowZeros = false;
			this.ctrlItem.Size = new System.Drawing.Size(280, 20);
			this.ctrlItem.StyleController = this.BoxStyles;
			this.ctrlItem.TabIndex = 1;
			// 
			// grpLocation
			// 
			this.grpLocation.AppearanceCaption.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.grpLocation.AppearanceCaption.Options.UseFont = true;
			this.grpLocation.Controls.Add(this.cboStoreGroup);
			this.grpLocation.Controls.Add(this.cboMarket);
			this.grpLocation.Controls.Add(this.labelControl13);
			this.grpLocation.Controls.Add(this.labelControl12);
			this.grpLocation.Controls.Add(this.cboStoreGrade);
			this.grpLocation.Controls.Add(this.labelControl11);
			this.grpLocation.Controls.Add(this.cboStore);
			this.grpLocation.Controls.Add(this.labelControl10);
			this.grpLocation.Location = new System.Drawing.Point(368, 111);
			this.grpLocation.Name = "grpLocation";
			this.grpLocation.Size = new System.Drawing.Size(318, 145);
			this.grpLocation.TabIndex = 2;
			this.grpLocation.Text = "Location";
			// 
			// cboStoreGroup
			// 
			this.cboStoreGroup.AllowedCharacters = "";
			this.cboStoreGroup.AutoSize = true;
			this.cboStoreGroup.EditValue = null;

			this.cboStoreGroup.Location = new System.Drawing.Point(78, 90);
			this.cboStoreGroup.LookupType = Disney.iDash.SRR.BusinessLayer.LookupSource.LookupTypes.StoreGroups;
			this.cboStoreGroup.MaxLength = 0;
			this.cboStoreGroup.Name = "cboStoreGroup";
			this.cboStoreGroup.Size = new System.Drawing.Size(228, 20);
			this.cboStoreGroup.StyleController = this.BoxStyles;
			this.cboStoreGroup.TabIndex = 5;
			this.cboStoreGroup.WhereClause = "";
			// 
			// cboMarket
			// 
			this.cboMarket.AllowedCharacters = "";
			this.cboMarket.AutoSize = true;
			this.cboMarket.EditValue = null;
			this.cboMarket.Location = new System.Drawing.Point(78, 116);
			this.cboMarket.LookupType = Disney.iDash.SRR.BusinessLayer.LookupSource.LookupTypes.Markets;
			this.cboMarket.MaxLength = 0;
			this.cboMarket.Name = "cboMarket";
			this.cboMarket.Size = new System.Drawing.Size(228, 20);
			this.cboMarket.StyleController = this.BoxStyles;
			this.cboMarket.TabIndex = 7;
			this.cboMarket.WhereClause = "";
			// 
			// labelControl13
			// 
			this.labelControl13.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControl13.Location = new System.Drawing.Point(14, 116);
			this.labelControl13.Name = "labelControl13";
			this.labelControl13.Size = new System.Drawing.Size(33, 13);
			this.labelControl13.TabIndex = 6;
			this.labelControl13.Text = "Market";
			// 
			// labelControl12
			// 
			this.labelControl12.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControl12.Location = new System.Drawing.Point(14, 90);
			this.labelControl12.Name = "labelControl12";
			this.labelControl12.Size = new System.Drawing.Size(58, 13);
			this.labelControl12.TabIndex = 4;
			this.labelControl12.Text = "Store Group";
			// 
			// cboStoreGrade
			// 
			this.cboStoreGrade.AllowedCharacters = "";

			this.cboStoreGrade.AutoSize = true;
			this.cboStoreGrade.EditValue = null;

			this.cboStoreGrade.Location = new System.Drawing.Point(78, 64);
			this.cboStoreGrade.LookupType = Disney.iDash.SRR.BusinessLayer.LookupSource.LookupTypes.StoreGrades;
			this.cboStoreGrade.MaxLength = 0;
			this.cboStoreGrade.Name = "cboStoreGrade";
			this.cboStoreGrade.Size = new System.Drawing.Size(228, 20);
			this.cboStoreGrade.StyleController = this.BoxStyles;
			this.cboStoreGrade.TabIndex = 3;
			this.cboStoreGrade.WhereClause = "";
			// 
			// labelControl11
			// 
			this.labelControl11.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControl11.Location = new System.Drawing.Point(14, 64);
			this.labelControl11.Name = "labelControl11";
			this.labelControl11.Size = new System.Drawing.Size(58, 13);
			this.labelControl11.TabIndex = 2;
			this.labelControl11.Text = "Store Grade";
			// 
			// cboStore
			// 
			this.cboStore.AllowedCharacters = "";
			this.cboStore.AutoSize = true;
			this.cboStore.EditValue = null;

			this.cboStore.Location = new System.Drawing.Point(78, 38);
			this.cboStore.LookupType = Disney.iDash.SRR.BusinessLayer.LookupSource.LookupTypes.Stores;
			this.cboStore.MaxLength = 0;
			this.cboStore.Name = "cboStore";
			this.cboStore.Size = new System.Drawing.Size(228, 20);
			this.cboStore.StyleController = this.BoxStyles;
			this.cboStore.TabIndex = 1;
			this.cboStore.WhereClause = "";
			// 
			// labelControl10
			// 
			this.labelControl10.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControl10.Location = new System.Drawing.Point(14, 38);
			this.labelControl10.Name = "labelControl10";
			this.labelControl10.Size = new System.Drawing.Size(26, 13);
			this.labelControl10.TabIndex = 0;
			this.labelControl10.Text = "Store";
			// 
			// grpOther
			// 
			this.grpOther.AppearanceCaption.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.grpOther.AppearanceCaption.Options.UseFont = true;
			this.grpOther.Controls.Add(this.chkExcludeItemsWithZeroStock);
			this.grpOther.Controls.Add(this.chkShowProposedMarkedDownLines);
			this.grpOther.Controls.Add(this.chkShowMarkDownLines);
			this.grpOther.Controls.Add(this.chkShowFullPriceLines);
			this.grpOther.Location = new System.Drawing.Point(442, 271);
			this.grpOther.Name = "grpOther";
			this.grpOther.Size = new System.Drawing.Size(244, 128);
			this.grpOther.TabIndex = 4;
			this.grpOther.Text = "Other";
			// 
			// chkExcludeItemsWithZeroStock
			// 
			this.chkExcludeItemsWithZeroStock.EditValue = true;
			this.chkExcludeItemsWithZeroStock.Location = new System.Drawing.Point(12, 102);
			this.chkExcludeItemsWithZeroStock.Name = "chkExcludeItemsWithZeroStock";
			this.chkExcludeItemsWithZeroStock.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.chkExcludeItemsWithZeroStock.Properties.Appearance.Options.UseBackColor = true;
			this.chkExcludeItemsWithZeroStock.Properties.Caption = "Exclude items with zero stock at the EDC";
			this.chkExcludeItemsWithZeroStock.Size = new System.Drawing.Size(224, 19);
			this.chkExcludeItemsWithZeroStock.TabIndex = 3;
			// 
			// chkShowProposedMarkedDownLines
			// 
			this.chkShowProposedMarkedDownLines.Location = new System.Drawing.Point(12, 77);
			this.chkShowProposedMarkedDownLines.Name = "chkShowProposedMarkedDownLines";
			this.chkShowProposedMarkedDownLines.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.chkShowProposedMarkedDownLines.Properties.Appearance.Options.UseBackColor = true;
			this.chkShowProposedMarkedDownLines.Properties.Caption = "Show proposed marked down lines";
			this.chkShowProposedMarkedDownLines.Size = new System.Drawing.Size(192, 19);
			this.chkShowProposedMarkedDownLines.TabIndex = 2;
			// 
			// chkShowMarkDownLines
			// 
			this.chkShowMarkDownLines.Location = new System.Drawing.Point(12, 52);
			this.chkShowMarkDownLines.Name = "chkShowMarkDownLines";
			this.chkShowMarkDownLines.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.chkShowMarkDownLines.Properties.Appearance.Options.UseBackColor = true;
			this.chkShowMarkDownLines.Properties.Caption = "Show mark down lines";
			this.chkShowMarkDownLines.Size = new System.Drawing.Size(138, 19);
			this.chkShowMarkDownLines.TabIndex = 1;
			// 
			// chkShowFullPriceLines
			// 
			this.chkShowFullPriceLines.EditValue = true;
			this.chkShowFullPriceLines.Location = new System.Drawing.Point(12, 27);
			this.chkShowFullPriceLines.Name = "chkShowFullPriceLines";
			this.chkShowFullPriceLines.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.chkShowFullPriceLines.Properties.Appearance.Options.UseBackColor = true;
			this.chkShowFullPriceLines.Properties.Caption = "Show full price lines";
			this.chkShowFullPriceLines.Size = new System.Drawing.Size(120, 19);
			this.chkShowFullPriceLines.TabIndex = 0;
			// 
			// groupControl1
			// 
			this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.groupControl1.AppearanceCaption.Options.UseFont = true;
			this.groupControl1.Controls.Add(this.DepartmentSelector);
			this.groupControl1.Location = new System.Drawing.Point(12, 17);
			this.groupControl1.Name = "groupControl1";
			this.groupControl1.Size = new System.Drawing.Size(672, 88);
			this.groupControl1.TabIndex = 0;
			this.groupControl1.Text = "Select Department";
			// 
			// DepartmentSelector
			// 
			this.DepartmentSelector.DepartmentId = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
			this.DepartmentSelector.Location = new System.Drawing.Point(13, 33);
			this.DepartmentSelector.Margin = new System.Windows.Forms.Padding(0);
			this.DepartmentSelector.Name = "DepartmentSelector";
			this.DepartmentSelector.Size = new System.Drawing.Size(649, 39);
			this.DepartmentSelector.StoreType = Disney.iDash.SRR.BusinessLayer.Constants.StoreTypes.BricksAndMortar;
			this.DepartmentSelector.StyleController = this.BoxStyles;
			this.DepartmentSelector.TabIndex = 0;
			this.DepartmentSelector.Workbench = Disney.iDash.SRR.BusinessLayer.Constants.Workbenches.Daily;
			this.DepartmentSelector.EditValueChanged += new Disney.iDash.SRR.Controls.DepartmentSelectorControl.ControlHandler(this.DepartmentSelector_EditValueChanged);
			this.DepartmentSelector.ComboLeave += new Disney.iDash.SRR.Controls.DepartmentSelectorControl.ControlHandler(this.DepartmentSelector_ComboLeave);
			// 
			// btnView
			// 
			this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnView.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnView.Location = new System.Drawing.Point(446, 551);
			this.btnView.Name = "btnView";
			this.btnView.Size = new System.Drawing.Size(56, 23);
			this.btnView.TabIndex = 5;
			this.btnView.Text = "&View";
			this.btnView.ToolTip = "View workbench selection.  You can \r\nsubsequently switch to edit mode if \r\nyou wa" +
    "nt.";
			this.btnView.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
			this.btnView.ToolTipTitle = "View Workbench";
			this.btnView.Click += new System.EventHandler(this.btnView_Click);
			// 
			// btnEdit
			// 
			this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEdit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnEdit.Location = new System.Drawing.Point(506, 551);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(56, 23);
			this.btnEdit.TabIndex = 6;
			this.btnEdit.Text = "&Edit";
			this.btnEdit.ToolTip = "Workbench will be shown in\r\nedit mode.  Select this option\r\nif you are sure you w" +
    "ill be making\r\nchanges to levers.";
			this.btnEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
			this.btnEdit.ToolTipTitle = "Edit Workbench";
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnReset
			// 
			this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReset.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnReset.Location = new System.Drawing.Point(566, 551);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(56, 23);
			this.btnReset.TabIndex = 7;
			this.btnReset.Text = "&Reset";
			this.btnReset.ToolTip = "Clear all selection criteria";
			this.btnReset.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
			this.btnReset.ToolTipTitle = "Reset Form";
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// DetailedParametersForm
			// 
			this.Appearance.Font = new System.Drawing.Font("Arial", 8.25F);
			this.Appearance.Options.UseFont = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
			this.CancelButton = this.btnExit;
			this.ClientSize = new System.Drawing.Size(699, 645);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "DetailedParametersForm";
			this.Text = "Detailed Allocators Workbench - Parameters";
			this.Shown += new System.EventHandler(this.DetailedParametersForm_Shown);
			((System.ComponentModel.ISupportInitialize)(this.ClientPanel)).EndInit();
			this.ClientPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.BoxStyles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.riProgress)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grpSummaryLevel)).EndInit();
			this.grpSummaryLevel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.rgParameter.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grpItemDetails)).EndInit();
			this.grpItemDetails.ResumeLayout(false);
			this.grpItemDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtUPC.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtCoordinateGroup.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grpLocation)).EndInit();
			this.grpLocation.ResumeLayout(false);
			this.grpLocation.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.grpOther)).EndInit();
			this.grpOther.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.chkExcludeItemsWithZeroStock.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chkShowProposedMarkedDownLines.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chkShowMarkDownLines.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chkShowFullPriceLines.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
			this.groupControl1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.GroupControl grpSummaryLevel;
        private DevExpress.XtraEditors.RadioGroup rgParameter;
        private DevExpress.XtraEditors.GroupControl grpOther;
        private DevExpress.XtraEditors.GroupControl grpLocation;
        private DevExpress.XtraEditors.GroupControl grpItemDetails;
        private DevExpress.XtraEditors.CheckEdit chkExcludeItemsWithZeroStock;
        private DevExpress.XtraEditors.CheckEdit chkShowProposedMarkedDownLines;
        private DevExpress.XtraEditors.CheckEdit chkShowMarkDownLines;
        private DevExpress.XtraEditors.CheckEdit chkShowFullPriceLines;
        private Controls.LookupComboControl cboFranchise;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private Controls.StockItemControl ctrlItem;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit txtCoordinateGroup;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private Controls.LookupComboControl cboEventCode;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private Controls.LookupListBoxControl lstPromotions;
        private DevExpress.XtraEditors.TextEdit txtUPC;
        private DevExpress.XtraEditors.LabelControl labelControl9;
		private DevExpress.XtraEditors.LabelControl labelControl13;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private Controls.LookupComboControl cboStoreGrade;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private Controls.LookupComboControl cboStore;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl lblCheckedCount;
        private Controls.LookupComboControl cboMarket;
        private DevExpress.XtraEditors.GroupControl groupControl1;
		private Controls.DepartmentSelectorControl DepartmentSelector;
		private DevExpress.XtraEditors.SimpleButton btnView;
		private DevExpress.XtraEditors.SimpleButton btnEdit;
		private Controls.LookupComboControl cboStoreGroup;
		private DevExpress.XtraEditors.SimpleButton btnReset;
		private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
		private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar riProgress;
    }
}

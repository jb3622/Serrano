namespace Disney.iDash.SRR.UI.Forms.Maintenance
{
    partial class StockItemMinDisplayQtyForm
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
			this.grpSelection = new DevExpress.XtraEditors.GroupControl();
			this.rbUPC = new System.Windows.Forms.RadioButton();
			this.rbStockItem = new System.Windows.Forms.RadioButton();
			this.txtUPC = new DevExpress.XtraEditors.TextEdit();
			this.spinMinDisplayQty = new DevExpress.XtraEditors.SpinEdit();
			this.ctrlItem = new Disney.iDash.SRR.Controls.StockItemControl();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
			this.cboDept = new Disney.iDash.SRR.Controls.LookupComboControl();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.gridItems = new DevExpress.XtraGrid.GridControl();
			this.viewItems = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colDepartment = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colClass = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colVendor = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colStyle = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colColour = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colSize = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colUPC = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colMinDistLot = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colMinDispQty = new DevExpress.XtraGrid.Columns.GridColumn();
			this.riMinDispQty = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.btnClose = new DevExpress.XtraEditors.SimpleButton();
			this.btnSave = new DevExpress.XtraEditors.SimpleButton();
			this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.ClientPanel)).BeginInit();
			this.ClientPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.BoxStyles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.grpSelection)).BeginInit();
			this.grpSelection.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtUPC.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spinMinDisplayQty.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.viewItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.riMinDispQty)).BeginInit();
			this.SuspendLayout();
			// 
			// ClientPanel
			// 
			this.ClientPanel.Controls.Add(this.btnCancel);
			this.ClientPanel.Controls.Add(this.btnSave);
			this.ClientPanel.Controls.Add(this.btnClose);
			this.ClientPanel.Controls.Add(this.gridItems);
			this.ClientPanel.Controls.Add(this.grpSelection);
			this.ClientPanel.Size = new System.Drawing.Size(869, 445);
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
			// grpSelection
			// 
			this.grpSelection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpSelection.Appearance.Font = new System.Drawing.Font("Arial", 8.25F);
			this.grpSelection.Appearance.Options.UseFont = true;
			this.grpSelection.AppearanceCaption.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
			this.grpSelection.AppearanceCaption.Options.UseFont = true;
			this.grpSelection.Controls.Add(this.rbUPC);
			this.grpSelection.Controls.Add(this.rbStockItem);
			this.grpSelection.Controls.Add(this.txtUPC);
			this.grpSelection.Controls.Add(this.spinMinDisplayQty);
			this.grpSelection.Controls.Add(this.ctrlItem);
			this.grpSelection.Controls.Add(this.labelControl3);
			this.grpSelection.Controls.Add(this.btnSearch);
			this.grpSelection.Controls.Add(this.cboDept);
			this.grpSelection.Controls.Add(this.labelControl1);
			this.grpSelection.Location = new System.Drawing.Point(12, 18);
			this.grpSelection.Name = "grpSelection";
			this.grpSelection.Size = new System.Drawing.Size(845, 81);
			this.grpSelection.TabIndex = 0;
			this.grpSelection.Text = "Criteria Selection";
			// 
			// rbUPC
			// 
			this.rbUPC.AutoSize = true;
			this.rbUPC.Location = new System.Drawing.Point(454, 29);
			this.rbUPC.Name = "rbUPC";
			this.rbUPC.Size = new System.Drawing.Size(45, 18);
			this.rbUPC.TabIndex = 5;
			this.rbUPC.Text = "UPC";
			this.rbUPC.UseVisualStyleBackColor = true;
			this.rbUPC.CheckedChanged += new System.EventHandler(this.rbUPC_CheckedChanged);
			// 
			// rbStockItem
			// 
			this.rbStockItem.AutoSize = true;
			this.rbStockItem.Checked = true;
			this.rbStockItem.Location = new System.Drawing.Point(253, 29);
			this.rbStockItem.Name = "rbStockItem";
			this.rbStockItem.Size = new System.Drawing.Size(44, 18);
			this.rbStockItem.TabIndex = 3;
			this.rbStockItem.TabStop = true;
			this.rbStockItem.Text = "Item";
			this.rbStockItem.UseVisualStyleBackColor = true;
			this.rbStockItem.CheckedChanged += new System.EventHandler(this.rbStockItem_CheckedChanged);
			// 
			// txtUPC
			// 
			this.txtUPC.Enabled = false;
			this.txtUPC.Location = new System.Drawing.Point(454, 49);
			this.txtUPC.Name = "txtUPC";
			this.txtUPC.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtUPC.Properties.Mask.EditMask = "N0";
			this.txtUPC.Properties.MaxLength = 12;
			this.txtUPC.Size = new System.Drawing.Size(100, 20);
			this.txtUPC.StyleController = this.BoxStyles;
			this.txtUPC.TabIndex = 6;
			this.txtUPC.EditValueChanged += new System.EventHandler(this.txtUPC_EditValueChanged);
			// 
			// spinMinDisplayQty
			// 
			this.spinMinDisplayQty.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.spinMinDisplayQty.Location = new System.Drawing.Point(560, 49);
			this.spinMinDisplayQty.Name = "spinMinDisplayQty";
			this.spinMinDisplayQty.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.spinMinDisplayQty.Properties.IsFloatValue = false;
			this.spinMinDisplayQty.Properties.Mask.EditMask = "N00";
			this.spinMinDisplayQty.Properties.Mask.UseMaskAsDisplayFormat = true;
			this.spinMinDisplayQty.Properties.MaxLength = 2;
			this.spinMinDisplayQty.Properties.MaxValue = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.spinMinDisplayQty.Size = new System.Drawing.Size(74, 20);
			this.spinMinDisplayQty.StyleController = this.BoxStyles;
			this.spinMinDisplayQty.TabIndex = 8;
			this.spinMinDisplayQty.EditValueChanged += new System.EventHandler(this.spinMinDisplayQty_EditValueChanged);
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
			this.ctrlItem.ItemColourVisible = false;
			this.ctrlItem.ItemSize = null;
			this.ctrlItem.ItemSizeVisible = false;
			this.ctrlItem.ItemStyle = null;
			this.ctrlItem.ItemStyleVisible = true;
			this.ctrlItem.ItemVendor = null;
			this.ctrlItem.Location = new System.Drawing.Point(253, 49);
			this.ctrlItem.Margin = new System.Windows.Forms.Padding(0);
			this.ctrlItem.MaximumSize = new System.Drawing.Size(261, 20);
			this.ctrlItem.MinimumSize = new System.Drawing.Size(140, 20);
			this.ctrlItem.Name = "ctrlItem";
			this.ctrlItem.ShowZeros = false;
			this.ctrlItem.Size = new System.Drawing.Size(198, 20);
			this.ctrlItem.StyleController = this.BoxStyles;
			this.ctrlItem.TabIndex = 4;
			this.ctrlItem.EditValueChanged += new System.EventHandler(this.ctrlItem_EditValueChanged);
			// 
			// labelControl3
			// 
			this.labelControl3.Location = new System.Drawing.Point(560, 30);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(74, 13);
			this.labelControl3.TabIndex = 7;
			this.labelControl3.Text = "Min Display Qty";
			// 
			// btnSearch
			// 
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.Location = new System.Drawing.Point(777, 46);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(63, 23);
			this.btnSearch.TabIndex = 0;
			this.btnSearch.Text = "Search";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// cboDept
			// 
			this.cboDept.AllowedCharacters = "";
			this.cboDept.AutoSize = true;
			this.cboDept.EditValue = null;
			this.cboDept.Location = new System.Drawing.Point(14, 50);
			this.cboDept.LookupType = Disney.iDash.SRR.BusinessLayer.LookupSource.LookupTypes.Departments;
			this.cboDept.MaxLength = 0;
			this.cboDept.Name = "cboDept";
			this.cboDept.Size = new System.Drawing.Size(235, 20);
			this.cboDept.StyleController = this.BoxStyles;
			this.cboDept.TabIndex = 2;
			this.cboDept.WhereClause = "";
			this.cboDept.EditValueChanged += new System.EventHandler(this.cboDept_EditValueChanged);
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point(14, 31);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(57, 13);
			this.labelControl1.TabIndex = 1;
			this.labelControl1.Text = "Department";
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
			this.gridItems.Location = new System.Drawing.Point(12, 105);
			this.gridItems.MainView = this.viewItems;
			this.gridItems.Name = "gridItems";
			this.gridItems.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riMinDispQty});
			this.gridItems.Size = new System.Drawing.Size(845, 306);
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
            this.colDepartment,
            this.colClass,
            this.colVendor,
            this.colStyle,
            this.colColour,
            this.colSize,
            this.colUPC,
            this.colDescription,
            this.colMinDistLot,
            this.colMinDispQty});
			this.viewItems.GridControl = this.gridItems;
			this.viewItems.Name = "viewItems";
			this.viewItems.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.viewItems.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.viewItems.OptionsView.EnableAppearanceEvenRow = true;
			this.viewItems.OptionsView.EnableAppearanceOddRow = true;
			this.viewItems.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.viewItems_RowCellStyle);
			this.viewItems.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.viewItems_CellValueChanged);
			this.viewItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.viewItems_KeyDown);
			// 
			// colDepartment
			// 
			this.colDepartment.Caption = "Department";
			this.colDepartment.FieldName = "DEPARTMENT";
			this.colDepartment.Name = "colDepartment";
			this.colDepartment.OptionsColumn.AllowEdit = false;
			this.colDepartment.Visible = true;
			this.colDepartment.VisibleIndex = 0;
			// 
			// colClass
			// 
			this.colClass.Caption = "Class";
			this.colClass.FieldName = "CLASS";
			this.colClass.Name = "colClass";
			this.colClass.OptionsColumn.AllowEdit = false;
			this.colClass.Visible = true;
			this.colClass.VisibleIndex = 1;
			// 
			// colVendor
			// 
			this.colVendor.Caption = "Vendor";
			this.colVendor.FieldName = "VENDOR";
			this.colVendor.Name = "colVendor";
			this.colVendor.OptionsColumn.AllowEdit = false;
			this.colVendor.Visible = true;
			this.colVendor.VisibleIndex = 2;
			// 
			// colStyle
			// 
			this.colStyle.Caption = "Style";
			this.colStyle.FieldName = "STYLE";
			this.colStyle.Name = "colStyle";
			this.colStyle.OptionsColumn.AllowEdit = false;
			this.colStyle.Visible = true;
			this.colStyle.VisibleIndex = 3;
			// 
			// colColour
			// 
			this.colColour.Caption = "Colour";
			this.colColour.FieldName = "COLOUR";
			this.colColour.Name = "colColour";
			this.colColour.OptionsColumn.AllowEdit = false;
			this.colColour.Visible = true;
			this.colColour.VisibleIndex = 4;
			// 
			// colSize
			// 
			this.colSize.Caption = "Size";
			this.colSize.FieldName = "SIZE";
			this.colSize.Name = "colSize";
			this.colSize.OptionsColumn.AllowEdit = false;
			this.colSize.Visible = true;
			this.colSize.VisibleIndex = 5;
			// 
			// colUPC
			// 
			this.colUPC.Caption = "UPC";
			this.colUPC.FieldName = "UPC";
			this.colUPC.Name = "colUPC";
			this.colUPC.OptionsColumn.AllowEdit = false;
			this.colUPC.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
			this.colUPC.Visible = true;
			this.colUPC.VisibleIndex = 6;
			// 
			// colDescription
			// 
			this.colDescription.Caption = "Description";
			this.colDescription.FieldName = "DESCRIPTION";
			this.colDescription.Name = "colDescription";
			this.colDescription.OptionsColumn.AllowEdit = false;
			this.colDescription.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
			this.colDescription.Visible = true;
			this.colDescription.VisibleIndex = 7;
			// 
			// colMinDistLot
			// 
			this.colMinDistLot.AppearanceCell.Options.UseTextOptions = true;
			this.colMinDistLot.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.colMinDistLot.Caption = "Distro";
			this.colMinDistLot.DisplayFormat.FormatString = "N0";
			this.colMinDistLot.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colMinDistLot.FieldName = "MINDISTLOT";
			this.colMinDistLot.Name = "colMinDistLot";
			this.colMinDistLot.OptionsColumn.AllowEdit = false;
			this.colMinDistLot.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
			this.colMinDistLot.Visible = true;
			this.colMinDistLot.VisibleIndex = 8;
			// 
			// colMinDispQty
			// 
			this.colMinDispQty.Caption = "Min. Disp. Qty.";
			this.colMinDispQty.ColumnEdit = this.riMinDispQty;
			this.colMinDispQty.FieldName = "MINDISPQTY";
			this.colMinDispQty.Name = "colMinDispQty";
			this.colMinDispQty.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
			this.colMinDispQty.Visible = true;
			this.colMinDispQty.VisibleIndex = 9;
			// 
			// riMinDispQty
			// 
			this.riMinDispQty.AutoHeight = false;
			this.riMinDispQty.IsFloatValue = false;
			this.riMinDispQty.Mask.EditMask = "N0";
			this.riMinDispQty.Mask.UseMaskAsDisplayFormat = true;
			this.riMinDispQty.MaxValue = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.riMinDispQty.Name = "riMinDispQty";
			this.riMinDispQty.Spin += new DevExpress.XtraEditors.Controls.SpinEventHandler(this.riMinDispQty_Spin);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(793, 417);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(63, 23);
			this.btnClose.TabIndex = 3;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Enabled = false;
			this.btnSave.Location = new System.Drawing.Point(655, 417);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(63, 23);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Enabled = false;
			this.btnCancel.Location = new System.Drawing.Point(724, 417);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(63, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "C&ancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// StockItemMinDisplayQtyForm
			// 
			this.AcceptButton = this.btnSearch;
			this.Appearance.Font = new System.Drawing.Font("Arial", 8.25F);
			this.Appearance.Options.UseFont = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(869, 499);
			this.Name = "StockItemMinDisplayQtyForm";
			this.Text = "Minimum Display Quantity";
			((System.ComponentModel.ISupportInitialize)(this.ClientPanel)).EndInit();
			this.ClientPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.BoxStyles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.grpSelection)).EndInit();
			this.grpSelection.ResumeLayout(false);
			this.grpSelection.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtUPC.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spinMinDisplayQty.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.viewItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.riMinDispQty)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpSelection;
        private Controls.LookupComboControl cboDept;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraGrid.GridControl gridItems;
        private DevExpress.XtraGrid.Views.Grid.GridView viewItems;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private Controls.StockItemControl ctrlItem;
        private DevExpress.XtraEditors.SpinEdit spinMinDisplayQty;
        private DevExpress.XtraEditors.TextEdit txtUPC;
        private System.Windows.Forms.RadioButton rbUPC;
        private System.Windows.Forms.RadioButton rbStockItem;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartment;
        private DevExpress.XtraGrid.Columns.GridColumn colClass;
        private DevExpress.XtraGrid.Columns.GridColumn colVendor;
        private DevExpress.XtraGrid.Columns.GridColumn colStyle;
        private DevExpress.XtraGrid.Columns.GridColumn colColour;
        private DevExpress.XtraGrid.Columns.GridColumn colSize;
        private DevExpress.XtraGrid.Columns.GridColumn colUPC;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colMinDistLot;
        private DevExpress.XtraGrid.Columns.GridColumn colMinDispQty;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit riMinDispQty;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}

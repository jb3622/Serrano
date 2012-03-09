namespace Disney.iDash.SRR.UI.Forms.Maintenance
{
    partial class StoreGroupsForm
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
			this.colChanged = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridItems = new DevExpress.XtraGrid.GridControl();
			this.viewItems = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colSelected = new DevExpress.XtraGrid.Columns.GridColumn();
			this.riSelected = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colChangedBy = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colChangedDate = new DevExpress.XtraGrid.Columns.GridColumn();
			this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
			this.btnSave = new DevExpress.XtraEditors.SimpleButton();
			this.btnTick = new DevExpress.XtraEditors.SimpleButton();
			this.btnUntick = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.ClientPanel)).BeginInit();
			this.ClientPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.BoxStyles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.viewItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.riSelected)).BeginInit();
			this.SuspendLayout();
			// 
			// ClientPanel
			// 
			this.ClientPanel.Controls.Add(this.btnUntick);
			this.ClientPanel.Controls.Add(this.btnTick);
			this.ClientPanel.Controls.Add(this.btnSave);
			this.ClientPanel.Controls.Add(this.btnCancel);
			this.ClientPanel.Controls.Add(this.gridItems);
			this.ClientPanel.Size = new System.Drawing.Size(596, 404);
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
			// colChanged
			// 
			this.colChanged.Caption = "Changed";
			this.colChanged.FieldName = "Changed";
			this.colChanged.Name = "colChanged";
			this.colChanged.OptionsColumn.ShowInCustomizationForm = false;
			this.colChanged.OptionsColumn.ShowInExpressionEditor = false;
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
			this.gridItems.Location = new System.Drawing.Point(12, 17);
			this.gridItems.MainView = this.viewItems;
			this.gridItems.Name = "gridItems";
			this.gridItems.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riSelected});
			this.gridItems.Size = new System.Drawing.Size(572, 347);
			this.gridItems.TabIndex = 0;
			this.gridItems.UseEmbeddedNavigator = true;
			this.gridItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewItems});
			this.gridItems.Visible = false;
			// 
			// viewItems
			// 
			this.viewItems.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.viewItems.Appearance.HeaderPanel.Options.UseFont = true;
			this.viewItems.Appearance.SelectedRow.BackColor = System.Drawing.Color.Red;
			this.viewItems.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
			this.viewItems.Appearance.SelectedRow.Options.UseBackColor = true;
			this.viewItems.Appearance.SelectedRow.Options.UseForeColor = true;
			this.viewItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSelected,
            this.colId,
            this.colDescription,
            this.colChangedBy,
            this.colChangedDate,
            this.colChanged});
			styleFormatCondition1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
			styleFormatCondition1.Appearance.Options.UseBackColor = true;
			styleFormatCondition1.ApplyToRow = true;
			styleFormatCondition1.Column = this.colChanged;
			styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
			styleFormatCondition1.Value1 = true;
			this.viewItems.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
			this.viewItems.GridControl = this.gridItems;
			this.viewItems.Name = "viewItems";
			this.viewItems.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.viewItems.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.viewItems.OptionsCustomization.AllowColumnMoving = false;
			this.viewItems.OptionsCustomization.AllowGroup = false;
			this.viewItems.OptionsSelection.MultiSelect = true;
			this.viewItems.OptionsView.EnableAppearanceEvenRow = true;
			this.viewItems.OptionsView.EnableAppearanceOddRow = true;
			this.viewItems.OptionsView.ShowGroupPanel = false;
			this.viewItems.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.viewItems_SelectionChanged);
			this.viewItems.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.viewItems_CellValueChanged);
			this.viewItems.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.viewItems_RowUpdated);
			// 
			// colSelected
			// 
			this.colSelected.AppearanceHeader.Options.UseTextOptions = true;
			this.colSelected.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colSelected.Caption = "Selected";
			this.colSelected.ColumnEdit = this.riSelected;
			this.colSelected.FieldName = "Selected";
			this.colSelected.Name = "colSelected";
			this.colSelected.Visible = true;
			this.colSelected.VisibleIndex = 0;
			// 
			// riSelected
			// 
			this.riSelected.AutoHeight = false;
			this.riSelected.Name = "riSelected";
			// 
			// colId
			// 
			this.colId.AppearanceHeader.Options.UseTextOptions = true;
			this.colId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colId.Caption = "Id";
			this.colId.FieldName = "Id";
			this.colId.Name = "colId";
			this.colId.OptionsColumn.AllowEdit = false;
			this.colId.Visible = true;
			this.colId.VisibleIndex = 1;
			// 
			// colDescription
			// 
			this.colDescription.AppearanceHeader.Options.UseTextOptions = true;
			this.colDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colDescription.Caption = "Description";
			this.colDescription.FieldName = "Description";
			this.colDescription.Name = "colDescription";
			this.colDescription.OptionsColumn.AllowEdit = false;
			this.colDescription.Visible = true;
			this.colDescription.VisibleIndex = 2;
			// 
			// colChangedBy
			// 
			this.colChangedBy.AppearanceHeader.Options.UseTextOptions = true;
			this.colChangedBy.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colChangedBy.Caption = "Changed By";
			this.colChangedBy.FieldName = "ChangedBy";
			this.colChangedBy.Name = "colChangedBy";
			this.colChangedBy.OptionsColumn.AllowEdit = false;
			this.colChangedBy.Visible = true;
			this.colChangedBy.VisibleIndex = 3;
			// 
			// colChangedDate
			// 
			this.colChangedDate.AppearanceHeader.Options.UseTextOptions = true;
			this.colChangedDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colChangedDate.Caption = "Changed Date";
			this.colChangedDate.DisplayFormat.FormatString = "dd-MMM-yyyy HH:mm";
			this.colChangedDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.colChangedDate.FieldName = "ChangedDate";
			this.colChangedDate.Name = "colChangedDate";
			this.colChangedDate.OptionsColumn.AllowEdit = false;
			this.colChangedDate.Visible = true;
			this.colChangedDate.VisibleIndex = 4;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(509, 370);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Enabled = false;
			this.btnSave.Location = new System.Drawing.Point(428, 370);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnTick
			// 
			this.btnTick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnTick.Enabled = false;
			this.btnTick.Location = new System.Drawing.Point(12, 370);
			this.btnTick.Name = "btnTick";
			this.btnTick.Size = new System.Drawing.Size(75, 23);
			this.btnTick.TabIndex = 3;
			this.btnTick.Text = "&Tick";
			this.btnTick.Click += new System.EventHandler(this.btnTick_Click);
			// 
			// btnUntick
			// 
			this.btnUntick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnUntick.Enabled = false;
			this.btnUntick.Location = new System.Drawing.Point(93, 370);
			this.btnUntick.Name = "btnUntick";
			this.btnUntick.Size = new System.Drawing.Size(75, 23);
			this.btnUntick.TabIndex = 4;
			this.btnUntick.Text = "&Untick";
			this.btnUntick.Click += new System.EventHandler(this.btnUntick_Click);
			// 
			// StoreGroupsForm
			// 
			this.Appearance.Font = new System.Drawing.Font("Arial", 8.25F);
			this.Appearance.Options.UseFont = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(596, 458);
			this.MaximizeBox = false;
			this.Name = "StoreGroupsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Store Group Maintenance";
			this.Shown += new System.EventHandler(this.StoreGroupsForm_Shown);
			((System.ComponentModel.ISupportInitialize)(this.ClientPanel)).EndInit();
			this.ClientPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.BoxStyles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.viewItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.riSelected)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridItems;
        private DevExpress.XtraGrid.Views.Grid.GridView viewItems;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraGrid.Columns.GridColumn colSelected;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit riSelected;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colChangedBy;
        private DevExpress.XtraGrid.Columns.GridColumn colChangedDate;
        private DevExpress.XtraGrid.Columns.GridColumn colChanged;
        private DevExpress.XtraEditors.SimpleButton btnTick;
        private DevExpress.XtraEditors.SimpleButton btnUntick;
    }
}

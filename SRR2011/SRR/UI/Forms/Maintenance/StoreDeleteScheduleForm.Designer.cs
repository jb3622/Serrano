namespace Disney.iDash.SRR.UI.Forms.Maintenance
{
    partial class StoreDeleteScheduleForm
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
			this.colDeleted = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridItems = new DevExpress.XtraGrid.GridControl();
			this.viewItems = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colStore = new DevExpress.XtraGrid.Columns.GridColumn();
			this.riStoreLookUp = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
			this.colScheduleDate = new DevExpress.XtraGrid.Columns.GridColumn();
			this.riScheduleDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
			this.colNetworkId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colLastUpdated = new DevExpress.XtraGrid.Columns.GridColumn();
			this.riLastUpdated = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
			this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
			this.riMinDispQty = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
			this.chkShowProcessedItems = new DevExpress.XtraEditors.CheckEdit();
			this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
			this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
			this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
			this.btnSave = new DevExpress.XtraEditors.SimpleButton();
			this.btnClose = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.ClientPanel)).BeginInit();
			this.ClientPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.BoxStyles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.viewItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.riStoreLookUp)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.riScheduleDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.riScheduleDate.VistaTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.riLastUpdated)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.riLastUpdated.VistaTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.riMinDispQty)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chkShowProcessedItems.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// ClientPanel
			// 
			this.ClientPanel.Controls.Add(this.btnDelete);
			this.ClientPanel.Controls.Add(this.btnAdd);
			this.ClientPanel.Controls.Add(this.btnCancel);
			this.ClientPanel.Controls.Add(this.btnSave);
			this.ClientPanel.Controls.Add(this.btnClose);
			this.ClientPanel.Controls.Add(this.chkShowProcessedItems);
			this.ClientPanel.Controls.Add(this.gridItems);
			this.ClientPanel.Size = new System.Drawing.Size(808, 416);
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
			// colDeleted
			// 
			this.colDeleted.Caption = "Deleted";
			this.colDeleted.FieldName = "DELETEFLAG";
			this.colDeleted.Name = "colDeleted";
			this.colDeleted.OptionsColumn.AllowEdit = false;
			this.colDeleted.OptionsColumn.ShowInCustomizationForm = false;
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
			this.gridItems.Location = new System.Drawing.Point(11, 12);
			this.gridItems.MainView = this.viewItems;
			this.gridItems.Name = "gridItems";
			this.gridItems.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.riMinDispQty,
            this.riStoreLookUp,
            this.riScheduleDate,
            this.riLastUpdated});
			this.gridItems.Size = new System.Drawing.Size(784, 367);
			this.gridItems.TabIndex = 2;
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
            this.colStore,
            this.colScheduleDate,
            this.colNetworkId,
            this.colLastUpdated,
            this.colStatus,
            this.colDeleted});
			styleFormatCondition1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Strikeout);
			styleFormatCondition1.Appearance.Options.UseFont = true;
			styleFormatCondition1.ApplyToRow = true;
			styleFormatCondition1.Column = this.colDeleted;
			styleFormatCondition1.Condition = DevExpress.XtraGrid.FormatConditionEnum.Equal;
			styleFormatCondition1.Value1 = 1;
			this.viewItems.FormatConditions.AddRange(new DevExpress.XtraGrid.StyleFormatCondition[] {
            styleFormatCondition1});
			this.viewItems.GridControl = this.gridItems;
			this.viewItems.Name = "viewItems";
			this.viewItems.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.viewItems.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.viewItems.OptionsSelection.MultiSelect = true;
			this.viewItems.OptionsView.EnableAppearanceEvenRow = true;
			this.viewItems.OptionsView.EnableAppearanceOddRow = true;
			this.viewItems.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.viewItems_RowCellStyle);
			this.viewItems.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.viewItems_FocusedRowChanged);
			this.viewItems.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.viewItems_CellValueChanged);
			this.viewItems.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.viewItems_InvalidRowException);
			this.viewItems.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.viewItems_ValidateRow);
			// 
			// colStore
			// 
			this.colStore.Caption = "Store";
			this.colStore.ColumnEdit = this.riStoreLookUp;
			this.colStore.FieldName = "STOREID";
			this.colStore.Name = "colStore";
			this.colStore.Visible = true;
			this.colStore.VisibleIndex = 0;
			// 
			// riStoreLookUp
			// 
			this.riStoreLookUp.AutoHeight = false;
			this.riStoreLookUp.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.riStoreLookUp.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id", "Id", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description", "Description")});
			this.riStoreLookUp.DisplayMember = "Description";
			this.riStoreLookUp.Name = "riStoreLookUp";
			this.riStoreLookUp.NullText = "";
			this.riStoreLookUp.ShowFooter = false;
			this.riStoreLookUp.ShowHeader = false;
			this.riStoreLookUp.ValueMember = "Id";
			// 
			// colScheduleDate
			// 
			this.colScheduleDate.Caption = "Schedule Date";
			this.colScheduleDate.ColumnEdit = this.riScheduleDate;
			this.colScheduleDate.FieldName = "SCHEDULEDATE";
			this.colScheduleDate.Name = "colScheduleDate";
			this.colScheduleDate.Visible = true;
			this.colScheduleDate.VisibleIndex = 1;
			// 
			// riScheduleDate
			// 
			this.riScheduleDate.AutoHeight = false;
			this.riScheduleDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.riScheduleDate.Mask.EditMask = "dd/MM/yyyy";
			this.riScheduleDate.Mask.UseMaskAsDisplayFormat = true;
			this.riScheduleDate.Name = "riScheduleDate";
			this.riScheduleDate.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
			this.riScheduleDate.VistaEditTime = DevExpress.Utils.DefaultBoolean.False;
			this.riScheduleDate.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			// 
			// colNetworkId
			// 
			this.colNetworkId.Caption = "User";
			this.colNetworkId.FieldName = "NETWORKID";
			this.colNetworkId.Name = "colNetworkId";
			this.colNetworkId.OptionsColumn.AllowEdit = false;
			this.colNetworkId.Visible = true;
			this.colNetworkId.VisibleIndex = 2;
			// 
			// colLastUpdated
			// 
			this.colLastUpdated.Caption = "Last Updated";
			this.colLastUpdated.ColumnEdit = this.riLastUpdated;
			this.colLastUpdated.FieldName = "LASTUPDATED";
			this.colLastUpdated.Name = "colLastUpdated";
			this.colLastUpdated.OptionsColumn.AllowEdit = false;
			this.colLastUpdated.Visible = true;
			this.colLastUpdated.VisibleIndex = 3;
			// 
			// riLastUpdated
			// 
			this.riLastUpdated.AutoHeight = false;
			this.riLastUpdated.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.riLastUpdated.Mask.EditMask = "dd/MM/yyyy hh:mm";
			this.riLastUpdated.Name = "riLastUpdated";
			this.riLastUpdated.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
			this.riLastUpdated.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
			this.riLastUpdated.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			// 
			// colStatus
			// 
			this.colStatus.Caption = "Status";
			this.colStatus.FieldName = "STATUS";
			this.colStatus.Name = "colStatus";
			this.colStatus.OptionsColumn.AllowEdit = false;
			this.colStatus.Visible = true;
			this.colStatus.VisibleIndex = 4;
			// 
			// riMinDispQty
			// 
			this.riMinDispQty.AutoHeight = false;
			this.riMinDispQty.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.riMinDispQty.IsFloatValue = false;
			this.riMinDispQty.Mask.EditMask = "N0";
			this.riMinDispQty.Mask.UseMaskAsDisplayFormat = true;
			this.riMinDispQty.MaxValue = new decimal(new int[] {
            99,
            0,
            0,
            0});
			this.riMinDispQty.Name = "riMinDispQty";
			// 
			// chkShowProcessedItems
			// 
			this.chkShowProcessedItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkShowProcessedItems.EditValue = true;
			this.chkShowProcessedItems.Location = new System.Drawing.Point(226, 389);
			this.chkShowProcessedItems.Name = "chkShowProcessedItems";
			this.chkShowProcessedItems.Properties.Caption = "Show processed items";
			this.chkShowProcessedItems.Size = new System.Drawing.Size(132, 19);
			this.chkShowProcessedItems.TabIndex = 3;
			this.chkShowProcessedItems.CheckStateChanged += new System.EventHandler(this.chkShowProcessedItems_CheckStateChanged);
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.Enabled = false;
			this.btnDelete.Location = new System.Drawing.Point(119, 387);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(101, 23);
			this.btnDelete.TabIndex = 11;
			this.btnDelete.Text = "&Delete/Undelete";
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAdd.Enabled = false;
			this.btnAdd.Location = new System.Drawing.Point(12, 387);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(101, 23);
			this.btnAdd.TabIndex = 10;
			this.btnAdd.Text = "&Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Enabled = false;
			this.btnCancel.Location = new System.Drawing.Point(663, 385);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(63, 23);
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "C&ancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Enabled = false;
			this.btnSave.Location = new System.Drawing.Point(594, 385);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(63, 23);
			this.btnSave.TabIndex = 7;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(732, 385);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(63, 23);
			this.btnClose.TabIndex = 9;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// StoreDeleteScheduleForm
			// 
			this.Appearance.Font = new System.Drawing.Font("Arial", 8.25F);
			this.Appearance.Options.UseFont = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
			this.ClientSize = new System.Drawing.Size(808, 470);
			this.Name = "StoreDeleteScheduleForm";
			this.Text = "Store Deletion Schedule";
			((System.ComponentModel.ISupportInitialize)(this.ClientPanel)).EndInit();
			this.ClientPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.BoxStyles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.viewItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.riStoreLookUp)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.riScheduleDate.VistaTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.riScheduleDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.riLastUpdated.VistaTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.riLastUpdated)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.riMinDispQty)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chkShowProcessedItems.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridItems;
        private DevExpress.XtraGrid.Views.Grid.GridView viewItems;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraGrid.Columns.GridColumn colStore;
        private DevExpress.XtraGrid.Columns.GridColumn colScheduleDate;
        private DevExpress.XtraGrid.Columns.GridColumn colNetworkId;
        private DevExpress.XtraGrid.Columns.GridColumn colLastUpdated;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit riMinDispQty;
        private DevExpress.XtraGrid.Columns.GridColumn colDeleted;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit riStoreLookUp;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit riScheduleDate;
        private DevExpress.XtraEditors.CheckEdit chkShowProcessedItems;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit riLastUpdated;
    }
}

namespace Disney.iDash.SRR.Controls
{
    partial class LookupComboControl
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
			this.cboLookup = new DevExpress.XtraEditors.GridLookUpEdit();
			this.cboLookupView = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
			((System.ComponentModel.ISupportInitialize)(this.cboLookup.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.cboLookupView)).BeginInit();
			this.SuspendLayout();
			// 
			// cboLookup
			// 
			this.cboLookup.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cboLookup.Location = new System.Drawing.Point(0, 0);
			this.cboLookup.Name = "cboLookup";
			this.cboLookup.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
			this.cboLookup.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
			this.cboLookup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
			this.cboLookup.Properties.DisplayMember = "Description";
			this.cboLookup.Properties.ImmediatePopup = true;
			this.cboLookup.Properties.NullText = "";
			this.cboLookup.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
			this.cboLookup.Properties.PopupResizeMode = DevExpress.XtraEditors.Controls.ResizeMode.FrameResize;
			this.cboLookup.Properties.ShowFooter = false;
			this.cboLookup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
			this.cboLookup.Properties.ValueMember = "Id";
			this.cboLookup.Properties.View = this.cboLookupView;
			this.cboLookup.Properties.ViewType = DevExpress.XtraEditors.Repository.GridLookUpViewType.GridView;
			this.cboLookup.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.cboLookup_Properties_ButtonClick);
			this.cboLookup.Size = new System.Drawing.Size(228, 20);
			this.cboLookup.TabIndex = 0;
			this.cboLookup.ProcessNewValue += new DevExpress.XtraEditors.Controls.ProcessNewValueEventHandler(this.cboLookup_ProcessNewValue);
			this.cboLookup.Popup += new System.EventHandler(this.cboLookup_Popup);
			this.cboLookup.EditValueChanged += new System.EventHandler(this.cboLookup_EditValueChanged);
			this.cboLookup.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboLookup_KeyPress);
			this.cboLookup.Validating += new System.ComponentModel.CancelEventHandler(this.cboLookup_Validating);
			// 
			// cboLookupView
			// 
			this.cboLookupView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.cboLookupView.Appearance.HeaderPanel.Options.UseFont = true;
			this.cboLookupView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colDescription});
			this.cboLookupView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.cboLookupView.Name = "cboLookupView";
			this.cboLookupView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.cboLookupView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.cboLookupView.OptionsBehavior.AllowFixedGroups = DevExpress.Utils.DefaultBoolean.False;
			this.cboLookupView.OptionsBehavior.AllowIncrementalSearch = true;
			this.cboLookupView.OptionsBehavior.AutoPopulateColumns = false;
			this.cboLookupView.OptionsBehavior.Editable = false;
			this.cboLookupView.OptionsCustomization.AllowColumnMoving = false;
			this.cboLookupView.OptionsCustomization.AllowGroup = false;
			this.cboLookupView.OptionsCustomization.AllowQuickHideColumns = false;
			this.cboLookupView.OptionsMenu.EnableColumnMenu = false;
			this.cboLookupView.OptionsMenu.EnableFooterMenu = false;
			this.cboLookupView.OptionsMenu.EnableGroupPanelMenu = false;
			this.cboLookupView.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.cboLookupView.OptionsView.ColumnAutoWidth = false;
			this.cboLookupView.OptionsView.ShowDetailButtons = false;
			this.cboLookupView.OptionsView.ShowGroupPanel = false;
			// 
			// colId
			// 
			this.colId.Caption = "Id";
			this.colId.FieldName = "Id";
			this.colId.Name = "colId";
			this.colId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
			this.colId.Visible = true;
			this.colId.VisibleIndex = 0;
			// 
			// colDescription
			// 
			this.colDescription.Caption = "Description";
			this.colDescription.FieldName = "Description";
			this.colDescription.Name = "colDescription";
			this.colDescription.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
			this.colDescription.Visible = true;
			this.colDescription.VisibleIndex = 1;
			// 
			// LookupComboControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.cboLookup);
			this.Name = "LookupComboControl";
			this.Size = new System.Drawing.Size(228, 21);
			((System.ComponentModel.ISupportInitialize)(this.cboLookup.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.cboLookupView)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraEditors.GridLookUpEdit cboLookup;
		private DevExpress.XtraGrid.Views.Grid.GridView cboLookupView;
		private DevExpress.XtraGrid.Columns.GridColumn colId;
		private DevExpress.XtraGrid.Columns.GridColumn colDescription;


	}
}

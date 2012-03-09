namespace Disney.iDash.SRR.UI.Forms.Workbench
{
    partial class StoreClassSelectorDialog
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
            this.gridStore = new DevExpress.XtraGrid.GridControl();
            this.viewStore = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridClass = new DevExpress.XtraGrid.GridControl();
            this.viewClass = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnBack = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.ClientPanel)).BeginInit();
            this.ClientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BoxStyles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridStore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewStore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ClientPanel
            // 
            this.ClientPanel.Controls.Add(this.labelControl1);
            this.ClientPanel.Controls.Add(this.splitContainer1);
            this.ClientPanel.Controls.Add(this.btnExit);
            this.ClientPanel.Controls.Add(this.btnNext);
            this.ClientPanel.Controls.Add(this.btnBack);
            this.ClientPanel.Location = new System.Drawing.Point(0, 27);
            this.ClientPanel.Size = new System.Drawing.Size(918, 510);
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
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 537);
            this.ribbonStatusBar.Size = new System.Drawing.Size(918, 31);
            // 
            // gridStore
            // 
            this.gridStore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridStore.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridStore.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridStore.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridStore.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridStore.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridStore.Location = new System.Drawing.Point(0, 0);
            this.gridStore.MainView = this.viewStore;
            this.gridStore.Name = "gridStore";
            this.gridStore.Size = new System.Drawing.Size(892, 225);
            this.gridStore.TabIndex = 0;
            this.gridStore.UseEmbeddedNavigator = true;
            this.gridStore.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewStore});
            // 
            // viewStore
            // 
            this.viewStore.Appearance.EvenRow.BackColor = System.Drawing.Color.LightCyan;
            this.viewStore.Appearance.EvenRow.Options.UseBackColor = true;
            this.viewStore.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.viewStore.Appearance.HeaderPanel.Options.UseFont = true;
            this.viewStore.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.viewStore.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.viewStore.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.viewStore.Appearance.OddRow.Options.UseBackColor = true;
            this.viewStore.Appearance.ViewCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.viewStore.Appearance.ViewCaption.Options.UseFont = true;
            this.viewStore.ColumnPanelRowHeight = 50;
            this.viewStore.GridControl = this.gridStore;
            this.viewStore.Name = "viewStore";
            this.viewStore.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.viewStore.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.viewStore.OptionsBehavior.Editable = false;
            this.viewStore.OptionsCustomization.AllowColumnMoving = false;
            this.viewStore.OptionsCustomization.AllowGroup = false;
            this.viewStore.OptionsCustomization.AllowQuickHideColumns = false;
            this.viewStore.OptionsView.ColumnAutoWidth = false;
            this.viewStore.OptionsView.EnableAppearanceEvenRow = true;
            this.viewStore.OptionsView.EnableAppearanceOddRow = true;
            this.viewStore.OptionsView.ShowGroupPanel = false;
            this.viewStore.OptionsView.ShowViewCaption = true;
            this.viewStore.ViewCaption = "Select Store";
            this.viewStore.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.viewStore_FocusedRowChanged);
            this.viewStore.DoubleClick += new System.EventHandler(this.viewStore_DoubleClick);
            // 
            // gridClass
            // 
            this.gridClass.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridClass.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridClass.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridClass.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridClass.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridClass.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridClass.Location = new System.Drawing.Point(0, 0);
            this.gridClass.MainView = this.viewClass;
            this.gridClass.Name = "gridClass";
            this.gridClass.Size = new System.Drawing.Size(892, 218);
            this.gridClass.TabIndex = 1;
            this.gridClass.UseEmbeddedNavigator = true;
            this.gridClass.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewClass});
            // 
            // viewClass
            // 
            this.viewClass.Appearance.EvenRow.BackColor = System.Drawing.Color.LightCyan;
            this.viewClass.Appearance.EvenRow.Options.UseBackColor = true;
            this.viewClass.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.viewClass.Appearance.HeaderPanel.Options.UseFont = true;
            this.viewClass.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.viewClass.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.viewClass.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.viewClass.Appearance.OddRow.Options.UseBackColor = true;
            this.viewClass.Appearance.ViewCaption.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.viewClass.Appearance.ViewCaption.Options.UseFont = true;
            this.viewClass.ColumnPanelRowHeight = 50;
            this.viewClass.GridControl = this.gridClass;
            this.viewClass.Name = "viewClass";
            this.viewClass.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.viewClass.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.viewClass.OptionsBehavior.Editable = false;
            this.viewClass.OptionsCustomization.AllowColumnMoving = false;
            this.viewClass.OptionsCustomization.AllowGroup = false;
            this.viewClass.OptionsCustomization.AllowQuickHideColumns = false;
            this.viewClass.OptionsView.ColumnAutoWidth = false;
            this.viewClass.OptionsView.EnableAppearanceEvenRow = true;
            this.viewClass.OptionsView.EnableAppearanceOddRow = true;
            this.viewClass.OptionsView.ShowGroupPanel = false;
            this.viewClass.OptionsView.ShowViewCaption = true;
            this.viewClass.ViewCaption = "Select Class";
            this.viewClass.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.viewClass_FocusedRowChanged);
            this.viewClass.DoubleClick += new System.EventHandler(this.viewClass_DoubleClick);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.Location = new System.Drawing.Point(668, 479);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "< &Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Enabled = false;
            this.btnNext.Location = new System.Drawing.Point(749, 479);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "&Next >";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(830, 479);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "&Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(13, 18);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gridStore);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gridClass);
            this.splitContainer1.Size = new System.Drawing.Size(892, 455);
            this.splitContainer1.SplitterDistance = 225;
            this.splitContainer1.SplitterWidth = 12;
            this.splitContainer1.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl1.Location = new System.Drawing.Point(13, 484);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(584, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Double-click the store to select at store level, or double-click a class or click" +
    " Next to select at class level";
            // 
            // StoreClassSelectorDialog
            // 
            this.Appearance.Font = new System.Drawing.Font("Arial", 8.25F);
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.ClientSize = new System.Drawing.Size(918, 568);
            this.Name = "StoreClassSelectorDialog";
            this.Tag = "SRR";
            this.Text = "Select Store & Class";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StoreClassSelector_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.ClientPanel)).EndInit();
            this.ClientPanel.ResumeLayout(false);
            this.ClientPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BoxStyles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridStore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewStore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewClass)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl gridStore;
        private DevExpress.XtraGrid.Views.Grid.GridView viewStore;
        private DevExpress.XtraGrid.GridControl gridClass;
        private DevExpress.XtraGrid.Views.Grid.GridView viewClass;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.SimpleButton btnBack;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}

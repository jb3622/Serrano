namespace Disney.iDash.Shared.Controls
{
    partial class ListMover
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
            this.lstAvailable = new DevExpress.XtraEditors.ListBoxControl();
            this.styleController1 = new DevExpress.XtraEditors.StyleController();
            this.lstSelected = new DevExpress.XtraEditors.ListBoxControl();
            this.btnMoveRightAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnMoveRight = new DevExpress.XtraEditors.SimpleButton();
            this.btnMoveLeft = new DevExpress.XtraEditors.SimpleButton();
            this.btnMoveLeftAll = new DevExpress.XtraEditors.SimpleButton();
            this.lblAvailable = new DevExpress.XtraEditors.LabelControl();
            this.lblSelected = new DevExpress.XtraEditors.LabelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)(this.lstAvailable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstAvailable
            // 
            this.lstAvailable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAvailable.Appearance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.lstAvailable.Appearance.Options.UseFont = true;
            this.lstAvailable.Enabled = false;
            this.lstAvailable.Location = new System.Drawing.Point(3, 22);
            this.lstAvailable.Name = "lstAvailable";
            this.lstAvailable.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstAvailable.Size = new System.Drawing.Size(146, 204);
            this.lstAvailable.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.lstAvailable.StyleController = this.styleController1;
            this.lstAvailable.TabIndex = 0;
            this.lstAvailable.Click += new System.EventHandler(this.lstAvailable_Click);
            this.lstAvailable.DoubleClick += new System.EventHandler(this.lstAvailable_DoubleClick);
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.styleController1.Appearance.Options.UseFont = true;
            this.styleController1.AppearanceFocused.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.styleController1.AppearanceFocused.Options.UseFont = true;
            // 
            // lstSelected
            // 
            this.lstSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSelected.Appearance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.lstSelected.Appearance.Options.UseFont = true;
            this.lstSelected.Enabled = false;
            this.lstSelected.Location = new System.Drawing.Point(2, 22);
            this.lstSelected.Name = "lstSelected";
            this.lstSelected.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstSelected.Size = new System.Drawing.Size(141, 203);
            this.lstSelected.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.lstSelected.StyleController = this.styleController1;
            this.lstSelected.TabIndex = 1;
            this.lstSelected.Click += new System.EventHandler(this.lstSelected_Click);
            this.lstSelected.DoubleClick += new System.EventHandler(this.lstSelected_DoubleClick);
            // 
            // btnMoveRightAll
            // 
            this.btnMoveRightAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMoveRightAll.Appearance.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveRightAll.Appearance.Options.UseFont = true;
            this.btnMoveRightAll.Enabled = false;
            this.btnMoveRightAll.Location = new System.Drawing.Point(40, 230);
            this.btnMoveRightAll.Name = "btnMoveRightAll";
            this.btnMoveRightAll.Size = new System.Drawing.Size(30, 25);
            this.btnMoveRightAll.StyleController = this.styleController1;
            this.btnMoveRightAll.TabIndex = 2;
            this.btnMoveRightAll.Text = ">>";
            this.btnMoveRightAll.ToolTip = "Move all";
            this.btnMoveRightAll.Click += new System.EventHandler(this.btnMoveRightAll_Click);
            // 
            // btnMoveRight
            // 
            this.btnMoveRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMoveRight.Appearance.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveRight.Appearance.Options.UseFont = true;
            this.btnMoveRight.Enabled = false;
            this.btnMoveRight.Location = new System.Drawing.Point(3, 230);
            this.btnMoveRight.Name = "btnMoveRight";
            this.btnMoveRight.Size = new System.Drawing.Size(30, 25);
            this.btnMoveRight.StyleController = this.styleController1;
            this.btnMoveRight.TabIndex = 3;
            this.btnMoveRight.Text = ">";
            this.btnMoveRight.ToolTip = "Move selected";
            this.btnMoveRight.Click += new System.EventHandler(this.btnMoveRight_Click);
            // 
            // btnMoveLeft
            // 
            this.btnMoveLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveLeft.Appearance.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveLeft.Appearance.Options.UseFont = true;
            this.btnMoveLeft.Enabled = false;
            this.btnMoveLeft.Location = new System.Drawing.Point(112, 230);
            this.btnMoveLeft.Name = "btnMoveLeft";
            this.btnMoveLeft.Size = new System.Drawing.Size(30, 25);
            this.btnMoveLeft.StyleController = this.styleController1;
            this.btnMoveLeft.TabIndex = 4;
            this.btnMoveLeft.Text = "<";
            this.btnMoveLeft.ToolTip = "Move selected";
            this.btnMoveLeft.Click += new System.EventHandler(this.btnMoveLeft_Click);
            // 
            // btnMoveLeftAll
            // 
            this.btnMoveLeftAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveLeftAll.Appearance.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoveLeftAll.Appearance.Options.UseFont = true;
            this.btnMoveLeftAll.Enabled = false;
            this.btnMoveLeftAll.Location = new System.Drawing.Point(78, 230);
            this.btnMoveLeftAll.Name = "btnMoveLeftAll";
            this.btnMoveLeftAll.Size = new System.Drawing.Size(30, 25);
            this.btnMoveLeftAll.StyleController = this.styleController1;
            this.btnMoveLeftAll.TabIndex = 5;
            this.btnMoveLeftAll.Text = "<<";
            this.btnMoveLeftAll.ToolTip = "Move all";
            this.btnMoveLeftAll.Click += new System.EventHandler(this.btnMoveLeftAll_Click);
            // 
            // lblAvailable
            // 
            this.lblAvailable.Appearance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblAvailable.Location = new System.Drawing.Point(3, 6);
            this.lblAvailable.Name = "lblAvailable";
            this.lblAvailable.Size = new System.Drawing.Size(58, 14);
            this.lblAvailable.StyleController = this.styleController1;
            this.lblAvailable.TabIndex = 6;
            this.lblAvailable.Text = "0 Available";
            // 
            // lblSelected
            // 
            this.lblSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelected.Appearance.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSelected.Location = new System.Drawing.Point(85, 7);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(57, 14);
            this.lblSelected.StyleController = this.styleController1;
            this.lblSelected.TabIndex = 7;
            this.lblSelected.Text = "0 Selected";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.lstAvailable);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnMoveRight);
            this.splitContainerControl1.Panel1.Controls.Add(this.lblAvailable);
            this.splitContainerControl1.Panel1.Controls.Add(this.btnMoveRightAll);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.lstSelected);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnMoveLeftAll);
            this.splitContainerControl1.Panel2.Controls.Add(this.lblSelected);
            this.splitContainerControl1.Panel2.Controls.Add(this.btnMoveLeft);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(303, 259);
            this.splitContainerControl1.SplitterPosition = 151;
            this.splitContainerControl1.TabIndex = 8;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // ListMover
            // 
            this.Appearance.Font = new System.Drawing.Font("Arial", 8.25F);
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ListMover";
            this.Size = new System.Drawing.Size(303, 259);
            this.Resize += new System.EventHandler(this.ListMover_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.lstAvailable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl lstAvailable;
        private DevExpress.XtraEditors.ListBoxControl lstSelected;
        private DevExpress.XtraEditors.SimpleButton btnMoveRightAll;
        private DevExpress.XtraEditors.SimpleButton btnMoveRight;
        private DevExpress.XtraEditors.SimpleButton btnMoveLeft;
        private DevExpress.XtraEditors.SimpleButton btnMoveLeftAll;
        private DevExpress.XtraEditors.StyleController styleController1;
        private DevExpress.XtraEditors.LabelControl lblAvailable;
        private DevExpress.XtraEditors.LabelControl lblSelected;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
    }
}

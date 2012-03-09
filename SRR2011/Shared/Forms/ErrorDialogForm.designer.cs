namespace Disney.iDash.Shared.Forms
{
    partial class ErrorDialogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorDialogForm));
            this.txtStackTrace = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnStackTrace = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip();
            this.btnSend = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtStackTrace
            // 
            resources.ApplyResources(this.txtStackTrace, "txtStackTrace");
            this.txtStackTrace.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStackTrace.Name = "txtStackTrace";
            this.txtStackTrace.ReadOnly = true;
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCopy
            // 
            resources.ApplyResources(this.btnCopy, "btnCopy");
            this.btnCopy.Name = "btnCopy";
            this.toolTip1.SetToolTip(this.btnCopy, resources.GetString("btnCopy.ToolTip"));
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Disney.iDash.Shared.Properties.Resources.Error_Icon;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // btnStackTrace
            // 
            resources.ApplyResources(this.btnStackTrace, "btnStackTrace");
            this.btnStackTrace.Name = "btnStackTrace";
            this.toolTip1.SetToolTip(this.btnStackTrace, resources.GetString("btnStackTrace.ToolTip"));
            this.btnStackTrace.UseVisualStyleBackColor = true;
            this.btnStackTrace.Click += new System.EventHandler(this.btnStackTrace_Click);
            // 
            // txtMessage
            // 
            resources.ApplyResources(this.txtMessage, "txtMessage");
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessage.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Tip:";
            // 
            // btnSend
            // 
            resources.ApplyResources(this.btnSend, "btnSend");
            this.btnSend.Name = "btnSend";
            this.toolTip1.SetToolTip(this.btnSend, resources.GetString("btnSend.ToolTip"));
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // ErrorDialogForm
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnStackTrace);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtStackTrace);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "ErrorDialogForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtStackTrace;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnStackTrace;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnSend;
    }
}
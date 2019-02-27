namespace PriceManager
{
    partial class ErrorForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.copyBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.erroeText = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.copyBtn);
            this.panel1.Controls.Add(this.exitBtn);
            this.panel1.Controls.Add(this.erroeText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(376, 289);
            this.panel1.TabIndex = 0;
            // 
            // copyBtn
            // 
            this.copyBtn.Location = new System.Drawing.Point(79, 256);
            this.copyBtn.Name = "copyBtn";
            this.copyBtn.Size = new System.Drawing.Size(75, 23);
            this.copyBtn.TabIndex = 2;
            this.copyBtn.Text = "复制剪贴板";
            this.copyBtn.UseVisualStyleBackColor = true;
            this.copyBtn.Click += new System.EventHandler(this.CopyBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(202, 256);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(75, 23);
            this.exitBtn.TabIndex = 1;
            this.exitBtn.Text = "关闭";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // erroeText
            // 
            this.erroeText.Dock = System.Windows.Forms.DockStyle.Top;
            this.erroeText.Location = new System.Drawing.Point(0, 0);
            this.erroeText.Multiline = true;
            this.erroeText.Name = "erroeText";
            this.erroeText.ReadOnly = true;
            this.erroeText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.erroeText.Size = new System.Drawing.Size(376, 250);
            this.erroeText.TabIndex = 0;
            // 
            // ErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 289);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Name = "ErrorForm";
            this.Text = "错误信息";
            this.Load += new System.EventHandler(this.ErrorForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox erroeText;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button copyBtn;
    }
}
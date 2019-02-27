namespace PriceManager
{
    partial class TextForm
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
            this.enterBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // enterBtn
            // 
            this.enterBtn.Location = new System.Drawing.Point(12, 269);
            this.enterBtn.Name = "enterBtn";
            this.enterBtn.Size = new System.Drawing.Size(86, 29);
            this.enterBtn.TabIndex = 0;
            this.enterBtn.Text = "确认";
            this.enterBtn.UseVisualStyleBackColor = true;
            this.enterBtn.Click += new System.EventHandler(this.EnterBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(130, 269);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(93, 29);
            this.exitBtn.TabIndex = 1;
            this.exitBtn.Text = "取消";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 13);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(210, 250);
            this.textBox1.TabIndex = 2;
            // 
            // TextForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 310);
            this.ControlBox = false;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.enterBtn);
            this.Name = "TextForm";
            this.Text = "输入框";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button enterBtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.TextBox textBox1;
    }
}
namespace PriceManager
{
    partial class ReSetPasswordForm
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
            this.newPwd = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.exitBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.oldPwd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // newPwd
            // 
            this.newPwd.Location = new System.Drawing.Point(82, 45);
            this.newPwd.Name = "newPwd";
            this.newPwd.Size = new System.Drawing.Size(175, 21);
            this.newPwd.TabIndex = 68;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(29, 48);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(47, 12);
            this.label31.TabIndex = 67;
            this.label31.Text = "新密码:";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(31, 81);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(75, 23);
            this.exitBtn.TabIndex = 69;
            this.exitBtn.Text = "取消";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(182, 81);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 70;
            this.saveBtn.Text = "确定";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // oldPwd
            // 
            this.oldPwd.Location = new System.Drawing.Point(82, 12);
            this.oldPwd.Name = "oldPwd";
            this.oldPwd.Size = new System.Drawing.Size(175, 21);
            this.oldPwd.TabIndex = 72;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 71;
            this.label1.Text = "旧密码:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ReSetPasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 125);
            this.ControlBox = false;
            this.Controls.Add(this.oldPwd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.newPwd);
            this.Controls.Add(this.label31);
            this.Name = "ReSetPasswordForm";
            this.Text = "修改密码";
            this.Load += new System.EventHandler(this.ReSetPasswordForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox newPwd;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.TextBox oldPwd;
        private System.Windows.Forms.Label label1;
    }
}
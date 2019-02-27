namespace PriceManager
{
    partial class LoginForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.exitBtn = new System.Windows.Forms.Button();
            this.loginBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.deptComboBox = new System.Windows.Forms.ComboBox();
            this.passWord = new System.Windows.Forms.TextBox();
            this.userName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.loginMsg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(60, 238);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(75, 23);
            this.exitBtn.TabIndex = 5;
            this.exitBtn.Text = "退出";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // loginBtn
            // 
            this.loginBtn.Location = new System.Drawing.Point(268, 238);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(75, 23);
            this.loginBtn.TabIndex = 4;
            this.loginBtn.Text = "登陆";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 276);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(317, 12);
            this.label1.TabIndex = 16;
            this.label1.Text = "Copyright © 2017-2019 国药控股广州有限公司信息技术部";
            // 
            // deptComboBox
            // 
            this.deptComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.deptComboBox.FormattingEnabled = true;
            this.deptComboBox.Location = new System.Drawing.Point(122, 187);
            this.deptComboBox.Name = "deptComboBox";
            this.deptComboBox.Size = new System.Drawing.Size(201, 20);
            this.deptComboBox.TabIndex = 3;
            // 
            // passWord
            // 
            this.passWord.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.passWord.Location = new System.Drawing.Point(122, 150);
            this.passWord.Name = "passWord";
            this.passWord.PasswordChar = '*';
            this.passWord.Size = new System.Drawing.Size(201, 21);
            this.passWord.TabIndex = 2;
            this.passWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PassWord_KeyDown);
            // 
            // userName
            // 
            this.userName.Location = new System.Drawing.Point(122, 123);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(201, 21);
            this.userName.TabIndex = 1;
            this.userName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserName_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(60, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 19);
            this.label2.TabIndex = 20;
            this.label2.Text = "账号:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(60, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 19);
            this.label3.TabIndex = 21;
            this.label3.Text = "密码:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(60, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 19);
            this.label4.TabIndex = 22;
            this.label4.Text = "部门:";
            // 
            // pictureBox
            // 
            this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
            this.pictureBox.Location = new System.Drawing.Point(34, 21);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(80, 80);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 23;
            this.pictureBox.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(115, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 39);
            this.label5.TabIndex = 24;
            this.label5.Text = "国药控股";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(115, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(249, 39);
            this.label6.TabIndex = 25;
            this.label6.Text = "价格体系管理系统";
            // 
            // loginMsg
            // 
            this.loginMsg.AutoSize = true;
            this.loginMsg.ForeColor = System.Drawing.Color.Red;
            this.loginMsg.Location = new System.Drawing.Point(120, 219);
            this.loginMsg.Name = "loginMsg";
            this.loginMsg.Size = new System.Drawing.Size(0, 12);
            this.loginMsg.TabIndex = 26;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(389, 302);
            this.ControlBox = false;
            this.Controls.Add(this.loginMsg);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.passWord);
            this.Controls.Add(this.deptComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.exitBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统登陆";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox deptComboBox;
        private System.Windows.Forms.TextBox passWord;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label loginMsg;
    }
}


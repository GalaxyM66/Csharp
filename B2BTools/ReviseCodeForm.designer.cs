namespace PriceManager.B2BTools
{
    partial class ReviseCodeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtCmsGoodsCode = new System.Windows.Forms.TextBox();
            this.confirmUpdateBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入修改后的<cms商品代码>：";
            // 
            // txtCmsGoodsCode
            // 
            this.txtCmsGoodsCode.Location = new System.Drawing.Point(176, 84);
            this.txtCmsGoodsCode.Name = "txtCmsGoodsCode";
            this.txtCmsGoodsCode.Size = new System.Drawing.Size(243, 28);
            this.txtCmsGoodsCode.TabIndex = 1;
            // 
            // confirmUpdateBtn
            // 
            this.confirmUpdateBtn.Location = new System.Drawing.Point(242, 139);
            this.confirmUpdateBtn.Name = "confirmUpdateBtn";
            this.confirmUpdateBtn.Size = new System.Drawing.Size(121, 41);
            this.confirmUpdateBtn.TabIndex = 2;
            this.confirmUpdateBtn.Text = "确定修改";
            this.confirmUpdateBtn.UseVisualStyleBackColor = true;
            this.confirmUpdateBtn.Click += new System.EventHandler(this.confirmUpdateBtn_Click);
            // 
            // ReviseCodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 207);
            this.Controls.Add(this.confirmUpdateBtn);
            this.Controls.Add(this.txtCmsGoodsCode);
            this.Controls.Add(this.label1);
            this.Name = "ReviseCodeForm";
            this.Text = "ReviseCodeForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCmsGoodsCode;
        private System.Windows.Forms.Button confirmUpdateBtn;
    }
}
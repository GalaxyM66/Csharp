namespace PriceManager
{
    partial class AddOrUpdateCstCheckConfigForm
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
            this.txtCstName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCstCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtManuFacture = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtExtMark = new System.Windows.Forms.TextBox();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnUpdate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtCstName
            // 
            this.txtCstName.Enabled = false;
            this.txtCstName.Location = new System.Drawing.Point(116, 70);
            this.txtCstName.Name = "txtCstName";
            this.txtCstName.Size = new System.Drawing.Size(180, 21);
            this.txtCstName.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "客户名称：";
            // 
            // txtCstCode
            // 
            this.txtCstCode.Location = new System.Drawing.Point(116, 26);
            this.txtCstCode.Name = "txtCstCode";
            this.txtCstCode.Size = new System.Drawing.Size(180, 21);
            this.txtCstCode.TabIndex = 12;
            this.txtCstCode.Leave += new System.EventHandler(this.txtCstCode_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "客户代码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "生产日期：";
            // 
            // txtManuFacture
            // 
            this.txtManuFacture.Location = new System.Drawing.Point(116, 113);
            this.txtManuFacture.Name = "txtManuFacture";
            this.txtManuFacture.Size = new System.Drawing.Size(180, 21);
            this.txtManuFacture.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "特殊备注：";
            // 
            // txtExtMark
            // 
            this.txtExtMark.Location = new System.Drawing.Point(116, 160);
            this.txtExtMark.Multiline = true;
            this.txtExtMark.Name = "txtExtMark";
            this.txtExtMark.Size = new System.Drawing.Size(180, 71);
            this.txtExtMark.TabIndex = 17;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(150, 254);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(83, 25);
            this.BtnAdd.TabIndex = 19;
            this.BtnAdd.Text = "新  增";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.Location = new System.Drawing.Point(150, 274);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(83, 25);
            this.BtnUpdate.TabIndex = 20;
            this.BtnUpdate.Text = "修  改";
            this.BtnUpdate.UseVisualStyleBackColor = true;
            this.BtnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // AddOrUpdateCstCheckConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 341);
            this.Controls.Add(this.BtnUpdate);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.txtExtMark);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtManuFacture);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCstName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCstCode);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "AddOrUpdateCstCheckConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddOrUpdateCstCheckConfigForm";
            this.Load += new System.EventHandler(this.AddOrUpdateCstCheckConfigForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCstName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCstCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtManuFacture;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtExtMark;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnUpdate;
    }
}
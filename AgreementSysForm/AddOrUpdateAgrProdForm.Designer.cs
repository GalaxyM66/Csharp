namespace PriceManager
{
    partial class AddOrUpdateAgrProdForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnUpdate = new System.Windows.Forms.Button();
            this.txtProdCode = new System.Windows.Forms.TextBox();
            this.txtProdName = new System.Windows.Forms.TextBox();
            this.txtMiddleMan = new System.Windows.Forms.TextBox();
            this.cBImport = new System.Windows.Forms.ComboBox();
            this.cBBuyerName = new System.Windows.Forms.ComboBox();
            this.cBManager = new System.Windows.Forms.ComboBox();
            this.cBAgreeType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.DateBegin = new System.Windows.Forms.DateTimePicker();
            this.BtnBatchUpdate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "供应商编码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(274, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "商务团队：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "厂家性质：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(274, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "协议性质：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "采购员：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(274, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "销售对接人：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(44, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "采购经理：";
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(212, 212);
            this.BtnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(94, 26);
            this.BtnAdd.TabIndex = 8;
            this.BtnAdd.Text = "新  增";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.Location = new System.Drawing.Point(212, 225);
            this.BtnUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(94, 26);
            this.BtnUpdate.TabIndex = 9;
            this.BtnUpdate.Text = "修  改";
            this.BtnUpdate.UseVisualStyleBackColor = true;
            this.BtnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // txtProdCode
            // 
            this.txtProdCode.Location = new System.Drawing.Point(122, 30);
            this.txtProdCode.Name = "txtProdCode";
            this.txtProdCode.Size = new System.Drawing.Size(123, 21);
            this.txtProdCode.TabIndex = 10;
            // 
            // txtProdName
            // 
            this.txtProdName.Location = new System.Drawing.Point(353, 30);
            this.txtProdName.Name = "txtProdName";
            this.txtProdName.Size = new System.Drawing.Size(123, 21);
            this.txtProdName.TabIndex = 11;
            // 
            // txtMiddleMan
            // 
            this.txtMiddleMan.Location = new System.Drawing.Point(353, 118);
            this.txtMiddleMan.Name = "txtMiddleMan";
            this.txtMiddleMan.Size = new System.Drawing.Size(123, 21);
            this.txtMiddleMan.TabIndex = 12;
            // 
            // cBImport
            // 
            this.cBImport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBImport.FormattingEnabled = true;
            this.cBImport.Location = new System.Drawing.Point(122, 74);
            this.cBImport.Name = "cBImport";
            this.cBImport.Size = new System.Drawing.Size(123, 20);
            this.cBImport.TabIndex = 21;
            // 
            // cBBuyerName
            // 
            this.cBBuyerName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBBuyerName.FormattingEnabled = true;
            this.cBBuyerName.Location = new System.Drawing.Point(122, 118);
            this.cBBuyerName.Name = "cBBuyerName";
            this.cBBuyerName.Size = new System.Drawing.Size(123, 20);
            this.cBBuyerName.TabIndex = 22;
            // 
            // cBManager
            // 
            this.cBManager.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBManager.FormattingEnabled = true;
            this.cBManager.Location = new System.Drawing.Point(122, 164);
            this.cBManager.Name = "cBManager";
            this.cBManager.Size = new System.Drawing.Size(123, 20);
            this.cBManager.TabIndex = 23;
            // 
            // cBAgreeType
            // 
            this.cBAgreeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBAgreeType.FormattingEnabled = true;
            this.cBAgreeType.Location = new System.Drawing.Point(353, 74);
            this.cBAgreeType.Name = "cBAgreeType";
            this.cBAgreeType.Size = new System.Drawing.Size(123, 20);
            this.cBAgreeType.TabIndex = 24;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(274, 166);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 25;
            this.label8.Text = "协议启动时间：";
            // 
            // DateBegin
            // 
            this.DateBegin.Location = new System.Drawing.Point(354, 162);
            this.DateBegin.Margin = new System.Windows.Forms.Padding(2);
            this.DateBegin.Name = "DateBegin";
            this.DateBegin.Size = new System.Drawing.Size(122, 21);
            this.DateBegin.TabIndex = 26;
            // 
            // BtnBatchUpdate
            // 
            this.BtnBatchUpdate.Location = new System.Drawing.Point(212, 242);
            this.BtnBatchUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.BtnBatchUpdate.Name = "BtnBatchUpdate";
            this.BtnBatchUpdate.Size = new System.Drawing.Size(94, 26);
            this.BtnBatchUpdate.TabIndex = 27;
            this.BtnBatchUpdate.Text = "批量修改";
            this.BtnBatchUpdate.UseVisualStyleBackColor = true;
            this.BtnBatchUpdate.Click += new System.EventHandler(this.BtnBatchUpdate_Click);
            // 
            // AddOrUpdateAgrProdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 299);
            this.Controls.Add(this.BtnBatchUpdate);
            this.Controls.Add(this.DateBegin);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cBAgreeType);
            this.Controls.Add(this.cBManager);
            this.Controls.Add(this.cBBuyerName);
            this.Controls.Add(this.cBImport);
            this.Controls.Add(this.txtMiddleMan);
            this.Controls.Add(this.txtProdName);
            this.Controls.Add(this.txtProdCode);
            this.Controls.Add(this.BtnUpdate);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AddOrUpdateAgrProdForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddOrUpdateAgrProdForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddOrUpdateAgrProdForm_FormClosing);
            this.Load += new System.EventHandler(this.AddOrUpdateAgrProdForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnUpdate;
        private System.Windows.Forms.TextBox txtProdCode;
        private System.Windows.Forms.TextBox txtProdName;
        private System.Windows.Forms.TextBox txtMiddleMan;
        private System.Windows.Forms.ComboBox cBImport;
        private System.Windows.Forms.ComboBox cBBuyerName;
        private System.Windows.Forms.ComboBox cBManager;
        private System.Windows.Forms.ComboBox cBAgreeType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker DateBegin;
        private System.Windows.Forms.Button BtnBatchUpdate;
    }
}
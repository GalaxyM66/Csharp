namespace PriceManager
{
    partial class AgreementForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cBBuyer = new System.Windows.Forms.ComboBox();
            this.BtnAll = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnDel = new System.Windows.Forms.Button();
            this.BtnSel = new System.Windows.Forms.Button();
            this.cBAgreeType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMiddleMan = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProdName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProdCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvAgreementInfo = new gfoidl.Windows.Forms.gfDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAgreementInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cBBuyer);
            this.groupBox1.Controls.Add(this.BtnAll);
            this.groupBox1.Controls.Add(this.BtnAdd);
            this.groupBox1.Controls.Add(this.BtnDel);
            this.groupBox1.Controls.Add(this.BtnSel);
            this.groupBox1.Controls.Add(this.cBAgreeType);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtMiddleMan);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtProdName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtProdCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(923, 117);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "功能模块";
            // 
            // cBBuyer
            // 
            this.cBBuyer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBBuyer.FormattingEnabled = true;
            this.cBBuyer.Location = new System.Drawing.Point(100, 29);
            this.cBBuyer.Name = "cBBuyer";
            this.cBBuyer.Size = new System.Drawing.Size(92, 20);
            this.cBBuyer.TabIndex = 20;
            // 
            // BtnAll
            // 
            this.BtnAll.Location = new System.Drawing.Point(724, 25);
            this.BtnAll.Name = "BtnAll";
            this.BtnAll.Size = new System.Drawing.Size(84, 26);
            this.BtnAll.TabIndex = 19;
            this.BtnAll.Text = "全  选";
            this.BtnAll.UseVisualStyleBackColor = true;
            this.BtnAll.Click += new System.EventHandler(this.BtnAll_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(617, 64);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(84, 26);
            this.BtnAdd.TabIndex = 18;
            this.BtnAdd.Text = "新  增";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnDel
            // 
            this.BtnDel.Location = new System.Drawing.Point(724, 64);
            this.BtnDel.Name = "BtnDel";
            this.BtnDel.Size = new System.Drawing.Size(84, 26);
            this.BtnDel.TabIndex = 17;
            this.BtnDel.Text = "删  除";
            this.BtnDel.UseVisualStyleBackColor = true;
            this.BtnDel.Click += new System.EventHandler(this.BtnDel_Click);
            // 
            // BtnSel
            // 
            this.BtnSel.Location = new System.Drawing.Point(499, 64);
            this.BtnSel.Name = "BtnSel";
            this.BtnSel.Size = new System.Drawing.Size(84, 26);
            this.BtnSel.TabIndex = 16;
            this.BtnSel.Text = "查  询";
            this.BtnSel.UseVisualStyleBackColor = true;
            this.BtnSel.Click += new System.EventHandler(this.BtnSel_Click);
            // 
            // cBAgreeType
            // 
            this.cBAgreeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBAgreeType.FormattingEnabled = true;
            this.cBAgreeType.Location = new System.Drawing.Point(100, 68);
            this.cBAgreeType.Name = "cBAgreeType";
            this.cBAgreeType.Size = new System.Drawing.Size(92, 20);
            this.cBAgreeType.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "协议性质：";
            // 
            // txtMiddleMan
            // 
            this.txtMiddleMan.Location = new System.Drawing.Point(317, 29);
            this.txtMiddleMan.Name = "txtMiddleMan";
            this.txtMiddleMan.Size = new System.Drawing.Size(133, 21);
            this.txtMiddleMan.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(246, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "销售对接人：";
            // 
            // txtProdName
            // 
            this.txtProdName.Location = new System.Drawing.Point(317, 68);
            this.txtProdName.Name = "txtProdName";
            this.txtProdName.Size = new System.Drawing.Size(133, 21);
            this.txtProdName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(246, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "商务团队：";
            // 
            // txtProdCode
            // 
            this.txtProdCode.Location = new System.Drawing.Point(574, 29);
            this.txtProdCode.Name = "txtProdCode";
            this.txtProdCode.Size = new System.Drawing.Size(128, 21);
            this.txtProdCode.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(496, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "供应商编号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "采购员：";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvAgreementInfo);
            this.groupBox2.Location = new System.Drawing.Point(7, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(921, 477);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "协议信息";
            // 
            // dgvAgreementInfo
            // 
            this.dgvAgreementInfo.AllowUserToAddRows = false;
            this.dgvAgreementInfo.AllowUserToDeleteRows = false;
            this.dgvAgreementInfo.AllowUserToResizeRows = false;
            this.dgvAgreementInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAgreementInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAgreementInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column8,
            this.Column6,
            this.Column9,
            this.Column13,
            this.Column34,
            this.Column35,
            this.Column36,
            this.Column4,
            this.Column2,
            this.Column3,
            this.Column5});
            this.dgvAgreementInfo.Location = new System.Drawing.Point(6, 20);
            this.dgvAgreementInfo.Name = "dgvAgreementInfo";
            this.dgvAgreementInfo.ReadOnly = true;
            this.dgvAgreementInfo.RowHeadersVisible = false;
            this.dgvAgreementInfo.RowTemplate.Height = 23;
            this.dgvAgreementInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAgreementInfo.Size = new System.Drawing.Size(909, 449);
            this.dgvAgreementInfo.TabIndex = 47;
            this.dgvAgreementInfo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvAgreementInfo_MouseDoubleClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "TagPtr";
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            this.Column1.Width = 72;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column8.DataPropertyName = "ProdId";
            this.Column8.HeaderText = "供应商ID";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 78;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column6.DataPropertyName = "ProdCode";
            this.Column6.HeaderText = "供应商编号";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 90;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column9.DataPropertyName = "ProdName";
            this.Column9.HeaderText = "商务团队";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 78;
            // 
            // Column13
            // 
            this.Column13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column13.DataPropertyName = "Import";
            this.Column13.HeaderText = "厂家性质";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 78;
            // 
            // Column34
            // 
            this.Column34.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column34.DataPropertyName = "BuyerName";
            this.Column34.HeaderText = "采购员";
            this.Column34.Name = "Column34";
            this.Column34.ReadOnly = true;
            this.Column34.Width = 66;
            // 
            // Column35
            // 
            this.Column35.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column35.DataPropertyName = "Manager";
            this.Column35.HeaderText = "采购经理";
            this.Column35.Name = "Column35";
            this.Column35.ReadOnly = true;
            this.Column35.Width = 78;
            // 
            // Column36
            // 
            this.Column36.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column36.DataPropertyName = "AgreeType";
            this.Column36.HeaderText = "协议性质";
            this.Column36.Name = "Column36";
            this.Column36.ReadOnly = true;
            this.Column36.Width = 78;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column4.DataPropertyName = "MiddleMan";
            this.Column4.HeaderText = "销售对接人";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 90;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.DataPropertyName = "BeginDate";
            this.Column2.HeaderText = "协议启动时间";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 102;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "CompId";
            this.Column3.HeaderText = "账套ID";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Visible = false;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "OwnerId";
            this.Column5.HeaderText = "货主ID";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Visible = false;
            // 
            // AgreementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 616);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "AgreementForm";
            this.Text = "AgreementForm";
            this.Load += new System.EventHandler(this.AgreementForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAgreementInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cBAgreeType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMiddleMan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProdName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProdCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BtnAll;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnDel;
        private System.Windows.Forms.Button BtnSel;
        private gfoidl.Windows.Forms.gfDataGridView dgvAgreementInfo;
        private System.Windows.Forms.ComboBox cBBuyer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column34;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column35;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column36;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}
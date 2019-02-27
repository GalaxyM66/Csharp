namespace PriceManager
{
    partial class ContractForm
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
            this.txtHdrMark = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnCommit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnCheck = new System.Windows.Forms.Button();
            this.cBtrans = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cBBilingType = new System.Windows.Forms.ComboBox();
            this.cBPay = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cBSendAdr = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvContractInfo = new System.Windows.Forms.DataGridView();
            this.Column0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnUpdate = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContractInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.BtnUpdate);
            this.groupBox1.Controls.Add(this.txtHdrMark);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.BtnCommit);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.BtnCheck);
            this.groupBox1.Controls.Add(this.cBtrans);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cBBilingType);
            this.groupBox1.Controls.Add(this.cBPay);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cBSendAdr);
            this.groupBox1.Location = new System.Drawing.Point(6, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(869, 131);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "合同操作";
            // 
            // txtHdrMark
            // 
            this.txtHdrMark.Location = new System.Drawing.Point(87, 85);
            this.txtHdrMark.Margin = new System.Windows.Forms.Padding(2);
            this.txtHdrMark.Name = "txtHdrMark";
            this.txtHdrMark.Size = new System.Drawing.Size(376, 21);
            this.txtHdrMark.TabIndex = 45;
            this.txtHdrMark.Leave += new System.EventHandler(this.txtHdrMark_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 88);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 44;
            this.label5.Text = "合同备注:";
            // 
            // BtnCommit
            // 
            this.BtnCommit.Enabled = false;
            this.BtnCommit.Location = new System.Drawing.Point(654, 83);
            this.BtnCommit.Name = "BtnCommit";
            this.BtnCommit.Size = new System.Drawing.Size(75, 23);
            this.BtnCommit.TabIndex = 23;
            this.BtnCommit.Text = "提交";
            this.BtnCommit.UseVisualStyleBackColor = true;
            this.BtnCommit.Click += new System.EventHandler(this.BtnCommit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "发运方式:";
            // 
            // BtnCheck
            // 
            this.BtnCheck.Location = new System.Drawing.Point(566, 83);
            this.BtnCheck.Name = "BtnCheck";
            this.BtnCheck.Size = new System.Drawing.Size(75, 23);
            this.BtnCheck.TabIndex = 22;
            this.BtnCheck.Text = "校验";
            this.BtnCheck.UseVisualStyleBackColor = true;
            this.BtnCheck.Click += new System.EventHandler(this.BtnCheck_Click);
            // 
            // cBtrans
            // 
            this.cBtrans.FormattingEnabled = true;
            this.cBtrans.Location = new System.Drawing.Point(87, 25);
            this.cBtrans.Name = "cBtrans";
            this.cBtrans.Size = new System.Drawing.Size(163, 20);
            this.cBtrans.TabIndex = 13;
            this.cBtrans.SelectedValueChanged += new System.EventHandler(this.cBtrans_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(277, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "开单方式:";
            // 
            // cBBilingType
            // 
            this.cBBilingType.FormattingEnabled = true;
            this.cBBilingType.Location = new System.Drawing.Point(342, 26);
            this.cBBilingType.Name = "cBBilingType";
            this.cBBilingType.Size = new System.Drawing.Size(121, 20);
            this.cBBilingType.TabIndex = 15;
            this.cBBilingType.SelectedValueChanged += new System.EventHandler(this.cBBilingType_SelectedValueChanged);
            // 
            // cBPay
            // 
            this.cBPay.FormattingEnabled = true;
            this.cBPay.Location = new System.Drawing.Point(557, 25);
            this.cBPay.Name = "cBPay";
            this.cBPay.Size = new System.Drawing.Size(172, 20);
            this.cBPay.TabIndex = 19;
            this.cBPay.SelectedValueChanged += new System.EventHandler(this.cBPay_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "送货地址:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(492, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "支付方式:";
            // 
            // cBSendAdr
            // 
            this.cBSendAdr.FormattingEnabled = true;
            this.cBSendAdr.Location = new System.Drawing.Point(87, 53);
            this.cBSendAdr.Name = "cBSendAdr";
            this.cBSendAdr.Size = new System.Drawing.Size(642, 20);
            this.cBSendAdr.TabIndex = 17;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvContractInfo);
            this.groupBox2.Location = new System.Drawing.Point(8, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(866, 426);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "合同信息";
            // 
            // dgvContractInfo
            // 
            this.dgvContractInfo.AllowUserToAddRows = false;
            this.dgvContractInfo.AllowUserToDeleteRows = false;
            this.dgvContractInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvContractInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvContractInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column0,
            this.Column9,
            this.Column12,
            this.Column8,
            this.Column1,
            this.Column11,
            this.Column10,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column7,
            this.Column5,
            this.Column6});
            this.dgvContractInfo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvContractInfo.Location = new System.Drawing.Point(6, 20);
            this.dgvContractInfo.MultiSelect = false;
            this.dgvContractInfo.Name = "dgvContractInfo";
            this.dgvContractInfo.RowHeadersVisible = false;
            this.dgvContractInfo.RowTemplate.Height = 23;
            this.dgvContractInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvContractInfo.Size = new System.Drawing.Size(854, 400);
            this.dgvContractInfo.TabIndex = 13;
            this.dgvContractInfo.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvContractInfo_CellEndEdit);
            this.dgvContractInfo.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvContractInfo_CellMouseDoubleClick);
            // 
            // Column0
            // 
            this.Column0.DataPropertyName = "TagPtr";
            this.Column0.Frozen = true;
            this.Column0.HeaderText = "Column0";
            this.Column0.Name = "Column0";
            this.Column0.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column0.Visible = false;
            this.Column0.Width = 53;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "Batchid";
            this.Column9.HeaderText = "批次号";
            this.Column9.Name = "Column9";
            this.Column9.Visible = false;
            this.Column9.Width = 66;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "ExcelSeqid";
            this.Column12.HeaderText = "序号";
            this.Column12.Name = "Column12";
            this.Column12.Visible = false;
            this.Column12.Width = 54;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "Goods";
            this.Column8.HeaderText = "商品CMS码";
            this.Column8.Name = "Column8";
            this.Column8.Width = 84;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "GoodName";
            this.Column1.HeaderText = "商品名称";
            this.Column1.Name = "Column1";
            this.Column1.Width = 78;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "Spec";
            this.Column11.HeaderText = "规格";
            this.Column11.Name = "Column11";
            this.Column11.Width = 54;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "Producer";
            this.Column10.HeaderText = "厂家";
            this.Column10.Name = "Column10";
            this.Column10.Width = 54;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Importprc";
            this.Column2.HeaderText = "计划单价";
            this.Column2.Name = "Column2";
            this.Column2.Width = 78;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Plancount";
            this.Column3.HeaderText = "计划数量";
            this.Column3.Name = "Column3";
            this.Column3.Width = 78;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Importmsg";
            this.Column4.HeaderText = "导入信息";
            this.Column4.Name = "Column4";
            this.Column4.Width = 78;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "Importstate";
            this.Column7.HeaderText = "导入状态";
            this.Column7.Name = "Column7";
            this.Column7.Width = 78;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Pmark";
            this.Column5.HeaderText = "批卡备注";
            this.Column5.Name = "Column5";
            this.Column5.Width = 78;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column6.DataPropertyName = "Amark";
            this.Column6.HeaderText = "审批备注";
            this.Column6.Name = "Column6";
            this.Column6.Width = 78;
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.Location = new System.Drawing.Point(476, 83);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(75, 23);
            this.BtnUpdate.TabIndex = 46;
            this.BtnUpdate.Text = "修改";
            this.BtnUpdate.UseVisualStyleBackColor = true;
            this.BtnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // ContractForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 586);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.Name = "ContractForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "合同生成";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ContractForm_FormClosing);
            this.Load += new System.EventHandler(this.ContractForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvContractInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnCommit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnCheck;
        private System.Windows.Forms.ComboBox cBtrans;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cBBilingType;
        private System.Windows.Forms.ComboBox cBPay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cBSendAdr;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvContractInfo;
        private System.Windows.Forms.TextBox txtHdrMark;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column0;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.Button BtnUpdate;
    }
}
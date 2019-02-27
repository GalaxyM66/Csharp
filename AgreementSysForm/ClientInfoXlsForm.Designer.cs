namespace PriceManager
{
    partial class ClientInfoXlsForm
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
            this.BtnInsert = new System.Windows.Forms.Button();
            this.BtnExport = new System.Windows.Forms.Button();
            this.BtnCheck = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtImport = new System.Windows.Forms.TextBox();
            this.BtnImport = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvClientInfo = new gfoidl.Windows.Forms.gfDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.BtnInsert);
            this.groupBox1.Controls.Add(this.BtnExport);
            this.groupBox1.Controls.Add(this.BtnCheck);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtImport);
            this.groupBox1.Controls.Add(this.BtnImport);
            this.groupBox1.Location = new System.Drawing.Point(6, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(821, 66);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Excel工具";
            // 
            // BtnInsert
            // 
            this.BtnInsert.Enabled = false;
            this.BtnInsert.Location = new System.Drawing.Point(528, 21);
            this.BtnInsert.Margin = new System.Windows.Forms.Padding(2);
            this.BtnInsert.Name = "BtnInsert";
            this.BtnInsert.Size = new System.Drawing.Size(78, 29);
            this.BtnInsert.TabIndex = 59;
            this.BtnInsert.Text = "导  入";
            this.BtnInsert.UseVisualStyleBackColor = true;
            this.BtnInsert.Click += new System.EventHandler(this.BtnInsert_Click);
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(633, 21);
            this.BtnExport.Margin = new System.Windows.Forms.Padding(2);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(78, 29);
            this.BtnExport.TabIndex = 58;
            this.BtnExport.Text = "导  出";
            this.BtnExport.UseVisualStyleBackColor = true;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // BtnCheck
            // 
            this.BtnCheck.Location = new System.Drawing.Point(426, 21);
            this.BtnCheck.Margin = new System.Windows.Forms.Padding(2);
            this.BtnCheck.Name = "BtnCheck";
            this.BtnCheck.Size = new System.Drawing.Size(78, 29);
            this.BtnCheck.TabIndex = 57;
            this.BtnCheck.Text = "校  验";
            this.BtnCheck.UseVisualStyleBackColor = true;
            this.BtnCheck.Click += new System.EventHandler(this.BtnCheck_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 56;
            this.label1.Text = "文件路径：";
            // 
            // txtImport
            // 
            this.txtImport.Location = new System.Drawing.Point(87, 26);
            this.txtImport.Margin = new System.Windows.Forms.Padding(2);
            this.txtImport.Name = "txtImport";
            this.txtImport.Size = new System.Drawing.Size(200, 21);
            this.txtImport.TabIndex = 54;
            // 
            // BtnImport
            // 
            this.BtnImport.Location = new System.Drawing.Point(323, 21);
            this.BtnImport.Margin = new System.Windows.Forms.Padding(2);
            this.BtnImport.Name = "BtnImport";
            this.BtnImport.Size = new System.Drawing.Size(78, 29);
            this.BtnImport.TabIndex = 53;
            this.BtnImport.Text = "打开文件";
            this.BtnImport.UseVisualStyleBackColor = true;
            this.BtnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvClientInfo);
            this.groupBox2.Location = new System.Drawing.Point(8, 80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(818, 396);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "详细信息";
            // 
            // dgvClientInfo
            // 
            this.dgvClientInfo.AllowUserToAddRows = false;
            this.dgvClientInfo.AllowUserToDeleteRows = false;
            this.dgvClientInfo.AllowUserToResizeRows = false;
            this.dgvClientInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvClientInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvClientInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column20,
            this.Column21,
            this.Column22,
            this.Column23,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column24,
            this.Column16,
            this.Column17,
            this.Column18,
            this.Column19});
            this.dgvClientInfo.Location = new System.Drawing.Point(6, 20);
            this.dgvClientInfo.Name = "dgvClientInfo";
            this.dgvClientInfo.ReadOnly = true;
            this.dgvClientInfo.RowHeadersVisible = false;
            this.dgvClientInfo.RowTemplate.Height = 23;
            this.dgvClientInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientInfo.Size = new System.Drawing.Size(806, 370);
            this.dgvClientInfo.TabIndex = 59;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "TagPtr";
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.DataPropertyName = "Batchid";
            this.Column2.HeaderText = "批次号";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            this.Column2.Width = 66;
            // 
            // Column20
            // 
            this.Column20.DataPropertyName = "Compuid";
            this.Column20.HeaderText = "货主ID";
            this.Column20.Name = "Column20";
            this.Column20.ReadOnly = true;
            this.Column20.Visible = false;
            // 
            // Column21
            // 
            this.Column21.DataPropertyName = "Ownerid";
            this.Column21.HeaderText = "账套ID";
            this.Column21.Name = "Column21";
            this.Column21.ReadOnly = true;
            this.Column21.Visible = false;
            // 
            // Column22
            // 
            this.Column22.DataPropertyName = "SaleDeptid";
            this.Column22.HeaderText = "部门ID";
            this.Column22.Name = "Column22";
            this.Column22.ReadOnly = true;
            this.Column22.Visible = false;
            // 
            // Column23
            // 
            this.Column23.DataPropertyName = "Empid";
            this.Column23.HeaderText = "工号Id";
            this.Column23.Name = "Column23";
            this.Column23.ReadOnly = true;
            this.Column23.Visible = false;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.DataPropertyName = "ExcelSeqid";
            this.Column3.HeaderText = "序号";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 54;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column4.DataPropertyName = "YearNum";
            this.Column4.HeaderText = "年份";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 54;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column5.DataPropertyName = "ProdId";
            this.Column5.HeaderText = "供应商ID";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 78;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column6.DataPropertyName = "AgreeLevel";
            this.Column6.HeaderText = "协议级别";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 78;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column7.DataPropertyName = "CstCode";
            this.Column7.HeaderText = "客户代码";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 78;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column8.DataPropertyName = "CstName";
            this.Column8.HeaderText = "客户名称";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 78;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column9.DataPropertyName = "LastUpStream";
            this.Column9.HeaderText = "去年对应上游";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 102;
            // 
            // Column10
            // 
            this.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column10.DataPropertyName = "ForeCastValues";
            this.Column10.HeaderText = "今年厂家预测量";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 114;
            // 
            // Column11
            // 
            this.Column11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column11.DataPropertyName = "TarGet";
            this.Column11.HeaderText = "目标分级";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 78;
            // 
            // Column12
            // 
            this.Column12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column12.DataPropertyName = "LastValues";
            this.Column12.HeaderText = "去年销售额";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 90;
            // 
            // Column13
            // 
            this.Column13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column13.DataPropertyName = "Saller";
            this.Column13.HeaderText = "销售代表";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 78;
            // 
            // Column14
            // 
            this.Column14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column14.DataPropertyName = "SallManager";
            this.Column14.HeaderText = "销售经理";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Width = 78;
            // 
            // Column15
            // 
            this.Column15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column15.DataPropertyName = "SallLeader";
            this.Column15.HeaderText = "销售副总";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.Width = 78;
            // 
            // Column24
            // 
            this.Column24.DataPropertyName = "CreateTime";
            this.Column24.HeaderText = "创建时间";
            this.Column24.Name = "Column24";
            this.Column24.ReadOnly = true;
            this.Column24.Visible = false;
            // 
            // Column16
            // 
            this.Column16.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column16.DataPropertyName = "CheckState";
            this.Column16.HeaderText = "检查状态";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            this.Column16.Width = 78;
            // 
            // Column17
            // 
            this.Column17.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column17.DataPropertyName = "CheckMsg";
            this.Column17.HeaderText = "检查信息";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column17.Width = 78;
            // 
            // Column18
            // 
            this.Column18.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column18.DataPropertyName = "ImportState";
            this.Column18.HeaderText = "导入状态";
            this.Column18.Name = "Column18";
            this.Column18.ReadOnly = true;
            this.Column18.Width = 78;
            // 
            // Column19
            // 
            this.Column19.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column19.DataPropertyName = "ImportMsg";
            this.Column19.HeaderText = "导入信息";
            this.Column19.Name = "Column19";
            this.Column19.ReadOnly = true;
            this.Column19.Width = 78;
            // 
            // ClientInfoXlsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 488);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ClientInfoXlsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ClientInfoXlsForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.Button BtnCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtImport;
        private System.Windows.Forms.Button BtnImport;
        private System.Windows.Forms.GroupBox groupBox2;
        private gfoidl.Windows.Forms.gfDataGridView dgvClientInfo;
        private System.Windows.Forms.Button BtnInsert;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column20;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column21;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column22;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column23;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column24;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
    }
}
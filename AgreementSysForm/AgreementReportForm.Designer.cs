namespace PriceManager
{
    partial class AgreementReportForm
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
            this.cBBeginDate = new System.Windows.Forms.CheckBox();
            this.cBAgreeType = new System.Windows.Forms.CheckBox();
            this.cBBuyerName = new System.Windows.Forms.CheckBox();
            this.cBSallLeader = new System.Windows.Forms.CheckBox();
            this.cBMiddleMan = new System.Windows.Forms.CheckBox();
            this.cBSaller = new System.Windows.Forms.CheckBox();
            this.cBSallManager = new System.Windows.Forms.CheckBox();
            this.cBProdName = new System.Windows.Forms.CheckBox();
            this.cBYearNum = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cBBuyer = new System.Windows.Forms.ComboBox();
            this.BtnExport = new System.Windows.Forms.Button();
            this.BtnSel = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSallManager = new System.Windows.Forms.TextBox();
            this.txtSallLeader = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMiddleMan = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSaller = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProdName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtYearNum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvReportInfo = new gfoidl.Windows.Forms.gfDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column28 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column27 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cBBeginDate);
            this.groupBox1.Controls.Add(this.cBAgreeType);
            this.groupBox1.Controls.Add(this.cBBuyerName);
            this.groupBox1.Controls.Add(this.cBSallLeader);
            this.groupBox1.Controls.Add(this.cBMiddleMan);
            this.groupBox1.Controls.Add(this.cBSaller);
            this.groupBox1.Controls.Add(this.cBSallManager);
            this.groupBox1.Controls.Add(this.cBProdName);
            this.groupBox1.Controls.Add(this.cBYearNum);
            this.groupBox1.Location = new System.Drawing.Point(5, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(942, 45);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "维度";
            // 
            // cBBeginDate
            // 
            this.cBBeginDate.AutoSize = true;
            this.cBBeginDate.Location = new System.Drawing.Point(729, 20);
            this.cBBeginDate.Name = "cBBeginDate";
            this.cBBeginDate.Size = new System.Drawing.Size(96, 16);
            this.cBBeginDate.TabIndex = 2;
            this.cBBeginDate.Text = "协议启动时间";
            this.cBBeginDate.UseVisualStyleBackColor = true;
            // 
            // cBAgreeType
            // 
            this.cBAgreeType.AutoSize = true;
            this.cBAgreeType.Location = new System.Drawing.Point(639, 20);
            this.cBAgreeType.Name = "cBAgreeType";
            this.cBAgreeType.Size = new System.Drawing.Size(72, 16);
            this.cBAgreeType.TabIndex = 2;
            this.cBAgreeType.Text = "协议性质";
            this.cBAgreeType.UseVisualStyleBackColor = true;
            // 
            // cBBuyerName
            // 
            this.cBBuyerName.AutoSize = true;
            this.cBBuyerName.Location = new System.Drawing.Point(562, 20);
            this.cBBuyerName.Name = "cBBuyerName";
            this.cBBuyerName.Size = new System.Drawing.Size(60, 16);
            this.cBBuyerName.TabIndex = 2;
            this.cBBuyerName.Text = "采购员";
            this.cBBuyerName.UseVisualStyleBackColor = true;
            // 
            // cBSallLeader
            // 
            this.cBSallLeader.AutoSize = true;
            this.cBSallLeader.Location = new System.Drawing.Point(469, 20);
            this.cBSallLeader.Name = "cBSallLeader";
            this.cBSallLeader.Size = new System.Drawing.Size(72, 16);
            this.cBSallLeader.TabIndex = 2;
            this.cBSallLeader.Text = "销售副总";
            this.cBSallLeader.UseVisualStyleBackColor = true;
            // 
            // cBMiddleMan
            // 
            this.cBMiddleMan.AutoSize = true;
            this.cBMiddleMan.Location = new System.Drawing.Point(367, 20);
            this.cBMiddleMan.Name = "cBMiddleMan";
            this.cBMiddleMan.Size = new System.Drawing.Size(84, 16);
            this.cBMiddleMan.TabIndex = 2;
            this.cBMiddleMan.Text = "销售对接人";
            this.cBMiddleMan.UseVisualStyleBackColor = true;
            // 
            // cBSaller
            // 
            this.cBSaller.AutoSize = true;
            this.cBSaller.Location = new System.Drawing.Point(275, 20);
            this.cBSaller.Name = "cBSaller";
            this.cBSaller.Size = new System.Drawing.Size(72, 16);
            this.cBSaller.TabIndex = 2;
            this.cBSaller.Text = "销售代表";
            this.cBSaller.UseVisualStyleBackColor = true;
            // 
            // cBSallManager
            // 
            this.cBSallManager.AutoSize = true;
            this.cBSallManager.Location = new System.Drawing.Point(187, 20);
            this.cBSallManager.Name = "cBSallManager";
            this.cBSallManager.Size = new System.Drawing.Size(72, 16);
            this.cBSallManager.TabIndex = 2;
            this.cBSallManager.Text = "销售经理";
            this.cBSallManager.UseVisualStyleBackColor = true;
            // 
            // cBProdName
            // 
            this.cBProdName.AutoSize = true;
            this.cBProdName.Location = new System.Drawing.Point(96, 20);
            this.cBProdName.Name = "cBProdName";
            this.cBProdName.Size = new System.Drawing.Size(72, 16);
            this.cBProdName.TabIndex = 2;
            this.cBProdName.Text = "商务团队";
            this.cBProdName.UseVisualStyleBackColor = true;
            // 
            // cBYearNum
            // 
            this.cBYearNum.AutoSize = true;
            this.cBYearNum.Location = new System.Drawing.Point(32, 20);
            this.cBYearNum.Name = "cBYearNum";
            this.cBYearNum.Size = new System.Drawing.Size(48, 16);
            this.cBYearNum.TabIndex = 1;
            this.cBYearNum.Text = "年份";
            this.cBYearNum.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cBBuyer);
            this.groupBox2.Controls.Add(this.BtnExport);
            this.groupBox2.Controls.Add(this.BtnSel);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtSallManager);
            this.groupBox2.Controls.Add(this.txtSallLeader);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtMiddleMan);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtSaller);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtProdName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtYearNum);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(5, 55);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(942, 107);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "查询条件";
            // 
            // cBBuyer
            // 
            this.cBBuyer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBBuyer.FormattingEnabled = true;
            this.cBBuyer.Location = new System.Drawing.Point(417, 67);
            this.cBBuyer.Name = "cBBuyer";
            this.cBBuyer.Size = new System.Drawing.Size(89, 20);
            this.cBBuyer.TabIndex = 51;
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(613, 63);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(69, 29);
            this.BtnExport.TabIndex = 50;
            this.BtnExport.Text = "导出";
            this.BtnExport.UseVisualStyleBackColor = true;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // BtnSel
            // 
            this.BtnSel.Location = new System.Drawing.Point(530, 63);
            this.BtnSel.Name = "BtnSel";
            this.BtnSel.Size = new System.Drawing.Size(69, 29);
            this.BtnSel.TabIndex = 49;
            this.BtnSel.Text = "查询";
            this.BtnSel.UseVisualStyleBackColor = true;
            this.BtnSel.Click += new System.EventHandler(this.BtnSel_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(363, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 47;
            this.label7.Text = "采购员：";
            // 
            // txtSallManager
            // 
            this.txtSallManager.Location = new System.Drawing.Point(417, 26);
            this.txtSallManager.Name = "txtSallManager";
            this.txtSallManager.Size = new System.Drawing.Size(89, 21);
            this.txtSallManager.TabIndex = 46;
            // 
            // txtSallLeader
            // 
            this.txtSallLeader.Location = new System.Drawing.Point(268, 66);
            this.txtSallLeader.Name = "txtSallLeader";
            this.txtSallLeader.Size = new System.Drawing.Size(89, 21);
            this.txtSallLeader.TabIndex = 45;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(197, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 44;
            this.label6.Text = "销售副总：";
            // 
            // txtMiddleMan
            // 
            this.txtMiddleMan.Location = new System.Drawing.Point(102, 66);
            this.txtMiddleMan.Name = "txtMiddleMan";
            this.txtMiddleMan.Size = new System.Drawing.Size(89, 21);
            this.txtMiddleMan.TabIndex = 43;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 42;
            this.label5.Text = "销售对接人：";
            // 
            // txtSaller
            // 
            this.txtSaller.Location = new System.Drawing.Point(583, 26);
            this.txtSaller.Name = "txtSaller";
            this.txtSaller.Size = new System.Drawing.Size(99, 21);
            this.txtSaller.TabIndex = 41;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(512, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 40;
            this.label4.Text = "销售代表：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(346, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 38;
            this.label3.Text = "销售经理：";
            // 
            // txtProdName
            // 
            this.txtProdName.Location = new System.Drawing.Point(222, 26);
            this.txtProdName.Name = "txtProdName";
            this.txtProdName.Size = new System.Drawing.Size(118, 21);
            this.txtProdName.TabIndex = 37;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(151, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 36;
            this.label2.Text = "商务团队：";
            // 
            // txtYearNum
            // 
            this.txtYearNum.Location = new System.Drawing.Point(66, 26);
            this.txtYearNum.Name = "txtYearNum";
            this.txtYearNum.Size = new System.Drawing.Size(79, 21);
            this.txtYearNum.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "年份：";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dgvReportInfo);
            this.groupBox3.Location = new System.Drawing.Point(5, 168);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(941, 446);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "报表数据";
            // 
            // dgvReportInfo
            // 
            this.dgvReportInfo.AllowUserToAddRows = false;
            this.dgvReportInfo.AllowUserToDeleteRows = false;
            this.dgvReportInfo.AllowUserToResizeRows = false;
            this.dgvReportInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvReportInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column28,
            this.Column9,
            this.Column5,
            this.Column7,
            this.Column4,
            this.Column3,
            this.Column34,
            this.Column36,
            this.Column2,
            this.Column16,
            this.Column17,
            this.Column25,
            this.Column26,
            this.Column27,
            this.Column11,
            this.Column6,
            this.Column12,
            this.Column8,
            this.Column13,
            this.Column15,
            this.Column18,
            this.Column10,
            this.Column14});
            this.dgvReportInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReportInfo.Location = new System.Drawing.Point(3, 17);
            this.dgvReportInfo.MultiSelect = false;
            this.dgvReportInfo.Name = "dgvReportInfo";
            this.dgvReportInfo.RowHeadersVisible = false;
            this.dgvReportInfo.RowTemplate.Height = 23;
            this.dgvReportInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvReportInfo.Size = new System.Drawing.Size(935, 426);
            this.dgvReportInfo.TabIndex = 53;
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
            // Column28
            // 
            this.Column28.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column28.DataPropertyName = "YearNum";
            this.Column28.HeaderText = "年份";
            this.Column28.Name = "Column28";
            this.Column28.ReadOnly = true;
            this.Column28.Visible = false;
            this.Column28.Width = 54;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column9.DataPropertyName = "ProdName";
            this.Column9.HeaderText = "商务团队";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Visible = false;
            this.Column9.Width = 78;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column5.DataPropertyName = "SallManager";
            this.Column5.HeaderText = "销售经理";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Visible = false;
            this.Column5.Width = 78;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column7.DataPropertyName = "Saller";
            this.Column7.HeaderText = "销售代表";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Visible = false;
            this.Column7.Width = 78;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column4.DataPropertyName = "MiddleMan";
            this.Column4.HeaderText = "销售对接人";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Visible = false;
            this.Column4.Width = 90;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.DataPropertyName = "SallLeader";
            this.Column3.HeaderText = "销售副总";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Visible = false;
            this.Column3.Width = 78;
            // 
            // Column34
            // 
            this.Column34.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column34.DataPropertyName = "BuyerName";
            this.Column34.HeaderText = "采购员";
            this.Column34.Name = "Column34";
            this.Column34.ReadOnly = true;
            this.Column34.Visible = false;
            this.Column34.Width = 66;
            // 
            // Column36
            // 
            this.Column36.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column36.DataPropertyName = "AgreeType";
            this.Column36.HeaderText = "协议性质";
            this.Column36.Name = "Column36";
            this.Column36.ReadOnly = true;
            this.Column36.Visible = false;
            this.Column36.Width = 78;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.DataPropertyName = "BeginDate";
            this.Column2.HeaderText = "协议启动时间";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            this.Column2.Width = 102;
            // 
            // Column16
            // 
            this.Column16.DataPropertyName = "BiBao";
            this.Column16.HeaderText = "必保客户数";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            this.Column16.Width = 90;
            // 
            // Column17
            // 
            this.Column17.DataPropertyName = "ZhengQu";
            this.Column17.HeaderText = "争取客户数";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column17.Width = 90;
            // 
            // Column25
            // 
            this.Column25.DataPropertyName = "QiTa";
            this.Column25.HeaderText = "其他客户";
            this.Column25.Name = "Column25";
            this.Column25.ReadOnly = true;
            this.Column25.Width = 78;
            // 
            // Column26
            // 
            this.Column26.DataPropertyName = "BbZq";
            this.Column26.HeaderText = "必保和争取目标客户总数";
            this.Column26.Name = "Column26";
            this.Column26.ReadOnly = true;
            this.Column26.Width = 162;
            // 
            // Column27
            // 
            this.Column27.DataPropertyName = "Xsfk";
            this.Column27.HeaderText = "销售反馈客户数";
            this.Column27.Name = "Column27";
            this.Column27.ReadOnly = true;
            this.Column27.Width = 114;
            // 
            // Column11
            // 
            this.Column11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column11.DataPropertyName = "Fkl";
            this.Column11.HeaderText = "销售反馈率";
            this.Column11.Name = "Column11";
            this.Column11.Width = 90;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "YxHx";
            this.Column6.HeaderText = "意向客户数";
            this.Column6.Name = "Column6";
            this.Column6.Width = 114;
            // 
            // Column12
            // 
            this.Column12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column12.DataPropertyName = "Yxl";
            this.Column12.HeaderText = "意向客户率";
            this.Column12.Name = "Column12";
            this.Column12.Width = 90;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "BbYxHx";
            this.Column8.HeaderText = "必保客户签订数";
            this.Column8.Name = "Column8";
            this.Column8.Width = 114;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "BbQdl";
            this.Column13.HeaderText = "必保客户签订率";
            this.Column13.Name = "Column13";
            this.Column13.Width = 90;
            // 
            // Column15
            // 
            this.Column15.DataPropertyName = "ZqHx";
            this.Column15.HeaderText = "争取客户签订数";
            this.Column15.Name = "Column15";
            // 
            // Column18
            // 
            this.Column18.DataPropertyName = "ZqQdl";
            this.Column18.HeaderText = "争取客户签订率";
            this.Column18.Name = "Column18";
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "QdHx";
            this.Column10.HeaderText = "整体签订客户数";
            this.Column10.Name = "Column10";
            this.Column10.Width = 102;
            // 
            // Column14
            // 
            this.Column14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column14.DataPropertyName = "Qdl";
            this.Column14.HeaderText = "整体签订率";
            this.Column14.Name = "Column14";
            this.Column14.Width = 90;
            // 
            // AgreementReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 618);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "AgreementReportForm";
            this.Text = "AgreementReportForm";
            this.Load += new System.EventHandler(this.AgreementReportForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cBBeginDate;
        private System.Windows.Forms.CheckBox cBAgreeType;
        private System.Windows.Forms.CheckBox cBBuyerName;
        private System.Windows.Forms.CheckBox cBSallLeader;
        private System.Windows.Forms.CheckBox cBMiddleMan;
        private System.Windows.Forms.CheckBox cBSaller;
        private System.Windows.Forms.CheckBox cBSallManager;
        private System.Windows.Forms.CheckBox cBProdName;
        private System.Windows.Forms.CheckBox cBYearNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.Button BtnSel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSallManager;
        private System.Windows.Forms.TextBox txtSallLeader;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMiddleMan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSaller;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProdName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtYearNum;
        private gfoidl.Windows.Forms.gfDataGridView dgvReportInfo;
        private System.Windows.Forms.ComboBox cBBuyer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column28;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column34;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column36;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column25;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column26;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column27;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
    }
}
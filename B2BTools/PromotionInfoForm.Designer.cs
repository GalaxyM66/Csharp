namespace PriceManager
{
    partial class PromotionInfoForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.BtnExcel = new System.Windows.Forms.Button();
            this.BtnBatchUpdate = new System.Windows.Forms.Button();
            this.BtnDel = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnQuery = new System.Windows.Forms.Button();
            this.txtLModifyUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCstName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProducer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGoods = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvPromotionInfo = new gfoidl.Windows.Forms.gfDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtPocName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPromotionInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtPocName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.BtnExcel);
            this.groupBox1.Controls.Add(this.BtnBatchUpdate);
            this.groupBox1.Controls.Add(this.BtnDel);
            this.groupBox1.Controls.Add(this.BtnAdd);
            this.groupBox1.Controls.Add(this.BtnQuery);
            this.groupBox1.Controls.Add(this.txtLModifyUser);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCstName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtProducer);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtGoods);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1067, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作信息";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(220, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 29);
            this.button1.TabIndex = 14;
            this.button1.Text = "全选";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BtnExcel
            // 
            this.BtnExcel.Location = new System.Drawing.Point(359, 65);
            this.BtnExcel.Name = "BtnExcel";
            this.BtnExcel.Size = new System.Drawing.Size(67, 29);
            this.BtnExcel.TabIndex = 13;
            this.BtnExcel.Text = "Excel导入";
            this.BtnExcel.UseVisualStyleBackColor = true;
            this.BtnExcel.Click += new System.EventHandler(this.BtnExcel_Click);
            // 
            // BtnBatchUpdate
            // 
            this.BtnBatchUpdate.Location = new System.Drawing.Point(286, 65);
            this.BtnBatchUpdate.Name = "BtnBatchUpdate";
            this.BtnBatchUpdate.Size = new System.Drawing.Size(67, 29);
            this.BtnBatchUpdate.TabIndex = 11;
            this.BtnBatchUpdate.Text = "批量修改";
            this.BtnBatchUpdate.UseVisualStyleBackColor = true;
            this.BtnBatchUpdate.Click += new System.EventHandler(this.BtnBatchUpdate_Click);
            // 
            // BtnDel
            // 
            this.BtnDel.Location = new System.Drawing.Point(154, 65);
            this.BtnDel.Name = "BtnDel";
            this.BtnDel.Size = new System.Drawing.Size(60, 29);
            this.BtnDel.TabIndex = 10;
            this.BtnDel.Text = "删除";
            this.BtnDel.UseVisualStyleBackColor = true;
            this.BtnDel.Click += new System.EventHandler(this.BtnDel_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(88, 65);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(60, 29);
            this.BtnAdd.TabIndex = 9;
            this.BtnAdd.Text = "新增";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnQuery
            // 
            this.BtnQuery.Location = new System.Drawing.Point(22, 65);
            this.BtnQuery.Name = "BtnQuery";
            this.BtnQuery.Size = new System.Drawing.Size(60, 29);
            this.BtnQuery.TabIndex = 8;
            this.BtnQuery.Text = "查询";
            this.BtnQuery.UseVisualStyleBackColor = true;
            this.BtnQuery.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // txtLModifyUser
            // 
            this.txtLModifyUser.Location = new System.Drawing.Point(624, 28);
            this.txtLModifyUser.Name = "txtLModifyUser";
            this.txtLModifyUser.Size = new System.Drawing.Size(100, 21);
            this.txtLModifyUser.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(547, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "最近修改人:";
            // 
            // txtCstName
            // 
            this.txtCstName.Location = new System.Drawing.Point(428, 28);
            this.txtCstName.Name = "txtCstName";
            this.txtCstName.Size = new System.Drawing.Size(100, 21);
            this.txtCstName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(363, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "客户名称:";
            // 
            // txtProducer
            // 
            this.txtProducer.Location = new System.Drawing.Point(242, 28);
            this.txtProducer.Name = "txtProducer";
            this.txtProducer.Size = new System.Drawing.Size(100, 21);
            this.txtProducer.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "厂家:";
            // 
            // txtGoods
            // 
            this.txtGoods.Location = new System.Drawing.Point(83, 28);
            this.txtGoods.Name = "txtGoods";
            this.txtGoods.Size = new System.Drawing.Size(100, 21);
            this.txtGoods.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "商品代码:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvPromotionInfo);
            this.groupBox2.Location = new System.Drawing.Point(5, 111);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1067, 502);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "促销信息";
            // 
            // dgvPromotionInfo
            // 
            this.dgvPromotionInfo.AllowUserToAddRows = false;
            this.dgvPromotionInfo.AllowUserToDeleteRows = false;
            this.dgvPromotionInfo.AllowUserToResizeRows = false;
            this.dgvPromotionInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPromotionInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPromotionInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column15,
            this.Column16,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column19,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column20,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column18,
            this.Column17});
            this.dgvPromotionInfo.Location = new System.Drawing.Point(7, 20);
            this.dgvPromotionInfo.Name = "dgvPromotionInfo";
            this.dgvPromotionInfo.ReadOnly = true;
            this.dgvPromotionInfo.RowHeadersVisible = false;
            this.dgvPromotionInfo.RowTemplate.Height = 23;
            this.dgvPromotionInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPromotionInfo.Size = new System.Drawing.Size(1054, 474);
            this.dgvPromotionInfo.TabIndex = 46;
            this.dgvPromotionInfo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvPromotionInfo_MouseDoubleClick);
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
            // Column15
            // 
            this.Column15.DataPropertyName = "Compid";
            this.Column15.HeaderText = "账套ID";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.Visible = false;
            // 
            // Column16
            // 
            this.Column16.DataPropertyName = "Ownerid";
            this.Column16.HeaderText = "货主ID";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            this.Column16.Visible = false;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "PocName";
            this.Column2.HeaderText = "活动政策名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 102;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Goods";
            this.Column3.HeaderText = "商品代码";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 78;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "GoodName";
            this.Column4.HeaderText = "商品名称";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 78;
            // 
            // Column19
            // 
            this.Column19.DataPropertyName = "GoodId";
            this.Column19.HeaderText = "商品ID";
            this.Column19.Name = "Column19";
            this.Column19.ReadOnly = true;
            this.Column19.Visible = false;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Spec";
            this.Column5.HeaderText = "规格";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 54;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "Producer";
            this.Column6.HeaderText = "厂家";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 54;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "CstCode";
            this.Column7.HeaderText = "活动对象代码";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 102;
            // 
            // Column20
            // 
            this.Column20.DataPropertyName = "Cstid";
            this.Column20.HeaderText = "客户ID";
            this.Column20.Name = "Column20";
            this.Column20.ReadOnly = true;
            this.Column20.Visible = false;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "CstName";
            this.Column8.HeaderText = "活动对象名称";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 102;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "BeginTime";
            this.Column9.HeaderText = "活动开始时间";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 102;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "EndTime";
            this.Column10.HeaderText = "活动结束时间";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 102;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "Policy";
            this.Column11.HeaderText = "活动政策";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 78;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "Remark";
            this.Column12.HeaderText = "备注";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 54;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "LModifyUser";
            this.Column13.HeaderText = "最近修改人";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 90;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "LModifyTime";
            this.Column14.HeaderText = "最近修改时间";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Width = 102;
            // 
            // Column18
            // 
            this.Column18.DataPropertyName = "Id";
            this.Column18.HeaderText = "标志符";
            this.Column18.Name = "Column18";
            this.Column18.ReadOnly = true;
            this.Column18.Visible = false;
            // 
            // Column17
            // 
            this.Column17.DataPropertyName = "CreateTime";
            this.Column17.HeaderText = "创建时间";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column17.Visible = false;
            // 
            // txtPocName
            // 
            this.txtPocName.Location = new System.Drawing.Point(834, 28);
            this.txtPocName.Name = "txtPocName";
            this.txtPocName.Size = new System.Drawing.Size(100, 21);
            this.txtPocName.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(745, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "活动政策名称:";
            // 
            // PromotionInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 617);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyPreview = true;
            this.Name = "PromotionInfoForm";
            this.Text = "PromotionInfoForm";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PromotionInfoForm_KeyPress);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPromotionInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BtnExcel;
        private System.Windows.Forms.Button BtnBatchUpdate;
        private System.Windows.Forms.Button BtnDel;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnQuery;
        private System.Windows.Forms.TextBox txtLModifyUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCstName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProducer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGoods;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private gfoidl.Windows.Forms.gfDataGridView dgvPromotionInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column20;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.TextBox txtPocName;
        private System.Windows.Forms.Label label5;
    }
}
namespace PriceManager
{
    partial class BTbPriceShieldForm
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
            this.BtnDel = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnSelAll = new System.Windows.Forms.Button();
            this.BtnQuery = new System.Windows.Forms.Button();
            this.txtGoods = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProdName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvPriceShieldInfo = new gfoidl.Windows.Forms.gfDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPriceShieldInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.BtnDel);
            this.groupBox1.Controls.Add(this.BtnAdd);
            this.groupBox1.Controls.Add(this.BtnSelAll);
            this.groupBox1.Controls.Add(this.BtnQuery);
            this.groupBox1.Controls.Add(this.txtGoods);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtProdName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(5, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(894, 72);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(747, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 29);
            this.button1.TabIndex = 8;
            this.button1.Text = "excel导入";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // BtnDel
            // 
            this.BtnDel.Location = new System.Drawing.Point(669, 24);
            this.BtnDel.Name = "BtnDel";
            this.BtnDel.Size = new System.Drawing.Size(59, 29);
            this.BtnDel.TabIndex = 7;
            this.BtnDel.Text = "删 除";
            this.BtnDel.UseVisualStyleBackColor = true;
            this.BtnDel.Click += new System.EventHandler(this.BtnDel_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(593, 24);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(59, 29);
            this.BtnAdd.TabIndex = 6;
            this.BtnAdd.Text = "新 增";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnSelAll
            // 
            this.BtnSelAll.Location = new System.Drawing.Point(513, 24);
            this.BtnSelAll.Name = "BtnSelAll";
            this.BtnSelAll.Size = new System.Drawing.Size(59, 29);
            this.BtnSelAll.TabIndex = 5;
            this.BtnSelAll.Text = "全 选";
            this.BtnSelAll.UseVisualStyleBackColor = true;
            this.BtnSelAll.Click += new System.EventHandler(this.BtnSelAll_Click);
            // 
            // BtnQuery
            // 
            this.BtnQuery.Location = new System.Drawing.Point(433, 24);
            this.BtnQuery.Name = "BtnQuery";
            this.BtnQuery.Size = new System.Drawing.Size(59, 29);
            this.BtnQuery.TabIndex = 4;
            this.BtnQuery.Text = "查 询";
            this.BtnQuery.UseVisualStyleBackColor = true;
            this.BtnQuery.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // txtGoods
            // 
            this.txtGoods.Location = new System.Drawing.Point(290, 29);
            this.txtGoods.Name = "txtGoods";
            this.txtGoods.Size = new System.Drawing.Size(99, 21);
            this.txtGoods.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "商品代码：";
            // 
            // txtProdName
            // 
            this.txtProdName.Location = new System.Drawing.Point(97, 29);
            this.txtProdName.Name = "txtProdName";
            this.txtProdName.Size = new System.Drawing.Size(99, 21);
            this.txtProdName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "商务团队：";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvPriceShieldInfo);
            this.groupBox2.Location = new System.Drawing.Point(5, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(893, 514);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "查询信息";
            // 
            // dgvPriceShieldInfo
            // 
            this.dgvPriceShieldInfo.AllowUserToAddRows = false;
            this.dgvPriceShieldInfo.AllowUserToDeleteRows = false;
            this.dgvPriceShieldInfo.AllowUserToResizeRows = false;
            this.dgvPriceShieldInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPriceShieldInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPriceShieldInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column8,
            this.Column9,
            this.Column6,
            this.Column13,
            this.Column34,
            this.Column35,
            this.Column36,
            this.Column4,
            this.Column2});
            this.dgvPriceShieldInfo.Location = new System.Drawing.Point(7, 20);
            this.dgvPriceShieldInfo.Name = "dgvPriceShieldInfo";
            this.dgvPriceShieldInfo.ReadOnly = true;
            this.dgvPriceShieldInfo.RowHeadersVisible = false;
            this.dgvPriceShieldInfo.RowTemplate.Height = 23;
            this.dgvPriceShieldInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPriceShieldInfo.Size = new System.Drawing.Size(880, 488);
            this.dgvPriceShieldInfo.TabIndex = 48;
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
            this.Column8.DataPropertyName = "Id";
            this.Column8.HeaderText = "序号";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 54;
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
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column6.DataPropertyName = "Goods";
            this.Column6.HeaderText = "商品代码";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 78;
            // 
            // Column13
            // 
            this.Column13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column13.DataPropertyName = "GoodsName";
            this.Column13.HeaderText = "商品名称";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 78;
            // 
            // Column34
            // 
            this.Column34.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column34.DataPropertyName = "Spec";
            this.Column34.HeaderText = "规格";
            this.Column34.Name = "Column34";
            this.Column34.ReadOnly = true;
            this.Column34.Width = 54;
            // 
            // Column35
            // 
            this.Column35.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column35.DataPropertyName = "Producer";
            this.Column35.HeaderText = "厂家";
            this.Column35.Name = "Column35";
            this.Column35.ReadOnly = true;
            this.Column35.Width = 54;
            // 
            // Column36
            // 
            this.Column36.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column36.DataPropertyName = "CreateUserCode";
            this.Column36.HeaderText = "创建人工号";
            this.Column36.Name = "Column36";
            this.Column36.ReadOnly = true;
            this.Column36.Width = 90;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column4.DataPropertyName = "CreateUserName";
            this.Column4.HeaderText = "创建人姓名";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 90;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.DataPropertyName = "CreateTime";
            this.Column2.HeaderText = "创建时间";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 78;
            // 
            // BTbPriceShieldForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 609);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "BTbPriceShieldForm";
            this.Text = "BTbPriceShieldForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPriceShieldInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnQuery;
        private System.Windows.Forms.TextBox txtGoods;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProdName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BtnDel;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnSelAll;
        private gfoidl.Windows.Forms.gfDataGridView dgvPriceShieldInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column34;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column35;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column36;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button button1;
    }
}
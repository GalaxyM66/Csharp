namespace PriceManager
{
    partial class AddSysGoodForm
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtSelName = new System.Windows.Forms.TextBox();
            this.txtSelGoodId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvGoods = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.txtProducer = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSpec = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtGoodId = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoods)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.txtSelName);
            this.groupBox4.Controls.Add(this.txtSelGoodId);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(312, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(210, 90);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "查询商品";
            // 
            // txtSelName
            // 
            this.txtSelName.Location = new System.Drawing.Point(77, 54);
            this.txtSelName.Name = "txtSelName";
            this.txtSelName.Size = new System.Drawing.Size(116, 21);
            this.txtSelName.TabIndex = 8;
            // 
            // txtSelGoodId
            // 
            this.txtSelGoodId.Location = new System.Drawing.Point(77, 20);
            this.txtSelGoodId.Name = "txtSelGoodId";
            this.txtSelGoodId.Size = new System.Drawing.Size(116, 21);
            this.txtSelGoodId.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "商品名称：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "商品代码：";
            // 
            // dgvGoods
            // 
            this.dgvGoods.AllowUserToAddRows = false;
            this.dgvGoods.AllowUserToDeleteRows = false;
            this.dgvGoods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvGoods.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvGoods.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.Column2,
            this.Column5,
            this.Column3,
            this.Column4,
            this.Column1});
            this.dgvGoods.Location = new System.Drawing.Point(6, 20);
            this.dgvGoods.Name = "dgvGoods";
            this.dgvGoods.ReadOnly = true;
            this.dgvGoods.RowHeadersVisible = false;
            this.dgvGoods.RowTemplate.Height = 23;
            this.dgvGoods.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGoods.Size = new System.Drawing.Size(286, 265);
            this.dgvGoods.TabIndex = 3;
            this.dgvGoods.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGoods_CellContentDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.dgvGoods);
            this.groupBox1.Location = new System.Drawing.Point(8, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 291);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "商品信息";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.BtnAdd);
            this.groupBox2.Controls.Add(this.txtProducer);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtSpec);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.txtGoodId);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(314, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(207, 194);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "新增商品";
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(75, 154);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(71, 27);
            this.BtnAdd.TabIndex = 26;
            this.BtnAdd.Text = "新 增";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // txtProducer
            // 
            this.txtProducer.Enabled = false;
            this.txtProducer.Location = new System.Drawing.Point(75, 127);
            this.txtProducer.Name = "txtProducer";
            this.txtProducer.Size = new System.Drawing.Size(116, 21);
            this.txtProducer.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 130);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 24;
            this.label11.Text = "厂家：";
            // 
            // txtSpec
            // 
            this.txtSpec.Enabled = false;
            this.txtSpec.Location = new System.Drawing.Point(75, 93);
            this.txtSpec.Name = "txtSpec";
            this.txtSpec.Size = new System.Drawing.Size(116, 21);
            this.txtSpec.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 96);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 22;
            this.label10.Text = "商品规格：";
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(75, 60);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(116, 21);
            this.txtName.TabIndex = 21;
            // 
            // txtGoodId
            // 
            this.txtGoodId.Enabled = false;
            this.txtGoodId.Location = new System.Drawing.Point(75, 26);
            this.txtGoodId.Name = "txtGoodId";
            this.txtGoodId.Size = new System.Drawing.Size(116, 21);
            this.txtGoodId.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "商品名称：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(13, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "商品代码：";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "TagPtr";
            this.dataGridViewTextBoxColumn4.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.DataPropertyName = "GoodId";
            this.Column2.HeaderText = "商品ID";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            this.Column2.Width = 66;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column5.DataPropertyName = "Goods";
            this.Column5.HeaderText = "商品代码";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 78;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.DataPropertyName = "Name";
            this.Column3.HeaderText = "商品名称";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 78;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column4.DataPropertyName = "Spec";
            this.Column4.HeaderText = "规格";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 54;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Producer";
            this.Column1.HeaderText = "厂家";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 54;
            // 
            // AddSysGoodForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 311);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.KeyPreview = true;
            this.Name = "AddSysGoodForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增商品";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AddSysGoodForm_KeyPress);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoods)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtSelName;
        private System.Windows.Forms.TextBox txtSelGoodId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvGoods;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtProducer;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSpec;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtGoodId;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}
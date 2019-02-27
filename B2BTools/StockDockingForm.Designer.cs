namespace PriceManager
{
    partial class StockDockingForm
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
            this.BtnExOut = new System.Windows.Forms.Button();
            this.BtnExIn = new System.Windows.Forms.Button();
            this.BtnDel = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnSel = new System.Windows.Forms.Button();
            this.txtProducer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGoods = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvStockDocking = new gfoidl.Windows.Forms.gfDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockDocking)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.BtnExOut);
            this.groupBox1.Controls.Add(this.BtnExIn);
            this.groupBox1.Controls.Add(this.BtnDel);
            this.groupBox1.Controls.Add(this.BtnAdd);
            this.groupBox1.Controls.Add(this.BtnSel);
            this.groupBox1.Controls.Add(this.txtProducer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtGoods);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1108, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "对接操作";
            // 
            // BtnExOut
            // 
            this.BtnExOut.Location = new System.Drawing.Point(972, 26);
            this.BtnExOut.Name = "BtnExOut";
            this.BtnExOut.Size = new System.Drawing.Size(76, 28);
            this.BtnExOut.TabIndex = 10;
            this.BtnExOut.Text = "Excel导出";
            this.BtnExOut.UseVisualStyleBackColor = true;
            // 
            // BtnExIn
            // 
            this.BtnExIn.Location = new System.Drawing.Point(880, 26);
            this.BtnExIn.Name = "BtnExIn";
            this.BtnExIn.Size = new System.Drawing.Size(76, 28);
            this.BtnExIn.TabIndex = 9;
            this.BtnExIn.Text = "Excel导入";
            this.BtnExIn.UseVisualStyleBackColor = true;
            // 
            // BtnDel
            // 
            this.BtnDel.Location = new System.Drawing.Point(798, 26);
            this.BtnDel.Name = "BtnDel";
            this.BtnDel.Size = new System.Drawing.Size(66, 28);
            this.BtnDel.TabIndex = 8;
            this.BtnDel.Text = "删除";
            this.BtnDel.UseVisualStyleBackColor = true;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(717, 26);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(66, 28);
            this.BtnAdd.TabIndex = 7;
            this.BtnAdd.Text = "新增";
            this.BtnAdd.UseVisualStyleBackColor = true;
            // 
            // BtnSel
            // 
            this.BtnSel.Location = new System.Drawing.Point(635, 26);
            this.BtnSel.Name = "BtnSel";
            this.BtnSel.Size = new System.Drawing.Size(66, 28);
            this.BtnSel.TabIndex = 6;
            this.BtnSel.Text = "查询";
            this.BtnSel.UseVisualStyleBackColor = true;
            this.BtnSel.Click += new System.EventHandler(this.BtnSel_Click);
            // 
            // txtProducer
            // 
            this.txtProducer.Location = new System.Drawing.Point(492, 31);
            this.txtProducer.Name = "txtProducer";
            this.txtProducer.Size = new System.Drawing.Size(117, 21);
            this.txtProducer.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(445, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "厂家：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(303, 31);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(117, 21);
            this.txtName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "商品名称：";
            // 
            // txtGoods
            // 
            this.txtGoods.Location = new System.Drawing.Point(93, 31);
            this.txtGoods.Name = "txtGoods";
            this.txtGoods.Size = new System.Drawing.Size(117, 21);
            this.txtGoods.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "商品代码：";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvStockDocking);
            this.groupBox2.Location = new System.Drawing.Point(7, 87);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1107, 528);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "库存信息";
            // 
            // dgvStockDocking
            // 
            this.dgvStockDocking.AllowUserToAddRows = false;
            this.dgvStockDocking.AllowUserToDeleteRows = false;
            this.dgvStockDocking.AllowUserToResizeRows = false;
            this.dgvStockDocking.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStockDocking.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvStockDocking.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column10,
            this.Column11,
            this.Column12});
            this.dgvStockDocking.Location = new System.Drawing.Point(6, 20);
            this.dgvStockDocking.Name = "dgvStockDocking";
            this.dgvStockDocking.ReadOnly = true;
            this.dgvStockDocking.RowHeadersVisible = false;
            this.dgvStockDocking.RowTemplate.Height = 23;
            this.dgvStockDocking.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStockDocking.Size = new System.Drawing.Size(1095, 502);
            this.dgvStockDocking.TabIndex = 46;
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
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.DataPropertyName = "Goods";
            this.Column3.HeaderText = "商品代码";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 78;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column4.DataPropertyName = "Name";
            this.Column4.HeaderText = "商品名称";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 78;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column5.DataPropertyName = "Spec";
            this.Column5.HeaderText = "商品规格";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 78;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column6.DataPropertyName = "Producer";
            this.Column6.HeaderText = "厂家";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 54;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column7.DataPropertyName = "BizOwnQty";
            this.Column7.HeaderText = "总库存";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 66;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column8.DataPropertyName = "AbutJoinQty";
            this.Column8.HeaderText = "平台可用库存";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 102;
            // 
            // Column10
            // 
            this.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column10.DataPropertyName = "TopLimit";
            this.Column10.HeaderText = "上限";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 54;
            // 
            // Column11
            // 
            this.Column11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column11.DataPropertyName = "LowerLimit";
            this.Column11.HeaderText = "下限";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 54;
            // 
            // Column12
            // 
            this.Column12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column12.DataPropertyName = "ModifyTime";
            this.Column12.HeaderText = "最近修改时间";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 102;
            // 
            // StockDockingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1128, 621);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "StockDockingForm";
            this.Text = "StockDockingForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockDocking)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnExOut;
        private System.Windows.Forms.Button BtnExIn;
        private System.Windows.Forms.Button BtnDel;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnSel;
        private System.Windows.Forms.TextBox txtProducer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGoods;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private gfoidl.Windows.Forms.gfDataGridView dgvStockDocking;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
    }
}
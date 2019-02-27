namespace PriceManager
{
    partial class GoodsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoodsForm));
            this.goodsDgv1 = new gfoidl.Windows.Forms.gfDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.producer = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.spec = new System.Windows.Forms.TextBox();
            this.count = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.queryBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportBtn = new System.Windows.Forms.ToolStripButton();
            this.goodsName = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.goodsCode = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.stopFlag_q = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtForbitarea = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLimitcsttype = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.goodsDgv1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // goodsDgv1
            // 
            this.goodsDgv1.AllowUserToAddRows = false;
            this.goodsDgv1.AllowUserToDeleteRows = false;
            this.goodsDgv1.AllowUserToOrderColumns = true;
            this.goodsDgv1.AllowUserToResizeRows = false;
            this.goodsDgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.goodsDgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column19,
            this.Column20,
            this.Column6,
            this.Column14,
            this.Column15,
            this.Column18,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column16,
            this.Column11,
            this.Column12,
            this.Column17,
            this.Column13});
            this.goodsDgv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.goodsDgv1.Location = new System.Drawing.Point(0, 0);
            this.goodsDgv1.MultiSelect = false;
            this.goodsDgv1.Name = "goodsDgv1";
            this.goodsDgv1.ReadOnly = true;
            this.goodsDgv1.RowHeadersVisible = false;
            this.goodsDgv1.RowTemplate.Height = 23;
            this.goodsDgv1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.goodsDgv1.Size = new System.Drawing.Size(1008, 592);
            this.goodsDgv1.TabIndex = 1;
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
            // Column2
            // 
            this.Column2.DataPropertyName = "goods";
            this.Column2.HeaderText = "商品代码";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 78;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "name";
            this.Column3.HeaderText = "商品名称";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 78;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "spec";
            this.Column4.HeaderText = "商品规格";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 78;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "producer";
            this.Column5.HeaderText = "生产厂家";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 78;
            // 
            // Column19
            // 
            this.Column19.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Column19.DataPropertyName = "sprdug";
            this.Column19.HeaderText = "特药";
            this.Column19.Name = "Column19";
            this.Column19.ReadOnly = true;
            this.Column19.Width = 54;
            // 
            // Column20
            // 
            this.Column20.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column20.DataPropertyName = "Ratifyflag";
            this.Column20.HeaderText = "首营";
            this.Column20.Name = "Column20";
            this.Column20.ReadOnly = true;
            this.Column20.Width = 54;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "islimitname";
            this.Column6.HeaderText = "是否限销";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 78;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "area";
            this.Column14.HeaderText = "限销区域";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Width = 78;
            // 
            // Column15
            // 
            this.Column15.DataPropertyName = "forbitarea";
            this.Column15.HeaderText = "禁销区域";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.Width = 78;
            // 
            // Column18
            // 
            this.Column18.DataPropertyName = "limitcsttype";
            this.Column18.HeaderText = "限销业态";
            this.Column18.Name = "Column18";
            this.Column18.ReadOnly = true;
            this.Column18.Width = 78;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "bargainname";
            this.Column7.HeaderText = "是否可议价";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 90;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "outrate";
            this.Column8.HeaderText = "税率";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 54;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "stopflagname";
            this.Column9.HeaderText = "停用标识";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 78;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "createusername";
            this.Column10.HeaderText = "创建人";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 66;
            // 
            // Column16
            // 
            this.Column16.DataPropertyName = "createusercode";
            this.Column16.HeaderText = "创建人工号";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            this.Column16.Width = 90;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "createdate";
            this.Column11.HeaderText = "创建时间";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 78;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "modifyusername";
            this.Column12.HeaderText = "修改人";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 66;
            // 
            // Column17
            // 
            this.Column17.DataPropertyName = "modifyusercode";
            this.Column17.HeaderText = "修改人工号";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column17.Width = 90;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "modifydate";
            this.Column13.HeaderText = "修改时间";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 78;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.goodsDgv1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 138);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1008, 592);
            this.panel2.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.txtLimitcsttype);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtForbitarea);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtArea);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.producer);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.spec);
            this.panel1.Controls.Add(this.count);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.goodsName);
            this.panel1.Controls.Add(this.label31);
            this.panel1.Controls.Add(this.goodsCode);
            this.panel1.Controls.Add(this.label30);
            this.panel1.Controls.Add(this.stopFlag_q);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 138);
            this.panel1.TabIndex = 2;
            // 
            // producer
            // 
            this.producer.Location = new System.Drawing.Point(833, 12);
            this.producer.Name = "producer";
            this.producer.Size = new System.Drawing.Size(130, 21);
            this.producer.TabIndex = 75;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(792, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 12);
            this.label10.TabIndex = 74;
            this.label10.Text = "厂家:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label9.Location = new System.Drawing.Point(557, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 72;
            this.label9.Text = "规格:";
            // 
            // spec
            // 
            this.spec.Location = new System.Drawing.Point(598, 12);
            this.spec.Name = "spec";
            this.spec.Size = new System.Drawing.Size(130, 21);
            this.spec.TabIndex = 73;
            // 
            // count
            // 
            this.count.AutoSize = true;
            this.count.Location = new System.Drawing.Point(71, 86);
            this.count.Name = "count";
            this.count.Size = new System.Drawing.Size(71, 12);
            this.count.TabIndex = 70;
            this.count.Text = "共0条数据。";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.queryBtn,
            this.toolStripSeparator2,
            this.exportBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 106);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1008, 32);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // queryBtn
            // 
            this.queryBtn.AutoSize = false;
            this.queryBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.queryBtn.Image = ((System.Drawing.Image)(resources.GetObject("queryBtn.Image")));
            this.queryBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.queryBtn.Name = "queryBtn";
            this.queryBtn.Size = new System.Drawing.Size(60, 30);
            this.queryBtn.Text = "查询";
            this.queryBtn.Click += new System.EventHandler(this.QueryBtn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // exportBtn
            // 
            this.exportBtn.AutoSize = false;
            this.exportBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exportBtn.Image = ((System.Drawing.Image)(resources.GetObject("exportBtn.Image")));
            this.exportBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(60, 30);
            this.exportBtn.Text = "导出";
            this.exportBtn.Click += new System.EventHandler(this.ExportBtn_Click);
            // 
            // goodsName
            // 
            this.goodsName.Location = new System.Drawing.Point(377, 12);
            this.goodsName.Name = "goodsName";
            this.goodsName.Size = new System.Drawing.Size(130, 21);
            this.goodsName.TabIndex = 63;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label31.Location = new System.Drawing.Point(72, 15);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(59, 12);
            this.label31.TabIndex = 60;
            this.label31.Text = "商品代码:";
            // 
            // goodsCode
            // 
            this.goodsCode.Location = new System.Drawing.Point(137, 12);
            this.goodsCode.Name = "goodsCode";
            this.goodsCode.Size = new System.Drawing.Size(130, 21);
            this.goodsCode.TabIndex = 61;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(312, 15);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(59, 12);
            this.label30.TabIndex = 62;
            this.label30.Text = "商品名称:";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // stopFlag_q
            // 
            this.stopFlag_q.FormattingEnabled = true;
            this.stopFlag_q.Location = new System.Drawing.Point(137, 52);
            this.stopFlag_q.Name = "stopFlag_q";
            this.stopFlag_q.Size = new System.Drawing.Size(130, 20);
            this.stopFlag_q.TabIndex = 59;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 58;
            this.label3.Text = "停用标示:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtArea
            // 
            this.txtArea.Location = new System.Drawing.Point(377, 52);
            this.txtArea.Name = "txtArea";
            this.txtArea.Size = new System.Drawing.Size(130, 21);
            this.txtArea.TabIndex = 77;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(312, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 76;
            this.label1.Text = "限销区域:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtForbitarea
            // 
            this.txtForbitarea.Location = new System.Drawing.Point(598, 52);
            this.txtForbitarea.Name = "txtForbitarea";
            this.txtForbitarea.Size = new System.Drawing.Size(130, 21);
            this.txtForbitarea.TabIndex = 79;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(533, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 78;
            this.label2.Text = "禁销区域:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLimitcsttype
            // 
            this.txtLimitcsttype.Location = new System.Drawing.Point(833, 52);
            this.txtLimitcsttype.Name = "txtLimitcsttype";
            this.txtLimitcsttype.Size = new System.Drawing.Size(130, 21);
            this.txtLimitcsttype.TabIndex = 81;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(768, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 80;
            this.label4.Text = "限销业态:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GoodsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyPreview = true;
            this.Name = "GoodsForm";
            this.Text = "GoodsForm";
            this.Load += new System.EventHandler(this.GoodsForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GoodsForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.goodsDgv1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private gfoidl.Windows.Forms.gfDataGridView goodsDgv1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton queryBtn;
        private System.Windows.Forms.Label count;
        private System.Windows.Forms.TextBox goodsName;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox goodsCode;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ComboBox stopFlag_q;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox producer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox spec;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton exportBtn;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column20;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.TextBox txtLimitcsttype;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtForbitarea;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtArea;
        private System.Windows.Forms.Label label1;
    }
}
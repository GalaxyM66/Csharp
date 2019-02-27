namespace PriceManager
{
    partial class ClientsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientsForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.areaCbo = new System.Windows.Forms.ComboBox();
            this.count = new System.Windows.Forms.Label();
            this.clientsType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.paytype = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.stopFlag_q = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.clientsName = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.clientsCode = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.queryBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.exportBtn = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.clientsDgv1 = new gfoidl.Windows.Forms.gfDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clientsDgv1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.areaCbo);
            this.panel1.Controls.Add(this.count);
            this.panel1.Controls.Add(this.clientsType);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.paytype);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.stopFlag_q);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.clientsName);
            this.panel1.Controls.Add(this.label30);
            this.panel1.Controls.Add(this.clientsCode);
            this.panel1.Controls.Add(this.label31);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 153);
            this.panel1.TabIndex = 0;
            // 
            // areaCbo
            // 
            this.areaCbo.DropDownHeight = 1;
            this.areaCbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.areaCbo.FormattingEnabled = true;
            this.areaCbo.IntegralHeight = false;
            this.areaCbo.Location = new System.Drawing.Point(568, 22);
            this.areaCbo.Name = "areaCbo";
            this.areaCbo.Size = new System.Drawing.Size(130, 20);
            this.areaCbo.TabIndex = 45;
            this.areaCbo.Click += new System.EventHandler(this.AreaCbo_Click);
            // 
            // count
            // 
            this.count.AutoSize = true;
            this.count.Location = new System.Drawing.Point(43, 103);
            this.count.Name = "count";
            this.count.Size = new System.Drawing.Size(71, 12);
            this.count.TabIndex = 44;
            this.count.Text = "共0条数据。";
            // 
            // clientsType
            // 
            this.clientsType.FormattingEnabled = true;
            this.clientsType.Location = new System.Drawing.Point(338, 69);
            this.clientsType.Name = "clientsType";
            this.clientsType.Size = new System.Drawing.Size(130, 20);
            this.clientsType.TabIndex = 43;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 42;
            this.label2.Text = "客户类型:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // paytype
            // 
            this.paytype.FormattingEnabled = true;
            this.paytype.Location = new System.Drawing.Point(109, 69);
            this.paytype.Name = "paytype";
            this.paytype.Size = new System.Drawing.Size(130, 20);
            this.paytype.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 40;
            this.label1.Text = "付款类型:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // stopFlag_q
            // 
            this.stopFlag_q.FormattingEnabled = true;
            this.stopFlag_q.Location = new System.Drawing.Point(810, 22);
            this.stopFlag_q.Name = "stopFlag_q";
            this.stopFlag_q.Size = new System.Drawing.Size(130, 20);
            this.stopFlag_q.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(744, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "停用标示:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(527, 25);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(35, 12);
            this.label26.TabIndex = 38;
            this.label26.Text = "区域:";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // clientsName
            // 
            this.clientsName.Location = new System.Drawing.Point(337, 22);
            this.clientsName.Name = "clientsName";
            this.clientsName.Size = new System.Drawing.Size(130, 21);
            this.clientsName.TabIndex = 37;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(272, 25);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(59, 12);
            this.label30.TabIndex = 36;
            this.label30.Text = "客户名称:";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // clientsCode
            // 
            this.clientsCode.Location = new System.Drawing.Point(108, 22);
            this.clientsCode.Name = "clientsCode";
            this.clientsCode.Size = new System.Drawing.Size(130, 21);
            this.clientsCode.TabIndex = 35;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label31.Location = new System.Drawing.Point(43, 25);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(59, 12);
            this.label31.TabIndex = 34;
            this.label31.Text = "客户代码:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.queryBtn,
            this.toolStripSeparator16,
            this.exportBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 121);
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
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(6, 32);
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
            // panel2
            // 
            this.panel2.Controls.Add(this.clientsDgv1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 153);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1008, 577);
            this.panel2.TabIndex = 1;
            // 
            // clientsDgv1
            // 
            this.clientsDgv1.AllowUserToAddRows = false;
            this.clientsDgv1.AllowUserToDeleteRows = false;
            this.clientsDgv1.AllowUserToOrderColumns = true;
            this.clientsDgv1.AllowUserToResizeRows = false;
            this.clientsDgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.clientsDgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column12,
            this.Column14,
            this.Column4,
            this.Column13,
            this.Column5,
            this.Column6,
            this.Column10,
            this.Column7,
            this.Column8,
            this.Column11,
            this.Column9});
            this.clientsDgv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clientsDgv1.Location = new System.Drawing.Point(0, 0);
            this.clientsDgv1.MultiSelect = false;
            this.clientsDgv1.Name = "clientsDgv1";
            this.clientsDgv1.ReadOnly = true;
            this.clientsDgv1.RowHeadersVisible = false;
            this.clientsDgv1.RowTemplate.Height = 23;
            this.clientsDgv1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.clientsDgv1.Size = new System.Drawing.Size(1008, 577);
            this.clientsDgv1.TabIndex = 1;
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
            this.Column2.DataPropertyName = "cstcode";
            this.Column2.HeaderText = "客户代码";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 78;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "cstname";
            this.Column3.HeaderText = "客户名称";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 78;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "Region";
            this.Column12.HeaderText = "客户地区";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 78;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "dept";
            this.Column14.HeaderText = "客户部门";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Width = 78;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "paytypename";
            this.Column4.HeaderText = "付款类型";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 78;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "Clienttypename";
            this.Column13.HeaderText = "客户类型";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 78;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "stopflagname";
            this.Column5.HeaderText = "停用标示";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 78;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "createusername";
            this.Column6.HeaderText = "创建人";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 66;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "createusercode";
            this.Column10.HeaderText = "创建人工号";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 90;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "createdate";
            this.Column7.HeaderText = "创建时间";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 78;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "modifyusername";
            this.Column8.HeaderText = "修改人";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 66;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "modifyusercode";
            this.Column11.HeaderText = "修改人工号";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 90;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "modifydate";
            this.Column9.HeaderText = "修改时间";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 78;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1_RunWorkerCompleted);
            // 
            // ClientsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyPreview = true;
            this.Name = "ClientsForm";
            this.Text = "ClientsForm";
            this.Load += new System.EventHandler(this.ClientsForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClientsForm_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.clientsDgv1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton queryBtn;
        private gfoidl.Windows.Forms.gfDataGridView clientsDgv1;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label count;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripButton exportBtn;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        internal System.Windows.Forms.TextBox clientsName;
        internal System.Windows.Forms.TextBox clientsCode;
        internal System.Windows.Forms.ComboBox clientsType;
        internal System.Windows.Forms.ComboBox stopFlag_q;
        internal System.Windows.Forms.ComboBox paytype;
        public System.Windows.Forms.ComboBox areaCbo;
    }
}
namespace PriceManager
{
    partial class DictionaryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DictionaryForm));
            this.panel3 = new System.Windows.Forms.Panel();
            this.dictionaryDgv1 = new gfoidl.Windows.Forms.gfDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.typeName_q = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.typeCode_q = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dictName_q = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.stopFlag_q = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dictCode_q = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.queryBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.newBtn = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dictName_s = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dictCode_s = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.typeName_s = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.typeId_s = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.typeCode_s = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.stopFlag_s = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.mark_s = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dictId_s = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.saveBtn = new System.Windows.Forms.ToolStripButton();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dictionaryDgv1)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dictionaryDgv1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 126);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1008, 387);
            this.panel3.TabIndex = 8;
            // 
            // dictionaryDgv1
            // 
            this.dictionaryDgv1.AllowUserToAddRows = false;
            this.dictionaryDgv1.AllowUserToDeleteRows = false;
            this.dictionaryDgv1.AllowUserToOrderColumns = true;
            this.dictionaryDgv1.AllowUserToResizeRows = false;
            this.dictionaryDgv1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dictionaryDgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dictionaryDgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8});
            this.dictionaryDgv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dictionaryDgv1.Location = new System.Drawing.Point(0, 0);
            this.dictionaryDgv1.MultiSelect = false;
            this.dictionaryDgv1.Name = "dictionaryDgv1";
            this.dictionaryDgv1.ReadOnly = true;
            this.dictionaryDgv1.RowHeadersVisible = false;
            this.dictionaryDgv1.RowTemplate.Height = 23;
            this.dictionaryDgv1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dictionaryDgv1.Size = new System.Drawing.Size(1008, 387);
            this.dictionaryDgv1.TabIndex = 2;
            this.dictionaryDgv1.SelectionChanged += new System.EventHandler(this.DictionaryDgv1_SelectionChanged);
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
            this.Column2.DataPropertyName = "typeid";
            this.Column2.HeaderText = "类型id";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 66;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "typecode";
            this.Column3.HeaderText = "类型编码";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 78;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "typename";
            this.Column9.HeaderText = "类型名称";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 78;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "code";
            this.Column10.HeaderText = "字典编码";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 78;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "name";
            this.Column11.HeaderText = "字典名称";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 78;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "mark";
            this.Column12.HeaderText = "字典备注";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 78;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column4.DataPropertyName = "createuser";
            this.Column4.HeaderText = "创建人";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 66;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column5.DataPropertyName = "createdate";
            this.Column5.HeaderText = "创建时间";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 78;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column6.DataPropertyName = "modifyuser";
            this.Column6.HeaderText = "修改人";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 66;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column7.DataPropertyName = "modifydate";
            this.Column7.HeaderText = "修改时间";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 78;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column8.DataPropertyName = "Stopflagname";
            this.Column8.HeaderText = "停用标示";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 78;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.typeName_q);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.typeCode_q);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.dictName_q);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.stopFlag_q);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.dictCode_q);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 126);
            this.panel1.TabIndex = 6;
            // 
            // typeName_q
            // 
            this.typeName_q.Location = new System.Drawing.Point(302, 23);
            this.typeName_q.Name = "typeName_q";
            this.typeName_q.Size = new System.Drawing.Size(130, 21);
            this.typeName_q.TabIndex = 12;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label13.Location = new System.Drawing.Point(237, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 12);
            this.label13.TabIndex = 11;
            this.label13.Text = "类型名称:";
            // 
            // typeCode_q
            // 
            this.typeCode_q.Location = new System.Drawing.Point(90, 23);
            this.typeCode_q.Name = "typeCode_q";
            this.typeCode_q.Size = new System.Drawing.Size(130, 21);
            this.typeCode_q.TabIndex = 10;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label12.Location = new System.Drawing.Point(25, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 9;
            this.label12.Text = "类型编码:";
            // 
            // dictName_q
            // 
            this.dictName_q.Location = new System.Drawing.Point(731, 23);
            this.dictName_q.Name = "dictName_q";
            this.dictName_q.Size = new System.Drawing.Size(130, 21);
            this.dictName_q.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label11.Location = new System.Drawing.Point(666, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 7;
            this.label11.Text = "字典名称:";
            // 
            // stopFlag_q
            // 
            this.stopFlag_q.FormattingEnabled = true;
            this.stopFlag_q.Location = new System.Drawing.Point(90, 61);
            this.stopFlag_q.Name = "stopFlag_q";
            this.stopFlag_q.Size = new System.Drawing.Size(130, 20);
            this.stopFlag_q.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "停用标示:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dictCode_q
            // 
            this.dictCode_q.Location = new System.Drawing.Point(518, 23);
            this.dictCode_q.Name = "dictCode_q";
            this.dictCode_q.Size = new System.Drawing.Size(130, 21);
            this.dictCode_q.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(453, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "字典编码:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.queryBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 94);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1008, 32);
            this.toolStrip1.TabIndex = 99;
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // newBtn
            // 
            this.newBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.newBtn.Image = ((System.Drawing.Image)(resources.GetObject("newBtn.Image")));
            this.newBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newBtn.Name = "newBtn";
            this.newBtn.Size = new System.Drawing.Size(36, 22);
            this.newBtn.Text = "新建";
            this.newBtn.Click += new System.EventHandler(this.NewBtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dictName_s);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.dictCode_s);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.typeName_s);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.typeId_s);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.typeCode_s);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.stopFlag_s);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.mark_s);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.dictId_s);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.toolStrip2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 513);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1008, 217);
            this.panel2.TabIndex = 7;
            // 
            // dictName_s
            // 
            this.dictName_s.Location = new System.Drawing.Point(302, 81);
            this.dictName_s.Name = "dictName_s";
            this.dictName_s.Size = new System.Drawing.Size(130, 21);
            this.dictName_s.TabIndex = 62;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(237, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 61;
            this.label7.Text = "字典名称:";
            // 
            // dictCode_s
            // 
            this.dictCode_s.Location = new System.Drawing.Point(302, 37);
            this.dictCode_s.Name = "dictCode_s";
            this.dictCode_s.Size = new System.Drawing.Size(130, 21);
            this.dictCode_s.TabIndex = 60;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(237, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 59;
            this.label9.Text = "字典编码:";
            // 
            // typeName_s
            // 
            this.typeName_s.Location = new System.Drawing.Point(90, 81);
            this.typeName_s.Name = "typeName_s";
            this.typeName_s.Size = new System.Drawing.Size(130, 21);
            this.typeName_s.TabIndex = 58;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 57;
            this.label5.Text = "类型名称:";
            // 
            // typeId_s
            // 
            this.typeId_s.Enabled = false;
            this.typeId_s.Location = new System.Drawing.Point(90, 123);
            this.typeId_s.Name = "typeId_s";
            this.typeId_s.Size = new System.Drawing.Size(130, 21);
            this.typeId_s.TabIndex = 56;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 55;
            this.label2.Text = "类型ID:";
            // 
            // typeCode_s
            // 
            this.typeCode_s.Location = new System.Drawing.Point(90, 37);
            this.typeCode_s.Name = "typeCode_s";
            this.typeCode_s.Size = new System.Drawing.Size(130, 21);
            this.typeCode_s.TabIndex = 54;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 53;
            this.label10.Text = "类型编码:";
            // 
            // stopFlag_s
            // 
            this.stopFlag_s.FormattingEnabled = true;
            this.stopFlag_s.Location = new System.Drawing.Point(518, 37);
            this.stopFlag_s.Name = "stopFlag_s";
            this.stopFlag_s.Size = new System.Drawing.Size(130, 20);
            this.stopFlag_s.TabIndex = 51;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(453, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 52;
            this.label8.Text = "停用标示:";
            // 
            // mark_s
            // 
            this.mark_s.Location = new System.Drawing.Point(518, 81);
            this.mark_s.Multiline = true;
            this.mark_s.Name = "mark_s";
            this.mark_s.Size = new System.Drawing.Size(171, 63);
            this.mark_s.TabIndex = 50;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(477, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 49;
            this.label6.Text = "备注:";
            // 
            // dictId_s
            // 
            this.dictId_s.Enabled = false;
            this.dictId_s.Location = new System.Drawing.Point(302, 123);
            this.dictId_s.Name = "dictId_s";
            this.dictId_s.Size = new System.Drawing.Size(130, 21);
            this.dictId_s.TabIndex = 48;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(249, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 47;
            this.label4.Text = "字典ID:";
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator3,
            this.newBtn,
            this.saveBtn});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip2.Size = new System.Drawing.Size(1008, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // saveBtn
            // 
            this.saveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveBtn.Image = ((System.Drawing.Image)(resources.GetObject("saveBtn.Image")));
            this.saveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(36, 22);
            this.saveBtn.Text = "保存";
            this.saveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // DictionaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "DictionaryForm";
            this.Text = "DictionaryForm";
            this.Load += new System.EventHandler(this.DictionaryForm_Load);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dictionaryDgv1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox stopFlag_q;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox dictCode_q;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton queryBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton newBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox typeCode_s;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox stopFlag_s;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox mark_s;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox dictId_s;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton saveBtn;
        private System.Windows.Forms.TextBox typeId_s;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox dictName_s;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox dictCode_s;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox typeName_s;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox typeName_q;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox typeCode_q;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox dictName_q;
        private System.Windows.Forms.Label label11;
        private gfoidl.Windows.Forms.gfDataGridView dictionaryDgv1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    }
}
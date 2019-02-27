namespace PriceManager
{
    partial class GenGoodRecordForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvGenGoodRecord = new gfoidl.Windows.Forms.gfDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column34 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column35 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.endDate = new System.Windows.Forms.DateTimePicker();
            this.beginDate = new System.Windows.Forms.DateTimePicker();
            this.selAllBtn = new System.Windows.Forms.Button();
            this.deleteBtn = new System.Windows.Forms.Button();
            this.importFileBtn = new System.Windows.Forms.Button();
            this.addBtn = new System.Windows.Forms.Button();
            this.queryBtn = new System.Windows.Forms.Button();
            this.txtRecordmark = new System.Windows.Forms.TextBox();
            this.txtGenspec = new System.Windows.Forms.TextBox();
            this.txtGengoods = new System.Windows.Forms.TextBox();
            this.txtDname = new System.Windows.Forms.TextBox();
            this.txtCstcode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGenGoodRecord)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvGenGoodRecord);
            this.groupBox2.Location = new System.Drawing.Point(16, 145);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(1203, 438);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "备案品种信息";
            // 
            // dgvGenGoodRecord
            // 
            this.dgvGenGoodRecord.AllowUserToAddRows = false;
            this.dgvGenGoodRecord.AllowUserToDeleteRows = false;
            this.dgvGenGoodRecord.AllowUserToResizeRows = false;
            this.dgvGenGoodRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvGenGoodRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvGenGoodRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column6,
            this.Column8,
            this.Column9,
            this.Column13,
            this.Column34,
            this.Column35,
            this.Column36,
            this.Column4,
            this.Column7,
            this.Column11,
            this.Column38,
            this.Column3,
            this.Column16,
            this.Column5,
            this.Column18,
            this.Column15,
            this.Column14,
            this.Column17,
            this.Column12});
            this.dgvGenGoodRecord.Location = new System.Drawing.Point(5, 19);
            this.dgvGenGoodRecord.Name = "dgvGenGoodRecord";
            this.dgvGenGoodRecord.ReadOnly = true;
            this.dgvGenGoodRecord.RowHeadersVisible = false;
            this.dgvGenGoodRecord.RowTemplate.Height = 23;
            this.dgvGenGoodRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGenGoodRecord.Size = new System.Drawing.Size(1185, 406);
            this.dgvGenGoodRecord.TabIndex = 46;
            this.dgvGenGoodRecord.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvGenGoodRecord_MouseDoubleClick);
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
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column6.DataPropertyName = "Goodrecordid";
            this.Column6.HeaderText = "备案品种ID";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Visible = false;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column8.DataPropertyName = "Compid";
            this.Column8.HeaderText = "账套ID";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Visible = false;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column9.DataPropertyName = "Ownerid";
            this.Column9.HeaderText = "货主ID";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Visible = false;
            // 
            // Column13
            // 
            this.Column13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column13.DataPropertyName = "Cstid";
            this.Column13.HeaderText = "客户ID";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Visible = false;
            // 
            // Column34
            // 
            this.Column34.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column34.DataPropertyName = "Cstcode";
            this.Column34.HeaderText = "客户代码";
            this.Column34.Name = "Column34";
            this.Column34.ReadOnly = true;
            this.Column34.Width = 78;
            // 
            // Column35
            // 
            this.Column35.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column35.DataPropertyName = "Dname";
            this.Column35.HeaderText = "客户名称";
            this.Column35.Name = "Column35";
            this.Column35.ReadOnly = true;
            this.Column35.Width = 78;
            // 
            // Column36
            // 
            this.Column36.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column36.DataPropertyName = "Gengoods";
            this.Column36.HeaderText = "商品外码";
            this.Column36.Name = "Column36";
            this.Column36.ReadOnly = true;
            this.Column36.Width = 78;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column4.DataPropertyName = "Genspec";
            this.Column4.HeaderText = "外部品名品规";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 102;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column7.DataPropertyName = "Recordmark";
            this.Column7.HeaderText = "备案原因";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 78;
            // 
            // Column11
            // 
            this.Column11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column11.DataPropertyName = "Recordtime";
            this.Column11.HeaderText = "最后备案时间";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 102;
            // 
            // Column38
            // 
            this.Column38.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column38.DataPropertyName = "Modifyusername";
            this.Column38.HeaderText = "修改人";
            this.Column38.Name = "Column38";
            this.Column38.ReadOnly = true;
            this.Column38.Width = 66;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.DataPropertyName = "Genproducer";
            this.Column3.HeaderText = "外部商品厂家";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 102;
            // 
            // Column16
            // 
            this.Column16.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column16.DataPropertyName = "Goodbar";
            this.Column16.HeaderText = "外部条形码";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            this.Column16.Width = 90;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column5.DataPropertyName = "Ratifier";
            this.Column5.HeaderText = "国药准字";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 78;
            // 
            // Column18
            // 
            this.Column18.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column18.DataPropertyName = "Createusername";
            this.Column18.HeaderText = "创建人";
            this.Column18.Name = "Column18";
            this.Column18.ReadOnly = true;
            this.Column18.Width = 66;
            // 
            // Column15
            // 
            this.Column15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column15.DataPropertyName = "Createdate";
            this.Column15.HeaderText = "创建时间";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.Width = 78;
            // 
            // Column14
            // 
            this.Column14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column14.DataPropertyName = "Createuser";
            this.Column14.HeaderText = "创建人ID";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Visible = false;
            // 
            // Column17
            // 
            this.Column17.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column17.DataPropertyName = "Modifyuser";
            this.Column17.HeaderText = "修改人ID";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column17.Visible = false;
            // 
            // Column12
            // 
            this.Column12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column12.DataPropertyName = "SelectFlag";
            this.Column12.HeaderText = "勾选标识";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.endDate);
            this.groupBox1.Controls.Add(this.beginDate);
            this.groupBox1.Controls.Add(this.selAllBtn);
            this.groupBox1.Controls.Add(this.deleteBtn);
            this.groupBox1.Controls.Add(this.importFileBtn);
            this.groupBox1.Controls.Add(this.addBtn);
            this.groupBox1.Controls.Add(this.queryBtn);
            this.groupBox1.Controls.Add(this.txtRecordmark);
            this.groupBox1.Controls.Add(this.txtGenspec);
            this.groupBox1.Controls.Add(this.txtGengoods);
            this.groupBox1.Controls.Add(this.txtDname);
            this.groupBox1.Controls.Add(this.txtCstcode);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(21, 21);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(1198, 120);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "备案信息";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(269, 81);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 19);
            this.label7.TabIndex = 85;
            this.label7.Text = "~";
            // 
            // endDate
            // 
            this.endDate.Location = new System.Drawing.Point(305, 75);
            this.endDate.MaxDate = new System.DateTime(2038, 12, 31, 0, 0, 0, 0);
            this.endDate.Name = "endDate";
            this.endDate.ShowCheckBox = true;
            this.endDate.Size = new System.Drawing.Size(130, 21);
            this.endDate.TabIndex = 84;
            this.endDate.ValueChanged += new System.EventHandler(this.endDate_ValueChanged);
            // 
            // beginDate
            // 
            this.beginDate.Location = new System.Drawing.Point(121, 75);
            this.beginDate.MaxDate = new System.DateTime(2038, 12, 31, 0, 0, 0, 0);
            this.beginDate.Name = "beginDate";
            this.beginDate.ShowCheckBox = true;
            this.beginDate.Size = new System.Drawing.Size(130, 21);
            this.beginDate.TabIndex = 83;
            this.beginDate.Value = new System.DateTime(2018, 7, 5, 0, 0, 0, 0);
            // 
            // selAllBtn
            // 
            this.selAllBtn.Location = new System.Drawing.Point(889, 73);
            this.selAllBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.selAllBtn.Name = "selAllBtn";
            this.selAllBtn.Size = new System.Drawing.Size(67, 27);
            this.selAllBtn.TabIndex = 16;
            this.selAllBtn.Text = "全 选";
            this.selAllBtn.UseVisualStyleBackColor = true;
            this.selAllBtn.Click += new System.EventHandler(this.selAllBtn_Click);
            // 
            // deleteBtn
            // 
            this.deleteBtn.Location = new System.Drawing.Point(974, 73);
            this.deleteBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(67, 27);
            this.deleteBtn.TabIndex = 15;
            this.deleteBtn.Text = "删 除";
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += new System.EventHandler(this.deleteBtn_Click);
            // 
            // importFileBtn
            // 
            this.importFileBtn.Location = new System.Drawing.Point(947, 28);
            this.importFileBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.importFileBtn.Name = "importFileBtn";
            this.importFileBtn.Size = new System.Drawing.Size(94, 27);
            this.importFileBtn.TabIndex = 14;
            this.importFileBtn.Text = "Excel导入";
            this.importFileBtn.UseVisualStyleBackColor = true;
            this.importFileBtn.Click += new System.EventHandler(this.importFileBtn_Click);
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(803, 73);
            this.addBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(65, 27);
            this.addBtn.TabIndex = 12;
            this.addBtn.Text = "新 增";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // queryBtn
            // 
            this.queryBtn.Location = new System.Drawing.Point(710, 73);
            this.queryBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.queryBtn.Name = "queryBtn";
            this.queryBtn.Size = new System.Drawing.Size(67, 27);
            this.queryBtn.TabIndex = 11;
            this.queryBtn.Text = "查 询";
            this.queryBtn.UseVisualStyleBackColor = true;
            this.queryBtn.Click += new System.EventHandler(this.queryBtn_Click);
            // 
            // txtRecordmark
            // 
            this.txtRecordmark.Location = new System.Drawing.Point(525, 77);
            this.txtRecordmark.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtRecordmark.Name = "txtRecordmark";
            this.txtRecordmark.Size = new System.Drawing.Size(165, 21);
            this.txtRecordmark.TabIndex = 10;
            // 
            // txtGenspec
            // 
            this.txtGenspec.Location = new System.Drawing.Point(774, 28);
            this.txtGenspec.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtGenspec.Name = "txtGenspec";
            this.txtGenspec.Size = new System.Drawing.Size(132, 21);
            this.txtGenspec.TabIndex = 9;
            // 
            // txtGengoods
            // 
            this.txtGengoods.Location = new System.Drawing.Point(525, 28);
            this.txtGengoods.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtGengoods.Name = "txtGengoods";
            this.txtGengoods.Size = new System.Drawing.Size(132, 21);
            this.txtGengoods.TabIndex = 8;
            // 
            // txtDname
            // 
            this.txtDname.Location = new System.Drawing.Point(305, 28);
            this.txtDname.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDname.Name = "txtDname";
            this.txtDname.Size = new System.Drawing.Size(132, 21);
            this.txtDname.TabIndex = 7;
            // 
            // txtCstcode
            // 
            this.txtCstcode.Location = new System.Drawing.Point(89, 28);
            this.txtCstcode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCstcode.Name = "txtCstcode";
            this.txtCstcode.Size = new System.Drawing.Size(132, 21);
            this.txtCstcode.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(681, 30);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "外部品名品规：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 75);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "最后备案时间：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(455, 79);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "备案原因：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(455, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "商品外码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "客户名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "客户代码：";
            // 
            // GenGoodRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 587);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "GenGoodRecordForm";
            this.Text = "GenGoodRecordForm";
            this.Load += new System.EventHandler(this.GenGoodRecordForm_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGenGoodRecord)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private gfoidl.Windows.Forms.gfDataGridView dgvGenGoodRecord;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button importFileBtn;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button queryBtn;
        private System.Windows.Forms.TextBox txtRecordmark;
        private System.Windows.Forms.TextBox txtGenspec;
        private System.Windows.Forms.TextBox txtGengoods;
        private System.Windows.Forms.TextBox txtDname;
        private System.Windows.Forms.TextBox txtCstcode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button deleteBtn;
        private System.Windows.Forms.Button selAllBtn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker endDate;
        private System.Windows.Forms.DateTimePicker beginDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column34;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column35;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column36;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column38;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
    }
}
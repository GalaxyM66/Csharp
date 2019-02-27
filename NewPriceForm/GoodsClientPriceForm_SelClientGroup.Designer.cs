namespace PriceManager
{
    partial class GoodsClientPriceForm_SelClientGroup
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
            this.cbDefaultdept = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtClientGroupCode = new System.Windows.Forms.TextBox();
            this.txtClentGroupName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvClientGroup = new gfoidl.Windows.Forms.gfDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column44 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column45 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbDefaultdept);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnSubmit);
            this.groupBox1.Controls.Add(this.txtClientGroupCode);
            this.groupBox1.Controls.Add(this.txtClentGroupName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(11, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(970, 60);
            this.groupBox1.TabIndex = 64;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // cbDefaultdept
            // 
            this.cbDefaultdept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDefaultdept.FormattingEnabled = true;
            this.cbDefaultdept.IntegralHeight = false;
            this.cbDefaultdept.Location = new System.Drawing.Point(69, 21);
            this.cbDefaultdept.Name = "cbDefaultdept";
            this.cbDefaultdept.Size = new System.Drawing.Size(152, 20);
            this.cbDefaultdept.TabIndex = 87;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 86;
            this.label8.Text = "归属部门:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(774, 16);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 30);
            this.btnAdd.TabIndex = 58;
            this.btnAdd.Text = "确定";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(693, 16);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 30);
            this.btnSubmit.TabIndex = 57;
            this.btnSubmit.Text = "查询数据";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // txtClientGroupCode
            // 
            this.txtClientGroupCode.Location = new System.Drawing.Point(308, 20);
            this.txtClientGroupCode.Name = "txtClientGroupCode";
            this.txtClientGroupCode.Size = new System.Drawing.Size(115, 21);
            this.txtClientGroupCode.TabIndex = 1;
            // 
            // txtClentGroupName
            // 
            this.txtClentGroupName.Location = new System.Drawing.Point(506, 20);
            this.txtClentGroupName.Name = "txtClentGroupName";
            this.txtClentGroupName.Size = new System.Drawing.Size(181, 21);
            this.txtClentGroupName.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(227, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 53;
            this.label5.Text = "*客户组代码：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(428, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 55;
            this.label6.Text = "*客户组名称：";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.dgvClientGroup);
            this.groupBox4.Location = new System.Drawing.Point(11, 75);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(970, 378);
            this.groupBox4.TabIndex = 63;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "客户组数据";
            // 
            // dgvClientGroup
            // 
            this.dgvClientGroup.AllowUserToAddRows = false;
            this.dgvClientGroup.AllowUserToDeleteRows = false;
            this.dgvClientGroup.AllowUserToResizeRows = false;
            this.dgvClientGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvClientGroup.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvClientGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvClientGroup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column12,
            this.Column11,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column17,
            this.Column16,
            this.Column18,
            this.Column19,
            this.Column44,
            this.Column45,
            this.Column20,
            this.Column21,
            this.Column22,
            this.Column23,
            this.Column24,
            this.Column25});
            this.dgvClientGroup.Location = new System.Drawing.Point(6, 20);
            this.dgvClientGroup.MultiSelect = false;
            this.dgvClientGroup.Name = "dgvClientGroup";
            this.dgvClientGroup.ReadOnly = true;
            this.dgvClientGroup.RowHeadersVisible = false;
            this.dgvClientGroup.RowTemplate.Height = 23;
            this.dgvClientGroup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientGroup.Size = new System.Drawing.Size(958, 352);
            this.dgvClientGroup.TabIndex = 23;
            this.dgvClientGroup.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClientGroup_CellClick);
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
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.DataPropertyName = "Ownerid";
            this.Column2.HeaderText = "货主代码";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 78;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Deptname";
            this.Column3.HeaderText = "货主名称";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 78;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column4.DataPropertyName = "Code";
            this.Column4.HeaderText = "定价客户组代码";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 114;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column5.DataPropertyName = "Name";
            this.Column5.HeaderText = "定价客户组名称";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 114;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "Detailtypename";
            this.Column6.HeaderText = "定价客户类型";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 102;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "Grouptypename";
            this.Column7.HeaderText = "定价客户组分类";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 114;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "Synctypename";
            this.Column8.HeaderText = "定价客户组同步";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 114;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "Attachcode";
            this.Column9.HeaderText = "默认归属代码";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Visible = false;
            this.Column9.Width = 102;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "Deliveryfeerate";
            this.Column10.HeaderText = "默认加点率";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 90;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "Mark";
            this.Column12.HeaderText = "备注";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 54;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "STOPNAME";
            this.Column11.HeaderText = "停用标识";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 78;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "Detailtype";
            this.Column13.HeaderText = "定价客户类型代码";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Visible = false;
            this.Column13.Width = 126;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "Grouptype";
            this.Column14.HeaderText = "定价客户组分类代码";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Visible = false;
            this.Column14.Width = 138;
            // 
            // Column15
            // 
            this.Column15.DataPropertyName = "Synctype";
            this.Column15.HeaderText = "定价客户组同步分类代码";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.Visible = false;
            this.Column15.Width = 162;
            // 
            // Column17
            // 
            this.Column17.DataPropertyName = "Createusername";
            this.Column17.HeaderText = "创建人员";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column17.Width = 78;
            // 
            // Column16
            // 
            this.Column16.DataPropertyName = "Stopflag";
            this.Column16.HeaderText = "停用标识代码";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            this.Column16.Visible = false;
            this.Column16.Width = 102;
            // 
            // Column18
            // 
            this.Column18.DataPropertyName = "Createdate";
            this.Column18.HeaderText = "创建时间";
            this.Column18.Name = "Column18";
            this.Column18.ReadOnly = true;
            this.Column18.Width = 78;
            // 
            // Column19
            // 
            this.Column19.DataPropertyName = "Modifyusername";
            this.Column19.HeaderText = "最后修改人员";
            this.Column19.Name = "Column19";
            this.Column19.ReadOnly = true;
            this.Column19.Width = 102;
            // 
            // Column44
            // 
            this.Column44.DataPropertyName = "Createuser";
            this.Column44.HeaderText = "创建人员代码";
            this.Column44.Name = "Column44";
            this.Column44.ReadOnly = true;
            this.Column44.Visible = false;
            this.Column44.Width = 102;
            // 
            // Column45
            // 
            this.Column45.DataPropertyName = "Modifyuser";
            this.Column45.HeaderText = "修改人员代码";
            this.Column45.Name = "Column45";
            this.Column45.ReadOnly = true;
            this.Column45.Visible = false;
            this.Column45.Width = 102;
            // 
            // Column20
            // 
            this.Column20.DataPropertyName = "Modifydate";
            this.Column20.HeaderText = "最后修改时间";
            this.Column20.Name = "Column20";
            this.Column20.ReadOnly = true;
            this.Column20.Width = 102;
            // 
            // Column21
            // 
            this.Column21.DataPropertyName = "Compid";
            this.Column21.HeaderText = "账套ID";
            this.Column21.Name = "Column21";
            this.Column21.ReadOnly = true;
            this.Column21.Visible = false;
            this.Column21.Width = 66;
            // 
            // Column22
            // 
            this.Column22.DataPropertyName = "Hdrid";
            this.Column22.HeaderText = "客户组ID";
            this.Column22.Name = "Column22";
            this.Column22.ReadOnly = true;
            this.Column22.Visible = false;
            this.Column22.Width = 78;
            // 
            // Column23
            // 
            this.Column23.DataPropertyName = "Saledeptid";
            this.Column23.HeaderText = "部门ID";
            this.Column23.Name = "Column23";
            this.Column23.ReadOnly = true;
            this.Column23.Visible = false;
            this.Column23.Width = 66;
            // 
            // Column24
            // 
            this.Column24.DataPropertyName = "Saledeptname";
            this.Column24.HeaderText = "部门名称";
            this.Column24.Name = "Column24";
            this.Column24.ReadOnly = true;
            this.Column24.Width = 78;
            // 
            // Column25
            // 
            this.Column25.DataPropertyName = "SelectFlag";
            this.Column25.HeaderText = "勾选标识";
            this.Column25.Name = "Column25";
            this.Column25.ReadOnly = true;
            this.Column25.Visible = false;
            this.Column25.Width = 78;
            // 
            // GoodsClientPriceForm_SelClientGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(996, 462);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Name = "GoodsClientPriceForm_SelClientGroup";
            this.Text = "客户组查询";
            this.Load += new System.EventHandler(this.GoodsClientPriceForm_SelClientGroup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.TextBox txtClientGroupCode;
        private System.Windows.Forms.TextBox txtClentGroupName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox4;
        private gfoidl.Windows.Forms.gfDataGridView dgvClientGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column44;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column45;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column20;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column21;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column22;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column23;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column24;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column25;
        private System.Windows.Forms.ComboBox cbDefaultdept;
        private System.Windows.Forms.Label label8;
    }
}
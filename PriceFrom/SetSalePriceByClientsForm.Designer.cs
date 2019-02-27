namespace PriceManager
{
    partial class SetSalePriceByClientsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetSalePriceByClientsForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.beginDate = new System.Windows.Forms.DateTimePicker();
            this.endDate = new System.Windows.Forms.DateTimePicker();
            this.costRateUpDown = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.offTypeCbo = new System.Windows.Forms.ComboBox();
            this.offLevelCbo = new System.Windows.Forms.ComboBox();
            this.costPriceUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.prcUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.costPrcUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.priceUpDown = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.clientsGroup = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.clientsName = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.clientsCode = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.queryBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitBtn = new System.Windows.Forms.ToolStripButton();
            this.saveBtn = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.selectClientsDgv1 = new gfoidl.Windows.Forms.gfDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.costRateUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.costPriceUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prcUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.costPrcUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.priceUpDown)).BeginInit();
            this.toolStrip4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.selectClientsDgv1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.clientsGroup);
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.clientsName);
            this.panel1.Controls.Add(this.label30);
            this.panel1.Controls.Add(this.clientsCode);
            this.panel1.Controls.Add(this.label31);
            this.panel1.Controls.Add(this.toolStrip4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(838, 237);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.beginDate);
            this.groupBox1.Controls.Add(this.endDate);
            this.groupBox1.Controls.Add(this.costRateUpDown);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.offTypeCbo);
            this.groupBox1.Controls.Add(this.offLevelCbo);
            this.groupBox1.Controls.Add(this.costPriceUpDown);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.prcUpDown);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.costPrcUpDown);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.priceUpDown);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Location = new System.Drawing.Point(12, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(813, 143);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设定价格";
            // 
            // beginDate
            // 
            this.beginDate.Location = new System.Drawing.Point(652, 30);
            this.beginDate.MaxDate = new System.DateTime(2037, 12, 31, 0, 0, 0, 0);
            this.beginDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.beginDate.Name = "beginDate";
            this.beginDate.Size = new System.Drawing.Size(130, 21);
            this.beginDate.TabIndex = 38;
            // 
            // endDate
            // 
            this.endDate.Location = new System.Drawing.Point(652, 68);
            this.endDate.MaxDate = new System.DateTime(2037, 12, 31, 0, 0, 0, 0);
            this.endDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(130, 21);
            this.endDate.TabIndex = 37;
            // 
            // costRateUpDown
            // 
            this.costRateUpDown.DecimalPlaces = 6;
            this.costRateUpDown.Enabled = false;
            this.costRateUpDown.Location = new System.Drawing.Point(121, 106);
            this.costRateUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.costRateUpDown.Name = "costRateUpDown";
            this.costRateUpDown.Size = new System.Drawing.Size(130, 21);
            this.costRateUpDown.TabIndex = 35;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Location = new System.Drawing.Point(47, 108);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 36;
            this.label8.Text = "考核毛利率:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Location = new System.Drawing.Point(587, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 34;
            this.label6.Text = "结束时间:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Location = new System.Drawing.Point(587, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 33;
            this.label7.Text = "开始时间:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Location = new System.Drawing.Point(587, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 30;
            this.label5.Text = "促销类型:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Location = new System.Drawing.Point(314, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 29;
            this.label4.Text = "促销级别:";
            // 
            // offTypeCbo
            // 
            this.offTypeCbo.FormattingEnabled = true;
            this.offTypeCbo.Location = new System.Drawing.Point(652, 105);
            this.offTypeCbo.Name = "offTypeCbo";
            this.offTypeCbo.Size = new System.Drawing.Size(130, 20);
            this.offTypeCbo.TabIndex = 28;
            // 
            // offLevelCbo
            // 
            this.offLevelCbo.FormattingEnabled = true;
            this.offLevelCbo.Location = new System.Drawing.Point(379, 105);
            this.offLevelCbo.Name = "offLevelCbo";
            this.offLevelCbo.Size = new System.Drawing.Size(130, 20);
            this.offLevelCbo.TabIndex = 27;
            // 
            // costPriceUpDown
            // 
            this.costPriceUpDown.DecimalPlaces = 6;
            this.costPriceUpDown.Enabled = false;
            this.costPriceUpDown.Location = new System.Drawing.Point(379, 69);
            this.costPriceUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.costPriceUpDown.Name = "costPriceUpDown";
            this.costPriceUpDown.Size = new System.Drawing.Size(130, 21);
            this.costPriceUpDown.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(47, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "含税成本价:";
            // 
            // prcUpDown
            // 
            this.prcUpDown.DecimalPlaces = 6;
            this.prcUpDown.Location = new System.Drawing.Point(121, 30);
            this.prcUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.prcUpDown.Name = "prcUpDown";
            this.prcUpDown.Size = new System.Drawing.Size(130, 21);
            this.prcUpDown.TabIndex = 12;
            this.prcUpDown.ValueChanged += new System.EventHandler(this.PrcUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(302, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "无税成本价:";
            // 
            // costPrcUpDown
            // 
            this.costPrcUpDown.DecimalPlaces = 6;
            this.costPrcUpDown.Location = new System.Drawing.Point(121, 69);
            this.costPrcUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.costPrcUpDown.Name = "costPrcUpDown";
            this.costPrcUpDown.Size = new System.Drawing.Size(130, 21);
            this.costPrcUpDown.TabIndex = 13;
            this.costPrcUpDown.ValueChanged += new System.EventHandler(this.CostPrcUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(290, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "公开无税售价:";
            // 
            // priceUpDown
            // 
            this.priceUpDown.DecimalPlaces = 6;
            this.priceUpDown.Enabled = false;
            this.priceUpDown.Location = new System.Drawing.Point(379, 30);
            this.priceUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.priceUpDown.Name = "priceUpDown";
            this.priceUpDown.Size = new System.Drawing.Size(130, 21);
            this.priceUpDown.TabIndex = 14;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label17.Location = new System.Drawing.Point(32, 34);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(83, 12);
            this.label17.TabIndex = 16;
            this.label17.Text = "公开含税售价:";
            // 
            // clientsGroup
            // 
            this.clientsGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clientsGroup.FormattingEnabled = true;
            this.clientsGroup.Location = new System.Drawing.Point(664, 21);
            this.clientsGroup.Name = "clientsGroup";
            this.clientsGroup.Size = new System.Drawing.Size(130, 20);
            this.clientsGroup.TabIndex = 33;
            this.clientsGroup.Click += new System.EventHandler(this.ClientsGroup_Click);
            this.clientsGroup.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Default_KeyUp);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(611, 24);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(47, 12);
            this.label26.TabIndex = 30;
            this.label26.Text = "客户组:";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // clientsName
            // 
            this.clientsName.Location = new System.Drawing.Point(391, 21);
            this.clientsName.Name = "clientsName";
            this.clientsName.Size = new System.Drawing.Size(130, 21);
            this.clientsName.TabIndex = 29;
            this.clientsName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Default_KeyUp);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(338, 24);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(47, 12);
            this.label30.TabIndex = 28;
            this.label30.Text = "客户名:";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // clientsCode
            // 
            this.clientsCode.Location = new System.Drawing.Point(136, 21);
            this.clientsCode.Name = "clientsCode";
            this.clientsCode.Size = new System.Drawing.Size(130, 21);
            this.clientsCode.TabIndex = 27;
            this.clientsCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Default_KeyUp);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label31.Location = new System.Drawing.Point(71, 24);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(59, 12);
            this.label31.TabIndex = 26;
            this.label31.Text = "客户编码:";
            // 
            // toolStrip4
            // 
            this.toolStrip4.AutoSize = false;
            this.toolStrip4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip4.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator5,
            this.queryBtn,
            this.toolStripSeparator2,
            this.toolStripSeparator1,
            this.exitBtn,
            this.saveBtn});
            this.toolStrip4.Location = new System.Drawing.Point(0, 205);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip4.Size = new System.Drawing.Size(838, 32);
            this.toolStrip4.TabIndex = 3;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 32);
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
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // exitBtn
            // 
            this.exitBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.exitBtn.AutoSize = false;
            this.exitBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exitBtn.Image = ((System.Drawing.Image)(resources.GetObject("exitBtn.Image")));
            this.exitBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(60, 30);
            this.exitBtn.Text = "退出";
            this.exitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.AutoSize = false;
            this.saveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveBtn.Image = ((System.Drawing.Image)(resources.GetObject("saveBtn.Image")));
            this.saveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(60, 30);
            this.saveBtn.Text = "保存";
            this.saveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.selectClientsDgv1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 237);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(838, 284);
            this.panel2.TabIndex = 2;
            // 
            // selectClientsDgv1
            // 
            this.selectClientsDgv1.AllowUserToAddRows = false;
            this.selectClientsDgv1.AllowUserToDeleteRows = false;
            this.selectClientsDgv1.AllowUserToResizeRows = false;
            this.selectClientsDgv1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.selectClientsDgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.selectClientsDgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15});
            this.selectClientsDgv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectClientsDgv1.Location = new System.Drawing.Point(0, 0);
            this.selectClientsDgv1.Name = "selectClientsDgv1";
            this.selectClientsDgv1.ReadOnly = true;
            this.selectClientsDgv1.RowHeadersVisible = false;
            this.selectClientsDgv1.RowTemplate.Height = 23;
            this.selectClientsDgv1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.selectClientsDgv1.Size = new System.Drawing.Size(838, 284);
            this.selectClientsDgv1.TabIndex = 2;
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
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column2.DataPropertyName = "cstcode";
            this.Column2.HeaderText = "客户代码";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 78;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column3.DataPropertyName = "cstname";
            this.Column3.HeaderText = "客户名称";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 78;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column4.DataPropertyName = "paytypename";
            this.Column4.HeaderText = "付款类型";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 78;
            // 
            // Column12
            // 
            this.Column12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column12.DataPropertyName = "region";
            this.Column12.HeaderText = "客户区域";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 78;
            // 
            // Column13
            // 
            this.Column13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column13.DataPropertyName = "dept";
            this.Column13.HeaderText = "归属部门";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 78;
            // 
            // Column14
            // 
            this.Column14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column14.DataPropertyName = "clienttypename";
            this.Column14.HeaderText = "客户类型";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Width = 78;
            // 
            // Column15
            // 
            this.Column15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column15.DataPropertyName = "clienttypegroupname";
            this.Column15.HeaderText = "客户类型组";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.Width = 90;
            // 
            // SetSalePriceByClientsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 521);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "SetSalePriceByClientsForm";
            this.Text = "添加客户促销价";
            this.Load += new System.EventHandler(this.SetSalePriceByClientsForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.costRateUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.costPriceUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prcUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.costPrcUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.priceUpDown)).EndInit();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.selectClientsDgv1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker beginDate;
        private System.Windows.Forms.DateTimePicker endDate;
        private System.Windows.Forms.NumericUpDown costRateUpDown;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox offTypeCbo;
        private System.Windows.Forms.ComboBox offLevelCbo;
        private System.Windows.Forms.NumericUpDown costPriceUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown prcUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown costPrcUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown priceUpDown;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox clientsGroup;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox clientsName;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox clientsCode;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton queryBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton exitBtn;
        private System.Windows.Forms.ToolStripButton saveBtn;
        private System.Windows.Forms.Panel panel2;
        private gfoidl.Windows.Forms.gfDataGridView selectClientsDgv1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
    }
}
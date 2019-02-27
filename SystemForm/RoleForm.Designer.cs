namespace PriceManager
{
    partial class RoleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoleForm));
            this.panel3 = new System.Windows.Forms.Panel();
            this.roleDgv1 = new gfoidl.Windows.Forms.gfDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.stopFlag_q = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.roleName_q = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.queryBtn = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.roleName_s = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.stopFlag_s = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.mark_s = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.roleId_s = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveBtn = new System.Windows.Forms.ToolStripButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.roleMenuQuery = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripSeparator();
            this.roleMenuSave = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.newBtn = new System.Windows.Forms.ToolStripButton();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.roleDgv1)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.roleDgv1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 101);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1008, 360);
            this.panel3.TabIndex = 5;
            // 
            // roleDgv1
            // 
            this.roleDgv1.AllowUserToAddRows = false;
            this.roleDgv1.AllowUserToDeleteRows = false;
            this.roleDgv1.AllowUserToResizeRows = false;
            this.roleDgv1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.roleDgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.roleDgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8});
            this.roleDgv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roleDgv1.Location = new System.Drawing.Point(0, 0);
            this.roleDgv1.MultiSelect = false;
            this.roleDgv1.Name = "roleDgv1";
            this.roleDgv1.ReadOnly = true;
            this.roleDgv1.RowHeadersVisible = false;
            this.roleDgv1.RowTemplate.Height = 23;
            this.roleDgv1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.roleDgv1.Size = new System.Drawing.Size(1008, 360);
            this.roleDgv1.TabIndex = 0;
            this.roleDgv1.SelectionChanged += new System.EventHandler(this.RoleDgv1_SelectionChanged);
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
            this.Column2.DataPropertyName = "Rolename";
            this.Column2.HeaderText = "角色名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 78;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column3.DataPropertyName = "Mark";
            this.Column3.HeaderText = "备注";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 54;
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
            this.Column8.DataPropertyName = "Stopflag";
            this.Column8.HeaderText = "停用标示";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 78;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.stopFlag_q);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.roleName_q);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 101);
            this.panel1.TabIndex = 3;
            // 
            // stopFlag_q
            // 
            this.stopFlag_q.FormattingEnabled = true;
            this.stopFlag_q.Location = new System.Drawing.Point(286, 24);
            this.stopFlag_q.Name = "stopFlag_q";
            this.stopFlag_q.Size = new System.Drawing.Size(130, 20);
            this.stopFlag_q.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "停用标示:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // roleName_q
            // 
            this.roleName_q.Location = new System.Drawing.Point(78, 24);
            this.roleName_q.Name = "roleName_q";
            this.roleName_q.Size = new System.Drawing.Size(130, 21);
            this.roleName_q.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(25, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "角色名:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.queryBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 69);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1008, 32);
            this.toolStrip1.TabIndex = 0;
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
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.toolStrip2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 461);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1008, 269);
            this.panel2.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tabControl1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 25);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1008, 244);
            this.panel4.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1008, 244);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.roleName_s);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.stopFlag_s);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.mark_s);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.roleId_s);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.toolStrip3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1000, 218);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "角色信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(214, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 43;
            this.label2.Text = "*";
            // 
            // roleName_s
            // 
            this.roleName_s.Location = new System.Drawing.Point(78, 74);
            this.roleName_s.Name = "roleName_s";
            this.roleName_s.Size = new System.Drawing.Size(130, 21);
            this.roleName_s.TabIndex = 37;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 36;
            this.label10.Text = "角色名称:";
            // 
            // stopFlag_s
            // 
            this.stopFlag_s.FormattingEnabled = true;
            this.stopFlag_s.Location = new System.Drawing.Point(286, 74);
            this.stopFlag_s.Name = "stopFlag_s";
            this.stopFlag_s.Size = new System.Drawing.Size(130, 20);
            this.stopFlag_s.TabIndex = 34;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(221, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 35;
            this.label8.Text = "停用标示:";
            // 
            // mark_s
            // 
            this.mark_s.Location = new System.Drawing.Point(286, 111);
            this.mark_s.Multiline = true;
            this.mark_s.Name = "mark_s";
            this.mark_s.Size = new System.Drawing.Size(263, 70);
            this.mark_s.TabIndex = 33;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(245, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 32;
            this.label6.Text = "备注:";
            // 
            // roleId_s
            // 
            this.roleId_s.Enabled = false;
            this.roleId_s.Location = new System.Drawing.Point(78, 111);
            this.roleId_s.Name = "roleId_s";
            this.roleId_s.Size = new System.Drawing.Size(130, 21);
            this.roleId_s.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 30;
            this.label4.Text = "角色ID:";
            // 
            // toolStrip3
            // 
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.saveBtn});
            this.toolStrip3.Location = new System.Drawing.Point(3, 3);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip3.Size = new System.Drawing.Size(994, 25);
            this.toolStrip3.TabIndex = 29;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel5);
            this.tabPage2.Controls.Add(this.toolStrip4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1000, 218);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "角色权限";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.treeView1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(3, 28);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(272, 187);
            this.panel5.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(272, 187);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterCheck);
            // 
            // toolStrip4
            // 
            this.toolStrip4.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.roleMenuQuery,
            this.toolStripButton2,
            this.roleMenuSave});
            this.toolStrip4.Location = new System.Drawing.Point(3, 3);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(994, 25);
            this.toolStrip4.TabIndex = 1;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // roleMenuQuery
            // 
            this.roleMenuQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.roleMenuQuery.Image = ((System.Drawing.Image)(resources.GetObject("roleMenuQuery.Image")));
            this.roleMenuQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.roleMenuQuery.Name = "roleMenuQuery";
            this.roleMenuQuery.Size = new System.Drawing.Size(60, 22);
            this.roleMenuQuery.Text = "查询权限";
            this.roleMenuQuery.Click += new System.EventHandler(this.RoleMenuQuery_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(6, 25);
            // 
            // roleMenuSave
            // 
            this.roleMenuSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.roleMenuSave.Image = ((System.Drawing.Image)(resources.GetObject("roleMenuSave.Image")));
            this.roleMenuSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.roleMenuSave.Name = "roleMenuSave";
            this.roleMenuSave.Size = new System.Drawing.Size(60, 22);
            this.roleMenuSave.Text = "保存权限";
            this.roleMenuSave.Click += new System.EventHandler(this.RoleMenuSave_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator3,
            this.newBtn});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip2.Size = new System.Drawing.Size(1008, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
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
            // RoleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "RoleForm";
            this.Text = "RoleForm";
            this.Load += new System.EventHandler(this.RoleForm_Load);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.roleDgv1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel3;
        private gfoidl.Windows.Forms.gfDataGridView roleDgv1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox stopFlag_q;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox roleName_q;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton queryBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox roleName_s;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox stopFlag_s;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox mark_s;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox roleId_s;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton saveBtn;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton newBtn;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripButton roleMenuQuery;
        private System.Windows.Forms.ToolStripSeparator toolStripButton2;
        private System.Windows.Forms.ToolStripButton roleMenuSave;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.Label label2;
    }
}
namespace PriceManager
{
    partial class SysMenuForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SysMenuForm));
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("节点2");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("节点3");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("节点0", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("节点4");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("节点5");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("节点1", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11});
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.sysMenuDgv1 = new gfoidl.Windows.Forms.gfDataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.menuName_s = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.stopFlag_s = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.formName_s = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.menuParents_s = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.menuLevel_s = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.menuCode_s = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuId_s = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.newBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveBtn = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sysMenuDgv1)).BeginInit();
            this.panel3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(207, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(801, 730);
            this.panel2.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.sysMenuDgv1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(801, 436);
            this.panel4.TabIndex = 1;
            // 
            // sysMenuDgv1
            // 
            this.sysMenuDgv1.AllowUserToAddRows = false;
            this.sysMenuDgv1.AllowUserToDeleteRows = false;
            this.sysMenuDgv1.AllowUserToResizeRows = false;
            this.sysMenuDgv1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.sysMenuDgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.sysMenuDgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column11,
            this.Column2,
            this.Column3,
            this.Column9,
            this.Column10,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8});
            this.sysMenuDgv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sysMenuDgv1.Location = new System.Drawing.Point(0, 0);
            this.sysMenuDgv1.MultiSelect = false;
            this.sysMenuDgv1.Name = "sysMenuDgv1";
            this.sysMenuDgv1.ReadOnly = true;
            this.sysMenuDgv1.RowHeadersVisible = false;
            this.sysMenuDgv1.RowTemplate.Height = 23;
            this.sysMenuDgv1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.sysMenuDgv1.Size = new System.Drawing.Size(801, 436);
            this.sysMenuDgv1.TabIndex = 1;
            this.sysMenuDgv1.SelectionChanged += new System.EventHandler(this.SysMenuDgv1_SelectionChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.menuName_s);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.stopFlag_s);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.formName_s);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.menuParents_s);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.menuLevel_s);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.menuCode_s);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.menuId_s);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.toolStrip1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 436);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(801, 294);
            this.panel3.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(436, 43);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 41;
            this.label9.Text = "*";
            // 
            // menuName_s
            // 
            this.menuName_s.Location = new System.Drawing.Point(514, 40);
            this.menuName_s.Name = "menuName_s";
            this.menuName_s.Size = new System.Drawing.Size(130, 21);
            this.menuName_s.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(449, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 32;
            this.label5.Text = "菜单名称:";
            // 
            // stopFlag_s
            // 
            this.stopFlag_s.FormattingEnabled = true;
            this.stopFlag_s.Location = new System.Drawing.Point(300, 78);
            this.stopFlag_s.Name = "stopFlag_s";
            this.stopFlag_s.Size = new System.Drawing.Size(130, 20);
            this.stopFlag_s.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(235, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 31;
            this.label4.Text = "停用标示:";
            // 
            // formName_s
            // 
            this.formName_s.Location = new System.Drawing.Point(86, 78);
            this.formName_s.Name = "formName_s";
            this.formName_s.Size = new System.Drawing.Size(130, 21);
            this.formName_s.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 28;
            this.label3.Text = "表单名称:";
            // 
            // menuParents_s
            // 
            this.menuParents_s.FormattingEnabled = true;
            this.menuParents_s.Location = new System.Drawing.Point(300, 118);
            this.menuParents_s.Name = "menuParents_s";
            this.menuParents_s.Size = new System.Drawing.Size(130, 20);
            this.menuParents_s.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(235, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 27;
            this.label2.Text = "父级菜单:";
            // 
            // menuLevel_s
            // 
            this.menuLevel_s.FormattingEnabled = true;
            this.menuLevel_s.Location = new System.Drawing.Point(86, 118);
            this.menuLevel_s.Name = "menuLevel_s";
            this.menuLevel_s.Size = new System.Drawing.Size(130, 20);
            this.menuLevel_s.TabIndex = 24;
            this.menuLevel_s.SelectedValueChanged += new System.EventHandler(this.MenuLevel_s_SelectedValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 25;
            this.label8.Text = "菜单级别:";
            // 
            // menuCode_s
            // 
            this.menuCode_s.Location = new System.Drawing.Point(300, 40);
            this.menuCode_s.Name = "menuCode_s";
            this.menuCode_s.Size = new System.Drawing.Size(130, 21);
            this.menuCode_s.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(235, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "菜单编码:";
            // 
            // menuId_s
            // 
            this.menuId_s.Enabled = false;
            this.menuId_s.Location = new System.Drawing.Point(86, 40);
            this.menuId_s.Name = "menuId_s";
            this.menuId_s.Size = new System.Drawing.Size(130, 21);
            this.menuId_s.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(33, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "菜单ID:";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.newBtn,
            this.toolStripSeparator1,
            this.saveBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(801, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(207, 730);
            this.panel1.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode7.Name = "节点2";
            treeNode7.Text = "节点2";
            treeNode8.Name = "节点3";
            treeNode8.Text = "节点3";
            treeNode9.Name = "节点0";
            treeNode9.Text = "节点0";
            treeNode10.Name = "节点4";
            treeNode10.Text = "节点4";
            treeNode11.Name = "节点5";
            treeNode11.Text = "节点5";
            treeNode12.Name = "节点1";
            treeNode12.Text = "节点1";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode12});
            this.treeView1.Size = new System.Drawing.Size(207, 730);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1_AfterSelect);
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
            // Column11
            // 
            this.Column11.DataPropertyName = "menuCode";
            this.Column11.HeaderText = "菜单代码";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 78;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column2.DataPropertyName = "Menuname";
            this.Column2.HeaderText = "菜单名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 78;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column3.DataPropertyName = "Levelname";
            this.Column3.HeaderText = "菜单等级";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 78;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column9.DataPropertyName = "Parentsname";
            this.Column9.HeaderText = "父级ID";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 66;
            // 
            // Column10
            // 
            this.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column10.DataPropertyName = "Formname";
            this.Column10.HeaderText = "表单名称";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 78;
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
            // SysMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "SysMenuForm";
            this.Text = "SysMenuForm";
            this.Load += new System.EventHandler(this.SysMenuForm_Load);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sysMenuDgv1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripButton1;
        private System.Windows.Forms.ToolStripButton newBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton saveBtn;
        private gfoidl.Windows.Forms.gfDataGridView sysMenuDgv1;
        private System.Windows.Forms.TextBox menuCode_s;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox menuId_s;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox menuParents_s;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox menuLevel_s;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox stopFlag_s;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox formName_s;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox menuName_s;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    }
}
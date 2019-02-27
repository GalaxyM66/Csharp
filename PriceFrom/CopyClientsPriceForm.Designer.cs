namespace PriceManager
{
    partial class CopyClientsPriceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CopyClientsPriceForm));
            this.copyClientsName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.mergeCover = new System.Windows.Forms.CheckBox();
            this.allCover = new System.Windows.Forms.CheckBox();
            this.copyClientsListDgv = new gfoidl.Windows.Forms.gfDataGridView();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn95 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn92 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn93 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn94 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
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
            ((System.ComponentModel.ISupportInitialize)(this.copyClientsListDgv)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.SuspendLayout();
            // 
            // copyClientsName
            // 
            this.copyClientsName.Enabled = false;
            this.copyClientsName.Location = new System.Drawing.Point(84, 12);
            this.copyClientsName.Name = "copyClientsName";
            this.copyClientsName.Size = new System.Drawing.Size(210, 21);
            this.copyClientsName.TabIndex = 13;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(19, 15);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 12);
            this.label15.TabIndex = 12;
            this.label15.Text = "复制客户:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mergeCover
            // 
            this.mergeCover.AutoSize = true;
            this.mergeCover.Location = new System.Drawing.Point(332, 14);
            this.mergeCover.Name = "mergeCover";
            this.mergeCover.Size = new System.Drawing.Size(72, 16);
            this.mergeCover.TabIndex = 16;
            this.mergeCover.Tag = "";
            this.mergeCover.Text = "合并复制";
            this.mergeCover.UseVisualStyleBackColor = true;
            this.mergeCover.Click += new System.EventHandler(this.MergeCover_Click);
            // 
            // allCover
            // 
            this.allCover.AutoSize = true;
            this.allCover.Checked = true;
            this.allCover.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allCover.Location = new System.Drawing.Point(489, 14);
            this.allCover.Name = "allCover";
            this.allCover.Size = new System.Drawing.Size(72, 16);
            this.allCover.TabIndex = 17;
            this.allCover.Tag = "";
            this.allCover.Text = "覆盖复制";
            this.allCover.UseVisualStyleBackColor = true;
            this.allCover.Click += new System.EventHandler(this.AllCover_Click);
            // 
            // copyClientsListDgv
            // 
            this.copyClientsListDgv.AllowUserToAddRows = false;
            this.copyClientsListDgv.AllowUserToDeleteRows = false;
            this.copyClientsListDgv.AllowUserToOrderColumns = true;
            this.copyClientsListDgv.AllowUserToResizeRows = false;
            this.copyClientsListDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.copyClientsListDgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn95,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn92,
            this.dataGridViewTextBoxColumn93,
            this.dataGridViewTextBoxColumn94});
            this.copyClientsListDgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.copyClientsListDgv.Location = new System.Drawing.Point(0, 127);
            this.copyClientsListDgv.Name = "copyClientsListDgv";
            this.copyClientsListDgv.ReadOnly = true;
            this.copyClientsListDgv.RowHeadersVisible = false;
            this.copyClientsListDgv.RowTemplate.Height = 23;
            this.copyClientsListDgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.copyClientsListDgv.Size = new System.Drawing.Size(700, 278);
            this.copyClientsListDgv.TabIndex = 18;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "TagPtr";
            this.dataGridViewTextBoxColumn11.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Visible = false;
            this.dataGridViewTextBoxColumn11.Width = 72;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "cstcode";
            this.dataGridViewTextBoxColumn12.HeaderText = "客户代码";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 78;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "cstname";
            this.dataGridViewTextBoxColumn13.HeaderText = "客户名称";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.Width = 78;
            // 
            // dataGridViewTextBoxColumn95
            // 
            this.dataGridViewTextBoxColumn95.DataPropertyName = "paytypename";
            this.dataGridViewTextBoxColumn95.HeaderText = "付款类型";
            this.dataGridViewTextBoxColumn95.Name = "dataGridViewTextBoxColumn95";
            this.dataGridViewTextBoxColumn95.ReadOnly = true;
            this.dataGridViewTextBoxColumn95.Width = 78;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "region";
            this.dataGridViewTextBoxColumn14.HeaderText = "客户区域";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.Width = 78;
            // 
            // dataGridViewTextBoxColumn92
            // 
            this.dataGridViewTextBoxColumn92.DataPropertyName = "dept";
            this.dataGridViewTextBoxColumn92.HeaderText = "归属部门";
            this.dataGridViewTextBoxColumn92.Name = "dataGridViewTextBoxColumn92";
            this.dataGridViewTextBoxColumn92.ReadOnly = true;
            this.dataGridViewTextBoxColumn92.Width = 78;
            // 
            // dataGridViewTextBoxColumn93
            // 
            this.dataGridViewTextBoxColumn93.DataPropertyName = "clienttypename";
            this.dataGridViewTextBoxColumn93.HeaderText = "客户类型";
            this.dataGridViewTextBoxColumn93.Name = "dataGridViewTextBoxColumn93";
            this.dataGridViewTextBoxColumn93.ReadOnly = true;
            this.dataGridViewTextBoxColumn93.Width = 78;
            // 
            // dataGridViewTextBoxColumn94
            // 
            this.dataGridViewTextBoxColumn94.DataPropertyName = "clienttypegroupname";
            this.dataGridViewTextBoxColumn94.HeaderText = "客户类型组";
            this.dataGridViewTextBoxColumn94.Name = "dataGridViewTextBoxColumn94";
            this.dataGridViewTextBoxColumn94.ReadOnly = true;
            this.dataGridViewTextBoxColumn94.Width = 90;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.clientsGroup);
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.clientsName);
            this.panel1.Controls.Add(this.label30);
            this.panel1.Controls.Add(this.clientsCode);
            this.panel1.Controls.Add(this.label31);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.mergeCover);
            this.panel1.Controls.Add(this.toolStrip4);
            this.panel1.Controls.Add(this.allCover);
            this.panel1.Controls.Add(this.copyClientsName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(700, 127);
            this.panel1.TabIndex = 19;
            // 
            // clientsGroup
            // 
            this.clientsGroup.DropDownHeight = 1;
            this.clientsGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clientsGroup.FormattingEnabled = true;
            this.clientsGroup.IntegralHeight = false;
            this.clientsGroup.Location = new System.Drawing.Point(540, 60);
            this.clientsGroup.Name = "clientsGroup";
            this.clientsGroup.Size = new System.Drawing.Size(130, 20);
            this.clientsGroup.TabIndex = 39;
            this.clientsGroup.Click += new System.EventHandler(this.ClientsGroup_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(487, 63);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(47, 12);
            this.label26.TabIndex = 38;
            this.label26.Text = "客户组:";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // clientsName
            // 
            this.clientsName.Location = new System.Drawing.Point(315, 60);
            this.clientsName.Name = "clientsName";
            this.clientsName.Size = new System.Drawing.Size(130, 21);
            this.clientsName.TabIndex = 37;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(262, 63);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(47, 12);
            this.label30.TabIndex = 36;
            this.label30.Text = "客户名:";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // clientsCode
            // 
            this.clientsCode.Location = new System.Drawing.Point(84, 60);
            this.clientsCode.Name = "clientsCode";
            this.clientsCode.Size = new System.Drawing.Size(130, 21);
            this.clientsCode.TabIndex = 35;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label31.Location = new System.Drawing.Point(19, 63);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(59, 12);
            this.label31.TabIndex = 34;
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
            this.toolStrip4.Location = new System.Drawing.Point(0, 95);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip4.Size = new System.Drawing.Size(700, 32);
            this.toolStrip4.TabIndex = 4;
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
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(700, 405);
            this.panel2.TabIndex = 20;
            // 
            // CopyClientsPriceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 405);
            this.ControlBox = false;
            this.Controls.Add(this.copyClientsListDgv);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.KeyPreview = true;
            this.Name = "CopyClientsPriceForm";
            this.Text = "客户复制";
            this.Load += new System.EventHandler(this.CopyPriceForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CopyClientsPriceForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.copyClientsListDgv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox copyClientsName;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox mergeCover;
        private System.Windows.Forms.CheckBox allCover;
        private gfoidl.Windows.Forms.gfDataGridView copyClientsListDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn95;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn92;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn93;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn94;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton queryBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton exitBtn;
        private System.Windows.Forms.ToolStripButton saveBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox clientsGroup;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox clientsName;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox clientsCode;
        private System.Windows.Forms.Label label31;
    }
}
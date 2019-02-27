namespace PriceManager
{
    partial class CopyGoodsPriceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CopyGoodsPriceForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.goodsGroup = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.goodsCode = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.goodsName = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.mergeCover = new System.Windows.Forms.CheckBox();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.queryBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitBtn = new System.Windows.Forms.ToolStripButton();
            this.saveBtn = new System.Windows.Forms.ToolStripButton();
            this.allCover = new System.Windows.Forms.CheckBox();
            this.copyGoodsName = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.copyGoodsListDgv = new gfoidl.Windows.Forms.gfDataGridView();
            this.dataGridViewTextBoxColumn98 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn99 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn100 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn101 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn102 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn103 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn104 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn105 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn106 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn107 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.copyGoodsListDgv)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.goodsGroup);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.goodsCode);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.goodsName);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.mergeCover);
            this.panel1.Controls.Add(this.toolStrip4);
            this.panel1.Controls.Add(this.allCover);
            this.panel1.Controls.Add(this.copyGoodsName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(707, 127);
            this.panel1.TabIndex = 20;
            // 
            // goodsGroup
            // 
            this.goodsGroup.DropDownHeight = 1;
            this.goodsGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.goodsGroup.FormattingEnabled = true;
            this.goodsGroup.IntegralHeight = false;
            this.goodsGroup.Location = new System.Drawing.Point(507, 62);
            this.goodsGroup.Name = "goodsGroup";
            this.goodsGroup.Size = new System.Drawing.Size(150, 20);
            this.goodsGroup.TabIndex = 25;
            this.goodsGroup.Click += new System.EventHandler(this.GoodsGroup_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label20.Location = new System.Drawing.Point(442, 65);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(59, 12);
            this.label20.TabIndex = 24;
            this.label20.Text = "商品组名:";
            // 
            // goodsCode
            // 
            this.goodsCode.Location = new System.Drawing.Point(84, 62);
            this.goodsCode.Name = "goodsCode";
            this.goodsCode.Size = new System.Drawing.Size(130, 21);
            this.goodsCode.TabIndex = 23;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label14.Location = new System.Drawing.Point(19, 65);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 12);
            this.label14.TabIndex = 22;
            this.label14.Text = "商品编码:";
            // 
            // goodsName
            // 
            this.goodsName.Location = new System.Drawing.Point(284, 62);
            this.goodsName.Name = "goodsName";
            this.goodsName.Size = new System.Drawing.Size(129, 21);
            this.goodsName.TabIndex = 20;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label19.Location = new System.Drawing.Point(231, 65);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(47, 12);
            this.label19.TabIndex = 19;
            this.label19.Text = "商品名:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(19, 15);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 12);
            this.label15.TabIndex = 12;
            this.label15.Text = "复制商品:";
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
            this.toolStrip4.Size = new System.Drawing.Size(707, 32);
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
            // copyGoodsName
            // 
            this.copyGoodsName.Enabled = false;
            this.copyGoodsName.Location = new System.Drawing.Point(84, 12);
            this.copyGoodsName.Name = "copyGoodsName";
            this.copyGoodsName.Size = new System.Drawing.Size(210, 21);
            this.copyGoodsName.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.copyGoodsListDgv);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 127);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(707, 268);
            this.panel2.TabIndex = 21;
            // 
            // copyGoodsListDgv
            // 
            this.copyGoodsListDgv.AllowUserToAddRows = false;
            this.copyGoodsListDgv.AllowUserToDeleteRows = false;
            this.copyGoodsListDgv.AllowUserToOrderColumns = true;
            this.copyGoodsListDgv.AllowUserToResizeRows = false;
            this.copyGoodsListDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.copyGoodsListDgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn98,
            this.dataGridViewTextBoxColumn99,
            this.dataGridViewTextBoxColumn100,
            this.dataGridViewTextBoxColumn101,
            this.dataGridViewTextBoxColumn102,
            this.dataGridViewTextBoxColumn103,
            this.dataGridViewTextBoxColumn104,
            this.dataGridViewTextBoxColumn105,
            this.dataGridViewTextBoxColumn106,
            this.dataGridViewTextBoxColumn107});
            this.copyGoodsListDgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.copyGoodsListDgv.Location = new System.Drawing.Point(0, 0);
            this.copyGoodsListDgv.Name = "copyGoodsListDgv";
            this.copyGoodsListDgv.ReadOnly = true;
            this.copyGoodsListDgv.RowHeadersVisible = false;
            this.copyGoodsListDgv.RowTemplate.Height = 23;
            this.copyGoodsListDgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.copyGoodsListDgv.Size = new System.Drawing.Size(707, 268);
            this.copyGoodsListDgv.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn98
            // 
            this.dataGridViewTextBoxColumn98.DataPropertyName = "TagPtr";
            this.dataGridViewTextBoxColumn98.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn98.Name = "dataGridViewTextBoxColumn98";
            this.dataGridViewTextBoxColumn98.ReadOnly = true;
            this.dataGridViewTextBoxColumn98.Visible = false;
            this.dataGridViewTextBoxColumn98.Width = 72;
            // 
            // dataGridViewTextBoxColumn99
            // 
            this.dataGridViewTextBoxColumn99.DataPropertyName = "goods";
            this.dataGridViewTextBoxColumn99.HeaderText = "商品代码";
            this.dataGridViewTextBoxColumn99.Name = "dataGridViewTextBoxColumn99";
            this.dataGridViewTextBoxColumn99.ReadOnly = true;
            this.dataGridViewTextBoxColumn99.Width = 78;
            // 
            // dataGridViewTextBoxColumn100
            // 
            this.dataGridViewTextBoxColumn100.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn100.HeaderText = "商品名称";
            this.dataGridViewTextBoxColumn100.Name = "dataGridViewTextBoxColumn100";
            this.dataGridViewTextBoxColumn100.ReadOnly = true;
            this.dataGridViewTextBoxColumn100.Width = 78;
            // 
            // dataGridViewTextBoxColumn101
            // 
            this.dataGridViewTextBoxColumn101.DataPropertyName = "spec";
            this.dataGridViewTextBoxColumn101.HeaderText = "商品规格";
            this.dataGridViewTextBoxColumn101.Name = "dataGridViewTextBoxColumn101";
            this.dataGridViewTextBoxColumn101.ReadOnly = true;
            this.dataGridViewTextBoxColumn101.Width = 78;
            // 
            // dataGridViewTextBoxColumn102
            // 
            this.dataGridViewTextBoxColumn102.DataPropertyName = "producer";
            this.dataGridViewTextBoxColumn102.HeaderText = "生产厂家";
            this.dataGridViewTextBoxColumn102.Name = "dataGridViewTextBoxColumn102";
            this.dataGridViewTextBoxColumn102.ReadOnly = true;
            this.dataGridViewTextBoxColumn102.Width = 78;
            // 
            // dataGridViewTextBoxColumn103
            // 
            this.dataGridViewTextBoxColumn103.DataPropertyName = "islimitname";
            this.dataGridViewTextBoxColumn103.HeaderText = "是否限销";
            this.dataGridViewTextBoxColumn103.Name = "dataGridViewTextBoxColumn103";
            this.dataGridViewTextBoxColumn103.ReadOnly = true;
            this.dataGridViewTextBoxColumn103.Width = 78;
            // 
            // dataGridViewTextBoxColumn104
            // 
            this.dataGridViewTextBoxColumn104.DataPropertyName = "bargainname";
            this.dataGridViewTextBoxColumn104.HeaderText = "是否可议价";
            this.dataGridViewTextBoxColumn104.Name = "dataGridViewTextBoxColumn104";
            this.dataGridViewTextBoxColumn104.ReadOnly = true;
            this.dataGridViewTextBoxColumn104.Width = 90;
            // 
            // dataGridViewTextBoxColumn105
            // 
            this.dataGridViewTextBoxColumn105.DataPropertyName = "area";
            this.dataGridViewTextBoxColumn105.HeaderText = "限销区域";
            this.dataGridViewTextBoxColumn105.Name = "dataGridViewTextBoxColumn105";
            this.dataGridViewTextBoxColumn105.ReadOnly = true;
            this.dataGridViewTextBoxColumn105.Width = 78;
            // 
            // dataGridViewTextBoxColumn106
            // 
            this.dataGridViewTextBoxColumn106.DataPropertyName = "limitcsttype";
            this.dataGridViewTextBoxColumn106.HeaderText = "限销业态";
            this.dataGridViewTextBoxColumn106.Name = "dataGridViewTextBoxColumn106";
            this.dataGridViewTextBoxColumn106.ReadOnly = true;
            this.dataGridViewTextBoxColumn106.Width = 78;
            // 
            // dataGridViewTextBoxColumn107
            // 
            this.dataGridViewTextBoxColumn107.DataPropertyName = "forbitarea";
            this.dataGridViewTextBoxColumn107.HeaderText = "禁销区域";
            this.dataGridViewTextBoxColumn107.Name = "dataGridViewTextBoxColumn107";
            this.dataGridViewTextBoxColumn107.ReadOnly = true;
            this.dataGridViewTextBoxColumn107.Width = 78;
            // 
            // CopyGoodsPriceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 395);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "CopyGoodsPriceForm";
            this.Text = "CopyGoodsPriceForm";
            this.Load += new System.EventHandler(this.CopyGoodsPriceForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CopyGoodsPriceForm_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.copyGoodsListDgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox mergeCover;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton queryBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton exitBtn;
        private System.Windows.Forms.ToolStripButton saveBtn;
        private System.Windows.Forms.CheckBox allCover;
        private System.Windows.Forms.TextBox copyGoodsName;
        private System.Windows.Forms.Panel panel2;
        private gfoidl.Windows.Forms.gfDataGridView copyGoodsListDgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn98;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn99;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn100;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn101;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn102;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn103;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn104;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn105;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn106;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn107;
        private System.Windows.Forms.ComboBox goodsGroup;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox goodsCode;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox goodsName;
        private System.Windows.Forms.Label label19;
    }
}
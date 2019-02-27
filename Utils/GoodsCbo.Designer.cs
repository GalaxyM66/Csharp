namespace PriceManager
{
    partial class GoodsCbo
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.goodsCboDgv1 = new gfoidl.Windows.Forms.gfDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.closeBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.queryText = new PriceManager.WatermarkTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.goodsCboDgv1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(353, 255);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.goodsCboDgv1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(349, 217);
            this.panel2.TabIndex = 3;
            // 
            // goodsCboDgv1
            // 
            this.goodsCboDgv1.AllowUserToAddRows = false;
            this.goodsCboDgv1.AllowUserToDeleteRows = false;
            this.goodsCboDgv1.AllowUserToResizeRows = false;
            this.goodsCboDgv1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.goodsCboDgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.goodsCboDgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column8});
            this.goodsCboDgv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.goodsCboDgv1.Location = new System.Drawing.Point(0, 0);
            this.goodsCboDgv1.MultiSelect = false;
            this.goodsCboDgv1.Name = "goodsCboDgv1";
            this.goodsCboDgv1.ReadOnly = true;
            this.goodsCboDgv1.RowHeadersVisible = false;
            this.goodsCboDgv1.RowTemplate.Height = 23;
            this.goodsCboDgv1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.goodsCboDgv1.Size = new System.Drawing.Size(349, 217);
            this.goodsCboDgv1.TabIndex = 2;
            this.goodsCboDgv1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellDoubleClick);
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
            this.Column2.DataPropertyName = "goods";
            this.Column2.HeaderText = "商品代码";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 78;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
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
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column5.DataPropertyName = "producer";
            this.Column5.HeaderText = "生产厂家";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 78;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column8.DataPropertyName = "outrate";
            this.Column8.HeaderText = "税率";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 54;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.closeBtn);
            this.panel3.Controls.Add(this.clearBtn);
            this.panel3.Controls.Add(this.queryText);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(349, 34);
            this.panel3.TabIndex = 2;
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(282, 3);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(57, 23);
            this.closeBtn.TabIndex = 9;
            this.closeBtn.Text = "关闭";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(219, 3);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(57, 23);
            this.clearBtn.TabIndex = 8;
            this.clearBtn.Text = "清空";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // queryText
            // 
            this.queryText.Location = new System.Drawing.Point(9, 5);
            this.queryText.Name = "queryText";
            this.queryText.Size = new System.Drawing.Size(204, 21);
            this.queryText.TabIndex = 6;
            this.queryText.WatermarkText = "可输入编码、名称、规格、厂家查询";
            this.queryText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.QueryText_KeyUp);
            // 
            // GoodsCbo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(353, 255);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GoodsCbo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "goods";
            this.Load += new System.EventHandler(this.ClientsGroup_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.goodsCboDgv1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button clearBtn;
        private WatermarkTextBox queryText;
        private gfoidl.Windows.Forms.gfDataGridView goodsCboDgv1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.Button closeBtn;
    }
}
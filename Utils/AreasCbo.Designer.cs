namespace PriceManager
{
    partial class AreasCbo
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
            this.areaDgv1 = new gfoidl.Windows.Forms.gfDataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.closeBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.queryText = new PriceManager.WatermarkTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.areaDgv1)).BeginInit();
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
            this.panel1.Size = new System.Drawing.Size(352, 243);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.areaDgv1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(348, 205);
            this.panel2.TabIndex = 3;
            // 
            // areaDgv1
            // 
            this.areaDgv1.AllowUserToAddRows = false;
            this.areaDgv1.AllowUserToDeleteRows = false;
            this.areaDgv1.AllowUserToResizeRows = false;
            this.areaDgv1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.areaDgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.areaDgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.Column22,
            this.Column23,
            this.Column24});
            this.areaDgv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.areaDgv1.Location = new System.Drawing.Point(0, 0);
            this.areaDgv1.Name = "areaDgv1";
            this.areaDgv1.ReadOnly = true;
            this.areaDgv1.RowHeadersVisible = false;
            this.areaDgv1.RowTemplate.Height = 23;
            this.areaDgv1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.areaDgv1.Size = new System.Drawing.Size(348, 205);
            this.areaDgv1.TabIndex = 6;
            this.areaDgv1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AreaDgv1_CellDoubleClick);
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "TagPtr";
            this.dataGridViewTextBoxColumn4.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            this.dataGridViewTextBoxColumn4.Width = 72;
            // 
            // Column22
            // 
            this.Column22.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column22.DataPropertyName = "name";
            this.Column22.HeaderText = "区域名称";
            this.Column22.Name = "Column22";
            this.Column22.ReadOnly = true;
            this.Column22.Width = 78;
            // 
            // Column23
            // 
            this.Column23.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column23.DataPropertyName = "typename";
            this.Column23.HeaderText = "区域类型";
            this.Column23.Name = "Column23";
            this.Column23.ReadOnly = true;
            this.Column23.Width = 78;
            // 
            // Column24
            // 
            this.Column24.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet;
            this.Column24.DataPropertyName = "mark";
            this.Column24.HeaderText = "备注";
            this.Column24.Name = "Column24";
            this.Column24.ReadOnly = true;
            this.Column24.Width = 54;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.closeBtn);
            this.panel3.Controls.Add(this.clearBtn);
            this.panel3.Controls.Add(this.queryText);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(348, 34);
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
            this.queryText.WatermarkText = "可输入编码、名称查询";
            this.queryText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.QueryText_KeyUp);
            // 
            // AreasCbo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 243);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AreasCbo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Areas";
            this.Load += new System.EventHandler(this.AreasCbo_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.areaDgv1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Button clearBtn;
        private WatermarkTextBox queryText;
        private gfoidl.Windows.Forms.gfDataGridView areaDgv1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column22;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column23;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column24;
    }
}
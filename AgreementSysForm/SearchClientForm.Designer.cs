namespace PriceManager
{
    partial class SearchClientForm
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
            this.BtnQuery = new System.Windows.Forms.Button();
            this.txtCstName = new System.Windows.Forms.TextBox();
            this.txtCstCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvSearchClient = new gfoidl.Windows.Forms.gfDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchClient)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.BtnQuery);
            this.groupBox1.Controls.Add(this.txtCstName);
            this.groupBox1.Controls.Add(this.txtCstCode);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(470, 59);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // BtnQuery
            // 
            this.BtnQuery.Location = new System.Drawing.Point(404, 18);
            this.BtnQuery.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BtnQuery.Name = "BtnQuery";
            this.BtnQuery.Size = new System.Drawing.Size(51, 26);
            this.BtnQuery.TabIndex = 25;
            this.BtnQuery.Text = "查询";
            this.BtnQuery.UseVisualStyleBackColor = true;
            this.BtnQuery.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // txtCstName
            // 
            this.txtCstName.Location = new System.Drawing.Point(272, 20);
            this.txtCstName.Name = "txtCstName";
            this.txtCstName.Size = new System.Drawing.Size(110, 21);
            this.txtCstName.TabIndex = 24;
            // 
            // txtCstCode
            // 
            this.txtCstCode.Location = new System.Drawing.Point(76, 20);
            this.txtCstCode.Name = "txtCstCode";
            this.txtCstCode.Size = new System.Drawing.Size(110, 21);
            this.txtCstCode.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(205, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 23;
            this.label7.Text = "客户名称：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "客户编码：";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgvSearchClient);
            this.groupBox2.Location = new System.Drawing.Point(11, 74);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(469, 328);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "查询信息";
            // 
            // dgvSearchClient
            // 
            this.dgvSearchClient.AllowUserToAddRows = false;
            this.dgvSearchClient.AllowUserToDeleteRows = false;
            this.dgvSearchClient.AllowUserToResizeRows = false;
            this.dgvSearchClient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSearchClient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSearchClient.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column12,
            this.Column14});
            this.dgvSearchClient.Location = new System.Drawing.Point(10, 20);
            this.dgvSearchClient.Name = "dgvSearchClient";
            this.dgvSearchClient.ReadOnly = true;
            this.dgvSearchClient.RowHeadersVisible = false;
            this.dgvSearchClient.RowTemplate.Height = 23;
            this.dgvSearchClient.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSearchClient.Size = new System.Drawing.Size(446, 302);
            this.dgvSearchClient.TabIndex = 49;
            this.dgvSearchClient.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvSearchClient_MouseDoubleClick);
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
            this.Column2.DataPropertyName = "CstId";
            this.Column2.HeaderText = "客户ID";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            this.Column2.Width = 66;
            // 
            // Column12
            // 
            this.Column12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column12.DataPropertyName = "CstCode";
            this.Column12.HeaderText = "客户编码";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 78;
            // 
            // Column14
            // 
            this.Column14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column14.DataPropertyName = "CstName";
            this.Column14.HeaderText = "客户名称";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Width = 78;
            // 
            // SearchClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 411);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchClientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SearchClientForm";
            this.Load += new System.EventHandler(this.SearchClientForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchClient)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCstName;
        private System.Windows.Forms.TextBox txtCstCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BtnQuery;
        private gfoidl.Windows.Forms.gfDataGridView dgvSearchClient;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
    }
}
namespace PriceManager
{
    partial class SetSalePriceForm
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
            this.costRateUpDown = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.costPriceUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.costPrcUpDown = new System.Windows.Forms.NumericUpDown();
            this.beginDate = new System.Windows.Forms.DateTimePicker();
            this.endDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.offTypeCbo = new System.Windows.Forms.ComboBox();
            this.offLevelCbo = new System.Windows.Forms.ComboBox();
            this.prcUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.priceUpDown = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.saveBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.costRateUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.costPriceUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.costPrcUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.prcUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.priceUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // costRateUpDown
            // 
            this.costRateUpDown.DecimalPlaces = 6;
            this.costRateUpDown.Enabled = false;
            this.costRateUpDown.Location = new System.Drawing.Point(97, 185);
            this.costRateUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.costRateUpDown.Name = "costRateUpDown";
            this.costRateUpDown.Size = new System.Drawing.Size(130, 21);
            this.costRateUpDown.TabIndex = 81;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Location = new System.Drawing.Point(20, 187);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 82;
            this.label8.Text = "考核毛利率:";
            // 
            // costPriceUpDown
            // 
            this.costPriceUpDown.DecimalPlaces = 6;
            this.costPriceUpDown.Enabled = false;
            this.costPriceUpDown.Location = new System.Drawing.Point(97, 141);
            this.costPriceUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.costPriceUpDown.Name = "costPriceUpDown";
            this.costPriceUpDown.Size = new System.Drawing.Size(130, 21);
            this.costPriceUpDown.TabIndex = 78;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(20, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 80;
            this.label3.Text = "含税成本价:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(20, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 79;
            this.label2.Text = "无税成本价:";
            // 
            // costPrcUpDown
            // 
            this.costPrcUpDown.DecimalPlaces = 6;
            this.costPrcUpDown.Location = new System.Drawing.Point(97, 100);
            this.costPrcUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.costPrcUpDown.Name = "costPrcUpDown";
            this.costPrcUpDown.Size = new System.Drawing.Size(130, 21);
            this.costPrcUpDown.TabIndex = 77;
            this.costPrcUpDown.ValueChanged += new System.EventHandler(this.CostPrcUpDown_ValueChanged);
            // 
            // beginDate
            // 
            this.beginDate.Enabled = false;
            this.beginDate.Location = new System.Drawing.Point(333, 27);
            this.beginDate.Name = "beginDate";
            this.beginDate.Size = new System.Drawing.Size(130, 21);
            this.beginDate.TabIndex = 76;
            this.beginDate.Value = new System.DateTime(2017, 5, 22, 10, 45, 53, 0);
            // 
            // endDate
            // 
            this.endDate.Location = new System.Drawing.Point(333, 60);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(130, 21);
            this.endDate.TabIndex = 75;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Location = new System.Drawing.Point(268, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 74;
            this.label6.Text = "结束时间:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Location = new System.Drawing.Point(268, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 73;
            this.label7.Text = "开始时间:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Location = new System.Drawing.Point(268, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 72;
            this.label5.Text = "促销类型:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Location = new System.Drawing.Point(268, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 71;
            this.label4.Text = "促销级别:";
            // 
            // offTypeCbo
            // 
            this.offTypeCbo.FormattingEnabled = true;
            this.offTypeCbo.Location = new System.Drawing.Point(333, 140);
            this.offTypeCbo.Name = "offTypeCbo";
            this.offTypeCbo.Size = new System.Drawing.Size(130, 20);
            this.offTypeCbo.TabIndex = 70;
            // 
            // offLevelCbo
            // 
            this.offLevelCbo.FormattingEnabled = true;
            this.offLevelCbo.Location = new System.Drawing.Point(333, 99);
            this.offLevelCbo.Name = "offLevelCbo";
            this.offLevelCbo.Size = new System.Drawing.Size(130, 20);
            this.offLevelCbo.TabIndex = 69;
            // 
            // prcUpDown
            // 
            this.prcUpDown.DecimalPlaces = 6;
            this.prcUpDown.Location = new System.Drawing.Point(97, 27);
            this.prcUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.prcUpDown.Name = "prcUpDown";
            this.prcUpDown.Size = new System.Drawing.Size(130, 21);
            this.prcUpDown.TabIndex = 65;
            this.prcUpDown.ValueChanged += new System.EventHandler(this.PrcUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(44, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 68;
            this.label1.Text = "无税价:";
            // 
            // priceUpDown
            // 
            this.priceUpDown.DecimalPlaces = 6;
            this.priceUpDown.Enabled = false;
            this.priceUpDown.Location = new System.Drawing.Point(97, 63);
            this.priceUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.priceUpDown.Name = "priceUpDown";
            this.priceUpDown.Size = new System.Drawing.Size(130, 21);
            this.priceUpDown.TabIndex = 66;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label17.Location = new System.Drawing.Point(44, 29);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 12);
            this.label17.TabIndex = 67;
            this.label17.Text = "含税价:";
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(388, 185);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 64;
            this.saveBtn.Text = "保存";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(270, 185);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(75, 23);
            this.exitBtn.TabIndex = 63;
            this.exitBtn.Text = "退出";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // SetSalePriceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 227);
            this.ControlBox = false;
            this.Controls.Add(this.costRateUpDown);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.costPriceUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.costPrcUpDown);
            this.Controls.Add(this.beginDate);
            this.Controls.Add(this.endDate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.offTypeCbo);
            this.Controls.Add(this.offLevelCbo);
            this.Controls.Add(this.prcUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.priceUpDown);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.exitBtn);
            this.Name = "SetSalePriceForm";
            this.Text = "促销价格设定";
            this.Load += new System.EventHandler(this.SetSalePriceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.costRateUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.costPriceUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.costPrcUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.prcUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.priceUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown costRateUpDown;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown costPriceUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown costPrcUpDown;
        private System.Windows.Forms.DateTimePicker beginDate;
        private System.Windows.Forms.DateTimePicker endDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox offTypeCbo;
        private System.Windows.Forms.ComboBox offLevelCbo;
        private System.Windows.Forms.NumericUpDown prcUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown priceUpDown;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button exitBtn;
    }
}
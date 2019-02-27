namespace PriceManager
{
    partial class SetPriceForm
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
            this.exitBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.beginDate = new System.Windows.Forms.DateTimePicker();
            this.endDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bargain_offType = new System.Windows.Forms.ComboBox();
            this.isCredit_offLevel = new System.Windows.Forms.ComboBox();
            this.prcUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.priceUpDown = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.costPriceUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.costPrcUpDown = new System.Windows.Forms.NumericUpDown();
            this.costRateUpDown = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.bottomPrcUpDown = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.bottomPriceUpDown = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.suggestExecPrcUpDown = new System.Windows.Forms.NumericUpDown();
            this.suggestCostprcUpDown = new System.Windows.Forms.NumericUpDown();
            this.suggestBottomprcUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.prcUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.priceUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.costPriceUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.costPrcUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.costRateUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomPrcUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomPriceUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.suggestExecPrcUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.suggestCostprcUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.suggestBottomprcUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(547, 241);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(75, 34);
            this.exitBtn.TabIndex = 0;
            this.exitBtn.Text = "退出";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(653, 241);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 34);
            this.saveBtn.TabIndex = 1;
            this.saveBtn.Text = "保存";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // beginDate
            // 
            this.beginDate.Enabled = false;
            this.beginDate.Location = new System.Drawing.Point(101, 195);
            this.beginDate.MaxDate = new System.DateTime(2038, 12, 31, 0, 0, 0, 0);
            this.beginDate.Name = "beginDate";
            this.beginDate.Size = new System.Drawing.Size(130, 21);
            this.beginDate.TabIndex = 56;
            this.beginDate.Value = new System.DateTime(2017, 5, 22, 10, 45, 53, 0);
            // 
            // endDate
            // 
            this.endDate.Location = new System.Drawing.Point(343, 195);
            this.endDate.MaxDate = new System.DateTime(2038, 12, 31, 0, 0, 0, 0);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(130, 21);
            this.endDate.TabIndex = 55;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Location = new System.Drawing.Point(278, 201);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 52;
            this.label6.Text = "结束时间:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Location = new System.Drawing.Point(36, 201);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 51;
            this.label7.Text = "开始时间:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Location = new System.Drawing.Point(533, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 50;
            this.label5.Text = "是否议价:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Location = new System.Drawing.Point(278, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 49;
            this.label4.Text = "价格类型:";
            // 
            // bargain_offType
            // 
            this.bargain_offType.FormattingEnabled = true;
            this.bargain_offType.Location = new System.Drawing.Point(598, 153);
            this.bargain_offType.Name = "bargain_offType";
            this.bargain_offType.Size = new System.Drawing.Size(130, 20);
            this.bargain_offType.TabIndex = 48;
            // 
            // isCredit_offLevel
            // 
            this.isCredit_offLevel.FormattingEnabled = true;
            this.isCredit_offLevel.Location = new System.Drawing.Point(343, 153);
            this.isCredit_offLevel.Name = "isCredit_offLevel";
            this.isCredit_offLevel.Size = new System.Drawing.Size(130, 20);
            this.isCredit_offLevel.TabIndex = 47;
            // 
            // prcUpDown
            // 
            this.prcUpDown.DecimalPlaces = 6;
            this.prcUpDown.Location = new System.Drawing.Point(101, 21);
            this.prcUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.prcUpDown.Name = "prcUpDown";
            this.prcUpDown.Size = new System.Drawing.Size(130, 21);
            this.prcUpDown.TabIndex = 39;
            this.prcUpDown.ValueChanged += new System.EventHandler(this.PrcUpDown_ValueChanged);
            this.prcUpDown.Click += new System.EventHandler(this.UpDown_Selected);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(254, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 44;
            this.label1.Text = "公开无税售价:";
            // 
            // priceUpDown
            // 
            this.priceUpDown.DecimalPlaces = 6;
            this.priceUpDown.Enabled = false;
            this.priceUpDown.Location = new System.Drawing.Point(343, 20);
            this.priceUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.priceUpDown.Name = "priceUpDown";
            this.priceUpDown.Size = new System.Drawing.Size(130, 21);
            this.priceUpDown.TabIndex = 41;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label17.Location = new System.Drawing.Point(12, 23);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(83, 12);
            this.label17.TabIndex = 43;
            this.label17.Text = "公开含税售价:";
            // 
            // costPriceUpDown
            // 
            this.costPriceUpDown.DecimalPlaces = 6;
            this.costPriceUpDown.Enabled = false;
            this.costPriceUpDown.Location = new System.Drawing.Point(343, 64);
            this.costPriceUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.costPriceUpDown.Name = "costPriceUpDown";
            this.costPriceUpDown.Size = new System.Drawing.Size(130, 21);
            this.costPriceUpDown.TabIndex = 58;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(24, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 60;
            this.label3.Text = "含税成本价:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(266, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 59;
            this.label2.Text = "无税成本价:";
            // 
            // costPrcUpDown
            // 
            this.costPrcUpDown.DecimalPlaces = 6;
            this.costPrcUpDown.Location = new System.Drawing.Point(101, 64);
            this.costPrcUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.costPrcUpDown.Name = "costPrcUpDown";
            this.costPrcUpDown.Size = new System.Drawing.Size(130, 21);
            this.costPrcUpDown.TabIndex = 57;
            this.costPrcUpDown.ValueChanged += new System.EventHandler(this.CostPrcUpDown_ValueChanged);
            this.costPrcUpDown.Click += new System.EventHandler(this.UpDown_Selected);
            // 
            // costRateUpDown
            // 
            this.costRateUpDown.DecimalPlaces = 6;
            this.costRateUpDown.Enabled = false;
            this.costRateUpDown.Location = new System.Drawing.Point(101, 154);
            this.costRateUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.costRateUpDown.Name = "costRateUpDown";
            this.costRateUpDown.Size = new System.Drawing.Size(130, 21);
            this.costRateUpDown.TabIndex = 61;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Location = new System.Drawing.Point(24, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 62;
            this.label8.Text = "考核毛利率:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label9.Location = new System.Drawing.Point(60, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 64;
            this.label9.Text = "底价:";
            // 
            // bottomPrcUpDown
            // 
            this.bottomPrcUpDown.DecimalPlaces = 6;
            this.bottomPrcUpDown.Location = new System.Drawing.Point(101, 107);
            this.bottomPrcUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.bottomPrcUpDown.Name = "bottomPrcUpDown";
            this.bottomPrcUpDown.Size = new System.Drawing.Size(130, 21);
            this.bottomPrcUpDown.TabIndex = 63;
            this.bottomPrcUpDown.ValueChanged += new System.EventHandler(this.BottomPrcUpDown_ValueChanged);
            this.bottomPrcUpDown.Click += new System.EventHandler(this.UpDown_Selected);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label10.Location = new System.Drawing.Point(278, 109);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 66;
            this.label10.Text = "无税底价:";
            // 
            // bottomPriceUpDown
            // 
            this.bottomPriceUpDown.DecimalPlaces = 6;
            this.bottomPriceUpDown.Enabled = false;
            this.bottomPriceUpDown.Location = new System.Drawing.Point(343, 107);
            this.bottomPriceUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.bottomPriceUpDown.Name = "bottomPriceUpDown";
            this.bottomPriceUpDown.Size = new System.Drawing.Size(130, 21);
            this.bottomPriceUpDown.TabIndex = 65;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label11.Location = new System.Drawing.Point(509, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(83, 12);
            this.label11.TabIndex = 70;
            this.label11.Text = "建议公开售价:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label12.Location = new System.Drawing.Point(497, 66);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(95, 12);
            this.label12.TabIndex = 71;
            this.label12.Text = "建议含税成本价:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label13.Location = new System.Drawing.Point(533, 109);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 12);
            this.label13.TabIndex = 72;
            this.label13.Text = "建议底价:";
            // 
            // suggestExecPrcUpDown
            // 
            this.suggestExecPrcUpDown.DecimalPlaces = 6;
            this.suggestExecPrcUpDown.Enabled = false;
            this.suggestExecPrcUpDown.Location = new System.Drawing.Point(598, 21);
            this.suggestExecPrcUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.suggestExecPrcUpDown.Name = "suggestExecPrcUpDown";
            this.suggestExecPrcUpDown.Size = new System.Drawing.Size(130, 21);
            this.suggestExecPrcUpDown.TabIndex = 73;
            // 
            // suggestCostprcUpDown
            // 
            this.suggestCostprcUpDown.DecimalPlaces = 6;
            this.suggestCostprcUpDown.Enabled = false;
            this.suggestCostprcUpDown.Location = new System.Drawing.Point(598, 64);
            this.suggestCostprcUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.suggestCostprcUpDown.Name = "suggestCostprcUpDown";
            this.suggestCostprcUpDown.Size = new System.Drawing.Size(130, 21);
            this.suggestCostprcUpDown.TabIndex = 74;
            // 
            // suggestBottomprcUpDown
            // 
            this.suggestBottomprcUpDown.DecimalPlaces = 6;
            this.suggestBottomprcUpDown.Enabled = false;
            this.suggestBottomprcUpDown.Location = new System.Drawing.Point(598, 107);
            this.suggestBottomprcUpDown.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.suggestBottomprcUpDown.Name = "suggestBottomprcUpDown";
            this.suggestBottomprcUpDown.Size = new System.Drawing.Size(130, 21);
            this.suggestBottomprcUpDown.TabIndex = 75;
            // 
            // SetPriceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 291);
            this.ControlBox = false;
            this.Controls.Add(this.suggestBottomprcUpDown);
            this.Controls.Add(this.suggestCostprcUpDown);
            this.Controls.Add(this.suggestExecPrcUpDown);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.bottomPriceUpDown);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.bottomPrcUpDown);
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
            this.Controls.Add(this.bargain_offType);
            this.Controls.Add(this.isCredit_offLevel);
            this.Controls.Add(this.prcUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.priceUpDown);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.exitBtn);
            this.Name = "SetPriceForm";
            this.Text = "价格设定";
            this.Load += new System.EventHandler(this.SetPriceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.prcUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.priceUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.costPriceUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.costPrcUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.costRateUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomPrcUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bottomPriceUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.suggestExecPrcUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.suggestCostprcUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.suggestBottomprcUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.DateTimePicker beginDate;
        private System.Windows.Forms.DateTimePicker endDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox bargain_offType;
        private System.Windows.Forms.ComboBox isCredit_offLevel;
        private System.Windows.Forms.NumericUpDown prcUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown priceUpDown;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown costPriceUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown costPrcUpDown;
        private System.Windows.Forms.NumericUpDown costRateUpDown;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown bottomPrcUpDown;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown bottomPriceUpDown;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown suggestExecPrcUpDown;
        private System.Windows.Forms.NumericUpDown suggestCostprcUpDown;
        private System.Windows.Forms.NumericUpDown suggestBottomprcUpDown;
    }
}
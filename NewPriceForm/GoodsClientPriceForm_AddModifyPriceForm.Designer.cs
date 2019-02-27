namespace PriceManager
{
    partial class GoodsClientPriceForm_AddModifyPriceForm
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
            this.cbShopflag = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbCostprctype = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbDeliveryfeerate = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCostrate = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBottomprice = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCostprice = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpEndtime = new System.Windows.Forms.DateTimePicker();
            this.dtpStarttime = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBottomprc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCostprc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPrc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtGoods = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.btnModfiy = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cbDeliveryfeerate)).BeginInit();
            this.SuspendLayout();
            // 
            // cbShopflag
            // 
            this.cbShopflag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbShopflag.Enabled = false;
            this.cbShopflag.FormattingEnabled = true;
            this.cbShopflag.IntegralHeight = false;
            this.cbShopflag.Location = new System.Drawing.Point(595, 180);
            this.cbShopflag.Name = "cbShopflag";
            this.cbShopflag.Size = new System.Drawing.Size(130, 20);
            this.cbShopflag.TabIndex = 169;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(530, 183);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 168;
            this.label12.Text = "停用标识:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbCostprctype
            // 
            this.cbCostprctype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCostprctype.Enabled = false;
            this.cbCostprctype.FormattingEnabled = true;
            this.cbCostprctype.IntegralHeight = false;
            this.cbCostprctype.Location = new System.Drawing.Point(595, 139);
            this.cbCostprctype.Name = "cbCostprctype";
            this.cbCostprctype.Size = new System.Drawing.Size(130, 20);
            this.cbCostprctype.TabIndex = 167;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(528, 143);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(59, 12);
            this.label26.TabIndex = 166;
            this.label26.Text = "成本类型:";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCode
            // 
            this.txtCode.Enabled = false;
            this.txtCode.Location = new System.Drawing.Point(351, 27);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(130, 21);
            this.txtCode.TabIndex = 164;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label10.Location = new System.Drawing.Point(272, 30);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 12);
            this.label10.TabIndex = 165;
            this.label10.Text = "客户组代码:";
            // 
            // cbDeliveryfeerate
            // 
            this.cbDeliveryfeerate.DecimalPlaces = 6;
            this.cbDeliveryfeerate.Enabled = false;
            this.cbDeliveryfeerate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.cbDeliveryfeerate.Location = new System.Drawing.Point(595, 67);
            this.cbDeliveryfeerate.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            393216});
            this.cbDeliveryfeerate.Minimum = new decimal(new int[] {
            -727379969,
            232,
            0,
            -2147090432});
            this.cbDeliveryfeerate.Name = "cbDeliveryfeerate";
            this.cbDeliveryfeerate.Size = new System.Drawing.Size(104, 21);
            this.cbDeliveryfeerate.TabIndex = 163;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label11.Location = new System.Drawing.Point(516, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 12);
            this.label11.TabIndex = 162;
            this.label11.Text = "默认加点率:";
            // 
            // txtCostrate
            // 
            this.txtCostrate.Enabled = false;
            this.txtCostrate.Location = new System.Drawing.Point(595, 105);
            this.txtCostrate.Name = "txtCostrate";
            this.txtCostrate.Size = new System.Drawing.Size(130, 21);
            this.txtCostrate.TabIndex = 161;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label9.Location = new System.Drawing.Point(518, 108);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 12);
            this.label9.TabIndex = 160;
            this.label9.Text = "考核毛利率:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Location = new System.Drawing.Point(286, 183);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 159;
            this.label8.Text = "结束时间:";
            // 
            // txtBottomprice
            // 
            this.txtBottomprice.Enabled = false;
            this.txtBottomprice.Location = new System.Drawing.Point(351, 105);
            this.txtBottomprice.Name = "txtBottomprice";
            this.txtBottomprice.Size = new System.Drawing.Size(130, 21);
            this.txtBottomprice.TabIndex = 158;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Location = new System.Drawing.Point(286, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 157;
            this.label5.Text = "无税底价:";
            // 
            // txtCostprice
            // 
            this.txtCostprice.Enabled = false;
            this.txtCostprice.Location = new System.Drawing.Point(351, 64);
            this.txtCostprice.Name = "txtCostprice";
            this.txtCostprice.Size = new System.Drawing.Size(130, 21);
            this.txtCostprice.TabIndex = 156;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Location = new System.Drawing.Point(272, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 155;
            this.label6.Text = "无税成本价:";
            // 
            // txtPrice
            // 
            this.txtPrice.Enabled = false;
            this.txtPrice.Location = new System.Drawing.Point(351, 143);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(130, 21);
            this.txtPrice.TabIndex = 154;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Location = new System.Drawing.Point(265, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 153;
            this.label7.Text = "公开无税销价:";
            // 
            // dtpEndtime
            // 
            this.dtpEndtime.Enabled = false;
            this.dtpEndtime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndtime.Location = new System.Drawing.Point(351, 180);
            this.dtpEndtime.Name = "dtpEndtime";
            this.dtpEndtime.Size = new System.Drawing.Size(130, 21);
            this.dtpEndtime.TabIndex = 147;
            // 
            // dtpStarttime
            // 
            this.dtpStarttime.Enabled = false;
            this.dtpStarttime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStarttime.Location = new System.Drawing.Point(116, 177);
            this.dtpStarttime.Name = "dtpStarttime";
            this.dtpStarttime.Size = new System.Drawing.Size(130, 21);
            this.dtpStarttime.TabIndex = 146;
            this.dtpStarttime.ValueChanged += new System.EventHandler(this.dtpStarttime_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(50, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 152;
            this.label3.Text = "开始时间:";
            // 
            // txtBottomprc
            // 
            this.txtBottomprc.Location = new System.Drawing.Point(116, 105);
            this.txtBottomprc.Name = "txtBottomprc";
            this.txtBottomprc.Size = new System.Drawing.Size(130, 21);
            this.txtBottomprc.TabIndex = 145;
            this.txtBottomprc.TextChanged += new System.EventHandler(this.txtBottomprc_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Location = new System.Drawing.Point(51, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 151;
            this.label4.Text = "含税底价:";
            // 
            // txtCostprc
            // 
            this.txtCostprc.Enabled = false;
            this.txtCostprc.Location = new System.Drawing.Point(116, 64);
            this.txtCostprc.Name = "txtCostprc";
            this.txtCostprc.Size = new System.Drawing.Size(130, 21);
            this.txtCostprc.TabIndex = 144;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(38, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 150;
            this.label2.Text = "含税成本价:";
            // 
            // txtPrc
            // 
            this.txtPrc.Enabled = false;
            this.txtPrc.Location = new System.Drawing.Point(116, 143);
            this.txtPrc.Name = "txtPrc";
            this.txtPrc.Size = new System.Drawing.Size(130, 21);
            this.txtPrc.TabIndex = 143;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(30, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 149;
            this.label1.Text = "公开含税销价:";
            // 
            // txtGoods
            // 
            this.txtGoods.Enabled = false;
            this.txtGoods.Location = new System.Drawing.Point(116, 27);
            this.txtGoods.Name = "txtGoods";
            this.txtGoods.Size = new System.Drawing.Size(130, 21);
            this.txtGoods.TabIndex = 142;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label21.Location = new System.Drawing.Point(52, 30);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(59, 12);
            this.label21.TabIndex = 148;
            this.label21.Text = "商品代码:";
            // 
            // btnModfiy
            // 
            this.btnModfiy.Location = new System.Drawing.Point(312, 225);
            this.btnModfiy.Name = "btnModfiy";
            this.btnModfiy.Size = new System.Drawing.Size(75, 23);
            this.btnModfiy.TabIndex = 170;
            this.btnModfiy.Text = "修改";
            this.btnModfiy.UseVisualStyleBackColor = true;
            this.btnModfiy.Click += new System.EventHandler(this.btnModfiy_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(312, 254);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 171;
            this.btnSubmit.Text = "新增";
            this.btnSubmit.UseVisualStyleBackColor = true;
            // 
            // GoodsClientPriceForm_AddModifyPriceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 299);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnModfiy);
            this.Controls.Add(this.cbShopflag);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cbCostprctype);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbDeliveryfeerate);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtCostrate);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtBottomprice);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCostprice);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtpEndtime);
            this.Controls.Add(this.dtpStarttime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBottomprc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCostprc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPrc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtGoods);
            this.Controls.Add(this.label21);
            this.Name = "GoodsClientPriceForm_AddModifyPriceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GoodsClientPriceForm_AddModifyPriceForm";
            this.Load += new System.EventHandler(this.GoodsClientPriceForm_AddModifyPriceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbDeliveryfeerate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbShopflag;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbCostprctype;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown cbDeliveryfeerate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtCostrate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBottomprice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCostprice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpEndtime;
        private System.Windows.Forms.DateTimePicker dtpStarttime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBottomprc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCostprc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPrc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGoods;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnModfiy;
        private System.Windows.Forms.Button btnSubmit;
    }
}
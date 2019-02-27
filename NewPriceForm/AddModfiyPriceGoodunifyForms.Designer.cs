namespace PriceManager
{
    partial class AddModfiyPriceGoodunifyForms
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
            this.cbDefaultdept = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbShopflag = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.btnModfiy = new System.Windows.Forms.Button();
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
            this.btnSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbDefaultdept
            // 
            this.cbDefaultdept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDefaultdept.Enabled = false;
            this.cbDefaultdept.FormattingEnabled = true;
            this.cbDefaultdept.Location = new System.Drawing.Point(364, 38);
            this.cbDefaultdept.Name = "cbDefaultdept";
            this.cbDefaultdept.Size = new System.Drawing.Size(130, 20);
            this.cbDefaultdept.TabIndex = 158;
            this.cbDefaultdept.TextChanged += new System.EventHandler(this.cbDefaultdept_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(299, 41);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 157;
            this.label10.Text = "默认部门:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbShopflag
            // 
            this.cbShopflag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbShopflag.Enabled = false;
            this.cbShopflag.FormattingEnabled = true;
            this.cbShopflag.IntegralHeight = false;
            this.cbShopflag.Location = new System.Drawing.Point(364, 226);
            this.cbShopflag.Name = "cbShopflag";
            this.cbShopflag.Size = new System.Drawing.Size(130, 20);
            this.cbShopflag.TabIndex = 156;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(299, 229);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(59, 12);
            this.label26.TabIndex = 155;
            this.label26.Text = "停用标识:";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnModfiy
            // 
            this.btnModfiy.Location = new System.Drawing.Point(224, 288);
            this.btnModfiy.Name = "btnModfiy";
            this.btnModfiy.Size = new System.Drawing.Size(75, 23);
            this.btnModfiy.TabIndex = 154;
            this.btnModfiy.Text = "修改";
            this.btnModfiy.UseVisualStyleBackColor = true;
            this.btnModfiy.Click += new System.EventHandler(this.btnModfiy_Click);
            // 
            // txtCostrate
            // 
            this.txtCostrate.Enabled = false;
            this.txtCostrate.Location = new System.Drawing.Point(129, 226);
            this.txtCostrate.Name = "txtCostrate";
            this.txtCostrate.Size = new System.Drawing.Size(130, 21);
            this.txtCostrate.TabIndex = 153;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label9.Location = new System.Drawing.Point(52, 229);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 12);
            this.label9.TabIndex = 152;
            this.label9.Text = "考核毛利率:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Location = new System.Drawing.Point(299, 194);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 151;
            this.label8.Text = "结束时间:";
            // 
            // txtBottomprice
            // 
            this.txtBottomprice.Enabled = false;
            this.txtBottomprice.Location = new System.Drawing.Point(364, 114);
            this.txtBottomprice.Name = "txtBottomprice";
            this.txtBottomprice.Size = new System.Drawing.Size(130, 21);
            this.txtBottomprice.TabIndex = 150;
            this.txtBottomprice.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBottomprice_KeyUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Location = new System.Drawing.Point(299, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 149;
            this.label5.Text = "无税底价:";
            // 
            // txtCostprice
            // 
            this.txtCostprice.Enabled = false;
            this.txtCostprice.Location = new System.Drawing.Point(364, 78);
            this.txtCostprice.Name = "txtCostprice";
            this.txtCostprice.Size = new System.Drawing.Size(130, 21);
            this.txtCostprice.TabIndex = 148;
            this.txtCostprice.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCostprice_KeyUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Location = new System.Drawing.Point(286, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 147;
            this.label6.Text = "无税成本价:";
            // 
            // txtPrice
            // 
            this.txtPrice.Enabled = false;
            this.txtPrice.Location = new System.Drawing.Point(365, 154);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(130, 21);
            this.txtPrice.TabIndex = 146;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Location = new System.Drawing.Point(279, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 145;
            this.label7.Text = "公开无税销价:";
            // 
            // dtpEndtime
            // 
            this.dtpEndtime.Enabled = false;
            this.dtpEndtime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndtime.Location = new System.Drawing.Point(364, 191);
            this.dtpEndtime.Name = "dtpEndtime";
            this.dtpEndtime.Size = new System.Drawing.Size(130, 21);
            this.dtpEndtime.TabIndex = 139;
            // 
            // dtpStarttime
            // 
            this.dtpStarttime.Enabled = false;
            this.dtpStarttime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStarttime.Location = new System.Drawing.Point(129, 188);
            this.dtpStarttime.Name = "dtpStarttime";
            this.dtpStarttime.Size = new System.Drawing.Size(130, 21);
            this.dtpStarttime.TabIndex = 138;
            this.dtpStarttime.ValueChanged += new System.EventHandler(this.dtpStarttime_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(63, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 144;
            this.label3.Text = "开始时间:";
            // 
            // txtBottomprc
            // 
            this.txtBottomprc.Location = new System.Drawing.Point(129, 114);
            this.txtBottomprc.Name = "txtBottomprc";
            this.txtBottomprc.Size = new System.Drawing.Size(130, 21);
            this.txtBottomprc.TabIndex = 137;
            this.txtBottomprc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBottomprc_KeyUp_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Location = new System.Drawing.Point(64, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 143;
            this.label4.Text = "含税底价:";
            // 
            // txtCostprc
            // 
            this.txtCostprc.Enabled = false;
            this.txtCostprc.Location = new System.Drawing.Point(129, 78);
            this.txtCostprc.Name = "txtCostprc";
            this.txtCostprc.Size = new System.Drawing.Size(130, 21);
            this.txtCostprc.TabIndex = 136;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(51, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 142;
            this.label2.Text = "含税成本价:";
            // 
            // txtPrc
            // 
            this.txtPrc.Enabled = false;
            this.txtPrc.Location = new System.Drawing.Point(130, 154);
            this.txtPrc.Name = "txtPrc";
            this.txtPrc.Size = new System.Drawing.Size(130, 21);
            this.txtPrc.TabIndex = 135;
            this.txtPrc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPrc_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(44, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 141;
            this.label1.Text = "公开含税销价:";
            // 
            // txtGoods
            // 
            this.txtGoods.Enabled = false;
            this.txtGoods.Location = new System.Drawing.Point(129, 38);
            this.txtGoods.Name = "txtGoods";
            this.txtGoods.Size = new System.Drawing.Size(130, 21);
            this.txtGoods.TabIndex = 134;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label21.Location = new System.Drawing.Point(65, 41);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(59, 12);
            this.label21.TabIndex = 140;
            this.label21.Text = "商品代码:";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(224, 315);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 159;
            this.btnSubmit.Text = "提交";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // AddModfiyPriceGoodunifyForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 350);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.cbDefaultdept);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbShopflag);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.btnModfiy);
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
            this.Name = "AddModfiyPriceGoodunifyForms";
            this.Text = "AddModfiyPriceGoodunifyForms";
            this.Load += new System.EventHandler(this.AddModfiyPriceGoodunifyForms_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDefaultdept;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbShopflag;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button btnModfiy;
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
        private System.Windows.Forms.Button btnSubmit;
    }
}
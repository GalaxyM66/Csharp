namespace PriceManager
{
    partial class AddBtbPriceShieldForm
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
            this.txtGoods = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProdName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGoodsName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSpec = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProducer = new System.Windows.Forms.TextBox();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtGoods
            // 
            this.txtGoods.Location = new System.Drawing.Point(115, 65);
            this.txtGoods.Name = "txtGoods";
            this.txtGoods.Size = new System.Drawing.Size(99, 21);
            this.txtGoods.TabIndex = 7;
            this.txtGoods.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtGoods_MouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "商品代码：";
            // 
            // txtProdName
            // 
            this.txtProdName.Location = new System.Drawing.Point(115, 18);
            this.txtProdName.Name = "txtProdName";
            this.txtProdName.Size = new System.Drawing.Size(99, 21);
            this.txtProdName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "商务团队：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "商品名称：";
            // 
            // txtGoodsName
            // 
            this.txtGoodsName.Enabled = false;
            this.txtGoodsName.Location = new System.Drawing.Point(115, 111);
            this.txtGoodsName.Name = "txtGoodsName";
            this.txtGoodsName.Size = new System.Drawing.Size(99, 21);
            this.txtGoodsName.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = " 规  格：";
            // 
            // txtSpec
            // 
            this.txtSpec.Enabled = false;
            this.txtSpec.Location = new System.Drawing.Point(115, 157);
            this.txtSpec.Name = "txtSpec";
            this.txtSpec.Size = new System.Drawing.Size(99, 21);
            this.txtSpec.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(50, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = " 厂  家：";
            // 
            // txtProducer
            // 
            this.txtProducer.Enabled = false;
            this.txtProducer.Location = new System.Drawing.Point(115, 202);
            this.txtProducer.Name = "txtProducer";
            this.txtProducer.Size = new System.Drawing.Size(99, 21);
            this.txtProducer.TabIndex = 13;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Location = new System.Drawing.Point(99, 244);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(61, 24);
            this.BtnAdd.TabIndex = 14;
            this.BtnAdd.Text = "新 增";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // AddBtbPriceShieldForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 289);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.txtProducer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSpec);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtGoodsName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtGoods);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtProdName);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "AddBtbPriceShieldForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddBtbPriceShieldForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddBtbPriceShieldForm_FormClosing);
            this.Load += new System.EventHandler(this.AddBtbPriceShieldForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtGoods;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProdName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGoodsName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSpec;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProducer;
        private System.Windows.Forms.Button BtnAdd;
    }
}
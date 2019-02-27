using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PriceManager
{
    public partial class AddBtbPriceShieldForm : Form
    {
        APDao_GoodsClientPrice dao = new APDao_GoodsClientPrice();
        SPRetInfo retinfo = new SPRetInfo();
        string goodid = "";

        public AddBtbPriceShieldForm()
        {
            InitializeComponent();
        }

        private void AddBtbPriceShieldForm_Load(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Red;
            label2.ForeColor = Color.Red;
        }
        //委托 事件处理方法
        void selForm_TransfEvent(GoodsDetails goodInfo)
        {
            txtGoods.Text = goodInfo.Goods;
            txtGoodsName.Text = goodInfo.GoodsName;
            txtSpec.Text = goodInfo.Spec;
            txtProducer.Text = goodInfo.Producer;
            goodid = goodInfo.GoodId;

        }
        //单击商品代码
        private void txtGoods_MouseClick(object sender, MouseEventArgs e)
        {
            BtbPriceDetailForm bpdForm = new BtbPriceDetailForm();
            bpdForm.Text = "商品详细";
            bpdForm.TrandfEvent += selForm_TransfEvent;
            bpdForm.ShowDialog();
        }
        //新增
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Boolean flag =true;
            string prodname = "";
            string goods = "";
            if (StringUtils.IsNotNull(txtProdName.Text))
            {
                 prodname = txtProdName.Text.ToString().Trim();
            }
            else {
                MessageBox.Show("商务团队为必填项！", "前台提示");
                txtProdName.Focus();
                flag = false;
            }
            if (StringUtils.IsNotNull(txtGoods.Text))
            {
                 goods = txtGoods.Text.ToString().Trim();               
            }
            else
            {
                MessageBox.Show("商品代码为必填项！", "前台提示");
                txtGoods.Focus();
                flag = false;
            }
            if (flag==true) {
                string goodsname = txtGoodsName.Text.ToString().Trim();
                string spec = txtSpec.Text.ToString().Trim();
                string producer = txtProducer.Text.ToString().Trim();

                dao.AddBtbPriceShieldInfo(goodid,prodname,goods,goodsname,spec,producer,retinfo);
                if (retinfo.num=="1") {
                    MessageBox.Show(retinfo.msg,"后台提示");
                }
                else {
                    MessageBox.Show(retinfo.msg, "后台提示");
                }
            }

        }
        //监听窗口关闭
        private void AddBtbPriceShieldForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}

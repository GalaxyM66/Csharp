using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace PriceManager.B2BTools
{
    public partial class AddOrUpdateGenCstGoodForm : Form
    {
        public int stateUI = 0;

        public GenCstGood GencstgoodInfo;
        APDao_B2BTools dao = null;
        PMSystemDao Pdao = new PMSystemDao();
        SPRetInfo retinfo = new SPRetInfo();

        public AddOrUpdateGenCstGoodForm()
        {
            InitializeComponent();
        }
        private void AddOrUpdateGenCstGoodForm_Load(object sender, EventArgs e)
        {
            dao =(APDao_B2BTools )this.Tag;
            initUI(stateUI);

        }
        //判断是新增还是修改
        private void initUI(int astate) {
            switch (astate) {
                case 0://新增界面
                    updateBtn.Visible = false;
                    emptyBtn.Visible = true;
                    submitBtn.Visible = true;
                    this.Text = "新增";
                    label1.ForeColor = Color.Red;
                    label2.ForeColor = Color.Red;
                    label3.ForeColor = Color.Red;
                    label4.ForeColor = Color.Red;
                    label5.ForeColor = Color.Red;
                    label6.ForeColor = Color.Red;
                    label9.ForeColor = Color.Red;
                    txtTransRate.Text = "1";
                    break;

                case 1://修改界面
                    emptyBtn.Visible = false;
                    submitBtn.Visible = false;
                    this.Text = "修改";
                    //修改界面需要将原始信息传送到文本框
                    txtCmsGoodsCode.Text = GencstgoodInfo.Goods;
                    txtSaleDept.Text = GencstgoodInfo.Saledeptid;
                    txtClientCode.Text = GencstgoodInfo.Cstcode;
                    txtGenClientGoodCode.Text = GencstgoodInfo.Gengoods;
                    txtGenSpec.Text = GencstgoodInfo.Genspec;
                    txtGenProducer.Text = GencstgoodInfo.Genproducer;
                    txtRatifier.Text = GencstgoodInfo.Ratifier;
                    txtGoodBar.Text = GencstgoodInfo.Goodbar;
                    txtTransRate.Text = GencstgoodInfo.Transrate;
                    //txtTransRate.Text = GencstgoodInfo.Relatid;
                    break;
            }
        }

        //新增对码  清空按钮
        private void emptyBtn_Click(object sender, EventArgs e)
        {
            txtCmsGoodsCode.Text = null;
            txtSaleDept.Text = null;
            txtClientCode.Text = null;
            txtGenClientGoodCode.Text = null;
            txtGenSpec.Text = null;
            txtGenProducer.Text = null;
            txtRatifier.Text = null;
            txtGoodBar.Text = null;
            txtTransRate.Text = null;
        }

        //新增对码  cms商品码文本框失去焦点事件
        private void txtCmsGoodsCode_Leave(object sender, EventArgs e)
        {
            SPRetInfo retinfo = new SPRetInfo();
            string Goods = txtCmsGoodsCode.Text.Trim();
            // 将商品码传给后台
            dao.DeptidForGoods(Goods, retinfo);
            if (retinfo.num == "1")
            {
                MessageBox.Show("cms商品码存在！" + retinfo.msg + "|后台提示");
                txtSaleDept.Text = retinfo.result;
                return;
            }
            else
            {
                MessageBox.Show("cms商品码不存在！" + retinfo.msg + "|后台提示");
                txtCmsGoodsCode.Text = null;
                txtCmsGoodsCode.Focus();
                return;
            }

        }
        //新增对码
        private void submitBtn_Click(object sender, EventArgs e)
        {
            
            //判断前台是否输入信息
            if (String.IsNullOrEmpty(txtCmsGoodsCode.Text)) {
                MessageBox.Show("前台提示||请输入cms商品码！");
                txtCmsGoodsCode.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtSaleDept.Text))
            {
                MessageBox.Show("前台提示||请输入部门！");
                txtSaleDept.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtClientCode.Text))
            {
                MessageBox.Show("前台提示||请输入客户代码！");
                txtClientCode.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtGenClientGoodCode.Text))
            {
                MessageBox.Show("前台提示||请输入客户商品外码！");
                txtGenClientGoodCode.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtGenSpec.Text))
            {
                MessageBox.Show("前台提示||请输入外部商品品名规格！");
                txtGenSpec.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtGenProducer.Text))
            {
                MessageBox.Show("前台提示||请输入外部商品厂家！");
                txtGenProducer.Focus();
                return;
            }
            //if (String.IsNullOrEmpty(txtRatifier.Text))
            //{
            //    MessageBox.Show("前台提示||请输入国药准字！");
            //    txtRatifier.Focus();
            //    return;
            //}
            //if (String.IsNullOrEmpty(txtGoodBar.Text))
            //{
            //    MessageBox.Show("前台提示||请输入外部条形码！");
            //    txtGoodBar.Focus();
            //    return;
            //}
            if (String.IsNullOrEmpty(txtTransRate.Text))
            {
                MessageBox.Show("前台提示||请输入转换比！");
                txtTransRate.Focus();
                return;
            }

            dao.AddOrUpdateGenCstGood(txtSaleDept.Text,txtClientCode.Text, txtCmsGoodsCode.Text,txtGenClientGoodCode.Text,txtGenSpec.Text,txtGenProducer.Text,txtTransRate.Text,txtGoodBar.Text,txtRatifier.Text,stateUI,retinfo);
            if (retinfo.num =="1") {
                MessageBox.Show("新增成功！" + retinfo.msg + "|后台提示");
                return;

            } else {
                MessageBox.Show("新增失败！" + retinfo.msg + "|后台提示");
                return;
            }

        }
        //修改对码
        private void updateBtn_Click(object sender, EventArgs e)
        {
            string RELATID = GencstgoodInfo.Relatid;
            //限制5个字段为必填项
            if (String.IsNullOrEmpty(txtCmsGoodsCode.Text))
            {
                MessageBox.Show("前台提示||请输入cms商品码！");
                txtCmsGoodsCode.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtSaleDept.Text))
            {
                MessageBox.Show("前台提示||请输入部门！");
                txtSaleDept.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtClientCode.Text))
            {
                MessageBox.Show("前台提示||请输入客户代码！");
                txtClientCode.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtGenClientGoodCode.Text))
            {
                MessageBox.Show("前台提示||请输入客户商品外码！");
                txtGenClientGoodCode.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtTransRate.Text))
            {
                MessageBox.Show("前台提示||请输入转换比！");
                txtTransRate.Focus();
                return;
            }
            dao.AddOrUpdateGenCstGood(txtSaleDept.Text, txtClientCode.Text, txtCmsGoodsCode.Text, txtGenClientGoodCode.Text, txtGenSpec.Text, txtGenProducer.Text, txtTransRate.Text, txtGoodBar.Text, txtRatifier.Text, int.Parse(RELATID), retinfo);
            if (retinfo.num == "1")
            {
                MessageBox.Show("修改成功！" + retinfo.msg + "|后台提示");
                this.Close();
                return;

            }
            else
            {
                MessageBox.Show("修改失败！" + retinfo.msg + "|后台提示");
                return;
            }


        }
    }
}

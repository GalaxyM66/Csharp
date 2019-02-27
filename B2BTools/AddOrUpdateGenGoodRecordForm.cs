using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/// <summary>
/// ------------------------2018-8-1-----------------------
/// 
/// 
/// </summary>
namespace PriceManager.B2BTools
{
    public partial class AddOrUpdateGenGoodRecordForm : Form
    {
        public int stateUI = 0;
        public GenGoodRecord GenGoodRecordInfo;
        APDao_B2BTools dao = null;
        PMSystemDao Pdao = new PMSystemDao();
        SPRetInfo retinfo = new SPRetInfo();


        public AddOrUpdateGenGoodRecordForm()
        {
            InitializeComponent();
        }
        private void AddOrUpdateGenGoodRecordForm_Load(object sender, EventArgs e)
        {
            dao = (APDao_B2BTools)this.Tag;
            initUI(stateUI);

        }
        //判断是新增还是修改
        private void initUI(int astate)
        {
            switch (astate)
            {
                case 0://新增界面
                    updateBtn.Visible = false;
                    emptyBtn.Visible = true;
                    submitBtn.Visible = true;
                    label1.ForeColor= Color.FromArgb(255,0,0);
                    label3.ForeColor = Color.FromArgb(255, 0, 0);
                    label4.ForeColor = Color.FromArgb(255, 0, 0);
                    this.Text = "新增";
                    break;

                case 1://修改界面
                    emptyBtn.Visible = false;
                    submitBtn.Visible = false;
                    this.Text = "修改";
                    //修改界面需要将原始信息传送到文本框 设置客户代码和商品外码不可修改
                    txtCstcode.Text = GenGoodRecordInfo.Cstcode;
                    txtCstcode.ReadOnly = true;
                    txtGengoods.Text = GenGoodRecordInfo.Gengoods;
                    txtGengoods.ReadOnly = true;
                    txtGenspec.Text = GenGoodRecordInfo.Genspec;
                    txtRecordmark.Text = GenGoodRecordInfo.Recordmark;
                    txtGenproducer.Text = GenGoodRecordInfo.Genproducer;
                    txtGoodbar.Text = GenGoodRecordInfo.Goodbar;
                    txtRatifier.Text = GenGoodRecordInfo.Ratifier;
                    break;
            }
        }
        //新增 提交
        private void submitBtn_Click(object sender, EventArgs e)
        {
            //判断客户代码，商品外码，备案原因 不能为空
            if (String.IsNullOrEmpty(txtCstcode.Text))
            {
                MessageBox.Show("前台提示||请输入客户代码！");
                txtCstcode.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtGengoods.Text))
            {
                MessageBox.Show("前台提示||请输入商品外码！");
                txtGengoods.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtRecordmark.Text))
            {
                MessageBox.Show("前台提示||请输入备案原因！");
                txtRecordmark.Focus();
                return;
            }
            dao.AddGenGoodRecord(txtCstcode.Text, txtGengoods.Text, txtGenspec.Text, txtGenproducer.Text, txtGoodbar.Text, txtRatifier.Text, txtRecordmark.Text, retinfo);
            if (retinfo.num == "1")
            {
                MessageBox.Show("新增成功！" + retinfo.msg + "|后台提示");
                return;

            }
            else
            {
                MessageBox.Show("新增失败！" + retinfo.msg + "|后台提示");
                return;
            }

        }

        // 清空
        private void emptyBtn_Click(object sender, EventArgs e)
        {
            txtCstcode.Text = null;
            txtGengoods.Text = null;
            txtGenspec.Text = null;
            txtRecordmark.Text = null;
            txtGenproducer.Text = null;
            txtGoodbar.Text = null;
            txtRatifier.Text = null;
        }

        //修改 备案品种
        private void updateBtn_Click(object sender, EventArgs e)
        {
            //判断备案原因不能为空
            if (String.IsNullOrEmpty(txtRecordmark.Text))
            {
                MessageBox.Show("前台提示||请输入备案原因！");
                txtRecordmark.Focus();
                return;
            }
            //获取参数
            string GoodRecordID = GenGoodRecordInfo.Goodrecordid;
            dao.UpdateGenGoodRecord(GoodRecordID, txtGenspec.Text, txtGenproducer.Text, txtGoodbar.Text, txtRatifier.Text, txtRecordmark.Text, retinfo);

            if (retinfo.num == "1")
            {
                MessageBox.Show("修改成功！" + retinfo.msg + "|后台提示");
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

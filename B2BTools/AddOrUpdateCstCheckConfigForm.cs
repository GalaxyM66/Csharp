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
    public partial class AddOrUpdateCstCheckConfigForm : Form
    {
        public int stateUI = 0;

        string cstid = "";
        public CstCheckConfig cstCheckConfig;
        APDao_B2BTools dao = null;
        PMSystemDao Pdao = new PMSystemDao();
        SPRetInfo retinfo = new SPRetInfo();
        public AddOrUpdateCstCheckConfigForm()
        {
            InitializeComponent();
        }

        private void AddOrUpdateCstCheckConfigForm_Load(object sender, EventArgs e)
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
                    BtnUpdate.Visible = false;
                    BtnAdd.Visible = true;
                    txtCstName.Enabled = false;
                    this.Text = "新增";
                    label1.ForeColor = Color.Red;
                    label2.ForeColor = Color.Red;
                    label3.ForeColor = Color.Red;
                    label4.ForeColor = Color.Red;
                    break;

                case 1://修改界面
                    BtnAdd.Visible = false;
                    BtnUpdate.Visible = true;
                    txtCstCode.Enabled = false;
                    txtCstName.Enabled = false;
                    this.Text = "修改";
                    //修改界面需要将原始信息传送到文本框
                    txtCstCode.Text = cstCheckConfig.CstCode;
                    txtCstName.Text = cstCheckConfig.CstName;
                    txtManuFacture.Text = cstCheckConfig.ManuFacture;
                    txtExtMark.Text = cstCheckConfig.ExtMark;
                    //txtTransRate.Text = GencstgoodInfo.Relatid;
                    break;
            }
        }
        //新增
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string Cstcode = "";
            string ManuFacture = "";
            string ExtMark = "";
            int i = 0;
            if (!StringUtils.IsNull(txtCstCode.Text))
            {
                Cstcode = txtCstCode.Text.ToString().Trim();
            }
            else {
                MessageBox.Show("客户代码为必填项！","前台提示");
                txtCstCode.Focus();
                return;
            }
            if (!StringUtils.IsNull(txtManuFacture.Text))
            {
                ManuFacture = txtManuFacture.Text.ToString().Trim();
                i++;
            }
            else
            {
                ManuFacture = "-1";
            }
            if (!StringUtils.IsNull(txtExtMark.Text))
            {
                ExtMark = txtExtMark.Text.ToString().Trim();
                i++;
            }
            else
            {
                ExtMark = "-1";
            }
            if (i <= 0)
            {
                MessageBox.Show("生产日期和特殊备注不能全为空！", "前台提示");
            }
            else {
                dao.AddCstCheckConfig(cstid, Cstcode, ManuFacture, ExtMark, retinfo);
                if (retinfo.num == "1")
                {
                    MessageBox.Show(retinfo.msg + "|后台提示");
                    return;

                }
                else
                {
                    MessageBox.Show(retinfo.msg + "|后台提示");
                    return;
                }
            }

        }
        //修改
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string ManuFacture = "";
            string ExtMark = "";
            int i = 0;
            if (!StringUtils.IsNull(txtManuFacture.Text))
            {
                ManuFacture = txtManuFacture.Text.ToString().Trim();
                i++;
            } else {
                ManuFacture ="-1";
            }
            if (!StringUtils.IsNull(txtExtMark.Text))
            {
                ExtMark = txtExtMark.Text.ToString().Trim();
                i++;
            }
            else
            {
                ExtMark = "-1";
            }
            if (i <= 0)
            {
                MessageBox.Show("修改框不能全为空！", "前台提示");
            }
            else {
                dao.UpdateCstCheckConfig(cstCheckConfig.Cstid, ManuFacture, ExtMark, retinfo);
                if (retinfo.num == "1")
                {
                    MessageBox.Show(retinfo.msg + "|后台提示");
                    return;

                }
                else
                {
                    MessageBox.Show(retinfo.msg + "|后台提示");
                    return;
                }
            }
        }
        //输入客户代码触发 带出客户名称
        private void txtCstCode_Leave(object sender, EventArgs e)
        {
            string cstcode = txtCstCode.Text.ToString().Trim();
            if (!StringUtils.IsNull(cstcode)) {
                cstid=dao.GetCstName(cstcode, retinfo);
                if (retinfo.num == "1")
                {
                    txtCstName.Text = retinfo.msg;
                    return;

                }
                else
                {
                    txtCstName.Text = retinfo.msg;
                    return;
                }
            }
        }
    }
}

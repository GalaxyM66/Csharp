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
    public partial class AddSysGoodForm : Form
    {
        public string AccountId;
        APDao_B2BTools dao = new APDao_B2BTools();
        List<ConfigGood> goodList = new List<ConfigGood>();
        string goodid = "";
        public AddSysGoodForm()
        {
            InitializeComponent();
            dgvGoods.AutoGenerateColumns = false;
        }

        private void AddSysGoodForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            Dictionary<string, string> sqlkeydicts = new Dictionary<string, string>();
            if (e.KeyChar == 13)
            {
                int i = 0;
                //查询商品
                if (StringUtils.IsNotNull(txtSelGoodId.Text))
                {
                    sqlkeydicts.Add("goods", txtSelGoodId.Text.ToString().Trim());
                    i++;
                }
                if (StringUtils.IsNotNull(txtSelName.Text))
                {
                    sqlkeydicts.Add("name%", "%" + txtSelName.Text.ToString().Trim() + "%");
                    i++;
                }
                if (i <= 0)
                {
                    MessageBox.Show("请至少输入一个查询条件！", "前台提示");
                    return;

                }
                goodList = dao.GetConfigGoods(sqlkeydicts);
                if (goodList.Count <= 0)
                {
                    MessageBox.Show("无相关商品！", "后台提示");
                    dgvGoods.DataSource = null;
                    dgvGoods.Refresh();
                    return;
                }
                else
                {
                    dgvGoods.DataSource = goodList;
                    dgvGoods.Refresh();
                    dgvGoods.CurrentCell = null;
                }
            }           
        }
        //选中
        private void dgvGoods_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ConfigGood info = dgvGoods.CurrentRow.DataBoundItem as ConfigGood;
            txtGoodId.Text = info.Goods;
            goodid = info.GoodId;
            txtName.Text = info.Name;
            txtSpec.Text = info.Spec;
            txtProducer.Text = info.Producer;
        }
        //新增
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (StringUtils.IsNull(goodid))
            {
                MessageBox.Show("商品代码不能为空！", "前台提示");
                txtGoodId.Focus();
                return;
            }

            SPRetInfo retinfo = new SPRetInfo();
            dao.AddGoodsd(AccountId, goodid, retinfo);
            if (retinfo.num == "1")
            {
                MessageBox.Show(retinfo.msg + "|" + retinfo.num, "后台提示！");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(retinfo.msg + "|" + retinfo.num, "后台提示！");
            }

        }
    }
}

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
    public partial class AddConfigForm : Form
    {
        APDao_B2BTools dao = new APDao_B2BTools();
        List<Dept> deptList = new List<Dept>();
        List<ConfigGood> goodList = new List<ConfigGood>();
        string goodid = ""; 
        public AddConfigForm()
        {
            InitializeComponent();
            dgvDeptInfo.AutoGenerateColumns = false;
            dgvGoods.AutoGenerateColumns = false;
        }
        //快捷键查询 仓库
        private void AddConfigForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            deptList.Clear();
            goodList.Clear();
            if (e.KeyChar == 13)
            {
                Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
                Dictionary<string, string> sqlkeydicts = new Dictionary<string, string>();
                //查询仓库
                if (StringUtils.IsNotNull(txtSelDeptCode.Text) || StringUtils.IsNotNull(txtSelDeptName.Text)) {
                    if (StringUtils.IsNotNull(txtSelDeptCode.Text))
                    {
                        sqlkeydict.Add("deptcode", txtSelDeptCode.Text.ToString().Trim());
                    }
                    if (StringUtils.IsNotNull(txtSelDeptName.Text))
                    {
                        sqlkeydict.Add("deptname%", "%" + txtSelDeptName.Text.ToString().Trim() + "%");
                    }
                    deptList = dao.GetDepts(sqlkeydict);
                    if (deptList.Count <= 0)
                    {
                        MessageBox.Show("无相关仓库！", "后台提示");
                        dgvDeptInfo.DataSource = null;
                        dgvDeptInfo.Refresh();
                        return;
                    }
                    else
                    {
                        dgvDeptInfo.DataSource = deptList;
                        dgvDeptInfo.Refresh();
                        dgvDeptInfo.CurrentCell = null;
                    }
                }
                //查询商品
                if (StringUtils.IsNotNull(txtSelGoodId.Text) || StringUtils.IsNotNull(txtSelName.Text))
                {
                    int i = 0;
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
                    if (i<=0) {
                        MessageBox.Show("请至少输入一个商品查询条件","前台提示");
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

        }
        //选中仓库
        private void dgvDeptInfo_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Dept info = dgvDeptInfo.CurrentRow.DataBoundItem as Dept;
            txtDeptName.Text = info.DeptName;
            txtDeptId.Text = info.DeptId;
        }
        //选中商品
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
            if (StringUtils.IsNull(txtDeptId.Text))
            {
                MessageBox.Show("仓库Id不能为空！", "前台提示");
                txtDeptId.Focus();
                return;
            }
            if (StringUtils.IsNull(goodid))
            {
                MessageBox.Show("商品代码不能为空！", "前台提示");
                txtGoodId.Focus();
                return;
            }
            if (StringUtils.IsNull(txtPercent.Text))
            {
                MessageBox.Show("百分比不能为空！", "前台提示");
                txtPercent.Focus();
                return;
            }
            SPRetInfo retinfo = new SPRetInfo();
            dao.AddConfigs(txtDeptId.Text, goodid, txtPercent.Text, retinfo);
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

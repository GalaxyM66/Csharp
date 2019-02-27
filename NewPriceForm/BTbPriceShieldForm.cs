using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PriceManager
{
    public partial class BTbPriceShieldForm : DockContent
    {
        APDao_GoodsClientPrice dao = new APDao_GoodsClientPrice();

        //操作临时表，用于存储过程
        SortableBindingList<DelTemp> DelClientList = new SortableBindingList<DelTemp>();

        List<PriceShield> infoList = new List<PriceShield>();
        public BTbPriceShieldForm()
        {
            InitializeComponent();
        }
        private void clearUI() {

            infoList.Clear();
        }
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            clearUI();
            this.Cursor = Cursors.WaitCursor;
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            int i = 0;
            if (!StringUtils.IsNull(txtProdName.Text)) {
                sqlkeydict.Add("prodname%","%"+txtProdName.Text.ToString().Trim()+"%");
                i++;
            }
            if (!StringUtils.IsNull(txtGoods.Text)) {
                sqlkeydict.Add("goods",txtGoods.Text.ToString().Trim());
                i++;
            }
            //权限控制-采购员
            if (SessionDto.Emproleid=="108") {
                //没填条件
                if (i <= 0)
                {
                    //按登录人的名字查询创建人为本人的数据。
                    infoList = dao.GetPriceShields();
                    if (infoList.Count <= 0)
                    {
                        MessageBox.Show("没有查到数据！", "后台提示");
                        dgvPriceShieldInfo.DataSource = infoList;
                        dgvPriceShieldInfo.Refresh();
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    else
                    {
                        dgvPriceShieldInfo.DataSource = infoList;
                        dgvPriceShieldInfo.Refresh();
                        dgvPriceShieldInfo.CurrentCell = null;
                        this.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    //填了条件
                    infoList=dao.GetPriceShield(sqlkeydict);
                    if (infoList.Count <= 0)
                    {
                        MessageBox.Show("没有查到数据！", "后台提示");
                        dgvPriceShieldInfo.DataSource = infoList;
                        dgvPriceShieldInfo.Refresh();
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    else
                    {
                        dgvPriceShieldInfo.DataSource = infoList;
                        dgvPriceShieldInfo.Refresh();
                        dgvPriceShieldInfo.CurrentCell = null;
                        this.Cursor = Cursors.Default;
                    }

                }

            }
            else {
                //管理员和采购经理
                //没填条件
                if (i <= 0)
                {
                    infoList = dao.GetPriceShields();
                    if (infoList.Count <= 0)
                    {
                        MessageBox.Show("没有查到数据！", "后台提示");
                        dgvPriceShieldInfo.DataSource = infoList;
                        dgvPriceShieldInfo.Refresh();
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    else
                    {
                        dgvPriceShieldInfo.DataSource = infoList;
                        dgvPriceShieldInfo.Refresh();
                        dgvPriceShieldInfo.CurrentCell = null;
                        this.Cursor = Cursors.Default;
                    }
                }
                else
                {
                 //填了条件
                    infoList = dao.GetPriceShield(sqlkeydict);
                    if (infoList.Count <= 0)
                    {
                        MessageBox.Show("没有查到数据！", "后台提示");
                        dgvPriceShieldInfo.DataSource = infoList;
                        dgvPriceShieldInfo.Refresh();
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    else
                    {
                        dgvPriceShieldInfo.DataSource = infoList;
                        dgvPriceShieldInfo.Refresh();
                        dgvPriceShieldInfo.CurrentCell = null;
                        this.Cursor = Cursors.Default;
                    }
                }
            }          
        }
        //全选
        private void BtnSelAll_Click(object sender, EventArgs e)
        {
            if (dgvPriceShieldInfo.RowCount <= 0) return;
            dgvPriceShieldInfo.SelectAll();
        }
        //新增
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddBtbPriceShieldForm adForm = new AddBtbPriceShieldForm();
            adForm.Text = "新增";
            adForm.ShowDialog();
            if (adForm.DialogResult == DialogResult.OK)
            {
                BtnQuery_Click(sender, e);
            }

        }
        //删除
        private void BtnDel_Click(object sender, EventArgs e)
        {
            //取得选中的行
            object obj2 = FormUtils.SelectRows(dgvPriceShieldInfo);
            if (obj2 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            List<PriceShield> list = new List<PriceShield>();

            list.Clear();
            foreach (DataGridViewRow dgvr in row)
            {
                list.Add((PriceShield)dgvr.Cells[0].Value);
            }
            //读取集合中的序号ID
            foreach (PriceShield info in list)
            {
                DelTemp deleteInfo = new DelTemp();
                deleteInfo.RelateId = info.Id;

                DelClientList.Add(deleteInfo);
            }
            SPRetInfo ret = new SPRetInfo();
            //提示确认是否删除
            DialogResult result = MessageBox.Show("确认删除？", "温馨提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                dao.DelPriceShield(DelClientList, ret);
                if (ret.num == "1")
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                }
                else
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                    return;
                }
                DelClientList.Clear();
                BtnQuery_Click(sender, e);
            }
            else
            {
                MessageBox.Show("您选择了取消删除！");
            }

        }
    }
}

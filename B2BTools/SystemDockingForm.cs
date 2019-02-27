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
    public partial class SystemDockingForm : DockContent
    {

        APDao_B2BTools dao = new APDao_B2BTools();
        List<MasterDataInfo> masterDataList = new List<MasterDataInfo>();
        List<DepotInfo> depotList = new List<DepotInfo>();
        List<ConfigInfo> configList = new List<ConfigInfo>();
        List<GoodsInfo> goodList = new List<GoodsInfo>();

        List<DelSystemDock> DelDepotList = new List<DelSystemDock>();

        SPRetInfo ret = new SPRetInfo();


        //全局变量AccountId
        string AccountId = "";
        int rows = -1;

        public SystemDockingForm()
        {
            InitializeComponent();
            dgvMasterData.AutoGenerateColumns = false;
            dgvDepotInfo.AutoGenerateColumns = false;
            dgvGoodInfo.AutoGenerateColumns = false;
            dgvConfigInfo.AutoGenerateColumns = false;
        }
        //清洗数据
        void clearUI() {
            masterDataList.Clear();
        }
        //界面初始化加载
        private void SystemDockingForm_Load(object sender, EventArgs e)
        {
            clearUI();
            masterDataList=dao.GetMasterData();
            if (masterDataList.Count <= 0)
            {
                MessageBox.Show("无相关库存信息！", "后台提示");
                dgvMasterData.DataSource = null;
                dgvMasterData.Refresh();
                return;
            }
            else {
                dgvMasterData.DataSource = masterDataList;
                dgvMasterData.Refresh();
                dgvMasterData.CurrentCell = null;
            }         
        }
        //双击主数据
        private void dgvMasterData_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex!=2&& e.ColumnIndex != 3) {
                dgvMasterData.Columns[e.ColumnIndex].ReadOnly = true;
                return;
            }
            //触发查询仓库信息
            if (e.ColumnIndex==2 && e.RowIndex != -1) {
                depotList.Clear();
                MasterDataInfo info = dgvMasterData.CurrentRow.DataBoundItem as MasterDataInfo;
                AccountId = info.AccountId;
                depotList=dao.GetDepotInfo();
                if (depotList.Count <= 0)
                {
                    MessageBox.Show("无相关仓库信息！", "后台提示");
                    dgvDepotInfo.DataSource = null;
                    dgvDepotInfo.Refresh();
                    return;
                }
                else
                {
                    dgvDepotInfo.DataSource = depotList;
                    dgvDepotInfo.Refresh();
                    dgvDepotInfo.CurrentCell = null;
                }
            }
            //触发修改平台百分比
            if (e.ColumnIndex==3 && e.RowIndex != -1) {
                dgvMasterData.Columns[e.ColumnIndex].ReadOnly = false;
                DataGridViewCell cell = dgvMasterData.Rows[e.RowIndex].Cells["Column4"];
                dgvMasterData.CurrentCell = cell;
                dgvMasterData.BeginEdit(true);
            }

        }
        //主数据修改完成
        private void dgvMasterData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            MasterDataInfo info = dgvMasterData.CurrentRow.DataBoundItem as MasterDataInfo;
            //平台库存百分比
            if (e.ColumnIndex == 3 && e.RowIndex != -1)
            {
                string accountPercent = info.AccountPercent;
                if (StringUtils.IsNull(info.AccountPercent)) {
                    accountPercent = "-1";
                }
                dao.UpdateAccountPercent(accountPercent,info.AccountId,ret);
                if (ret.num != "1")
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                }
                return;
            }
        }
        //特殊配置查询
        private void BtnSel_Click(object sender, EventArgs e)
        {
            configList.Clear();
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            this.Cursor = Cursors.WaitCursor;
            if (StringUtils.IsNotNull(txtGoods.Text)) {
                sqlkeydict.Add("goods",txtGoods.Text.ToString().Trim());
            }
            if (StringUtils.IsNotNull(txtName.Text))
            {
                sqlkeydict.Add("name%", "%"+txtName.Text.ToString().Trim()+"%");
            }
            if (StringUtils.IsNotNull(txtProducer.Text))
            {
                sqlkeydict.Add("producer%", "%" + txtProducer.Text.ToString().Trim() + "%");
            }
            configList=dao.GetConfigInfo(sqlkeydict);
            if (configList.Count <= 0)
            {
                MessageBox.Show("无相关特殊配置信息！", "后台提示");
                dgvConfigInfo.DataSource = configList;
                dgvConfigInfo.Refresh();
                this.Cursor = Cursors.Default;
                return;
            }
            else
            {
                dgvConfigInfo.DataSource = configList;
                dgvConfigInfo.Refresh();
                dgvConfigInfo.CurrentCell = null;
                this.Cursor = Cursors.Default;
            }
        }
        //商品信息查询
        private void BtnSelGood_Click(object sender, EventArgs e)
        {
            goodList.Clear();
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            this.Cursor = Cursors.WaitCursor;
            if (StringUtils.IsNotNull(txtGoodsInfo.Text))
            {
                sqlkeydict.Add("goods", txtGoodsInfo.Text.ToString().Trim());
            }
            if (StringUtils.IsNotNull(txtNameInfo.Text))
            {
                sqlkeydict.Add("name%", "%" + txtNameInfo.Text.ToString().Trim() + "%");
            }
            if (StringUtils.IsNotNull(txtProducerInfo.Text))
            {
                sqlkeydict.Add("producer%", "%" + txtProducerInfo.Text.ToString().Trim() + "%");
            }
            goodList = dao.GetGoodsInfo(sqlkeydict,AccountId);
            if (goodList.Count <= 0)
            {
                MessageBox.Show("无相关商品信息！", "后台提示");
                dgvGoodInfo.DataSource = goodList;
                dgvGoodInfo.Refresh();
                this.Cursor = Cursors.Default;
                return;
            }
            else
            {
                dgvGoodInfo.DataSource = goodList;
                dgvGoodInfo.Refresh();
                dgvGoodInfo.CurrentCell = null;
                this.Cursor = Cursors.Default;
            }

        }
        //新增仓库
        private void BtnAddDepot_Click(object sender, EventArgs e)
        {
            AddDepotForm addDepot = new AddDepotForm();
            addDepot.Tag = dao;
            addDepot.ShowDialog();
            if (addDepot.DialogResult == DialogResult.OK)
            {
                //再次查询仓库信息
                depotList.Clear();
                depotList = dao.GetDepotInfo();
                if (depotList.Count <= 0)
                {
                    MessageBox.Show("无相关仓库信息！", "后台提示");
                    dgvDepotInfo.DataSource = depotList;
                    dgvDepotInfo.Refresh();
                    return;
                }
                else
                {
                    dgvDepotInfo.DataSource = depotList;
                    dgvDepotInfo.Refresh();
                    dgvDepotInfo.CurrentCell = null;
                }
            }

        }
        //删除仓库
        private void BtnDelDepot_Click(object sender, EventArgs e)
        {
            // 取得选中的行
            DepotInfo info = dgvDepotInfo.CurrentRow.DataBoundItem as DepotInfo;
            //提示确认是否删除
            DialogResult result = MessageBox.Show("确认删除？", "温馨提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                dao.DelDepot(info, ret);
                if (ret.num == "1")
                {
                    MessageBox.Show(ret.msg + "|" + ret.result, "后台提示！");
                }
                else
                {
                    MessageBox.Show(ret.msg + "|" + ret.result, "后台提示！");
                    return;
                }
                //再次查询仓库信息
                depotList.Clear();
                depotList = dao.GetDepotInfo();
                if (depotList.Count <= 0)
                {
                    MessageBox.Show("无相关仓库信息！", "后台提示");
                    dgvDepotInfo.DataSource = depotList;
                    dgvDepotInfo.Refresh();
                    return;
                }
                else
                {
                    dgvDepotInfo.DataSource = depotList;
                    dgvDepotInfo.Refresh();
                    dgvDepotInfo.CurrentCell = null;
                }
            }
            else
            {
                MessageBox.Show("您选择了取消删除！");
            }
        }

        //双击修改仓库百分比
        private void dgvDepotInfo_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 4)
            {
                dgvDepotInfo.Columns[e.ColumnIndex].ReadOnly = true;
                return;
            }
            //触发修改平台百分比
            if (e.ColumnIndex == 4 && e.RowIndex != -1)
            {
                dgvDepotInfo.Columns[e.ColumnIndex].ReadOnly = false;
                DataGridViewCell cell = dgvDepotInfo.Rows[e.RowIndex].Cells["Column20"];
                dgvDepotInfo.CurrentCell = cell;
                dgvDepotInfo.BeginEdit(true);
            }


        }
        //修改完成触发
        private void dgvDepotInfo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DepotInfo info = dgvDepotInfo.CurrentRow.DataBoundItem as DepotInfo;
            //平台库存百分比
            if (e.ColumnIndex == 4 && e.RowIndex != -1)
            {
                string Percent = info.Percent;
                if (StringUtils.IsNull(Percent))
                {
                    Percent = "-1";
                }
                dao.UpdateDepotPercent(info.StorageId, Percent, ret);
                if (ret.num != "1")
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                }
                return;
            }
        }


        //特殊配置删除
        private void BtnDel_Click(object sender, EventArgs e)
        {
            //取得选中的行
            object obj2 = FormUtils.SelectRows(dgvConfigInfo);
            if (obj2 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            List<ConfigInfo> configDelList = new List<ConfigInfo>();

            configDelList.Clear();
            foreach (DataGridViewRow dgvr in row)
            {
                configDelList.Add((ConfigInfo)dgvr.Cells[0].Value);
            }
            //读取GencstgoodList集合中的Relatid
            foreach (ConfigInfo info in configDelList)
            {
                DelSystemDock deleteInfo = new DelSystemDock();
                deleteInfo.Relatid = int.Parse(info.SpecialId);

                DelDepotList.Add(deleteInfo);
            }
            //提示确认是否删除
            DialogResult result = MessageBox.Show("确认删除？", "温馨提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                dao.DeleteConfig(DelDepotList, ret);
                if (ret.num == "1")
                {
                    MessageBox.Show(ret.msg + "|" + ret.result, "后台提示！");
                }
                else
                {
                    MessageBox.Show(ret.msg + "|" + ret.result, "后台提示！");
                    return;
                }
                DelDepotList.Clear();
                BtnSel_Click(sender, e);
            }
            else
            {
                MessageBox.Show("您选择了取消删除！");
            }
        }
        //特殊配置 新增
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddConfigForm addConfig = new AddConfigForm();
            addConfig.Tag = dao;
            addConfig.ShowDialog();
            if (addConfig.DialogResult == DialogResult.OK)
            {
                //再次查询特殊配置
                BtnSel_Click(sender,e);
            }
        }
        //删除商品
        private void BtnDelGood_Click(object sender, EventArgs e)
        {
            //取得选中的行
            object obj2 = FormUtils.SelectRows(dgvGoodInfo);
            if (obj2 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            List<GoodsInfo> goodDelList = new List<GoodsInfo>();

            goodDelList.Clear();
            foreach (DataGridViewRow dgvr in row)
            {
                goodDelList.Add((GoodsInfo)dgvr.Cells[0].Value);
            }
            //读取GencstgoodList集合中的Relatid
            foreach (GoodsInfo info in goodDelList)
            {
                DelSystemDock deleteInfo = new DelSystemDock();
                deleteInfo.Relatid = int.Parse(info.GoodId);

                DelDepotList.Add(deleteInfo);
            }
            //提示确认是否删除
            DialogResult result = MessageBox.Show("确认删除？", "温馨提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                dao.DeleteGoodsd(DelDepotList,AccountId, ret);
                if (ret.num == "1")
                {
                    MessageBox.Show(ret.msg + "|" + ret.result, "后台提示！");
                }
                else
                {
                    MessageBox.Show(ret.msg + "|" + ret.result, "后台提示！");
                    return;
                }
                DelDepotList.Clear();
                BtnSelGood_Click(sender, e);
            }
            else
            {
                MessageBox.Show("您选择了取消删除！");
            }
        }
        //新增商品
        private void BtnAddGood_Click(object sender, EventArgs e)
        {
            AddSysGoodForm addGood = new AddSysGoodForm();
            addGood.Tag = dao;
            addGood.AccountId = AccountId;
            addGood.ShowDialog();
            if (addGood.DialogResult == DialogResult.OK)
            {
                //再次查询   商品          
                BtnSelGood_Click(sender, e);
            }
        }
        
        //Excel导入
        private void BtnExcelImport_Click(object sender, EventArgs e)
        {
            SysDockForXlsForm importXsltEmpForm = new SysDockForXlsForm();
            importXsltEmpForm.AccountId = AccountId;
            importXsltEmpForm.Tag = dao;
            importXsltEmpForm.ShowDialog();
        }
        //快捷键 修改特殊配置的百分比
        private void SystemDockingForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==13) {
                ConfigInfo info = dgvConfigInfo.CurrentRow.DataBoundItem as ConfigInfo;
                if (rows!=-1) {
                    string Percent = txtPercent.Text.ToString().Trim();
                    if (StringUtils.IsNull(Percent))
                    {
                        Percent = "-1";
                    }
                    dao.UpdateConfigPercent(info.SpecialId, Percent, ret);
                    if (ret.num != "1")
                    {
                        MessageBox.Show(ret.msg, "后台提示！");
                    }
                    else
                    {
                        MessageBox.Show(ret.msg, "后台提示！");
                        this.dgvConfigInfo.Rows[rows].Cells["Column22"].Value = Percent;
                        txtPercent.Text = "";
                    }
                }
               

            }
        }
        //选中特殊配置某行
        private void dgvConfigInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rows = e.RowIndex;
        }

        private void BtnUpDatePercent_Click(object sender, EventArgs e)
        {
            ConfigInfo info = dgvConfigInfo.CurrentRow.DataBoundItem as ConfigInfo;
            if (rows != -1)
            {
                string Percent = txtPercent.Text.ToString().Trim();
                if (StringUtils.IsNull(Percent))
                {
                    Percent = "-1";
                }
                dao.UpdateConfigPercent(info.SpecialId, Percent, ret);
                if (ret.num != "1")
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                }
                else
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                    this.dgvConfigInfo.Rows[rows].Cells["Column22"].Value = Percent;
                    txtPercent.Text = "";
                }
            }
        }


    }
}

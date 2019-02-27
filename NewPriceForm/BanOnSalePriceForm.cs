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
    public partial class BanOnSalePriceForm : DockContent
    {
        public BanOnSalePriceForm()
        {
            InitializeComponent();
        }

        NameValue[] SaleflagList ={new NameValue("限销","00"),
                                    new NameValue("禁销","99"),
                                    new NameValue("-请选择-","")
                                  };
        //限禁销渠道维护查询
        SortableBindingList<ScmPriceForbidsale>ScmPriceForbidsaleList = new SortableBindingList<ScmPriceForbidsale>();
        //查询商品
        SortableBindingList<SelWaredict>SelWaredictList = new SortableBindingList<SelWaredict>();
        //查询客户
        SortableBindingList<SelClientsGroup>SelClientsGroupList = new SortableBindingList<SelClientsGroup>();
        //操作临时表,用于存储过程
        SortableBindingList<ScmPriceForbidsaleTemp>ScmPriceForbidsaleTempList = new SortableBindingList<ScmPriceForbidsaleTemp>();
        //SortableBindingList<>List = new SortableBindingList<?>();
        APDao_ClientGroup dao = new APDao_ClientGroup();

        //载入窗体
        private void BanOnSalePriceForm_Load(object sender, EventArgs e)
        {
            dgvBanOnSaleClients.AutoGenerateColumns = false;
            dgvBanOnsaleGoods.AutoGenerateColumns = false;
            dgvBanOnSalePriceH.AutoGenerateColumns = false;

            cbSaleflag.DataSource = SaleflagList;
            cbSaleflag.DisplayMember = "Name";
            cbSaleflag.ValueMember = "Value";
            if (PubOwnerConfigureDto.BanOnSale == "0")
            {
                cbSaleflag.SelectedValue = "99";
                cbSaleflag.Visible = false;
                label7.Visible = false;
            }
            else if (PubOwnerConfigureDto.BanOnSale == "1")
            {
                cbSaleflag.SelectedValue = "00";
                cbSaleflag.Visible = false;
                label7.Visible = false;
            }
            else
            {
                cbSaleflag.Text = "-请选择-";
                cbSaleflag.Visible = true;
                label7.Visible = true;
            }
            initUI(2);

        }

        //UI锁
        private void initUI(int astate)
        {
            int uiState = astate;
            switch (uiState)
            {
                case 0://主框重置
                    txtCstcode.Text = "";
                    txtFCstname.Text = "";
                    txtFHdrcode.Text = "";
                    txtFHdrname.Text = "";
                    txtFName.Text = "";
                    txtFProducer.Text = "";
                    txtFRegion.Text = "";
                    txtGoods.Text = "";
                    if (dgvBanOnSalePriceH.RowCount > 0)
                    {
                        dgvBanOnSalePriceH.CurrentCell = null;
                    }

                    break;
                case 1://新增
                    txtACsts.Enabled = true;
                    txtAGoods.Enabled = true;
                    dtpEndtime.Enabled = true;
                    dtpStarttime.Enabled = true;

                    break;
                case 2://锁定
                    txtACsts.Enabled = false;
                    txtAGoods.Enabled = false;
                    dtpEndtime.Enabled = false;
                    dtpStarttime.Enabled = false;

                    break;

                case 3://新增框重置
                    txtACsts.Text = "";
                    txtAGoods.Text = "";
                    SelWaredictList.Clear();
                    SelClientsGroupList.Clear();
                    dgvBanOnSaleClients.DataSource = SelClientsGroupList;
                    dgvBanOnSaleClients.Refresh();
                    dgvBanOnsaleGoods.DataSource = SelWaredictList;
                    dgvBanOnsaleGoods.Refresh();
                   
                    break;

                case 4://新增结束
                   
                    break;
            }
        }

        //主表查询按钮
        private void btnASelect_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(txtCstcode.Text) & string.IsNullOrEmpty(txtGoods.Text))
            {
                MessageBox.Show("请至少输入一个查询条件！", "系统提示！");
                return;
            }
            if (!string.IsNullOrEmpty(txtGoods.Text))
            {
                sqlkeydict.Add("goods%", "%" + txtGoods.Text + "%");
            }
            if (!string.IsNullOrEmpty(txtCstcode.Text))
            {
                sqlkeydict.Add("cstcode%", "%" + txtCstcode.Text + "%");
            }
            ScmPriceForbidsaleList = dao.GetScmPriceForbidsaleList(sqlkeydict);
            if (ScmPriceForbidsaleList.Count <= 0)
            {
                MessageBox.Show("未查询到数据！", "系统提示！");
            }
            dgvBanOnSalePriceH.DataSource = ScmPriceForbidsaleList;
            dgvBanOnSalePriceH.Refresh();
            dgvBanOnSalePriceH.CurrentCell = null;


        }

        //筛选功能
        private void btnAFilter_Click(object sender, EventArgs e)
        {
            var q = from bill in ScmPriceForbidsaleList
                    select bill;
            if (!string.IsNullOrEmpty(txtFCstname.Text))
            {
                q = q.Where(p => p.Cstname.Contains(txtFCstname.Text));
            }
            if (!string.IsNullOrEmpty(txtFHdrcode.Text))
            {
                q = q.Where(p => p.Hdrcode.Contains(txtFHdrcode.Text));
            }
            if (!string.IsNullOrEmpty(txtFHdrname.Text))
            {
                q = q.Where(p => p.Hdrname.Contains(txtFHdrname.Text));
            }
            if (!string.IsNullOrEmpty(txtFName.Text))
            {
                q = q.Where(p => p.Name.Contains(txtFName.Text));
            }
            if (!string.IsNullOrEmpty(txtFProducer.Text))
            {
                q = q.Where(p => p.Producer.Contains(txtFProducer.Text));
            }
            if (!string.IsNullOrEmpty(txtFRegion.Text))
            {
                q = q.Where(p => p.Region.Contains(txtFRegion.Text));
            }
            if (!string.IsNullOrEmpty(txtClienttypename.Text))
            {
                q = q.Where(p => p.Clienttypename.Contains(txtClienttypename.Text));
            }
            //q.ToList();

            dgvBanOnSalePriceH.DataSource = new SortableBindingList<ScmPriceForbidsale>(q.ToList());
            dgvBanOnSalePriceH.Refresh();
            dgvBanOnSalePriceH.CurrentCell = null;

        }

        private void btnAReset_Click(object sender, EventArgs e)
        {
            initUI(0);
        }

        private void btnADelect_Click(object sender, EventArgs e)
        {
            if (dgvBanOnSalePriceH.RowCount <= 0) return;
            object Prcobj2 = FormUtils.SelectRows(dgvBanOnSalePriceH);
            if (Prcobj2 == null) return;
            SortableBindingList<ScmPriceForbidsale> TempPrcList = new SortableBindingList<ScmPriceForbidsale>();
            DataGridViewSelectedRowCollection Prcrow = (DataGridViewSelectedRowCollection)Prcobj2;
            //临时表
            foreach (DataGridViewRow dgvr in Prcrow)
            {
                TempPrcList.Add((ScmPriceForbidsale)dgvr.Cells[0].Value);
            }

            foreach (ScmPriceForbidsale info in TempPrcList)
            {
                ScmPriceForbidsaleTemp DeleteInfo = new ScmPriceForbidsaleTemp();
                DeleteInfo.Cstid = info.Cstid;
                DeleteInfo.Goodid = info.Goodid;
                DeleteInfo.Saleflag = info.Saleflag;
                DeleteInfo.Operatetype = "1";
                DeleteInfo.Begindate = info.Begindate;
                DeleteInfo.Enddate = info.Enddate;
                DeleteInfo.Source = "01";

                ScmPriceForbidsaleTempList.Add(DeleteInfo);
            }
            SPRetInfo ret = new SPRetInfo();
            dao.PPrcForbitsaleInf(ScmPriceForbidsaleTempList, ret);
            if (ret.num == "1")
            {
                MessageBox.Show("提交成功！" + ret.msg + "|" + ret.result, "后台提示！");
            }
            else
            {
                MessageBox.Show("提交失败！" + ret.msg + "|" + ret.result, "后台提示！");
                return;
            }
            ScmPriceForbidsaleTempList.Clear();

            btnASelect_Click(sender, e);
        }

        //新增按钮
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            initUI(1);

            dtpStarttime.MinDate = DateTime.Today;
            dtpEndtime.MinDate = dtpStarttime.Value.AddDays(1);
        }

        //新增保存按钮
        private void btnASave_Click(object sender, EventArgs e)
        {
            if (cbSaleflag.Text == "-请选择-")
            {
                MessageBox.Show("未选择限禁销渠道配置！", "系统提示！");
                return;
            }
            if (dgvBanOnSaleClients.RowCount != 0 & dgvBanOnsaleGoods.RowCount != 0)
            {
                foreach (SelWaredict Goodsinfo in SelWaredictList)
                {
                    foreach (SelClientsGroup Clientsinfo in SelClientsGroupList)
                    {
                        ScmPriceForbidsaleTemp Tempinfo = new ScmPriceForbidsaleTemp();

                        Tempinfo.Cstid = Clientsinfo.Cstid;
                        Tempinfo.Goodid = Goodsinfo.Goodid;
                        Tempinfo.Saleflag = cbSaleflag.SelectedValue.ToString();
                        Tempinfo.Operatetype = "2";
                        Tempinfo.Begindate = dtpStarttime.Value.ToString("yyyy-MM-dd");
                        Tempinfo.Enddate = dtpEndtime.Value.ToString("yyyy-MM-dd");
                        Tempinfo.Source = "01";
                        Tempinfo.Mark = txtMark.Text;

                        ScmPriceForbidsaleTempList.Add(Tempinfo);
                    }
                }
                SPRetInfo ret = new SPRetInfo();
                dao.PPrcForbitsaleInf(ScmPriceForbidsaleTempList, ret);
                if (ret.num == "1")
                {
                    MessageBox.Show("提交成功！" + ret.msg + "|" + ret.result, "后台提示！");
                    initUI(2);
                }
                else
                {
                    MessageBox.Show("提交失败！" + ret.msg + "|" + ret.result, "后台提示！");
                    return;
                }
                ScmPriceForbidsaleTempList.Clear();
                SelWaredictList.Clear();
                SelClientsGroupList.Clear();
                dgvBanOnsaleGoods.DataSource = SelWaredictList;
                dgvBanOnsaleGoods.Refresh();
                dgvBanOnSaleClients.DataSource = SelClientsGroupList;
                dgvBanOnSaleClients.Refresh();

            }
            else
            {
                MessageBox.Show("未录入商品或客户信息！请确认！", "系统提示！");
                return;
            }
        }

        private void dtpStarttime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dtpEndtime.MinDate = dtpStarttime.Value.AddDays(1);
                //dtpEndtime.MaxDate = dtpStarttime.Value.AddDays(30);
                //dtpEndtime.Text = dtpStarttime.Value.AddDays(1).ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                dtpEndtime.MinDate = dtpStarttime.Value.AddDays(1);
                //dtpEndtime.MaxDate = dtpStarttime.Value.AddDays(30);
                //dtpEndtime.Text = dtpStarttime.Value.AddDays(1).ToString("yyyy-MM-dd");
            }
        }

        //新增框重置
        private void btnNReset_Click(object sender, EventArgs e)
        {
            initUI(3);
        }

        //新增商品
        private void txtAGoods_Click(object sender, EventArgs e)
        {
            AddClientAndGoodsForm addForm = new AddClientAndGoodsForm();
            addForm.IntUI = 1;
            addForm.OldSelWaredictList = SelWaredictList;
            addForm.ShowDialog();

            SortableBindingList<SelWaredict> TempList = new SortableBindingList<SelWaredict>();
            if (addForm.DialogResult == DialogResult.OK)
            {
                TempList = addForm.SelGoodsList;

               
                    foreach (SelWaredict Tempinfo in TempList)
                    {
                        SelWaredictList.Add(Tempinfo);
                    }
                    dgvBanOnsaleGoods.DataSource = SelWaredictList;
                    dgvBanOnsaleGoods.Refresh();
            }
        }

        //新增客户
        private void txtACsts_Click(object sender, EventArgs e)
        {
            AddClientAndGoodsForm addForm = new AddClientAndGoodsForm();
            addForm.IntUI = 2;
            addForm.OldSelClientsGroupList = SelClientsGroupList;
            addForm.ShowDialog();

            SortableBindingList<SelClientsGroup> TempList = new SortableBindingList<SelClientsGroup>();
            if (addForm.DialogResult == DialogResult.OK)
            {
                TempList = addForm.SelClientsList;


                foreach (SelClientsGroup Tempinfo in TempList)
                {
                    SelClientsGroupList.Add(Tempinfo);
                }
                dgvBanOnSaleClients.DataSource = SelClientsGroupList;
                dgvBanOnSaleClients.Refresh();
            }
        }
    }
}

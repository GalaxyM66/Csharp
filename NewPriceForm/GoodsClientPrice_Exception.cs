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
    public partial class GoodsClientPrice_Exception : DockContent
    {
        APDao_GoodsClientPrice dao = new APDao_GoodsClientPrice();
        private SortableBindingList<ScmPriceExeWait> ScmPriceExeWaitList = new SortableBindingList<ScmPriceExeWait>();
        private SortableBindingList<ScmPriceExeWait> UnScmPriceExeWaitList = new SortableBindingList<ScmPriceExeWait>();
        private SortableBindingList<ScmPriceExeWait> ScmPriceExeWaitOKList = new SortableBindingList<ScmPriceExeWait>();
        private SortableBindingList<ScmPriceExeWait> ScmPriceExeWaitCancelList = new SortableBindingList<ScmPriceExeWait>();
        private ScmPriceExeWait SelScmPriceExeWait = new ScmPriceExeWait();

        private SortableBindingList<PubEmpBuyer> PubEmpBuyerList = new SortableBindingList<PubEmpBuyer>();//采购人员
        public GoodsClientPrice_Exception()
        {
            InitializeComponent();
            dgvPriceExeWait.AutoGenerateColumns = false;
            dgvPriceExeWaitOK.AutoGenerateColumns = false;
            dgvPriceExeWaitCancel.AutoGenerateColumns = false;
        }

        private void GoodsClientPrice_Exception_Load(object sender, EventArgs e)
        {
            PubEmpBuyerList = dao.GetPubEmpBuyerList();

            PubEmpBuyer info = new PubEmpBuyer();
            info.Empcode = "ALL";
            info.Empname = "-全部-";
            PubEmpBuyerList.Add(info);

            cbBuyername.DataSource = PubEmpBuyerList;
            cbBuyername.DisplayMember = "Empname";
            cbBuyername.ValueMember = "Empcode";
            cbBuyername.Text = "-全部-";

            if ("105".Equals(SessionDto.Emproleid) || "99".Equals(SessionDto.Emproleid))
            { 
                cbBuyername.Enabled = true;
            }
            else {
                cbBuyername.Text = SessionDto.Empname;
                cbBuyername.Enabled = false;
            }
        }
        private void Clear() {
            ScmPriceExeWaitList.Clear();
            dgvPriceExeWait.DataSource = ScmPriceExeWaitList;
            dgvPriceExeWait.Refresh();
            ScmPriceExeWaitOKList.Clear();
            dgvPriceExeWaitOK.DataSource = ScmPriceExeWaitOKList;
            dgvPriceExeWaitOK.Refresh();
            ScmPriceExeWaitCancelList.Clear();
            dgvPriceExeWaitCancel.DataSource = ScmPriceExeWaitCancelList;
            dgvPriceExeWaitCancel.Refresh();
        }
        private void queryBtn_Click(object sender, EventArgs e)
        {
            Clear();
            this.Cursor = Cursors.WaitCursor;
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            if (!StringUtils.IsNull(txtClientCode.Text))
            {
                sqlkeydict.Add("cstcode%", "%" + txtClientCode.Text.ToString()+"%");
            }
            if (!StringUtils.IsNull(txtGoods.Text))
            {
                sqlkeydict.Add("goods%", "%" + txtGoods.Text.ToString() + "%");
            }
            if (!StringUtils.IsNull(txtClientName.Text))
            {
                sqlkeydict.Add("cstname%", "%" + txtClientName.Text.ToString() + "%");
            }
            if (!StringUtils.IsNull(txtRegion.Text))
            {
                sqlkeydict.Add("region%", "%" + txtRegion.Text.ToString() + "%");
            }
            if (!StringUtils.IsNull(txtClientGroupCode.Text))
            {
                sqlkeydict.Add("hdrcode%", "%" + txtClientGroupCode.Text.ToString() + "%");
            }
            if (!StringUtils.IsNull(txtClientGroupName.Text))
            {
                sqlkeydict.Add("hdrname%", "%" + txtClientGroupName.Text.ToString() + "%");
            }
            if (!StringUtils.IsNull(txtGoodsName.Text))
            {
                sqlkeydict.Add("goodsname%", "%" + txtGoodsName.Text.ToString() + "%");
            }
            if (!StringUtils.IsNull(txtProducer.Text))
            {
                sqlkeydict.Add("producer%", "%" + txtProducer.Text.ToString() + "%");
            }
            if (!cbBuyername.SelectedValue.ToString().Equals("ALL"))
            {
                sqlkeydict.Add("buyercode", cbBuyername.SelectedValue.ToString());
            }
            ScmPriceExeWaitList = dao.GetScmPriceExeWaitList(sqlkeydict);
            dgvPriceExeWait.DataSource = ScmPriceExeWaitList;
            dgvPriceExeWait.Refresh();
            dgvPriceExeWait.CurrentCell = null;
            this.Cursor = Cursors.Default;
        }



        

        private void dgvPriceExeWait_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewSelectedRowCollection row = dgvPriceExeWait.SelectedRows;
            SelScmPriceExeWait = (ScmPriceExeWait)(row[0].Cells[0].Value);

        }


        private void BtnAdd_Click(object sender, EventArgs e)
        {
            
            if (SelScmPriceExeWait.Id == null||SelScmPriceExeWait.Id == "") {
                MessageBox.Show("请选择要处理的数据！","程序提示");
                return;
            }
            ScmPriceExeWaitOKList.Add(SelScmPriceExeWait);
            
            foreach (ScmPriceExeWait info in ScmPriceExeWaitList)
            {
                //判断客户明细是否已选择，已选择显示标识赋值为99
                if (info.Id == SelScmPriceExeWait.Id)
                {
                    info.UnFlag = "99";
                }
                if (info.Id != SelScmPriceExeWait.Id && info.Compid == SelScmPriceExeWait.Compid && info.Ownerid == SelScmPriceExeWait.Ownerid && info.Saledeptid == SelScmPriceExeWait.Saledeptid && info.Goodid == SelScmPriceExeWait.Goodid && info.Cstid == SelScmPriceExeWait.Cstid)
                {
                    info.UnFlag = "99";
                    ScmPriceExeWaitCancelList.Add(info);
                }

            }
            dgvPriceExeWaitOK.DataSource = ScmPriceExeWaitOKList;
            dgvPriceExeWaitOK.Refresh();

            dgvPriceExeWaitCancel.DataSource = ScmPriceExeWaitCancelList;
            dgvPriceExeWaitCancel.Refresh();

            var UnScmPriceExeWaitList = from info in ScmPriceExeWaitList
                                      where info.UnFlag == "00"
                                      select info;
            dgvPriceExeWait.DataSource = new SortableBindingList<ScmPriceExeWait>(UnScmPriceExeWaitList);
            dgvPriceExeWait.Refresh();
            dgvPriceExeWait.CurrentCell = null;
            SelScmPriceExeWait = new ScmPriceExeWait();
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (SelScmPriceExeWait.Id == null || SelScmPriceExeWait.Id == "")
            {
                MessageBox.Show("请选择要处理的数据！", "程序提示");
                return;
            }
            
            foreach (ScmPriceExeWait info in ScmPriceExeWaitList)
            {
                //判断是否已选择，已选择显示标识赋值为99
                if (info.Compid == SelScmPriceExeWait.Compid && info.Ownerid == SelScmPriceExeWait.Ownerid && info.Saledeptid == SelScmPriceExeWait.Saledeptid && info.Goodid == SelScmPriceExeWait.Goodid && info.Cstid == SelScmPriceExeWait.Cstid)
                {
                    info.UnFlag = "99";
                    ScmPriceExeWaitCancelList.Add(info);
                }

            }
            dgvPriceExeWaitCancel.DataSource = ScmPriceExeWaitCancelList;
            dgvPriceExeWaitCancel.Refresh();

            var UnScmPriceExeWaitList = from info in ScmPriceExeWaitList
                                        where info.UnFlag == "00"
                                        select info;
            dgvPriceExeWait.DataSource = new SortableBindingList<ScmPriceExeWait>(UnScmPriceExeWaitList);
            dgvPriceExeWait.Refresh();
            dgvPriceExeWait.CurrentCell = null;
            SelScmPriceExeWait = new ScmPriceExeWait();
        }

        private void BtnSubmint_Click(object sender, EventArgs e)
        {
            if (ScmPriceExeWaitOKList.Count <= 0 && ScmPriceExeWaitCancelList.Count <= 0) {
                MessageBox.Show("未选择待处理数据，请确认！","程序提示");
                return;
            }
            SPRetInfo retinfo = new SPRetInfo();
            dao.PPrcWaitInf(ScmPriceExeWaitOKList,ScmPriceExeWaitCancelList, retinfo);
            if (retinfo.num == "1")
            {

                MessageBox.Show(retinfo.msg + "|" +retinfo.count+"|"+ retinfo.result, "后台提示");
                Clear();
                return;
            }
            else
            {
                MessageBox.Show(retinfo.msg + "|" + retinfo.count + "|" + retinfo.result, "后台提示");
                return;
            }
        }
    }
}

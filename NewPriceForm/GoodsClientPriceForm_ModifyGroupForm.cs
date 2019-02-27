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
    public partial class GoodsClientPriceForm_ModifyGroupForm : Form
    {
        public SelPubWaredict Waredictinfo = new SelPubWaredict();
        private CstGroupHdr Selclentgroupinfo = new CstGroupHdr();

        public ScmPriceGoodtap PriceGoodtapinfo = new ScmPriceGoodtap();

        APDao_GoodsClientPrice dao = null;
        APDao_ClientGroup CGdao = new APDao_ClientGroup();

        private string Defaultdept = "";
        private ScmPriceGoodtap AddpriceGoodtaped = new ScmPriceGoodtap();

        private SortableBindingList<CstGroupHdr> CstGroupHdrList = new SortableBindingList<CstGroupHdr>();
        private SortableBindingList<ScmPriceGoodtap> ScmPriceGoodtapList = new SortableBindingList<ScmPriceGoodtap>();
        private SortableBindingList<ScmPriceExe> ScmPriceExeList = new SortableBindingList<ScmPriceExe>();

        public GoodsClientPriceForm_ModifyGroupForm()
        {
            InitializeComponent();
            dgvClientGroup.AutoGenerateColumns = false;
            dgvScmPriceGoodtap.AutoGenerateColumns = false;
            dgvPriceExe.AutoGenerateColumns = false;
        }

        private void GoodsClientPriceForm_ModifyGroupForm_Load(object sender, EventArgs e)
        {
            dao = (APDao_GoodsClientPrice)this.Tag;
            txtGoods.Text = Waredictinfo.Goods;
            txtClient.Text = PriceGoodtapinfo.Code;

            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();

            sqlkeydict.Add("hdrid", PriceGoodtapinfo.Hdrid);
            CstGroupHdrList = CGdao.GetCstGroupHdrList(sqlkeydict);
            dgvClientGroup.DataSource = CstGroupHdrList;
            dgvClientGroup.Refresh();
            dgvClientGroup.CurrentCell = null;

            sqlkeydict.Add("goodid", Waredictinfo.Goodid);
            ScmPriceGoodtapList.Add(PriceGoodtapinfo);
            dgvScmPriceGoodtap.DataSource = ScmPriceGoodtapList;
            dgvScmPriceGoodtap.Refresh();
            //dgvScmPriceGoodtap.CurrentCell = null;

            ScmPriceExeList = dao.GetScmPriceExeList(sqlkeydict);
            dgvPriceExe.DataSource = ScmPriceExeList;
            dgvPriceExe.Refresh();
            dgvPriceExe.CurrentCell = null;
        }
        private void BtnModifyClientGroupPrice_Click(object sender, EventArgs e)
        {
            object obj1 = FormUtils.SelectRows(dgvScmPriceGoodtap);
            if (obj1 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj1;
            //selinfo = (CstGroupDtl)dgvr.Cells[0].Value;
            //DataGridViewSelectedRowCollection row = dgvScmPriceGoodtap.SelectedRows;
            foreach (DataGridViewRow dgvr in row)
            {
                AddpriceGoodtaped = (ScmPriceGoodtap)dgvr.Cells[0].Value;
            }

            GoodsClientPriceForm_AddModifyPriceForm addgoodsclientpriceform = new GoodsClientPriceForm_AddModifyPriceForm();
            addgoodsclientpriceform.Tag = dao;
            addgoodsclientpriceform.stateUI = 1;
            addgoodsclientpriceform.Text = "修改客户组定价";
            addgoodsclientpriceform.Waredictinfo = Waredictinfo;
            foreach (CstGroupHdr info in CstGroupHdrList)
            {
                Selclentgroupinfo = info;

            }
            addgoodsclientpriceform.Selclentgroupinfo = Selclentgroupinfo;
            addgoodsclientpriceform.AddpriceGoodtap = AddpriceGoodtaped;
            addgoodsclientpriceform.Defaultdept = PriceGoodtapinfo.Saledeptid;
            Defaultdept = PriceGoodtapinfo.Saledeptid;
            addgoodsclientpriceform.ShowDialog();
            if (addgoodsclientpriceform.DialogResult == DialogResult.OK)
            {
                //定价
                AddpriceGoodtaped = addgoodsclientpriceform.AddpriceGoodtap;
                //拼客户组信息
                ScmPriceGoodtapList.Clear();
                ScmPriceGoodtapList.Add(AddpriceGoodtaped);

                //进行循环拼客户定价数据
                foreach (ScmPriceExe info in ScmPriceExeList)
                {
                    //修改客户定价明细

                    info.Goodid = Waredictinfo.Goodid;
                    info.GoodsName = Waredictinfo.Name;
                    info.GoodsCode = Waredictinfo.Goods;
                    info.Prc = AddpriceGoodtaped.Prc;
                    info.Price = AddpriceGoodtaped.Price;
                    info.Bottomprc = AddpriceGoodtaped.Bottomprc;
                    info.Bottomprice = AddpriceGoodtaped.Bottomprice;
                    info.Costprc = AddpriceGoodtaped.Costprc;
                    info.Costprice = AddpriceGoodtaped.Costprice;
                    info.Begindate = AddpriceGoodtaped.Begindate;
                    info.Enddate = AddpriceGoodtaped.Enddate;
                    info.Source = "08";
                    info.Type = "3";
                    info.Costrate = AddpriceGoodtaped.Costrate;
                    info.Synctype = AddpriceGoodtaped.Synctype;
                    info.Grouptype = AddpriceGoodtaped.Grouptype;
                    info.Stopflag = AddpriceGoodtaped.Stopflag;
                    info.Stopflagname = AddpriceGoodtaped.Stopflagname;
                    info.Goodtapid = AddpriceGoodtaped.Goodtapid;
                }

                dgvPriceExe.DataSource = ScmPriceExeList;
                dgvPriceExe.Refresh();

            }
        }

        private void dgvScmPriceGoodtap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            object obj1 = FormUtils.SelectRows(dgvScmPriceGoodtap);
            if (obj1 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj1;
            //selinfo = (CstGroupDtl)dgvr.Cells[0].Value;
            //DataGridViewSelectedRowCollection row = dgvScmPriceGoodtap.SelectedRows;
            foreach (DataGridViewRow dgvr in row)
            {
                AddpriceGoodtaped = (ScmPriceGoodtap)dgvr.Cells[0].Value;
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (ScmPriceExeList.Count < 1)
            {
                MessageBox.Show("未新增客户组定价的客户明细，请检查！", "程序提示");
                return;
            }
            SPRetInfo retinfo = new SPRetInfo();
            dao.PPrcGoodDtapInf(ScmPriceGoodtapList, ScmPriceExeList, Defaultdept, "2", retinfo);
            if (retinfo.num == "1")
            {
                MessageBox.Show("提交成功！|" + retinfo.msg + "|" + retinfo.count + "|" + retinfo.selflag + "|" + retinfo.result, "后台提示");
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            else
            {
                MessageBox.Show("提交失败！|" + retinfo.msg + "|" + retinfo.count + "|" + retinfo.selflag + "|" + retinfo.result, "后台提示");
                return;
            }
        }








    }
}

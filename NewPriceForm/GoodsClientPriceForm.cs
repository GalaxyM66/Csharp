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
    public partial class GoodsClientPriceForm : DockContent
    {
        APDao_GoodsClientPrice dao = new APDao_GoodsClientPrice();
        SelPubWaredict Waredictinfo = new SelPubWaredict();
        private SortableBindingList<ScmPriceGoodunify> ScmPriceGoodunifyList = new SortableBindingList<ScmPriceGoodunify>();
        private SortableBindingList<ScmPriceGoodtap> ScmPriceGoodtapList = new SortableBindingList<ScmPriceGoodtap>();
        private SortableBindingList<ScmPriceExe> ScmPriceExeList = new SortableBindingList<ScmPriceExe>();
        private SortableBindingList<CstGroupDtl> SelCstGroupDtlList = new SortableBindingList<CstGroupDtl>();
        //选中商品价格
        ScmPriceGoodunify Selpricegoodunfiy = new ScmPriceGoodunify();
        //选中客户组定价
        ScmPriceGoodtap Selpricegoodtap = new ScmPriceGoodtap();
        public GoodsClientPriceForm()
        {
            InitializeComponent();
            dgvPriceGoodunify.AutoGenerateColumns = false;
            dgvPriceGoodtap.AutoGenerateColumns = false;
            dgvPriceExe.AutoGenerateColumns = false;

        }
        //可查询商品信息带出商品代码
        private void txtGoods_Click(object sender, EventArgs e)
        {
            PubWaredictForm pub_waredict = new PubWaredictForm();
            //pub_waredict.Tag = dao;
            pub_waredict.ShowDialog();
            if (pub_waredict.DialogResult == DialogResult.OK)
            {
                Waredictinfo = pub_waredict.waredictinfo;
                txtGoods.Text = Waredictinfo.Goods;
                queryBtn_Click(sender, e);
            }
        }

        private void GoodsClientPriceForm_Load(object sender, EventArgs e)
        {
            txtGoods.Focus();
            BtnAdd.Enabled = false;
            BtnModify.Enabled = false;
            BtnAddClientGroupPrice.Enabled = false;
            BtnModfiyClientGroupPrice.Enabled = false;
        }

        //清空信息
        private void Clear() {
            ScmPriceGoodunifyList.Clear();
            dgvPriceGoodunify.DataSource = ScmPriceGoodunifyList;
            dgvPriceGoodunify.Refresh();
            ScmPriceGoodtapList.Clear();
            dgvPriceGoodtap.DataSource = ScmPriceGoodtapList;
            dgvPriceGoodtap.Refresh();
            ScmPriceExeList.Clear();
            dgvPriceExe.DataSource = ScmPriceExeList;
            dgvPriceExe.Refresh();
        }
        //查询商品统一价
        private void queryBtn_Click(object sender, EventArgs e)
        {
            BtnAdd.Enabled = false;
            BtnModify.Enabled = false;
            BtnAddClientGroupPrice.Enabled = false;
            BtnModfiyClientGroupPrice.Enabled = false;
            Selpricegoodunfiy = new ScmPriceGoodunify();
            if (!StringUtils.IsNull(txtGoods.Text))
            {
                this.Cursor = Cursors.WaitCursor;
                Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
                sqlkeydict.Add("goodid", Waredictinfo.Goodid);
                ScmPriceGoodunifyList = dao.GetScmPriceGoodunifyList(sqlkeydict);
                if (ScmPriceGoodunifyList.Count < 1)
                {
                    Clear();
                    ScmPriceGoodunify info = new ScmPriceGoodunify();
                    info.Goodid = Waredictinfo.Goodid;
                    info.Goods = Waredictinfo.Goods;
                    info.Name = Waredictinfo.Name;
                    info.Spec = Waredictinfo.Spec;
                    info.Producer = Waredictinfo.Producer;
                    info.Iscredit = Waredictinfo.Islimit;
                    info.Outrate=Waredictinfo.Outrate;
                    info.Sprdug=Waredictinfo.Sprdug;
                    ScmPriceGoodunifyList.Add(info);
                    BtnAdd.Enabled = true;
                    BtnAddClientGroupPrice.Enabled = true;
                }

                    BtnModify.Enabled = true;
                    
                    dgvPriceGoodunify.DataSource = ScmPriceGoodunifyList;
                    dgvPriceGoodunify.Refresh();
                    dgvPriceGoodunify.CurrentCell = null;

                    ScmPriceGoodtapList = dao.GetScmPriceGoodtapList(sqlkeydict);

                    BtnAddClientGroupPrice.Enabled = true;

                    dgvPriceGoodtap.DataSource = ScmPriceGoodtapList;
                    dgvPriceGoodtap.Refresh();
                    dgvPriceGoodtap.CurrentCell = null;
                    if (ScmPriceGoodtapList.Count > 1) {
                        BtnModfiyClientGroupPrice.Enabled = true;
                    }

                   
                
                this.Cursor = Cursors.Default;
            }
            else {
                Clear();
                MessageBox.Show("请查询商品！", "程序提示");
                return;
            }
        }

        //新增商品统一价
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddModfiyPriceGoodunifyForm addpriceform = new AddModfiyPriceGoodunifyForm();
            addpriceform.Tag = dao;
            addpriceform.stateUI = 0;
            addpriceform.Waredictinfo = Waredictinfo;
            addpriceform.Text = "新增商品统一价";
            addpriceform.ShowDialog();
            if (addpriceform.DialogResult == DialogResult.OK) {
                queryBtn_Click(sender, e);
            }
        }

        //修改商品统一价
        private void BtnModify_Click(object sender, EventArgs e)
        {
            if (Selpricegoodunfiy.Id == null)
            {
                MessageBox.Show("请选择修改客户组统一价","程序提示");
                return;
            }
                AddModfiyPriceGoodunifyForm modifypriceform = new AddModfiyPriceGoodunifyForm();
                modifypriceform.Tag = dao;
                modifypriceform.stateUI = 1;
                modifypriceform.Waredictinfo = Waredictinfo;
                modifypriceform.Selpricegoodunfiy = Selpricegoodunfiy;
                modifypriceform.Text = "修改商品统一价";
                modifypriceform.ShowDialog();
                if (modifypriceform.DialogResult == DialogResult.OK)
                {
                    queryBtn_Click(sender, e);
                }
        }

        //新增商品客户组定价
        private void BtnAddClientGroupPrice_Click(object sender, EventArgs e)
        {
            GoodsClientPriceForm_AddGroup addgoodsgrouppriceform = new GoodsClientPriceForm_AddGroup();
            addgoodsgrouppriceform.Tag = dao;
            addgoodsgrouppriceform.Waredictinfo = Waredictinfo;//查询的商品信息
            addgoodsgrouppriceform.ScmPriceGoodunifyList = ScmPriceGoodunifyList;//商品统一价
            addgoodsgrouppriceform.Text = "新增商品客户组定价";
            addgoodsgrouppriceform.ShowDialog();
            if (addgoodsgrouppriceform.DialogResult == DialogResult.OK) {
                queryBtn_Click(sender, e);
            }
        }

        //选择商品统一价进行修改
        private void dgvPriceGoodunify_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewSelectedRowCollection row = dgvPriceGoodunify.SelectedRows;
            Selpricegoodunfiy = (ScmPriceGoodunify)(row[0].Cells[0].Value);
            //string infostatus = this.dgvBillhdr.CurrentRow.Cells["dataGridViewTextBoxColumn26"].Value.ToString();
            
        }
        //修改商品客户组定价
        private void BtnModfiyClientGroupPrice_Click(object sender, EventArgs e)
        {
            if (Selpricegoodtap.Hdrid == null) {
                MessageBox.Show("请选择修改客户组定价信息", "程序提示");
                return;
            }
            GoodsClientPriceForm_ModifyGroup modifyGrouppriceform = new GoodsClientPriceForm_ModifyGroup();
            modifyGrouppriceform.Tag = dao;
            //modifyGrouppriceform.stateUI = 1;
            modifyGrouppriceform.Waredictinfo = Waredictinfo;
            modifyGrouppriceform.PriceGoodtapinfo = Selpricegoodtap;
            modifyGrouppriceform.Text = "修改商品客户组定价";
            modifyGrouppriceform.ShowDialog();
            if (modifyGrouppriceform.DialogResult == DialogResult.OK)
            {
                queryBtn_Click(sender, e);
            }
        }

        //选择商品客户组定价信息
        private void dgvPriceGoodtap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewSelectedRowCollection row = dgvPriceGoodtap.SelectedRows;
            Selpricegoodtap = (ScmPriceGoodtap)(row[0].Cells[0].Value);
            if (Selpricegoodtap.Synctype == "00") {
                BtnModfiyClientGroupPrice.Enabled = true;
            }
            //不同步客户组定价不能修改商品客户组定价信息
            if (Selpricegoodtap.Synctype == "10") {
                BtnModfiyClientGroupPrice.Enabled = false;
            }
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            sqlkeydict.Add("goodid", Waredictinfo.Goodid);
            sqlkeydict.Add("hdrid", Selpricegoodtap.Hdrid);

            ScmPriceExeList = dao.GetScmPriceExeList(sqlkeydict);//查询客户组明细
            dgvPriceExe.DataSource = ScmPriceExeList;
            dgvPriceExe.Refresh();
            dgvPriceExe.CurrentCell = null;
           
        }
        //新增客户定价
        private void BtnAddClient_Click(object sender, EventArgs e)
        {
            if (Selpricegoodtap.Hdrid == null)
            {
                MessageBox.Show("请选择修改客户组定价信息", "程序提示");
                return;
            }
            GoodsClientPriceForm_AddClientPrice addclientpriceform = new GoodsClientPriceForm_AddClientPrice();
            addclientpriceform.Tag = dao;
            //modifyGrouppriceform.stateUI = 1;
            addclientpriceform.Waredictinfo = Waredictinfo;
            addclientpriceform.PriceGoodtapinfo = Selpricegoodtap;
            addclientpriceform.Text = "新增商品客户定价";
            addclientpriceform.ShowDialog();
            if (addclientpriceform.DialogResult == DialogResult.OK)
            {
                queryBtn_Click(sender, e);
            }
        }
        //停用
        private void btnStop_Click(object sender, EventArgs e)
        {
            object obj1 = FormUtils.SelectRows(dgvPriceExe);
            if (obj1 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj1;

            SortableBindingList<ScmPriceExe> SelScmPriceExeList = new SortableBindingList<ScmPriceExe>();

            foreach (DataGridViewRow dgvr in row)
            {
                ScmPriceExe selinfo = (ScmPriceExe)dgvr.Cells[0].Value;
                selinfo.Source = "09";
                selinfo.Type = "2";
                SelScmPriceExeList.Add(selinfo);
            }
            if (SelScmPriceExeList.Count < 1)
            {
                MessageBox.Show("请选择客户明细定价数据！", "程序提示");
                return;
            }
            else {
                SPRetInfo retinfo = new SPRetInfo();
                string Defaultdept = Selpricegoodtap.Saledeptid;
                dao.PPrcGoodDtapInf(ScmPriceGoodtapList, SelScmPriceExeList, Defaultdept, "4", retinfo);
                if (retinfo.num == "1")
                {
                    MessageBox.Show("提示成功！"+retinfo.msg + "|" + retinfo.count + "|" + retinfo.selflag + "|" + retinfo.result, "后台提示");
                    queryBtn_Click(sender, e);
                    return;
                }
                else
                {
                    MessageBox.Show("提示失败！" + retinfo.msg + "|" + retinfo.count + "|" + retinfo.selflag + "|" + retinfo.result, "后台提示");
                    return;
                }
            }
        }

        private void txtGoods_KeyDown(object sender, KeyEventArgs e)
        {
            txtGoods_Click(new object(),new EventArgs());
        }
        //修改客户定价
        private void BtnClientPrice_Click(object sender, EventArgs e)
        {
            //获取选中原客户组信息
            object obj2 = FormUtils.SelectRows(dgvPriceGoodtap);
            if (obj2 == null) return;
            DataGridViewSelectedRowCollection row2 = (DataGridViewSelectedRowCollection)obj2;
            ScmPriceGoodtap oldgoodtapinfo = new ScmPriceGoodtap();
            foreach (DataGridViewRow dgvr2 in row2)
            {
                oldgoodtapinfo = (ScmPriceGoodtap)dgvr2.Cells[0].Value;
            }

            object obj1 = FormUtils.SelectRows(dgvPriceExe);
            if (obj1 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj1;

            SortableBindingList<ScmPriceExe> SelScmPriceExeList = new SortableBindingList<ScmPriceExe>();
            SelCstGroupDtlList.Clear();
            foreach (DataGridViewRow dgvr in row)
            {
                ScmPriceExe selinfo = (ScmPriceExe)dgvr.Cells[0].Value;
                selinfo.Source = "16";
                CstGroupDtl info = new CstGroupDtl();
                info.Cstcode = selinfo.Cstcode;
                info.CSTNAME = selinfo.Cstname;
                info.Cstid = selinfo.Cstid;
                info.Region = selinfo.Region;
                info.Compid = selinfo.Compid;
                info.Stopflag = selinfo.Stopflag;
                info.STOPNAME = selinfo.Stopflagname;
                info.UseFlag = "00";
                info.Goodtapid = selinfo.Goodtapid;
                SelCstGroupDtlList.Add(info);
                //SelScmPriceExeList.Add(selinfo);
            }
            if (SelCstGroupDtlList.Count < 1)
            {
                MessageBox.Show("请选择客户明细定价数据！", "程序提示");
                return;
            }
            else {
                GoodsClientPriceForm_ModifyClients modifyclientsform = new GoodsClientPriceForm_ModifyClients();
                modifyclientsform.Tag = dao;
                modifyclientsform.Waredictinfo = Waredictinfo;
                modifyclientsform.Oldgoodtapinfo = oldgoodtapinfo;
                modifyclientsform.SelCstGroupDtlList = SelCstGroupDtlList;
                modifyclientsform.Text = "修改商品客户定价";
                modifyclientsform.ShowDialog();
                if (modifyclientsform.DialogResult == DialogResult.OK)
                {
                    queryBtn_Click(sender, e);
                }
            }
        }
        //筛选查询
        private void BtnSel_Click(object sender, EventArgs e)
        {
            var q = from info in ScmPriceExeList
                    select info;

            if (!string.IsNullOrEmpty(txtClientCode.Text))
            {
                q = q.Where(p => p.Cstcode.Contains(txtClientCode.Text));
            }
            if (!string.IsNullOrEmpty(txtClientName.Text))
            {
                q = q.Where(p => p.Cstname.Contains(txtClientName.Text));
            }

            if (!string.IsNullOrEmpty(txtRegion.Text))
            {
                q = q.Where(p => p.Region.Contains(txtRegion.Text));
            }

            dgvPriceExe.DataSource = new SortableBindingList<ScmPriceExe>(q.ToList());
            dgvPriceExe.Refresh();
            dgvPriceExe.CurrentCell = null;
        }
        //全选
        private void BtnAllSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvPriceExe.RowCount; i++)
            {
                dgvPriceExe.Rows[i].Selected = true;
            }
        }
        //取消全选
        private void BtnCancelSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvPriceExe.RowCount; i++)
            {
                dgvPriceExe.Rows[i].Selected = false;
            }
        }
        //查询商品所有客户组明细定价信息
        private void BtnASelect_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            sqlkeydict.Add("goodid", Waredictinfo.Goodid);

            ScmPriceExeList = dao.GetScmPriceExeList(sqlkeydict);
            dgvPriceExe.DataSource = ScmPriceExeList;
            dgvPriceExe.Refresh();
            dgvPriceExe.CurrentCell = null;
        }

    }
}

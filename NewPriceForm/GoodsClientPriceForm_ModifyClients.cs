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
    public partial class GoodsClientPriceForm_ModifyClients : Form
    {
        public int stateUI = 0;

        public SelPubWaredict Waredictinfo = new SelPubWaredict();
        public ScmPriceGoodtap Oldgoodtapinfo = new ScmPriceGoodtap();
        private CstGroupHdr Selclentgroupinfo = new CstGroupHdr();

        APDao_GoodsClientPrice dao = null;
        APDao_ClientGroup CGdao = new APDao_ClientGroup();

        private SortableBindingList<CstGroupHdr> SelCstGroupHdrList = new SortableBindingList<CstGroupHdr>();
        public SortableBindingList<CstGroupDtl> SelCstGroupDtlList = new SortableBindingList<CstGroupDtl>();
        private SortableBindingList<CstGroupDtl> CstGroupDtlList = new SortableBindingList<CstGroupDtl>();


        private string Defaultdept = "";
        private ScmPriceGoodtap AddpriceGoodtaped = new ScmPriceGoodtap();
        private SortableBindingList<ScmPriceGoodtap> ScmPriceGoodtapList = new SortableBindingList<ScmPriceGoodtap>();

        private SortableBindingList<CstGroupDtl> SelClientList = new SortableBindingList<CstGroupDtl>();

        Dictionary<string, CstGroupDtl> ListDict = new Dictionary<string, CstGroupDtl>();

        private SortableBindingList<ScmPriceExe> SelScmPriceExeList = new SortableBindingList<ScmPriceExe>();

        private string flag = "";
        private void initUI(int astate)
        {
            switch (astate)
            {
                case 0://初始界面
                    BtnSynGroupPrcClient.Enabled = false;
                    BtnClientPrcClient.Enabled = false;
                    BtnAddClientPrice.Enabled = false;
                    BtnAddClientGroupPrice.Enabled = false;
                    break;
                case 1://修改界面

                    break;

            }
        }


        public GoodsClientPriceForm_ModifyClients()
        {
            InitializeComponent();
            dgvClientGroup.AutoGenerateColumns = false;
            dgvClientDtl.AutoGenerateColumns = false;
            dgvScmPriceGoodtap.AutoGenerateColumns = false;
            dgvPriceExe.AutoGenerateColumns = false;
        }

        private void GoodsClientPriceForm_ModifyClients_Load(object sender, EventArgs e)
        {
            initUI(stateUI);
            dao = (APDao_GoodsClientPrice)this.Tag;
            txtGoods.Text = Waredictinfo.Goods;
            CstGroupDtlList.Clear();
            CstGroupDtlList = SelCstGroupDtlList;
            //var USelCstGroupDtlList = from info in CstGroupDtlList
            //                          where info.UseFlag == "00"
            //                          select info;

            dgvClientDtl.DataSource = CstGroupDtlList;
            dgvClientDtl.Refresh();
        }

        private void txtClient_Click(object sender, EventArgs e)
        {
            GoodsClientPriceForm_SelClientGroup selclientgroup = new GoodsClientPriceForm_SelClientGroup();
            selclientgroup.Tag = dao;
            selclientgroup.ShowDialog();
            if (selclientgroup.DialogResult == DialogResult.OK)
            {
                Selclentgroupinfo = selclientgroup.selclentgroupinfo;
                Defaultdept = selclientgroup.Defaultdept;
                if (Selclentgroupinfo.Hdrid != null)
                {
                    SelCstGroupHdrList.Clear();
                    SelCstGroupHdrList.Add(Selclentgroupinfo);
                    dgvClientGroup.DataSource = SelCstGroupHdrList;
                    dgvClientGroup.Refresh();

                    Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();

                    sqlkeydict.Add("hdrid", Selclentgroupinfo.Hdrid);
                    sqlkeydict.Add("goodid", Waredictinfo.Goodid);
                    ScmPriceGoodtapList = dao.GetScmPriceGoodtapList(sqlkeydict);
                    dgvScmPriceGoodtap.DataSource = ScmPriceGoodtapList;
                    dgvScmPriceGoodtap.Refresh();
                    dgvScmPriceGoodtap.CurrentCell = null;
                    
                    if (Selclentgroupinfo.Synctype == "00")
                    {
                        BtnSynGroupPrcClient.Enabled = true;
                        BtnClientPrcClient.Enabled = false;
                        if (ScmPriceGoodtapList.Count > 0)
                        {
                            flag = "5";//是否有值
                            BtnAddClientGroupPrice.Enabled = false;
                            foreach (ScmPriceGoodtap info in ScmPriceGoodtapList)
                            {

                                AddpriceGoodtaped.Prc = info.Prc;
                                AddpriceGoodtaped.Price = info.Price;
                                AddpriceGoodtaped.Bottomprc = info.Bottomprc;
                                AddpriceGoodtaped.Bottomprice = info.Bottomprice;
                                AddpriceGoodtaped.Costprc = info.Costprc;
                                AddpriceGoodtaped.Costprice = info.Costprice;
                                AddpriceGoodtaped.Begindate = info.Begindate;
                                AddpriceGoodtaped.Enddate = info.Enddate;
                                AddpriceGoodtaped.Costrate = info.Costrate;
                                AddpriceGoodtaped.Synctype = info.Synctype;
                                AddpriceGoodtaped.Grouptype = info.Grouptype;
                                AddpriceGoodtaped.Goodtapid = info.Goodtapid;
                                info.PreGoodtapid = Oldgoodtapinfo.Goodtapid;
                            }
                        }
                        else {
                            flag = "6";
                            BtnAddClientGroupPrice.Enabled = true;
                            
                        }

                       
                    }
                    if (Selclentgroupinfo.Synctype == "10")
                    {
                        BtnSynGroupPrcClient.Enabled = false;
                        BtnClientPrcClient.Enabled = true;
                        //flag = "5";
                        if (ScmPriceGoodtapList.Count > 0)
                        {
                            flag = "5";//是否有值
                            BtnAddClientGroupPrice.Enabled = false;
                            foreach (ScmPriceGoodtap info in ScmPriceGoodtapList)
                            {

                                AddpriceGoodtaped.Prc = info.Prc;
                                AddpriceGoodtaped.Price = info.Price;
                                AddpriceGoodtaped.Bottomprc = info.Bottomprc;
                                AddpriceGoodtaped.Bottomprice = info.Bottomprice;
                                AddpriceGoodtaped.Costprc = info.Costprc;
                                AddpriceGoodtaped.Costprice = info.Costprice;
                                AddpriceGoodtaped.Begindate = info.Begindate;
                                AddpriceGoodtaped.Enddate = info.Enddate;
                                AddpriceGoodtaped.Costrate = info.Costrate;
                                AddpriceGoodtaped.Synctype = info.Synctype;
                                AddpriceGoodtaped.Grouptype = info.Grouptype;
                                AddpriceGoodtaped.Goodtapid = info.Goodtapid;
                                info.PreGoodtapid = Oldgoodtapinfo.Goodtapid;
                            }
                        }
                        else
                        {
                            flag = "6";
                            BtnAddClientGroupPrice.Enabled = true;
                        }
                    }
                }
            }
        }

        private void BtnAddClientGroupPrice_Click(object sender, EventArgs e)
        {
            if (Selclentgroupinfo.Code == null)
            {
                MessageBox.Show("未选择客户组信息，无法新增客户组价格！", "程序提示");
                return;
            }
            if (ScmPriceGoodtapList.Count >= 1)
            {
                MessageBox.Show("已存在客户组定价，不允许继续新增！", "程序提示");
                return;
            }
            GoodsClientPriceForm_AddModifyPrice addgoodsclientpriceform = new GoodsClientPriceForm_AddModifyPrice();
            addgoodsclientpriceform.Tag = dao;
            addgoodsclientpriceform.stateUI = 0;
            addgoodsclientpriceform.Text = "客户组定价";
            addgoodsclientpriceform.Waredictinfo = Waredictinfo;
            addgoodsclientpriceform.Selclentgroupinfo = Selclentgroupinfo;
            addgoodsclientpriceform.Defaultdept = Defaultdept;
            //addgoodsclientpriceform.Text = "修改商品统一价";
            addgoodsclientpriceform.ShowDialog();
            if (addgoodsclientpriceform.DialogResult == DialogResult.OK)
            {
                AddpriceGoodtaped = addgoodsclientpriceform.AddpriceGoodtap;
                AddpriceGoodtaped.PreGoodtapid = Oldgoodtapinfo.Goodtapid;
                ScmPriceGoodtapList.Add(AddpriceGoodtaped);
                dgvScmPriceGoodtap.DataSource = ScmPriceGoodtapList;
                dgvScmPriceGoodtap.Refresh();
            }
        }


        public int AddClientDistinct(CstGroupDtl info)
        {
            string cstid = info.Cstid;

            if (!ListDict.ContainsKey(cstid))
            {
                ListDict.Add(cstid, info);
                return 1;
            }
            else
            {
                info = ListDict[cstid];
                return 0;
            }
        }

        //选中客户进行同步客户组定价
        private void BtnSynGroupPrcClient_Click(object sender, EventArgs e)
        {
            if (ScmPriceGoodtapList.Count <= 0)
            {
                MessageBox.Show("请先设置客户组定价！", "程序提示");
                return;
            }
            object obj1 = FormUtils.SelectRows(dgvClientDtl);
            if (obj1 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj1;
            SelClientList.Clear();

            string retmsg = "";//记录已存在数据的客户代码进行提示
            int rcount = 0;//记录重复条数
            foreach (DataGridViewRow dgvr in row)
            {
                CstGroupDtl selinfo = (CstGroupDtl)dgvr.Cells[0].Value;
                //判断客户是否已新增，已新增不允许加载
                int ret = AddClientDistinct(selinfo);
                if (ret == 1)
                {
                    SelClientList.Add(selinfo);
                    foreach (CstGroupDtl info in CstGroupDtlList)
                    {
                        //判断客户明细是否已选择，已选择显示标识赋值为99
                        if (info.Cstid == dgvr.Cells["dataGridViewTextBoxColumn11"].Value.ToString())
                        {
                            info.UseFlag = "99";
                        }

                    }
                    //新增到客户定价明细
                    ScmPriceExe priceexeinfo = new ScmPriceExe();
                    priceexeinfo.Cstid = selinfo.Cstid;
                    priceexeinfo.Cstcode = selinfo.Cstcode;
                    priceexeinfo.Cstname = selinfo.CSTNAME;
                    priceexeinfo.Hdrid = Selclentgroupinfo.Hdrid;
                    priceexeinfo.Goodid = Waredictinfo.Goodid;
                    priceexeinfo.GoodsName = Waredictinfo.Name;
                    priceexeinfo.GoodsCode = Waredictinfo.Goods;
                    priceexeinfo.Prc = AddpriceGoodtaped.Prc;
                    priceexeinfo.Price = AddpriceGoodtaped.Price;
                    priceexeinfo.Bottomprc = AddpriceGoodtaped.Bottomprc;
                    priceexeinfo.Bottomprice = AddpriceGoodtaped.Bottomprice;
                    priceexeinfo.Costprc = AddpriceGoodtaped.Costprc;
                    priceexeinfo.Costprice = AddpriceGoodtaped.Costprice;
                    priceexeinfo.Begindate = AddpriceGoodtaped.Begindate;
                    priceexeinfo.Enddate = AddpriceGoodtaped.Enddate;
                    priceexeinfo.Source = "16";
                    priceexeinfo.Type = "3";
                    priceexeinfo.Costrate = AddpriceGoodtaped.Costrate;
                    priceexeinfo.Synctype = AddpriceGoodtaped.Synctype;
                    priceexeinfo.Grouptype = AddpriceGoodtaped.Grouptype;
                    priceexeinfo.Goodtapid = AddpriceGoodtaped.Goodtapid;                   
                    SelScmPriceExeList.Add(priceexeinfo);
                }
                else
                {
                    rcount++;
                    retmsg = retmsg + selinfo.Cstcode + "、";
                }
            }

            var USelCstGroupDtlList = from info in CstGroupDtlList
                                      where info.UseFlag == "00"
                                      select info;

            dgvClientDtl.DataSource = new SortableBindingList<CstGroupDtl>(USelCstGroupDtlList);
            dgvClientDtl.Refresh();

            dgvPriceExe.DataSource = SelScmPriceExeList;
            dgvPriceExe.Refresh();
            if (rcount > 0)
            {
                MessageBox.Show("客户代码：" + retmsg + "已新增，不允许重复新增！");
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (Selclentgroupinfo.Synctype == "00")
            {
                if (ScmPriceGoodtapList.Count < 1)
                {
                    MessageBox.Show("未设置客户组定价，请检查！", "程序提示");
                    return;
                }
                if (SelScmPriceExeList.Count < 1)
                {
                    MessageBox.Show("未新增客户组定价的客户明细，请检查！", "程序提示");
                    return;
                }
                SPRetInfo retinfo = new SPRetInfo();
                dao.PPrcGoodDtapInf(ScmPriceGoodtapList, SelScmPriceExeList, Defaultdept, flag, retinfo);
                if (retinfo.num == "1")
                {
                    MessageBox.Show("提交成功！|"+retinfo.msg + "|" + retinfo.count + "|" + retinfo.selflag + "|" + retinfo.result, "后台提示");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }
                else
                {
                    MessageBox.Show("提交失败！|"+retinfo.msg + "|" + retinfo.count + "|" + retinfo.selflag + "|" + retinfo.result, "后台提示");
                    return;
                }
            }
            if (Selclentgroupinfo.Synctype == "10")
            {

                if (SelScmPriceExeList.Count < 1)
                {
                    MessageBox.Show("未新增客户组定价的客户明细，请检查！", "程序提示");
                    return;
                }
                SPRetInfo retinfo = new SPRetInfo();
                dao.PPrcGoodDtapInf(ScmPriceGoodtapList, SelScmPriceExeList, Defaultdept, flag, retinfo);
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

        private void BtnClientPrcClient_Click(object sender, EventArgs e)
        {
            object obj1 = FormUtils.SelectRows(dgvClientDtl);
            if (obj1 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj1;

            string retmsg = "";//记录已存在数据的客户代码进行提示
            int rcount = 0;//记录重复条数
            foreach (DataGridViewRow dgvr in row)
            {
                CstGroupDtl selinfo = (CstGroupDtl)dgvr.Cells[0].Value;
                //判断客户是否已新增，已新增不允许加载
                int ret = AddClientDistinct(selinfo);
                if (ret == 1)
                {
                    SelClientList.Add(selinfo);
                    foreach (CstGroupDtl info in CstGroupDtlList)
                    {
                        //判断客户明细是否已选择，已选择显示标识赋值为99
                        if (info.Cstid == dgvr.Cells["dataGridViewTextBoxColumn11"].Value.ToString())
                        {
                            info.UseFlag = "99";
                            break;
                        }

                    }
                }
                else
                {
                    rcount++;
                    retmsg = retmsg + selinfo.Cstcode + "、";
                }
            }

            var USelCstGroupDtlList = from info in CstGroupDtlList
                                      where info.UseFlag == "00"
                                      select info;

            dgvClientDtl.DataSource = new SortableBindingList<CstGroupDtl>(USelCstGroupDtlList);
            dgvClientDtl.Refresh();

            dgvSelClientDtl.DataSource = SelClientList;
            dgvSelClientDtl.Refresh();

            if (SelClientList.Count > 0)
            {
                BtnAddClientPrice.Enabled = true;
            }
            else
            {
                BtnAddClientPrice.Enabled = false;
            }

            if (rcount > 0)
            {
                MessageBox.Show("客户代码：" + retmsg + "已新增，不允许重复新增！");
            }

        }

        private void BtnAddClientPrice_Click(object sender, EventArgs e)
        {
            GoodsClientPriceForm_AddModifyPrice addgoodsclientpriceform = new GoodsClientPriceForm_AddModifyPrice();
            addgoodsclientpriceform.Tag = dao;
            //addgoodsclientpriceform.stateUI = 1;
            addgoodsclientpriceform.Text = "客户定价";
            addgoodsclientpriceform.Waredictinfo = Waredictinfo;
            addgoodsclientpriceform.Selclentgroupinfo = Selclentgroupinfo;
            addgoodsclientpriceform.Defaultdept = Defaultdept;
            //addgoodsclientpriceform.Text = "修改商品统一价";
            addgoodsclientpriceform.ShowDialog();
            if (addgoodsclientpriceform.DialogResult == DialogResult.OK)
            {
                //定价
                ScmPriceGoodtap AddpriceGoodtapedOnly = new ScmPriceGoodtap();
                AddpriceGoodtapedOnly = addgoodsclientpriceform.AddpriceGoodtap;
                ////拼客户组信息
                if (ScmPriceGoodtapList.Count < 1)
                {
                    AddpriceGoodtaped.Hdrid = Selclentgroupinfo.Hdrid;
                    AddpriceGoodtaped.Synctype = Selclentgroupinfo.Synctype;
                    AddpriceGoodtaped.Grouptype = Selclentgroupinfo.Grouptype;
                    AddpriceGoodtaped.Goodid = Waredictinfo.Goodid;
                    AddpriceGoodtaped.PreGoodtapid = Oldgoodtapinfo.Goodtapid;
                    AddpriceGoodtaped.Costprctype = "10";
                    ScmPriceGoodtapList.Add(AddpriceGoodtaped);
                }
                ////进行循环拼客户定价数据
                foreach (CstGroupDtl info in SelClientList)
                {
                    //新增到客户定价明细
                    ScmPriceExe priceexeinfo = new ScmPriceExe();
                    priceexeinfo.Cstid = info.Cstid;
                    priceexeinfo.Cstcode = info.Cstcode;
                    priceexeinfo.Cstname = info.CSTNAME;
                    priceexeinfo.Hdrid = Selclentgroupinfo.Hdrid;
                    priceexeinfo.Goodid = Waredictinfo.Goodid;
                    priceexeinfo.GoodsName = Waredictinfo.Name;
                    priceexeinfo.GoodsCode = Waredictinfo.Goods;
                    priceexeinfo.Prc = AddpriceGoodtapedOnly.Prc;
                    priceexeinfo.Price = AddpriceGoodtapedOnly.Price;
                    priceexeinfo.Bottomprc = AddpriceGoodtapedOnly.Bottomprc;
                    priceexeinfo.Bottomprice = AddpriceGoodtapedOnly.Bottomprice;
                    priceexeinfo.Costprc = AddpriceGoodtapedOnly.Costprc;
                    priceexeinfo.Costprice = AddpriceGoodtapedOnly.Costprice;
                    priceexeinfo.Begindate = AddpriceGoodtapedOnly.Begindate;
                    priceexeinfo.Enddate = AddpriceGoodtapedOnly.Enddate;
                    priceexeinfo.Source = "16";
                    priceexeinfo.Type = "3";
                    priceexeinfo.Costrate = AddpriceGoodtapedOnly.Costrate;
                    priceexeinfo.Synctype = AddpriceGoodtaped.Synctype;
                    priceexeinfo.Grouptype = AddpriceGoodtaped.Grouptype;
                    if (flag == "5") {
                        priceexeinfo.Goodtapid = AddpriceGoodtaped.Goodtapid;
                    }
                    SelScmPriceExeList.Add(priceexeinfo);
                }


                SelClientList.Clear();
                dgvPriceExe.DataSource = SelScmPriceExeList;
                dgvPriceExe.Refresh();

            }
        }

        private void BtnSelect_Click(object sender, EventArgs e)
        {
            var q = from info in CstGroupDtlList
                    where info.UseFlag == "00"
                    select info;

            if (!string.IsNullOrEmpty(txtClientCode.Text))
            {
                q = q.Where(p => p.Cstcode.Contains(txtClientCode.Text));
            }
            if (!string.IsNullOrEmpty(txtClientName.Text))
            {
                q = q.Where(p => p.CSTNAME.Contains(txtClientName.Text));
            }

            if (!string.IsNullOrEmpty(txtRegion.Text))
            {
                q = q.Where(p => p.Region.Contains(txtRegion.Text));
            }

            dgvClientDtl.DataSource = new SortableBindingList<CstGroupDtl>(q.ToList());
            dgvClientDtl.Refresh();
        }

        private void BtnAllSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvClientDtl.RowCount; i++)
            {
                dgvClientDtl.Rows[i].Selected = true;
            }
        }

        private void BtnCancelSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvClientDtl.RowCount; i++)
            {
                dgvClientDtl.Rows[i].Selected = false;
            }
        }

    }
}

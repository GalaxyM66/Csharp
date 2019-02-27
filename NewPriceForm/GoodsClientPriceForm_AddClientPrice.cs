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
    public partial class GoodsClientPriceForm_AddClientPrice : Form
    {
        public SelPubWaredict Waredictinfo = new SelPubWaredict();
        //private CstGroupHdr Selclentgroupinfo = new CstGroupHdr();

        public ScmPriceGoodtap PriceGoodtapinfo = new ScmPriceGoodtap();

        APDao_GoodsClientPrice dao = null;
        APDao_ClientGroup CGdao = new APDao_ClientGroup();

        private string Defaultdept = "";
        private ScmPriceGoodtap AddpriceGoodtaped = new ScmPriceGoodtap();

        private SortableBindingList<CstGroupHdr> CstGroupHdrList = new SortableBindingList<CstGroupHdr>();
        private CstGroupHdr Selclentgroupinfo = new CstGroupHdr();
        private SortableBindingList<ScmPriceGoodtap> ScmPriceGoodtapList = new SortableBindingList<ScmPriceGoodtap>();
        private SortableBindingList<ScmPriceExe> SelScmPriceExeList = new SortableBindingList<ScmPriceExe>();


        Dictionary<string, CstGroupDtl> ListDict = new Dictionary<string, CstGroupDtl>();
        private SortableBindingList<CstGroupDtl> SelClientList = new SortableBindingList<CstGroupDtl>();
        public GoodsClientPriceForm_AddClientPrice()
        {
            InitializeComponent();
            dgvClientGroup.AutoGenerateColumns = false;
            dgvScmPriceGoodtap.AutoGenerateColumns = false;
            dgvPriceExe.AutoGenerateColumns = false;
        }

        private void GoodsClientPriceForm_AddClientPrice_Load(object sender, EventArgs e)
        {
            dao = (APDao_GoodsClientPrice)this.Tag;
            txtGoods.Text = Waredictinfo.Goods;
            txtClient.Text = PriceGoodtapinfo.Code;
            Defaultdept = PriceGoodtapinfo.Saledeptid;
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();

            sqlkeydict.Add("hdrid", PriceGoodtapinfo.Hdrid);
            CstGroupHdrList = CGdao.GetCstGroupHdrList(sqlkeydict);
            foreach (CstGroupHdr info in CstGroupHdrList) {
                Selclentgroupinfo = info;
            }
            dgvClientGroup.DataSource = CstGroupHdrList;
            dgvClientGroup.Refresh();
            dgvClientGroup.CurrentCell = null;

            ScmPriceGoodtapList.Add(PriceGoodtapinfo);
            dgvScmPriceGoodtap.DataSource = ScmPriceGoodtapList;
            dgvScmPriceGoodtap.Refresh();
            dgvScmPriceGoodtap.CurrentCell = null;


                BtnAddClientPrice.Enabled = false;

            //ScmPriceExeList = dao.GetScmPriceExeList(sqlkeydict);
            //dgvPriceExe.DataSource = ScmPriceExeList;
            //dgvPriceExe.Refresh();
            //dgvPriceExe.CurrentCell = null;
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

        private void BtnClientPrice_Click(object sender, EventArgs e)
        {
            if (PriceGoodtapinfo.Synctype == "00")
            {
                if (PriceGoodtapinfo.Code == null)
                {
                    MessageBox.Show("未选择客户组信息，无法新增客户组价格！", "程序提示");
                    return;
                }
                if (ScmPriceGoodtapList.Count < 1)
                {
                    MessageBox.Show("未设置客户组定价，不允许继续新增！", "程序提示");
                    return;
                }
            }

            SortableBindingList<CstGroupDtl> NewSelClientList = new SortableBindingList<CstGroupDtl>();

            GoodsClientPriceForm_SelClient addselclient = new GoodsClientPriceForm_SelClient();
            addselclient.Tag = dao;
            NewSelClientList = addselclient.SelClientList;
            addselclient.ShowDialog();
            if (addselclient.DialogResult == DialogResult.OK)
            {
                if (PriceGoodtapinfo.Synctype == "10")
                {
                    string retmsg = "";//记录已存在数据的客户代码进行提示
                    int rcount = 0;//记录重复条数
                    foreach (CstGroupDtl selinfo in NewSelClientList)
                    {
                        int ret = AddClientDistinct(selinfo);
                        if (ret == 1)
                        {
                            SelClientList.Add(selinfo);


                        }
                        else
                        {
                            rcount++;
                            retmsg = retmsg + selinfo.Cstcode + "、";
                        }

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

                    dgvSelClientDtl.DataSource = SelClientList;
                    dgvSelClientDtl.Refresh();

                    dgvPriceExe.DataSource = SelScmPriceExeList;
                    dgvPriceExe.Refresh();

                    if (SelClientList.Count > 0)
                    {
                        BtnAddClientPrice.Enabled = true;
                    }
                    else {
                        BtnAddClientPrice.Enabled = false;
                    }
                    
                }

                if (PriceGoodtapinfo.Synctype == "00")
                {


                    string retmsg = "";//记录已存在数据的客户代码进行提示
                    int rcount = 0;//记录重复条数
                    foreach (CstGroupDtl selinfo in NewSelClientList)
                    {
                        int ret = AddClientDistinct(selinfo);
                        if (ret == 1)
                        {
                            //新增到客户定价明细
                            ScmPriceExe priceexeinfo = new ScmPriceExe();
                            priceexeinfo.Cstid = selinfo.Cstid;
                            priceexeinfo.Cstcode = selinfo.Cstcode;
                            priceexeinfo.Cstname = selinfo.CSTNAME;
                            priceexeinfo.Hdrid = PriceGoodtapinfo.Hdrid;
                            priceexeinfo.Goodid = Waredictinfo.Goodid;
                            priceexeinfo.GoodsName = Waredictinfo.Name;
                            priceexeinfo.GoodsCode = Waredictinfo.Goods;
                            priceexeinfo.Prc = PriceGoodtapinfo.Prc;
                            priceexeinfo.Price = PriceGoodtapinfo.Price;
                            priceexeinfo.Bottomprc = PriceGoodtapinfo.Bottomprc;
                            priceexeinfo.Bottomprice = PriceGoodtapinfo.Bottomprice;
                            priceexeinfo.Costprc = PriceGoodtapinfo.Costprc;
                            priceexeinfo.Costprice = PriceGoodtapinfo.Costprice;
                            priceexeinfo.Begindate = PriceGoodtapinfo.Begindate;
                            priceexeinfo.Enddate = PriceGoodtapinfo.Enddate;
                            priceexeinfo.Source = "10";
                            priceexeinfo.Type = "0";
                            priceexeinfo.Costrate = PriceGoodtapinfo.Costrate;
                            priceexeinfo.Synctype = PriceGoodtapinfo.Synctype;
                            priceexeinfo.Grouptype = PriceGoodtapinfo.Grouptype;
                            priceexeinfo.Goodtapid = PriceGoodtapinfo.Goodtapid;
                            SelScmPriceExeList.Add(priceexeinfo);


                        }
                        else
                        {
                            rcount++;
                            retmsg = retmsg + selinfo.Cstcode + "、";
                        }

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
                    SelClientList.Clear();
                    dgvPriceExe.DataSource = SelScmPriceExeList;
                    dgvPriceExe.Refresh();
                }

            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (PriceGoodtapinfo.Synctype == "00")
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
                dao.PPrcGoodDtapInf(ScmPriceGoodtapList, SelScmPriceExeList, Defaultdept,"3", retinfo);
                if (retinfo.num == "1")
                {
                    MessageBox.Show("提交成功！|"+retinfo.msg + "|" + retinfo.count + "|" + retinfo.selflag + "|" + retinfo.result, "后台提示");
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
            if (PriceGoodtapinfo.Synctype == "10")
            {

                if (SelScmPriceExeList.Count < 1)
                {
                    MessageBox.Show("未新增客户组定价的客户明细，请检查！", "程序提示");
                    return;
                }
                SPRetInfo retinfo = new SPRetInfo();
                dao.PPrcGoodDtapInf(ScmPriceGoodtapList, SelScmPriceExeList, Defaultdept, "3", retinfo);
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

        private void BtnAddClientPrice_Click(object sender, EventArgs e)
        {
            GoodsClientPriceForm_AddModifyPrice addgoodsclientpriceform = new GoodsClientPriceForm_AddModifyPrice();          
            addgoodsclientpriceform.stateUI = 0;
            addgoodsclientpriceform.Text = "客户定价";
            addgoodsclientpriceform.Waredictinfo = Waredictinfo;
            addgoodsclientpriceform.Selclentgroupinfo = Selclentgroupinfo;
            addgoodsclientpriceform.Defaultdept = Defaultdept;
            //addgoodsclientpriceform.Text = "修改商品统一价";
            addgoodsclientpriceform.Tag = dao;
            addgoodsclientpriceform.ShowDialog();
            if (addgoodsclientpriceform.DialogResult == DialogResult.OK)
            {
                //定价
                AddpriceGoodtaped = addgoodsclientpriceform.AddpriceGoodtap;
                //拼客户组信息
                if (ScmPriceGoodtapList.Count < 1)
                {
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
                    priceexeinfo.Hdrid = PriceGoodtapinfo.Hdrid;
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
                    priceexeinfo.Source = "14";
                    priceexeinfo.Type = "0";
                    priceexeinfo.Costrate = AddpriceGoodtaped.Costrate;
                    priceexeinfo.Synctype = AddpriceGoodtaped.Synctype;
                    priceexeinfo.Grouptype = AddpriceGoodtaped.Grouptype;
                    priceexeinfo.Goodtapid = PriceGoodtapinfo.Goodtapid;
                    SelScmPriceExeList.Add(priceexeinfo);
                }


                SelClientList.Clear();
                dgvPriceExe.DataSource = SelScmPriceExeList;
                dgvPriceExe.Refresh();

            }
        }
    }
}

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
    public partial class GoodsClientPriceForm_AddModifyPriceForm : Form
    {
        APDao_GoodsClientPrice dao = null;

        public int stateUI = 0;

        public SelPubWaredict Waredictinfo = new SelPubWaredict();
        public CstGroupHdr Selclentgroupinfo = new CstGroupHdr();
        public string Defaultdept = "";//传选择定价客户组的归属部门

        public ScmPriceGoodtap AddpriceGoodtap = new ScmPriceGoodtap();
        public SortableBindingList<ScmPriceGoodunify> ScmPriceGoodunifyList = new SortableBindingList<ScmPriceGoodunify>();//商品统一价

        PMSystemDao Pdao = new PMSystemDao();
        private SortableBindingList<SysCode> CostprctypeList = new SortableBindingList<SysCode>();

        private DeptConfigureDto DeptConfigureDtoInfo = new DeptConfigureDto();
        private SortableBindingList<SysCode> stopflagList = new SortableBindingList<SysCode>();
        private void initUI(int astate)
        {
            switch (astate)
            {
                case 0://新增界面
                    txtGoods.Enabled = true;
                    txtCode.Enabled = true;
                    txtPrice.Enabled = false;
                    txtCostprice.Enabled = false;
                    txtBottomprice.Enabled = false;
                    txtCostrate.Enabled = false;
                    btnSubmit.Visible = true;
                    txtPrc.Enabled = false;
                    label12.Visible = false;
                    cbShopflag.Visible = false;
                    btnSubmit.Visible = true;
                    btnModfiy.Visible = false;
                    cbDeliveryfeerate.Value = decimal.Parse(Selclentgroupinfo.Deliveryfeerate);

                    foreach (ScmPriceGoodunify info in ScmPriceGoodunifyList)
                    {
                        if (info.Costprc != "" || info.Costprc != null)
                        {
                            txtCostprc.Text = info.Costprc;
                            Dictionary<string, string> sqlkeydict1 = new Dictionary<string, string>();
                            sqlkeydict1.Add("Costprc", "");
                            CalculatePrice(sqlkeydict1);
                        }
                    }

                    break;
                case 1://修改界面
                    txtGoods.Enabled = false;
                    txtCode.Enabled = false;
                    txtPrice.Enabled = false;
                    txtPrc.Text = AddpriceGoodtap.Prc;
                    txtPrc.Focus();
                    txtPrice.Enabled = false;
                    txtPrice.Text = AddpriceGoodtap.Price;
                    txtCostprc.Text = AddpriceGoodtap.Costprc;
                    txtCostprice.Enabled = false;
                    txtCostprc.Focus();
                    txtCostprice.Enabled = false;
                    txtCostprice.Text = AddpriceGoodtap.Costprice;
                    txtBottomprc.Text = AddpriceGoodtap.Bottomprc;
                    txtBottomprc.Focus();
                    txtBottomprice.Enabled = false;
                    txtBottomprice.Text = AddpriceGoodtap.Bottomprice;
                    txtCostrate.Enabled = false;
                    txtCostrate.Text = AddpriceGoodtap.Costrate;
                    cbDeliveryfeerate.Value = decimal.Parse(AddpriceGoodtap.Pgdeliveryfeerate);
                    btnSubmit.Visible = true;
                    txtPrc.Enabled = false;
                    label12.Visible = true;
                    btnSubmit.Visible = false;
                    btnModfiy.Visible = true;

                    cbShopflag.Visible = true;
                    Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
                    sqlkeydict.Add("typeid", "1");
                    stopflagList = Pdao.GetSysCode(sqlkeydict);

                    cbShopflag.DataSource = stopflagList;
                    cbShopflag.DisplayMember = "Name";
                    cbShopflag.ValueMember = "Code";

                    break;

            }
        }
        public GoodsClientPriceForm_AddModifyPriceForm()
        {
            InitializeComponent();
        }

        private void GoodsClientPriceForm_AddModifyPriceForm_Load(object sender, EventArgs e)
        {
            dao = (APDao_GoodsClientPrice)this.Tag;


            txtGoods.Text = Waredictinfo.Goods;
            txtCode.Text = Selclentgroupinfo.Code;
            initUI(stateUI);
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            sqlkeydict.Add("typeid", "49");
            CostprctypeList = Pdao.GetSysCode(sqlkeydict);

            cbCostprctype.DataSource = CostprctypeList;
            cbCostprctype.DisplayMember = "Name";
            cbCostprctype.ValueMember = "Code";

            if (Selclentgroupinfo.Synctype == "00")//客户组同步类型为同步客户价的，不控制
            {
                try
                {
                    cbCostprctype.Enabled = false;
                }
                catch (Exception ex) { }
            }

            if (Selclentgroupinfo.Synctype == "10")//客户组同步类型为不同步客户价的，控制只能选”独立”
            {
                try
                {
                    label10.Visible = false;
                    txtCode.Visible = false;
                    cbCostprctype.Enabled = false;
                    cbCostprctype.SelectedValue = "10";
                }
                catch (Exception ex) { }
            }
            if (Defaultdept == null || Defaultdept == "")
            {
                MessageBox.Show("未选择客户组归属部门！", "程序提示");
                return;
            }
            DeptConfigureDtoInfo = Pdao.GetDeptConfigureInfo(Defaultdept);
            if (DeptConfigureDtoInfo.WaitFlag == "0")
            {
                dtpStarttime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                dtpStarttime.MinDate = DateTime.Now;
                dtpEndtime.MinDate = dtpStarttime.Value.AddDays(1);
                if (DeptConfigureDtoInfo.DefaultEnddate == null || DeptConfigureDtoInfo.DefaultEnddate == "")
                {
                    dtpEndtime.Text = dtpStarttime.Value.AddDays(int.Parse(DeptConfigureDtoInfo.ValidDays)).ToString("yyyy-MM-dd");
                }
                else
                {
                    dtpEndtime.Text = DateTime.Now.ToString(DeptConfigureDtoInfo.DefaultEnddate);
                }

                dtpStarttime.Enabled = false;
            }
            if (DeptConfigureDtoInfo.WaitFlag == "1")
            {
                dtpStarttime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                dtpStarttime.MinDate = DateTime.Now;
                dtpEndtime.MinDate = dtpStarttime.Value.AddDays(1);
                if (DeptConfigureDtoInfo.DefaultEnddate == null || DeptConfigureDtoInfo.DefaultEnddate == "")
                {
                    dtpEndtime.Text = dtpStarttime.Value.AddDays(int.Parse(DeptConfigureDtoInfo.ValidDays)).ToString("yyyy-MM-dd");
                }
                else
                {
                    dtpEndtime.Text = DateTime.Now.ToString(DeptConfigureDtoInfo.DefaultEnddate);
                }
                dtpStarttime.Enabled = false;
            }

        }
        private void CalculatePrice(Dictionary<string, string> sqlkeydict)
        {
            //设置变量,计算无税底价
            //遍历Dictionary的Values值
            foreach (KeyValuePair<string, string> kv in sqlkeydict)
            {
                if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                {
                    if (kv.Key == "Bottomprc1")
                    {
                        if (!String.IsNullOrEmpty(Waredictinfo.Goodid) && !String.IsNullOrEmpty(Waredictinfo.Outrate))
                        {
                            if (!String.IsNullOrEmpty(txtBottomprc.Text.ToString()) && !String.IsNullOrEmpty(cbDeliveryfeerate.Value.ToString()))
                            {
                                try
                                {
                                    txtBottomprice.Text = (Math.Round(double.Parse(txtBottomprc.Text.ToString()) / (1 + double.Parse(Waredictinfo.Outrate)), 6)).ToString();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("请检查输入的价格是否有误！", "程序提示");
                                    txtBottomprc.Clear();
                                    txtBottomprice.Clear();
                                }
                            }
                            else
                            {
                                txtBottomprice.Clear();
                            }
                        }
                        else
                        {
                            MessageBox.Show("请检查是否未选择商品信息或商品未设置税率！", "程序提示");

                        }
                    }               
                }

            }




        }

        private void dtpStarttime_ValueChanged(object sender, EventArgs e)
        {
            //try
            //{

            //    dtpEndtime.MinDate = dtpStarttime.Value.AddDays(1);
            //    //dtpEndtime.MaxDate = dtpStarttime.Value.AddDays(1);
            //    if (DeptConfigureDtoInfo.DefaultEnddate.Equals(null) || DeptConfigureDtoInfo.DefaultEnddate.Equals(""))
            //    {
            //        dtpEndtime.Text = dtpStarttime.Value.AddDays(int.Parse(DeptConfigureDtoInfo.ValidDays)).ToString("yyyy-MM-dd");
            //    }
            //    else
            //    {
            //        dtpEndtime.Text = DateTime.Now.ToString(DeptConfigureDtoInfo.DefaultEnddate);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    //dtpEndtime.MaxDate = dtpStarttime.Value;
            //    dtpEndtime.MinDate = dtpStarttime.Value.AddDays(1);
            //    if (DeptConfigureDtoInfo.DefaultEnddate.Equals(null) || DeptConfigureDtoInfo.DefaultEnddate.Equals(""))
            //    {
            //        dtpEndtime.Text = dtpStarttime.Value.AddDays(int.Parse(DeptConfigureDtoInfo.ValidDays)).ToString("yyyy-MM-dd");
            //    }
            //    else
            //    {
            //        dtpEndtime.Text = DateTime.Now.ToString(DeptConfigureDtoInfo.DefaultEnddate);
            //    }
            //}
        }

        private void btnModfiy_Click(object sender, EventArgs e)
        {
            double Bprc = double.Parse(txtBottomprc.Text.ToString());
            double Cprc = double.Parse(txtCostprc.Text.ToString());
            double Prc = double.Parse(txtPrc.Text.ToString());
            if (String.IsNullOrEmpty(txtBottomprc.Text))
            {
                MessageBox.Show("请填写含税底价！", "程序提示");
                txtBottomprc.Focus();
                return;
            }
            if (Bprc < Cprc)
            {
                MessageBox.Show("含税底价不能低于含税成本价！", "前台提示");
                txtBottomprc.Focus();

                return;
            }
            if (Bprc > Prc)
            {
                MessageBox.Show("含税底价不能高于公开含税销价！", "前台提示");
                txtBottomprc.Focus();
                return;
            }    

            if (double.Parse(cbDeliveryfeerate.Value.ToString()) <= 0)
            {

                Form_confirm fconfirm = new Form_confirm();
                fconfirm.Message = "加点率小于等于0，是否确认！";
                fconfirm.ShowDialog();

                if (fconfirm.DialogResult != DialogResult.Yes)
                {
                    return;
                }

            }

            if (double.Parse(txtCostrate.Text.ToString()) <= 0)
            {

                Form_confirm fconfirm = new Form_confirm();
                fconfirm.Message = "考核毛利率小于等于0，是否确认！";
                fconfirm.ShowDialog();

                if (fconfirm.DialogResult != DialogResult.Yes)
                {
                    return;
                }

            }
            AddpriceGoodtap.Goodid = Waredictinfo.Goodid;
            AddpriceGoodtap.Hdrid = Selclentgroupinfo.Hdrid;
            AddpriceGoodtap.Pgdeliveryfeerate = Math.Round(decimal.Parse(cbDeliveryfeerate.Value.ToString()), 6).ToString();
            AddpriceGoodtap.Prc = txtPrc.Text.ToString();
            AddpriceGoodtap.Price = txtPrice.Text.ToString();
            AddpriceGoodtap.Costprctype = cbCostprctype.SelectedValue.ToString();
            AddpriceGoodtap.Costprc = txtCostprc.Text.ToString();
            AddpriceGoodtap.Costprice = txtCostprice.Text.ToString();
            AddpriceGoodtap.Costrate = txtCostrate.Text.ToString();
            AddpriceGoodtap.Bottomprc = txtBottomprc.Text.ToString();
            AddpriceGoodtap.Bottomprice = txtBottomprice.Text.ToString();
            AddpriceGoodtap.Begindate = dtpStarttime.Value.ToString("yyyy-MM-dd");
            AddpriceGoodtap.Enddate = dtpEndtime.Value.ToString("yyyy-MM-dd");
            AddpriceGoodtap.Synctype = Selclentgroupinfo.Synctype;
            AddpriceGoodtap.Grouptype = Selclentgroupinfo.Grouptype;
            AddpriceGoodtap.Operatype = "1";
            AddpriceGoodtap.Stopflag = cbShopflag.SelectedValue.ToString();
            AddpriceGoodtap.Stopflagname = cbShopflag.Text.ToString();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtBottomprc_TextChanged(object sender, EventArgs e)
        {
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            sqlkeydict.Add("Bottomprc1", "");
            CalculatePrice(sqlkeydict);
        }
    }
}

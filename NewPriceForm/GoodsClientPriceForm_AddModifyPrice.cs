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
    public partial class GoodsClientPriceForm_AddModifyPrice : Form
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
                    
                    foreach (ScmPriceGoodunify info in ScmPriceGoodunifyList) {
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

        public GoodsClientPriceForm_AddModifyPrice()
        {
            InitializeComponent();
        }

        private void GoodsClientPriceForm_AddPrice_Load(object sender, EventArgs e)
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
                    cbCostprctype.Enabled = true;
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
            if(Defaultdept==null||Defaultdept==""){
                MessageBox.Show("未选择客户组归属部门！","程序提示");
                return;
            }
                DeptConfigureDtoInfo = Pdao.GetDeptConfigureInfo(Defaultdept);
                if (DeptConfigureDtoInfo.WaitFlag == "0")
                {
                    dtpStarttime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    dtpStarttime.MinDate = DateTime.Now;
                    dtpEndtime.MinDate = dtpStarttime.Value.AddDays(1);
                    if (DeptConfigureDtoInfo.DefaultEnddate==null || DeptConfigureDtoInfo.DefaultEnddate=="")
                    {
                        dtpEndtime.Text = dtpStarttime.Value.AddDays(int.Parse(DeptConfigureDtoInfo.ValidDays)).ToString("yyyy-MM-dd");
                    }
                    else {
                        dtpEndtime.Text = DateTime.Now.ToString(DeptConfigureDtoInfo.DefaultEnddate);
                    }
                    
                    dtpStarttime.Enabled = false;
                }
                if (DeptConfigureDtoInfo.WaitFlag == "1")
                {
                    dtpStarttime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    dtpStarttime.MinDate = DateTime.Now;
                    dtpEndtime.MinDate = dtpStarttime.Value.AddDays(1);
                    if (DeptConfigureDtoInfo.DefaultEnddate==null || DeptConfigureDtoInfo.DefaultEnddate=="")
                    {
                        dtpEndtime.Text = dtpStarttime.Value.AddDays(int.Parse(DeptConfigureDtoInfo.ValidDays)).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        dtpEndtime.Text = DateTime.Now.ToString(DeptConfigureDtoInfo.DefaultEnddate);
                    }
                    dtpStarttime.Enabled = true;
                }
            
            
        }



        private void CalculatePrice(Dictionary<string, string> sqlkeydict)
        {
            //设置变量，Pic：计算公开无税销价，Costprc：计算无税成本销价，3：计算无税底价，4计算毛利率

            //遍历Dictionary的Values值
            foreach (KeyValuePair<string, string> kv in sqlkeydict)
            {
                if (!kv.Key.Equals(null) && !kv.Key.Equals(""))
                {
                    if (kv.Key == "Pic1")
                    {
                        if (!String.IsNullOrEmpty(Waredictinfo.Outrate))
                        {
                            if (!String.IsNullOrEmpty(txtPrc.Text.ToString()) && !String.IsNullOrEmpty(txtBottomprc.Text.ToString()))
                            {
                                try
                                {
                                    txtPrice.Text = (Math.Round(double.Parse(txtPrc.Text.ToString()) / (1 + double.Parse(Waredictinfo.Outrate)), 6)).ToString();                                   
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("请检查输入的价格是否有误！", "程序提示");
                                    txtPrc.Clear();
                                    txtPrice.Clear();
                                }
                            }
                            else
                            {
                                txtPrice.Clear();

                            }
                        }
                        else
                        {
                            MessageBox.Show("请检查是否未选择商品信息或商品未设置税率！", "程序提示");
                        }
                    }

                    if (kv.Key == "Pic")
                    {
                        if (!String.IsNullOrEmpty(Waredictinfo.Goodid) && !String.IsNullOrEmpty(Waredictinfo.Outrate))
                        {
                            if (!String.IsNullOrEmpty(txtPrc.Text.ToString()) && !String.IsNullOrEmpty(txtBottomprc.Text.ToString()))
                            {
                                try
                                {                                
                                    cbDeliveryfeerate.Value = decimal.Round((decimal.Parse(txtPrc.Text.ToString()) / decimal.Parse(txtBottomprc.Text.ToString()) - 1), 6, MidpointRounding.AwayFromZero);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("请检查输入的价格是否有误！", "程序提示");
                                    //txtPrc.SelectAll();

                                    txtCostrate.Clear();
                                }
                            }
                            else
                            {
                                txtCostrate.Clear();

                            }
                        }
                        else
                        {
                            MessageBox.Show("请检查是否未选择商品信息或商品未设置税率！", "程序提示");
                            txtGoods.Focus();
                        }
                    }
                    if (kv.Key == "Costprc")
                    {
                        if (!String.IsNullOrEmpty(Waredictinfo.Goodid) && !String.IsNullOrEmpty(Waredictinfo.Outrate))
                        {
                            if (!String.IsNullOrEmpty(txtCostprc.Text.ToString()))
                            {
                                try
                                {
                                    txtCostprice.Text = (Math.Round(double.Parse(txtCostprc.Text.ToString()) / (1 + double.Parse(Waredictinfo.Outrate)), 6)).ToString();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("请检查输入的价格是否有误！", "程序提示");
                                    txtCostprc.Clear();
                                    txtCostprice.Clear();
                                    txtCostrate.Clear();
                                }
                            }
                            else
                            {
                                txtCostprice.Clear();
                                txtCostrate.Clear();

                            }
                        }
                        else
                        {
                            MessageBox.Show("请检查是否未选择商品信息或商品未设置税率！", "程序提示");
                            txtGoods.Focus();
                        }

                    }

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

                    if (kv.Key == "Bottomprc")
                    {
                        if (!String.IsNullOrEmpty(Waredictinfo.Goodid) && !String.IsNullOrEmpty(Waredictinfo.Outrate))
                        {
                            if (!String.IsNullOrEmpty(txtBottomprc.Text.ToString()) && !String.IsNullOrEmpty(cbDeliveryfeerate.Value.ToString()))
                            {
                                try
                                {
                                    txtPrc.Enabled = true;
                                    txtPrc.Text = (double.Parse(txtBottomprc.Text.ToString()) * (1 + double.Parse(cbDeliveryfeerate.Value.ToString()))).ToString();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("请检查输入的价格是否有误！", "程序提示");
                                    txtCostrate.Clear();
                                    txtPrc.Clear();
                                    txtPrice.Clear();
                                    txtPrc.Enabled = false;

                                }
                            }
                            else
                            {
                                txtCostrate.Clear();
                                txtPrc.Clear();
                                txtPrice.Clear();
                                txtPrc.Enabled = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("请检查是否未选择商品信息或商品未设置税率！", "程序提示");
                            txtGoods.Focus();
                        }
                    }

                    if (kv.Key == "Costrate")
                    {
                        if (!String.IsNullOrEmpty(txtPrc.Text) && !String.IsNullOrEmpty(txtCostprc.Text))
                        {
                            try
                            {
                                txtCostrate.Text = (Math.Round(((double.Parse(txtPrc.Text.ToString()) - double.Parse(txtCostprc.Text.ToString())) / double.Parse(txtCostprc.Text.ToString())), 6)).ToString();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("请检查输入的价格是否有误！", "程序提示");
                                //txtCostrate.Clear();
                                //txtBottomprice.Clear();
                            }
                        }
                        else
                        {

                            txtCostrate.Clear();
                        }
                    }
                    if (kv.Key == "Deliveryfeerate")
                    {
                        if (!String.IsNullOrEmpty(Waredictinfo.Goodid) && !String.IsNullOrEmpty(Waredictinfo.Outrate))
                        {
                            if (!String.IsNullOrEmpty(cbDeliveryfeerate.Value.ToString()) && !String.IsNullOrEmpty(txtBottomprc.Text))
                            {
                                try
                                {
                                    txtPrc.Text = (double.Parse(txtBottomprc.Text.ToString()) * (1 + double.Parse(cbDeliveryfeerate.Value.ToString()))).ToString();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("请检查输入的价格是否有误！", "程序提示");
                                    txtCostprc.SelectAll();
                                    txtCostprice.Clear();
                                    return;
                                }
                            }
                            else
                            {
                                txtCostprice.Clear();

                            }
                        }
                        else
                        {
                            MessageBox.Show("请检查是否未选择商品信息或商品未设置税率！", "程序提示");
                            txtGoods.Focus();
                            return;
                        }

                    }
                }

            }



        }

        private void txtPrc_Leave(object sender, EventArgs e)
        {
            //if (!String.IsNullOrEmpty(Waredictinfo.Goodid) && !String.IsNullOrEmpty(Waredictinfo.Outrate))
            //{
            //    if (!String.IsNullOrEmpty(txtPrc.Text))
            //    {
            //        try
            //        {
            //            txtPrice.Text = (Math.Round(double.Parse(txtPrc.Text.ToString()) / (1 + double.Parse(Waredictinfo.Outrate)), 6)).ToString();
            //            cbDeliveryfeerate.Value = decimal.Parse((double.Parse(txtPrc.Text.ToString()) / double.Parse(txtBottomprc.Text.ToString()) - 1).ToString());
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("请检查输入的价格是否有误！", "程序提示");
            //            txtPrc.SelectAll();
            //            txtPrice.Clear();
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        txtPrice.Clear();
            //        txtPrc.Enabled = false;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("请检查是否未选择商品信息或商品未设置税率！", "程序提示");
            //    txtGoods.Focus();
            //    return;
            //}

            ////计算毛利率
            //if (!String.IsNullOrEmpty(txtPrc.Text) && !String.IsNullOrEmpty(txtCostprc.Text))
            //{
            //    try
            //    {
            //        txtCostrate.Text = (Math.Round(((double.Parse(txtPrc.Text.ToString()) - double.Parse(txtCostprc.Text.ToString())) / double.Parse(txtCostprc.Text.ToString())), 6)).ToString();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("请检查输入的价格是否有误！", "程序提示");
            //        txtCostrate.SelectAll();
            //        txtBottomprice.Clear();
            //        return;
            //    }
            //}
            //else
            //{
            //    txtBottomprice.Clear();

            //}
        }

        private void txtBottomprc_Leave(object sender, EventArgs e)
        {
            //if (!String.IsNullOrEmpty(Waredictinfo.Goodid) && !String.IsNullOrEmpty(Waredictinfo.Outrate))
            //{
            //    if (!String.IsNullOrEmpty(txtBottomprc.Text))
            //    {
            //        try
            //        {
            //            txtBottomprice.Text = (Math.Round(double.Parse(txtBottomprc.Text.ToString()) / (1 + double.Parse(Waredictinfo.Outrate)), 6)).ToString();
            //            txtPrc.Enabled = true;
            //            txtPrc.Text=(double.Parse(txtBottomprc.Text.ToString())*(1+double.Parse(cbDeliveryfeerate.Value.ToString()))).ToString();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("请检查输入的价格是否有误！", "程序提示");
            //            txtBottomprc.SelectAll();
            //            txtBottomprice.Clear();                       
            //            return;
            //        }

            //    }
            //    else
            //    {
            //        txtBottomprice.Clear();

            //    }
            //}
            //else
            //{
            //    MessageBox.Show("请检查是否未选择商品信息或商品未设置税率！", "程序提示");
            //    txtGoods.Focus();
            //    return;
            //}
        }

        private void txtCostprc_Leave(object sender, EventArgs e)
        {
            //if (!String.IsNullOrEmpty(Waredictinfo.Goodid) && !String.IsNullOrEmpty(Waredictinfo.Outrate))
            //{
            //    if (!String.IsNullOrEmpty(txtCostprc.Text))
            //    {
            //        try
            //        {
            //            txtCostprice.Text = (Math.Round(double.Parse(txtCostprc.Text.ToString()) / (1 + double.Parse(Waredictinfo.Outrate)), 6)).ToString();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("请检查输入的价格是否有误！", "程序提示");
            //            txtCostprc.SelectAll();
            //            txtCostprice.Clear();
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        txtCostprice.Clear();

            //    }
            //}
            //else
            //{
            //    MessageBox.Show("请检查是否未选择商品信息或商品未设置税率！", "程序提示");
            //    txtGoods.Focus();
            //    return;
            //}

            ////计算毛利率
            //if (!String.IsNullOrEmpty(txtPrc.Text) && !String.IsNullOrEmpty(txtCostprc.Text))
            //{
            //    try
            //    {
            //        txtCostrate.Text = (Math.Round(((double.Parse(txtPrc.Text.ToString()) - double.Parse(txtCostprc.Text.ToString())) / double.Parse(txtCostprc.Text.ToString())), 6)).ToString();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("请检查输入的价格是否有误！", "程序提示");
            //        txtCostrate.SelectAll();
            //        txtBottomprice.Clear();
            //        return;
            //    }
            //}
            //else
            //{
            //    txtBottomprice.Clear();

            //}
        }

        private void cbDeliveryfeerate_Leave(object sender, EventArgs e)
        {
            //if (!String.IsNullOrEmpty(Waredictinfo.Goodid) && !String.IsNullOrEmpty(Waredictinfo.Outrate))
            //{
            //    if (!String.IsNullOrEmpty(cbDeliveryfeerate.Value.ToString()))
            //    {
            //        try
            //        {
            //            txtPrc.Text = (double.Parse(txtBottomprc.Text.ToString()) * (1 + double.Parse(cbDeliveryfeerate.Value.ToString()))).ToString();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("请检查输入的价格是否有误！", "程序提示");
            //            txtCostprc.SelectAll();
            //            txtCostprice.Clear();
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        txtCostprice.Clear();

            //    }
            //}
            //else
            //{
            //    MessageBox.Show("请检查是否未选择商品信息或商品未设置税率！", "程序提示");
            //    txtGoods.Focus();
            //    return;
            //}
            //Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            //sqlkeydict.Add("Deliveryfeerate", "");
            //sqlkeydict.Add("Pic", "");
            ////sqlkeydict.Add("Costprc", "");
            ////sqlkeydict.Add("Bottomprc", "");
            ////sqlkeydict.Add("Costrate", "");

            //CalculatePrice(sqlkeydict);
        }

        private void cbDeliveryfeerate_ValueChanged(object sender, EventArgs e)
        {
            //if (!String.IsNullOrEmpty(Waredictinfo.Goodid) && !String.IsNullOrEmpty(Waredictinfo.Outrate))
            //{
            //    if (!String.IsNullOrEmpty(cbDeliveryfeerate.Value.ToString()))
            //    {
            //        try
            //        {
            //            txtPrc.Text = (double.Parse(txtBottomprc.Text.ToString()) * (1 + double.Parse(cbDeliveryfeerate.Value.ToString()))).ToString();
            //        }
            //        catch (Exception ex)
            //        {
            //        }
            //    }
            //    else
            //    {
            //        txtCostprice.Clear();

            //    }
            //}
            //else
            //{
            //    MessageBox.Show("请检查是否未选择商品信息或商品未设置税率！", "程序提示");
            //    txtGoods.Focus();
            //    return;
            //}
            //Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            //sqlkeydict.Add("Deliveryfeerate", "");
            //sqlkeydict.Add("Pic", "");
            //sqlkeydict.Add("Costprc", "");
            //sqlkeydict.Add("Bottomprc", "");
            //sqlkeydict.Add("Costrate", "");

            //CalculatePrice(sqlkeydict);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPrc.Text))
            {
                MessageBox.Show("请填写公开含税销价！", "程序提示");
                txtPrc.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtCostprc.Text))
            {
                MessageBox.Show("请填写含税成本销价！", "程序提示");
                txtCostprc.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtBottomprc.Text))
            {
                MessageBox.Show("请填写含税底价！", "程序提示");
                txtBottomprc.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtPrice.Text))
            {
                MessageBox.Show("请填写公开未税销价！", "程序提示");
                txtPrice.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtCostprice.Text))
            {
                MessageBox.Show("请填写未税成本销价！", "程序提示");
                txtCostprice.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtBottomprice.Text))
            {
                MessageBox.Show("请填写未税底价！", "程序提示");
                txtBottomprice.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtCostrate.Text))
            {
                MessageBox.Show("请填写考核毛利率！", "程序提示");
                txtCostrate.Focus();
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
                AddpriceGoodtap.Pgdeliveryfeerate = Math.Round(decimal.Parse(cbDeliveryfeerate.Value.ToString()),6).ToString();
                AddpriceGoodtap.Prc = txtPrc.Text.ToString();
                AddpriceGoodtap.Price = txtPrice.Text.ToString();
                AddpriceGoodtap.Costprctype = cbCostprctype.SelectedValue.ToString();
                AddpriceGoodtap.Costprctypename = cbCostprctype.Text.ToString();
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
      
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dtpStarttime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                
                    dtpEndtime.MinDate = dtpStarttime.Value.AddDays(1);
                    //dtpEndtime.MaxDate = dtpStarttime.Value.AddDays(1);
                    if (DeptConfigureDtoInfo.DefaultEnddate.Equals(null) || DeptConfigureDtoInfo.DefaultEnddate.Equals(""))
                    {
                        dtpEndtime.Text = dtpStarttime.Value.AddDays(int.Parse(DeptConfigureDtoInfo.ValidDays)).ToString("yyyy-MM-dd");
                    }
                    else {
                        dtpEndtime.Text = DateTime.Now.ToString(DeptConfigureDtoInfo.DefaultEnddate);
                    }
                
            }
            catch (Exception ex)
            {
                //dtpEndtime.MaxDate = dtpStarttime.Value;
                dtpEndtime.MinDate = dtpStarttime.Value.AddDays(1);
                if (DeptConfigureDtoInfo.DefaultEnddate.Equals(null) || DeptConfigureDtoInfo.DefaultEnddate.Equals(""))
                {
                    dtpEndtime.Text = dtpStarttime.Value.AddDays(int.Parse(DeptConfigureDtoInfo.ValidDays)).ToString("yyyy-MM-dd");
                }
                else
                {
                    dtpEndtime.Text = DateTime.Now.ToString(DeptConfigureDtoInfo.DefaultEnddate);
                }
            }
        }

        private void txtPrc_KeyUp(object sender, KeyEventArgs e)
        {
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            sqlkeydict.Add("Pic1", "");
            sqlkeydict.Add("Pic", "");
            //sqlkeydict.Add("Costprc", "");
            //sqlkeydict.Add("Bottomprc", "");
            sqlkeydict.Add("Costrate", "");

            CalculatePrice(sqlkeydict);
        }
        private void txtPrc_TextChanged(object sender, EventArgs e)
        {
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            sqlkeydict.Add("Pic1", "");
            //sqlkeydict.Add("Pic", "");
            //sqlkeydict.Add("Costprc", "");
            //sqlkeydict.Add("Bottomprc", "");
            sqlkeydict.Add("Costrate", "");

            CalculatePrice(sqlkeydict);
        }

        private void txtBottomprc_KeyUp(object sender, KeyEventArgs e)
        {
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            sqlkeydict.Add("Pic1", "");
            //sqlkeydict.Add("Costprc", "");
            sqlkeydict.Add("Bottomprc1", "");
            sqlkeydict.Add("Bottomprc", "");
            sqlkeydict.Add("Costrate", "");
            //sqlkeydict.Add("Deliveryfeerate", "");

            CalculatePrice(sqlkeydict);
        }

        private void txtCostprc_KeyUp(object sender, KeyEventArgs e)
        {
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();

            sqlkeydict.Add("Costprc", "");

            CalculatePrice(sqlkeydict);
        }

        private void cbDeliveryfeerate_KeyUp(object sender, KeyEventArgs e)
        {
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            
            sqlkeydict.Add("Deliveryfeerate", "");
            sqlkeydict.Add("Pic", "");
            //sqlkeydict.Add("Costprc", "");
            //sqlkeydict.Add("Bottomprc", "");
            //sqlkeydict.Add("Costrate", "");

            CalculatePrice(sqlkeydict);
        }

        private void cbDeliveryfeerate_MouseClick(object sender, MouseEventArgs e)
        {
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();

            sqlkeydict.Add("Deliveryfeerate", "");
            sqlkeydict.Add("Pic", "");
            //sqlkeydict.Add("Costprc", "");
            //sqlkeydict.Add("Bottomprc", "");
            //sqlkeydict.Add("Costrate", "");

            CalculatePrice(sqlkeydict);
        }

        private void cbDeliveryfeerate_MouseDown(object sender, MouseEventArgs e)
        {
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();

            sqlkeydict.Add("Deliveryfeerate", "");
            sqlkeydict.Add("Pic", "");
            //sqlkeydict.Add("Costprc", "");
            //sqlkeydict.Add("Bottomprc", "");
            //sqlkeydict.Add("Costrate", "");

            CalculatePrice(sqlkeydict);
        }

        private void cbDeliveryfeerate_MouseUp(object sender, MouseEventArgs e)
        {
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();

            sqlkeydict.Add("Deliveryfeerate", "");
            sqlkeydict.Add("Pic", "");
            //sqlkeydict.Add("Costprc", "");
            //sqlkeydict.Add("Bottomprc", "");
            //sqlkeydict.Add("Costrate", "");

            CalculatePrice(sqlkeydict);
        }

        private void btnModfiy_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPrc.Text))
            {
                MessageBox.Show("请填写公开含税销价！", "程序提示");
                txtPrc.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtCostprc.Text))
            {
                MessageBox.Show("请填写含税成本销价！", "程序提示");
                txtCostprc.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtBottomprc.Text))
            {
                MessageBox.Show("请填写含税底价！", "程序提示");
                txtBottomprc.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtPrice.Text))
            {
                MessageBox.Show("请填写公开未税销价！", "程序提示");
                txtPrice.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtCostprice.Text))
            {
                MessageBox.Show("请填写未税成本销价！", "程序提示");
                txtCostprice.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtBottomprice.Text))
            {
                MessageBox.Show("请填写未税底价！", "程序提示");
                txtBottomprice.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtCostrate.Text))
            {
                MessageBox.Show("请填写考核毛利率！", "程序提示");
                txtCostrate.Focus();
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

    

        

       

        

        

   


    }
}

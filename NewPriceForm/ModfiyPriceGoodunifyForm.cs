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
    public partial class ModfiyPriceGoodunifyForm : Form
    {
        public int stateUI = 0;
        private string Defaltdept = "";//默认部门
        public SelPubWaredict Waredictinfo;
        public ScmPriceGoodunify Selpricegoodunfiy;

        APDao_GoodsClientPrice dao = null;
        PMSystemDao Pdao = new PMSystemDao();
        private SortableBindingList<GoodunifyDeptmenu> DeptList = new SortableBindingList<GoodunifyDeptmenu>();
        private SortableBindingList<SysCode> stopflagList = new SortableBindingList<SysCode>();
        private DeptConfigureDto DeptConfigureDtoInfo = new DeptConfigureDto();
        public ModfiyPriceGoodunifyForm()
        {
            InitializeComponent();
        }

        private void ModfiyPriceGoodunifyForm_Load(object sender, EventArgs e)
        {
            dao = (APDao_GoodsClientPrice)this.Tag;
            initUI(stateUI);


            txtGoods.Text = Waredictinfo.Goods;

            DeptList = dao.GetGoodunifyDeptmenuList(int.Parse(Properties.Settings.Default.COMPID), int.Parse(Properties.Settings.Default.OWNERID));
            cbDefaultdept.DataSource = DeptList;
            cbDefaultdept.DisplayMember = "Deptname";
            cbDefaultdept.ValueMember = "Saledeptid";

            if (PubOwnerConfigureDto.PrcDept == "1")
            {
                try
                {
                    Defaltdept = dao.FGetGoodunifyDefaultdept(int.Parse(Properties.Settings.Default.COMPID), int.Parse(Properties.Settings.Default.OWNERID), int.Parse(Waredictinfo.Goodid));
                    cbDefaultdept.SelectedValue = Defaltdept;
                }
                catch (Exception ex) { }
            }
            DeptConfigureDtoInfo = Pdao.GetDeptConfigureInfo(Defaltdept);
            dtpStarttime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtpEndtime.MinDate = dtpStarttime.Value.AddDays(1);
            //dtpEndtime.MaxDate = dtpStarttime.Value.AddDays(1);
            if (DeptConfigureDtoInfo.DefaultEnddate == null || DeptConfigureDtoInfo.DefaultEnddate == "")
            {
                if (DeptConfigureDtoInfo.ValidDays == null || DeptConfigureDtoInfo.ValidDays == "")
                {
                    dtpEndtime.Text = DateTime.Now.ToString(dtpStarttime.Value.AddDays(1).ToString("yyyy-MM-dd"));
                }
                else
                {
                    dtpEndtime.Text = dtpStarttime.Value.AddDays(int.Parse(DeptConfigureDtoInfo.ValidDays)).ToString("yyyy-MM-dd");
                }
            }
            else
            {
                dtpEndtime.Text = DateTime.Now.ToString(DeptConfigureDtoInfo.DefaultEnddate);
            }
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            sqlkeydict.Add("Pic", "");
            sqlkeydict.Add("Costprc", "");
            sqlkeydict.Add("Bottomprc", "");
            sqlkeydict.Add("Costrate", "");

            CalculatePrice(sqlkeydict);
        }

        private void initUI(int astate)
        {
            switch (astate)
            {
                case 1://修改界面
                    txtPrc.Text = Selpricegoodunfiy.Prc;
                    txtPrice.Enabled = false;
                    txtPrc.Text = Selpricegoodunfiy.Prc;
                    txtPrc.Focus();
                    txtBottomprc.Text = Selpricegoodunfiy.Bottomprc;
                    txtBottomprc.Focus();
                    txtBottomprice.Enabled = false;
                    txtCostprc.Text = Selpricegoodunfiy.Costprc;
                    txtCostprice.Enabled = false;
                    txtCostprc.Focus();
                    txtCostrate.Enabled = false;
                    btnModfiy.Visible = true;
                    label26.Visible = true;
                    cbShopflag.Visible = true;
                    Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
                    sqlkeydict.Add("typeid", "1");
                    stopflagList = Pdao.GetSysCode(sqlkeydict);

                    cbShopflag.DataSource = stopflagList;
                    cbShopflag.DisplayMember = "Name";
                    cbShopflag.ValueMember = "Code";

                    txtGoods.Focus();
                    break;

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
                    if (kv.Key == "Pic")
                    {
                        if (!String.IsNullOrEmpty(Waredictinfo.Goodid) && !String.IsNullOrEmpty(Waredictinfo.Outrate))
                        {
                            if (!String.IsNullOrEmpty(txtPrc.Text.ToString()))
                            {
                                try
                                {
                                    txtPrice.Text = (Math.Round(double.Parse(txtPrc.Text.ToString()) / (1 + double.Parse(Waredictinfo.Outrate)), 6)).ToString();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("请检查输入的价格是否有误！", "程序提示");
                                    //txtPrc.SelectAll();
                                    txtPrc.Clear();
                                    txtPrice.Clear();
                                    txtCostrate.Clear();
                                }
                            }
                            else
                            {
                                txtPrice.Clear();
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
                    if (kv.Key == "Bottomprc")
                    {
                        if (!String.IsNullOrEmpty(Waredictinfo.Goodid) && !String.IsNullOrEmpty(Waredictinfo.Outrate))
                        {
                            if (!String.IsNullOrEmpty(txtBottomprc.Text.ToString()))
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
                                    txtCostrate.Clear();
                                }
                            }
                            else
                            {
                                txtBottomprice.Clear();
                                txtCostrate.Clear();

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
                            }
                        }
                        else
                        {

                            txtCostrate.Clear();
                        }
                    }
                }

            }



        }

        private void btnModfiy_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtBottomprc.Text))
            {
                MessageBox.Show("请填写含税底价！", "程序提示");
                txtBottomprc.Focus();
                return;
            }
   
            SPRetInfo retinfo = new SPRetInfo();
            string starttime = dtpStarttime.Value.ToString("yyyy-MM-dd");
            string endtime = dtpEndtime.Value.ToString("yyyy-MM-dd");
            dao.PScmGoodunifyUpdate(int.Parse(Properties.Settings.Default.COMPID), int.Parse(Properties.Settings.Default.OWNERID), int.Parse(cbDefaultdept.SelectedValue.ToString()), int.Parse(Waredictinfo.Goodid), double.Parse(txtPrc.Text.ToString()), double.Parse(txtPrice.Text.ToString()), cbShopflag.SelectedValue.ToString(), int.Parse(SessionDto.Empid), double.Parse(txtCostprc.Text.ToString()), double.Parse(txtCostprice.Text.ToString()), double.Parse(txtCostrate.Text.ToString()), starttime, endtime, double.Parse(txtBottomprc.Text.ToString()), double.Parse(txtBottomprice.Text.ToString()), "01", retinfo);
            if (retinfo.num == "1")
            {

                MessageBox.Show("提交成功！" + retinfo.msg + retinfo.result, "后台提示");
                return;
            }
            else
            {
                MessageBox.Show("提交失败！" + retinfo.msg + retinfo.result, "后台提示");
                return;
            }
        }

        private void dtpStarttime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dtpEndtime.MinDate = dtpStarttime.Value.AddDays(1);
                //dtpEndtime.MaxDate = dtpStarttime.Value.AddDays(1);
                if (DeptConfigureDtoInfo.DefaultEnddate == null || DeptConfigureDtoInfo.DefaultEnddate == "")
                {
                    if (DeptConfigureDtoInfo.ValidDays == null || DeptConfigureDtoInfo.ValidDays == "")
                    {
                        dtpEndtime.Text = DateTime.Now.ToString(dtpStarttime.Value.AddDays(1).ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        dtpEndtime.Text = dtpStarttime.Value.AddDays(int.Parse(DeptConfigureDtoInfo.ValidDays)).ToString("yyyy-MM-dd");
                    }
                }
                else
                {
                    dtpEndtime.Text = DateTime.Now.ToString(DeptConfigureDtoInfo.DefaultEnddate);
                }
            }
            catch (Exception ex)
            {
                //dtpEndtime.MaxDate = dtpStarttime.Value;
                dtpEndtime.MinDate = dtpStarttime.Value.AddDays(1);

                if (DeptConfigureDtoInfo.DefaultEnddate == null || DeptConfigureDtoInfo.DefaultEnddate == "")
                {
                    if (DeptConfigureDtoInfo.ValidDays == null || DeptConfigureDtoInfo.ValidDays == "")
                    {
                        dtpEndtime.Text = DateTime.Now.ToString(dtpStarttime.Value.AddDays(1).ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        dtpEndtime.Text = dtpStarttime.Value.AddDays(int.Parse(DeptConfigureDtoInfo.ValidDays)).ToString("yyyy-MM-dd");
                    }
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
            sqlkeydict.Add("Pic", txtPrc.Text.ToString());
            sqlkeydict.Add("Costprc", txtCostprc.Text.ToString());
            sqlkeydict.Add("Bottomprc", txtBottomprc.Text.ToString());
            sqlkeydict.Add("Costrate", "");

            CalculatePrice(sqlkeydict);
        }

        private void txtCostprc_KeyUp(object sender, KeyEventArgs e)
        {
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            sqlkeydict.Add("Pic", txtPrc.Text.ToString());
            sqlkeydict.Add("Costprc", txtCostprc.Text.ToString());
            sqlkeydict.Add("Bottomprc", txtBottomprc.Text.ToString());
            sqlkeydict.Add("Costrate", "");

            CalculatePrice(sqlkeydict);
        }

        private void txtBottomprc_KeyUp(object sender, KeyEventArgs e)
        {
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            sqlkeydict.Add("Pic", txtPrc.Text.ToString());
            sqlkeydict.Add("Costprc", txtCostprc.Text.ToString());
            sqlkeydict.Add("Bottomprc", txtBottomprc.Text.ToString());
            sqlkeydict.Add("Costrate", "");

            CalculatePrice(sqlkeydict);
        }

        private void cbDefaultdept_TextChanged(object sender, EventArgs e)
        {
            Defaltdept = cbDefaultdept.SelectedValue.ToString();
            DeptConfigureDtoInfo = Pdao.GetDeptConfigureInfo(Defaltdept);
            dtpStarttime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtpEndtime.MinDate = dtpStarttime.Value.AddDays(1);
            //dtpEndtime.MaxDate = dtpStarttime.Value.AddDays(1);
            if (DeptConfigureDtoInfo.DefaultEnddate == null || DeptConfigureDtoInfo.DefaultEnddate == "")
            {
                if (DeptConfigureDtoInfo.ValidDays == null || DeptConfigureDtoInfo.ValidDays == "")
                {
                    dtpEndtime.Text = DateTime.Now.ToString(dtpStarttime.Value.AddDays(1).ToString("yyyy-MM-dd"));
                }
                else
                {
                    dtpEndtime.Text = dtpStarttime.Value.AddDays(int.Parse(DeptConfigureDtoInfo.ValidDays)).ToString("yyyy-MM-dd");
                }
            }
            else
            {
                dtpEndtime.Text = DateTime.Now.ToString(DeptConfigureDtoInfo.DefaultEnddate);
            }
        }


    }
}

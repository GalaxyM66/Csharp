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
    public partial class SetPriceByGoodsForm : Form
    {
        PMBaseDao dao = new PMBaseDao();
        PMPriceDao priceDao = new PMPriceDao();
        SortableBindingList<PubWaredict> goodsList = new SortableBindingList<PubWaredict>();
        ChannelPrice priceResult = new ChannelPrice();
        public PubClients clients = null;
        LowPrice suggestPrice = new LowPrice();
        decimal outRate = 0;

        public SetPriceByGoodsForm()
        {
            InitializeComponent();
        }

        private void SetPriceByGoodsForm_Load(object sender, EventArgs e)
        {
            //设置日期
            beginDate.Format = DateTimePickerFormat.Custom;
            beginDate.CustomFormat = "yyyy-MM-dd";
            endDate.Format = DateTimePickerFormat.Custom;
            endDate.CustomFormat = "yyyy-MM-dd";
            beginDate.Value = DateTime.Now;
            endDate.Value = DateTime.Now.AddDays(30);
            //--是否议价
            FormUtils.SetComboBox(bargain, StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("2")), "Name", "TagPtr");
            //--价格类型
            FormUtils.SetComboBox(isCredit, StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("3")), "Name", "TagPtr");

            //--声明事件,禁止鼠标滚轮操作
            prcUpDown.MouseWheel += new MouseEventHandler(FormUtils.Num_DiscountAmount_MouseWheel);
            costPrcUpDown.MouseWheel += new MouseEventHandler(FormUtils.Num_DiscountAmount_MouseWheel);
            bottomPrcUpDown.MouseWheel += new MouseEventHandler(FormUtils.Num_DiscountAmount_MouseWheel);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (prcUpDown.Value == 0 || priceUpDown.Value == 0 ||
                costPrcUpDown.Value == 0 || costPriceUpDown.Value == 0 ||
                bottomPrcUpDown.Value == 0 || bottomPriceUpDown.Value == 0 ||
                costRateUpDown.Value > 1 || FormUtils.SelectRow(selectGoodsDgv) == null)
            {
                MessageBox.Show("参数错误");
                return;
            }
            PriceSave();
            priceResult.Cstid = clients.Cstid;
            string value = priceResult.GetString("");
            string[] result = priceDao.SavePrice(value, "", "executed");
            if ("1".Equals(result[0]))
                MessageBox.Show("成功!!");
            else
                MessageBox.Show(result[1]);
        }

        private void PriceSave()
        {
            priceResult.Costprice = costPriceUpDown.Value.ToString();
            priceResult.Costprc = costPrcUpDown.Value.ToString();
            priceResult.Prc = prcUpDown.Value.ToString();
            priceResult.Price = priceUpDown.Value.ToString();
            priceResult.Costrate = costRateUpDown.Value.ToString();
            priceResult.Iscredit = ((Dictionary)isCredit.SelectedValue).Code;
            priceResult.Bargain = ((Dictionary)bargain.SelectedValue).Code;
            priceResult.Begindate = beginDate.Text.Trim();
            priceResult.Enddate = endDate.Text.Trim();
            priceResult.Bottomprc = bottomPrcUpDown.Value.ToString();
            priceResult.Bottomprice = bottomPriceUpDown.Value.ToString();
        }
       

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            GoodsGroups cbo = new GoodsGroups();
            if (goodsGroup.SelectedValue != null)
                cbo = (GoodsGroups)(goodsGroup.SelectedValue);
            goodsList = StringUtils.TableToEntity<PubWaredict>(dao.GetGoodsByGroupId(goodsCode.Text.Trim(), goodsName.Text.Trim(), cbo.Id, cbo.Type, clients.Clienttypegroupname));
            FormUtils.RefreshDataGridView(selectGoodsDgv, goodsList);
        }

        private void GoodsGroup_Click(object sender, EventArgs e)
        {
            FormUtils.GroupComboBoxSetting(sender, "goods");
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow(selectGoodsDgv);
            if (obj == null) return;
            PubWaredict goods = (PubWaredict)obj;
            suggestPrice = priceDao.GetSuggestValue(goods.Goodid, clients.Clienttypegroup);
            SetSuggestValue(suggestPrice);
            priceResult.Compid = Properties.Settings.Default.COMPID;
            priceResult.Ownerid = Properties.Settings.Default.OWNERID;
            priceResult.Saledeptid = SessionDto.Empdeptid;
            priceResult.Goodid = goods.Goodid;
            priceResult.Stopflag = "00";
            priceResult.Suggestbottomprc = suggestPrice.Prc;
            priceResult.Suggestcostprc = suggestPrice.Costprc;
            priceResult.Suggestexecprc = suggestPrice.Suggestexecprc;
            priceResult.Source = "01";
            outRate = StringUtils.ToDecimal(goods.Outrate);
        }

        private void SetSuggestValue(LowPrice lowPrice)
        {
            prcUpDown.Value = StringUtils.ToDecimal(lowPrice.Suggestexecprc);
            costPrcUpDown.Value = StringUtils.ToDecimal(lowPrice.Costprc);
            bottomPrcUpDown.Value = StringUtils.ToDecimal(lowPrice.Prc);
            priceUpDown.Value = StringUtils.ToDecimal(lowPrice.Suggestexecprice);
            costPriceUpDown.Value = StringUtils.ToDecimal(lowPrice.Costprice);
            bottomPriceUpDown.Value = StringUtils.ToDecimal(lowPrice.Price);
            costRateUpDown.Value = StringUtils.ToDecimal(lowPrice.Costrate);
            bargain.SelectedIndex = bargain.FindString(StringUtils.IsNull(lowPrice.Ismodifyexecname) ? "是" : lowPrice.Ismodifyexecname);
        }

        private void Default_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryBtn_Click(sender, e);
            }
        }

        private void BottomPrcUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (bottomPrcUpDown.Value > prcUpDown.Value || bottomPrcUpDown.Value < costPrcUpDown.Value)
            {
                bottomPrcUpDown.Value = 0;
                return;
            }
            bottomPriceUpDown.Value = bottomPrcUpDown.Value / (1 + outRate);
        }

        private void CostPrcUpDown_ValueChanged(object sender, EventArgs e)
        {
            costPriceUpDown.Value = costPrcUpDown.Value / (1 + outRate);
            SetCostRate();
        }

        private void PrcUpDown_ValueChanged(object sender, EventArgs e)
        {
            priceUpDown.Value = prcUpDown.Value / (1 + outRate);
            SetCostRate();
        }

        private void SetCostRate()
        {
            if (costPrcUpDown.Value == 0 || prcUpDown.Value == 0 || prcUpDown.Value < costPrcUpDown.Value)
            {
                costRateUpDown.Value = 0;
                return;
            }
            costRateUpDown.Value = 1 - (costPrcUpDown.Value / prcUpDown.Value);
        }

        private void UpDown_Selected(object sender, EventArgs e)
        {
            ((NumericUpDown)sender).Select(0, ((NumericUpDown)sender).Value.ToString().Length);
        }
    }
}

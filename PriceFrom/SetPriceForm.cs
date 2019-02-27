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
    public partial class SetPriceForm : Form
    {
        public ChannelPrice priceResult = new ChannelPrice();
        PMBaseDao dao = new PMBaseDao();
        public SetPriceForm()
        {
            InitializeComponent();
        }

        private void SetPriceForm_Load(object sender, EventArgs e)
        {
            //设置日期
            beginDate.Format = DateTimePickerFormat.Custom;
            beginDate.CustomFormat = "yyyy-MM-dd";
            endDate.Format = DateTimePickerFormat.Custom;
            endDate.CustomFormat = "yyyy-MM-dd";
            beginDate.Value = DateTime.Now;
            endDate.Value = DateTime.Now.AddDays(30);
            //--是否议价
            FormUtils.SetComboBox(bargain_offType, StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("2")), "Name", "TagPtr");
            //--价格类型
            FormUtils.SetComboBox(isCredit_offLevel, StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("3")), "Name", "TagPtr");
            SetValue(priceResult);

            //--声明事件,禁止鼠标滚轮操作
            prcUpDown.MouseWheel += new MouseEventHandler(FormUtils.Num_DiscountAmount_MouseWheel);
            costPrcUpDown.MouseWheel += new MouseEventHandler(FormUtils.Num_DiscountAmount_MouseWheel);
            bottomPrcUpDown.MouseWheel += new MouseEventHandler(FormUtils.Num_DiscountAmount_MouseWheel);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (prcUpDown.Value == 0 || priceUpDown.Value == 0 ||
                costPrcUpDown.Value == 0 || costPriceUpDown.Value == 0 ||
                bottomPrcUpDown.Value == 0)
            {
                MessageBox.Show("参数错误");
                return;
            }
            PriceSave();
            DialogResult = DialogResult.OK;
            Close();
        }
        private void PriceSave()
        {
            priceResult.Costprice = costPriceUpDown.Value.ToString();
            priceResult.Costprc = costPrcUpDown.Value.ToString();
            priceResult.Prc = prcUpDown.Value.ToString();
            priceResult.Price = priceUpDown.Value.ToString();
            priceResult.Costrate = costRateUpDown.Value.ToString();
            priceResult.Iscredit = ((Dictionary)isCredit_offLevel.SelectedValue).Code;
            priceResult.Bargain = ((Dictionary)bargain_offType.SelectedValue).Code;
            //priceResult.Begindate = beginDate.Text.Trim();
            priceResult.Enddate = endDate.Text.Trim();
            priceResult.Bottomprc = bottomPrcUpDown.Value.ToString();
            priceResult.Bottomprice = bottomPriceUpDown.Value.ToString();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void SetValue(ChannelPrice price)
        {
            costPriceUpDown.Value = Convert.ToDecimal(price.Costprice);
            costPrcUpDown.Value = Convert.ToDecimal(price.Costprc);
            prcUpDown.Value = Convert.ToDecimal(price.Prc);
            priceUpDown.Value = Convert.ToDecimal(price.Price);
            costRateUpDown.Value = Convert.ToDecimal(price.Costrate);
            isCredit_offLevel.SelectedIndex = isCredit_offLevel.FindString(price.Iscreditname);
            bargain_offType.SelectedIndex = bargain_offType.FindString(price.Bargainname);
            beginDate.Text = price.Begindate;
            endDate.Text = price.Enddate;
            bottomPrcUpDown.Value = Convert.ToDecimal(priceResult.Bottomprc);
            bottomPriceUpDown.Value = Convert.ToDecimal(priceResult.Bottomprice);
            suggestExecPrcUpDown.Value = Convert.ToDecimal(priceResult.Suggestexecprc);
            suggestCostprcUpDown.Value = Convert.ToDecimal(priceResult.Suggestcostprc);
            suggestBottomprcUpDown.Value = Convert.ToDecimal(priceResult.Suggestbottomprc);
        }

        private void BottomPrcUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (bottomPrcUpDown.Value > prcUpDown.Value || bottomPrcUpDown.Value < costPrcUpDown.Value)
            {
                bottomPrcUpDown.Value = 0;
                return;
            }
            bottomPriceUpDown.Value = bottomPrcUpDown.Value / (1 + StringUtils.ToDecimal(priceResult.Outrate));
        }

        private void CostPrcUpDown_ValueChanged(object sender, EventArgs e)
        {
            costPriceUpDown.Value = costPrcUpDown.Value / (1 + StringUtils.ToDecimal(priceResult.Outrate));
            SetCostRate();
        }

        private void PrcUpDown_ValueChanged(object sender, EventArgs e)
        {
            priceUpDown.Value = prcUpDown.Value / (1 + StringUtils.ToDecimal(priceResult.Outrate));
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

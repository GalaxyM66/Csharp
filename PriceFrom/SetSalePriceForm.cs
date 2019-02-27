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
    public partial class SetSalePriceForm : Form
    {
        public SalePrice salePriceResult = new SalePrice();
        PMBaseDao dao = new PMBaseDao();
        public SetSalePriceForm()
        {
            InitializeComponent();
        }

        private void SetSalePriceForm_Load(object sender, EventArgs e)
        {
            //设置日期
            beginDate.Format = DateTimePickerFormat.Custom;
            beginDate.CustomFormat = "yyyy-MM-dd";
            endDate.Format = DateTimePickerFormat.Custom;
            endDate.CustomFormat = "yyyy-MM-dd";
            beginDate.Value = DateTime.Now;
            endDate.Value = DateTime.Now.AddDays(30);

            FormUtils.SetComboBox(offTypeCbo, StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("6")), "Name", "TagPtr");
            FormUtils.SetComboBox(offLevelCbo, StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("7")), "Name", "TagPtr");

            //--声明事件,禁止鼠标滚轮操作
            prcUpDown.MouseWheel += new MouseEventHandler(FormUtils.Num_DiscountAmount_MouseWheel);
            costPrcUpDown.MouseWheel += new MouseEventHandler(FormUtils.Num_DiscountAmount_MouseWheel);

            SetSaleValue(salePriceResult);
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (prcUpDown.Value == 0 || priceUpDown.Value == 0 || costPrcUpDown.Value == 0 || costPriceUpDown.Value == 0)
            {
                MessageBox.Show("不可设为0");
                return;
            }
            SalePriceSave();
            DialogResult = DialogResult.OK;
            Close();
        }

        public void SetSaleValue(SalePrice saleprice)
        {
            costPriceUpDown.Value = Convert.ToDecimal(saleprice.Costprice);
            costPrcUpDown.Value = Convert.ToDecimal(saleprice.Costprc);
            prcUpDown.Value = Convert.ToDecimal(saleprice.Prc);
            priceUpDown.Value = Convert.ToDecimal(saleprice.Price);
            costRateUpDown.Value = Convert.ToDecimal(saleprice.Costrate);
            offLevelCbo.SelectedIndex = offLevelCbo.FindString(saleprice.Offlevelname);
            offTypeCbo.SelectedIndex = offTypeCbo.FindString(saleprice.Offtypename);
            beginDate.Text = saleprice.Begindate;
            endDate.Text = saleprice.Enddate;
        }

        private void SalePriceSave()
        {
            salePriceResult.Costprice = costPriceUpDown.Value.ToString();
            salePriceResult.Costprc = costPrcUpDown.Value.ToString();
            salePriceResult.Prc = prcUpDown.Value.ToString();
            salePriceResult.Price = priceUpDown.Value.ToString();
            salePriceResult.Costrate = costRateUpDown.Value.ToString();
            salePriceResult.Offlevel = ((Dictionary)offLevelCbo.SelectedValue).Code;
            salePriceResult.Offtype = ((Dictionary)offTypeCbo.SelectedValue).Code;
            //salePriceResult.Begindate = beginDate.Text.Trim();
            salePriceResult.Enddate = endDate.Text.Trim();
        }

        private void PrcUpDown_ValueChanged(object sender, EventArgs e)
        {
            priceUpDown.Value = prcUpDown.Value / (1 + StringUtils.ToDecimal(salePriceResult.Outrate));
            SetCostRate();
        }

        private void CostPrcUpDown_ValueChanged(object sender, EventArgs e)
        {
            costPriceUpDown.Value = costPrcUpDown.Value / (1 + StringUtils.ToDecimal(salePriceResult.Outrate));
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
    }
}

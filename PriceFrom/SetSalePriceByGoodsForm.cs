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
    public partial class SetSalePriceByGoodsForm : Form
    {
        PMBaseDao dao = new PMBaseDao();
        PMPriceDao priceDao = new PMPriceDao();
        SortableBindingList<PubWaredict> goodsList = new SortableBindingList<PubWaredict>();
        SalePrice salePriceResult = new SalePrice();
        public PubClients clients = null;
        decimal outRate = 0;
        public SetSalePriceByGoodsForm()
        {
            InitializeComponent();
        }

        private void SelectGoodsDgv_SelectionChanged(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow(selectGoodsDgv1);
            if (obj == null) return;
            PubWaredict goods = (PubWaredict)obj;
            salePriceResult.Compid = Properties.Settings.Default.COMPID;
            salePriceResult.Ownerid = Properties.Settings.Default.OWNERID;
            salePriceResult.Goodid = goods.Goodid;
            salePriceResult.Stopflag = "00";
            salePriceResult.Saledeptid = SessionDto.Empdeptid;
            outRate = StringUtils.ToDecimal(goods.Outrate);
        }

        private void SetSalePriceByGoodsForm_Load(object sender, EventArgs e)
        {
            //设置日期
            beginDate.Format = DateTimePickerFormat.Custom;
            beginDate.CustomFormat = "yyyy-MM-dd";
            endDate.Format = DateTimePickerFormat.Custom;
            endDate.CustomFormat = "yyyy-MM-dd";
            beginDate.Value = DateTime.Now;
            endDate.Value = DateTime.Now.AddDays(30);

            //--促销级别
            FormUtils.SetComboBox(offLevelCbo, StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("7")), "Name", "TagPtr");
            //--促销类型
            FormUtils.SetComboBox(offTypeCbo, StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("6")), "Name", "TagPtr");

            //--声明事件,禁止鼠标滚轮操作
            prcUpDown.MouseWheel += new MouseEventHandler(FormUtils.Num_DiscountAmount_MouseWheel);
            costPrcUpDown.MouseWheel += new MouseEventHandler(FormUtils.Num_DiscountAmount_MouseWheel);
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            GoodsGroups cbo = new GoodsGroups();
            if (goodsGroup.SelectedValue != null)
                cbo = (GoodsGroups)(goodsGroup.SelectedValue);
            goodsList = StringUtils.TableToEntity<PubWaredict>(dao.GetGoodsByGroupId(goodsCode.Text.Trim(), goodsName.Text.Trim(), cbo.Id, cbo.Type, clients.Clienttypegroupname));
            FormUtils.RefreshDataGridView(selectGoodsDgv1, goodsList);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (prcUpDown.Value == 0 || priceUpDown.Value == 0 ||
                costPrcUpDown.Value == 0 || costPriceUpDown.Value == 0 || 
                FormUtils.SelectRow(selectGoodsDgv1) == null)
            {
                MessageBox.Show("参数错误");
                return;
            }
            SalePriceSave();
            salePriceResult.Cstid = clients.Cstid;
            string value = salePriceResult.GetString("");
            string[] result = priceDao.SavePrice(value, "", "off");
            if ("1".Equals(result[0]))
                MessageBox.Show("成功!!");
            else
                MessageBox.Show(result[1]);
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
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
            salePriceResult.Begindate = beginDate.Text.Trim();
            salePriceResult.Enddate = endDate.Text.Trim();
        }

        private void GoodsGroup_Click(object sender, EventArgs e)
        {
            FormUtils.GroupComboBoxSetting(sender, "goods");
        }

        private void Default_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryBtn_Click(sender, e);
            }
        }

        private void PrcUpDown_ValueChanged(object sender, EventArgs e)
        {
            priceUpDown.Value = prcUpDown.Value / (1 + outRate);
            SetCostRate();
        }

        private void CostPrcUpDown_ValueChanged(object sender, EventArgs e)
        {
            costPriceUpDown.Value = costPrcUpDown.Value / (1 + outRate);
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

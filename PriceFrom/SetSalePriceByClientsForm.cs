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
    public partial class SetSalePriceByClientsForm : Form
    {
        PMBaseDao dao = new PMBaseDao();
        PMPriceDao priceDao = new PMPriceDao();
        SortableBindingList<PubClients> clientsList = new SortableBindingList<PubClients>();
        SortableBindingList<SalePrice> saleListResult = new SortableBindingList<SalePrice>();
        public PubWaredict goods = null;
        ResultInfo resultMsg = new ResultInfo();
        public SetSalePriceByClientsForm()
        {
            InitializeComponent();
        }

        private void SetSalePriceByClientsForm_Load(object sender, EventArgs e)
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
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            ClientsGroups cbo = new ClientsGroups();
            if (clientsGroup.SelectedValue != null)
                cbo = (ClientsGroups)(clientsGroup.SelectedValue);
            clientsList = StringUtils.TableToEntity<PubClients>(dao.GetClientsByGroupId(clientsCode.Text.Trim(), clientsName.Text.Trim(), cbo.Id, cbo.Type, ""));
            FormUtils.RefreshDataGridView(selectClientsDgv1, clientsList);
        }

        private void ClientsGroup_Click(object sender, EventArgs e)
        {
            FormUtils.GroupComboBoxSetting(sender, "clients");
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (prcUpDown.Value == 0 || priceUpDown.Value == 0 ||
                costPrcUpDown.Value == 0 || costPriceUpDown.Value == 0 ||
                selectClientsDgv1.DataSource == null)
            {
                MessageBox.Show("参数错误");
                return;
            }
            SalePriceSave();
            foreach (var salePrice in saleListResult)
            {
                string value = salePrice.GetString("");
                string[] result = priceDao.SavePrice(value, "", "off");
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
        }

        private void Default_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryBtn_Click(sender, e);
            }
        }

        private void SalePriceSave()
        {
            saleListResult.Clear();
            SalePrice salePrice = null;
            object obj = FormUtils.SelectRows(selectClientsDgv1);
            if (obj == null) return;
            foreach (DataGridViewRow dgvr in (DataGridViewSelectedRowCollection)obj)
            {
                salePrice = new SalePrice()
                {
                    Compid = Properties.Settings.Default.COMPID,
                    Ownerid = Properties.Settings.Default.OWNERID,
                    Saledeptid = SessionDto.Empdeptid,
                    Cstid = ((PubClients)dgvr.Cells[0].Value).Cstid,
                    Goodid = goods.Goodid,
                    Stopflag = "00",
                    Costprice = costPriceUpDown.Value.ToString(),
                    Costprc = costPrcUpDown.Value.ToString(),
                    Prc = prcUpDown.Value.ToString(),
                    Price = priceUpDown.Value.ToString(),
                    Costrate = costRateUpDown.Value.ToString(),
                    Offlevel = ((Dictionary)offLevelCbo.SelectedValue).Code,
                    Offtype = ((Dictionary)offTypeCbo.SelectedValue).Code,
                    Begindate = beginDate.Text.Trim(),
                    Enddate = endDate.Text.Trim(),
                };
                saleListResult.Add(salePrice);
            }
        }

        private void PrcUpDown_ValueChanged(object sender, EventArgs e)
        {
            priceUpDown.Value = prcUpDown.Value / (1 + StringUtils.ToDecimal(goods.Outrate));
            SetCostRate();
        }

        private void CostPrcUpDown_ValueChanged(object sender, EventArgs e)
        {
            costPriceUpDown.Value = costPrcUpDown.Value / (1 + StringUtils.ToDecimal(goods.Outrate));
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

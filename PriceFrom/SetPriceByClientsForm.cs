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
    public partial class SetPriceByClientsForm : Form
    {
        PMBaseDao dao = new PMBaseDao();
        PMPriceDao priceDao = new PMPriceDao();
        SortableBindingList<PubClients> clientsList = new SortableBindingList<PubClients>();
        SortableBindingList<ChannelPrice> listResult = new SortableBindingList<ChannelPrice>();
        public PubWaredict selectGoods = null;
        LowPrice suggestPrice = null;
        ResultInfo resultMsg = new ResultInfo();
        public SetPriceByClientsForm()
        {
            InitializeComponent();
        }

        private void SetPriceByClientsForm_Load(object sender, EventArgs e)
        {
            //设置日期
            beginDate.Format = DateTimePickerFormat.Custom;
            beginDate.CustomFormat = "yyyy-MM-dd";
            endDate.Format = DateTimePickerFormat.Custom;
            endDate.CustomFormat = "yyyy-MM-dd";
            beginDate.Value = DateTime.Now;
            endDate.Value = DateTime.Now.AddDays(30);
            PriceSet();
            //--声明事件,禁止鼠标滚轮操作
            prcUpDown.MouseWheel += new MouseEventHandler(FormUtils.Num_DiscountAmount_MouseWheel);
            costPrcUpDown.MouseWheel += new MouseEventHandler(FormUtils.Num_DiscountAmount_MouseWheel);
            bottomPrcUpDown.MouseWheel += new MouseEventHandler(FormUtils.Num_DiscountAmount_MouseWheel);
        }

        private void PriceSet()
        {
            //--是否议价
            FormUtils.SetComboBox(bargain, StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("2")), "Name", "TagPtr");
            //--价格类型
            FormUtils.SetComboBox(isCredit, StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("3")), "Name", "TagPtr");
            //--客户类型组
            if (StringUtils.IsNotNull(selectGoods.Limitcsttype))
            {
                SortableBindingList<GoodsSub> clientTypeGroupList = StringUtils.TableToEntity<GoodsSub>(dao.GetGoodsSub(selectGoods.Goodid, "13"));
                int i = 0;
                string allValue = "";
                foreach (var ctg in clientTypeGroupList)
                {
                    allValue += FormUtils.AddMarks(ctg.Subid, i);
                    i++;
                }
                clientTypeGroupList.Insert(0, new GoodsSub { Subid = allValue, Subname = "全部" });
                FormUtils.SetComboBox(clientTypeGroup, clientTypeGroupList, "Subname", "TagPtr");
            }
            else
            {
               SortableBindingList<Dictionary> clientTypeGroupList =  StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("13"));
                clientTypeGroupList.Insert(0, new Dictionary { Name = "全部", Code = "" });
                FormUtils.SetComboBox(clientTypeGroup, clientTypeGroupList, "Name", "TagPtr");
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (prcUpDown.Value == 0 || priceUpDown.Value == 0 ||
                costPrcUpDown.Value == 0 || costPriceUpDown.Value == 0 ||
                bottomPrcUpDown.Value == 0 || bottomPriceUpDown.Value == 0 ||
                costRateUpDown.Value > 1)
               // || FormUtils.SelectRow(selectClientsDgv) == null)
            {
                MessageBox.Show("参数错误");
                return;
            }
            PriceSave();
            foreach (var price in listResult)
            {
                string value = price.GetString("");
                string[] result = priceDao.SavePrice(value, "", "executed");
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
        }

        private void PriceSave()
        {
            listResult.Clear();
            ChannelPrice price = null;
            object obj = FormUtils.SelectRows(selectClientsDgv);
            if (obj == null) return;
            foreach (DataGridViewRow dgvr in (DataGridViewSelectedRowCollection)obj)
            {
                price = new ChannelPrice()
                {
                    Compid = Properties.Settings.Default.COMPID,
                    Ownerid = Properties.Settings.Default.OWNERID,
                    Saledeptid = SessionDto.Empdeptid,
                    Cstid = ((PubClients)dgvr.Cells[0].Value).Cstid,
                    Goodid = selectGoods.Goodid,
                    Stopflag = "00",
                    Suggestexecprc = suggestPrice.Suggestexecprc,
                    Suggestcostprc = suggestPrice.Costprc,
                    Suggestbottomprc = suggestPrice.Prc,
                    Source = "01",
                    Costprice = costPriceUpDown.Value.ToString(),
                    Costprc = costPrcUpDown.Value.ToString(),
                    Prc = prcUpDown.Value.ToString(),
                    Price = priceUpDown.Value.ToString(),
                    Costrate = costRateUpDown.Value.ToString(),
                    Iscredit = ((Dictionary)isCredit.SelectedValue).Code,
                    Bargain = ((Dictionary)bargain.SelectedValue).Code,
                    Begindate = beginDate.Text.Trim(),
                    Enddate = endDate.Text.Trim(),
                    Bottomprc = bottomPrcUpDown.Value.ToString(),
                    Bottomprice = bottomPriceUpDown.Value.ToString()
                };
                listResult.Add(price);
            }
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            ClientsGroups cbo = new ClientsGroups();
            if (clientsGroup.SelectedValue != null)
                cbo = (ClientsGroups)(clientsGroup.SelectedValue);
            clientsList = StringUtils.TableToEntity<PubClients>(dao.GetClientsByGroupId(clientsCode.Text.Trim(), clientsName.Text.Trim(), cbo.Id, cbo.Type, StringUtils.IsNotNull(selectGoods.Limitcsttype) ?
                ((GoodsSub)clientTypeGroup.SelectedValue).Subid :
                ((Dictionary)clientTypeGroup.SelectedValue).Code));
            FormUtils.RefreshDataGridView(selectClientsDgv, clientsList);
        }

        private void ClientsGroup_Click(object sender, EventArgs e)
        {
            FormUtils.GroupComboBoxSetting(sender, "clients");
        }

        public void SetSuggestValue(LowPrice lowPrice)
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

        private void ClientTypeGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            suggestPrice = priceDao.GetSuggestValue(selectGoods.Goodid,
                StringUtils.IsNotNull(selectGoods.Limitcsttype) ?
                ((GoodsSub)clientTypeGroup.SelectedValue).Subid :
                ((Dictionary)clientTypeGroup.SelectedValue).Code);
            SetSuggestValue(suggestPrice);
        }


        private void BottomPrcUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (bottomPrcUpDown.Value > prcUpDown.Value || bottomPrcUpDown.Value < costPrcUpDown.Value)
            {
                bottomPrcUpDown.Value = 0;
                return;
            }
            bottomPriceUpDown.Value = bottomPrcUpDown.Value / (1 + StringUtils.ToDecimal(selectGoods.Outrate));
        }

        private void CostPrcUpDown_ValueChanged(object sender, EventArgs e)
        {
            costPriceUpDown.Value = costPrcUpDown.Value / (1 + StringUtils.ToDecimal(selectGoods.Outrate));
            SetCostRate();
        }

        private void PrcUpDown_ValueChanged(object sender, EventArgs e)
        {
            priceUpDown.Value = prcUpDown.Value / (1 + StringUtils.ToDecimal(selectGoods.Outrate));
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

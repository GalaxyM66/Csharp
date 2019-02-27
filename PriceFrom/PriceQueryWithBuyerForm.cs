using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
 
 
 


namespace PriceManager
{
    public partial class PriceQueryWithBuyerForm : DockContent
    {
        PMPriceDao dao = new PMPriceDao();
        PMBaseDao baseDao = new PMBaseDao();
        PMSystemDao sysDao = new PMSystemDao();
        SortableBindingList<ChannelPrice> priceListWithBuyer = new SortableBindingList<ChannelPrice>();
        SortableBindingList<LowPrice> lowPriceListWithBuyer = new SortableBindingList<LowPrice>();
        ResultInfo resultMsg = new ResultInfo();
        public PriceQueryWithBuyerForm()
        {
            InitializeComponent();
        }

        private void PriceQueryWithBuyerForm_Load(object sender, EventArgs e)
        {
            beginDateBegin.Format = DateTimePickerFormat.Custom;
            beginDateBegin.CustomFormat = "yyyy-MM-dd";
            string vs = "20170701";
            beginDateBegin.Value= DateTime.ParseExact(vs, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            //beginDateBegin.Value = DateTime.Now.AddDays(1 - DateTime.Now.Day);

            beginDateEnd.Format = DateTimePickerFormat.Custom;
            beginDateEnd.CustomFormat = "yyyy-MM-dd";
            beginDateEnd.Value = System.DateTime.Now;
            //beginDateEnd.Value = DateTime.Now.AddYears(1);

            SortableBindingList<Dictionary> stopFlagList = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("1"));
            SortableBindingList<Dictionary> originList = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("36"));
            SortableBindingList<Dictionary> clientTypeGroupList = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("26"));
            SortableBindingList<Dictionary> isModifyExecList = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("27"));
            stopFlagList.Insert(0, new Dictionary { Name = "全部", Code = "" });
            clientTypeGroupList.Insert(0, new Dictionary { Name = "全部", Code = "" });
            isModifyExecList.Insert(0, new Dictionary { Name = "全部", Code = "" });
            FormUtils.SetComboBox(stopFlag_p1, stopFlagList, "Name", "TagPtr");
            FormUtils.SetComboBox(stopFlag_p2, stopFlagList, "Name", "TagPtr");
            FormUtils.SetComboBox(priceOrigin, originList, "Name", "TagPtr");
            FormUtils.SetComboBox(clientTypeGroup, clientTypeGroupList, "Name", "TagPtr");
            FormUtils.SetComboBox(isModifyExec, isModifyExecList, "Name", "TagPtr");

            SortableBindingList<PubEmp> buyerList = StringUtils.TableToEntity<PubEmp>(sysDao.GetBuyer());
            FormUtils.SetComboBox(buyer_p1, buyerList, "Empname", "TagPtr");
            FormUtils.SetComboBox(buyer_p2, buyerList, "Empname", "TagPtr");
            
            if ("109".Equals(SessionDto.Emproleid))
            {
                display.Visible = true;
                undisplay.Visible = true;
            }
            if ("108".Equals(SessionDto.Emproleid))
            {
                display.Visible = true;
                undisplay.Visible = true;
                label7.Visible = false;
                label8.Visible = false;
                buyer_p1.Visible = false;
                buyer_p2.Visible = false;
                buyer_p1.DataSource = null;
                buyer_p2.DataSource = null;

            }
        }

        private void GoodsCbo_Click(object sender, EventArgs e)
        {
            FormUtils.GoodsComboBoxSetting(sender);
        }

        private void QueryPriceBtn_Click(object sender, EventArgs e)
        {
            if (StringUtils.IsNull(clientsCode_p1.Text.Trim() + clientsName_p1.Text.Trim() + goodsCode_p1.Text.Trim() + goodsName_p1.Text.Trim() + spec_p1.Text.Trim() + producer_p1.Text.Trim()))
            {
                MessageBox.Show("请输入查询条件");
                return;
            }
            priceListWithBuyer = StringUtils.TableToEntity<ChannelPrice>(
                dao.GetPriceByTable(clientsCode_p1.Text.Trim(), clientsName_p1.Text.Trim(), 
                goodsCode_p1.Text.Trim(), goodsName_p1.Text.Trim(), spec_p1.Text.Trim(),
                producer_p1.Text.Trim(), ((Dictionary)stopFlag_p1.SelectedValue).Code,
                ((Dictionary)priceOrigin.SelectedValue).Code, beginDateBegin.Text, 
                beginDateEnd.Text, buyer_p1.DataSource == null ? SessionDto.Empcode : ((PubEmp)buyer_p1.SelectedValue).Empcode));
            FormUtils.RefreshDataGridView(channelPriceWithBuyerDgv, priceListWithBuyer);
            count_p1.Text = "共" + priceListWithBuyer.Count.ToString() + "条数据。";
        }

        private void ExportPriceBtn_Click(object sender, EventArgs e)
        {
            FormUtils.ExcelExport(channelPriceWithBuyerDgv, saveFileDialog1, "渠道价");
        }

        private void QueryLowPriceBtn_Click(object sender, EventArgs e)
        {
            PubWaredict cbo = new PubWaredict();
            if (goodsCbo.SelectedValue != null)
                cbo = (PubWaredict)(goodsCbo.SelectedValue);
            lowPriceListWithBuyer = StringUtils.TableToEntity<LowPrice>(dao.GetLowPrice(cbo.Goodid, goodsCode_p2.Text.Trim(),
                goodsName_p2.Text.Trim(), spec_p2.Text.Trim(), producer_p2.Text.Trim(), ((Dictionary)stopFlag_p2.SelectedValue).Code,
                ((Dictionary)clientTypeGroup.SelectedValue).Code, ((Dictionary)isModifyExec.SelectedValue).Code, buyer_p2.DataSource == null ? SessionDto.Empcode : ((PubEmp)buyer_p2.SelectedValue).Empcode));
            FormUtils.RefreshDataGridView(lowPriceWithBuyerDgv, lowPriceListWithBuyer);
            count_p2.Text = "共" + lowPriceListWithBuyer.Count.ToString() + "条数据。";
        }

        private void ExportLowPriceBtn_Click(object sender, EventArgs e)
        {
            FormUtils.ExcelExport(lowPriceWithBuyerDgv, saveFileDialog1, "底价");
        }

        private void Dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            FormUtils.CellFormatting(sender, e);
        }

        private void PriceQueryWithBuyerForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                switch (tabControl.SelectedTab.Name)
                {
                    case "tabPage1":
                        QueryPriceBtn_Click(sender, e);
                        break;
                    case "tabPage2":
                        QueryLowPriceBtn_Click(sender, e);
                        break;
                    default:break;
                }
            }
        }

        private void Display_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRows(channelPriceWithBuyerDgv);
            if (obj == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj;
            foreach (DataGridViewRow dgvr in row)
            {
                string[] result = dao.SetDisplay((ChannelPrice)dgvr.Cells[0].Value, "00");
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            QueryPriceBtn_Click(sender, e);
        }

        private void Undisplay_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRows(channelPriceWithBuyerDgv);
            if (obj == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj;
            foreach (DataGridViewRow dgvr in row)
            {
                string[] result = dao.SetDisplay((ChannelPrice)dgvr.Cells[0].Value, "99");
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            QueryPriceBtn_Click(sender, e);
        }
    }
}

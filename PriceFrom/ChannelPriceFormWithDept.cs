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
    public partial class ChannelPriceFormWithDept : DockContent
    {
        PMPriceDao dao = new PMPriceDao();
        PMBaseDao baseDao = new PMBaseDao();
        SortableBindingList<ChannelPrice> priceList = new SortableBindingList<ChannelPrice>();
        ChannelPrice price = null;
        ResultInfo resultMsg = new ResultInfo();
        public ChannelPriceFormWithDept()
        {
            InitializeComponent();
        }

        private void ChannelPriceFormWithDept_Load(object sender, EventArgs e)
        {
            beginDate.Format = DateTimePickerFormat.Custom;
            beginDate.CustomFormat = "yyyy-MM-dd";
            endDate.Format = DateTimePickerFormat.Custom;
            endDate.CustomFormat = "yyyy-MM-dd";
            SortableBindingList<Dictionary> stopFlagList = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("1"));
            stopFlagList.Insert(0, new Dictionary { Name = "全部", Code = "" });
            FormUtils.SetComboBox(stopFlag, stopFlagList, "Name", "TagPtr");
            prcUpDown.MouseWheel += new MouseEventHandler(FormUtils.Num_DiscountAmount_MouseWheel);
        }

        private void InitForm()
        {
            prcUpDown.Value = 0;
            priceUpDown.Value = 0;
            suggestExecPrcUpDown.Value = 0;
            beginDate.Text = "";
            endDate.Text = "";
            costRateUpDown.Value = 0;
            prcUpDown.Enabled = false;
            addRateUpDown.Value = 0;
            saveBtn.Enabled = false;
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            if (clientsCbo.DataSource == null && goodsCbo.DataSource == null)
            {
                MessageBox.Show("请选择查询参数");
                return;
            }
            RefreshDataGridView1();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (prcUpDown.Value < StringUtils.ToDecimal(price.Costprc) || prcUpDown.Value < StringUtils.ToDecimal(price.Bottomprc))
            {
                MessageBox.Show("销价低于底价");
                return;
            }
            if ("是".Equals(price.Bargainname))
            {
                price.Prc = prcUpDown.Value.ToString();
                price.Price = priceUpDown.Value.ToString();
                price.Costrate = costRateUpDown.Value.ToString();
            }
            price.Enddate = endDate.Text;
            string value = price.GetString("");
            string[] result = dao.SavePrice(value, "", "executed");
            if ("1".Equals(result[0]))
            {
                MessageBox.Show("成功!!");
                QueryBtn_Click(sender, e);
            }
            else
                MessageBox.Show(result[1]);
        }

        private void RefreshDataGridView1()
        {
            priceList = StringUtils.TableToEntity<ChannelPrice>(dao.GetPriceDraftAndExec(
                clientsCbo.DataSource == null ? "" : ((PubClients)clientsCbo.SelectedValue).Cstid,
                goodsCbo.DataSource == null ? "" : ((PubWaredict)goodsCbo.SelectedValue).Goodid,
                ((Dictionary)stopFlag.SelectedValue).Code));
            FormUtils.RefreshDataGridView(channelPriceWithDeptDgv, priceList);
            count.Text = "共" + priceList.Count.ToString() + "条数据。";
        }

        private void ChannelPriceWithDeptDgv_SelectionChanged(object sender, EventArgs e)
        {
            InitForm();
            object obj = FormUtils.SelectRow(channelPriceWithDeptDgv);
            if (obj == null)
            {
                return;
            }
            saveBtn.Enabled = true;
            price = (ChannelPrice)obj;
            prcUpDown.Value = StringUtils.ToDecimal(price.Prc);
            priceUpDown.Value = StringUtils.ToDecimal(price.Price);
            suggestExecPrcUpDown.Value = StringUtils.ToDecimal(price.Suggestexecprc);
            beginDate.Text = price.Begindate;
            endDate.Text = price.Enddate;
            costRateUpDown.Value = StringUtils.ToDecimal(price.Costrate);
            if ("否".Equals(price.Bargainname))
                prcUpDown.Enabled = false;
            else
                prcUpDown.Enabled = true;
        }

        private void ClientsCbo_Click(object sender, EventArgs e)
        {
            FormUtils.ClientsComboBoxSetting(sender);
        }

        private void GoodsCbo_Click(object sender, EventArgs e)
        {
            FormUtils.GoodsComboBoxSetting(sender);
        }

        private void ChannelPriceWithDeptDgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            FormUtils.CellFormatting(sender,e);
        }

        private void PrcUpDown_ValueChanged(object sender, EventArgs e)
        {
            priceUpDown.Value = prcUpDown.Value / (1 + StringUtils.ToDecimal(price.Outrate));
            if (prcUpDown.Value == 0 || StringUtils.ToDecimal(price.Costprc) > prcUpDown.Value)
            {
                return;
            }
            costRateUpDown.Value = 1 - (StringUtils.ToDecimal(price.Costprc) / prcUpDown.Value);
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            FormUtils.ExcelExport(channelPriceWithDeptDgv, saveFileDialog1, "渠道价");
        }

        private void Undisplay_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRows(channelPriceWithDeptDgv);
            if (obj == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj;
            foreach (DataGridViewRow dgvr in row)
            {
                string[] result = dao.SetDisplay((ChannelPrice)dgvr.Cells[0].Value, "99");
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            QueryBtn_Click(sender, e);
        }

        private void Display_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRows(channelPriceWithDeptDgv);
            if (obj == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj;
            foreach (DataGridViewRow dgvr in row)
            {
                string[] result = dao.SetDisplay((ChannelPrice)dgvr.Cells[0].Value, "00");
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            QueryBtn_Click(sender, e);
        }

        private void ChannelPriceFormWithDept_KeyDown(object sender, KeyEventArgs e)
        {
                if (e.KeyCode == Keys.Enter)
                {
                    QueryBtn_Click(sender, e);
                }
        }

        private void SaveBtnAll_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRows(channelPriceWithDeptDgv);
            if (obj == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj;
            ChannelPrice price = null;
            decimal addrate = Math.Round(addRateUpDown.Value, 6);
            foreach (DataGridViewRow dgvr in row)
            {
                price = (ChannelPrice)dgvr.Cells[0].Value;

                string[] result = dao.SavePriceWithAddrate(price, addrate);

                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            RefreshDataGridView1();
        }
    }
}

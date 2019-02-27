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
    public partial class ChangeConfirmation : Form
    {
        PMPriceDao dao = new PMPriceDao();
        SortableBindingList<ChannelPrice> changeList = new SortableBindingList<ChannelPrice>();
        internal LowPrice lowPrice;
        ResultInfo resultMsg = new ResultInfo();
        public ChangeConfirmation()
        {
            InitializeComponent();
        }

        private void ChangeConfirmation_Load(object sender, EventArgs e)
        {
            QueryBtn_Click(sender, e);
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            changeList = StringUtils.TableToEntity<ChannelPrice>(dao.GetChangeConfirmPrice(lowPrice.Goodid, lowPrice.Clienttypegroup,
                clientsCbo.DataSource == null ? "" : ((PubClients)clientsCbo.SelectedValue).Cstid, clientsCode.Text.Trim(), clientsName.Text.Trim(), region.Text.Trim()));
            FormUtils.RefreshDataGridView(changeConfirmDgv, changeList);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRows(changeConfirmDgv);
            if (obj == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj;
            ChannelPrice price;
            foreach (DataGridViewRow dgvr in row)
            {
                price = (ChannelPrice)dgvr.Cells[0].Value;
                price.Prc = lowPrice.Suggestexecprc;
                price.Price = (StringUtils.ToDecimal(price.Prc) / (1 + StringUtils.ToDecimal(lowPrice.Outrate))).ToString();
                price.Costprc = lowPrice.Costprc;
                price.Costprice = lowPrice.Costprice;
                price.Bottomprc = lowPrice.Prc;
                price.Bottomprice = lowPrice.Price;
                price.Costrate = lowPrice.Costrate;
                price.Bargain = lowPrice.Ismodifyexec;
                price.Source = "02";
                price.Stopflag = lowPrice.Stopflag;
                if (StringUtils.IsNull(price.Compid+ price.Ownerid + price.Begindate))
                {
                    price.Compid = Properties.Settings.Default.COMPID;
                    price.Ownerid = Properties.Settings.Default.OWNERID;
                    price.Saledeptid = SessionDto.Empdeptid;
                    price.Suggestexecprc = lowPrice.Suggestexecprc;
                    price.Suggestcostprc = lowPrice.Costprc;
                    price.Suggestbottomprc = lowPrice.Prc;
                    price.Iscredit = "10";
                    price.Begindate = Convert.ToString(DateTime.Now.Date);
                    price.Enddate = Convert.ToString(new DateTime(2038, 01, 01));
                    price.Goodid = lowPrice.Goodid;
                }
                string value = price.GetString("");
                string[] result = dao.SavePrice(value, "", "executed");
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            QueryBtn_Click(sender, e);
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ClientsCbo_Click(object sender, EventArgs e)
        {
            FormUtils.ClientsComboBoxSetting(sender);
        }

        private void ChangeConfirmDgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            FormUtils.CellFormatting(sender, e);
        }

        private void ChangeConfirmDgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                ChannelPrice price = (ChannelPrice)(dgv.Rows[i].Cells[0].Value);
                if ("01".Equals(price.Personal))
                {
                    dgv.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
                if ("draft".Equals(price.Origin))
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }

        private void ChangeConfirmDgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex == -1)
            {
                changeConfirmDgv.SelectAll();
            }
         }

        private void ChangeConfirmation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryBtn_Click(sender, e);
            }
        }
    }
}

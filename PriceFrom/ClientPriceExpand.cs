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
    public partial class ClientPriceExpand : Form
    {
        PMPriceDao dao = new PMPriceDao();
        SortableBindingList<ChannelPrice> expandList = new SortableBindingList<ChannelPrice>();
        public PubClients clients;
        ResultInfo resultMsg = new ResultInfo();
        public ClientPriceExpand()
        {
            InitializeComponent();
        }

        private void ClientPriceExpand_Load(object sender, EventArgs e)
        {
            Query();
        }

        private void Query()
        {
            expandList = StringUtils.TableToEntity<ChannelPrice>(dao.GetClientPriceExpand(clients.Cstid));
            FormUtils.RefreshDataGridView(priceExpandDgv, expandList);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            foreach (var price in expandList)
            {
                string value = price.GetString("");
                string[] result = dao.SavePrice(value, "", "executed");
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void priceExpandDgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            FormUtils.CellFormatting(sender, e);
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            Query();
        }

        private void ClientPriceExpand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                    QueryBtn_Click(sender, e);
            }
        }
    }
}

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
    public partial class ClientsCbo : Form
    {
        SortableBindingList<PubClients> clientsList = new SortableBindingList<PubClients>();
        internal SortableBindingList<PubClients> clients = null;
        PMBaseDao dao = new PMBaseDao();

        public ClientsCbo()
        {
            InitializeComponent();
        }

        private void ClientsCbo_Load(object sender, EventArgs e)
        {
            queryText.Focus();
        }

        private void Query()
        {
            clientsList = StringUtils.TableToEntity<PubClients>(dao.GetClientssByCbo(queryText.Text.Trim()));
            FormUtils.RefreshDataGridView(clientsCboDgv1, clientsList);
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            queryText.Clear();
            clientsCboDgv1.DataSource = null;
            clients = null;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            queryText.Clear();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void QueryText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Query();
            }
        }

        private void ClientsCboDgv1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            clients = new SortableBindingList<PubClients>();
            DataGridViewSelectedRowCollection row = clientsCboDgv1.SelectedRows;
            if (row.Count != 1) return;
            clients.Add((PubClients)(row[0].Cells[0].Value));
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

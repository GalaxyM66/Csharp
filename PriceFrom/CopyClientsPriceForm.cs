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
    public partial class CopyClientsPriceForm : Form
    {
        PMBaseDao dao = new PMBaseDao();
        PMPriceDao priceDao = new PMPriceDao();
        SortableBindingList<PubClients> clientsList = new SortableBindingList<PubClients>();
        public PubClients copyClients = new PubClients();
        ResultInfo resultMsg = new ResultInfo();
        string status = "all_cover";
        public CopyClientsPriceForm()
        {
            InitializeComponent();
        }

        private void CopyPriceForm_Load(object sender, EventArgs e)
        {
            copyClientsName.Text = copyClients.Cstname;
        }

        private void MergeCover_Click(object sender, EventArgs e)
        {
            if (mergeCover.Checked)
            {
                allCover.Checked = false;
                status = "merge_cover";
            }
            else
            {
                allCover.Checked = true;
                status = "all_cover";
            }
        }

        private void AllCover_Click(object sender, EventArgs e)
        {
            if (allCover.Checked)
            {
                mergeCover.Checked = false;
                status = "all_cover";
            }
            else
            {
                mergeCover.Checked = true;
                status = "merge_cover";
            }
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRows(copyClientsListDgv);
            if (obj == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj;
            foreach (DataGridViewRow dgvr in row)
            {
                string[] result = priceDao.CopyPrice(copyClients.Cstid, ((PubClients)dgvr.Cells[0].Value).Cstid, status, "p_scm_exec_clients_copy");
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            ClientsGroups cbo = new ClientsGroups();
            if (clientsGroup.SelectedValue != null)
                cbo = (ClientsGroups)(clientsGroup.SelectedValue);
            clientsList = StringUtils.TableToEntity<PubClients>(dao.GetClientsByGroupId(clientsCode.Text.Trim(), clientsName.Text.Trim(), cbo.Id, cbo.Type, copyClients.Clienttypegroup));
            FormUtils.RefreshDataGridView(copyClientsListDgv, clientsList);
        }

        private void ClientsGroup_Click(object sender, EventArgs e)
        {
            FormUtils.GroupComboBoxSetting(sender, "clients");
        }

        private void CopyClientsPriceForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryBtn_Click(sender, e);
            }
        }
    }
}

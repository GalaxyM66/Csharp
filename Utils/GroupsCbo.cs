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
    public partial class GroupsCbo : Form
    {
        SortableBindingList<ClientsGroups> cgroupList = new SortableBindingList<ClientsGroups>();
        SortableBindingList<GoodsGroups> ggroupList = new SortableBindingList<GoodsGroups>();
        internal SortableBindingList<ClientsGroups> cgroup = null;
        internal SortableBindingList<GoodsGroups> ggroup = null;
        public string  option ="";
        PMPriceDao dao = new PMPriceDao();

        public GroupsCbo()
        {
            InitializeComponent();
            Query();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            queryText.Clear();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ClientsGroup_Load(object sender, EventArgs e)
        {
            queryText.Focus();
        }

        private void Query()
        {
            if ("clients".Equals(option))
            {
                cgroupList = StringUtils.TableToEntity<ClientsGroups>(dao.GetCliensGroupsByCbo(queryText.Text.Trim()));
                FormUtils.RefreshDataGridView(GroupsCboDgv1, cgroupList);
            }
            else if ("goods".Equals(option))
            {
                ggroupList = StringUtils.TableToEntity<GoodsGroups>(dao.GetGoodsGroupsByCbo(queryText.Text.Trim()));
                FormUtils.RefreshDataGridView(GroupsCboDgv1, ggroupList);
            } 
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            queryText.Clear();
            GroupsCboDgv1.DataSource = null;
            ggroup = null;
            cgroup = null;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void QueryText_KeyUp(object sender, KeyEventArgs e)
        {
            Query();
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewSelectedRowCollection row = GroupsCboDgv1.SelectedRows;
            if (row.Count != 1) return;
            if ("clients".Equals(option))
            {
                cgroup = new SortableBindingList<ClientsGroups>() { (ClientsGroups)(row[0].Cells[0].Value) };
            }
            else if ("goods".Equals(option))
            {
                ggroup = new SortableBindingList<GoodsGroups> { (GoodsGroups)(row[0].Cells[0].Value) };
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}

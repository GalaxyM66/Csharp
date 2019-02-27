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
    public partial class GoodsCbo : Form
    {
        SortableBindingList<PubWaredict> goodsList = new SortableBindingList<PubWaredict>();
        internal SortableBindingList<PubWaredict> goods = null;
        PMBaseDao dao = new PMBaseDao();

        public GoodsCbo()
        {
            InitializeComponent();
            //Query();
        }

        private void ClientsGroup_Load(object sender, EventArgs e)
        {
            queryText.Focus();
        }

        private void Query()
        {
            goodsList = StringUtils.TableToEntity<PubWaredict>(dao.GetGoodsByCbo(queryText.Text.Trim()));
            FormUtils.RefreshDataGridView(goodsCboDgv1, goodsList);
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            queryText.Clear();
            goodsCboDgv1.DataSource = null;
            goods = null;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void QueryText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Query();
            }
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            goods = new SortableBindingList<PubWaredict>();
            DataGridViewSelectedRowCollection row = goodsCboDgv1.SelectedRows;
            if (row.Count != 1) return;
            goods.Add((PubWaredict)(row[0].Cells[0].Value));
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            queryText.Clear();
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

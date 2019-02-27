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
    public partial class AreasCbo : Form
    {
        SortableBindingList<DictionarySub> areaList = new SortableBindingList<DictionarySub>();
        public SortableBindingList<DictionarySub> area = null;
        PMBaseDao dao = new PMBaseDao();

        public AreasCbo()
        {
            InitializeComponent();
        }

        private void AreaDgv1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            area = new SortableBindingList<DictionarySub>();
            DataGridViewSelectedRowCollection row = areaDgv1.SelectedRows;
            if (row.Count != 1) return;
            area.Add((DictionarySub)(row[0].Cells[0].Value));
            DialogResult = DialogResult.OK;
            Close();
        }

        private void AreasCbo_Load(object sender, EventArgs e)
        {
            queryText.Focus();
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            queryText.Clear();
            areaDgv1.DataSource = null;
            area = null;
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
            Query();
        }

        private void Query()
        {
            areaList = StringUtils.TableToEntity<DictionarySub>(dao.GetAreaDictionary(queryText.Text.Trim(), ""));
            FormUtils.RefreshDataGridView(areaDgv1, areaList);
        }
    }
}

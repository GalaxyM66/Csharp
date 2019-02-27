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
    public partial class GoodsClientPriceForm_SelClient : Form
    {
        APDao_GoodsClientPrice dao = new APDao_GoodsClientPrice();
        private SortableBindingList<CstGroupDtl> ClientList = new SortableBindingList<CstGroupDtl>();
        public SortableBindingList<CstGroupDtl> SelClientList = new SortableBindingList<CstGroupDtl>();
        public GoodsClientPriceForm_SelClient()
        {
            InitializeComponent();
            dgvClientDtl.AutoGenerateColumns = false;
        }

        private void GoodsClientPriceForm_SelClient_Load(object sender, EventArgs e)
        {

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(txtFCstcode.Text))
            {
                sqlkeydict.Add("cstcode%", "%" + txtFCstcode.Text + "%");
            }
            if (!string.IsNullOrEmpty(txtFCSTNAME.Text))
            {
                sqlkeydict.Add("cstname%", "%" + txtFCSTNAME.Text + "%");
            }
            if (!string.IsNullOrEmpty(txtFregion.Text))
            {
                sqlkeydict.Add("region%", "%" + txtFregion.Text + "%");
            }
            ClientList = dao.GetSelClientsList(sqlkeydict);
            if (ClientList.Count <= 0)
            {
                MessageBox.Show("未查询到客户信息数据！", "系统提示！");
            }
            dgvClientDtl.DataSource = ClientList;
            dgvClientDtl.Refresh();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            object obj2 = FormUtils.SelectRows(dgvClientDtl);
            if (obj2 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            SelClientList.Clear();
            foreach (DataGridViewRow dgvr in row)
            {
                SelClientList.Add((CstGroupDtl)dgvr.Cells[0].Value);
            }
            
                this.DialogResult = DialogResult.OK;
                this.Close();
            
        }
    }
}

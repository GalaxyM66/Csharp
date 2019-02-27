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
    public partial class SelNewClient : DockContent
    {
        public SelNewClient()
        {
            InitializeComponent();
        }

        APDao_ClientGroup dao = new APDao_ClientGroup();
        public SortableBindingList<CstGroupDtl> ClientList = new SortableBindingList<CstGroupDtl>();
        public SortableBindingList<CstGroupDtl> SelClientList = new SortableBindingList<CstGroupDtl>();
        public SortableBindingList<CstGroupDtl> OldClientList = new SortableBindingList<CstGroupDtl>();

        public int Hdrid = 0;
        public int Batchid = 0;

        private void SelNewClient_Load(object sender, EventArgs e)
        {
            dgvClientDtl.AutoGenerateColumns = false;
        }

        //查询
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
            if (!string.IsNullOrEmpty(txtClienttypename.Text))
            {
                sqlkeydict.Add("clienttypename%", "%" + txtClienttypename.Text + "%");
            }
            ClientList = dao.GetSelClientsList(sqlkeydict, Hdrid);
            if (ClientList.Count <= 0)
            {
                MessageBox.Show("未查询到客户信息数据！", "系统提示！");
            }
            dgvClientDtl.DataSource = ClientList;
            dgvClientDtl.Refresh();
        }

        //确认新增
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
            bool b = false;
            string Warming = "";
            foreach (CstGroupDtl OldInfo in OldClientList)
            {
                foreach (CstGroupDtl SelInfo in SelClientList)
                {
                    if (OldInfo.Cstcode == SelInfo.Cstcode)
                    {
                        Warming = Warming + SelInfo.Cstcode + ",";
                        b = true;
                        break;
                    }
                    b = false;
                }
            }
            if (Warming != "")
            {
                MessageBox.Show("客户代码" + Warming + "已存在！新增失败！", "系统提示！");
                return;
            }
            else
            {

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        //全选
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            if (dgvClientDtl.RowCount <= 0) return;
            dgvClientDtl.SelectAll();
        }
    }
}

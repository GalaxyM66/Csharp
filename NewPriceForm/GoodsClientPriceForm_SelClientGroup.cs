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
    public partial class GoodsClientPriceForm_SelClientGroup : Form
    {
        APDao_GoodsClientPrice dao = null;
        APDao_ClientGroup CGdao = new APDao_ClientGroup();
        private SortableBindingList<GoodunifyDeptmenu> DeptList = new SortableBindingList<GoodunifyDeptmenu>();

        private SortableBindingList<CstGroupHdr> CstGroupHdrList = new SortableBindingList<CstGroupHdr>();

        public CstGroupHdr selclentgroupinfo = new CstGroupHdr();

        public string Defaultdept = "";
        public GoodsClientPriceForm_SelClientGroup()
        {
            InitializeComponent();
            dgvClientGroup.AutoGenerateColumns = false;

            
        }

        private void GoodsClientPriceForm_SelClientGroup_Load(object sender, EventArgs e)
        {
            dao = (APDao_GoodsClientPrice)this.Tag;

            DeptList = dao.GetGoodunifyDeptmenuList(int.Parse(Properties.Settings.Default.COMPID), int.Parse(Properties.Settings.Default.OWNERID));
            cbDefaultdept.DataSource = DeptList;
            cbDefaultdept.DisplayMember = "Deptname";
            cbDefaultdept.ValueMember = "Saledeptid";

            if (PubOwnerConfigureDto.Saledepttype == "0") {
                cbDefaultdept.SelectedValue = SessionDto.Empdeptid;
                cbDefaultdept.Enabled = false;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(txtClientGroupCode.Text))
            {
                sqlkeydict.Add("code%", "%" + txtClientGroupCode.Text.ToString() + "%");
            }
            if (!string.IsNullOrEmpty(txtClentGroupName.Text))
            {
                sqlkeydict.Add("name%", "%" + txtClentGroupName.Text.ToString() + "%");
            }

            try
            {
                CstGroupHdrList = CGdao.GetCstGroupClientGroupPriceList(cbDefaultdept.SelectedValue.ToString(), sqlkeydict);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (CstGroupHdrList.Count <= 0)
            {
                MessageBox.Show("未查询到数据！", "系统提示！");
                return;
            }
            else
            {
                dgvClientGroup.DataSource = CstGroupHdrList;
                dgvClientGroup.Refresh();
            }
        }

        private void dgvClientGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewSelectedRowCollection row = dgvClientGroup.SelectedRows;


            if (e.ColumnIndex < 1)//第一列才触发事件
                return;
            selclentgroupinfo = (CstGroupHdr)(row[0].Cells[0].Value);
            Defaultdept = cbDefaultdept.SelectedValue.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }




    }
}

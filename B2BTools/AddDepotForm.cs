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
    public partial class AddDepotForm : Form
    {
        string StorageId = "";
        
        APDao_B2BTools dao = new APDao_B2BTools();
        List<Dept> deptList = new List<Dept>();
        public AddDepotForm()
        {
            InitializeComponent();
            dgvDeptInfo.AutoGenerateColumns = false;
        }
        //快捷键 查询仓库
        private void AddDepotForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            deptList.Clear();
            if (e.KeyChar==13) {
                Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
                if(StringUtils.IsNotNull(txtSelDeptCode.Text)){
                    sqlkeydict.Add("deptcode",txtSelDeptCode.Text.ToString().Trim());
                }
                if (StringUtils.IsNotNull(txtSelDeptName.Text))
                {
                    sqlkeydict.Add("deptname%", "%"+txtSelDeptName.Text.ToString().Trim()+"%");
                }
                deptList=dao.GetDepts(sqlkeydict);
                if (deptList.Count <= 0)
                {
                    MessageBox.Show("无相关仓库！", "后台提示");
                    dgvDeptInfo.DataSource = null;
                    dgvDeptInfo.Refresh();
                    return;
                }
                else
                {
                    dgvDeptInfo.DataSource = deptList;
                    dgvDeptInfo.Refresh();
                    dgvDeptInfo.CurrentCell = null;
                }
            }

        }
        //选中仓库
        private void dgvDeptInfo_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Dept info = dgvDeptInfo.CurrentRow.DataBoundItem as Dept;           
            txtDeptName.Text = info.DeptName;
            txtDeptId.Text = info.DeptId;
        }
        //新增
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (StringUtils.IsNull(txtDeptId.Text))
            {
                MessageBox.Show("仓库Id不能为空！", "前台提示");
                txtDeptId.Focus();
                return;
            }
            if (StringUtils.IsNull(txtPercent.Text))
            {
                MessageBox.Show("百分比不能为空！", "前台提示");
                txtPercent.Focus();
                return;
            }
            SPRetInfo retinfo = new SPRetInfo();
            dao.AddDepts(txtDeptId.Text,txtPercent.Text, retinfo);
            if (retinfo.num == "1")
            {
                MessageBox.Show(retinfo.msg + "|" + retinfo.num, "后台提示！");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(retinfo.msg + "|" + retinfo.num, "后台提示！");
            }
        }
    }
}

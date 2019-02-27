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
    public partial class UpdateNameForm : Form
    {
        SortableBindingList<EmpInfos> infoList = new SortableBindingList<EmpInfos>();
        APDao_Agreement dao = new APDao_Agreement();
        //声明委托和事件
        public delegate void TransfDelegate(EmpInfos empInfo);
        public event TransfDelegate TrandfEvent;
        public UpdateNameForm()
        {
            InitializeComponent();
            this.dgvEmpInfo.AutoGenerateColumns = false;
        }

        private void clearUI() {

            infoList.Clear();
            
        }
        //查询
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            clearUI();
            this.Cursor = Cursors.WaitCursor;
            int i = 0;
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            if (!StringUtils.IsNull(txtEmpCode.Text)) {
                sqlkeydict.Add("empcode%","%"+ txtEmpCode.Text.ToString().Trim()+ "%");
                i++;
            }
            if (!StringUtils.IsNull(txtEmpName.Text))
            {
                sqlkeydict.Add("empname%", "%" + txtEmpName.Text.ToString().Trim() + "%");
                i++;
            }
            if (i <= 0)
            {
                MessageBox.Show("请至少输入一个查询条件!", "前台提示");
                this.Cursor = Cursors.Default;
                return;
            }
            else {
                infoList=dao.GetEmpInfo(sqlkeydict);
                if (infoList.Count<=0) {
                    MessageBox.Show("未查询到数据！", "程序提示");
                    dgvEmpInfo.DataSource = null;
                    this.Cursor = Cursors.Default;
                    return;
                }
                else {
                    dgvEmpInfo.DataSource = infoList;
                    dgvEmpInfo.Refresh();
                    dgvEmpInfo.CurrentCell = null;
                    this.Cursor = Cursors.Default;
                }

            }           
        }
        //双击选中读取的行
        private void dgvEmpInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //获取选中行的信息
            EmpInfos info = dgvEmpInfo.CurrentRow.DataBoundItem as EmpInfos;
            //触发事件
            TrandfEvent(info);
            this.Close();
        }

        private void UpdateNameForm_Load(object sender, EventArgs e)
        {
            this.Text = "员工信息";
        }
    }
}

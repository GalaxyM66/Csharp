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
    public partial class CstCheckConfigForm : DockContent
    {
        APDao_B2BTools dao = new APDao_B2BTools();
        SortableBindingList<CstCheckConfig> infosList = new SortableBindingList<CstCheckConfig>();
        CstCheckConfig cstCheckConfigInfo = new CstCheckConfig();
        //操作临时表，用于存储过程
        SortableBindingList<DelTempCstCheckConfig> DelTempList = new SortableBindingList<DelTempCstCheckConfig>();
        public CstCheckConfigForm()
        {
            InitializeComponent();
            dgvCstCheckConfig.AutoGenerateColumns = false;
        }
        private void clearUI()
        {
            infosList.Clear();

        }
        //全选
        private void BtnSelAll_Click(object sender, EventArgs e)
        {
            if (dgvCstCheckConfig.RowCount <= 0) return;
            dgvCstCheckConfig.SelectAll();
        }
        //查询
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            clearUI();
            this.Cursor = Cursors.WaitCursor;
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            int i = 0;
            if (!StringUtils.IsNull(txtCstCode.Text))
            {
                sqlkeydict.Add("cstcode", txtCstCode.Text.ToString().Trim());
                i++;
            }
            if (!StringUtils.IsNull(txtCstName.Text))
            {
                sqlkeydict.Add("cstname%", "%"+txtCstName.Text.ToString().Trim()+"%");
                i++;
            }
            if (i <= 0)
            {
                MessageBox.Show("请输入至少一个查询条件", "前台提示");
                this.Cursor = Cursors.Default;
                return;
            }
            else {
                infosList=dao.GetCstCheckConfig(sqlkeydict);
                if (infosList.Count <= 0)
                {
                    MessageBox.Show("未查询到数据！", "程序提示");
                    dgvCstCheckConfig.DataSource = null;
                    dgvCstCheckConfig.CurrentCell = null;
                    this.Cursor = Cursors.Default;
                    return;
                }

                    dgvCstCheckConfig.DataSource = infosList;
                    dgvCstCheckConfig.Refresh();
                    dgvCstCheckConfig.CurrentCell = null;
                    this.Cursor = Cursors.Default;
                

            }

        }
        //删除事件
        private void BtnDel_Click(object sender, EventArgs e)
        {
            //取得选中的行
            object obj2 = FormUtils.SelectRows(dgvCstCheckConfig);
            if (obj2 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            SortableBindingList<CstCheckConfig> infoList = new SortableBindingList<CstCheckConfig>();

            infoList.Clear();
            foreach (DataGridViewRow dgvr in row)
            {
                infoList.Add((CstCheckConfig)dgvr.Cells[0].Value);
            }
            //读取GencstgoodList集合中的Prodid
            foreach (CstCheckConfig info in infoList)
            {
                DelTempCstCheckConfig deleteInfo = new DelTempCstCheckConfig();
                deleteInfo.Cstid = info.Cstid;

                DelTempList.Add(deleteInfo);
            }
            SPRetInfo ret = new SPRetInfo();
            //提示确认是否删除
            DialogResult result = MessageBox.Show("确认删除？", "温馨提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                dao.DeleteCstCheckConfig(DelTempList, ret);
                if (ret.num == "1")
                {
                    MessageBox.Show(ret.msg + "|" + ret.result, "后台提示！");
                }
                else
                {
                    MessageBox.Show(ret.msg + "|" + ret.result, "后台提示！");
                    return;
                }
                DelTempList.Clear();
                BtnQuery_Click(sender, e);
            }
            else
            {
                MessageBox.Show("您选择了取消删除！");
            }
        }
        //新增
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddOrUpdateCstCheckConfigForm addCstCheckConfig = new AddOrUpdateCstCheckConfigForm();
            addCstCheckConfig.Tag = dao;
            addCstCheckConfig.stateUI = 0;//传值为0，界面为新增页面
            addCstCheckConfig.ShowDialog();
            addCstCheckConfig.cstCheckConfig = cstCheckConfigInfo;
            if (addCstCheckConfig.DialogResult == DialogResult.OK)
            {
                BtnQuery_Click(sender, e);
            }
        }
        //修改
        private void dgvCstCheckConfig_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CstCheckConfig info = dgvCstCheckConfig.CurrentRow.DataBoundItem as CstCheckConfig;

            AddOrUpdateCstCheckConfigForm updateCstCheckConfig = new AddOrUpdateCstCheckConfigForm();
            updateCstCheckConfig.cstCheckConfig = info;
            updateCstCheckConfig.Tag = dao;
            updateCstCheckConfig.stateUI = 1;//传值为1，界面为修改页面
            updateCstCheckConfig.ShowDialog();
            if (updateCstCheckConfig.DialogResult == DialogResult.OK)
            {
                BtnQuery_Click(sender, e);
            }

        }
    }
}

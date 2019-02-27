using PriceManager.B2BTools;
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
    /// <summary>
    /// ---------2018-7-30
    /// </summary>
    public partial class GenGoodRecordForm : DockContent
    {
        GenGoodRecord GenGoodRecordinfo = new GenGoodRecord();
        private SortableBindingList<GenGoodRecord> GenGoodRecordList = new SortableBindingList<GenGoodRecord>();
        //操作临时表，用于存储过程
        private SortableBindingList<DelTempGenGoodRecord> DelTempGenGoodRecordList = new SortableBindingList<DelTempGenGoodRecord>();
        APDao_B2BTools dao = new APDao_B2BTools();
        public GenGoodRecordForm()
        {
            InitializeComponent();
        }

        private void GenGoodRecordForm_Load(object sender, EventArgs e)
        {
            //beginDate.Format = DateTimePickerFormat.Custom;
            //beginDate.CustomFormat = "yyyy-MM-dd";
            //endDate.Format = DateTimePickerFormat.Custom;
            dgvGenGoodRecord.AutoGenerateColumns = false;
            beginDate.Checked = false;
            endDate.Checked = false;
        }
        private void  clearUI() {
            GenGoodRecordList.Clear();
            DelTempGenGoodRecordList.Clear();
            dgvGenGoodRecord.DataSource = GenGoodRecordList;
            dgvGenGoodRecord.Refresh();


        }
        //查询事件
        private void queryBtn_Click(object sender, EventArgs e)
        {

            clearUI();
            string begintime = "";
            string endtime = "";
            this.Cursor = Cursors.WaitCursor;
            if (beginDate.Checked)
            {
                begintime = beginDate.Value.ToString("yyyyMMdd");
            }
            else {
                 begintime = "";
            }
            if (endDate.Checked)
            {
                endtime = endDate.Value.ToString("yyyyMMdd");
                
            }
            else
            {
                endtime = "";
            }
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            int i = 0;
            if ((StringUtils.IsNotNull(begintime))&&(StringUtils.IsNotNull(endtime)))
            {
                i++;
            }
            if (!StringUtils.IsNull(txtCstcode.Text))
            {
                sqlkeydict.Add("cstcode%", "%" + txtCstcode.Text.ToString() + "%");
                i++;
            }
            if (!StringUtils.IsNull(txtDname.Text))
            {
                sqlkeydict.Add("dname%", "%" + txtDname.Text.ToString() + "%");
                i++;
            }
            if (!StringUtils.IsNull(txtGengoods.Text))
            {
                sqlkeydict.Add("gengoods%", "%" + txtGengoods.Text.ToString() + "%");
                i++;
            }
            if (!StringUtils.IsNull(txtRecordmark.Text))
            {
                sqlkeydict.Add("recordmark%", "%" + txtRecordmark.Text.ToString() + "%");
                i++;
            }
            if (!StringUtils.IsNull(txtGenspec.Text))
            {
                sqlkeydict.Add("genspec%", "%" + txtGenspec.Text.ToString() + "%");
                i++;
            }

            if (i < 1)
            {
                MessageBox.Show("前台提示||请至少输入一个查询条件！");
                this.Cursor = Cursors.Default;
                return;
            }

            GenGoodRecordList = dao.GetGenGoodRecordList(begintime,endtime,sqlkeydict);

            if (GenGoodRecordList.Count <= 0)
            {
                MessageBox.Show("未查询到数据！", "程序提示");
                dgvGenGoodRecord.DataSource = null;
                this.Cursor = Cursors.Default;
                return;
            }
            dgvGenGoodRecord.DataSource = GenGoodRecordList;
            dgvGenGoodRecord.Refresh();
            dgvGenGoodRecord.CurrentCell = null;
            this.Cursor = Cursors.Default;
        }

        //全选事件
        private void selAllBtn_Click(object sender, EventArgs e)
        {
            if (dgvGenGoodRecord.RowCount <= 0) return;
            dgvGenGoodRecord.SelectAll();
        }

        //删除事件
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            //取得选中的行
            object obj2 = FormUtils.SelectRows(dgvGenGoodRecord);
            if (obj2 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            SortableBindingList<GenGoodRecord> GenGoodRecordList = new SortableBindingList<GenGoodRecord>();

            GenGoodRecordList.Clear();
            foreach (DataGridViewRow dgvr in row)
            {
                GenGoodRecordList.Add((GenGoodRecord)dgvr.Cells[0].Value);
            }

            //读取GenGoodRecordList集合中的Goodrecordid 
            foreach (GenGoodRecord info in GenGoodRecordList)
            {
                DelTempGenGoodRecord deleteInfo = new DelTempGenGoodRecord();
                deleteInfo.Goodrecordid = int.Parse(info.Goodrecordid);
                DelTempGenGoodRecordList.Add(deleteInfo);
            }
            SPRetInfo ret = new SPRetInfo();
            //提示确认是否删除
            DialogResult result = MessageBox.Show("确认删除？", "温馨提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                dao.DeleteGenGoodRecord(DelTempGenGoodRecordList, ret);
                if (ret.num == "1")
                {
                    MessageBox.Show(ret.msg + "|" + ret.result, "后台提示！");
                }
                else
                {
                    MessageBox.Show(ret.msg + "|" + ret.result, "后台提示！");
                    return;
                }
                DelTempGenGoodRecordList.Clear();
                //dgvGenGoodRecord.DataSource = null;
                queryBtn_Click(sender, e);
            }
            else
            {
                MessageBox.Show("您选择了取消删除！");
            }
        }
        //设置下一个时间控件不能选上一个时间控件之前的时间
        private void endDate_ValueChanged(object sender, EventArgs e)
        {
            endDate.CustomFormat = "yyyy-MM-dd";
            endDate.MinDate = DateTime.Parse(beginDate.Value.ToString());

        }
        /// <summary>
        /// --------------2018-8-1-------------
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        //新增备案品种
        private void addBtn_Click(object sender, EventArgs e)
        {
            AddOrUpdateGenGoodRecordForm addGenGoodRecord= new AddOrUpdateGenGoodRecordForm();
            addGenGoodRecord.Tag = dao;
            addGenGoodRecord.stateUI = 0;//传值为0，界面为新增页面
            addGenGoodRecord.ShowDialog();
            addGenGoodRecord.GenGoodRecordInfo = GenGoodRecordinfo;
            if (addGenGoodRecord.DialogResult == DialogResult.OK)
            {
                queryBtn_Click(sender, e);
            }


        }
        //修改备案品种
        private void dgvGenGoodRecord_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GenGoodRecord info = dgvGenGoodRecord.CurrentRow.DataBoundItem as GenGoodRecord;

            AddOrUpdateGenGoodRecordForm updateGenGoodRecordForm = new AddOrUpdateGenGoodRecordForm();
            updateGenGoodRecordForm.GenGoodRecordInfo = info;
            updateGenGoodRecordForm.Tag = dao;
            updateGenGoodRecordForm.stateUI = 1;//传值为1，界面为修改页面
            updateGenGoodRecordForm.ShowDialog();
            if (updateGenGoodRecordForm.DialogResult == DialogResult.OK)
            {
                queryBtn_Click(sender, e);
            }
        }
        //Excel导入
        private void importFileBtn_Click(object sender, EventArgs e)
        {
            ImportGoodRecordExcelForm importGoodRecordExcelForm = new ImportGoodRecordExcelForm();
            importGoodRecordExcelForm.Tag = dao;
            importGoodRecordExcelForm.Text = "备案品种Excel导入";
            importGoodRecordExcelForm.ShowDialog();


        }
    }
}

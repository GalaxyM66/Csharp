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
    public partial class GenCstGoodForm : DockContent
    {
        GenCstGood GencstgoodInfo = new GenCstGood();
        APDao_B2BTools dao = new APDao_B2BTools();
        private SortableBindingList<GenCstGood> GenCstGoodList = new SortableBindingList<GenCstGood>();
        //操作临时表，用于存储过程
        SortableBindingList<DelTempGenCstGood> DelTempGenCstGoodList = new SortableBindingList<DelTempGenCstGood>();
        public GenCstGoodForm()
        {
            InitializeComponent();
            dgvGenCstGood.AutoGenerateColumns = false;


        }
        private void Clear()
        {

            
        }
        //查询客户对码
        private void queryBtn_Click(object sender, EventArgs e)
        {
            Clear();
            this.Cursor = Cursors.WaitCursor;
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            int i = 0;
            if (!StringUtils.IsNull(txtClientsCode.Text)) {
                sqlkeydict.Add("cstcode%","%"+txtClientsCode.Text.ToString()+"%");
                i++;
            }
            if (!StringUtils.IsNull(txtCMSGoodsCode.Text)) {
                sqlkeydict.Add("goods%","%"+txtCMSGoodsCode.Text .ToString()+"%");
                i++;
            }
            if (!StringUtils.IsNull(txtClientOutsideCode .Text)) {
                sqlkeydict.Add("gengoods%", "%"+txtClientOutsideCode.Text.ToString()+"%");
                i++;
            }
            if (i<1){
                MessageBox.Show("前台提示||请至少输入一个查询条件！");
                this.Cursor = Cursors.Default;
                return;
            }
            GenCstGoodList = dao.GetGenCstGoodList(sqlkeydict);
            if (GenCstGoodList.Count <= 0)
            {
                MessageBox.Show("未查询到数据！","程序提示");
                dgvGenCstGood.DataSource = null;
                this.Cursor = Cursors.Default;
                return;
            }
            dgvGenCstGood.DataSource = GenCstGoodList;
            dgvGenCstGood.Refresh();
            dgvGenCstGood.CurrentCell = null;
            this.Cursor = Cursors.Default;
        }
       
        //新增客户对码
        private void addBtn_Click(object sender, EventArgs e)
        {
            AddOrUpdateGenCstGoodForm addGenCstGood = new AddOrUpdateGenCstGoodForm();
            addGenCstGood.Tag = dao;
            addGenCstGood.stateUI = 0;//传值为0，界面为新增页面
            addGenCstGood.ShowDialog();
            addGenCstGood.GencstgoodInfo = GencstgoodInfo;
            if (addGenCstGood.DialogResult == DialogResult.OK)
            {
                queryBtn_Click(sender, e);
            }
        }
        
        //双击跳到修改对码界面
        private void dgvGenCstGood_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GenCstGood info = dgvGenCstGood.CurrentRow.DataBoundItem as GenCstGood;

            AddOrUpdateGenCstGoodForm updateGenCstGoodForm = new AddOrUpdateGenCstGoodForm();
            updateGenCstGoodForm.GencstgoodInfo = info;
            updateGenCstGoodForm.Tag = dao;
            updateGenCstGoodForm.stateUI = 1;//传值为1，界面为修改页面
            updateGenCstGoodForm.ShowDialog();
            if (updateGenCstGoodForm.DialogResult == DialogResult.OK)
            {
                queryBtn_Click(sender, e);
            }
        }
        
        //删除对码
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            //取得选中的行
            object obj2 = FormUtils.SelectRows(dgvGenCstGood);
            if (obj2 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            SortableBindingList<GenCstGood> GencstgoodList = new SortableBindingList<GenCstGood>();

            GencstgoodList.Clear();
            foreach (DataGridViewRow dgvr in row)
            {
                GencstgoodList.Add((GenCstGood)dgvr.Cells[0].Value);
            }
            //读取GencstgoodList集合中的Relatid
            foreach (GenCstGood info in GencstgoodList) {
                DelTempGenCstGood deleteInfo = new DelTempGenCstGood();
                deleteInfo.Relatid = int.Parse(info.Relatid);

                DelTempGenCstGoodList.Add(deleteInfo);
            }
            SPRetInfo ret = new SPRetInfo();
            //提示确认是否删除
            DialogResult result = MessageBox.Show("确认删除？", "温馨提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                dao.DeleteGenCstGood(DelTempGenCstGoodList, ret);
                if (ret.num == "1")
                {
                    MessageBox.Show(ret.msg + "|" + ret.result, "后台提示！");
                }
                else
                {
                    MessageBox.Show(ret.msg + "|" + ret.result, "后台提示！");
                    return;
                }
                DelTempGenCstGoodList.Clear();
                queryBtn_Click(sender, e);
            }
            else {
                MessageBox.Show("您选择了取消删除！");
            }
        }
        //全选
        private void CheckAllBtn_Click(object sender, EventArgs e)
        {
            if (dgvGenCstGood.RowCount <= 0) return;
            dgvGenCstGood.SelectAll();

        }
        //统一改码
        private void UnifiedUpdateCodeBtn_Click(object sender, EventArgs e)
        {
            //取的选中的行
            object obj2 = FormUtils.SelectRows(dgvGenCstGood);
            if (obj2 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;

            SortableBindingList<GenCstGood> SelGenCstGoodsList = new SortableBindingList<GenCstGood>();
            SortableBindingList<GenCstGood> RemoveGenCstGoodsList = new SortableBindingList<GenCstGood>();
            SelGenCstGoodsList.Clear();
            foreach (DataGridViewRow dgvr in row)
            {
                SelGenCstGoodsList.Add((GenCstGood)dgvr.Cells[0].Value);
                RemoveGenCstGoodsList.Add((GenCstGood)dgvr.Cells[0].Value);
            }
            UnifiedUpdateCodeForm unifiedUpdateCodeForm = new UnifiedUpdateCodeForm();

            unifiedUpdateCodeForm.Tag = dao;
            unifiedUpdateCodeForm.SelGenCstGoodsList = SelGenCstGoodsList;
            unifiedUpdateCodeForm.RemoveGenCstGoodsList = RemoveGenCstGoodsList;
            unifiedUpdateCodeForm.Text = "统一改码";
            unifiedUpdateCodeForm.ShowDialog();


        }
        //Excel导入事件 2018-7-25
        private void ExImportBtn_Click(object sender, EventArgs e)
        {

            ImportXsltEmpForm importXsltEmpForm = new ImportXsltEmpForm();
            importXsltEmpForm.Tag = dao;
            importXsltEmpForm.Text = "Excel导入";
            importXsltEmpForm.ShowDialog();

        }
    }
   
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PriceManager.B2BTools
{
    public partial class UnifiedUpdateCodeForm : Form
    {
        APDao_B2BTools dao = null;
        public SortableBindingList<GenCstGood> SelGenCstGoodsList = new SortableBindingList<GenCstGood>();
        public SortableBindingList<GenCstGood> RemoveGenCstGoodsList = new SortableBindingList<GenCstGood>();

        public SortableBindingList<GenCstGood> TempList = new SortableBindingList<GenCstGood>();
        public SortableBindingList<GenCstGood> RemovegoodList = new SortableBindingList<GenCstGood>();
        
        Dictionary<string, GenCstGood> ListDict = new Dictionary<string, GenCstGood>();
        public UnifiedUpdateCodeForm()
        {
            InitializeComponent();
            dgvUpdateItems.AutoGenerateColumns = false;
        }

        private void UnifiedUpdateCodeForm_Load(object sender, EventArgs e)
        {
            dao = (APDao_B2BTools)this.Tag;
            dgvUpdateItems.DataSource = SelGenCstGoodsList;
            dgvUpdateItems.Refresh();


            dgvRemoveItems.AutoGenerateColumns = false;
            var RemoveList = from RemoveInfo in RemoveGenCstGoodsList
                             where RemoveInfo.SelectFlag == false
                             select RemoveInfo;
           
            dgvRemoveItems.DataSource= new SortableBindingList<GenCstGood>(RemoveList);
            dgvRemoveItems.Refresh();
           
        }
        ////////-----2018-07-24--------------------
        //筛选出无需改码的事件
        private void outScreenConditionsBtn_Click(object sender, EventArgs e)
        {
            //获取筛选条件
            String outClientCode = txtOutClientCode.Text.Trim();
            //判断是否条件为空
            if (StringUtils.IsNull(outClientCode)) {
                MessageBox.Show("前台提示||请输入筛选条件");
                txtOutClientCode.Focus();
            }
            int rows = dgvUpdateItems.Rows.Count;
            bool flag = false;
            for (int i = 0; i<rows;i++)
            {
                    if (outClientCode == dgvUpdateItems.Rows[i].Cells["Column34"].Value.ToString().Trim())
                    {
                        dgvUpdateItems.Rows[i].Selected = true;    
                    flag = true;
                    return;
                }
                    else {
                    flag = false;
                    continue;
                }

            }
            if (flag==false)
            {
                MessageBox.Show("后台提示||筛选的结果不存在");
            }

        }
        //筛选出需要改码的事件
        private void inScreenConditionsBtn_Click(object sender, EventArgs e)
        {
            //获取筛选条件
            String inClientCode = txtInClientCode.Text.Trim();
            //判断条件是否为空
            if (StringUtils.IsNull(inClientCode)) {
                MessageBox.Show("前台提示||请输入筛选条件");
                txtInClientCode.Focus();
            }
            int rows = dgvRemoveItems.Rows.Count;
            bool flag = false;
            for (int i=0;i<rows;i++) {
                if (inClientCode == dgvRemoveItems.Rows[i].Cells["dataGridViewTextBoxColumn9"].Value.ToString().Trim())
                {
                    dgvRemoveItems.Rows[i].Selected = true;
                    flag = true;
                    return;
                }
                else {

                    flag = false;
                    continue;
                }
            }
            if (flag==false) {
                MessageBox.Show("后台提示||筛选的结果不存在");
            }
        }


        //将无须改码项从统一改码项中移出
        private void shiftOutBtn_Click(object sender, EventArgs e)
        {
            //获取选中的行
            object obj2 = FormUtils.SelectRows(dgvUpdateItems);
            if (obj2 == null) return;

            //读取选中的行的数据
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            foreach (DataGridViewRow dgvr in row) {
                GenCstGood selInfo = (GenCstGood)dgvr.Cells[0].Value;
                    TempList.Add(selInfo);
                    foreach (GenCstGood UpdateInfo in SelGenCstGoodsList)
                    
                    {
                        if (UpdateInfo.Cstid == dgvr.Cells["Column13"].Value.ToString() && UpdateInfo.Gengoods == dgvr.Cells["Column36"].Value.ToString())
                        {
                            UpdateInfo.SelectFlag = false;
                            break;

                        }
                    }
                //不要改码的项设置成fasle可见
                foreach (GenCstGood RemoveInfo in RemoveGenCstGoodsList) {

                    if (RemoveInfo.Cstid == dgvr.Cells["Column13"].Value.ToString() && RemoveInfo.Gengoods == dgvr.Cells["Column36"].Value.ToString())
                    {
                        RemoveInfo.SelectFlag = false;
                        break;

                    }

                }
             
            }

            var UpdateList = from UpdateInfo in SelGenCstGoodsList
                             where UpdateInfo.SelectFlag == true
                             select UpdateInfo;

            var RemoveList = from RemoveInfo in RemoveGenCstGoodsList
                             where RemoveInfo.SelectFlag == false
                             select RemoveInfo;


            dgvUpdateItems.DataSource= new SortableBindingList<GenCstGood>(UpdateList);
            dgvUpdateItems.Refresh();

            dgvRemoveItems.DataSource = new SortableBindingList<GenCstGood>(RemoveList);
            dgvRemoveItems.Refresh();
        }


        //将选中的无须改码项移入统一改码项中
        private void moveIntoBtn_Click(object sender, EventArgs e)
        {
            //获取选中的行
            object obj2 = FormUtils.SelectRows(dgvRemoveItems);
            if (obj2 == null) return;

            //读取选中的行的数据
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            SortableBindingList<GenCstGood> TeList = new SortableBindingList<GenCstGood>();

            foreach (DataGridViewRow dgvr in row)
            {
                GenCstGood selectInfo = (GenCstGood)dgvr.Cells[0].Value;
                //TeList.Add(selectInfo);

                foreach (GenCstGood UpdateInfo in SelGenCstGoodsList)
                {
                    if (UpdateInfo.Cstid == dgvr.Cells["dataGridViewTextBoxColumn8"].Value.ToString() && UpdateInfo.Gengoods == dgvr.Cells["dataGridViewTextBoxColumn15"].Value.ToString())
                    {
                        UpdateInfo.SelectFlag = true;
                        break;
                    }

                    foreach (GenCstGood RemoveInfo in RemoveGenCstGoodsList)
                    {
                        if (RemoveInfo.Cstid == dgvr.Cells["dataGridViewTextBoxColumn8"].Value.ToString() && RemoveInfo.Gengoods == dgvr.Cells["dataGridViewTextBoxColumn15"].Value.ToString())
                        {
                            RemoveInfo.SelectFlag = true;
                            break;
                        }
                    }

                }
              

            }

            var RemoveList = from info in RemoveGenCstGoodsList
                             where info.SelectFlag == false
                             select info;

            var UpdateList = from UpdateInfo in SelGenCstGoodsList
                             where UpdateInfo.SelectFlag == true
                             select UpdateInfo;

            dgvRemoveItems.DataSource = new SortableBindingList<GenCstGood>(RemoveList);
            dgvRemoveItems.Refresh();

            dgvUpdateItems.DataSource = new SortableBindingList<GenCstGood>(UpdateList); ;
            dgvUpdateItems.Refresh();

        }
       
        
        //改码
        private void reviseCodeBtn_Click(object sender, EventArgs e)
        {
            //获取被选中的统一改码项
            //object obj2 = FormUtils.SelectRows(dgvUpdateItems);
            //if (obj2 == null) return;
            //DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;

            SortableBindingList<GenCstGood> UpdateGenCstGoodsList = new SortableBindingList<GenCstGood>();

            //foreach (DataGridViewRow dgvr in row) {
            //    UpdateGenCstGoodsList.Add((GenCstGood)dgvr.Cells[0].Value);

            //}
            var source = dgvUpdateItems.DataSource;
            UpdateGenCstGoodsList = (SortableBindingList<GenCstGood>)source;
            //将选中的信息传给下一个界面
            ReviseCodeForm reviseCodeForm = new ReviseCodeForm();
            reviseCodeForm.Tag = dao;
            reviseCodeForm.UpdateGenCstGoodsList = UpdateGenCstGoodsList;
            reviseCodeForm.Text = "修改cms商品码";
            reviseCodeForm.ShowDialog();
            this.Close();
            
        }
        //改码全选
        //private void allSelectBtn_Click(object sender, EventArgs e)
        //{
        //    if (dgvUpdateItems.RowCount <= 0) return;
        //    dgvUpdateItems.SelectAll();


        //}
    }
}

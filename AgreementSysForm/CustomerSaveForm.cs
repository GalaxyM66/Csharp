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
    public partial class CustomerSaveForm : DockContent
    {
        SortableBindingList<AgreeClient> infolist = new SortableBindingList<AgreeClient>();
        SPRetInfo ret = new SPRetInfo();
        APDao_Agreement dao = new APDao_Agreement();

        //修改下拉框类型
        string typecodes = "";
        //新增修改的下拉框      
        private ComboBox Seal_ComboBox = new ComboBox();
        Dictionary<string, string> SealDic = new Dictionary<string, string>();
        List<string> Seal = new List<string>();

        private ComboBox Onfi_ComboBox = new ComboBox();
        Dictionary<string, string> OnfiDic = new Dictionary<string, string>();
        List<string> Onfi = new List<string>();

        public CustomerSaveForm()
        {
            InitializeComponent();
            dgvCustomerInfo.AutoGenerateColumns = false;
        }
        private void CustomerSaveForm_Load(object sender, EventArgs e)
        {

            //绑定是否盖章下拉框
            BindSeal();
            // 设置下拉列表框不可见
            Seal_ComboBox.Visible = false;
            // 添加下拉列表框事件
            Seal_ComboBox.SelectedIndexChanged += new EventHandler(Seal_SelectedIndexChanged);
            // 将下拉列表框加入到DataGridView控件中
            this.dgvCustomerInfo.Controls.Add(Seal_ComboBox);

            //绑定是否存档下拉框
            BindOnfi();
            // 设置下拉列表框不可见
            Onfi_ComboBox.Visible = false;
            // 添加下拉列表框事件
            Onfi_ComboBox.SelectedIndexChanged += new EventHandler(Onfi_SelectedIndexChanged);
            // 将下拉列表框加入到DataGridView控件中
            this.dgvCustomerInfo.Controls.Add(Onfi_ComboBox);

        }
        #region 下拉框数据绑定处理
        /// <summary>
        /// 绑定是否盖章下拉列表框
        /// </summary>
        private void BindSeal()
        {
            DataTable dtSeal = new DataTable();
            dtSeal.Columns.Add("Value");
            dtSeal.Columns.Add("Name");
            DataRow drSeal;
            typecodes = "CHOICE";
            SealDic = dao.getCbContent(typecodes);
            Seal = StringUtils.GetValue(SealDic);
            for (int i = 0; i < Seal.Count; i++)
            {
                drSeal = dtSeal.NewRow();
                drSeal[0] = StringUtils.GetKey(Seal[i], SealDic);
                drSeal[1] = Seal[i];
                dtSeal.Rows.Add(drSeal);
            }
            //this.CstIntention_ComboBox.DataSource = Csti;
            Seal_ComboBox.ValueMember = "Value";
            Seal_ComboBox.DisplayMember = "Name";
            Seal_ComboBox.DataSource = dtSeal;
            Seal_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        // 当用户选择下拉列表框时改变DataGridView单元格的内容
        private void Seal_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Seal.Count; i++)
            {
                if (((ComboBox)sender).Text == Seal[i])
                {
                    dgvCustomerInfo.CurrentCell.Value = Seal[i];
                    dgvCustomerInfo.CurrentCell.Tag = StringUtils.GetKey(Seal[i], SealDic);
                }
            }
            try
            {
                AgreeClient info = dgvCustomerInfo.CurrentRow.DataBoundItem as AgreeClient;
                string seal = Seal_ComboBox.SelectedValue.ToString();
                //string Cstkey = StringUtils.GetKey(cst,CstiDic);
                string colName = "SEAL";
                int roleType = 2;
                dao.UpdateColValues(colName, seal, info.AgreementId, roleType, ret);
                if (ret.num != "1")
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                }
                this.Seal_ComboBox.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 绑定是否存档下拉列表框
        /// </summary>
        private void BindOnfi()
        {
            DataTable dtOnfi = new DataTable();
            dtOnfi.Columns.Add("Value");
            dtOnfi.Columns.Add("Name");
            DataRow drOnfi;
            typecodes = "CHOICE";
            OnfiDic = dao.getCbContent(typecodes);
            Onfi = StringUtils.GetValue(OnfiDic);
            for (int i = 0; i < Onfi.Count; i++)
            {
                drOnfi = dtOnfi.NewRow();
                drOnfi[0] = StringUtils.GetKey(Onfi[i], OnfiDic);
                drOnfi[1] = Onfi[i];
                dtOnfi.Rows.Add(drOnfi);
            }
            //this.CstIntention_ComboBox.DataSource = Csti;
            Onfi_ComboBox.ValueMember = "Value";
            Onfi_ComboBox.DisplayMember = "Name";
            Onfi_ComboBox.DataSource = dtOnfi;
            Onfi_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        // 当用户选择下拉列表框时改变DataGridView单元格的内容
        private void Onfi_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Onfi.Count; i++)
            {
                if (((ComboBox)sender).Text == Onfi[i])
                {
                    dgvCustomerInfo.CurrentCell.Value = Onfi[i];
                    dgvCustomerInfo.CurrentCell.Tag = StringUtils.GetKey(Onfi[i], OnfiDic);
                }
            }
            try
            {
                AgreeClient info = dgvCustomerInfo.CurrentRow.DataBoundItem as AgreeClient;
                string onfi = Onfi_ComboBox.SelectedValue.ToString();
                //string Cstkey = StringUtils.GetKey(cst,CstiDic);
                string colName = "ONFILE";
                int roleType = 2;
                dao.UpdateColValues(colName, onfi, info.AgreementId, roleType, ret);
                if (ret.num != "1")
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                }
                this.Onfi_ComboBox.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        #endregion
        void clearUI() {
            infolist.Clear();
        }
        //查询事件
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            clearUI();
            this.Cursor = Cursors.WaitCursor;
            int i = 0;
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            if (!StringUtils.IsNull(txtYearNum.Text))
            {
                sqlkeydict.Add("yearnum",txtYearNum.Text.ToString().Trim());
                i++;
            }
            if (!StringUtils.IsNull(txtProdName.Text))
            {
                sqlkeydict.Add("prod_name%", "%"+txtProdName.Text.ToString().Trim()+"%");
                i++;
            }
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
            if (!StringUtils.IsNull(txtSaller.Text))
            {
                sqlkeydict.Add("saller%", "%"+txtSaller.Text.ToString().Trim()+"%");
                i++;
            }
            if (i <= 0)
            {
                MessageBox.Show("请输入至少一个查询条件！", "前台提示");
                this.Cursor = Cursors.Default;
                return;
            }
            else {
                infolist=dao.SelCustomerInfo(sqlkeydict);
                if (infolist.Count <= 0)
                {
                    MessageBox.Show("未查询到数据！", "程序提示");
                    dgvCustomerInfo.DataSource = null;
                    //dgvAgreementInfo.Refresh();
                    this.Cursor = Cursors.Default;
                    return;
                }
                else
                {
                    dgvCustomerInfo.DataSource = infolist;
                    dgvCustomerInfo.Refresh();
                    dgvCustomerInfo.CurrentCell = null;
                    this.Cursor = Cursors.Default;
                }
            }
        }
        #region 双击修改事件
        //修改        
        private void dgvCustomerInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //只有最终协议量可以修改
            if (e.ColumnIndex != 18 )
            {
                dgvCustomerInfo.Columns[e.ColumnIndex].ReadOnly = true;
                return;
            }
            if (e.ColumnIndex == 18 && e.RowIndex != -1)
            {
                dgvCustomerInfo.Columns[e.ColumnIndex].ReadOnly = false;
                DataGridViewCell cell = dgvCustomerInfo.Rows[e.RowIndex].Cells["Column36"];
                dgvCustomerInfo.CurrentCell = cell;
                dgvCustomerInfo.BeginEdit(true);
            }
        }
        //修改完成触发
        private void dgvCustomerInfo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SPRetInfo ret = new SPRetInfo();
            AgreeClient info = dgvCustomerInfo.CurrentRow.DataBoundItem as AgreeClient;

            //最终协议量
            if (e.ColumnIndex == 18 && e.RowIndex != -1)
            {
                string colName = "FINALVALUES";
                int roleType = 2;
                if (StringUtils.IsNull(info.FinalValues))
                {
                    info.FinalValues = "-1";
                }
                dao.UpdateColValues(colName, info.FinalValues, info.AgreementId, roleType, ret);
                if (ret.num != "1")
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                }
                return;
            }
        }

        #endregion
        // 改变DataGridView列宽时将下拉列表框设为不可见
        private void dgvCustomerInfo_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.Seal_ComboBox.Visible = false;
            this.Onfi_ComboBox.Visible = false;
        }
        // 滚动DataGridView时将下拉列表框设为不可见
        private void dgvCustomerInfo_Scroll(object sender, ScrollEventArgs e)
        {
            this.Seal_ComboBox.Visible = false;
            this.Onfi_ComboBox.Visible = false;
        }
        #region  下拉框处理
        private void dgvCustomerInfo_CurrentCellChanged(object sender, EventArgs e)
        {
            try {
                if (this.dgvCustomerInfo.CurrentCell.ColumnIndex == 16)
                {
                    Rectangle rect = dgvCustomerInfo.GetCellDisplayRectangle(dgvCustomerInfo.CurrentCell.ColumnIndex, dgvCustomerInfo.CurrentCell.RowIndex, false);
                    string SealValue = dgvCustomerInfo.CurrentCell.Value.ToString();
                    for (int i = 0; i < Seal.Count; i++)
                    {
                        if (SealValue == StringUtils.GetKey(Seal[i], SealDic))
                        {
                            Seal_ComboBox.Text = Seal[i];
                        }

                    }
                    Seal_ComboBox.Left = rect.Left;
                    Seal_ComboBox.Top = rect.Top;
                    Seal_ComboBox.Width = rect.Width;
                    Seal_ComboBox.Height = rect.Height;
                    Seal_ComboBox.Visible = true;
                }
                else
                {
                    Seal_ComboBox.Visible = false;
                }

                if (this.dgvCustomerInfo.CurrentCell.ColumnIndex == 17)
                {
                    Rectangle rect = dgvCustomerInfo.GetCellDisplayRectangle(dgvCustomerInfo.CurrentCell.ColumnIndex, dgvCustomerInfo.CurrentCell.RowIndex, false);
                    string OnfiValue = dgvCustomerInfo.CurrentCell.Value.ToString();
                    for (int i = 0; i < Onfi.Count; i++)
                    {
                        if (OnfiValue == StringUtils.GetKey(Onfi[i], OnfiDic))
                        {
                            Seal_ComboBox.Text = Onfi[i];
                        }

                    }
                    Onfi_ComboBox.Left = rect.Left;
                    Onfi_ComboBox.Top = rect.Top;
                    Onfi_ComboBox.Width = rect.Width;
                    Onfi_ComboBox.Height = rect.Height;
                    Onfi_ComboBox.Visible = true;
                }
                else
                {
                    Onfi_ComboBox.Visible = false;
                }

            }
            catch {

            }



        }

        //private void dgvCustomerInfo_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        //{
            //for (int i = 0; i < this.dgvCustomerInfo.Rows.Count; i++)
            //{
            //    if (dgvCustomerInfo.Rows[i].Cells["Column34"].Value != null && dgvCustomerInfo.Rows[i].Cells["Column34"].ColumnIndex == 16)
            //    {
            //        dgvCustomerInfo.Rows[i].Cells["Column34"].Tag = dgvCustomerInfo.Rows[i].Cells["Column34"].Value.ToString();
            //        for (int j = 0; j < Seal.Count; j++)
            //        {
            //            if (dgvCustomerInfo.Rows[j].Cells["Column34"].Value.ToString() == StringUtils.GetKey(Seal[j], SealDic))
            //            {
            //                dgvCustomerInfo.Rows[j].Cells["Column34"].Value = Seal[j];
            //            }

            //        }
            //    }

            //    if (dgvCustomerInfo.Rows[i].Cells["Column35"].Value != null && dgvCustomerInfo.Rows[i].Cells["Column35"].ColumnIndex == 17)
            //    {
            //        dgvCustomerInfo.Rows[i].Cells["Column35"].Tag = dgvCustomerInfo.Rows[i].Cells["Column35"].Value.ToString();
            //        for (int j = 0; j < Onfi.Count; j++)
            //        {
            //            if (dgvCustomerInfo.Rows[j].Cells["Column35"].Value.ToString() == StringUtils.GetKey(Onfi[j], OnfiDic))
            //            {
            //                dgvCustomerInfo.Rows[j].Cells["Column35"].Value = Onfi[j];
            //            }

            //        }
            //    }



            //}
        //}
        #endregion


    }
}

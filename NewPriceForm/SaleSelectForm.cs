using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PriceManager
{
    public partial class SaleSelectForm :  DockContent
    {
        APDao_GoodsClientPrice dao = new APDao_GoodsClientPrice();
        //查询视图返回的
        private SortableBindingList<SalePriceEx> ScmPriceExeList = new SortableBindingList<SalePriceEx>();

        private SortableBindingList<SalePriceEx> LastList = new SortableBindingList<SalePriceEx>();

        Boolean bo1 = true;
        Boolean bo2 = true;

        public SaleSelectForm()
        {
            InitializeComponent();
        }

        private void SaleSelectForm_Load(object sender, EventArgs e)
        {
            dgvPriceExe.AutoGenerateColumns = false;
        }

        private void dgvPriceExeClear()
        {
            ScmPriceExeList.Clear();
            LastList.Clear();
            dgvPriceExe.DataSource = LastList;
            dgvPriceExe.Refresh();
        }
        private void txtClientCode_Leave(object sender, EventArgs e)
        {
            bo1 = true;
            if (!StringUtils.IsNull(txtClientCode.Text)) {
                string strs = dao.getCodeCounts(txtClientCode.Text.ToString().Trim());
                if (StringUtils.IsNull(strs))
                {
                    MessageBox.Show("填写的客户代码不存在！", "程序提示");
                    bo1 = false;
                    txtClientCode.Focus();
                    return;
                }
            }
        }
        private void txtGoods_Leave(object sender, EventArgs e)
        {
            bo2 = true;
            if (!StringUtils.IsNull(txtGoods.Text))
            {
                string str = dao.getGoodsCounts(txtGoods.Text.ToString().Trim());
                if (StringUtils.IsNull(str))
                {
                    MessageBox.Show("填写的商品代码不存在！", "程序提示");
                    bo2 = false;
                    txtGoods.Focus();
                    return;
                }
            }
        }
        private void btnSel_Click(object sender, EventArgs e)
        {
            dgvPriceExeClear();
            this.Cursor = Cursors.WaitCursor;
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            int i = 0;
            if (!StringUtils.IsNull(txtClientCode.Text))
            {            
                i++;
            }
            if (!StringUtils.IsNull(txtGoods.Text))
            {               
                i++;
            }
            if (!StringUtils.IsNull(txtClientName.Text))
            {
                sqlkeydict.Add("cstname%", "%" + txtClientName.Text.ToString() + "%");
            }
            if (!StringUtils.IsNull(txtRegion.Text))
            {
                sqlkeydict.Add("region%", "%" + txtRegion.Text.ToString() + "%");
            }
            if (!StringUtils.IsNull(txtClientGroupCode.Text))
            {
                sqlkeydict.Add("hdrcode%","%"+ txtClientGroupCode.Text.ToString() + "%");
            }
            if (!StringUtils.IsNull(txtClientGroupName.Text))
            {
                sqlkeydict.Add("hdrname%", "%" + txtClientGroupName.Text.ToString() + "%");
            }
            if (!StringUtils.IsNull(txtBGoodsName.Text))
            {
                sqlkeydict.Add("name%", "%" + txtBGoodsName.Text.ToString() + "%");
            }
            if (!StringUtils.IsNull(txtBProducer.Text))
            {
                sqlkeydict.Add("producer%", "%" + txtBProducer.Text.ToString() + "%");
            }
            if (i<= 0)
            {
                MessageBox.Show("客户代码或者商品代码，两者必须填写其中一项！", "程序提示");
                this.Cursor = Cursors.Default;
                return;
            }
            if ((bo1==true)&&(bo2==true)) {
                //只填写商品代码
                if (StringUtils.IsNull(txtClientCode.Text) && StringUtils.IsNotNull(txtGoods.Text))
                {
                    //查询视图
                    ScmPriceExeList = dao.GetSalePriceExeLists(sqlkeydict,txtGoods.Text.ToString().Trim());
                }
                //只填写了客户代码
                if (StringUtils.IsNotNull(txtClientCode.Text) && StringUtils.IsNull(txtGoods.Text))
                {
                    //查询视图
                    ScmPriceExeList = dao.GetSalePriceExeListss(sqlkeydict,txtClientCode.Text.ToString().Trim());

                }
                //都填写了
                if (StringUtils.IsNotNull(txtClientCode.Text) && StringUtils.IsNotNull(txtGoods.Text))
                {
                    //查询视图
                    ScmPriceExeList = dao.GetSalePriceExeListsss(sqlkeydict, txtClientCode.Text.ToString().Trim(), txtGoods.Text.ToString().Trim());

                }
            }
            
            ////查询视图  
            //ScmPriceExeList = dao.GetSalePriceExeList(sqlkeydict);

            LastList = dao.GetSaleExe(ScmPriceExeList);


            if (LastList.Count <= 0)
            {
                MessageBox.Show("未查询到数据！", "程序提示");
                dgvPriceExe.DataSource = null;
                this.Cursor = Cursors.Default;
                return;
            }

            dgvPriceExe.DataSource = LastList;
            dgvPriceExe.Refresh();
            dgvPriceExe.CurrentCell = null;
            this.Cursor = Cursors.Default;
            BtnExport.Enabled = true;

        }

        private void txtClientCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键
            {
                this.btnSel_Click(sender, e);//触发按钮事件
            }
        }

        private void txtGoods_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键
            {
                this.btnSel_Click(sender, e);//触发按钮事件
            }
        }

        private void txtClientName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键
            {
                this.btnSel_Click(sender, e);//触发按钮事件
            }
        }

        private void txtRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键
            {
                this.btnSel_Click(sender, e);//触发按钮事件
            }
        }

        private void txtClientGroupCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键
            {
                this.btnSel_Click(sender, e);//触发按钮事件
            }
        }

        private void txtClientGroupName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键
            {
                this.btnSel_Click(sender, e);//触发按钮事件
            }
        }

        private void txtBGoodsName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键
            {
                this.btnSel_Click(sender, e);//触发按钮事件
            }
        }

        private void txtBProducer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键
            {
                this.btnSel_Click(sender, e);//触发按钮事件
            }
        }
        //excel导出
        private void BtnExport_Click(object sender, EventArgs e)
        {
            //申明保存对话框    
            SaveFileDialog dlg = new SaveFileDialog();
            //默然文件后缀    
            dlg.DefaultExt = "xlsx";
            //文件后缀列表    
            dlg.Filter = "EXCEL文件(*.XLS;*XLSX)|*.xlsx;*.xls";
            //默然路径是系统当前路径    
            dlg.InitialDirectory = Directory.GetCurrentDirectory();
            //打开保存对话框    
            if (dlg.ShowDialog() == DialogResult.Cancel) return;
            //返回文件路径    
            string fileNameString = dlg.FileName;
            //验证strFileName是否为空或值无效    
            if (fileNameString.Trim() == " ")
            { return; }

            this.Cursor = Cursors.WaitCursor;

            // 导出到excel
            //ExcelHelper.DataGridViewToExcel(dgvBill);
            //导出到excel时打开excel
            //ExcelHelper.DataGridviewShowToExcel(dgvBill, true);
            //导出到excel速度最快
            DataTable dt = GetDgvToTable(dgvPriceExe);
            string errMsg;
            if (ExcelHelper.ExportDataToExcel(dt, fileNameString, false, out errMsg))
            {
                MessageBox.Show(errMsg, "系统提示！");
            }
            this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// 绑定DataGridView数据到DataTable
        /// </summary>
        /// <param name="dgv">复制数据的DataGridView</param>
        /// <returns>返回的绑定数据后的DataTable</returns>
        public DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].HeaderText.ToString());
                dt.Columns.Add(dc);
            }
            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            //选择需要导出的字段
            DataTable t = dt.DefaultView.ToTable("dt", true, "部门id", "客户代码", "客户名称", "商品代码", "商品名称", "规格", "生产厂家", "国药准字", "含税公开销价", "无税公开销价","价格信息", "最近合同价", "件包装", "采购员", "计划员");

            return t;
        }


    }
}

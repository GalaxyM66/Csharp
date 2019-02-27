using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PriceManager.B2BTools
{
    public partial class ImportGoodRecordExcelForm : Form
    {
        public ImportGoodRecordExcelForm()
        {
            InitializeComponent();
        }
        APDao_B2BTools dao = new APDao_B2BTools();
        //excel表数据
        SortableBindingList<InoutGenGoodRecordXlstemp> ExcelList = new SortableBindingList<InoutGenGoodRecordXlstemp>();
        //临时表数据
        SortableBindingList<InoutGenGoodRecordXlstemp> TempList = new SortableBindingList<InoutGenGoodRecordXlstemp>();

        //处理完成显示List
        SortableBindingList<InoutGenGoodRecordXlstemp> GenGoodRecordList = new SortableBindingList<InoutGenGoodRecordXlstemp>();
        //清空界面
        private void clearUI()
        {
            ExcelList.Clear();
            TempList.Clear();
            GenGoodRecordList.Clear();
        }
        ////////-----2018-8-1--------------------
        //导入Excel事件
        private void importBtn_Click(object sender, EventArgs e)
        {
            //选择文件
            OpenFileDialog frm = new OpenFileDialog();
            frm.Filter = "Excel文件(*.xls,xlsx)|*.xls;*.xlsx";
            frm.ShowReadOnly = true; //设定文件是否只读
            if (frm.ShowDialog() != DialogResult.OK)
            {
                txtImport.Text = "";

                return;
            }
            clearUI();
            txtImport.Text = frm.FileName;

            //获取文件名
            string fileName = txtImport.Text.ToString();
            ExcelList = ExcelHelper.OpenImportExcelGGR(fileName);
            if (ExcelList.Count <= 0) return;
            foreach (InoutGenGoodRecordXlstemp info in ExcelList)
            {

                info.Empid = SessionDto.Empid;
                info.Ownerid = Properties.Settings.Default.OWNERID;
                info.Compid = Properties.Settings.Default.COMPID;


                TempList.Add(info);
            }

            SPRetInfo ret = new SPRetInfo();
            GenGoodRecordList = dao.PGetGenGoodRecordData(TempList, ret);

            dgvImportGoodRecordExcel.DataSource = GenGoodRecordList;
            dgvImportGoodRecordExcel.Refresh();


        }
        //导出事件
        private void exportBtn_Click(object sender, EventArgs e)
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

            //this.Cursor = Cursors.WaitCursor;

            // 导出到excel
            //ExcelHelper.DataGridViewToExcel(dgvBill);
            //导出到excel时打开excel
            //ExcelHelper.DataGridviewShowToExcel(dgvBill, true);
            //导出到excel速度最快
            DataTable dt = GetDgvToTable(dgvImportGoodRecordExcel);
            string errMsg;
            if (ExcelHelper.ExportDataToExcel(dt, fileNameString, false, out errMsg))
            {
                MessageBox.Show(errMsg, "系统提示！");
            }
            //this.Cursor = Cursors.Default;
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
            DataTable t = dt.DefaultView.ToTable("dt", true, "序号", "客户代码", "客户商品代码", "品名规格", "转换比", "厂家", "条形码", "国药准字", "备案原因","导入状态","结果相关信息");

            return t;
        }

        private void ImportGoodRecordExcelForm_Load(object sender, EventArgs e)
        {
            dgvImportGoodRecordExcel.AutoGenerateColumns = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PriceManager
{
    public partial class PromoInfoXlsForm : Form
    {
        //excel表数据
        SortableBindingList<PromoExcelInfo> ExcelList = new SortableBindingList<PromoExcelInfo>();
        //临时表数据
        SortableBindingList<PromoExcelInfo> TempList = new SortableBindingList<PromoExcelInfo>();
        //处理完成显示List
        SortableBindingList<PromoExcelInfo> ClientInfoXlsList = new SortableBindingList<PromoExcelInfo>();
        //校验完成显示List
        SortableBindingList<PromoExcelInfo> CheckedList = new SortableBindingList<PromoExcelInfo>();
        //导入完成显示List
        SortableBindingList<PromoExcelInfo> InsertList = new SortableBindingList<PromoExcelInfo>();

        APDao_B2BTools dao = new APDao_B2BTools();
        SPRetInfo ret = new SPRetInfo();
        string BatchId = "";
        public PromoInfoXlsForm()
        {
            InitializeComponent();
        }
        private void clearUI()
        {

            ExcelList.Clear();
            TempList.Clear();
            ClientInfoXlsList.Clear();

        }
        private void PromoInfoXlsForm_Load(object sender, EventArgs e)
        {
            dgvPoromsIn.AutoGenerateColumns = false;
        }
        private void BtnImport_Click(object sender, EventArgs e)
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
            txtImport.Text = frm.FileName;
            clearUI();
            //获取文件名
            string fileName = txtImport.Text.ToString();
            ExcelList = ExcelHelper.ImportExcePromo(fileName);
            if (ExcelList.Count <= 0) return;
            foreach (PromoExcelInfo info in ExcelList)
            {

                info.EmpName = SessionDto.Empname;
                info.Ownerid = Properties.Settings.Default.OWNERID;
                info.Compid = Properties.Settings.Default.COMPID;


                TempList.Add(info);
            }
            ClientInfoXlsList = dao.GetPromosData(TempList, ret);
            BatchId = ret.result;
            dgvPoromsIn.DataSource = ClientInfoXlsList;
            dgvPoromsIn.Refresh();
            BtnCheck.Enabled = true;
            BtnExport.Enabled = true;
        }
        //校验
        private void BtnCheck_Click(object sender, EventArgs e)
        {
            CheckedList.Clear();
            CheckedList = dao.chackPromosInfo(ClientInfoXlsList, BatchId);
            dgvPoromsIn.DataSource = CheckedList;
            dgvPoromsIn.Refresh();
            BtnInsert.Enabled = true;
        }
        //导入
        private void BtnInsert_Click(object sender, EventArgs e)
        {
            InsertList.Clear();
            InsertList = dao.insertPromosInfo(ClientInfoXlsList, BatchId);
            dgvPoromsIn.DataSource = InsertList;
            dgvPoromsIn.Refresh();
        }
        //导出
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

            //this.Cursor = Cursors.WaitCursor;

            // 导出到excel
            //ExcelHelper.DataGridViewToExcel(dgvBill);
            //导出到excel时打开excel
            //ExcelHelper.DataGridviewShowToExcel(dgvBill, true);
            //导出到excel速度最快
            DataTable dt = GetDgvToTable(dgvPoromsIn);
            string errMsg;
            if (ExcelHelper.ExportDataToExcel(dt, fileNameString, false, out errMsg))
            {
                MessageBox.Show(errMsg, "系统提示！");
            }

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
            DataTable t = dt.DefaultView.ToTable("dt", true, "序号", "活动政策名称", "商品代码", "活动对象代码", "活动开始时间", "活动结束时间", "活动政策", "备注", "检查状态", "检查信息", "导入状态", "导入信息");

            return t;
        }


    }
}

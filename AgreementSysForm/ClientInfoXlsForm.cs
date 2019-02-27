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
    public partial class ClientInfoXlsForm : Form
    {
        APDao_Agreement dao = new APDao_Agreement();

        //excel表数据
        SortableBindingList<ClientXlsInfo> ExcelList = new SortableBindingList<ClientXlsInfo>();
        //临时表数据
        SortableBindingList<ClientXlsInfo> TempList = new SortableBindingList<ClientXlsInfo>();
        //处理完成显示List
        SortableBindingList<ClientXlsInfo> ClientInfoXlsList = new SortableBindingList<ClientXlsInfo>();
        //校验完成显示List
        SortableBindingList<ClientXlsInfo> CheckedList = new SortableBindingList<ClientXlsInfo>();
        //导入完成显示List
        SortableBindingList<ClientXlsInfo> InsertList = new SortableBindingList<ClientXlsInfo>();
        //导出完成显示List
        SortableBindingList<ClientXlsInfo> ImportedList = new SortableBindingList<ClientXlsInfo>();

        SPRetInfo ret = new SPRetInfo();
        string BatchId = "";
        public ClientInfoXlsForm()
        {
            InitializeComponent();
            dgvClientInfo.AutoGenerateColumns = false;
        }
        private void clearUI() {

            ExcelList.Clear();
            TempList.Clear();
            ClientInfoXlsList.Clear();

        }
        //导入excel文件
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
            ExcelList = ExcelHelper.OpenImportExceCI(fileName);
            if (ExcelList.Count <= 0) return;
            foreach (ClientXlsInfo info in ExcelList)
            {

                info.Empid = SessionDto.Empid;
                info.Ownerid = Properties.Settings.Default.OWNERID;
                info.Compid = Properties.Settings.Default.COMPID;
                info.SaleDeptid = SessionDto.Empdeptid;


                TempList.Add(info);
            }
            ClientInfoXlsList = dao.GetClientData(TempList,ret);
            BatchId = ret.result;
            dgvClientInfo.DataSource = ClientInfoXlsList;
            dgvClientInfo.Refresh();

        }
        //校验
        private void BtnCheck_Click(object sender, EventArgs e)
        {
            CheckedList.Clear();
            CheckedList =dao.chackClientInfo(ClientInfoXlsList, BatchId);
            dgvClientInfo.DataSource = CheckedList;
            dgvClientInfo.Refresh();
            BtnInsert.Enabled = true;
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
            DataTable dt = GetDgvToTable(dgvClientInfo);
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
            DataTable t = dt.DefaultView.ToTable("dt", true, "序号", "年份", "供应商编号", "协议级别", "客户代码", "客户名称", "去年对应上游", "今年厂家预测量", "目标分级", "去年销售额", "销售代表", "销售经理", "销售副总", "检查状态", "检查信息", "导入状态", "导入信息");

            return t;
        }
        //导入
        private void BtnInsert_Click(object sender, EventArgs e)
        {
            InsertList.Clear();
            InsertList=dao.insertClientInfo(ClientInfoXlsList, BatchId);
            dgvClientInfo.DataSource = InsertList;
            dgvClientInfo.Refresh();
        }

    }
}

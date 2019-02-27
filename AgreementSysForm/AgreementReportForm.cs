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
    public partial class AgreementReportForm : DockContent
    {
        //修改下拉框类型
        string typecode = "";
        List<string> BuyerName = new List<string>();
        Dictionary<string, string> BuyerNameDic = new Dictionary<string, string>();
        Dictionary<string, string> dimension = new Dictionary<string, string>();

        APDao_Agreement dao = new APDao_Agreement();

        SortableBindingList<AgreeReport> infoList = new SortableBindingList<AgreeReport>();


        public AgreementReportForm()
        {
            InitializeComponent();
            dgvReportInfo.AutoGenerateColumns = false;
        }
        //加载界面 
        private void AgreementReportForm_Load(object sender, EventArgs e)
        {
            //默认选中
            cBYearNum.CheckState = CheckState.Checked;
            cBBuyerName.CheckState = CheckState.Checked;
            cBAgreeType.CheckState = CheckState.Checked;
            cBBeginDate.CheckState = CheckState.Checked;
            //默认年份为今年
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string date= currentTime.Year.ToString();
            txtYearNum.Text = date;
            //加载下拉框
            typecode = "BUYERNAME";
            BuyerNameDic = dao.getCbContent(typecode);
            BuyerName = StringUtils.GetValue(BuyerNameDic);
            this.cBBuyer.DataSource = BuyerName;
        }
        private void clearUI() {
            infoList.Clear();

        }
        //查询
        private void BtnSel_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            dimension.Clear();
            clearUI();
            int i = 0;
            //维度
            if (cBYearNum.CheckState == CheckState.Checked) {
                dgvReportInfo.Columns[1].Visible = true;
                dimension.Add("yearnum", "I.YEARNUM");

                i++;
            }
            else {
                dgvReportInfo.Columns[1].Visible = false;
            }
            if (cBProdName.CheckState == CheckState.Checked)
            {
                dgvReportInfo.Columns[2].Visible = true;
                dimension.Add("prod_name", "I.PROD_NAME");

                i++;
            }
            else
            {
                dgvReportInfo.Columns[2].Visible = false;
            }
            if (cBSallManager.CheckState == CheckState.Checked)
            {
                dgvReportInfo.Columns[3].Visible = true;
                dimension.Add("sallmanager", "I.SALLMANAGER");

                i++;
            }
            else
            {
                dgvReportInfo.Columns[3].Visible = false;
            }
            if (cBSaller.CheckState == CheckState.Checked)
            {
                dgvReportInfo.Columns[4].Visible = true;
                dimension.Add("saller", "I.SALLER");

                i++;
            }
            else
            {
                dgvReportInfo.Columns[4].Visible = false;
            }
            if (cBMiddleMan.CheckState == CheckState.Checked)
            {
                dgvReportInfo.Columns[5].Visible = true;
                dimension.Add("middle", "I.MIDDLEMAN");

                i++;
            }
            else
            {
                dgvReportInfo.Columns[5].Visible = false;
            }
            if (cBSallLeader.CheckState == CheckState.Checked)
            {
                dgvReportInfo.Columns[6].Visible = true;
                dimension.Add("sallleader", "I.SALLLEADER");
                i++;
            }
            else
            {
                dgvReportInfo.Columns[6].Visible = false;
            }
            if (cBBuyerName.CheckState == CheckState.Checked)
            {
                dgvReportInfo.Columns[7].Visible = true;
                dimension.Add("buyername", "PIBUYERNAMENAME");
                i++;
            }
            else
            {
                dgvReportInfo.Columns[7].Visible = false;
            }
            if (cBAgreeType.CheckState == CheckState.Checked)
            {
                dgvReportInfo.Columns[8].Visible = true;
                dimension.Add("agreetype", "I.AGREETYPENAME");
                i++;
            }
            else
            {
                dgvReportInfo.Columns[8].Visible = false;
            }
            if (cBBeginDate.CheckState == CheckState.Checked)
            {
                dgvReportInfo.Columns[9].Visible = true;
                dimension.Add("begindate", "I.BEGINDATE");
                i++;
            }
            else
            {
                dgvReportInfo.Columns[9].Visible = false;
            }
            //判断维度至少勾选一项
            if (i <= 0)
            {
                MessageBox.Show("维度至少勾选一项！", "前台提示");
                this.Cursor = Cursors.Default;
                return;
            }
            else {
                int j = 0;
                if (!StringUtils.IsNull(txtYearNum.Text.ToString().Trim())) {
                    sqlkeydict.Add("yearnum", txtYearNum.Text.ToString().Trim());
                    j++;
                }
                if (!StringUtils.IsNull(txtProdName.Text.ToString().Trim()))
                {
                    sqlkeydict.Add("prod_name%", "%" + txtProdName.Text.ToString().Trim() + "%");
                    j++;
                }
                if (!StringUtils.IsNull(txtSallManager.Text.ToString().Trim()))
                {
                    sqlkeydict.Add("sallmanager%", "%" + txtSallManager.Text.ToString().Trim() + "%");
                    j++;
                }
                if (!StringUtils.IsNull(txtSaller.Text.ToString().Trim()))
                {
                    sqlkeydict.Add("saller%", "%" + txtSaller.Text.ToString().Trim() + "%");
                    j++;
                }
                if (!StringUtils.IsNull(txtMiddleMan.Text.ToString().Trim()))
                {
                    sqlkeydict.Add("middleman%", "%" + txtMiddleMan.Text.ToString().Trim() + "%");
                    j++;
                }
                if (!StringUtils.IsNull(txtSallLeader.Text.ToString().Trim()))
                {
                    sqlkeydict.Add("sallleader%", "%" + txtSallLeader.Text.ToString().Trim() + "%");
                    j++;
                }
                //获取下拉框的值
                string BuyerName = cBBuyer.SelectedItem.ToString().Trim();
                string BuyerNameKey = "";
                BuyerNameKey = StringUtils.GetKey(BuyerName, BuyerNameDic);
                if (!StringUtils.IsNull(BuyerName))
                {
                    sqlkeydict.Add("PIBUYERNAME", BuyerNameKey);
                    i++;
                }
                //判断查询条件非空
                if (j <= 0)
                {
                    MessageBox.Show("至少填入一个查询条件！", "前台提示");
                    this.Cursor = Cursors.Default;
                    return;
                }
                else
                {
                    infoList=dao.GetAgreeReport(sqlkeydict, dimension);
                    if (infoList.Count <= 0)
                    {
                        MessageBox.Show("未查询到数据！", "程序提示");
                        dgvReportInfo.DataSource = null;
                        //dgvAgreementInfo.Refresh();
                        this.Cursor = Cursors.Default;
                        return;
                    }
                    else
                    {
                        dgvReportInfo.DataSource = infoList;
                        dgvReportInfo.Refresh();
                        dgvReportInfo.CurrentCell = null;
                        this.Cursor = Cursors.Default;
                    }

                }
            }         
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
            DataTable dt = GetDgvToTable(dgvReportInfo);
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
            DataTable t = dt.DefaultView.ToTable("dt", true,"年份","商务团队","销售经理","销售代表","销售对接人","销售副总","采购员","协议性质","协议启动时间","必保客户数", "争取客户数", "其他客户", "必保和争取目标客户总数", "销售反馈客户数","销售反馈率", "意向客户数","意向客户率", "必保客户签订数", "必保客户签订率","争取客户签订数", "争取客户签订率", "整体签订客户数", "整体签订率");
            return t;
        }


    }
}

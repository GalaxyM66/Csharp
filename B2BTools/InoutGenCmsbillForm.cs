using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;
using Microsoft.Office.Interop.Excel;
using System.Collections;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
//--------------------------2018-8-8----------------------------------
namespace PriceManager
{
    public partial class InoutGenCmsbillForm : DockContent
    {
        public InoutGenCmsbillForm()
        {
            InitializeComponent();
            dgvGenCmsbill.AutoGenerateColumns = false;
        }
        APDao_B2BTools dao = new APDao_B2BTools();

        //excel表数据
        SortableBindingList<InoutGenCmsbillXlstemp> ExcelList = new SortableBindingList<InoutGenCmsbillXlstemp>();
        //写入临时表数据
        SortableBindingList<InoutGenCmsbillXlstemp> TempList = new SortableBindingList<InoutGenCmsbillXlstemp>();
        //Goodid存在的临时List
        SortableBindingList<InoutGenCmsbillXlstemp> GoodidTempList = new SortableBindingList<InoutGenCmsbillXlstemp>();
        //已从MySQL获取价格写回中间表List
        SortableBindingList<InoutGenCmsbillXlstemp> PrcTempList = new SortableBindingList<InoutGenCmsbillXlstemp>();
        //处理完成显示List
        SortableBindingList<InoutGenCmsbillXlstemp> InoutGenCmsbillXlstempList = new SortableBindingList<InoutGenCmsbillXlstemp>();
        //临时List
        SortableBindingList<InoutGenCmsbillXlstemp> GenCmsBillList = new SortableBindingList<InoutGenCmsbillXlstemp>();
        //写回中间表List
        SortableBindingList<InoutGenCmsbillXlstemp> CheckedTempList = new SortableBindingList<InoutGenCmsbillXlstemp>();
        //处理完成显示List
        SortableBindingList<InoutGenCmsbillXlstemp> ImportedTempList = new SortableBindingList<InoutGenCmsbillXlstemp>();
        //筛选完成显示List
        SortableBindingList<InoutGenCmsbillXlstemp> ScreenList = new SortableBindingList<InoutGenCmsbillXlstemp>();
        //对码刷新-未对码
        SortableBindingList<InoutGenCmsbillXlstemp> NotGoodidlist = new SortableBindingList<InoutGenCmsbillXlstemp>();
        //合同处理控制
        SortableBindingList<InoutGenCmsbillXlstemp> ContractConlist = new SortableBindingList<InoutGenCmsbillXlstemp>();

        //下拉框List
        Dictionary<string, string> myDictionary = new Dictionary<string, string>();
        List<string> list = new List<string>();
        //暂存客户id
        string Cstid = "";
        //操作批次号
        string Batchid = "";
        //客户代码
        string cstcode = "";
        //客户信息
        string cstinfo = "";
        string fileName = "";
        string path = "";

        //当前行
        int rows = -1;
       
        //界面初始化加载项
        private void InoutGenCmsbillForm_Load(object sender, EventArgs e)
        {
            //下拉框加载
            myDictionary = dao.getCheckMsg();
            // 遍历字典中的值
            foreach (var item in myDictionary.Values)
            {
                list.Add(item);
            }
            this.cBCheckmsg.DataSource = list;

            this.cBColdChain.Items.AddRange(new object[] {
                "",
                "是",
                "否"
            });
            this.cBColdChain.SelectedIndex = 0;

            this.cBInvPostFlag.Items.AddRange(new object[] {
                "是",
                "否"
            });
            this.cBInvPostFlag.SelectedIndex = 0;
            
            //添加复选框列
            DataGridViewCheckBoxColumn columcb = new DataGridViewCheckBoxColumn();
            columcb.HeaderText = "  选  择";
            columcb.Name = "cb_check";
            columcb.TrueValue = true;
            columcb.FalseValue = false;
            columcb.DataPropertyName = "IsChecked";
            dgvGenCmsbill.Columns.Insert(0,columcb);
        }

        //清空界面
        private void clearUI()
        {
            ExcelList.Clear();
            TempList.Clear();
            GoodidTempList.Clear();
            PrcTempList.Clear();
            InoutGenCmsbillXlstempList.Clear();
            GenCmsBillList.Clear();
            CheckedTempList.Clear();
            ImportedTempList.Clear();         
            ScreenList.Clear();
            NotGoodidlist.Clear();
        }


        //客户代码回车
        private void txtCstcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCstcode.Text)) return;
            if (e.KeyValue == 13)
            {
                cstcode = txtCstcode.Text;
                SPRetInfo ret = new SPRetInfo();
                dao.PCstCode(cstcode, ret);
                if (ret.num == "1")
                {
                    cstinfo = ret.msg;
                    txtCstInfo.Text = cstinfo;
                    Cstid = ret.result;
                    //1.提示用户效期要求 --2018-11-1-----
                    dao.Tips(Cstid, ret);
                    if (ret.num == "1")
                    {
                        MessageBox.Show(ret.msg + "|" + ret.num, "后台提示！");
                    }
                    else
                    {
                        MessageBox.Show(ret.msg + "|" + ret.num, "后台提示！");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(ret.msg + "|" + ret.num, "后台提示！");
                    cstinfo = "";
                    txtCstInfo.Text = cstinfo;
                    Cstid = "";
                    return;
                }
               
            }
        }
        //打开文件按钮
        private void txtCstInfo_TextChanged(object sender, EventArgs e)
        {
            if (txtCstInfo.Text == "")
            {
                openFileBtn.Enabled = false;
            }
            else
            {
                openFileBtn.Enabled = true;
            }
           
        }
        #region 打开excel文件事件
        private void openFileBtn_Click(object sender, EventArgs e)
        {
            submitCmsBtn.Enabled = false;
            cBCheckmsg.SelectedIndex = 0;
            cBColdChain.SelectedIndex = 0;
            txtCmsAdress.Text = "";
            //--------------------导入excel表--------------------
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "订单文件|*.xlsx;*.xls|All files (*.*)|*.*"; //过滤文件类型
            fd.ShowReadOnly = true; //设定文件是否只读
            DialogResult r = fd.ShowDialog();
            if (r != DialogResult.OK)
            {
                txtFileName.Text = "";
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            clearUI();
            txtFileName.Clear();
            txtFileName.Text = fd.FileName;
            path = Path.GetDirectoryName(fd.FileName);
            fileName = fd.SafeFileName;

            //System.DateTime currentTime = new System.DateTime();
            //currentTime = System.DateTime.Now;
            //int bs = currentTime.Second;
            

            
            //---------------------------------------------------

            //---------------------处理excel表-------------------
            ExcelList = ExcelHelper.OpenInquiryExcelGCB(fd.FileName);
            if (ExcelList.Count <= 0) return;
            foreach (InoutGenCmsbillXlstemp info in ExcelList)
            {
                info.Cstid = Cstid;
                info.Empid = SessionDto.Empid;
                info.Ownerid = Properties.Settings.Default.OWNERID;
                info.Compid = Properties.Settings.Default.COMPID;
                TempList.Add(info);
            }
            //-------------------查询goodid------------------8.13--------
            SPRetInfo ret = new SPRetInfo();
            dao.PGetGenCmsBillData(TempList, Cstid, ret);
            //
            if (ret.num == "1")
            {
                Batchid = ret.result;
                GoodidTempList = dao.GetGoodid(Batchid);
                //------------------------------------------------
                if (GoodidTempList.Count > 0)
                {
                    //------------Goodid存在的调用MySQL查询价格-------
                    InoutGenCmsbillXlstemp OutputGoodidInfo = new InoutGenCmsbillXlstemp();
                    foreach (InoutGenCmsbillXlstemp InputGoodidInfo in GoodidTempList)
                    {
                        OutputGoodidInfo = InputGoodidInfo;
                        dao.PGetB2BPrcs(InputGoodidInfo, OutputGoodidInfo);
                        if (!string.IsNullOrEmpty(OutputGoodidInfo.Prcresultcode))//!=
                        {
                            if (string.IsNullOrEmpty(OutputGoodidInfo.Prc))
                            {
                                OutputGoodidInfo.Prc = "-1";
                                OutputGoodidInfo.Price = "-1";
                                OutputGoodidInfo.Bottomprc = "-1";
                                OutputGoodidInfo.Bottomprice = "-1";
                                OutputGoodidInfo.Costprc = "-1";
                                OutputGoodidInfo.Costprice = "-1";
                                PrcTempList.Add(OutputGoodidInfo);
                            }
                            else
                            {
                                PrcTempList.Add(OutputGoodidInfo);
                            }

                        }
                    }

                    //------------------------------------------------
                    //------------------将价格写回中间表-------------
                    SPRetInfo SetRet = new SPRetInfo();
                    foreach (InoutGenCmsbillXlstemp SetBillPrcInputInfo in PrcTempList)
                    {
                        dao.PSetBillPrcs(SetBillPrcInputInfo, SetRet);
                        if (SetRet.num == "-1")
                            MessageBox.Show(SetRet.msg + "|" + SetRet.num, "后台提示！");
                    }
                }
            }
            else
            {
                MessageBox.Show(ret.msg + "|" + ret.num, "后台提示！");
                return;
            }
            //再次查询中间表
            InoutGenCmsbillXlstempList = dao.GetInoutGenCmsbillXlstempList(Batchid);
            dgvGenCmsbill.DataSource = InoutGenCmsbillXlstempList;
            //选中框逻辑
            for (int i=0;i< InoutGenCmsbillXlstempList.Count;i++) {
                if (InoutGenCmsbillXlstempList[i].ChooseFlag == "1")
                {
                    ((DataGridViewCheckBoxCell)dgvGenCmsbill.Rows[i].Cells[0]).Value = true;
                }
                else {
                    ((DataGridViewCheckBoxCell)dgvGenCmsbill.Rows[i].Cells[0]).Value = false;
                }
            }

            dgvGenCmsbill.Refresh();
            this.Cursor = Cursors.Default;//加载完成，改变鼠标样式
            if (dgvGenCmsbill.Rows.Count != 0) {
                checkBtn.Enabled = true;
                RefreshBtn.Enabled = true;
                ScreenBtn.Enabled = true;
                exportBtn.Enabled = true;
                AllSelBtn.Enabled = true;
                cancelBtn.Enabled = true;
                
            }
            //System.DateTime endTime = new System.DateTime();
            //endTime = System.DateTime.Now;
            //int bss = endTime.Second;
            //MessageBox.Show("用时：" + (bss-bs));

        }
        #endregion
        //-----------------------2018-8-9--------------------------
        //校验事件
        private void checkBtn_Click(object sender, EventArgs e)
        {
            SPRetInfo ret = new SPRetInfo();
            Color c1 = Color.FromArgb(250, 191, 143);
            CheckedTempList = dao.CheckGenCmsBillData(Batchid, ret);
            if (CheckedTempList.Count <= 0) return;
            dgvGenCmsbill.DataSource = CheckedTempList;
            //选中框逻辑
            for (int i = 0; i < CheckedTempList.Count; i++)
            {
                if (CheckedTempList[i].ChooseFlag == "1")
                {
                    ((DataGridViewCheckBoxCell)dgvGenCmsbill.Rows[i].Cells[0]).Value = true;
                }
                else
                {
                    ((DataGridViewCheckBoxCell)dgvGenCmsbill.Rows[i].Cells[0]).Value = false;
                }
            }

            //获取到勾选的数据行数
            //int j = dgvGenCmsbill.Rows.Count;
            for (int i = 0; i < dgvGenCmsbill.Rows.Count; i++)
            {
                if (dgvGenCmsbill.Rows[i].Cells["Column17"].Value.ToString() != dgvGenCmsbill.Rows[i].Cells["Column27"].Value.ToString())
                {
                    dgvGenCmsbill.Rows[i].Cells["Column17"].Style.BackColor = c1;
                }
            }
            dgvGenCmsbill.Refresh();
            submitCmsBtn.Enabled = true;
            PartSubmitBtn.Enabled = true;
            //触发筛选事件
            ScreenBtn_Click(sender, e);

        }
        //提交cms事件
        private void submitCmsBtn_Click(object sender, EventArgs e)
        {
            SPRetInfo ret = new SPRetInfo();
            ImportedTempList = dao.ImportGenCmsBillData(Batchid, ret);
            dgvGenCmsbill.DataSource = ImportedTempList;
            dgvGenCmsbill.Refresh();
            exportBtn.Enabled = true;
            ScreenBtn_Click(sender,e);

        }
        #region Excel导出优化版本（NPOI）流导出
        private void exportBtn_Click(object sender, EventArgs e)
        {
            //申明保存对话框    
            SaveFileDialog dlg = new SaveFileDialog();
            //默然文件后缀    
            dlg.DefaultExt = "xls";
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
            try
            {
                // 1.获取数据集合
                SortableBindingList<InoutGenCmsbillXlstemp> enlist = new SortableBindingList<InoutGenCmsbillXlstemp>() { };
                enlist = dao.GetInoutGenCmsbillXlstempLists(Batchid);

                // 2.设置单元格抬头
                // key：实体对象属性名称，可通过反射获取值
                // value：Excel列的名称
                Dictionary<string, string> cellheader = new Dictionary<string, string> {
            { "ExcelSeqid", "序号" },
            { "Saledeptid", "部门ID" },
            { "Gengoods", "客户商品代码" },
            { "Goods", "cms商品代码" },
            { "GoodName", "cms品名" },
            { "Spec", "cms规格" },
            { "Planprc", "计划单价" },
            { "Plancount", "计划下单量" },
            { "Quotation", "系统报价" },
            { "BizownQty", "总存库" },
            { "AllowQty", "满足效期库存" },
            { "HdrmMark", "总备注" },
            { "Pmark", "批卡备注" },
            { "Amark", "审批备注" },
            { "Genaddresscode", "地址外码" },
            { "Cmsaddresscode", "地址cms码" },
            { "Goodtype", "商品大类" },
            { "Prc", "公开销价" },
            { "Checkstate", "检查状态" },
            { "Checkmsg", "检查信息" },
            { "Importstate", "导入状态" },
            { "Importmsg", "导入信息" }

        };
                // 3.进行Excel转换操作
                string urlPath = ExcelHelper.EntityListToExcel2003(cellheader, enlist, "导出excel", fileNameString);
                System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                MessageBox.Show("导出成功！");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        #endregion
        #region excel导出
        //private void exportBtn_Click(object sender, EventArgs e)
        //{
        //    //查询临时表获取数据源
        //    InoutGenCmsbillXlstempList = dao.GetInoutGenCmsbillXlstempList(Batchid);
        //    //申明保存对话框    
        //    SaveFileDialog dlg = new SaveFileDialog();
        //    //默然文件后缀    
        //    dlg.DefaultExt = "xlsx";
        //    //文件后缀列表    
        //    dlg.Filter = "EXCEL文件(*.XLS;*XLSX)|*.xlsx;*.xls";
        //    //dlg.Filter = "EXCEL文件(*.XLS)|*.xls";
        //    //默然路径是系统当前路径    
        //    dlg.InitialDirectory = Directory.GetCurrentDirectory();
        //    //打开保存对话框    
        //    if (dlg.ShowDialog() == DialogResult.Cancel) return;
        //    //返回文件路径    
        //    string fileNameString = dlg.FileName;
        //    //验证strFileName是否为空或值无效    
        //    if (fileNameString.Trim() == " ")
        //    { return; }
        //    //CreateAdvExcel(InoutGenCmsbillXlstempList, fileNameString, fieldNames, fieldNames);
        //    //this.Cursor = Cursors.WaitCursor;

        //    //导出到excel
        //    //ExcelHelper.DataGridViewToExcel(dgvBill);
        //    //导出到excel时打开excel
        //    //ExcelHelper.DataGridviewShowToExcel(dgvBill, true);
        //    //导出到excel速度最快
        //    //DataTable dt = GetDgvToTable(dgvGenCmsbill);
        //    //string errMsg;s
        //    //if (ExcelHelper.ExportDataToExcel(dt, fileNameString, false, out errMsg))
        //    //{
        //    //    MessageBox.Show(errMsg, "系统提示！");
        //    //}


        //    //string[] fieldNames = new string[] { "序号", "部门ID", "客户商品代码", "cms商品代码", "cms品名", "cms规格", "计划单价", "计划下单量", "系统报价", "总库存", "满足效期库存", "总备注", "批卡备注", "审批备注", "地址外码", "地址cms码", "商品大类", "公开销价", "检查状态", "检查信息", "导入状态", "导入信息" };
        //    object misValue = System.Reflection.Missing.Value;
        //    Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        //    Workbook xlWorkBook = xlApp.Workbooks.Add(misValue);
        //    Worksheet xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

        //    //PropertyInfo[] props = GetPropertyInfoArray();
        //   // 表头
        //        //xlWorkSheet.Cells[1, i + 1] = props[i].Name;
        //        xlWorkSheet.Cells[1,1] = "序号";
        //        xlWorkSheet.Cells[1, 2] = "部门ID";
        //        xlWorkSheet.Cells[1, 3] = "客户商品代码";
        //        xlWorkSheet.Cells[1, 4] = "cms商品代码";
        //        xlWorkSheet.Cells[1, 5] = "cms品名";
        //        xlWorkSheet.Cells[1, 6] = "cms规格";
        //        xlWorkSheet.Cells[1, 7] = "计划单价";
        //        xlWorkSheet.Cells[1, 8] = "计划下单量";
        //        xlWorkSheet.Cells[1, 9] = "系统报价";
        //        xlWorkSheet.Cells[1, 10] = "总库存";
        //        xlWorkSheet.Cells[1, 11] = "满足效期库存";
        //        xlWorkSheet.Cells[1, 12] = "总备注";
        //        xlWorkSheet.Cells[1, 13] = "批卡备注";
        //        xlWorkSheet.Cells[1, 14] = "审批备注";
        //        xlWorkSheet.Cells[1, 15] = "地址外码";
        //        xlWorkSheet.Cells[1, 16] = "地址cms码";
        //        xlWorkSheet.Cells[1, 17] = "商品大类";
        //        xlWorkSheet.Cells[1, 18] = "公开销价";
        //        xlWorkSheet.Cells[1, 19] = "检查状态";
        //        xlWorkSheet.Cells[1, 20] = "检查信息";
        //        xlWorkSheet.Cells[1, 21] = "导入状态";
        //        xlWorkSheet.Cells[1, 22] = "导入信息";

        //    //表内容
        //    for (int i = 0; i < InoutGenCmsbillXlstempList.Count; i++)
        //    {
        //        xlWorkSheet.Cells[i + 2, 1] = InoutGenCmsbillXlstempList[i].ExcelSeqid;
        //        xlWorkSheet.Cells[i + 2, 2] = InoutGenCmsbillXlstempList[i].Saledeptid;
        //        xlWorkSheet.Cells[i + 2, 3] = InoutGenCmsbillXlstempList[i].Gengoods;
        //        xlWorkSheet.Cells[i + 2, 4] = InoutGenCmsbillXlstempList[i].Goods;
        //        xlWorkSheet.Cells[i + 2, 5] = InoutGenCmsbillXlstempList[i].GoodName;
        //        xlWorkSheet.Cells[i + 2, 6] = InoutGenCmsbillXlstempList[i].Spec;
        //        xlWorkSheet.Cells[i + 2, 7] = InoutGenCmsbillXlstempList[i].Planprc;
        //        xlWorkSheet.Cells[i + 2, 8] = InoutGenCmsbillXlstempList[i].Plancount;
        //        xlWorkSheet.Cells[i + 2, 9] = InoutGenCmsbillXlstempList[i].Quotation;
        //        xlWorkSheet.Cells[i + 2, 10] = InoutGenCmsbillXlstempList[i].BizownQty;
        //        xlWorkSheet.Cells[i + 2, 11] = InoutGenCmsbillXlstempList[i].AllowQty;
        //        xlWorkSheet.Cells[i + 2, 12] = InoutGenCmsbillXlstempList[i].HdrmMark;
        //        xlWorkSheet.Cells[i + 2, 13] = InoutGenCmsbillXlstempList[i].Pmark;
        //        xlWorkSheet.Cells[i + 2, 14] = InoutGenCmsbillXlstempList[i].Amark;
        //        xlWorkSheet.Cells[i + 2, 15] = InoutGenCmsbillXlstempList[i].Genaddresscode;
        //        xlWorkSheet.Cells[i + 2, 16] = InoutGenCmsbillXlstempList[i].Cmsaddresscode;
        //        xlWorkSheet.Cells[i + 2, 17] = InoutGenCmsbillXlstempList[i].Goodtype;
        //        xlWorkSheet.Cells[i + 2, 18] = InoutGenCmsbillXlstempList[i].Prc;
        //        xlWorkSheet.Cells[i + 2, 19] = InoutGenCmsbillXlstempList[i].Checkstate;
        //        xlWorkSheet.Cells[i + 2, 20] = InoutGenCmsbillXlstempList[i].Checkmsg;
        //        xlWorkSheet.Cells[i + 2, 21] = InoutGenCmsbillXlstempList[i].Importstate;
        //        xlWorkSheet.Cells[i + 2, 22] = InoutGenCmsbillXlstempList[i].Importmsg;

        //    }
        //    try
        //    {
        //        xlWorkBook.SaveAs(fileNameString, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
        //        xlWorkBook.Close(true, misValue, misValue);
        //        xlApp.Quit();
        //        MessageBox.Show("导出成功！");
        //    } 
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString().Trim());
        //    }

        //}

        //private PropertyInfo[] GetPropertyInfoArray()
        //{
        //    PropertyInfo[] props = null;
        //    try
        //    {
        //        Type type = typeof(PriceManager.InoutGenCmsbillXlstemp);
        //        object obj = Activator.CreateInstance(type);
        //        props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString().Trim(), "错误信息");
        //    }
        //    return props;
        //}
        #endregion
        #region 双击单元格触发事件
        private void dgvGenCmsbill_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex!=15&&e.ColumnIndex!=17&&e.ColumnIndex!=25&&e.ColumnIndex!=26&&e.ColumnIndex != 19) {
                dgvGenCmsbill.Columns[e.ColumnIndex].ReadOnly = true;
                return;
            }
            //计划下单量
            if (e.ColumnIndex==15&&e.RowIndex!=-1) {
                dgvGenCmsbill.Columns[e.ColumnIndex].ReadOnly = false;
                DataGridViewCell cell = dgvGenCmsbill.Rows[e.RowIndex].Cells["Column38"];
                dgvGenCmsbill.CurrentCell = cell;
                dgvGenCmsbill.BeginEdit(true);
                dgvGenCmsbill.CellValueChanged += new DataGridViewCellEventHandler(dgvGenCmsbill_CellValueChanged);
              
            }
            //cms导入价
            if (e.ColumnIndex==17&&e.RowIndex!=-1) {
                dgvGenCmsbill.Columns[e.ColumnIndex].ReadOnly = false;
                DataGridViewCell cell = dgvGenCmsbill.Rows[e.RowIndex].Cells["Column28"];
                dgvGenCmsbill.CurrentCell = cell;
                dgvGenCmsbill.BeginEdit(true);
                dgvGenCmsbill.CellValueChanged += new DataGridViewCellEventHandler(dgvGenCmsbill_CellValueChanged);
               
            }
            //批卡备注
           if (e.ColumnIndex==25&&e.RowIndex!=-1) {
                dgvGenCmsbill.Columns[e.ColumnIndex].ReadOnly = false;
                DataGridViewCell cell = dgvGenCmsbill.Rows[e.RowIndex].Cells["Column37"];
                dgvGenCmsbill.CurrentCell = cell;
                dgvGenCmsbill.BeginEdit(true);
                dgvGenCmsbill.CellValueChanged += new DataGridViewCellEventHandler(dgvGenCmsbill_CellValueChanged);
               
            }
            //审批备注
           if(e.ColumnIndex==26&&e.RowIndex!=-1) {
                dgvGenCmsbill.Columns[e.ColumnIndex].ReadOnly = false;
                DataGridViewCell cell = dgvGenCmsbill.Rows[e.RowIndex].Cells["Column2"];
                dgvGenCmsbill.CurrentCell = cell;
                dgvGenCmsbill.BeginEdit(true);
                dgvGenCmsbill.CellValueChanged += new DataGridViewCellEventHandler(dgvGenCmsbill_CellValueChanged);
                
            }
           //点击库存 可查看库存详细信息 
            if (e.ColumnIndex==19&&e.RowIndex!=-1) {
                InoutGenCmsbillXlstemp info = dgvGenCmsbill.CurrentRow.DataBoundItem as InoutGenCmsbillXlstemp;
                StockForm stock = new StockForm();
                stock.inoutGenCmsbillXlstemp = info;
                stock.Text = "库存信息";
                stock.stateUI = 1;
                stock.ShowDialog();
                //监测子窗体关闭后 库存此单元格不能修改库存数据
                if (stock.DialogResult == DialogResult.OK)
                {
                    dgvGenCmsbill.Columns[e.ColumnIndex].ReadOnly = true;
                }
            }
        }
        #endregion
        void dgvGenCmsbill_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 15 && e.ColumnIndex != 17 && e.ColumnIndex != 25 && e.ColumnIndex != 26) return;

            dgvGenCmsbill.CurrentCell.Style.BackColor = Color.Purple;

            submitCmsBtn.Enabled = false;
        }
        #region  修改完成触发
        //编辑完成事件
        private void dgvGenCmsbill_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SPRetInfo ret = new SPRetInfo();
            InoutGenCmsbillXlstemp info = dgvGenCmsbill.CurrentRow.DataBoundItem as InoutGenCmsbillXlstemp;
            //cms导入价
            if (e.ColumnIndex == 17 && e.RowIndex != -1)
            {
                if (!string.IsNullOrEmpty(info.Importprc))
                {
                    dao.UpdateCmsprc(int.Parse(info.Batchid), int.Parse(info.ExcelSeqid), double.Parse(info.Importprc), ret);
                    if (ret.num != "1")
                    {
                        MessageBox.Show(ret.msg, "后台提示！");
                    }
                }
                return;
            }
            //计划下单量
            if (e.ColumnIndex == 15 && e.RowIndex != -1)
            {
                if (!string.IsNullOrEmpty(info.Plancount))
                {
                    dao.UpdatePlancount(int.Parse(info.Batchid), int.Parse(info.ExcelSeqid), double.Parse(info.Plancount), ret);
                    if (ret.num != "1")
                    {
                        MessageBox.Show(ret.msg, "后台提示！");
                    }
                }
                return;
            }
            //批卡备注
            if (e.ColumnIndex == 25 && e.RowIndex != -1)
            {
                if (!string.IsNullOrEmpty(info.Pmark))
                {
                    dao.UpdatePmark(int.Parse(info.Batchid), int.Parse(info.ExcelSeqid), info.Pmark, ret);
                    if (ret.num != "1")
                    {
                        MessageBox.Show(ret.msg, "后台提示！");
                    }
                }
                else {
                    string pmark = "-1";
                    dao.UpdatePmark(int.Parse(info.Batchid), int.Parse(info.ExcelSeqid), pmark, ret);
                    if (ret.num != "1")
                    {
                        MessageBox.Show(ret.msg, "后台提示！");
                    }
                }
                return;
            }
            //审批备注
            if (e.ColumnIndex == 26 && e.RowIndex != -1)
            {
                if (!string.IsNullOrEmpty(info.Amark))
                {
                    dao.UpdateAmark(int.Parse(info.Batchid), int.Parse(info.ExcelSeqid), info.Amark, ret);
                    if (ret.num != "1")
                    {
                        MessageBox.Show(ret.msg, "后台提示！");
                    }
                }
                else {
                    string amark = "-1";
                    dao.UpdateAmark(int.Parse(info.Batchid), int.Parse(info.ExcelSeqid), amark, ret);
                    if (ret.num != "1")
                    {
                        MessageBox.Show(ret.msg, "后台提示！");
                    }
                }
                return;
            }

        }

        #endregion
        //筛选按钮事件
        private void ScreenBtn_Click(object sender, EventArgs e)
        {
            //2018-12-29  新增一个判断checkbox
            string flag = "";
            if (ChBContranct.Checked == true)
            {
                flag = "yes";
            }
            else {
                flag = "no";
            }
            this.Cursor = Cursors.WaitCursor;
            clearUI();
            SPRetInfo ret = new SPRetInfo();
            string info = "";
            string checkInfo = cBCheckmsg.SelectedValue.ToString().Trim();
            string coldChain = cBColdChain.SelectedItem.ToString().Trim();
            foreach (string key in myDictionary.Keys)
            {
                if (myDictionary[key].Equals(checkInfo))
                {
                   info = key;
                }
            }
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            if (!StringUtils.IsNull(txtCmsAdress.Text))
            {
                sqlkeydict.Add("cmsaddresscode%", "%" +txtCmsAdress.Text.ToString()+ "%");
               
            }
            if (!info.Equals("00")) {
                sqlkeydict.Add("errorcode%", "%" + info + "%");
            }
            if (!StringUtils.IsNull(coldChain))
            {
                if (!coldChain.Equals("否"))
                {
                    sqlkeydict.Add("coldchain", "%" + coldChain + "%");
                    ScreenList = dao.ScreenInfo(sqlkeydict, Batchid,flag, ret);
                }
                else
                {
                    ScreenList = dao.ScreenInfos(sqlkeydict, Batchid,flag, ret);
                }
            }
            else {
                ScreenList = dao.ScreenInfo(sqlkeydict, Batchid,flag, ret);
            }
            if (ScreenList.Count <= 0)
            {
                MessageBox.Show("未查询到数据！", "程序提示");
                dgvGenCmsbill.DataSource = null;
                this.Cursor = Cursors.Default;
                return;
            }
           dgvGenCmsbill.DataSource = ScreenList;
            //选中框逻辑
            for (int i = 0; i < ScreenList.Count; i++)
            {
                if (ScreenList[i].ChooseFlag == "1")
                {
                    ((DataGridViewCheckBoxCell)dgvGenCmsbill.Rows[i].Cells[0]).Value = true;
                }
                else
                {
                    ((DataGridViewCheckBoxCell)dgvGenCmsbill.Rows[i].Cells[0]).Value = false;
                }
            }

            //颜色判定
            Color c1 = Color.FromArgb(250, 191, 143);
            for (int i = 0; i < dgvGenCmsbill.Rows.Count; i++)
            {
                if (dgvGenCmsbill.Rows[i].Cells["Column17"].Value.ToString() != dgvGenCmsbill.Rows[i].Cells["Column27"].Value.ToString())
                {
                    dgvGenCmsbill.Rows[i].Cells["Column17"].Style.BackColor = c1;
                }
            }

            dgvGenCmsbill.Refresh();
           this.Cursor = Cursors.Default;//加载完成，改变鼠标样式
        }
        //对码刷新
        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            clearUI();
            SPRetInfo ret = new SPRetInfo();
            InoutGenCmsbillXlstemp infos = new InoutGenCmsbillXlstemp();
            InoutGenCmsbillXlstemp OutInfos = new InoutGenCmsbillXlstemp();
            //1.查询未对码数据
            NotGoodidlist =dao.getNotGoodidData(Batchid);
            if (NotGoodidlist.Count<=0) {
                MessageBox.Show("没有需进行对码刷新的数据！", "程序提示");
                this.Cursor = Cursors.Default;
                return;
            }
            else {
                //2.循环调存储过程  
                foreach (InoutGenCmsbillXlstemp info in NotGoodidlist) {
                    dao.GetGoodids(info,ret);
                    if (ret.num == "1") {
                        //3.再次查询 通过batchid和excelid确定有goodid的单行数据
                        infos=dao.SelData(info);
                        if (!StringUtils.IsNull(infos.Goodid))
                        {
                            OutInfos = infos;
                            //4.去mysql查价格
                            dao.PGetB2BPrcs(infos, OutInfos);
                            if (!string.IsNullOrEmpty(OutInfos.Prcresultcode))//!=
                            {
                                if (string.IsNullOrEmpty(OutInfos.Prc))
                                {
                                    OutInfos.Prc = "-1";
                                    OutInfos.Price = "-1";
                                    OutInfos.Bottomprc = "-1";
                                    OutInfos.Bottomprice = "-1";
                                    OutInfos.Costprc = "-1";
                                    OutInfos.Costprice = "-1";
                                }
                            }
                            //5.插进中间表
                            dao.PSetBillPrcs(OutInfos, ret);
                            if (ret.num == "-1")
                            {
                                MessageBox.Show(ret.msg + "|" + ret.num, "后台提示！");
                            }

                        }
                    }
                    else {                      
                        MessageBox.Show(ret.msg + "|" + ret.num, "后台提示！");
                        this.Cursor = Cursors.Default;
                        return;
                    }
                   
                }
                //6.再次查询中间表
                InoutGenCmsbillXlstempList = dao.GetInoutGenCmsbillXlstempList(Batchid);
                dgvGenCmsbill.DataSource = InoutGenCmsbillXlstempList;
                //选中框逻辑
                for (int i = 0; i < InoutGenCmsbillXlstempList.Count; i++)
                {
                    if (InoutGenCmsbillXlstempList[i].ChooseFlag == "1")
                    {
                        ((DataGridViewCheckBoxCell)dgvGenCmsbill.Rows[i].Cells[0]).Value = true;
                    }
                    else
                    {
                        ((DataGridViewCheckBoxCell)dgvGenCmsbill.Rows[i].Cells[0]).Value = false;
                    }
                }
                //颜色判定
                Color c1 = Color.FromArgb(250, 191, 143);
                for (int i = 0; i < dgvGenCmsbill.Rows.Count; i++)
                {
                    if (dgvGenCmsbill.Rows[i].Cells["Column17"].Value.ToString() != dgvGenCmsbill.Rows[i].Cells["Column27"].Value.ToString())
                    {
                        dgvGenCmsbill.Rows[i].Cells["Column17"].Style.BackColor = c1;
                    }
                }


                dgvGenCmsbill.Refresh();
                MessageBox.Show("对码刷新成功！", "程序提示");
                this.Cursor = Cursors.Default;//加载完成，改变鼠标样式
            }

        }

        #region 单击单选框触发事件  选中单选框
        private void dgvGenCmsbill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                dgvGenCmsbill.Columns[e.ColumnIndex].ReadOnly = true;
                return;
            }
            //选中单选框 触发事件
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                //监听行
                rows = e.RowIndex;
                //获取控件的值
                //MessageBox.Show(this.dgvGenCmsbill.Rows[e.RowIndex].Cells[0].EditedFormattedValue.ToString());
                string chooseFlag = this.dgvGenCmsbill.Rows[e.RowIndex].Cells[0].EditedFormattedValue.ToString().Trim();
                if (chooseFlag.Equals("True"))
                {
                    //选中
                    InoutGenCmsbillXlstemp info = dgvGenCmsbill.CurrentRow.DataBoundItem as InoutGenCmsbillXlstemp;
                    dao.UpdateChooseFlag(info);

                    ////暂缓开票
                    //if (info.InvPostFlag == "否")
                    //{
                    //    this.cBInvPostFlag.SelectedIndex = 1;
                    //}
                    //else {
                    //    this.cBInvPostFlag.SelectedIndex = 0;
                    //}

                    SalePromoInfo ins = new SalePromoInfo();
                    string compid = info.Compid;
                    string ownerid = info.Ownerid;
                    string cstid = info.Cstid;
                    string goodid = info.Goodid;
                    ins = dao.GetPromosInfos(compid, ownerid, cstid, goodid);
                    txtBeginTime.Text = ins.BeginTime;
                    txtEndTime.Text = ins.EndTime;
                    txtPolicy.Text = ins.Policy;
                    txtRemark.Text = ins.Remark;

                }
                else {
                    //取消选中
                    InoutGenCmsbillXlstemp info = dgvGenCmsbill.CurrentRow.DataBoundItem as InoutGenCmsbillXlstemp;
                    dao.UpdateChooseFlagFalse(info);

                    txtBeginTime.Text = "";
                    txtEndTime.Text = "";
                    txtPolicy.Text = "";
                    txtRemark.Text = "";
                }

            }


        }
        #endregion
        /// <summary>
        /// /全选触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllSelBtn_Click(object sender, EventArgs e)
        {
            SortableBindingList<InoutGenCmsbillXlstemp> dataList = new SortableBindingList<InoutGenCmsbillXlstemp>();
            InoutGenCmsbillXlstemp ix = new InoutGenCmsbillXlstemp();
            dataList.Clear();
            //改变chooseflag的值
            for (int i = 0; i < dgvGenCmsbill.RowCount; i++)
            {
                ((DataGridViewCheckBoxCell)dgvGenCmsbill.Rows[i].Cells[0]).Value = true;
            }
                //获取界面上的数据源
                foreach (DataGridViewRow dgvr in dgvGenCmsbill.Rows)
            {
                dataList.Add((InoutGenCmsbillXlstemp)dgvr.DataBoundItem);

            }

            //改变数据库中的值
            foreach (InoutGenCmsbillXlstemp info in dataList) {
                dao.UpdateChooseFlag(info);

            }        
        }
        /// <summary>
        /// /取消全选触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            SortableBindingList<InoutGenCmsbillXlstemp> dataList = new SortableBindingList<InoutGenCmsbillXlstemp>();
            InoutGenCmsbillXlstemp ix = new InoutGenCmsbillXlstemp();
            dataList.Clear();
            //改变chooseflag的值
            for (int i = 0; i < dgvGenCmsbill.RowCount; i++)
            {
                ((DataGridViewCheckBoxCell)dgvGenCmsbill.Rows[i].Cells[0]).Value = false;
            }
            //获取界面上的数据源
            foreach (DataGridViewRow dgvr in dgvGenCmsbill.Rows ) {
                dataList.Add((InoutGenCmsbillXlstemp)dgvr.DataBoundItem);
            }

            foreach (InoutGenCmsbillXlstemp info in dataList)
            {
                dao.UpdateChooseFlagFalse(info);
            }

        }
        //部分提交CMS
        private void PartSubmitBtn_Click(object sender, EventArgs e)
        {
            SortableBindingList<InoutGenCmsbillXlstemp> dataList = new SortableBindingList<InoutGenCmsbillXlstemp>();
            dataList.Clear();
            //判断勾选数据
            for (int i = 0; i < dgvGenCmsbill.RowCount; i++)
            {
                string chooseFlag = this.dgvGenCmsbill.Rows[i].Cells[0].EditedFormattedValue.ToString().Trim();
                if (chooseFlag.Equals("True"))
                {
                    InoutGenCmsbillXlstemp info = dgvGenCmsbill.CurrentRow.DataBoundItem as InoutGenCmsbillXlstemp;
                    dataList.Add(info);
                }
            }
            //部分提交cms
            SPRetInfo ret = new SPRetInfo();
            ImportedTempList = dao.ImportGenCmsBillDatas(Batchid, ret);
            dgvGenCmsbill.DataSource = ImportedTempList;
            dgvGenCmsbill.Refresh();
            exportBtn.Enabled = true;
            ScreenBtn_Click(sender, e);


        }

        private void dgvGenCmsbill_MouseClick(object sender, MouseEventArgs e)
        {
            //InoutGenCmsbillXlstemp info = dgvGenCmsbill.CurrentRow.DataBoundItem as InoutGenCmsbillXlstemp;
            //SalePromoInfo ins = new SalePromoInfo();
            //string compid = info.Compid;
            //string ownerid = info.Ownerid;
            //string cstid = info.Cstid;
            //string goodid = info.Goodid;
            //ins = dao.GetPromosInfos(compid, ownerid, cstid, goodid);
            //txtBeginTime.Text = ins.BeginTime;
            //txtEndTime.Text = ins.EndTime;
            //txtPolicy.Text = ins.Policy;
            //txtRemark.Text = ins.Remark;
        }

        private void dgvGenCmsbill_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if ("1" == this.dgvGenCmsbill.Rows[e.RowIndex].Cells["Column42"].Value.ToString())
            {
                this.dgvGenCmsbill.Rows[e.RowIndex].Cells["Column35"].Style.BackColor = Color.Red;
            }
        }
        //合同处理
        private void BtnContractHandle_Click(object sender, EventArgs e)
        {     
            SortableBindingList<InoutGenCmsbillXlstemp> dataList = new SortableBindingList<InoutGenCmsbillXlstemp>();
            dataList.Clear();
            //获取勾选数据
            dataList=dao.GetSelectData(Batchid);
            if (dataList.Count<=0) {
                MessageBox.Show("请勾选进行合同处理的数据！","前台提示");
                return;
            }
            SPRetInfo retinfo = new SPRetInfo();
            //-----2019-2-14---校验当次勾选是否存在重复明细
            dao.CheckFos(Batchid,retinfo);
            if (retinfo.num == "1")
            {
                MessageBox.Show(retinfo.msg, "后台提示");
            }
            else {

                ContractForm contranctForm = new ContractForm();
                contranctForm.dataList = dataList;
                contranctForm.batchid = Batchid;
                contranctForm.cstid = Cstid;
                contranctForm.Tag = dao;
                contranctForm.ShowDialog();
                if (contranctForm.DialogResult == DialogResult.OK)
                {
                    ChBContranct_CheckedChanged(sender, e);
                    //取消所有勾选
                    dao.Cancel(Batchid);
                    //改变chooseflag的值
                    for (int i = 0; i < dgvGenCmsbill.RowCount; i++)
                    {
                        ((DataGridViewCheckBoxCell)dgvGenCmsbill.Rows[i].Cells[0]).Value = false;
                    }

                }

            }

        }

        private void cBInvPostFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("触发！！！");
            SPRetInfo retinfo = new SPRetInfo();
            string value = cBInvPostFlag.SelectedItem.ToString().Trim();
            string code = "";
            if (value == "是")
            {
                code = "10";
            }
            else {
                code = "00";
            }
            if (rows != -1) {
                //修改暂缓开票
                string batchid = this.dgvGenCmsbill.Rows[rows].Cells["Column6"].Value.ToString().Trim();
                string excelsqlid = this.dgvGenCmsbill.Rows[rows].Cells["Column7"].Value.ToString().Trim();
                dao.UpdateInvPostFlag(batchid,excelsqlid,code,retinfo);
                if (retinfo.num=="-1") {
                    MessageBox.Show(retinfo.msg + "|" + retinfo.num, "后台提示！");
                }
                else {
                    this.dgvGenCmsbill.Rows[rows].Cells["Column46"].Value = value;
                }
            }
        }
        //判断是否勾选 可处理合同项
        private void ChBContranct_CheckedChanged(object sender, EventArgs e)
        {
            if (ChBContranct.Checked == true)
            {
                ContractConlist=dao.GetDataSource(Batchid);
                if (ContractConlist.Count<=0) {
                    MessageBox.Show("请先进行校验！","后台提示");
                    dgvGenCmsbill.DataSource = ContractConlist;
                    dgvGenCmsbill.Refresh();
                }
                else {
                    dgvGenCmsbill.DataSource = ContractConlist;
                    //选中框逻辑
                    for (int i = 0; i < ContractConlist.Count; i++)
                    {
                        if (ContractConlist[i].ChooseFlag == "1")
                        {
                            ((DataGridViewCheckBoxCell)dgvGenCmsbill.Rows[i].Cells[0]).Value = true;
                        }
                        else
                        {
                            ((DataGridViewCheckBoxCell)dgvGenCmsbill.Rows[i].Cells[0]).Value = false;
                        }
                    }
                    dgvGenCmsbill.Refresh();
                    BtnContractHandle.Enabled = true;
                    AllSelBtn_Click(sender, e);
                }
            }
            else
            {
                BtnContractHandle.Enabled = false;
                ScreenBtn_Click(sender, e);
                cancelBtn_Click(sender, e);
            }            
        }

    }
}

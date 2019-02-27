using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Reflection;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Web;
using System.Linq;

namespace PriceManager
{
    public static class ExcelHelper
    {
        #region 导出CSV
        /// <summary>
        /// Export the data from datatable to CSV file
        /// </summary>
        /// <param name="grid"></param>
        public static void ExportDataGridToCSV(DataTable dt,string path)
        {
            string strFile = "";

            //File info initialization
            strFile = "test";
            strFile = strFile + DateTime.Now.ToString("yyyyMMddhhmmss");
            strFile = strFile + ".csv";

            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, new System.Text.UnicodeEncoding());
            //Tabel header
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sw.Write(dt.Columns[i].ColumnName);
                sw.Write("\t");
            }
            sw.WriteLine("");
            //Table body
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    sw.Write(DelQuota(dt.Rows[i][j].ToString()));
                    sw.Write("\t");
                }
                sw.WriteLine("");
            }
            sw.Flush();
            sw.Close();
        }

        /// <summary>
        /// Delete special symbol
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DelQuota(string str)
        {
            string result = str;
            string[] strQuota = { "\r\n" };
            for (int i = 0; i < strQuota.Length; i++)
            {
                if (result.IndexOf(strQuota[i]) > -1)
                    result = result.Replace(strQuota[i], "");
            }
            if (result.StartsWith("0"))
                return string.Format("\'{0}",result);
            else
                return result;
        }
        #endregion

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="dataTable">数据表格</param>
        /// <param name="absolutePath">导出Excel绝对路径，例如（C:\Users\wuzk\Desktop\Temporary\1.xlsx）</param>
        /// <param name="isAddStyle">是否添加固定单元格样式</param>
        /// <param name="styleDic">单元格样式字典：
        /// Alignment（水平对齐）；
        /// Background（单元格背景颜色）；
        /// Border：单元格边框粗细；
        /// FontName：字体名称；
        /// FontSize：字体大小；
        /// FontWeight：字体是否加粗；
        /// FontColor：字体颜色；
        /// </param>
        /// <param name="errMsg">异常消息</param>
        /// <returns>成功或失败</returns>
        public static bool ExportDataToExcel(DataTable dataTable, string absolutePath, bool isAddStyle, out string errMsg)
        {
            errMsg = string.Empty;
            List<DataTable> dts = new List<DataTable>
            {
                dataTable
            };
            return ExportDataToExcel(dts, absolutePath, isAddStyle, out errMsg);
        }
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="dataTables">数据表格</param>
        /// <param name="absolutePath">导出Excel绝对路径，例如（C:\Users\wuzk\Desktop\Temporary\1.xlsx）</param>
        /// <param name="isAddStyle">是否添加固定单元格样式</param>
        /// <param name="errMsg">异常消息</param>
        /// <returns>成功或失败</returns>
        public static bool ExportDataToExcel(IEnumerable<DataTable> dataTables, string absolutePath, bool isAddStyle, out string errMsg)
        {
            bool result = true;
            errMsg = "导出Excel成功";
            try
            {
                IWorkbook workbook = null;
                string fileEx = Path.GetExtension(absolutePath);
                switch (fileEx)
                {
                    case ".xls":
                        workbook = new HSSFWorkbook();
                        break;
                    default:
                        workbook = new XSSFWorkbook();
                        break;
                }
                int i = 0;
                foreach (DataTable dt in dataTables)
                {
                    //创建工作簿名称
                    string sheetName = string.IsNullOrEmpty(dt.TableName)
                        ? "Sheet " + (++i).ToString()
                        : dt.TableName;
                    ISheet sheet = workbook.CreateSheet(sheetName);
                    //所有单元格列宽度
                    sheet.DefaultColumnWidth = 15;
                    sheet.DefaultRowHeight = 11;
                    //创建列
                    IRow headerRow = sheet.CreateRow(0);
                    //第一列的高度
                    headerRow.Height = 20 * 15;
                    //赋值头部单元格样式
                    ICellStyle headerCellStyle = isAddStyle == true ? SetHeaderCellStyle(workbook) : workbook.CreateCellStyle();
                    //自动转行
                    headerCellStyle.WrapText = true;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string columnName = string.IsNullOrEmpty(dt.Columns[j].ColumnName)
                            ? "Column " + j.ToString()
                            : dt.Columns[j].ColumnName;
                        //设置列名称
                        headerRow.CreateCell(j).SetCellValue(columnName);
                        //将定义样式赋给单元格
                        headerRow.GetCell(j).CellStyle = headerCellStyle;
                    }

                    //赋值头部单元格样式
                    ICellStyle otherCellStyle = isAddStyle == true ? SetOtherCellStyle(workbook) : workbook.CreateCellStyle();
                    //自动转行
                    otherCellStyle.WrapText = true;
                    //创建行单元格并赋值
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        //第N行
                        DataRow dr = dt.Rows[k];
                        IRow row = sheet.CreateRow(k + 1);
                        //第N行第N单元格
                        for (int r = 0; r < dt.Columns.Count; r++)
                        {
                            row.CreateCell(r).SetCellValue(dr[r] != DBNull.Value ? dr[r].ToString() : string.Empty);
                            row.GetCell(r).CellStyle = otherCellStyle;
                        }
                    }
                    
                }
                using (FileStream fs = File.Create(absolutePath))
                {
                    workbook.Write(fs);
                }
            }
            catch (Exception ex)
            {
                result = false;
                errMsg = ex.Message;
            }
            
            return result;
        }
        /// <summary>
        /// 获取Excel数据
        /// </summary>
        /// <param name="absolutePath">Excel绝对路径（C:\Users\wuzk\Desktop\Temporary\1.xlsx）</param>
        /// <param name="tableName">读取表名</param>
        /// <param name="errMsg">异常消息</param>
        /// <returns>数据表格</returns>
        public static DataSet GetDataSetByExcel(string absolutePath, string tableName, out string errMsg)
        {
            errMsg = "获取Excel数据成功";
            DataSet ds = new DataSet();
            try
            {
                if (!File.Exists(absolutePath))
                    throw new FileNotFoundException("文件不存在");
                Stream stream = new FileStream(absolutePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); //new MemoryStream(File.ReadAllBytes(absolutePath));--对内存进行读取,
                IWorkbook workbook = WorkbookFactory.Create(stream);
                DataTable dt = new DataTable
                {
                    TableName = tableName
                };
                ISheet sheet = workbook.GetSheet(tableName);
                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;
                for (int j = headerRow.FirstCellNum; j < cellCount; j++)
                {
                    DataColumn column = new DataColumn(headerRow.GetCell(j).StringCellValue);
                    dt.Columns.Add(column);
                }
                int rowCount = sheet.LastRowNum;
                for (int a = (sheet.FirstRowNum + 1); a < rowCount + 1; a++)
                {
                    IRow row = sheet.GetRow(a);
                    if (row == null) continue;

                    DataRow dr = dt.NewRow();
                    for (int b = row.FirstCellNum; b < cellCount; b++)
                    {
                        if (row.GetCell(b) == null) continue;
                        if ((row.GetCell(b).CellType == CellType.Formula|| row.GetCell(b).CellType == CellType.Numeric) 
                            && DateUtil.IsCellDateFormatted(row.GetCell(b)))
                        {
                            dr[b] = row.GetCell(b).DateCellValue.ToString("yyyy/MM/dd");
                            continue;
                        }
                        dr[b] = row.GetCell(b).ToString();
                    }

                    dt.Rows.Add(dr);
                }
                stream.Close();
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return ds;
        }
        /// <summary>
        /// 设置头部样式
        /// </summary>
        /// <param name="workbook">工作簿对象</param>
        /// <returns></returns>
        private static ICellStyle SetHeaderCellStyle(IWorkbook workbook)
        {
            //创建单元格样式对象
            ICellStyle style = workbook.CreateCellStyle();
            //垂直对齐居中
            style.VerticalAlignment = VerticalAlignment.Center;
            //水平对齐居中
            style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            //设置字体前背景颜色
            style.FillForegroundColor = HSSFColor.LightBlue.Index;
            style.FillPattern = FillPattern.SolidForeground;
            //设置单元格边框大小和边框前背景颜色
            style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BottomBorderColor = HSSFColor.Black.Index;
            style.LeftBorderColor = HSSFColor.Black.Index;
            style.RightBorderColor = HSSFColor.Black.Index;
            style.TopBorderColor = HSSFColor.Black.Index;
            //创建一个字体样式对象
            IFont font = workbook.CreateFont();
            //设置字体
            font.FontName = "宋体";
            //设置字体尺寸（字号）
            font.FontHeightInPoints = 10;
            //设置字体加粗样式
            font.Boldweight = (short)FontBoldWeight.Bold;
            //设置字体颜色
            font.Color = HSSFColor.White.Index;
            //设置字体样式
            style.SetFont(font);
            return style;
        }
        /// <summary>
        /// 设置其他单元格（除头部以下）样式
        /// </summary>
        /// <param name="workbook">工作簿对象</param>
        /// <returns></returns>
        private static ICellStyle SetOtherCellStyle(IWorkbook workbook)
        {
            //创建其他单元格样式对象
            ICellStyle otherCellStyle = workbook.CreateCellStyle();
            //设置单元格边框大小和边框前背景颜色
            otherCellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            otherCellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            otherCellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            otherCellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            otherCellStyle.BottomBorderColor = HSSFColor.Black.Index;
            otherCellStyle.LeftBorderColor = HSSFColor.Black.Index;
            otherCellStyle.RightBorderColor = HSSFColor.Black.Index;
            otherCellStyle.TopBorderColor = HSSFColor.Black.Index;
            //创建一个字体样式对象
            IFont otherCellFont = workbook.CreateFont();
            //设置字体
            otherCellFont.FontName = "宋体";
            //设置字体尺寸（字号）
            otherCellFont.FontHeightInPoints = 9;
            //设置字体样式
            otherCellStyle.SetFont(otherCellFont);
            return otherCellStyle;
        }
        /// <summary>
        /// 组合Excel数据与数据库数据
        /// </summary>
        /// <param name="outputDS">要合并的与合并后输出的数据集</param>
        /// <param name="byDS">要合并的数据集</param>
        /// <returns>数据集</returns>
        public static DataSet CombinedDataByDataSet(DataSet outputDS, DataSet byDS)
        {
            if (null != byDS && byDS.Tables.Count > 0)
            {
                DataRowCollection byDr = byDS.Tables[0].Rows;
                int cLen = byDS.Tables[0].Columns.Count;
                foreach (DataRow dr in byDr)
                {
                    DataRow outputDr = outputDS.Tables[0].NewRow();
                    for (int i = 0; i < cLen; i++)
                    {
                        outputDr[i] = dr[i];
                    }
                    outputDS.Tables[0].Rows.Add(outputDr);
                }
            }
            return outputDS;
        }
        /// <summary>
        /// list转变为DataSet
        /// </summary>
        /// <typeparam name="T">T型结构</typeparam>
        /// <param name="data">List数据</param>
        /// <returns>DataSet</returns>
        public static DataSet ToDataSet<T>(IList<T> data)
        {
            DataSet result = new DataSet();
            DataTable dataTable = new DataTable();
            if (data.Count > 0)
            {
                PropertyInfo[] propertys = data[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    dataTable.Columns.Add(pi.Name, pi.PropertyType);
                }
                for (int i = 0; i < data.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(data[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dataTable.LoadDataRow(array, true);
                }
            }
            result.Tables.Add(dataTable);
            return result;
        }
        /// <summary>
        /// list转变为DataTable
        /// </summary>
        /// <typeparam name="T">T型结构</typeparam>
        /// <param name="data">List数据</param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dt = new DataTable();
            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dt.Columns.Add(property.Name, property.PropertyType);
            }
            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }
                dt.Rows.Add(values);
            }
            return dt;
        }


        // NPOI
        static public SortableBindingList<BillQuotationTemp> OpenInquiryExcelNPOI(string excelFile)
        {
            IWorkbook wk = null;
            try
            {
                Stream stream = new FileStream(excelFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                wk = WorkbookFactory.Create(stream);
                
                {
                    ISheet sheet = wk.GetSheetAt(0);
                    

                    SortableBindingList<BillQuotationTemp> orderList = new SortableBindingList<BillQuotationTemp>();
                    string OrderCode = null;
                    IRow row = sheet.GetRow(0);  //读取当前行数据
                    //LastRowNum 是当前表的总行数-1（注意）


                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        BillQuotationTemp order = new BillQuotationTemp();
                        //序号	客户商品代码	品名规格	厂家	条形码	国药准字   客户期望价

                        row = sheet.GetRow(i);  //读取当前行数据
                        if (row == null) continue;




                        Object value = row.GetCell(0);
                        if (i == 1)
                        {
                            if (value != null)
                            {
                                OrderCode = value.ToString();
                                order.ExcelSeqid = OrderCode;
                            }
                            else
                            {
                                MessageBox.Show("第[1]行序号有错误!请检查文件准确性!");
                                orderList.Clear();
                                return orderList;
                            }
                        }
                        else
                        {
                            if (value == null) continue;
                            else if (OrderCode.CompareTo(value.ToString()) == 0)
                            {
                                MessageBox.Show(String.Format("第[{0:d}]行序号有错误!\n请检查文件准确性!", i));
                                orderList.Clear();
                                return orderList;
                            }
                            else
                            {
                                value = row.GetCell(0);
                                OrderCode = value.ToString();
                                order.ExcelSeqid = OrderCode;
                            }
                        }

                        value = row.GetCell(1); //MyValues.GetValue(1, 2);
                        if (value != null)
                            order.Gengoods = value.ToString();
                        else
                            //order.ExtSkuCode = "99"; --滤掉无代码的行
                            continue;

                        value = row.GetCell(2); //MyValues.GetValue(1, 3);
                        if (value != null)
                            order.Genspec = value.ToString();
                        else
                            order.Genspec = "";
                        value = row.GetCell(3); //MyValues.GetValue(1, 4);
                        if (value != null)
                            order.Genproducer = value.ToString();
                        else
                            order.Genproducer = "";

                        value = row.GetCell(4); // MyValues.GetValue(1, 5);
                        if (value != null)
                            order.Goodbar = value.ToString();
                        else
                            order.Goodbar = "";

                        value = row.GetCell(5); //MyValues.GetValue(1, 6);
                        if (value != null)
                            order.Ratifier = value.ToString();
                        else
                            order.Ratifier = "";
                        value = row.GetCell(6); //MyValues.GetValue(1, 6);
                        if (value != null)
                            order.Hopeprc = value.ToString();
                        else
                            order.Hopeprc = "";

                        //order.ExtSkuUnit = "盒";
                        orderList.Add(order);

                    }

                    return orderList;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("数据绑定Excel失败!失败原因：" + err.Message, "提示信息",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            
        }

        #region DataGridView数据显示到Excel
        /// <summary>      
        /// 打开Excel并将DataGridView控件中数据导出到Excel     
        /// </summary>      
        /// <param name="dgv">DataGridView对象 </param>      
        /// <param name="isShowExcle">是否显示Excel界面 </param>      
        /// <remarks>     
        /// add com "Microsoft Excel 11.0 Object Library"     
        /// using Excel=Microsoft.Office.Interop.Excel;     
        /// </remarks>     
        /// <returns> </returns>      
        static public bool DataGridviewShowToExcel(DataGridView dgv, bool isShowExcle)
        {
            if (dgv.Rows.Count == 0)
                return false;
            //建立Excel对象      
            Excel.Application excel = new Excel.Application();
            excel.Application.Workbooks.Add(true);
            excel.Visible = isShowExcle;
            //生成字段名称      
            for (int i = 0; i < dgv.ColumnCount; i++)
            {
                excel.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
            }
            //填充数据      
            for (int i = 0; i < dgv.RowCount - 1; i++)
            {
                for (int j = 0; j < dgv.ColumnCount; j++)
                {
                    if (dgv[j, i].ValueType == typeof(string))
                    {
                        excel.Cells[i + 2, j + 1] = "'" + dgv[j, i].Value.ToString();
                    }
                    else
                    {
                        excel.Cells[i + 2, j + 1] = dgv[j, i].Value.ToString();
                    }
                }
            }
            return true;
        }
        #endregion     

        #region DataGridView导出到Excel，有一定的判断性
        /// <summary>    
        ///方法，导出DataGridView中的数据到Excel文件    
        /// </summary>    
        /// <remarks>   
        /// add com "Microsoft Excel 11.0 Object Library"   
        /// using Excel=Microsoft.Office.Interop.Excel;   
        /// using System.Reflection;   
        /// </remarks>   
        /// <param name= "dgv"> DataGridView </param>    
        public static void DataGridViewToExcel(DataGridView dgv)
        {


            #region   验证可操作性

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
            //定义表格内数据的行数和列数    

            int rowscount = dgv.Rows.Count;
            int colscount = dgv.Columns.Count;
            //行数必须大于0    
            if (rowscount <= 0)
            {
                MessageBox.Show("没有数据可供保存 ", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //列数必须大于0    
            if (colscount <= 0)
            {
                MessageBox.Show("没有数据可供保存 ", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //行数不可以大于65536    
            if (rowscount > 65536)
            {
                MessageBox.Show("数据记录数太多(最多不能超过65536条)，不能保存 ", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //列数不可以大于255    
            if (colscount > 255)
            {
                MessageBox.Show("数据记录行数太多，不能保存 ", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //验证以fileNameString命名的文件是否存在，如果存在删除它    
            FileInfo file = new FileInfo(fileNameString);
            if (file.Exists)
            {
                try
                {
                    file.Delete();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "删除失败 ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            #endregion
            Excel.Application objExcel = null;
            Excel.Workbook objWorkbook = null;
            Excel.Worksheet objsheet = null;
            try
            {
                //申明对象    
                objExcel = new Microsoft.Office.Interop.Excel.Application();
                objWorkbook = objExcel.Workbooks.Add(Missing.Value);
                objsheet = (Excel.Worksheet)objWorkbook.ActiveSheet;
                //设置EXCEL不可见    
                objExcel.Visible = false;

                //向Excel中写入表格的表头    
                int displayColumnsCount = 1;
                for (int i = 0; i <= dgv.ColumnCount - 1; i++)
                {
                    if (dgv.Columns[i].Visible == true)
                    {
                        objExcel.Cells[1, displayColumnsCount] = dgv.Columns[i].HeaderText.Trim();
                        displayColumnsCount++;
                    }
                }
                //设置进度条    
                //tempProgressBar.Refresh();
                //tempProgressBar.Visible = true;
                //tempProgressBar.Minimum = 1;
                //tempProgressBar.Maximum = dgv.RowCount;
                //tempProgressBar.Step = 1;    
                //向Excel中逐行逐列写入表格中的数据    
                for (int row = 0; row <= dgv.RowCount - 1; row++)
                {
                    //tempProgressBar.PerformStep();    

                    displayColumnsCount = 1;
                    for (int col = 0; col < colscount; col++)
                    {
                        if (dgv.Columns[col].Visible == true)
                        {
                            try
                            {
                                objExcel.Cells[row + 2, displayColumnsCount] = dgv.Rows[row].Cells[col].Value.ToString().Trim();
                                displayColumnsCount++;
                            }
                            catch (Exception)
                            {

                            }

                        }
                    }
                }
                //隐藏进度条    
                //tempProgressBar.Visible   =   false;    
                //保存文件    
                objWorkbook.SaveAs(fileNameString, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                        Missing.Value, Excel.XlSaveAsAccessMode.xlShared, Missing.Value, Missing.Value, Missing.Value,
                        Missing.Value, Missing.Value);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "警告 ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            finally
            {
                //关闭Excel应用    
                if (objWorkbook != null) objWorkbook.Close(Missing.Value, Missing.Value, Missing.Value);
                if (objExcel.Workbooks != null) objExcel.Workbooks.Close();
                if (objExcel != null) objExcel.Quit();

                objsheet = null;
                objWorkbook = null;
                objExcel = null;
            }
            MessageBox.Show(fileNameString + "导出完毕! ", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        #endregion  
        //GenCstGood的excel导入导出  2018-7-25
        static public SortableBindingList<InoutGenCstGoodXlstemp> OpenImportExcelGCG(string excelFile)
        {

            IWorkbook wk = null;
            try
            {
                Stream stream = new FileStream(excelFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                wk = WorkbookFactory.Create(stream);

                {
                    ISheet sheet = wk.GetSheetAt(0);


                    SortableBindingList<InoutGenCstGoodXlstemp> inoutGenCstGoodXlstempList = new SortableBindingList<InoutGenCstGoodXlstemp>();
                    string inoutGenCstGoodXlstempCode = null;
                    IRow row = sheet.GetRow(0);  //读取当前行数据
                    //LastRowNum 是当前表的总行数-1（注意）

                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        InoutGenCstGoodXlstemp inoutGenCstGoodXlstemp = new InoutGenCstGoodXlstemp();
                        //序号	 客户商品代码	 cms商品代码   品名规格    转换比	 厂家    条形码	   国药准字     

                        row = sheet.GetRow(i);  //读取当前行数据
                        if (row == null) continue;

                        Object value = row.GetCell(0);
                        if (i == 1)
                        {
                            if (value != null)
                            {
                                inoutGenCstGoodXlstempCode = value.ToString();
                                inoutGenCstGoodXlstemp.ExcelSeqid = inoutGenCstGoodXlstempCode;
                            }
                            else
                            {
                                MessageBox.Show("第[1]行序号有错误!请检查文件准确性!");
                                inoutGenCstGoodXlstempList.Clear();
                                return inoutGenCstGoodXlstempList;
                            }
                        }
                        else
                        {
                            if (value == null) continue;
                            else if (inoutGenCstGoodXlstempCode.CompareTo(value.ToString()) == 0)
                            {
                                MessageBox.Show(String.Format("第[{0:d}]行序号有错误!\n请检查文件准确性!", i));
                                inoutGenCstGoodXlstempList.Clear();
                                return inoutGenCstGoodXlstempList;
                            }
                            else
                            {
                                value = row.GetCell(0);
                                inoutGenCstGoodXlstempCode = value.ToString();
                                inoutGenCstGoodXlstemp.ExcelSeqid = inoutGenCstGoodXlstempCode;
                            }
                        }

                        value = row.GetCell(1); //MyValues.GetValue(1, 2);
                        if (value != null)
                            inoutGenCstGoodXlstemp.Cstcode = value.ToString();
                        else
                            continue;

                        value = row.GetCell(2); //MyValues.GetValue(1, 2);
                        if (value != null)
                            inoutGenCstGoodXlstemp.Gengoods = value.ToString();
                        else
                            inoutGenCstGoodXlstemp.Gengoods = "";
                        value = row.GetCell(3); //MyValues.GetValue(1, 3);
                        if (value != null)
                            inoutGenCstGoodXlstemp.Goods = value.ToString();
                        else
                            inoutGenCstGoodXlstemp.Goods = "";
                        value = row.GetCell(4); //MyValues.GetValue(1, 4);
                        if (value != null)
                            inoutGenCstGoodXlstemp.Genspec = value.ToString();
                        else
                            inoutGenCstGoodXlstemp.Genspec = "";

                        value = row.GetCell(5); // MyValues.GetValue(1, 5);
                        if (value != null)
                            inoutGenCstGoodXlstemp.Transrate = value.ToString();
                        else
                            inoutGenCstGoodXlstemp.Transrate = "";

                        value = row.GetCell(6); //MyValues.GetValue(1, 6);
                        if (value != null)
                            inoutGenCstGoodXlstemp.Genproducer = value.ToString();
                        else
                            inoutGenCstGoodXlstemp.Genproducer = "";
                        value = row.GetCell(7); //MyValues.GetValue(1, 6);
                        if (value != null)
                            inoutGenCstGoodXlstemp.Goodbar = value.ToString();
                        else
                            inoutGenCstGoodXlstemp.Goodbar = "";
                        value = row.GetCell(8);//MyValues.GetValue(1, 7);
                        if (value != null)
                            inoutGenCstGoodXlstemp.Ratifier = value.ToString();
                        else
                            inoutGenCstGoodXlstemp.Ratifier = "";

                        inoutGenCstGoodXlstempList.Add(inoutGenCstGoodXlstemp);

                    }
                    return inoutGenCstGoodXlstempList;
                }

            }
            catch (Exception err)
            {
                MessageBox.Show("数据绑定Excel失败!失败原因：" + err.Message, "提示信息",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {


            }

        }
        //GenGoodRecord的excel导入导出  2018-8-1
        static public SortableBindingList<InoutGenGoodRecordXlstemp> OpenImportExcelGGR(string excelFile)
        {

            IWorkbook wk = null;
            try
            {
                Stream stream = new FileStream(excelFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                wk = WorkbookFactory.Create(stream);

                {
                    ISheet sheet = wk.GetSheetAt(0);


                    SortableBindingList<InoutGenGoodRecordXlstemp> inoutGenGoodRecordXlstempList = new SortableBindingList<InoutGenGoodRecordXlstemp>();
                    string inoutGenGoodRecordXlstempCode = null;
                    IRow row = sheet.GetRow(0);  //读取当前行数据
                    //LastRowNum 是当前表的总行数-1（注意）

                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        InoutGenGoodRecordXlstemp inoutGenGoodRecordXlstemp = new InoutGenGoodRecordXlstemp();
                        //序号	 客户代码  客户商品代码	  品名规格    转换比	 厂家    条形码	   国药准字    备案原因 

                        row = sheet.GetRow(i);  //读取当前行数据
                        if (row == null) continue;

                        Object value = row.GetCell(0);
                        if (i == 1)
                        {
                            if (value != null)
                            {
                                inoutGenGoodRecordXlstempCode = value.ToString();
                                inoutGenGoodRecordXlstemp.ExcelSeqid = inoutGenGoodRecordXlstempCode;
                            }
                            else
                            {
                                MessageBox.Show("第[1]行序号有错误!请检查文件准确性!");
                                inoutGenGoodRecordXlstempList.Clear();
                                return inoutGenGoodRecordXlstempList;
                            }
                        }
                        else
                        {
                            if (value == null) continue;
                            else if (inoutGenGoodRecordXlstempCode.CompareTo(value.ToString()) == 0)
                            {
                                MessageBox.Show(String.Format("第[{0:d}]行序号有错误!\n请检查文件准确性!", i));
                                inoutGenGoodRecordXlstempList.Clear();
                                return inoutGenGoodRecordXlstempList;
                            }
                            else
                            {
                                value = row.GetCell(0);
                                inoutGenGoodRecordXlstempCode = value.ToString();
                                inoutGenGoodRecordXlstemp.ExcelSeqid = inoutGenGoodRecordXlstempCode;
                            }
                        }

                        value = row.GetCell(1); //MyValues.GetValue(1, 1);
                        if (value != null)
                            inoutGenGoodRecordXlstemp.Cstcode = value.ToString();
                        else
                            continue;
                        value = row.GetCell(2); //MyValues.GetValue(1, 2);
                        if (value != null)
                            inoutGenGoodRecordXlstemp.Gengoods = value.ToString();
                        else
                            inoutGenGoodRecordXlstemp.Gengoods = "";
                        value = row.GetCell(3); //MyValues.GetValue(1, 3);
                        if (value != null)
                            inoutGenGoodRecordXlstemp.Genspec = value.ToString();
                        else
                            inoutGenGoodRecordXlstemp.Genspec = "";

                        value = row.GetCell(4); // MyValues.GetValue(1, 4);
                        if (value != null)
                            inoutGenGoodRecordXlstemp.Transrate = value.ToString();
                        else
                            inoutGenGoodRecordXlstemp.Transrate = "";

                        value = row.GetCell(5); //MyValues.GetValue(1, 5);
                        if (value != null)
                            inoutGenGoodRecordXlstemp.Genproducer = value.ToString();
                        else
                            inoutGenGoodRecordXlstemp.Genproducer = "";
                        value = row.GetCell(6); //MyValues.GetValue(1, 6);
                        if (value != null)
                            inoutGenGoodRecordXlstemp.Goodbar = value.ToString();
                        else
                            inoutGenGoodRecordXlstemp.Goodbar = "";
                        value = row.GetCell(7);//MyValues.GetValue(1, 7);
                        if (value != null)
                            inoutGenGoodRecordXlstemp.Ratifier = value.ToString();
                        else
                            inoutGenGoodRecordXlstemp.Ratifier = "";
                        value = row.GetCell(8);//MyValues.GetValue(1,8);
                        if (value != null)
                            inoutGenGoodRecordXlstemp.Recordmark = value.ToString();
                        else
                            inoutGenGoodRecordXlstemp.Recordmark = "";

                        inoutGenGoodRecordXlstempList.Add(inoutGenGoodRecordXlstemp);

                    }
                    return inoutGenGoodRecordXlstempList;
                }

            }
            catch (Exception err)
            {
                MessageBox.Show("数据绑定Excel失败!失败原因：" + err.Message, "提示信息",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {


            }

        }



        ///////------2018-8-8------
        //////
        static public SortableBindingList<InoutGenCmsbillXlstemp> OpenInquiryExcelGCB(string excelFile)
        {
            IWorkbook wk = null;
            try
            {
                Stream stream = new FileStream(excelFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                wk = WorkbookFactory.Create(stream);

                {
                    ISheet sheet = wk.GetSheetAt(0);


                    SortableBindingList<InoutGenCmsbillXlstemp> inoutGenCmsbillXlstempList = new SortableBindingList<InoutGenCmsbillXlstemp>();
                    string inoutGenCmsbillXlstempCode = null;
                    IRow row = sheet.GetRow(0);  //读取当前行数据
                    //LastRowNum 是当前表的总行数-1（注意）

                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        InoutGenCmsbillXlstemp inoutGenCmsbillXlstemp = new InoutGenCmsbillXlstemp();
                        //序号	 部门   客户商品代码	  cms商品代码    计划单价	 计划下单量    批卡备注	  审批备注   地址外码  地址cms码  

                        row = sheet.GetRow(i);  //读取当前行数据
                        if (row == null) continue;

                        Object value = row.GetCell(0);
                        if (i == 1)
                        {
                            if (value != null)
                            {
                                inoutGenCmsbillXlstempCode = value.ToString();
                                inoutGenCmsbillXlstemp.ExcelSeqid = inoutGenCmsbillXlstempCode;
                            }
                            else
                            {
                                MessageBox.Show("第[1]行序号有错误!请检查文件准确性!");
                                inoutGenCmsbillXlstempList.Clear();
                                return inoutGenCmsbillXlstempList;
                            }
                        }
                        else
                        {
                            if (value == null) continue;
                            else if (inoutGenCmsbillXlstempCode.CompareTo(value.ToString()) == 0)
                            {
                                MessageBox.Show(String.Format("第[{0:d}]行序号有错误!\n请检查文件准确性!", i));
                                inoutGenCmsbillXlstempList.Clear();
                                return inoutGenCmsbillXlstempList;
                            }
                            else
                            {
                                value = row.GetCell(0);
                                inoutGenCmsbillXlstempCode = value.ToString();
                                inoutGenCmsbillXlstemp.ExcelSeqid = inoutGenCmsbillXlstempCode;
                            }
                        }

                        value = row.GetCell(1); //MyValues.GetValue(1, 1);
                        if (value != null)
                            inoutGenCmsbillXlstemp.Saledeptid = value.ToString();
                        else
                            inoutGenCmsbillXlstemp.Saledeptid = "";

                        value = row.GetCell(2); //MyValues.GetValue(1, 1);
                        if (value != null)
                            inoutGenCmsbillXlstemp.Gengoods = value.ToString();
                        else
                            inoutGenCmsbillXlstemp.Gengoods = "";
                        value = row.GetCell(3); //MyValues.GetValue(1, 2);
                        if (value != null)
                            inoutGenCmsbillXlstemp.Goods = value.ToString();
                        else
                            inoutGenCmsbillXlstemp.Goods = "";
                        value = row.GetCell(4); //MyValues.GetValue(1, 3);
                        if (value != null)
                        {
                            inoutGenCmsbillXlstemp.Planprc = value.ToString();
                            inoutGenCmsbillXlstemp.Importprc = value.ToString();
                        }
                        else
                            continue;

                        value = row.GetCell(5); // MyValues.GetValue(1, 4);
                        if (value != null)
                            inoutGenCmsbillXlstemp.Plancount = value.ToString();
                        else
                            continue;

                        value = row.GetCell(6); //MyValues.GetValue(1, 5);
                        if (value != null)
                            inoutGenCmsbillXlstemp.Pmark = value.ToString();
                        else
                            inoutGenCmsbillXlstemp.Pmark = "";
                        value = row.GetCell(7); //MyValues.GetValue(1, 6);
                        if (value != null)
                            inoutGenCmsbillXlstemp.Amark = value.ToString();
                        else
                            inoutGenCmsbillXlstemp.Amark = "";
                        value = row.GetCell(8);//MyValues.GetValue(1, 7);
                        if (value != null)
                            inoutGenCmsbillXlstemp.Genaddresscode = value.ToString();
                        else
                            inoutGenCmsbillXlstemp.Genaddresscode = "";
                        value = row.GetCell(9);//MyValues.GetValue(1,8);
                        if (value != null)
                            inoutGenCmsbillXlstemp.Cmsaddresscode = value.ToString();
                        else
                            inoutGenCmsbillXlstemp.Cmsaddresscode = "";

                        inoutGenCmsbillXlstempList.Add(inoutGenCmsbillXlstemp);

                    }
                    return inoutGenCmsbillXlstempList;
                }

            }
            catch (Exception err)
            {
                MessageBox.Show("数据绑定Excel失败!失败原因：" + err.Message, "提示信息",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {


            }
        }
        /// <summary>
        /// 实体类集合导出到Excle
        /// </summary>
        /// <param name="cellHeard">单元头的Key和Value：{ { "ExcelSeqid", "序号" }, {"Saledeptid", "部门ID" } };</param>
        /// <param name="enList">数据源</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>文件的下载地址</returns>
        public static string EntityListToExcel2003(Dictionary<string, string> cellHeard, IList enList, string sheetName,string fileNameString)
        {
            try
            {
                string fileName = sheetName + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls"; // 文件名称
                string urlPath = "UpFiles/ExcelFiles/" + fileName; // 文件下载的URL地址，供给前台下载
                string filePath = fileNameString; // 文件路径

                // 1.检测是否存在文件夹，若不存在就建立个文件夹
                //string directoryName = Path.GetDirectoryName(filePath);
                //if (!Directory.Exists(directoryName))
                //{
                //    Directory.CreateDirectory(directoryName);
                //}

                // 2.解析单元格头部，设置单元头的中文名称
                HSSFWorkbook workbook = new HSSFWorkbook(); // 工作簿
                ISheet sheet = workbook.CreateSheet(sheetName); // 工作表
                IRow row = sheet.CreateRow(0);
                List<string> keys = cellHeard.Keys.ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    row.CreateCell(i).SetCellValue(cellHeard[keys[i]]); // 列名为Key的值
                }

                // 3.List对象的值赋值到Excel的单元格里
                int rowIndex = 1; // 从第二行开始赋值(第一行已设置为单元头)
                foreach (var en in enList)
                {
                    IRow rowTmp = sheet.CreateRow(rowIndex);
                    for (int i = 0; i < keys.Count; i++) // 根据指定的属性名称，获取对象指定属性的值
                    {
                        string cellValue = ""; // 单元格的值
                        object properotyValue = null; // 属性的值
                        System.Reflection.PropertyInfo properotyInfo = null; // 属性的信息

                        // 3.1 若属性头的名称包含'.',就表示是子类里的属性，那么就要遍历子类，eg：UserEn.UserName
                        if (keys[i].IndexOf(".") >= 0)
                        {
                            // 3.1.1 解析子类属性(这里只解析1层子类，多层子类未处理)
                            string[] properotyArray = keys[i].Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                            string subClassName = properotyArray[0]; // '.'前面的为子类的名称
                            string subClassProperotyName = properotyArray[1]; // '.'后面的为子类的属性名称
                            System.Reflection.PropertyInfo subClassInfo = en.GetType().GetProperty(subClassName); // 获取子类的类型
                            if (subClassInfo != null)
                            {
                                // 3.1.2 获取子类的实例
                                var subClassEn = en.GetType().GetProperty(subClassName).GetValue(en, null);
                                // 3.1.3 根据属性名称获取子类里的属性类型
                                properotyInfo = subClassInfo.PropertyType.GetProperty(subClassProperotyName);
                                if (properotyInfo != null)
                                {
                                    properotyValue = properotyInfo.GetValue(subClassEn, null); // 获取子类属性的值
                                }
                            }
                        }
                        else
                        {
                            // 3.2 若不是子类的属性，直接根据属性名称获取对象对应的属性
                            properotyInfo = en.GetType().GetProperty(keys[i]);
                            if (properotyInfo != null)
                            {
                                properotyValue = properotyInfo.GetValue(en, null);
                            }
                        }

                        // 3.3 属性值经过转换赋值给单元格值
                        if (properotyValue != null)
                        {
                            cellValue = properotyValue.ToString();
                            // 3.3.1 对时间初始值赋值为空
                            if (cellValue.Trim() == "0001/1/1 0:00:00" || cellValue.Trim() == "0001/1/1 23:59:59")
                            {
                                cellValue = "";
                            }
                        }

                        // 3.4 填充到Excel的单元格里
                        rowTmp.CreateCell(i).SetCellValue(cellValue);
                    }
                    rowIndex++;
                }

                // 4.生成文件
                FileStream file = new FileStream(filePath, FileMode.Create);
                workbook.Write(file);
                file.Close();

                // 5.返回下载路径
                return urlPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw ex;
            }
        }

        //list集合转化为DataTable
        /// <summary>

        /// Convert a List{T} to a DataTable.

        /// </summary>

        public static DataTable ToDataTable<T>(List<T> items)

        {
            var tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                   values[i] = props[i].GetValue(item, null);
                }
                tb.Rows.Add(values);
            }
            return tb;
        }

        /// <summary>

        /// Determine of specified type is nullable

        /// </summary>

        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }

        }


        //协议系统 ClientInfo的excel导入导出  2018-7-25
        static public SortableBindingList<ClientXlsInfo> OpenImportExceCI(string excelFile)
        {

            IWorkbook wk = null;
            try
            {
                Stream stream = new FileStream(excelFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                wk = WorkbookFactory.Create(stream);

                {
                    ISheet sheet = wk.GetSheetAt(0);


                    SortableBindingList<ClientXlsInfo> inoutClientXlstempList = new SortableBindingList<ClientXlsInfo>();
                    string inoutClientXlstempCode = null;
                    IRow row = sheet.GetRow(0);  //读取当前行数据
                    //LastRowNum 是当前表的总行数-1（注意）

                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        ClientXlsInfo inoutClientXlstemp = new ClientXlsInfo();
                        //序号	 年份	 供应商编号   协议级别    客户代码	客户名称   去年对应上游	   今年厂家预测量...     

                        row = sheet.GetRow(i);  //读取当前行数据
                        if (row == null) continue;

                        Object value = row.GetCell(0);
                        if (i == 1)
                        {
                            if (value != null)
                            {
                                inoutClientXlstempCode = value.ToString();
                                inoutClientXlstemp.ExcelSeqid = inoutClientXlstempCode;
                            }
                            else
                            {
                                MessageBox.Show("第[1]行序号有错误!请检查文件准确性!");
                                inoutClientXlstempList.Clear();
                                return inoutClientXlstempList;
                            }
                        }
                        else
                        {
                            if (value == null) continue;
                            else if (inoutClientXlstempCode.CompareTo(value.ToString()) == 0)
                            {
                                MessageBox.Show(String.Format("第[{0:d}]行序号有错误!\n请检查文件准确性!", i));
                                inoutClientXlstempList.Clear();
                                return inoutClientXlstempList;
                            }
                            else
                            {
                                value = row.GetCell(0);
                                inoutClientXlstempCode = value.ToString();
                                inoutClientXlstemp.ExcelSeqid = inoutClientXlstempCode;
                            }
                        }

                        value = row.GetCell(1); //MyValues.GetValue(1, 2);
                        if (value != null)
                            inoutClientXlstemp.YearNum = value.ToString();
                        else
                            continue;

                        value = row.GetCell(2); //MyValues.GetValue(1, 2);
                        if (value != null)
                            inoutClientXlstemp.ProdId = value.ToString();
                        else
                            inoutClientXlstemp.ProdId = "";
                        value = row.GetCell(3); //MyValues.GetValue(1, 3);
                        if (value != null)
                            inoutClientXlstemp.AgreeLevel = value.ToString();
                        else
                            inoutClientXlstemp.AgreeLevel = "";
                        value = row.GetCell(4); //MyValues.GetValue(1, 4);
                        if (value != null)
                            inoutClientXlstemp.CstCode = value.ToString();
                        else
                            inoutClientXlstemp.CstCode = "";

                        value = row.GetCell(5); // MyValues.GetValue(1, 5);
                        if (value != null)
                            inoutClientXlstemp.CstName = value.ToString();
                        else
                            inoutClientXlstemp.CstName = "";

                        value = row.GetCell(6); //MyValues.GetValue(1, 6);
                        if (value != null)
                            inoutClientXlstemp.LastUpStream = value.ToString();
                        else
                            inoutClientXlstemp.LastUpStream = "";
                        value = row.GetCell(7); //MyValues.GetValue(1, 6);
                        if (value != null)
                            inoutClientXlstemp.ForeCastValues = value.ToString();
                        else
                            inoutClientXlstemp.ForeCastValues = "";
                        value = row.GetCell(8);//MyValues.GetValue(1, 7);
                        if (value != null)
                            inoutClientXlstemp.TarGet = value.ToString();
                        else
                            inoutClientXlstemp.TarGet = "";
                        value = row.GetCell(9);//MyValues.GetValue(1, 7);
                        if (value != null)
                            inoutClientXlstemp.LastValues = value.ToString();
                        else
                            inoutClientXlstemp.LastValues = "";
                      
                        inoutClientXlstempList.Add(inoutClientXlstemp);

                    }
                    return inoutClientXlstempList;
                }

            }
            catch (Exception err)
            {
                MessageBox.Show("数据绑定Excel失败!失败原因：" + err.Message, "提示信息",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {


            }

        }


        //客户名称匹配
        static public SortableBindingList<CstNameMatch> OpenInquiryExcelCstName(string excelFile)
        {
            IWorkbook wk = null;
            try
            {
                Stream stream = new FileStream(excelFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                wk = WorkbookFactory.Create(stream);

                {
                    ISheet sheet = wk.GetSheetAt(0);


                    SortableBindingList<CstNameMatch> inoutList = new SortableBindingList<CstNameMatch>();
                    string inoutCode = null;
                    IRow row = sheet.GetRow(0);  //读取当前行数据
                    //LastRowNum 是当前表的总行数-1（注意）

                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        CstNameMatch inoutXlstemp = new CstNameMatch();
                        //序号	 部门   客户商品代码	  cms商品代码    计划单价	 计划下单量    批卡备注	  审批备注   地址外码  地址cms码  

                        row = sheet.GetRow(i);  //读取当前行数据
                        if (row == null) continue;

                        Object value = row.GetCell(0);
                        if (i == 1)
                        {
                            if (value != null)
                            {
                                inoutCode = value.ToString();
                                inoutXlstemp.ExcelSeqid = inoutCode;
                            }
                            else
                            {
                                MessageBox.Show("第[1]行序号有错误!请检查文件准确性!");
                                inoutList.Clear();
                                return inoutList;
                            }
                        }
                        else
                        {
                            if (value == null) continue;
                            else if (inoutCode.CompareTo(value.ToString()) == 0)
                            {
                                MessageBox.Show(String.Format("第[{0:d}]行序号有错误!\n请检查文件准确性!", i));
                                inoutList.Clear();
                                return inoutList;
                            }
                            else
                            {
                                value = row.GetCell(0);
                                inoutCode = value.ToString();
                                inoutXlstemp.ExcelSeqid = inoutCode;
                            }
                        }

                        value = row.GetCell(1); //MyValues.GetValue(1, 1);
                        if (value != null)
                            inoutXlstemp.CstName = value.ToString();
                        else
                            inoutXlstemp.CstName = "";

                        inoutList.Add(inoutXlstemp);
                    }
                    return inoutList;
                }

            }
            catch (Exception err)
            {
                MessageBox.Show("数据绑定Excel失败!失败原因：" + err.Message, "提示信息",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {


            }
        }


        //订单工具 PromoInfo的excel导入导出  2018-7-25
        static public SortableBindingList<PromoExcelInfo> ImportExcePromo(string excelFile)
        {

            IWorkbook wk = null;
            try
            {
                Stream stream = new FileStream(excelFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                wk = WorkbookFactory.Create(stream);

                {
                    ISheet sheet = wk.GetSheetAt(0);


                    SortableBindingList<PromoExcelInfo> inoutClientXlstempList = new SortableBindingList<PromoExcelInfo>();
                    string inoutClientXlstempCode = null;
                    IRow row = sheet.GetRow(0);  //读取当前行数据
                    //LastRowNum 是当前表的总行数-1（注意）

                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        PromoExcelInfo inoutClientXlstemp = new PromoExcelInfo();
                        //序号	 活动政策名称	 商品代码   活动对象代码    开始时间	结束时间   活动政策	   备注...     

                        row = sheet.GetRow(i);  //读取当前行数据
                        if (row == null) continue;

                        Object value = row.GetCell(0);
                        if (i == 1)
                        {
                            if (value != null)
                            {
                                inoutClientXlstempCode = value.ToString();
                                inoutClientXlstemp.ExcelSeqid = inoutClientXlstempCode;
                            }
                            else
                            {
                                MessageBox.Show("第[1]行序号有错误!请检查文件准确性!");
                                inoutClientXlstempList.Clear();
                                return inoutClientXlstempList;
                            }
                        }
                        else
                        {
                            if (value == null) continue;
                            else if (inoutClientXlstempCode.CompareTo(value.ToString()) == 0)
                            {
                                MessageBox.Show(String.Format("第[{0:d}]行序号有错误!\n请检查文件准确性!", i));
                                inoutClientXlstempList.Clear();
                                return inoutClientXlstempList;
                            }
                            else
                            {
                                value = row.GetCell(0);
                                inoutClientXlstempCode = value.ToString();
                                inoutClientXlstemp.ExcelSeqid = inoutClientXlstempCode;
                            }
                        }

                        value = row.GetCell(1); //MyValues.GetValue(1, 2);
                        if (value != null)
                            inoutClientXlstemp.PocName = value.ToString();
                        else
                            continue;

                        value = row.GetCell(2); //MyValues.GetValue(1, 2);
                        if (value != null)
                            inoutClientXlstemp.Goods = value.ToString();
                        else
                            inoutClientXlstemp.Goods = "";
                        value = row.GetCell(3); //MyValues.GetValue(1, 3);
                        if (value != null)
                            inoutClientXlstemp.CstCode = value.ToString();
                        else
                            inoutClientXlstemp.CstCode = "";

                        value = row.GetCell(4); // MyValues.GetValue(1, 5);
                        if (value != null)
                            inoutClientXlstemp.BeginTime = value.ToString();
                        else
                            inoutClientXlstemp.BeginTime = "";

                        value = row.GetCell(5); //MyValues.GetValue(1, 6);
                        if (value != null)
                            inoutClientXlstemp.EndTime = value.ToString();
                        else
                            inoutClientXlstemp.EndTime = "";
                        value = row.GetCell(6); //MyValues.GetValue(1, 6);
                        if (value != null)
                            inoutClientXlstemp.Policy = value.ToString();
                        else
                            inoutClientXlstemp.Policy = "";
                        value = row.GetCell(7);//MyValues.GetValue(1, 7);
                        if (value != null)
                            inoutClientXlstemp.Remark = value.ToString();
                        else
                            inoutClientXlstemp.Remark = "";
                       
                        inoutClientXlstempList.Add(inoutClientXlstemp);

                    }
                    return inoutClientXlstempList;
                }

            }
            catch (Exception err)
            {
                MessageBox.Show("数据绑定Excel失败!失败原因：" + err.Message, "提示信息",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {


            }

        }


        //系统库存对接 的excel导入导出  2019-1-17
        static public SortableBindingList<SysDockXlsInfo> OpenImportExceSys(string excelFile)
        {

            IWorkbook wk = null;
            try
            {
                Stream stream = new FileStream(excelFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                wk = WorkbookFactory.Create(stream);

                {
                    ISheet sheet = wk.GetSheetAt(0);


                    SortableBindingList<SysDockXlsInfo> inoutClientXlstempList = new SortableBindingList<SysDockXlsInfo>();
                    string inoutClientXlstempCode = null;
                    IRow row = sheet.GetRow(0);  //读取当前行数据
                    //LastRowNum 是当前表的总行数-1（注意）

                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        SysDockXlsInfo inoutClientXlstemp = new SysDockXlsInfo();
                        //序号	 商品代码.     

                        row = sheet.GetRow(i);  //读取当前行数据
                        if (row == null) continue;

                        Object value = row.GetCell(0);
                        if (i == 1)
                        {
                            if (value != null)
                            {
                                inoutClientXlstempCode = value.ToString();
                                inoutClientXlstemp.ExcelSeqid = inoutClientXlstempCode;
                            }
                            else
                            {
                                MessageBox.Show("第[1]行序号有错误!请检查文件准确性!");
                                inoutClientXlstempList.Clear();
                                return inoutClientXlstempList;
                            }
                        }
                        else
                        {
                            if (value == null) continue;
                            else if (inoutClientXlstempCode.CompareTo(value.ToString()) == 0)
                            {
                                MessageBox.Show(String.Format("第[{0:d}]行序号有错误!\n请检查文件准确性!", i));
                                inoutClientXlstempList.Clear();
                                return inoutClientXlstempList;
                            }
                            else
                            {
                                value = row.GetCell(0);
                                inoutClientXlstempCode = value.ToString();
                                inoutClientXlstemp.ExcelSeqid = inoutClientXlstempCode;
                            }
                        }

                        value = row.GetCell(1); //MyValues.GetValue(1, 2);
                        if (value != null)
                            inoutClientXlstemp.Goods = value.ToString();
                        else
                            continue;                    
                        inoutClientXlstempList.Add(inoutClientXlstemp);

                    }
                    return inoutClientXlstempList;
                }

            }
            catch (Exception err)
            {
                MessageBox.Show("数据绑定Excel失败!失败原因：" + err.Message, "提示信息",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            finally
            {


            }

        }





    }
}
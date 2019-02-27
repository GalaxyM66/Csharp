using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace PriceManager
{
    class FormUtils
    {
        /// <summary>
        /// combox绑定数据源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cbo">需要绑定的combobox</param>
        /// <param name="list">数据源</param>
        /// <param name="display">显示值名</param>
        /// <param name="value">实际值名</param>
        public static void SetComboBox<T>(ComboBox cbo, SortableBindingList<T> list, string display, string value)
        {
            cbo.DataSource = list;
            cbo.DisplayMember = display;
            cbo.ValueMember = value;
            cbo.SelectedIndex = 0;
        }
        /// <summary>
        /// 拼接多选字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="text">需要拼接的字符串</param>
        public static string StrConstruct(string text)
        {
            if (StringUtils.IsNull(text))
                return null;
            string result = "";
            string[] sArray = Regex.Split(text, "\r\n", RegexOptions.IgnoreCase);
            int log = 0;
            foreach (var i in sArray)
            {
                result += AddMarks(i, log);
                log++;
            }
            return result;
        }

        public static string AddMarks(string i, int log)
        {
            if (log == 0)
                return "'" + i + "'";
            else
                return ",'" + i + "'";
        }

        /// <summary>
        /// DataGridView绑定数据源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dgv">需要绑定的DataGridView</param>
        /// <param name="list">数据源</param>
        public static void RefreshDataGridView<T>(DataGridView dgv, SortableBindingList<T> list)
        {
            dgv.DataSource = null;
            dgv.AutoGenerateColumns = false;
            dgv.DataSource = list;
        }

        /// <summary>
        /// 绑定时使用使用子对象的属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //DataGridView dgv = (DataGridView)sender;
            //DataGridViewRow row = dgv.Rows[e.RowIndex];
            //DataGridViewColumn col = dgv.Columns[e.ColumnIndex];
            //if (row.DataBoundItem != null && col.DataPropertyName.Contains("."))
            //{
            //    string[] props = col.DataPropertyName.Split('.');
            //    PropertyInfo propInfo = row.DataBoundItem.GetType().GetProperty(props[0]);
            //    object val = propInfo.GetValue(row.DataBoundItem, null);

            //    for (int i = 1; i < props.Length; i++)
            //    {
            //        propInfo = val.GetType().GetProperty(props[i]);
            //        val = propInfo.GetValue(val, null);
            //    }
            //    //dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = val;
            //    e.Value = val;
            //}
        }

        /// <summary>
        /// Excel导出
        /// </summary>
        /// <param name="dgv">需导出的DataGridView</param>
        /// <param name="sfd">SaveFileDialog</param>
        /// <param name="fileName">文件名</param>
        public static void ExcelExport(DataGridView dgv, SaveFileDialog sfd, string fileName)
        {
            if (dgv.DataSource == null || dgv.Rows.Count == 0)
            {
                MessageBox.Show("无内容导出");
                return;
            }
            string msg = "";
            sfd.Filter = " xlsx files(*.xlsx)|*.*";
            sfd.FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + fileName + ".xlsx";
            DataTable dt = null;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                fileName = sfd.FileName;
                try
                {
                    dt = DataGridviewToDataTable(dgv);
                    ExcelHelper.ExportDataToExcel(dt, fileName, true, out msg);
                }
                catch (Exception ex)
                {
                    msg = ex.Message.ToString();
                }
                MessageBox.Show(msg);
            }
        }

        /// <summary>
        ///DataGridView转换成DataTable
        /// </summary>
        ///         /// <param name="dgv">DataGridView</param>
        public static DataTable DataGridviewToDataTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            DataColumn dc = null;
            DataRow dr = null;
            try
            {
                int columnsCount = dgv.Columns.Count;
                // 列强制转换  
                for (int count = 1; count < columnsCount; count++)
                {
                    dc = new DataColumn(dgv.Columns[count].HeaderText.ToString());
                    dt.Columns.Add(dc);
                }

                // 循环行  
                for (int rcount = 0; rcount < dgv.Rows.Count; rcount++)
                {
                    dr = dt.NewRow();
                    for (int countsub = 0; countsub < columnsCount - 1; countsub++)
                    {
                        dr[countsub] = Convert.ToString(dgv.Rows[rcount].Cells[countsub + 1].FormattedValue).Trim();
                    }
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dc = null;
                dr = null;
            }
            return dt;
        }

        /// <summary>
        /// 用户组和商品组combobox下拉查询设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="option"></param>
        public static void GroupComboBoxSetting(object sender, string option)
        {
            ComboBox cbo = (ComboBox)sender;
            GroupsCbo form = new GroupsCbo()
            {
                Top = Cursor.Position.Y,
                Left = Cursor.Position.X
            };
            form.option = option;
            if (form.ShowDialog() == DialogResult.OK)
            {
                if ("clients".Equals(option))
                {
                    if (form.cgroup != null && form.cgroup.Count > 0)
                        SetComboBox(cbo, form.cgroup, "Groupname", "TagPtr");
                    else
                        cbo.DataSource = null;
                }
                else if ("goods".Equals(option))
                {
                    if (form.ggroup != null && form.ggroup.Count > 0)
                        SetComboBox(cbo, form.ggroup, "Groupname", "TagPtr");
                    else
                        cbo.DataSource = null;
                }

            }
        }

        /// <summary>
        /// DataGridView单选
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static Object SelectRow(DataGridView dgv)
        {
            DataGridViewSelectedRowCollection row = dgv.SelectedRows;
            if (row.Count == 1)
                return row[0].Cells[0].Value;
            else
                return null;
        }
        /// <summary>
        /// DataGridView多选
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static Object SelectRows(DataGridView dgv)
        {
            DataGridViewSelectedRowCollection row = dgv.SelectedRows;
            if (row.Count > 0)
                return row.SyncRoot;
            else
                return null;
        }

        /// <summary>
        /// 记录返回数据
        /// </summary>
        /// <param name="result"></param>
        /// <param name="resMsg"></param>
        public static void SetResult(string[] result, ResultInfo resMsg)
        {
            if ("1".Equals(result[0])) {
                resMsg.SuccessCount++;
                resMsg.ResultMsg += result[1] + "\r\n";
            }
            else
            {
                resMsg.ErrorCount++;
                resMsg.ResultMsg += result[1] + "\r\n";
            }
        }

        /// <summary>
        /// 显示返回数据
        /// </summary>
        /// <param name="resMsg"></param>
        public static void SendMsg(ResultInfo resMsg)
        {
          //  MessageBox.Show($"成功数:{resMsg.SuccessCount.ToString()}\r\n失败数:{resMsg.ErrorCount.ToString()}\r\n");
            if (resMsg.ErrorCount > 0)
            {
                ErrorForm form = new ErrorForm()
                {
                    result = resMsg,
                    StartPosition = FormStartPosition.CenterScreen
                };
                form.Show();
            }
            MessageBox.Show(resMsg.ResultMsg,"后台提示");
            resMsg.InitResult();
        }

        /// <summary>
        /// 商品combobox下拉查询设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="option"></param>
        public static void GoodsComboBoxSetting(object sender)
        {
            ComboBox cbo = (ComboBox)sender;
            GoodsCbo form = new GoodsCbo()
            {
                Top = Cursor.Position.Y,
                Left = Cursor.Position.X
            };
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.goods != null && form.goods.Count > 0)
                    SetComboBox(cbo, form.goods, "Name", "TagPtr");
                else
                    cbo.DataSource = null;
            }
        }

        /// <summary>
        /// 客户combobox下拉查询设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="option"></param>
        public static void ClientsComboBoxSetting(object sender)
        {
            ComboBox cbo = (ComboBox)sender;
            ClientsCbo form = new ClientsCbo()
            {
                Top = Cursor.Position.Y,
                Left = Cursor.Position.X
            };
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.clients != null && form.clients.Count > 0)
                    SetComboBox(cbo, form.clients, "Cstname", "TagPtr");
                else
                    cbo.DataSource = null;
            }
        }

        /// <summary>
        /// 区域combobox下拉查询设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="option"></param>
        public static void AreasComboBoxSetting(object sender)
        {
            ComboBox cbo = (ComboBox)sender;
            AreasCbo form = new AreasCbo()
            {
                Top = Cursor.Position.Y,
                Left = Cursor.Position.X
            };
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.area != null && form.area.Count > 0)
                    SetComboBox(cbo, form.area, "Name", "TagPtr");
                else
                    cbo.DataSource = null;
            }
        }

        /// <summary>
        /// 商品从属性模板下载
        /// </summary>
        /// <param name="sfd"></param>
        /// <param name="fileName">文件名</param>
        internal static Boolean TmpDownload(SaveFileDialog sfd, string fileName,string URL)
        {
            sfd.Filter = " xlsx files(*.xlsx)|*.*";
            sfd.FileName = fileName + ".xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    HttpDownload(sfd.FileName, URL);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            return false;
        }

        private static void HttpDownload(string filePath, string url)
        {
            string tempPath = Path.GetDirectoryName(filePath); //+ @"\temp";
            Directory.CreateDirectory(tempPath);  //创建临时文件目录
            string tempFile = tempPath + @"\" + Path.GetFileName(filePath) + ".temp"; //临时文件
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);    //存在则删除
            }
            FileStream fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream responseStream = response.GetResponseStream();
            //创建本地文件写入流
            //Stream stream = new FileStream(tempFile, FileMode.Create);
            byte[] bArr = new byte[1024];
            int size = responseStream.Read(bArr, 0, (int)bArr.Length);
            while (size > 0)
            {
                //stream.Write(bArr, 0, size);
                fs.Write(bArr, 0, size);
                size = responseStream.Read(bArr, 0, (int)bArr.Length);
            }
            //stream.Close();
            fs.Close();
            responseStream.Close();
            if (File.Exists(filePath))
                File.Delete(filePath);    //存在则删除
            File.Move(tempFile, filePath);
        }

        public static void Num_DiscountAmount_MouseWheel(object sender, MouseEventArgs e)
        {
        //    if (e is HandledMouseEventArgs h)
            //    h.Handled = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

namespace PriceManager
{
    public partial class ToolsForm_BillQuotation : DockContent
    {
        BillQuotationTemp infoss = new BillQuotationTemp();
        string quan = "";
        string keys = "";
        public ToolsForm_BillQuotation()
        {
            InitializeComponent();
            dgvBill.AutoGenerateColumns = false;
        }
        APDao_B2BTools dao = new APDao_B2BTools();

        //excel表数据
        SortableBindingList<BillQuotationTemp> ExcelList = new SortableBindingList<BillQuotationTemp>();
        //临时表数据
        SortableBindingList<BillQuotationTemp> TempList = new SortableBindingList<BillQuotationTemp>();
        //Goodid存在的临时List
        SortableBindingList<BillQuotationTemp> GoodidTempList = new SortableBindingList<BillQuotationTemp>();
        //已从MySQL获取价格写回中间表List
        SortableBindingList<BillQuotationTemp> PrcTempList = new SortableBindingList<BillQuotationTemp>();
        //处理完成显示List
        SortableBindingList<BillQuotationTemp> BillQuotationList = new SortableBindingList<BillQuotationTemp>();

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
        //清空界面
        private void clearUI()
        {
            //dataGridView1.DataSource = null;
            //dataGridView2.DataSource = null;
            //fileNameTextBox.Text = "";
            //CustomerCodeTB.Text = "";
            //CustomerInfoTB.Text = "";
            //CheckCustomerBT.Enabled = false;
            //BidPriceBT.Enabled = false;
            //MatchSKUBT.Enabled = false;
            //ExportBT.Enabled = false;
            //SQueryBT.Enabled = false;
            txtFileName.Clear();
            TempList.Clear();
            btnRefresh.Enabled = false;
            ExcelList.Clear();
            TempList.Clear();
            GoodidTempList.Clear();
            PrcTempList.Clear();
            BillQuotationList.Clear();
        }

        //打开excel文件
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            //--------------------导入excel表--------------------6.1
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
            txtFileName.Text = fd.FileName;
            path = Path.GetDirectoryName(fd.FileName);
            fileName = fd.SafeFileName;
            //---------------------------------------------------

            //---------------------处理excel表-------------------6.2
            ExcelList = ExcelHelper.OpenInquiryExcelNPOI(fd.FileName);
            if (ExcelList.Count <= 0) return;
            foreach (BillQuotationTemp info in ExcelList)
            {
                info.Cstid = Cstid;
                info.Empid = SessionDto.Empid;
                info.Ownerid = Properties.Settings.Default.OWNERID;
                info.Compid = Properties.Settings.Default.COMPID;
                TempList.Add(info);
            }
            //----------------------------------------------------6.3
            SPRetInfo BillDataret = new SPRetInfo();
            dao.PGetBillData(TempList, BillDataret);
            if (BillDataret.num == "1")
            {
                //-----------------查询goodid---------------------6.3.1
                Batchid = BillDataret.result;
                GoodidTempList = dao.GetGoodidExistList(Batchid);
                //------------------------------------------------
                if (GoodidTempList.Count > 0)
                {
                    //------------Goodid存在的调用MySQL查询价格-------6.4
                    BillQuotationTemp OutputGoodidInfo = new BillQuotationTemp();
                    foreach (BillQuotationTemp InputGoodidInfo in GoodidTempList)
                    {
                        OutputGoodidInfo = InputGoodidInfo;
                        dao.PGetB2BPrc(InputGoodidInfo, OutputGoodidInfo);
                        if (!string.IsNullOrEmpty(OutputGoodidInfo.Prcresultcode))//!=
                        {
                            if (string.IsNullOrEmpty(OutputGoodidInfo.Prc))
                            {
                                OutputGoodidInfo.Prc = "-1";
                                OutputGoodidInfo.Bottomprc = "-1";
                                OutputGoodidInfo.Costprc = "-1";
                                PrcTempList.Add(OutputGoodidInfo);
                            }
                            else
                            {
                                PrcTempList.Add(OutputGoodidInfo);
                            }
                            
                        }
                    }

                    //------------------------------------------------
                    //------------------将价格写回中间表--------------6.4.1
                    SPRetInfo SetRet = new SPRetInfo();
                    foreach (BillQuotationTemp SetBillPrcInputInfo in PrcTempList)
                    {
                        dao.PSetBillPrc(SetBillPrcInputInfo, SetRet);
                        if (SetRet.num == "-1") 
                        MessageBox.Show(SetRet.msg + "|" + SetRet.num, "后台提示！");
                    }
                    
                }
                //------------------------------------------------
                
            }
            else
            {
                MessageBox.Show(BillDataret.msg + "|" + BillDataret.num, "后台提示！");
                return;
            }
            //------写完价格信息调用P_SET_BILL_QUOTATION------6.5

            SPRetInfo QuotationRet = new SPRetInfo();
            dao.PSetBillQuotation(Batchid, QuotationRet);
            if (QuotationRet.num == "1")
            {
                //------------------------------------------------
                //--------------------查询中间表------------------7
                BillQuotationList = dao.GetBillQuotationList(Batchid);
                dgvBill.DataSource = new SortableBindingList<BillQuotationTemp>(BillQuotationList);
                dgvBill.Refresh();
            }
            else
            {
                MessageBox.Show(QuotationRet.msg + "|" + QuotationRet.num, "后台提示！");
                return;
            }
            //if (dgvBill.RowCount > 0) ChangBackColor();

            //------------------------------------------------
            this.Cursor = Cursors.Default;//加载完成，改变鼠标样式
            ExportBT.Enabled = true;
            MatchSKUBT.Enabled = true;
        }

        //客户代码回车
        private void txtCstCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCstCode.Text)) return;
            //暂存客户id
            Cstid = "";
            //客户代码
            cstcode = "";
            //客户信息
            cstinfo = "";
            if (e.KeyValue == 13)
            {
                cstcode = txtCstCode.Text;
                SPRetInfo ret = new SPRetInfo();
                dao.PCstCode(cstcode,ret);
                if (ret.num == "1")
                {
                    cstinfo = ret.msg;
                    txtCstInfo.Text = cstinfo;
                    Cstid = ret.result;
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
                btnOpenFile.Enabled = false;
            }
            else
            {
                btnOpenFile.Enabled = true;
            }
        }

        //载入窗体
        private void ToolsForm_BillQuotation_Load(object sender, EventArgs e)
        {
            string server = AppConfigUtils.getConfig("server");
            string sPort = AppConfigUtils.getConfig("port");
            if (server == null || sPort == null)
            {
                MessageBox.Show("请检查配置文件!");
            }
            int port = int.Parse(sPort);
            
            if (dao.ConnectServer(server, port) == -1) return;

            if (txtCstInfo.Text == "")
            {
                btnOpenFile.Enabled = false;
            }
            else
            {
                btnOpenFile.Enabled = true;
            }
        }
        
        //改变单元格的颜色
        private void dgvBill_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            Color c1 = Color.FromArgb(184, 204, 228);
            if (this.dgvBill.Rows[e.RowIndex].Cells["Column24"].Style.BackColor == Color.Green ||
                this.dgvBill.Rows[e.RowIndex].Cells["Column24"].Style.BackColor == Color.Yellow||
                this.dgvBill.Rows[e.RowIndex].Cells["Column24"].Style.BackColor == Color.Purple
                ) return;
            if ("最近合同价" == this.dgvBill.Rows[e.RowIndex].Cells["Column26"].Value.ToString())
            {
                this.dgvBill.Rows[e.RowIndex].Cells["Column24"].Style.BackColor = Color.Green;
            }
            else if ("电脑价" == this.dgvBill.Rows[e.RowIndex].Cells["Column26"].Value.ToString())
            {
                this.dgvBill.Rows[e.RowIndex].Cells["Column24"].Style.BackColor = Color.Yellow;
            }
            else if ("人工报价" == this.dgvBill.Rows[e.RowIndex].Cells["Column26"].Value.ToString())
            {
                this.dgvBill.Rows[e.RowIndex].Cells["Column24"].Style.BackColor = Color.Purple;
            }
            if (this.dgvBill.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.Gray) return;
            if ("1" == this.dgvBill.Rows[e.RowIndex].Cells["Column36"].Value.ToString())
            {
                this.dgvBill.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gray;
            }
            else if ("-1" == this.dgvBill.Rows[e.RowIndex].Cells["Column36"].Value.ToString())
            {
                this.dgvBill.Rows[e.RowIndex].DefaultCellStyle.BackColor = c1;
            }
            if ("1" == this.dgvBill.Rows[e.RowIndex].Cells["Column46"].Value.ToString())
            {
                this.dgvBill.Rows[e.RowIndex].Cells["Column11"].Style.BackColor = Color.Red;
            }


        }

        //双击单元格进行修改
        private void dgvBill_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 10 || e.RowIndex == -1)
            {
                dgvBill.Columns[e.ColumnIndex].ReadOnly = true;
            }
            if (e.ColumnIndex == 10 && e.RowIndex == -1)
            {
                DataGridViewCell cell = dgvBill.Rows[e.RowIndex].Cells["Column24"];
                dgvBill.CurrentCell = cell;
                dgvBill.BeginEdit(true);
                dgvBill.CellValueChanged += new DataGridViewCellEventHandler(dgvBill_CellValueChanged);

            }
            //dgvBill.Columns["UpdSkuSonAfter"].DefaultCellStyle.BackColor = Color.White;
           
        }

        void dgvBill_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            dgvBill.CurrentCell.Style.BackColor = Color.Purple;
        }

        //编辑完成事件
        private void dgvBill_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SPRetInfo ret = new SPRetInfo();
            BillQuotationTemp info = dgvBill.CurrentRow.DataBoundItem as BillQuotationTemp;

            if (!string.IsNullOrEmpty(info.Quotation))
            {
                dao.PBqHis(int.Parse(info.Batchid), int.Parse(info.ExcelSeqid), double.Parse(info.Quotation), ret);
                if (ret.num != "1")
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                }
                else
                {
                    btnRefresh.Enabled = true;
                }
            }
            else {
               string Quotatio = "-1";
                dao.PBqHis(int.Parse(info.Batchid), int.Parse(info.ExcelSeqid), double.Parse(Quotatio), ret);
                if (ret.num != "1")
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                }
                else
                {
                    btnRefresh.Enabled = true;
                }

            }
            
        }

        //刷新数据按钮
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BillQuotationList = dao.GetBillQuotationList(Batchid);
            dgvBill.DataSource = BillQuotationList;
            dgvBill.Refresh();
        }

        //导出excel
        private void ExportBT_Click(object sender, EventArgs e)
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
            //导出到excel
            //ExcelHelper.DataGridViewToExcel(dgvBill);
            //导出到excel时打开excel
            //ExcelHelper.DataGridviewShowToExcel(dgvBill, true);
            //导出到excel速度最快
            DataTable dt = GetDgvToTable(dgvBill);
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
            DataTable t = dt.DefaultView.ToTable("dt", true, "表序号", "外部商品编码",
                "外部品名规格", "外部商品厂家", "条形码", "国药准字", "部门ID", "CMS商品码",
                "CMS商品名", "CMS商品规格", "CMS商品厂家", "系统报价", "报价来源",
                "自动对码", "可用库存（本货主库存）","可调库存","包装","备案原因");

            return t;
        }

        //委托 事件处理方法
        void selForm_TransfEvent(string quans,string key)
        {
            keys = "";
            quan = "";
            quan = quans;
            keys = key;
        }
        //商品对码
        private void MatchSKUBT_Click(object sender, EventArgs e)
        {
            keys = "";
            if (dgvBill.RowCount <= 0 || dgvBill.CurrentCell == null) return;
            if (string.IsNullOrEmpty(cstinfo) || string.IsNullOrEmpty(cstinfo))
            {
                MessageBox.Show("客户信息连接中断！请刷新！", "系统提示！");
                txtCstCode.Focus();
                return;
               
            }
            string Goodid = "";
            string Saledeptid = "";
            BillQuotationTemp info = dgvBill.CurrentRow.DataBoundItem as BillQuotationTemp;
            MatchForm match = new MatchForm();
            //注册事件
            match.TrandfEvent += selForm_TransfEvent;
            //dgvBill.Rows[]
            //match.ShowDialog();
            //txtSallLeader.Text = empName;
            match.CstCode = cstcode;
            match.CstInfo = cstinfo;
            match.MatchInfo = info;
            match.Cstid = Cstid;

            //match.Goods = info.Gengoods;
            //match.KeyWord = info.Genspec + info.Genproducer + info.Ratifier;
            //match.Producer = info.Genproducer;
            //match.Ratifier = info.Ratifier;
            //match.GoodBar = info.Goodbar;
            //match.Goodname = info.Genspec;
            match.ShowDialog();
            if (match.DialogResult == DialogResult.OK)//新增对码操作
            {
                //Goodid = match.Goodid;
                //Saledeptid = match.Saledeptid;
                //dgvBill.CurrentRow.Cells
                //int index = dgvBill.CurrentRow.Index; //获取选中行的行号
                //dgvBill.Rows[index].Cells["Column24"].Value = quan;
                btnRefresh.Enabled = true;
            }
            else if (match.DialogResult == DialogResult.No)//取消对码
            {
                //Goodid = "-1";
                //Saledeptid = match.Saledeptid;
                btnRefresh.Enabled = true;
            }
            //if (match.DialogResult == DialogResult.OK)
            //{
            //    btnRefresh_Click(sender, e);
            //}

            else
            {
                if (keys == "0")//新增对码
                {
                    int index = dgvBill.CurrentRow.Index; //获取选中行的行号
                    dgvBill.Rows[index].Cells["Column24"].Value = quan;
                }
                if (keys == "1")//取消对码
                {
                    int index = dgvBill.CurrentRow.Index; //获取选中行的行号
                    dgvBill.Rows[index].Cells["Column24"].Value = null;
                }
                btnRefresh.Enabled = true;
                dgvBill.CurrentRow.DefaultCellStyle.BackColor = Color.Gray;
                return;
            }

            //---------------------处理当前条数据-------------------
            SortableBindingList<BillQuotationTemp> SingleList = new SortableBindingList<BillQuotationTemp>();
            //Goodid存在的临时List
            SortableBindingList<BillQuotationTemp> SingleGoodidTempList = new SortableBindingList<BillQuotationTemp>();
            //已从MySQL获取价格写回中间表List
            SortableBindingList<BillQuotationTemp> SinglePrcTempList = new SortableBindingList<BillQuotationTemp>();
           
            SPRetInfo BillDataret = new SPRetInfo();
            dao.PSingleCstCode(Batchid, info.ExcelSeqid, BillDataret);//1.1调用存储过程
            if (BillDataret.num == "1")
            {
                if (Goodid != "-1")
                {
                    //------------通过Goodid去查价格----------1.2 对码ID = Goodid
                    SingleGoodidTempList = dao.GetSingleGoodidExistList(Batchid, info.ExcelSeqid);
                }
                else
                {
                    BillQuotationTemp SingleGoodidInfo = new BillQuotationTemp();

                    SingleGoodidInfo.ExcelSeqid = info.ExcelSeqid;
                    SingleGoodidInfo.Batchid = Batchid;
                    SingleGoodidInfo.Goodid = Goodid;
                    SingleGoodidInfo.Compid = info.Compid;
                    SingleGoodidInfo.Ownerid = info.Ownerid;
                    SingleGoodidInfo.Saledeptid = Saledeptid;
                    SingleGoodidInfo.Cstid = Cstid;
                    SingleGoodidTempList.Add(SingleGoodidInfo);
                }
                //------------调用MySQL查询价格-------1.3 
                
                if (SingleGoodidTempList.Count > 0)
                {
                    //------------Goodid存在的调用MySQL查询价格-------6.4
                    BillQuotationTemp OutputGoodidInfo = new BillQuotationTemp();
                    foreach (BillQuotationTemp InputGoodidInfo in SingleGoodidTempList)
                    {

                        OutputGoodidInfo = InputGoodidInfo;
                        dao.PGetB2BPrc(InputGoodidInfo, OutputGoodidInfo);
                        if (!string.IsNullOrEmpty(OutputGoodidInfo.Prcresultcode))//!=
                        {
                            if (string.IsNullOrEmpty(OutputGoodidInfo.Prc))
                            {
                                OutputGoodidInfo.Prc = "-1";
                                OutputGoodidInfo.Bottomprc = "-1";
                                OutputGoodidInfo.Costprc = "-1";
                                SinglePrcTempList.Add(OutputGoodidInfo);
                            }
                            else
                            {
                                SinglePrcTempList.Add(OutputGoodidInfo);
                            }

                        }
                    }
                }

                //------------------------------------------------
                //------------------将价格写回中间表--------------1.4
                SPRetInfo SetRet = new SPRetInfo();
                foreach (BillQuotationTemp SetBillPrcInputInfo in SinglePrcTempList)
                {
                    dao.PSetBillPrc(SetBillPrcInputInfo, SetRet);
                    if (SetRet.num == "-1")
                        MessageBox.Show(SetRet.msg + "|" + SetRet.num, "后台提示！");
                }

                //}
                //------------------------------------------------

            }
            else
            {
                MessageBox.Show(BillDataret.msg + "|" + BillDataret.num, "后台提示！");
                return;
            }
            //------写完价格信息调用P_SET_BILL_QUOTATION------6.5

            SPRetInfo QuotationRet = new SPRetInfo();
            dao.PSingleSetBillQuotation(Batchid, info.ExcelSeqid, QuotationRet);
            if (QuotationRet.num == "1")
            {
                MessageBox.Show("对码操作成功！" + "|" + QuotationRet.num, "后台提示！");
                dgvBill.CurrentRow.DefaultCellStyle.BackColor = Color.Gray;
            }
            else
            {
                MessageBox.Show("对码操作失败！" + "|" + QuotationRet.num, "后台提示！");
                return;
            }
            //if (dgvBill.RowCount > 0) ChangBackColor();

            //------------------------------------------------
            this.Cursor = Cursors.Default;//加载完成，改变鼠标样式
           
        }
        //排序 1序号排序 2 对码排序
        private void dgvBill_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                //try
                //{

                //    dgvBill.Sort(dgvBill.Columns[1], ListSortDirection.Ascending);
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.ToString());
                //}
                dgvBill.DataSource = new SortableBindingList<BillQuotationTemp>(BillQuotationList);
                dgvBill.Refresh();
                //dgvBill.Sort(dgvBill.Columns[41], ListSortDirection.Ascending);

            }
            if (e.ColumnIndex == 10)
            {
                dgvBill.Sort(dgvBill.Columns[43], ListSortDirection.Descending);
            }

        }
        //单击显示促销信息
        private void dgvBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BillQuotationTemp info = dgvBill.CurrentRow.DataBoundItem as BillQuotationTemp;
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

            if (e.ColumnIndex == 0)
            {
                dgvBill.DataSource = new SortableBindingList<BillQuotationTemp>(BillQuotationList);
                dgvBill.Refresh();
            }

        }

    }
}

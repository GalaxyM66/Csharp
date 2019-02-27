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
    public partial class OrderButtForm : DockContent
    {
        APDao_B2BTools dao = new APDao_B2BTools();
        SPRetInfo retinfo = new SPRetInfo();
        //检查信息下拉框数据源
        Dictionary<string, string> CheckMsgDic = new Dictionary<string, string>();
        List<string> CheckMsg = new List<string>();

        //接口来源下拉框数据源
        Dictionary<string, string> InfCstNameDic = new Dictionary<string, string>();
        List<string> InfCstName = new List<string>();

        SortableBindingList<OrderButtDto> infolist = new SortableBindingList<OrderButtDto>();


        public OrderButtForm()
        {
            InitializeComponent();
        }

        private void OrderButtForm_Load(object sender, EventArgs e)
        {
            dgvOrderButtInfo.AutoGenerateColumns = false;
           
            //日期起时间
            beginDate.Text = DateTime.Now.ToString();
            DateTime dStart = this.beginDate.Value.Date;
            string startDate = dStart.ToString("yyyy-MM-dd HH:mm:ss");//转成字符串
            beginDate.Text = startDate;
            beginDate.Checked = false;

            //日期结束时间 
            DateTime dEnd = new DateTime(this.endDate.Value.Year, this.endDate.Value.Month, this.endDate.Value.Day, 23, 59, 59);
            string enDate = dEnd.ToString("yyyy-MM-dd HH:mm:ss");//转成字符串 
            endDate.Text = enDate;
            //endDate.Checked = false;
            //加载导入状态下拉框
            this.cBImportState.Items.AddRange(new object[] {
                "",
                "成功",
                "失败",
                "待处理",
                "作废"
            });
            cBImportState.SelectedIndex = 3;

            //加载检查信息下拉框
            CheckMsgDic = dao.getCheckMsg();
            CheckMsg = StringUtils.GetValue(CheckMsgDic);
            cBCheckMsg.DataSource = CheckMsg;

            //加载接口来源下拉框
            InfCstNameDic = dao.getInfCstName();
            InfCstName = StringUtils.GetValue(InfCstNameDic);
            cBInfCstName.DataSource = InfCstName;


            //添加复选框列
            DataGridViewCheckBoxColumn columcb = new DataGridViewCheckBoxColumn();
            columcb.HeaderText = "  选  择";
            columcb.Name = "cb_check";
            columcb.TrueValue = true;
            columcb.FalseValue = false;
            columcb.DataPropertyName = "IsChecked";
            dgvOrderButtInfo.Columns.Insert(0, columcb);

        }
        private void clearUI() {
            infolist.Clear();
        }
        //查询
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            clearUI();
            this.Cursor = Cursors.WaitCursor;
            //处理时间控件
            string begintime = "";
            string endtime = "";
            if (beginDate.Checked)
            {
                begintime = beginDate.Value.ToString("yyyyMMddHHmmss");
            }
            else {
                begintime = "";
            }
            if (endDate.Checked)
            {
                endtime = endDate.Value.ToString("yyyyMMddHHmmss");

            }
            else
            {
                endtime = "";
            }
            //处理下拉框
            string checkmsg = cBCheckMsg.SelectedValue.ToString().Trim();
            string checkmsgKey = StringUtils.GetKey(checkmsg,CheckMsgDic);

            string infcstname = cBInfCstName.SelectedValue.ToString().Trim();
            string infcstnameKey = StringUtils.GetKey(infcstname,InfCstNameDic);

            string importstate = cBImportState.SelectedItem.ToString().Trim();

            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            sqlkeydict.Add("accountid", infcstnameKey);
            if (!checkmsgKey.Equals("00")) {
                sqlkeydict.Add("errorcode%", "%"+checkmsgKey+"%");
            }
            if (StringUtils.IsNotNull(importstate))
            {
                sqlkeydict.Add("importstate", importstate);
            }
            if (StringUtils.IsNotNull(txtCstCode.Text)) {
                sqlkeydict.Add("cstcode",txtCstCode.Text.ToString().Trim());
            }
            if (StringUtils.IsNotNull(txtCstName.Text))
            {
                sqlkeydict.Add("cstname%", "%"+txtCstName.Text.ToString().Trim()+"%");
            }
            if (StringUtils.IsNotNull(txtCmsAddressCode.Text))
            {
                sqlkeydict.Add("cmsaddresscode%", "%"+txtCstCode.Text.ToString().Trim()+"%");
            }
            if (StringUtils.IsNotNull(txtAddr_Line_1.Text))
            {
                sqlkeydict.Add("addr_line_1%", "%"+txtAddr_Line_1.Text.ToString().Trim()+"%");
            }
            if (StringUtils.IsNotNull(txtGoods.Text))
            {
                sqlkeydict.Add("goods", txtGoods.Text.ToString().Trim());
            }
            if (StringUtils.IsNotNull(txtGoodName.Text))
            {
                sqlkeydict.Add("goodname%", "%"+txtGoodName.Text.ToString().Trim()+"%");
            }
            infolist=dao.GetOrderButtInfo(begintime, endtime, sqlkeydict);
            if (infolist.Count <= 0)
            {
                MessageBox.Show("未查询到数据！", "后台提示");
                dgvOrderButtInfo.DataSource = infolist;
                dgvOrderButtInfo.Refresh();
                this.Cursor = Cursors.Default;
                return;
            }
            else {
                dgvOrderButtInfo.DataSource = infolist;
                dgvOrderButtInfo.Refresh();
                dgvOrderButtInfo.CurrentCell = null;
                this.Cursor = Cursors.Default;
                this.BtnCheck.Enabled = true;
                this.BtnCancel.Enabled = true;
                this.BtnSelAll.Enabled = true;
                this.BtnNotSel.Enabled = true;
                this.BtnExcImprot.Enabled = true;
            }
            //选中框逻辑
            for (int i = 0; i < infolist.Count; i++)
            {
                if (infolist[i].ChooseFlag == "1")
                {
                    ((DataGridViewCheckBoxCell)dgvOrderButtInfo.Rows[i].Cells[0]).Value = true;
                }
                else
                {
                    ((DataGridViewCheckBoxCell)dgvOrderButtInfo.Rows[i].Cells[0]).Value = false;
                }
            }

        }
        //检验
        private void BtnCheck_Click(object sender, EventArgs e)
        {
            BtnQuery_Click(sender, e);
            //获取新的ID
            string BatchMainId = dao.GetNewBatchId();
            //获取界面的数据源
            List<OrderButtDto> dataLists = new List<OrderButtDto>();
            OrderButtDto ix = new OrderButtDto();
            dataLists.Clear();
            //获取界面上的数据源
            foreach (DataGridViewRow dgvr in dgvOrderButtInfo.Rows)
            {
                dataLists.Add((OrderButtDto)dgvr.DataBoundItem);
            }
            if (dataLists.Count <= 0) return;
            //判断是否选中
            foreach (OrderButtDto infos in dataLists) {
                //选中
                if (infos.ChooseFlag=="1") {
                    //1.插表
                    dao.InsertTemp(BatchMainId,infos);
                    //2.调用存储过程
                    dao.CheckOrderButtInfo(BatchMainId);
                }

            }
            BtnSubmit.Enabled = true;
            //触发筛选事件
            BtnQuery_Click(sender, e);
            this.BtnSubmit.Enabled = true;
            //颜色判定
            Color c1 = Color.FromArgb(250, 191, 143);
            for (int i = 0; i < dgvOrderButtInfo.Rows.Count; i++)
            {
                if (dgvOrderButtInfo.Rows[i].Cells["Column12"].Value.ToString() != dgvOrderButtInfo.Rows[i].Cells["Column14"].Value.ToString())
                {
                    dgvOrderButtInfo.Rows[i].Cells["Column12"].Style.BackColor = c1;
                }
            }
        }
        //刷新数据事件
        private void BtnUpdateData_Click(object sender, EventArgs e)
        {
            BtnQuery_Click(sender, e);
            OrderButtDto prcInfo = new OrderButtDto();
            //获取新的ID
            string BatchMainId = dao.GetNewBatchId();
            //获取界面的数据源
            List<OrderButtDto> dataLists = new List<OrderButtDto>();
            dataLists.Clear();
            //获取界面上的数据源
            foreach (DataGridViewRow dgvr in dgvOrderButtInfo.Rows)
            {
                dataLists.Add((OrderButtDto)dgvr.DataBoundItem);
            }
            if (dataLists.Count <= 0) return;
            //判断是否选中
            foreach (OrderButtDto infos in dataLists)
            {
                //选中
                if (infos.ChooseFlag == "1")
                {
                    //1.插表
                    dao.InsertTemp(BatchMainId, infos);
                    //2.调用存储过程 重写
                    dao.LotCheck(BatchMainId);
                    //3.查询GoodId
                    OrderButtDto stInfo= dao.GetGoodid(infos);

                    if (StringUtils.IsNull(stInfo.GoodId)) {
                        stInfo.GoodId = "-1";
                    }
                    //4.从mysql查价格
                    prcInfo=dao.GetB2BPrc(stInfo);
                    if (!string.IsNullOrEmpty(prcInfo.Prcresultcode))//
                    {
                        if (string.IsNullOrEmpty(prcInfo.Prc))
                        {
                            infos.Prc = "-1";
                            infos.Price = "-1";
                            infos.BottomPrc = "-1";
                            infos.BottomPrice = "-1";
                            infos.CostPrc = "-1";
                            infos.CostPrice = "-1";
                            infos.Prcresultcode = prcInfo.Prcresultcode;
                            infos.PrcMsg = prcInfo.PrcMsg;
                        }
                        else
                        {
                            infos.Prc = prcInfo.Prc;
                            infos.Price = prcInfo.Price;
                            infos.BottomPrc = prcInfo.BottomPrc;
                            infos.BottomPrice = prcInfo.BottomPrice;
                            infos.CostPrc = prcInfo.CostPrc;
                            infos.CostPrice = prcInfo.CostPrice;
                            infos.Prcresultcode = prcInfo.Prcresultcode;
                            infos.PrcMsg = prcInfo.PrcMsg;
                        }

                    }
                    //5.将价格写入中间表
                    dao.SetBillPrc(infos,retinfo);
                }

            }
            //5.查询
            BtnQuery_Click(sender, e);
            //颜色判定
            Color c1 = Color.FromArgb(250, 191, 143);
            for (int i = 0; i < dgvOrderButtInfo.Rows.Count; i++)
            {
                if (dgvOrderButtInfo.Rows[i].Cells["Column12"].Value.ToString() != dgvOrderButtInfo.Rows[i].Cells["Column14"].Value.ToString())
                {
                    dgvOrderButtInfo.Rows[i].Cells["Column12"].Style.BackColor = c1;
                }
            }
        }
        //提交CMS
        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            BtnQuery_Click(sender, e);

            //获取新的ID
            string BatchMainId = dao.GetNewBatchId();
            //获取界面的数据源
            List<OrderButtDto> dataLists = new List<OrderButtDto>();
            OrderButtDto ix = new OrderButtDto();
            dataLists.Clear();
            //获取界面上的数据源
            foreach (DataGridViewRow dgvr in dgvOrderButtInfo.Rows)
            {
                dataLists.Add((OrderButtDto)dgvr.DataBoundItem);
            }
            if (dataLists.Count <= 0) return;
            //判断是否选中
            foreach (OrderButtDto infos in dataLists)
            {
                //选中
                if (infos.ChooseFlag == "1")
                {
                    //1.更新Empid
                    dao.UpdateEmpid(infos);
                    //2.获取新ID 插表
                    dao.InsertTemp(BatchMainId, infos);
                    //3.调用存储过程 
                    dao.SubmitCms(BatchMainId);
                }

            }
            //4.再次查询此表
            BtnQuery_Click(sender, e);
            //颜色判定
            Color c1 = Color.FromArgb(250, 191, 143);
            for (int i = 0; i < dgvOrderButtInfo.Rows.Count; i++)
            {
                if (dgvOrderButtInfo.Rows[i].Cells["Column12"].Value.ToString() != dgvOrderButtInfo.Rows[i].Cells["Column14"].Value.ToString())
                {
                    dgvOrderButtInfo.Rows[i].Cells["Column12"].Style.BackColor = c1;
                }
            }
        }
        //全选
        private void BtnSelAll_Click(object sender, EventArgs e)
        {
            List<OrderButtDto> dataList = new List<OrderButtDto>();
            OrderButtDto ix = new OrderButtDto();
            dataList.Clear();
            //改变chooseflag的值
            for (int i = 0; i < dgvOrderButtInfo.RowCount; i++)
            {
                ((DataGridViewCheckBoxCell)dgvOrderButtInfo.Rows[i].Cells[0]).Value = true;
            }
            //获取界面上的数据源
            foreach (DataGridViewRow dgvr in dgvOrderButtInfo.Rows)
            {
                dataList.Add((OrderButtDto)dgvr.DataBoundItem);

            }

            //改变数据库中的值
            foreach (OrderButtDto info in dataList)
            {
                dao.ChangeFlag(info);
            }
        }
        //取消全选
        private void BtnNotSel_Click(object sender, EventArgs e)
        {
            List<OrderButtDto> dataList = new List<OrderButtDto>();
            OrderButtDto ix = new OrderButtDto();
            dataList.Clear();
            //改变chooseflag的值
            for (int i = 0; i < dgvOrderButtInfo.RowCount; i++)
            {
                ((DataGridViewCheckBoxCell)dgvOrderButtInfo.Rows[i].Cells[0]).Value = false;
            }
            //获取界面上的数据源
            foreach (DataGridViewRow dgvr in dgvOrderButtInfo.Rows)
            {
                dataList.Add((OrderButtDto)dgvr.DataBoundItem);

            }

            //改变数据库中的值
            foreach (OrderButtDto info in dataList)
            {
                dao.ChangeFlagNot(info);
            }

        }

        //双击修改事件
        private void dgvOrderButtInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex!=18 && e.ColumnIndex != 24 && e.ColumnIndex != 25 )
            {
                dgvOrderButtInfo.Columns[e.ColumnIndex].ReadOnly = true;
                return;
            }
            //批卡备注
            if (e.ColumnIndex == 24 && e.RowIndex != -1)
            {
                dgvOrderButtInfo.Columns[e.ColumnIndex].ReadOnly = false;
                DataGridViewCell cell = dgvOrderButtInfo.Rows[e.RowIndex].Cells["Column22"];
                dgvOrderButtInfo.CurrentCell = cell;
                dgvOrderButtInfo.BeginEdit(true);
                dgvOrderButtInfo.CellValueChanged += new DataGridViewCellEventHandler(dgvOrderButtInfo_CellValueChanged);

            }
            //审批备注
            if (e.ColumnIndex == 25 && e.RowIndex != -1)
            {
                dgvOrderButtInfo.Columns[e.ColumnIndex].ReadOnly = false;
                DataGridViewCell cell = dgvOrderButtInfo.Rows[e.RowIndex].Cells["Column23"];
                dgvOrderButtInfo.CurrentCell = cell;
                dgvOrderButtInfo.BeginEdit(true);
                dgvOrderButtInfo.CellValueChanged += new DataGridViewCellEventHandler(dgvOrderButtInfo_CellValueChanged);

            }
            if (e.ColumnIndex == 18&& e.RowIndex != -1) {
                OrderButtDto info = dgvOrderButtInfo.CurrentRow.DataBoundItem as OrderButtDto;
                StockForm stock = new StockForm();
                stock.orderinfo = info;
                stock.Text = "库存信息";
                stock.stateUI = 0;
                stock.ShowDialog();
                //监测子窗体关闭后 库存此单元格不能修改库存数据
                if (stock.DialogResult == DialogResult.OK)
                {
                    dgvOrderButtInfo.Columns[e.ColumnIndex].ReadOnly = true;
                }

            }

        }

        private void dgvOrderButtInfo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 24 && e.ColumnIndex != 25) return;

            dgvOrderButtInfo.CurrentCell.Style.BackColor = Color.Purple;

            BtnSubmit.Enabled = false;
        }
        //编辑完成事件
        private void dgvOrderButtInfo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SPRetInfo ret = new SPRetInfo();
            OrderButtDto info = dgvOrderButtInfo.CurrentRow.DataBoundItem as OrderButtDto;
            //批卡备注
            if (e.ColumnIndex == 24 && e.RowIndex != -1)
            {
                if (!string.IsNullOrEmpty(info.Pmark))
                {
                    dao.UpdatePmark(int.Parse(info.Batchid), int.Parse(info.ExcelSeqid), info.Pmark, ret);
                    if (ret.num != "1")
                    {
                        MessageBox.Show(ret.msg, "后台提示！");
                    }
                }
                else
                {
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
            if (e.ColumnIndex == 25 && e.RowIndex != -1)
            {
                if (!string.IsNullOrEmpty(info.Amark))
                {
                    dao.UpdateAmark(int.Parse(info.Batchid), int.Parse(info.ExcelSeqid), info.Amark, ret);
                    if (ret.num != "1")
                    {
                        MessageBox.Show(ret.msg, "后台提示！");
                    }
                }
                else
                {
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
        //勾选事件
        private void dgvOrderButtInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                dgvOrderButtInfo.Columns[e.ColumnIndex].ReadOnly = true;
                return;
            }
            //选中单选框 触发事件
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                //获取控件的值
                //MessageBox.Show(this.dgvGenCmsbill.Rows[e.RowIndex].Cells[0].EditedFormattedValue.ToString());
                string chooseFlag = this.dgvOrderButtInfo.Rows[e.RowIndex].Cells[0].EditedFormattedValue.ToString().Trim();
                if (chooseFlag.Equals("True"))
                {
                    //选中
                    OrderButtDto info = dgvOrderButtInfo.CurrentRow.DataBoundItem as OrderButtDto;
                    dao.ChangeFlag(info);
                }
                else
                {
                    //取消选中
                    OrderButtDto info = dgvOrderButtInfo.CurrentRow.DataBoundItem as OrderButtDto;
                    dao.ChangeFlagNot(info);
                }
            }
        }
        //Excel导出
        private void BtnExcImprot_Click(object sender, EventArgs e)
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
            DataTable dt = GetDgvToTable(dgvOrderButtInfo);
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
            DataTable t = dt.DefaultView.ToTable("dt", true, "接口来源", "订单ID", "下单时间", "客户CMS码", "客户名称", "商品CMS码", "商品名称", "规格", "厂家", "计划单价", "计划数量", "库存数", "满足效期库存数", "批卡备注", "审批备注", "CMS地址码", "送货地址", "商品大类", "公开销价", "检查状态", "检查信息", "导入状态", "导入结果", "客户留言");

            return t;
        }
        //作废
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            //1.查询此表 更新勾选数据的chooseFlag
            BtnQuery_Click(sender, e);
            //获取界面的数据源
            List<OrderButtDto> dataLists = new List<OrderButtDto>();
            OrderButtDto ix = new OrderButtDto();
            dataLists.Clear();
            //获取界面上的数据源
            foreach (DataGridViewRow dgvr in dgvOrderButtInfo.Rows)
            {
                dataLists.Add((OrderButtDto)dgvr.DataBoundItem);
            }
            if (dataLists.Count <= 0) return;
            //判断是否选中
            foreach (OrderButtDto infos in dataLists)
            {
                //选中
                if (infos.ChooseFlag == "1")
                {
                    if (infos.ImportState != "成功") {
                        //2.更新字段ImportState
                        dao.CanCelOrderButt(infos);
                    }
                    
                }

            }
            //3.再次查询此表
            BtnQuery_Click(sender, e);
            //颜色判定
            Color c1 = Color.FromArgb(250, 191, 143);
            for (int i = 0; i < dgvOrderButtInfo.Rows.Count; i++)
            {
                if (dgvOrderButtInfo.Rows[i].Cells["Column12"].Value.ToString() != dgvOrderButtInfo.Rows[i].Cells["Column14"].Value.ToString())
                {
                    dgvOrderButtInfo.Rows[i].Cells["Column12"].Style.BackColor = c1;
                }
            }

        }
        //取消作废
        private void BtnNotCancel_Click(object sender, EventArgs e)
        {
            //1.查询此表 更新勾选数据的chooseFlag
            BtnQuery_Click(sender, e);
            //获取界面的数据源
            List<OrderButtDto> dataLists = new List<OrderButtDto>();
            OrderButtDto ix = new OrderButtDto();
            dataLists.Clear();
            //获取界面上的数据源
            foreach (DataGridViewRow dgvr in dgvOrderButtInfo.Rows)
            {
                dataLists.Add((OrderButtDto)dgvr.DataBoundItem);
            }
            if (dataLists.Count <= 0) return;
            //判断是否选中
            foreach (OrderButtDto infos in dataLists)
            {
                //选中
                if (infos.ChooseFlag == "1")
                {
                    if (infos.ImportState == "作废")
                    {
                        //2.更新字段ImportState
                        dao.NotCanCelOrderButt(infos);
                    }

                }

            }
            //3.再次查询此表
            BtnQuery_Click(sender, e);
            //颜色判定
            Color c1 = Color.FromArgb(250, 191, 143);
            for (int i = 0; i < dgvOrderButtInfo.Rows.Count; i++)
            {
                if (dgvOrderButtInfo.Rows[i].Cells["Column12"].Value.ToString() != dgvOrderButtInfo.Rows[i].Cells["Column14"].Value.ToString())
                {
                    dgvOrderButtInfo.Rows[i].Cells["Column12"].Style.BackColor = c1;
                }
            }

        }

    }
}

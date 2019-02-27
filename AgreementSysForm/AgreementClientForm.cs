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
    public partial class AgreementClientForm : DockContent
    {
        APDao_Agreement dao = new APDao_Agreement();
        AgreeClient agreeClientInfo = new AgreeClient();

        //操作临时表，用于存储过程
        SortableBindingList<DelTemp> DelClientList = new SortableBindingList<DelTemp>();

        //客户名称Excel数据
        SortableBindingList<CstNameMatch> ExcelList = new SortableBindingList<CstNameMatch>();
        //写入临时表数据
        SortableBindingList<CstNameMatch> TempList = new SortableBindingList<CstNameMatch>();

        string path = "";
        SPRetInfo retInfo = new SPRetInfo();

        //下拉框
        List<string> AgreeLevel = new List<string>();
        Dictionary<string, string> agreeLevelDic = new Dictionary<string, string>();

        List<string> Buyername = new List<string>();
        Dictionary<string, string> buyernameDic = new Dictionary<string, string>();

        List<string> Manager = new List<string>();
        Dictionary<string, string> managerDic = new Dictionary<string, string>();

        SortableBindingList<AgreeClient> infolist = new SortableBindingList<AgreeClient>();
        public AgreementClientForm()
        {
            InitializeComponent();
            dgvAgreeClient.AutoGenerateColumns = false;
        }

        private void AgreementClientForm_Load(object sender, EventArgs e)
        {
            DateTime dt = System.DateTime.Now;
            string ys = dt.ToString("yyyy");
            txtYearNum.Text = ys;

            //加载下拉框
            string typecode = "AGREELEVEL";
            agreeLevelDic = dao.getCbContent(typecode);
            // 遍历字典中的值
            foreach (var item in agreeLevelDic.Values)
            {
                AgreeLevel.Add(item);

            }
            this.cBAgreeLevel.DataSource = AgreeLevel;
            string typecode1 = "BUYERNAME";
            buyernameDic = dao.getCbContent(typecode1);
            // 遍历字典中的值
            foreach (var item in buyernameDic.Values)
            {
                Buyername.Add(item);

            }
            this.cBBuyerName.DataSource = Buyername;
            string typecode2 = "MANAGER";
            managerDic = dao.getCbContent(typecode2);
            // 遍历字典中的值
            foreach (var item in managerDic.Values)
            {
                Manager.Add(item);

            }
            this.cBManager.DataSource = Manager;
        }
        private void clearUI() {
            infolist.Clear();
            ExcelList.Clear();
            TempList.Clear();

        }
        //查询
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            clearUI();
            int i = 0;
            //获取下拉框的值
            string agreeLevel = cBAgreeLevel.SelectedItem.ToString().Trim();
            string agreeLevelKey = "";

            string buyerName = cBBuyerName.SelectedItem.ToString().Trim();
            string buyerNameKey = "";

            string manager = cBManager.SelectedItem.ToString().Trim();
            string managerKey = "";

            foreach (string key in agreeLevelDic.Keys) {
                if (agreeLevelDic[key].Equals(agreeLevel)) {
                    agreeLevelKey = key;
                    break;
                }
            }

            foreach (string key in buyernameDic.Keys)
            {
                if (buyernameDic[key].Equals(buyerName))
                {
                    buyerNameKey = key;
                    break;
                }
            }

            foreach (string key in managerDic.Keys)
            {
                if (managerDic[key].Equals(manager))
                {
                    managerKey = key;
                    break;
                }
            }
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            if (!StringUtils.IsNull(agreeLevel)) {
                sqlkeydict.Add("agreelevel", agreeLevelKey);
                i++;
            }
            if (!StringUtils.IsNull(buyerName))
            {
                sqlkeydict.Add("buyername", buyerNameKey);
                i++;
            }
            if (!StringUtils.IsNull(manager))
            {
                sqlkeydict.Add("manager", managerKey);
                i++;
            }
            if (!StringUtils.IsNull(txtYearNum.Text)) {
                sqlkeydict.Add("yearnum", txtYearNum.Text.ToString().Trim());
                i++;
            }
            if (!StringUtils.IsNull(txtSaller.Text))
            {
                sqlkeydict.Add("saller%", "%"+txtSaller.Text.ToString().Trim()+"%");
                i++;
            }
            if (!StringUtils.IsNull(txtProdName.Text))
            {
                sqlkeydict.Add("prod_name%", "%"+txtProdName.Text.ToString().Trim()+"%");
                i++;
            }
            if (!StringUtils.IsNull(txtCstCode.Text))
            {
                sqlkeydict.Add("cstcode", txtCstCode.Text.ToString().Trim());
                i++;
            }
            if (!StringUtils.IsNull(txtCstName.Text))
            {
                sqlkeydict.Add("cstname%", "%"+txtCstName.Text.ToString().Trim()+"%");
                i++;
            }
            if (i <= 0)
            {
                MessageBox.Show("至少输入一个查询条件！", "前台提示");
                this.Cursor = Cursors.Default;
                return;
            }
            else {
                infolist=dao.GetAgreeClientInfo(sqlkeydict);
                if (infolist.Count <= 0)
                {
                    MessageBox.Show("未查询到数据！", "程序提示");
                    dgvAgreeClient.DataSource = null;
                    //dgvAgreementInfo.Refresh();
                    this.Cursor = Cursors.Default;
                    return;
                }
                else
                {
                    dgvAgreeClient.DataSource = infolist;
                    dgvAgreeClient.Refresh();
                    dgvAgreeClient.CurrentCell = null;
                    this.Cursor = Cursors.Default;
                    BtnImport.Enabled = true;
                }

            }

        }
        //删除
        private void BtnDel_Click(object sender, EventArgs e)
        {
            //取得选中的行
            object obj2 = FormUtils.SelectRows(dgvAgreeClient);
            if (obj2 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            SortableBindingList<AgreeClient> agreeClientList = new SortableBindingList<AgreeClient>();

            agreeClientList.Clear();
            foreach (DataGridViewRow dgvr in row)
            {
                agreeClientList.Add((AgreeClient)dgvr.Cells[0].Value);
            }
            //读取agreeClientList集合中的Prodid
            foreach (AgreeClient info in agreeClientList)
            {
                DelTemp deleteInfo = new DelTemp();
                deleteInfo.RelateId = info.AgreementId;

                DelClientList.Add(deleteInfo);
            }
            SPRetInfo ret = new SPRetInfo();
            //提示确认是否删除
            DialogResult result = MessageBox.Show("确认删除？", "温馨提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                dao.DelClient(DelClientList, ret);
                if (ret.num == "1")
                {
                    MessageBox.Show(ret.msg + "|" + ret.result, "后台提示！");
                }
                else
                {
                    MessageBox.Show(ret.msg + "|" + ret.result, "后台提示！");
                    return;
                }
                DelClientList.Clear();
                BtnQuery_Click(sender, e);
            }
            else
            {
                MessageBox.Show("您选择了取消删除！");
            }
        }
        //新增
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddOrUpdateAgrClientForm addAgrClient = new AddOrUpdateAgrClientForm();
            addAgrClient.Tag = dao;
            addAgrClient.stateUI = 0;//传值为0，界面为新增页面
            addAgrClient.ShowDialog();
            addAgrClient.agrClientInfo = agreeClientInfo;
            if (addAgrClient.DialogResult == DialogResult.OK)
            {
                BtnQuery_Click(sender, e);
            }
        }
        //双击修改
        private void dgvAgreeClient_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AgreeClient info = dgvAgreeClient.CurrentRow.DataBoundItem as AgreeClient;

            AddOrUpdateAgrClientForm updateAgrClient = new AddOrUpdateAgrClientForm();
            updateAgrClient.agrClientInfo = info;
            updateAgrClient.Tag = dao;
            updateAgrClient.stateUI = 1;//传值为1，界面为修改页面
            updateAgrClient.ShowDialog();
            if (updateAgrClient.DialogResult == DialogResult.OK)
            {
                BtnQuery_Click(sender, e);
            }
        }
        //批量修改
        private void BtnBatchUpdate_Click(object sender, EventArgs e)
        {
            Boolean flag = true;
            AgreeProducerInfo batchUpInfo = new AgreeProducerInfo();
            //取得选中的行
            object obj2 = FormUtils.SelectRows(dgvAgreeClient);
            if (obj2 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            SortableBindingList<AgreeClient> agreeClientList = new SortableBindingList<AgreeClient>();

            agreeClientList.Clear();
            foreach (DataGridViewRow dgvr in row)
            {
                agreeClientList.Add((AgreeClient)dgvr.Cells[0].Value);
            }
            if (agreeClientList.Count <= 1) {
                MessageBox.Show("批量修改至少需要选择两项！", "前台提示");
                return;
            }
            //读取agreeClientList集合中的 供应商ID是否一致 
            string symbol = agreeClientList[0].ProdId;
            for (int i = 0; i < agreeClientList.Count; i++) {
                if (symbol != agreeClientList[i].ProdId)
                {
                    MessageBox.Show("选择批量修改的数据--供应商ID不一致！", "前台提示");
                    flag = false;
                    break;
                }
                else {
                    continue;
                }

            }
            if (flag == true)
            {
                //读取集合中的字段
                foreach (AgreeClient info in agreeClientList)
                {
                    batchUpInfo.ProdCode = info.ProdCode;
                    batchUpInfo.ProdName = info.ProdName;
                    batchUpInfo.Import = info.Import;
                    batchUpInfo.BuyerName = info.BuyerName;
                    batchUpInfo.Manager = info.Manager;
                    batchUpInfo.MiddleMan = info.MiddleMan;
                    batchUpInfo.AgreeType = info.AgreeType;
                    batchUpInfo.BeginDate = info.BeginDate;
                }

                AddOrUpdateAgrProdForm BatchupdateAgrProd = new AddOrUpdateAgrProdForm();
                BatchupdateAgrProd.agrProdInfo = batchUpInfo;
                BatchupdateAgrProd.BatchList = agreeClientList;
                BatchupdateAgrProd.Tag = dao;
                BatchupdateAgrProd.stateUI = 2;//传值为2，界面为批量修改页面
                BatchupdateAgrProd.ShowDialog();
                if (BatchupdateAgrProd.DialogResult == DialogResult.OK)
                {
                    BtnQuery_Click(sender, e);
                }
            }
              
        }
        //Excel导入
        private void BtnExcel_Click(object sender, EventArgs e)
        {
            ClientInfoXlsForm importXsltEmpForm = new ClientInfoXlsForm();
            importXsltEmpForm.Tag = dao;
            importXsltEmpForm.Text = "Excel导入";
            importXsltEmpForm.ShowDialog();

        }

        private void BtnMatch_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "订单文件|*.xlsx;*.xls|All files (*.*)|*.*"; //过滤文件类型
            fd.ShowReadOnly = true; //设定文件是否只读
            DialogResult r = fd.ShowDialog();
            if (r != DialogResult.OK)
            {               
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            clearUI();
            path = Path.GetDirectoryName(fd.FileName);
            //System.DateTime currentTime = new System.DateTime();
            //currentTime = System.DateTime.Now;
            //int bs = currentTime.Second;

            //---------------------------------------------------

            //第一步 处理excel表-------------------
            ExcelList = ExcelHelper.OpenInquiryExcelCstName(fd.FileName);
            if (ExcelList.Count <= 0) return;
            foreach (CstNameMatch info in ExcelList)
            {
                info.Empid = SessionDto.Empid;
                info.Ownerid = Properties.Settings.Default.OWNERID;
                info.Compid = Properties.Settings.Default.COMPID;
                info.SaleDeptid = SessionDto.Empdeptid;
                TempList.Add(info);
            }
            //第二步  处理带出两个字段 成功就导出
            dao.DealWithMatch(TempList,retInfo);
            if (retInfo.num == "1")
            {
                //第三步  导出excel
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
                try
                {
                    // 1.获取数据集合
                    SortableBindingList<CstNameMatch> enlist = new SortableBindingList<CstNameMatch>() { };
                    enlist.Clear();
                    enlist = dao.GetCstNameList(retInfo.result);

                    // 2.设置单元格抬头
                    // key：实体对象属性名称，可通过反射获取值
                    // value：Excel列的名称
                    Dictionary<string, string> cellheader = new Dictionary<string, string> {
                    { "ExcelSeqid", "序号" },
                    { "CstName", "客户名称" },
                    { "CstCode", "客户编码" },
                    { "CheckMsg", "导出说明" }
         
                 };
                    // 3.进行Excel转换操作
                    string urlPath = ExcelHelper.EntityListToExcel2003(cellheader, enlist, "导出excel", fileNameString);
                    System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                    MessageBox.Show("导出成功！");
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else {
                MessageBox.Show(retInfo.msg, "后台提示");
                this.Cursor = Cursors.Default;
                return;
            }
        }
        //Excel导出
        private void BtnImport_Click(object sender, EventArgs e)
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
            DataTable dt = GetDgvToTable(dgvAgreeClient);
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
            DataTable t = dt.DefaultView.ToTable("dt", true, "年份", "协议ID", "供应商ID", "供应商编号", "商务团队", "厂家性质", "采购员", "采购经理", "协议性质", "销售对接人", "销售副总", "销售经理", "销售代表", "协议级别", "客户编码", "客户名称", "去年销售额", "去年对应上游", "今年厂家预测量（万元）", "目标分级", "协议启动时间", "客户意向", "厂家意向","备注","签约动态", "最终签约渠道", "今年产生销售额", "意向协议量（万元）", "是否盖章", "协议存档", "最终协议量（万元）", "销售最近修改时间");

            return t;
        }


    }
}

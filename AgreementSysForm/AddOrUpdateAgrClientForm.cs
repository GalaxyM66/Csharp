using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PriceManager
{
    public partial class AddOrUpdateAgrClientForm : Form
    {
        public int stateUI = 0;

        public AgreeClient agrClientInfo;
        public AgreeProducerInfo agreeProdInfo;
        public SearchClientInfo clientInfo;
        APDao_Agreement dao = null;
        PMSystemDao Pdao = new PMSystemDao();
        SPRetInfo retinfo = new SPRetInfo();

        string empName = "";

        string typecode = "";

        string prodIdAdd = "";
        string cstidAdd = "";

        string prodCode = "";
        string prodName = "";
        string importKey = "";
        string buyerNameKey ="";
        string managerKey = "";
        string agreeTypeKey = "";
        string middleMan = "";
        string beginTime = "";
        string yearNum ="";
        string agreeLevelKey ="";
        string cstCode = "";
        string cstName = "";
        string lastValues = "";
        string lastUpStreamKey = "";
        string foreCastValues = "";
        string tarGetKey = "";
        string sallLeader = "";
        string sallManager = "";
        string saller = "";

       Dictionary<string, string> importDic = new Dictionary<string, string>();
        List<string> importList = new List<string>();

        Dictionary<string, string> buyernameDic = new Dictionary<string, string>();
        List<string> buyerNameList = new List<string>();

        Dictionary<string, string> managerDic = new Dictionary<string, string>();
        List<string> managerList = new List<string>();

        Dictionary<string, string> agreeTypeDic = new Dictionary<string, string>();
        List<string> agreeTypeList = new List<string>();

        Dictionary<string, string> agreeLevelDic = new Dictionary<string, string>();
        List<string> agreeLevelList = new List<string>();

        Dictionary<string, string> lastUpStreamDic = new Dictionary<string, string>();
        List<string> lastUpStreamList = new List<string>();

        Dictionary<string, string> TarGetDic = new Dictionary<string, string>();
        List<string> TarGetList = new List<string>();

        public AddOrUpdateAgrClientForm()
        {
            InitializeComponent();
        }
        //加载
        private void AddOrUpdateAgrClientForm_Load(object sender, EventArgs e)
        {
            dao = (APDao_Agreement)this.Tag;

            //厂家性质下拉框加载
            typecode = "IMPORT";
            importDic = dao.getCbContent(typecode);
            importList = StringUtils.GetValue(importDic);
            this.cBImport.DataSource = importList;

            //采购员下拉框加载
            typecode = "BUYERNAME";
            buyernameDic = dao.getCbContent(typecode);
            buyerNameList = StringUtils.GetValue(buyernameDic);
            this.cBBuyerName.DataSource = buyerNameList;

            //采购经理下拉框加载
            typecode = "MANAGER";
            managerDic = dao.getCbContent(typecode);
            managerList = StringUtils.GetValue(managerDic);
            this.cBManager.DataSource = managerList;

            //协议性质下拉框加载
            typecode = "AGREETYPE";
            agreeTypeDic = dao.getCbContent(typecode);
            agreeTypeList = StringUtils.GetValue(agreeTypeDic);
            this.cBAgreeType.DataSource = agreeTypeList;

            //协议级别下拉框加载
            typecode = "AGREELEVEL";
            agreeLevelDic = dao.getCbContent(typecode);
            agreeLevelList = StringUtils.GetValue(agreeLevelDic);
            this.cBAgreeLevel.DataSource = agreeLevelList;

            //去年对应上游下拉框加载
            typecode = "UPSTREAM";
            lastUpStreamDic = dao.getCbContent(typecode);
            lastUpStreamList = StringUtils.GetValue(lastUpStreamDic);
            this.cBLastUpStream.DataSource = lastUpStreamList;

            //目标分级下拉框加载
            typecode = "TARGET";
            TarGetDic = dao.getCbContent(typecode);
            TarGetList = StringUtils.GetValue(TarGetDic);
            this.cBTarGet.DataSource = TarGetList;

            initUI(stateUI);
        }
        //判断是新增还是修改界面
        private void initUI(int astate)
        {
            switch (astate)
            {
                case 0://新增界面 
                    BtnUpdate.Visible = false;
                    BtnAdd.Visible = true;
                    this.Text = "新增";
                    label1.ForeColor = Color.Red;
                    label2.ForeColor = Color.Red;
                    label3.ForeColor = Color.Red;
                    label4.ForeColor = Color.Red;
                    label5.ForeColor = Color.Red;
                    label6.ForeColor = Color.Red;
                    label7.ForeColor = Color.Red;
                    label8.ForeColor = Color.Red;
                    label9.ForeColor = Color.Red;
                    label10.ForeColor = Color.Red;
                    label12.ForeColor = Color.Red;
                    label13.ForeColor = Color.Red;
                    label14.ForeColor = Color.Red;
                    label16.ForeColor = Color.Red;
                    break;

                case 1://修改界面
                    BtnAdd.Visible = false;
                    BtnUpdate.Visible = true;
                    this.Text = "修改";
                    //修改界面需要将原始信息传送到时间控件
                    DateBegin.Text = agrClientInfo.BeginDate;
                    //修改界面需要将原始信息传送到文本框
                    txtProdCode.Text = agrClientInfo.ProdCode;
                    txtProdName.Text = agrClientInfo.ProdName;
                    txtMiddleMan.Text = agrClientInfo.MiddleMan;
                    txtYearNum.Text = agrClientInfo.YearNum;
                    txtCstCode.Text = agrClientInfo.CstCode;
                    txtCstName.Text = agrClientInfo.CstName;
                    txtLastValues.Text = agrClientInfo.LastValues;
                    txtForecastValues.Text = agrClientInfo.ForecastValues;
                    txtSallLeader.Text = agrClientInfo.SallLeader;
                    txtSallManager.Text = agrClientInfo.SallManager;
                    txtSaller.Text = agrClientInfo.Saller;
                    //修改界面需要将原始信息传送到下拉框
                    int sltIndex = 0;
                    for (int x = 0; x < importList.Count; x++)
                    {
                        if (cBImport.Items[x].ToString() == agrClientInfo.Import)
                        {
                            sltIndex = x;
                            break;
                        }
                    }
                    cBImport.SelectedIndex = sltIndex;

                    for (int x = 0; x < buyerNameList.Count; x++)
                    {
                        if (cBBuyerName.Items[x].ToString() == agrClientInfo.BuyerName)
                        {
                            sltIndex = x;
                            break;
                        }
                    }
                    cBBuyerName.SelectedIndex = sltIndex;

                    for (int x = 0; x < managerList.Count; x++)
                    {
                        if (cBManager.Items[x].ToString() == agrClientInfo.Manager)
                        {
                            sltIndex = x;
                            break;
                        }
                    }
                    cBManager.SelectedIndex = sltIndex;

                    for (int x = 0; x < agreeTypeList.Count; x++)
                    {
                        if (cBAgreeType.Items[x].ToString() == agrClientInfo.AgreeType)
                        {
                            sltIndex = x;
                            break;
                        }
                    }
                    cBAgreeType.SelectedIndex = sltIndex;

                    for (int x = 0; x < agreeLevelList.Count; x++)
                    {
                        if (cBAgreeLevel.Items[x].ToString() == agrClientInfo.AgreeLevel)
                        {
                            sltIndex = x;
                            break;
                        }
                    }
                    cBAgreeLevel.SelectedIndex = sltIndex;

                    for (int x = 0; x < lastUpStreamList.Count; x++)
                    {
                        if (cBLastUpStream.Items[x].ToString() == agrClientInfo.LastUpStream)
                        {
                            sltIndex = x;
                            break;
                        }
                    }
                    cBLastUpStream.SelectedIndex = sltIndex;

                    for (int x = 0; x < TarGetList.Count; x++)
                    {
                        if (cBTarGet.Items[x].ToString() == agrClientInfo.TarGet)
                        {
                            sltIndex = x;
                            break;
                        }
                    }
                    cBTarGet.SelectedIndex = sltIndex;

                    break;
            }
        }
        #region 供应商编码文本框点击事件 
        private void txtProdCode_Click(object sender, EventArgs e)
        {
            SearchProdForm selForm = new SearchProdForm();
            //注册事件
            selForm.TransfEvent += selForm_TransfEvent;
            selForm.ShowDialog();
        }
        //事件处理方法
        void selForm_TransfEvent(AgreeProducerInfo agreeProdInfo)
        {
            //将子界面的值接受过来进行赋值
            txtProdCode.Text = agreeProdInfo.ProdCode;
            txtProdName.Text = agreeProdInfo.ProdName;
            txtMiddleMan.Text = agreeProdInfo.MiddleMan;

            //传送到时间控件
            DateBegin.Text = agreeProdInfo.BeginDate;

            //传送到下拉框
            int sltIndex = 0;
            for (int x = 0; x < importList.Count; x++)
            {
                if (cBImport.Items[x].ToString() == agreeProdInfo.Import)
                {
                    sltIndex = x;
                    break;
                }
            }
            cBImport.SelectedIndex = sltIndex;

            for (int x = 0; x < buyerNameList.Count; x++)
            {
                if (cBBuyerName.Items[x].ToString() == agreeProdInfo.BuyerName)
                {
                    sltIndex = x;
                    break;
                }
            }
            cBBuyerName.SelectedIndex = sltIndex;

            for (int x = 0; x < managerList.Count; x++)
            {
                if (cBManager.Items[x].ToString() == agreeProdInfo.Manager)
                {
                    sltIndex = x;
                    break;
                }
            }
            cBManager.SelectedIndex = sltIndex;

            for (int x = 0; x < agreeTypeList.Count; x++)
            {
                if (cBAgreeType.Items[x].ToString() == agreeProdInfo.AgreeType)
                {
                    sltIndex = x;
                    break;
                }
            }
            cBAgreeType.SelectedIndex = sltIndex;
            prodIdAdd = agreeProdInfo.ProdId;
        }
        #endregion
        #region 客户编码文本框点击事件
        private void txtCstCode_Click(object sender, EventArgs e)
        {
            SearchClientForm sel = new SearchClientForm();
            //注册事件
            sel.TrandfEvent += sel_TransEvent;
            sel.ShowDialog();

        }
        void sel_TransEvent(SearchClientInfo info) {
            txtCstCode.Text = info.CstCode;
            txtCstName.Text = info.CstName;
            txtSallLeader.Text = info.SallLeader;
            txtSallManager.Text = info.SallManager;
            txtSaller.Text = info.Saller;
            cstidAdd = info.CstId;
        }

        #endregion
        //新增事件
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (StringUtils.IsNull(cstidAdd)) {
                cstidAdd = "-1";
            }
            
            if (!StringUtils.IsNull(txtProdCode.Text)) {

                prodCode = txtProdCode.Text.ToString().Trim();
                prodName = txtProdName.Text.ToString().Trim();
                string import = cBImport.SelectedItem.ToString().Trim();
                importKey = StringUtils.GetKey(import, importDic);
                string buyerName = cBBuyerName.SelectedItem.ToString().Trim();
                buyerNameKey = StringUtils.GetKey(buyerName, buyernameDic);
                string manager = cBManager.SelectedItem.ToString().Trim();
                managerKey = StringUtils.GetKey(manager, managerDic);
                string agreeType = cBAgreeType.SelectedItem.ToString().Trim();
                agreeTypeKey = StringUtils.GetKey(agreeType, agreeTypeDic);
                middleMan = txtMiddleMan.Text.ToString().Trim();
                beginTime = DateBegin.Value.ToString("yyyyMMdd");
            }
            else
            {
                MessageBox.Show("供应商编码为必填项！", "前台提示");
                txtCstCode.Focus();
                return;
            }
            if (!StringUtils.IsNull(txtYearNum.Text))
            {
                yearNum = txtYearNum.Text.ToString().Trim();
            }
            else {
                MessageBox.Show("年份为必填项！", "前台提示");
                txtYearNum.Focus();
                return;
            }
            string agreeLevel = cBAgreeLevel.SelectedItem.ToString().Trim();
            agreeLevelKey = StringUtils.GetKey(agreeLevel, agreeLevelDic);
            if (!StringUtils.IsNull(txtCstCode.Text)) {
                cstCode = txtCstCode.Text.ToString().Trim();
            }
            else {
                cstCode = "-1";
                }
            if (!StringUtils.IsNull(txtCstName.Text))
            {
                cstName = txtCstName.Text.ToString().Trim();
            }
            else {
                MessageBox.Show("客户名称为必填项！", "前台提示");
                txtCstName.Focus();
                return;
            }
            if (!StringUtils.IsNull(txtLastValues.Text))
            {
                lastValues = txtLastValues.Text.ToString().Trim();
            }
            else
            {
                lastValues = "-1";
            }
            string lastUpStream = cBLastUpStream.SelectedItem.ToString().Trim();
            lastUpStreamKey = StringUtils.GetKey(lastUpStream, lastUpStreamDic);
            if (!StringUtils.IsNull(txtForecastValues.Text))
            {
                foreCastValues = txtForecastValues.Text.ToString().Trim();
            }
            else
            {
                foreCastValues = "-1";
            }
            string tarGet = cBTarGet.SelectedItem.ToString().Trim();
            tarGetKey = StringUtils.GetKey(tarGet, TarGetDic);
            if (!StringUtils.IsNull(txtSallLeader.Text))
            {
                sallLeader = txtSallLeader.Text.ToString().Trim();
            }
            else
            {
                sallLeader = "-1";
            }
            if (!StringUtils.IsNull(txtSallManager.Text))
            {
                sallManager = txtSallManager.Text.ToString().Trim();
            }
            else
            {
                sallManager = "-1";
            }
            if (!StringUtils.IsNull(txtSaller.Text))
            {
                saller = txtSaller.Text.ToString().Trim();
            }
            else
            {
                saller = "-1";
            }
            dao.AddAgrClient(prodIdAdd, prodCode,prodName,importKey,buyerNameKey,managerKey,middleMan,agreeTypeKey,yearNum,cstidAdd,cstCode,cstName,saller,sallManager,sallLeader,agreeLevelKey,lastValues,foreCastValues,lastUpStreamKey,tarGetKey,beginTime,retinfo);
            if (retinfo.num == "1")
            {
                MessageBox.Show(retinfo.msg + "|后台提示");
                return;

            }
            else
            {
                MessageBox.Show(retinfo.msg + "|后台提示");
                return;
            }
        }
        //修改事件
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string AgreeId = agrClientInfo.AgreementId;
            string prodId = agrClientInfo.ProdId;
            string cstId = agrClientInfo.CstId;
            if (StringUtils.IsNull(cstId)) {
                cstId = "-1";
            }
            if (!StringUtils.IsNull(txtProdCode.Text))
            {

                prodCode = txtProdCode.Text.ToString().Trim();
                prodName = txtProdName.Text.ToString().Trim();

               string import = cBImport.SelectedItem.ToString().Trim();
               importKey=StringUtils.GetKey(import,importDic);
               string buyerName = cBBuyerName.SelectedItem.ToString().Trim();
               buyerNameKey = StringUtils.GetKey(buyerName, buyernameDic);
               string manager = cBManager.SelectedItem.ToString().Trim();
               managerKey = StringUtils.GetKey(manager, managerDic);
               string agreeType = cBAgreeType.SelectedItem.ToString().Trim();
               agreeTypeKey = StringUtils.GetKey(agreeType, agreeTypeDic);

                middleMan = txtMiddleMan.Text.ToString().Trim();
                beginTime = DateBegin.Value.ToString("yyyyMMdd");
            }
            else
            {
                MessageBox.Show("供应商编码为必填项！", "前台提示");
                txtCstCode.Focus();
                return;
            }
            if (!StringUtils.IsNull(txtYearNum.Text))
            {
                yearNum = txtYearNum.Text.ToString().Trim();
            }
            else
            {
                MessageBox.Show("年份为必填项！", "前台提示");
                txtYearNum.Focus();
                return;
            }
            string agreeLevel = cBAgreeLevel.SelectedItem.ToString().Trim();
            agreeLevelKey = StringUtils.GetKey(agreeLevel, agreeLevelDic);

            if (!StringUtils.IsNull(txtCstCode.Text))
            {
                cstCode = txtCstCode.Text.ToString().Trim();
            }
            else
            {
                cstCode = "-1";
            }
            if (!StringUtils.IsNull(txtCstName.Text))
            {
                cstName = txtCstName.Text.ToString().Trim();
            }
            else
            {
                MessageBox.Show("客户名称为必填项！", "前台提示");
                txtCstName.Focus();
                return;
            }
            if (!StringUtils.IsNull(txtLastValues.Text))
            {
                lastValues = txtLastValues.Text.ToString().Trim();
            }
            else
            {
                lastValues = "-1";
            }
            string lastUpStream = cBLastUpStream.SelectedItem.ToString().Trim();
            lastUpStreamKey = StringUtils.GetKey(lastUpStream, lastUpStreamDic);

            if (!StringUtils.IsNull(txtForecastValues.Text))
            {
                foreCastValues = txtForecastValues.Text.ToString().Trim();
            }
            else
            {
                foreCastValues = "-1";
            }
            string tarGet = cBTarGet.SelectedItem.ToString().Trim();
            tarGetKey = StringUtils.GetKey(tarGet, TarGetDic);
            if (!StringUtils.IsNull(txtSallLeader.Text))
            {
                sallLeader = txtSallLeader.Text.ToString().Trim();
            }
            else
            {
                sallLeader = "-1";
            }
            if (!StringUtils.IsNull(txtSallManager.Text))
            {
                sallManager = txtSallManager.Text.ToString().Trim();
            }
            else
            {
                sallManager = "-1";
            }
            if (!StringUtils.IsNull(txtSaller.Text))
            {
                saller = txtSaller.Text.ToString().Trim();
            }
            else
            {
                saller = "-1";
            }
            string compid = agrClientInfo.CompId;
            string ownerid = agrClientInfo.OwnerId;
            dao.UpdateAgrClient(AgreeId,prodId, prodCode, prodName, importKey, buyerNameKey, managerKey, middleMan, agreeTypeKey, yearNum, cstId, cstCode, cstName, saller, sallManager, sallLeader, agreeLevel, lastValues, foreCastValues, lastUpStreamKey, tarGetKey, beginTime,compid,ownerid,retinfo);
            if (retinfo.num == "1")
            {
                MessageBox.Show(retinfo.msg + "|后台提示");
                return;

            }
            else
            {
                MessageBox.Show(retinfo.msg + "|后台提示");
                return;
            }
        }
        #region 修改销售副总 销售经理 销售代表
        //委托 事件处理方法
        void selForm_TransfEvent(EmpInfos empInfo)
        {
            empName = empInfo.EmpName;
        }

        private void txtSallLeader_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            empName = "";
            UpdateNameForm un = new UpdateNameForm();
            //注册事件
            un.TrandfEvent += selForm_TransfEvent;
            un.ShowDialog();
            txtSallLeader.Text = empName;

        }

        private void txtSallManager_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            empName = "";
            UpdateNameForm un = new UpdateNameForm();
            //注册事件
            un.TrandfEvent += selForm_TransfEvent;
            un.ShowDialog();
            txtSallManager.Text = empName;
        }

        private void txtSaller_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            empName = "";
            UpdateNameForm un = new UpdateNameForm();
            //注册事件
            un.TrandfEvent += selForm_TransfEvent;
            un.ShowDialog();
            txtSaller.Text = empName;
        }
        #endregion
    }
}

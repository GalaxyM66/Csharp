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
    public partial class AddOrUpdateAgrProdForm : Form
    {
        public int stateUI = 0;

        public AgreeProducerInfo agrProdInfo;
        public SortableBindingList<AgreeClient> BatchList;
        APDao_Agreement dao = null;
        PMSystemDao Pdao = new PMSystemDao();
        SPRetInfo retinfo = new SPRetInfo();
        //
        string typecode = "";
        
        Dictionary<string, string> importDic = new Dictionary<string, string>();
        List<string> importList = new List<string>();

        Dictionary<string, string> agreeTypeDic = new Dictionary<string, string>();
        List<string> agreeTypeList = new List<string>();

        Dictionary<string, string> buyernameDic = new Dictionary<string, string>();
        List<string> buyerNameList = new List<string>();

        Dictionary<string, string> managerDic = new Dictionary<string, string>();
        List<string> managerList = new List<string>();
        public AddOrUpdateAgrProdForm()
        {
            InitializeComponent();
        }

        private void AddOrUpdateAgrProdForm_Load(object sender, EventArgs e)
        {
            dao = (APDao_Agreement)this.Tag;

            //加载下拉框
            //1.厂家性质
            typecode = "IMPORT";
            importDic = dao.getCbContents(typecode);
            importList= StringUtils.GetValue(importDic);
            this.cBImport.DataSource = importList;

            //2.协议性质
            typecode = "AGREETYPE";
            agreeTypeDic = dao.getCbContents(typecode);
            agreeTypeList = StringUtils.GetValue(agreeTypeDic);
            this.cBAgreeType.DataSource = agreeTypeList;

            //3.采购员
            typecode = "BUYERNAME";
            buyernameDic = dao.getCbContents(typecode);
            buyerNameList = StringUtils.GetValue(buyernameDic);
            this.cBBuyerName.DataSource = buyerNameList;

            //4.采购经理
            typecode = "MANAGER";
            managerDic = dao.getCbContents(typecode);
            managerList = StringUtils.GetValue(managerDic);
            this.cBManager.DataSource = managerList;
            initUI(stateUI);
        }
        //判断是新增还是修改
        private void initUI(int astate)
        {
            switch (astate)
            {
                case 0://新增界面
                    BtnUpdate.Visible = false;
                    BtnBatchUpdate.Visible = false;
                    BtnAdd.Visible = true;
                    this.Text = "新增";
                    label1.ForeColor = Color.Red;
                    label2.ForeColor = Color.Red;
                    label3.ForeColor = Color.Red;
                    label4.ForeColor = Color.Red;
                    label5.ForeColor = Color.Red;
                    label6.ForeColor = Color.Red;
                    label7.ForeColor = Color.Red;
                    break;

                case 1://修改界面
                    BtnAdd.Visible = false;
                    BtnBatchUpdate.Visible = false;
                    BtnUpdate.Visible = true;
                    this.Text = "修改";
                    //修改界面需要将原始信息传送到时间控件
                    DateBegin.Text = agrProdInfo.BeginDate;
                    //修改界面需要将原始信息传送到文本框
                    txtProdCode.Text = agrProdInfo.ProdCode;
                    txtProdName.Text = agrProdInfo.ProdName;
                    txtMiddleMan.Text = agrProdInfo.MiddleMan;
                    //修改界面需要将原始信息传送到下拉框
                    int sltIndex = 0;
                    for (int x = 0; x < importList.Count; x++) {
                        if (cBImport.Items[x].ToString() ==agrProdInfo.Import) {
                            sltIndex = x;
                            break;
                        }
                    }
                    cBImport.SelectedIndex = sltIndex;

                    for (int x = 0; x < agreeTypeList.Count; x++)
                    {
                        if (cBAgreeType.Items[x].ToString() == agrProdInfo.AgreeType)
                        {
                            sltIndex = x;
                            break;
                        }
                    }
                    cBAgreeType.SelectedIndex = sltIndex;

                    for (int x = 0; x < buyerNameList.Count; x++)
                    {
                        if (cBBuyerName.Items[x].ToString() == agrProdInfo.BuyerName)
                        {
                            sltIndex = x;
                            break;
                        }
                    }
                    cBBuyerName.SelectedIndex = sltIndex;

                    for (int x = 0; x < managerList.Count; x++)
                    {
                        if (cBManager.Items[x].ToString() == agrProdInfo.Manager)
                        {
                            sltIndex = x;
                            break;
                        }
                    }
                    cBManager.SelectedIndex = sltIndex;


                    //txtTransRate.Text = GencstgoodInfo.Relatid;
                    break;

                case 2://批量修改界面
                    BtnAdd.Visible = false;
                    BtnUpdate.Visible = false;
                    BtnBatchUpdate.Visible = true;
                    this.Text = "批量修改";
                    //修改界面需要将原始信息传送到时间控件
                    DateBegin.Text = agrProdInfo.BeginDate;
                    //修改界面需要将原始信息传送到文本框
                    txtProdCode.Text = agrProdInfo.ProdCode;
                    txtProdName.Text = agrProdInfo.ProdName;
                    txtMiddleMan.Text = agrProdInfo.MiddleMan;
                    //修改界面需要将原始信息传送到下拉框
                    int sltIndexs = 0;
                    for (int x = 0; x < importList.Count; x++)
                    {
                        if (cBImport.Items[x].ToString() == agrProdInfo.Import)
                        {
                            sltIndexs = x;
                            break;
                        }
                    }
                    cBImport.SelectedIndex = sltIndexs;

                    for (int x = 0; x < agreeTypeList.Count; x++)
                    {
                        if (cBAgreeType.Items[x].ToString() == agrProdInfo.AgreeType)
                        {
                            sltIndexs = x;
                            break;
                        }
                    }
                    cBAgreeType.SelectedIndex = sltIndexs;

                    for (int x = 0; x < buyerNameList.Count; x++)
                    {
                        if (cBBuyerName.Items[x].ToString() == agrProdInfo.BuyerName)
                        {
                            sltIndexs = x;
                            break;
                        }
                    }
                    cBBuyerName.SelectedIndex = sltIndexs;

                    for (int x = 0; x < managerList.Count; x++)
                    {
                        if (cBManager.Items[x].ToString() == agrProdInfo.Manager)
                        {
                            sltIndexs = x;
                            break;
                        }
                    }
                    cBManager.SelectedIndex = sltIndexs;

                    //txtTransRate.Text = GencstgoodInfo.Relatid;
                    break;
            }
        }
        //修改
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            //判断前台是否输入信息
            if (String.IsNullOrEmpty(txtProdCode.Text))
            {
                MessageBox.Show("前台提示||请输入供应商编码！");
                txtProdCode.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtProdName.Text))
            {
                MessageBox.Show("前台提示||请输入供应商名称！");
                txtProdName.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtMiddleMan.Text))
            {
                MessageBox.Show("前台提示||请输入销售对接人！");
                txtMiddleMan.Focus();
                return;
            }
            //获取下拉框的值
            string import = cBImport.SelectedItem.ToString().Trim();
            string importKey = "";
            string agreeType = cBAgreeType.SelectedItem.ToString().Trim();
            string agreeTypeKey = "";
            string buyer = cBBuyerName.SelectedItem.ToString().Trim();
            string buyerKey = "";
            string manager = cBManager.SelectedItem.ToString().Trim();
            string managerKey = "";
            //获取对应的key
            importKey = StringUtils.GetKey(import, importDic);
            agreeTypeKey = StringUtils.GetKey(agreeType, agreeTypeDic);
            buyerKey = StringUtils.GetKey(buyer, buyernameDic);
            managerKey = StringUtils.GetKey(manager, managerDic);
            //获取时间
            string begintime = DateBegin.Value.ToString("yyyyMMdd");
            string compid = agrProdInfo.CompId;
            string ownerid = agrProdInfo.OwnerId;
            dao.UpdateAgrProd(agrProdInfo.ProdId, txtProdCode.Text, txtProdName.Text, importKey, buyerKey, managerKey, txtMiddleMan.Text, agreeTypeKey,begintime,compid,ownerid,retinfo);
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
        //新增
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            //判断前台是否输入信息
            if (String.IsNullOrEmpty(txtProdCode.Text))
            {
                MessageBox.Show("前台提示||请输入供应商编码！");
                txtProdCode.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtProdName.Text))
            {
                MessageBox.Show("前台提示||请输入供应商名称！");
                txtProdName.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtMiddleMan.Text))
            {
                MessageBox.Show("前台提示||请输入销售对接人！");
                txtMiddleMan.Focus();
                return;
            }
            //获取下拉框的值
            string import = cBImport.SelectedItem.ToString().Trim();
            string importKey = "";
            string agreeType = cBAgreeType.SelectedItem.ToString().Trim();
            string agreeTypeKey = "";
            string buyer = cBBuyerName.SelectedItem.ToString().Trim();
            string buyerKey = "";
            string manager = cBManager.SelectedItem.ToString().Trim();
            string managerKey = "";
            //获取对应的key
            importKey = StringUtils.GetKey(import, importDic);
            agreeTypeKey = StringUtils.GetKey(agreeType, agreeTypeDic);
            buyerKey = StringUtils.GetKey(buyer, buyernameDic);
            managerKey = StringUtils.GetKey(manager, managerDic);
            //获取时间
            string begintime = DateBegin.Value.ToString("yyyyMMdd");
            dao.AddAgrProd(txtProdCode.Text, txtProdName.Text,importKey, buyerKey, managerKey,txtMiddleMan.Text, agreeTypeKey, begintime,retinfo);
            if (retinfo.num == "1")
            {
                MessageBox.Show( retinfo.msg + "|后台提示");
                return;

            }
            else
            {
                MessageBox.Show(retinfo.msg + "|后台提示");
                return;
            }
        }
        //批量修改
        private void BtnBatchUpdate_Click(object sender, EventArgs e)
        {
            //判断前台是否输入信息
            if (String.IsNullOrEmpty(txtProdCode.Text))
            {
                MessageBox.Show("前台提示||请输入供应商编码！");
                txtProdCode.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtProdName.Text))
            {
                MessageBox.Show("前台提示||请输入供应商名称！");
                txtProdName.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtMiddleMan.Text))
            {
                MessageBox.Show("前台提示||请输入销售对接人！");
                txtMiddleMan.Focus();
                return;
            }
            //获取下拉框的值
            string import = cBImport.SelectedItem.ToString().Trim();
            string importKey = "";
            string agreeType = cBAgreeType.SelectedItem.ToString().Trim();
            string agreeTypeKey = "";
            string buyer = cBBuyerName.SelectedItem.ToString().Trim();
            string buyerKey = "";
            string manager = cBManager.SelectedItem.ToString().Trim();
            string managerKey = "";
            //获取对应的key
            importKey = StringUtils.GetKey(import,importDic);
            agreeTypeKey= StringUtils.GetKey(agreeType, agreeTypeDic);
            buyerKey= StringUtils.GetKey(buyer, buyernameDic);
            managerKey = StringUtils.GetKey(manager, managerDic);
            //获取时间
            string begintime = DateBegin.Value.ToString("yyyyMMdd");
            dao.BatchUpClient(BatchList,txtProdCode.Text, txtProdName.Text, importKey, buyerKey, managerKey, txtMiddleMan.Text, agreeTypeKey, begintime, retinfo);
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
        //监测此窗口关闭 刷新上个界面
        private void AddOrUpdateAgrProdForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}

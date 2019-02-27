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
    public partial class SaleFeedBacksForm : DockContent
    {
        //获取登录用户ID
        string LoginId = SessionDto.Emproleid;

        string empName = "";

        //修改下拉框类型
        string typecodes = "";
        //新增修改的下拉框      
        private ComboBox CstIntention_ComboBoxs = new ComboBox();
        Dictionary<string, string> CstiDic = new Dictionary<string, string>();
        List<string> Csti = new List<string>();

        private ComboBox ProdIntention_ComboBox = new ComboBox();
        Dictionary<string, string> ProdDic = new Dictionary<string, string>();
        List<string> Prod = new List<string>();

        private ComboBox Dynas_ComboBox = new ComboBox();
        Dictionary<string, string> DynaDic = new Dictionary<string, string>();
        List<string> Dyna = new List<string>();

        private ComboBox Fina_ComboBox = new ComboBox();
        Dictionary<string, string> FinaDic = new Dictionary<string, string>();
        List<string> Fina = new List<string>();



        APDao_Agreement dao = new APDao_Agreement();
        SPRetInfo ret = new SPRetInfo();
        SortableBindingList<SaleFeed> infolist = new SortableBindingList<SaleFeed>();
        //查询条件下拉框
        string typecode = "";//下拉框类型
        List<string> TarGet = new List<string>();
        List<string> CstIntention = new List<string>();
        List<string> ProdIntention = new List<string>();
        List<string> Dynamics = new List<string>();
        List<string> FinalChannel = new List<string>();
        List<string> BuyerName = new List<string>();
        List<string> Manager = new List<string>();
        List<string> AgreeType = new List<string>();
        List<string> AgreeLevel = new List<string>();
        List<string> Seal = new List<string>();
        List<string> LastUpStream = new List<string>();

        Dictionary<string, string> TarGetDic = new Dictionary<string, string>();
        Dictionary<string, string> CstIntentionDic = new Dictionary<string, string>();
        Dictionary<string, string> ProdIntentionDic = new Dictionary<string, string>();
        Dictionary<string, string> DynamicsDic = new Dictionary<string, string>();
        Dictionary<string, string> FinalChannelDic = new Dictionary<string, string>();
        Dictionary<string, string> BuyerNameDic = new Dictionary<string, string>();
        Dictionary<string, string> ManagerDic = new Dictionary<string, string>();
        Dictionary<string, string> AgreeTypeDic = new Dictionary<string, string>();
        Dictionary<string, string> AgreeLevelDic = new Dictionary<string, string>();
        Dictionary<string, string> SealDic = new Dictionary<string, string>();
        Dictionary<string, string> LastUpStreamDic = new Dictionary<string, string>();
        public SaleFeedBacksForm()
        {
            InitializeComponent();
            dgvSaleFeedBackInfo.AutoGenerateColumns = false;
        }

        private void SaleFeedBacksForm_Load(object sender, EventArgs e)
        {
            label20.ForeColor = Color.Red;
            DateTime time= System.DateTime.Now;
            string st = time.ToString("yyyy");
            txtYearNum.Text = st;
            DateBegin.Checked = false;
            //目标分级下拉框加载

            typecode = "TARGET";
            TarGetDic = dao.getCbContent(typecode);
            TarGet = StringUtils.GetValue(TarGetDic);
            this.cBTarGet.DataSource = TarGet;

            //客户意向下拉框加载
            typecode = "CSTINTENTIONS";
            CstIntentionDic = dao.getCbContent(typecode);
            CstIntention = StringUtils.GetValue(CstIntentionDic);
            this.cBCstIntention.DataSource = CstIntention;


            //厂家意向下拉框加载
            typecode = "UPSTREAM";
            ProdIntentionDic = dao.getCbContent(typecode);
            ProdIntention = StringUtils.GetValue(ProdIntentionDic);
            this.cBProdIntention.DataSource = ProdIntention;


            //最终签约渠道
            typecode = "UPSTREAM";
            FinalChannelDic = dao.getCbContent(typecode);
            FinalChannel = StringUtils.GetValue(FinalChannelDic);
            this.cBFinalChannel.DataSource = FinalChannel;

            //签约动态下拉框加载
            typecode = "DYNAMICS";
            DynamicsDic = dao.getCbContent(typecode);
            Dynamics = StringUtils.GetValue(DynamicsDic);
            this.cBDynamics.DataSource = Dynamics;

            //采购员下拉框加载
            typecode = "BUYERNAME";
            BuyerNameDic = dao.getCbContent(typecode);
            BuyerName = StringUtils.GetValue(BuyerNameDic);
            this.cBBuyerName.DataSource = BuyerName;

            //采购经理下拉框加载
            typecode = "MANAGER";
            ManagerDic = dao.getCbContent(typecode);
            Manager = StringUtils.GetValue(ManagerDic);
            this.cBManager.DataSource = Manager;

            //协议性质下拉框加载
            typecode = "AGREETYPE";
            AgreeTypeDic = dao.getCbContent(typecode);
            AgreeType = StringUtils.GetValue(AgreeTypeDic);
            this.cBAgreeType.DataSource = AgreeType;

            //协议级别下拉框加载
            typecode = "AGREELEVEL";
            AgreeLevelDic = dao.getCbContent(typecode);
            AgreeLevel = StringUtils.GetValue(AgreeLevelDic);
            this.cBAgreeLevel.DataSource = AgreeLevel;

            //是否盖章下拉框加载
            typecode = "CHOICE";
            SealDic = dao.getCbContent(typecode);
            Seal = StringUtils.GetValue(SealDic);
            this.cBSeal.DataSource = Seal;

            //去年对应上游下拉框加载 
            typecode = "UPSTREAM";
            LastUpStreamDic = dao.getCbContent(typecode);
            LastUpStream = StringUtils.GetValue(LastUpStreamDic);
            this.cBLastUpStream.DataSource = LastUpStream;

            //绑定客户意向下拉框
            BindCsti();
            // 设置下拉列表框不可见
            CstIntention_ComboBoxs.Visible = false;
            // 添加下拉列表框事件
            CstIntention_ComboBoxs.SelectedIndexChanged += new EventHandler(cmb_Temp_SelectedIndexChanged);
            // 将下拉列表框加入到DataGridView控件中
            this.dgvSaleFeedBackInfo.Controls.Add(CstIntention_ComboBoxs);

            //绑定厂家意向下拉框
            BindProd();
            // 设置下拉列表框不可见
            ProdIntention_ComboBox.Visible = false;
            // 添加下拉列表框事件
            ProdIntention_ComboBox.SelectedIndexChanged += new EventHandler(Prod_SelectedIndexChanged);
            // 将下拉列表框加入到DataGridView控件中
            this.dgvSaleFeedBackInfo.Controls.Add(ProdIntention_ComboBox);

            //绑定签约动态下拉框
            BindDyna();
            // 设置下拉列表框不可见
            Dynas_ComboBox.Visible = false;
            // 添加下拉列表框事件
            Dynas_ComboBox.SelectedIndexChanged += new EventHandler(Dyna_SelectedIndexChanged);
            // 将下拉列表框加入到DataGridView控件中
            this.dgvSaleFeedBackInfo.Controls.Add(Dynas_ComboBox);

            //绑定签约渠道下拉框
            BindFina();
            // 设置下拉列表框不可见
            Fina_ComboBox.Visible = false;
            // 添加下拉列表框事件
            Fina_ComboBox.SelectedIndexChanged += new EventHandler(Fina_SelectedIndexChanged);
            // 将下拉列表框加入到DataGridView控件中
            this.dgvSaleFeedBackInfo.Controls.Add(Fina_ComboBox);
        }
        #region 绑定   下拉框数据处理
        /// <summary>
        /// 绑定客户意向下拉列表框
        /// </summary>
        private void BindCsti()
        {
            DataTable dtCstis = new DataTable();
            dtCstis.Columns.Add("Value");
            dtCstis.Columns.Add("Name");
            DataRow drCstis;
            typecodes = "CSTINTENTIONS";
            CstiDic = dao.getCbContent(typecodes);
            Csti = StringUtils.GetValue(CstiDic);
            for (int i=0;i<Csti.Count;i++) {
                drCstis = dtCstis.NewRow();
                drCstis[0] = StringUtils.GetKey(Csti[i], CstiDic);
                drCstis[1] = Csti[i];              
                dtCstis.Rows.Add(drCstis);
            }
            //this.CstIntention_ComboBox.DataSource = Csti;
            CstIntention_ComboBoxs.ValueMember = "Value";
            CstIntention_ComboBoxs.DisplayMember = "Name";
            CstIntention_ComboBoxs.DataSource = dtCstis;
            CstIntention_ComboBoxs.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        // 当用户选择下拉列表框时改变DataGridView单元格的内容
        private void cmb_Temp_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Csti.Count; i++)
            {
                if (((ComboBox)sender).Text == Csti[i])
                {
                    dgvSaleFeedBackInfo.CurrentCell.Value = Csti[i];
                    dgvSaleFeedBackInfo.CurrentCell.Tag = StringUtils.GetKey(Csti[i], CstiDic);
                }
            }
            try
            {
                SaleFeed info = dgvSaleFeedBackInfo.CurrentRow.DataBoundItem as SaleFeed;
                string cst = CstIntention_ComboBoxs.SelectedValue.ToString();
                //string Cstkey = StringUtils.GetKey(cst,CstiDic);
                string colName = "CSTINTENTION";
                int roleType = 1;
                dao.UpdateColValues(colName, cst, info.AgreementId, roleType, ret);
                if (ret.num != "1")
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                }
                this.CstIntention_ComboBoxs.Visible = false;
                //BtnQuery_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        /// <summary>
        /// 绑定厂家意向下拉列表框
        /// </summary>
        private void BindProd()
        {
            DataTable dtProd = new DataTable();
            dtProd.Columns.Add("Value");
            dtProd.Columns.Add("Name");
            DataRow drProd;
            typecodes = "UPSTREAM";
            ProdDic = dao.getCbContent(typecodes);
            Prod = StringUtils.GetValue(ProdDic);
            for (int i = 0; i < Prod.Count; i++)
            {
                drProd = dtProd.NewRow();
                drProd[0] = StringUtils.GetKey(Prod[i], ProdDic);
                drProd[1] = Prod[i];
                dtProd.Rows.Add(drProd);
            }
            //this.CstIntention_ComboBox.DataSource = Csti;
            ProdIntention_ComboBox.ValueMember = "Value";
            ProdIntention_ComboBox.DisplayMember = "Name";
            ProdIntention_ComboBox.DataSource = dtProd;
            ProdIntention_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        // 当用户选择下拉列表框时改变DataGridView单元格的内容
        private void Prod_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Prod.Count; i++)
            {
                if (((ComboBox)sender).Text == Prod[i])
                {
                    dgvSaleFeedBackInfo.CurrentCell.Value = Prod[i];
                    dgvSaleFeedBackInfo.CurrentCell.Tag = StringUtils.GetKey(Prod[i], ProdDic);
                }
            }
            try
            {
                SaleFeed info = dgvSaleFeedBackInfo.CurrentRow.DataBoundItem as SaleFeed;
                string prod = ProdIntention_ComboBox.SelectedValue.ToString();
                //string Cstkey = StringUtils.GetKey(cst,CstiDic);
                string colName = "PRODINTENTION";
                int roleType = 1;
                dao.UpdateColValues(colName, prod, info.AgreementId, roleType, ret);
                if (ret.num != "1")
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                }
                this.ProdIntention_ComboBox.Visible = false;
                //BtnQuery_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 绑定签约动态下拉列表框
        /// </summary>
        private void BindDyna()
        {
            DataTable dtDyna = new DataTable();
            dtDyna.Columns.Add("Value");
            dtDyna.Columns.Add("Name");
            DataRow drDyna;
            typecodes = "DYNAMICS";
            DynaDic = dao.getCbContent(typecodes);
            Dyna = StringUtils.GetValue(DynaDic);
            for (int i = 0; i < Dyna.Count; i++)
            {
                drDyna = dtDyna.NewRow();
                drDyna[0] = StringUtils.GetKey(Dyna[i], DynaDic);
                drDyna[1] = Dyna[i];
                dtDyna.Rows.Add(drDyna);
            }
            //this.CstIntention_ComboBox.DataSource = Csti;
            Dynas_ComboBox.ValueMember = "Value";
            Dynas_ComboBox.DisplayMember = "Name";
            Dynas_ComboBox.DataSource = dtDyna;
            Dynas_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        // 当用户选择下拉列表框时改变DataGridView单元格的内容
        private void Dyna_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Dyna.Count; i++)
            {
                if (((ComboBox)sender).Text == Dyna[i])
                {
                    dgvSaleFeedBackInfo.CurrentCell.Value = Dyna[i];
                    dgvSaleFeedBackInfo.CurrentCell.Tag = StringUtils.GetKey(Dyna[i], DynaDic);
                }
            }
            try
            {
                SaleFeed info = dgvSaleFeedBackInfo.CurrentRow.DataBoundItem as SaleFeed;
                string Dyna = Dynas_ComboBox.SelectedValue.ToString();
                //string Cstkey = StringUtils.GetKey(cst,CstiDic);
                string colName = "DYNAMICS";
                int roleType = 1;
                dao.UpdateColValues(colName, Dyna, info.AgreementId, roleType, ret);
                if (ret.num != "1")
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                }
                this.Dynas_ComboBox.Visible = false;
                //BtnQuery_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        /// <summary>
        /// 绑定签约渠道下拉列表框
        /// </summary>
        private void BindFina()
        {
            DataTable dtFina = new DataTable();
            dtFina.Columns.Add("Value");
            dtFina.Columns.Add("Name");
            DataRow drFina;
            typecodes = "UPSTREAM";
            FinaDic = dao.getCbContent(typecodes);
            Fina = StringUtils.GetValue(FinaDic);
            for (int i = 0; i < Fina.Count; i++)
            {
                drFina = dtFina.NewRow();
                drFina[0] = StringUtils.GetKey(Fina[i], FinaDic);
                drFina[1] = Fina[i];
                dtFina.Rows.Add(drFina);
            }
            //this.CstIntention_ComboBox.DataSource = Csti;
            Fina_ComboBox.ValueMember = "Value";
            Fina_ComboBox.DisplayMember = "Name";
            Fina_ComboBox.DataSource = dtFina;
            Fina_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        // 当用户选择下拉列表框时改变DataGridView单元格的内容
        private void Fina_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Fina.Count; i++)
            {
                if (((ComboBox)sender).Text == Fina[i])
                {
                    dgvSaleFeedBackInfo.CurrentCell.Value = Fina[i];
                    dgvSaleFeedBackInfo.CurrentCell.Tag = StringUtils.GetKey(Fina[i], FinaDic);
                }
            }
            try
            {
                SaleFeed info = dgvSaleFeedBackInfo.CurrentRow.DataBoundItem as SaleFeed;
                string Fina = Fina_ComboBox.SelectedValue.ToString();
                //string Cstkey = StringUtils.GetKey(cst,CstiDic);
                string colName = "FINALCHANNEL";
                int roleType = 1;
                dao.UpdateColValues(colName, Fina, info.AgreementId, roleType, ret);
                if (ret.num != "1")
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                }
                this.Fina_ComboBox.Visible = false;
                //BtnQuery_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion
        // 滚动DataGridView时将下拉列表框设为不可见
        private void dgvSaleFeedBackInfo_Scroll(object sender, ScrollEventArgs e)
        {
            this.CstIntention_ComboBoxs.Visible = false;
            this.ProdIntention_ComboBox.Visible = false;
            this.Dynas_ComboBox.Visible = false;
            this.Fina_ComboBox.Visible = false; 

        }
        // 改变DataGridView列宽时将下拉列表框设为不可见
        private void dgvSaleFeedBackInfo_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            this.CstIntention_ComboBoxs.Visible = false;
            this.ProdIntention_ComboBox.Visible = false;
            this.Dynas_ComboBox.Visible = false;
            this.Fina_ComboBox.Visible = false;
        }
        #region 打开和关闭查询条件
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            gB1.Visible = true;
        }

        private void BtnDiss_Click(object sender, EventArgs e)
        {
            gB1.Visible = false;
        }
        private void clearUI()
        {
            infolist.Clear();

        }
        #endregion
        #region 查询事件
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            clearUI();
            //this.Cursor = Cursors.WaitCursor;
            //获取下拉框的值
            string TarGet = cBTarGet.SelectedItem.ToString().Trim();
            string CstIntention = cBCstIntention.SelectedItem.ToString().Trim();
            string ProdIntention = cBProdIntention.SelectedItem.ToString().Trim();
            string Dynamics = cBDynamics.SelectedItem.ToString().Trim();
            string FinalChannel = cBFinalChannel.SelectedItem.ToString().Trim();
            string BuyerName = cBBuyerName.SelectedItem.ToString().Trim();
            string Manager = cBManager.SelectedItem.ToString().Trim();
            string AgreeType = cBAgreeType.SelectedItem.ToString().Trim();
            string AgreeLevel = cBAgreeLevel.SelectedItem.ToString().Trim();
            string Seal = cBSeal.SelectedItem.ToString().Trim();
            string LastUpStream = cBLastUpStream.SelectedItem.ToString().Trim();

            string TarGetKey = "";
            string CstIntentionKey = "";
            string ProdIntentionKey = "";
            string DynamicsKey = "";
            string FinalChannelKey = "";
            string BuyerNameKey = "";
            string ManagerKey = "";
            string AgreeTypeKey = "";
            string AgreeLevelKey = "";
            string SealKey = "";
            string LastUpStreamKey = "";
            //获取对应的key
            TarGetKey = StringUtils.GetKey(TarGet, TarGetDic);
            CstIntentionKey = StringUtils.GetKey(CstIntention, CstIntentionDic);
            ProdIntentionKey = StringUtils.GetKey(ProdIntention, ProdIntentionDic);
            DynamicsKey = StringUtils.GetKey(Dynamics, DynamicsDic);
            FinalChannelKey = StringUtils.GetKey(FinalChannel, FinalChannelDic);
            BuyerNameKey = StringUtils.GetKey(BuyerName, BuyerNameDic);
            ManagerKey = StringUtils.GetKey(Manager, ManagerDic);
            AgreeTypeKey = StringUtils.GetKey(AgreeType, AgreeTypeDic);
            AgreeLevelKey = StringUtils.GetKey(AgreeLevel, AgreeLevelDic);
            SealKey = StringUtils.GetKey(Seal, SealDic);
            LastUpStreamKey = StringUtils.GetKey(LastUpStream, LastUpStreamDic);

            //获取时间
            string begintime = "";
            if (DateBegin.Checked)
            {
                begintime = DateBegin.Value.ToString("yyyyMMdd");
                Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
                if (!StringUtils.IsNull(txtYearNum.Text.ToString().Trim()))
                {
                    sqlkeydict.Add("yearnum", txtYearNum.Text.ToString().Trim());
                }
                else
                {
                    MessageBox.Show("年份为必填项！", "前台提示");
                    //this.Cursor = Cursors.Default;
                    return;
                }
                if (!StringUtils.IsNull(TarGet))
                {
                    sqlkeydict.Add("target", TarGetKey);
                }
                if (!StringUtils.IsNull(CstIntention))
                {
                    sqlkeydict.Add("cstintention", CstIntentionKey);
                }
                if (!StringUtils.IsNull(ProdIntention))
                {
                    sqlkeydict.Add("prodintention", ProdIntentionKey);
                }
                if (!StringUtils.IsNull(Dynamics))
                {
                    sqlkeydict.Add("dynamics", DynamicsKey);
                }
                if (!StringUtils.IsNull(FinalChannel))
                {
                    sqlkeydict.Add("finalchannel", FinalChannelKey);
                }
                if (!StringUtils.IsNull(BuyerName))
                {
                    sqlkeydict.Add("buyername", BuyerNameKey);
                }
                if (!StringUtils.IsNull(Manager))
                {
                    sqlkeydict.Add("Manager", ManagerKey);
                }
                if (!StringUtils.IsNull(AgreeType))
                {
                    sqlkeydict.Add("agreetype", AgreeTypeKey);
                }
                if (!StringUtils.IsNull(AgreeLevel))
                {
                    sqlkeydict.Add("agreelevel", AgreeLevelKey);
                }
                if (!StringUtils.IsNull(Seal))
                {
                    sqlkeydict.Add("seal", SealKey);
                }
                if (!StringUtils.IsNull(LastUpStream))
                {
                    sqlkeydict.Add("lastupstream", LastUpStreamKey);
                }

                if (!StringUtils.IsNull(txtProdName.Text))
                {
                    sqlkeydict.Add("prod_name%", "%" + txtProdName.Text.ToString() + "%");
                }
                if (!StringUtils.IsNull(txtCstCode.Text))
                {
                    sqlkeydict.Add("cstcode%", "%" + txtCstCode.Text.ToString() + "%");
                }
                if (!StringUtils.IsNull(txtCstName.Text))
                {
                    sqlkeydict.Add("cstname%", "%" + txtCstName.Text.ToString() + "%");
                }
                if (!StringUtils.IsNull(txtSallLeader.Text))
                {
                    sqlkeydict.Add("sallleader%", "%" + txtSallLeader.Text.ToString() + "%");
                }
                if (!StringUtils.IsNull(txtSallManager.Text))
                {
                    sqlkeydict.Add("sallmanager%", "%" + txtSallManager.Text.ToString() + "%");
                }
                if (!StringUtils.IsNull(txtSaller.Text))
                {
                    sqlkeydict.Add("saller%", "%" + txtSaller.Text.ToString() + "%");
                }
                if (!StringUtils.IsNull(txtMiddleMan.Text))
                {
                    sqlkeydict.Add("middleman%", "%" + txtMiddleMan.Text.ToString() + "%");
                }
                infolist = dao.GetSaleFeedBackInfos(sqlkeydict,begintime);
                if (infolist.Count <= 0)
                {
                    MessageBox.Show("未查询到数据！", "程序提示");
                    
                    dgvSaleFeedBackInfo.DataSource = new List<SaleFeed>(infolist);
                    dgvSaleFeedBackInfo.Refresh();
                    //this.Cursor = Cursors.Default;
                    return;
                }
                else
                {

                    dgvSaleFeedBackInfo.DataSource = infolist;
                    dgvSaleFeedBackInfo.Refresh();
                    dgvSaleFeedBackInfo.CurrentCell = null;
                    //this.Cursor = Cursors.Default;
                }

            }
            else
            {
                Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
                if (!StringUtils.IsNull(txtYearNum.Text.ToString().Trim()))
                {
                    sqlkeydict.Add("yearnum", txtYearNum.Text.ToString().Trim());
                }
                else
                {
                    MessageBox.Show("年份为必填项！", "前台提示");
                    this.Cursor = Cursors.Default;
                    return;
                }
                if (!StringUtils.IsNull(TarGet))
                {
                    sqlkeydict.Add("target", TarGetKey);
                }
                if (!StringUtils.IsNull(CstIntention))
                {
                    sqlkeydict.Add("cstintention", CstIntentionKey);
                }
                if (!StringUtils.IsNull(ProdIntention))
                {
                    sqlkeydict.Add("prodintention", ProdIntentionKey);
                }
                if (!StringUtils.IsNull(Dynamics))
                {
                    sqlkeydict.Add("dynamics", DynamicsKey);
                }
                if (!StringUtils.IsNull(FinalChannel))
                {
                    sqlkeydict.Add("finalchannel", FinalChannelKey);
                }
                if (!StringUtils.IsNull(BuyerName))
                {
                    sqlkeydict.Add("buyername", BuyerNameKey);
                }
                if (!StringUtils.IsNull(Manager))
                {
                    sqlkeydict.Add("Manager", ManagerKey);
                }
                if (!StringUtils.IsNull(AgreeType))
                {
                    sqlkeydict.Add("agreetype", AgreeTypeKey);
                }
                if (!StringUtils.IsNull(AgreeLevel))
                {
                    sqlkeydict.Add("agreelevel", AgreeLevelKey);
                }
                if (!StringUtils.IsNull(LastUpStream))
                {
                    sqlkeydict.Add("lastupstream", LastUpStreamKey);
                }
                if (!StringUtils.IsNull(Seal))
                {
                    sqlkeydict.Add("seal", SealKey);
                }

                if (!StringUtils.IsNull(txtProdName.Text))
                {
                    sqlkeydict.Add("prod_name%", "%" + txtProdName.Text.ToString() + "%");
                }
                if (!StringUtils.IsNull(txtCstCode.Text))
                {
                    sqlkeydict.Add("cstcode%", "%" + txtCstCode.Text.ToString() + "%");
                }
                if (!StringUtils.IsNull(txtCstName.Text))
                {
                    sqlkeydict.Add("cstname%", "%" + txtCstName.Text.ToString() + "%");
                }
                if (!StringUtils.IsNull(txtSallLeader.Text))
                {
                    sqlkeydict.Add("sallleader%", "%" + txtSallLeader.Text.ToString() + "%");
                }
                if (!StringUtils.IsNull(txtSallManager.Text))
                {
                    sqlkeydict.Add("sallmanager%", "%" + txtSallManager.Text.ToString() + "%");
                }
                if (!StringUtils.IsNull(txtSaller.Text))
                {
                    sqlkeydict.Add("saller%", "%" + txtSaller.Text.ToString() + "%");
                }
                if (!StringUtils.IsNull(txtMiddleMan.Text))
                {
                    sqlkeydict.Add("middleman%", "%" + txtMiddleMan.Text.ToString() + "%");
                }

                infolist = dao.GetSaleFeedBackInfo(sqlkeydict);
                if (infolist.Count <= 0)
                {
                    MessageBox.Show("未查询到数据！", "程序提示");
                    dgvSaleFeedBackInfo.DataSource = new List<SaleFeed>(infolist);
                    dgvSaleFeedBackInfo.Refresh();
                    //this.Cursor = Cursors.Default;
                    return;
                }
                else
                {
                    dgvSaleFeedBackInfo.DataSource = infolist;
                    dgvSaleFeedBackInfo.Refresh();
                    dgvSaleFeedBackInfo.CurrentCell = null;
                    //this.Cursor = Cursors.Default;
                }
            }          
        }
        #endregion
        #region 双击修改事件
        //委托 事件处理方法
        void selForm_TransfEvent(EmpInfos empInfo)
        {
            empName = empInfo.EmpName;
        }
            //修改
            private void dgvSaleFeedBackInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //控制只能是经理和超级管理员做前三个修改
            if (LoginId == "118" || LoginId == "120" || LoginId == "99" || LoginId == "121" || LoginId == "123")
            {
                if (e.ColumnIndex != 9 && e.ColumnIndex != 10 && e.ColumnIndex != 11 && e.ColumnIndex != 14 && e.ColumnIndex != 18)
                {
                    dgvSaleFeedBackInfo.Columns[e.ColumnIndex].ReadOnly = true;
                    return;
                }
                //销售副总
                if (e.ColumnIndex == 9 && e.RowIndex != -1)
                {
                    empName = "";
                    dgvSaleFeedBackInfo.Columns[e.ColumnIndex].ReadOnly = false;
                    UpdateNameForm un = new UpdateNameForm();
                    //注册事件
                    un.TrandfEvent += selForm_TransfEvent;
                    un.ShowDialog();
                    dgvSaleFeedBackInfo.Rows[e.RowIndex].Cells["Column10"].Value = empName;
                    DataGridViewCell cell = dgvSaleFeedBackInfo.Rows[e.RowIndex].Cells["Column10"];
                    dgvSaleFeedBackInfo.CurrentCell = cell;
                    dgvSaleFeedBackInfo.BeginEdit(true);
                    
                }
                //销售经理
                if (e.ColumnIndex == 10 && e.RowIndex != -1)
                {
                    empName = "";
                    dgvSaleFeedBackInfo.Columns[e.ColumnIndex].ReadOnly = false;
                    UpdateNameForm un = new UpdateNameForm();
                    //注册事件
                    un.TrandfEvent += selForm_TransfEvent;
                    un.ShowDialog();
                    dgvSaleFeedBackInfo.Rows[e.RowIndex].Cells["Column11"].Value = empName;
                    DataGridViewCell cell = dgvSaleFeedBackInfo.Rows[e.RowIndex].Cells["Column11"];
                    dgvSaleFeedBackInfo.CurrentCell = cell;
                    dgvSaleFeedBackInfo.BeginEdit(true);
                   
                }
                //销售代表
                if (e.ColumnIndex == 11 && e.RowIndex != -1)
                {
                    empName = "";
                    dgvSaleFeedBackInfo.Columns[e.ColumnIndex].ReadOnly = false;
                    UpdateNameForm un = new UpdateNameForm();
                    //注册事件
                    un.TrandfEvent += selForm_TransfEvent;
                    un.ShowDialog();
                    dgvSaleFeedBackInfo.Rows[e.RowIndex].Cells["Column12"].Value = empName;
                    DataGridViewCell cell = dgvSaleFeedBackInfo.Rows[e.RowIndex].Cells["Column12"];
                    dgvSaleFeedBackInfo.CurrentCell = cell;
                    dgvSaleFeedBackInfo.BeginEdit(true);
                   
                }
                //备注
                if (e.ColumnIndex == 14 && e.RowIndex != -1)
                {
                    dgvSaleFeedBackInfo.Columns[e.ColumnIndex].ReadOnly = false;
                    DataGridViewCell cell = dgvSaleFeedBackInfo.Rows[e.RowIndex].Cells["Column32"];
                    dgvSaleFeedBackInfo.CurrentCell = cell;
                    dgvSaleFeedBackInfo.BeginEdit(true);

                }
                //意向协议量
                if (e.ColumnIndex == 18 && e.RowIndex != -1)
                {
                    dgvSaleFeedBackInfo.Columns[e.ColumnIndex].ReadOnly = false;
                    DataGridViewCell cell = dgvSaleFeedBackInfo.Rows[e.RowIndex].Cells["Column18"];
                    dgvSaleFeedBackInfo.CurrentCell = cell;
                    dgvSaleFeedBackInfo.BeginEdit(true);                  
                }

            }
            else {
                if (e.ColumnIndex != 14 && e.ColumnIndex != 18) {
                    dgvSaleFeedBackInfo.Columns[e.ColumnIndex].ReadOnly = true;
                    return;
                }
                //备注
                if (e.ColumnIndex == 14 && e.RowIndex != -1)
                {
                    dgvSaleFeedBackInfo.Columns[e.ColumnIndex].ReadOnly = false;
                    DataGridViewCell cell = dgvSaleFeedBackInfo.Rows[e.RowIndex].Cells["Column32"];
                    dgvSaleFeedBackInfo.CurrentCell = cell;
                    dgvSaleFeedBackInfo.BeginEdit(true);

                }

                //意向协议量
                if (e.ColumnIndex == 18 && e.RowIndex != -1)
                {
                    dgvSaleFeedBackInfo.Columns[e.ColumnIndex].ReadOnly = false;
                    DataGridViewCell cell = dgvSaleFeedBackInfo.Rows[e.RowIndex].Cells["Column18"];
                    dgvSaleFeedBackInfo.CurrentCell = cell;
                    dgvSaleFeedBackInfo.BeginEdit(true);
                   
                }
            }              
        }
        //修改完成
        private void dgvSaleFeedBackInfo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SPRetInfo ret = new SPRetInfo();
            SaleFeed info = dgvSaleFeedBackInfo.CurrentRow.DataBoundItem as SaleFeed;

            //销售副总
            if (e.ColumnIndex == 9 && e.RowIndex != -1)
            {
                string colName = "SALLLEADER";
                int roleType = 1;
                if (StringUtils.IsNull(info.SallLeader))
                {
                    string sallleader = "-1";
                    dao.UpdateColValues(colName, sallleader, info.AgreementId, roleType, ret);
                    if (ret.num != "1")
                    {
                        MessageBox.Show(ret.msg, "后台提示！");
                    }
                }
                dao.UpdateColValues(colName, info.SallLeader, info.AgreementId, roleType, ret);
                if (ret.num != "1")
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                }
                return;
            }
            //销售经理
            if (e.ColumnIndex == 10 && e.RowIndex != -1)
            {
                string colName = "SALLMANAGER";
                int roleType = 1;
                if (StringUtils.IsNull(info.SallManager))
                {
                    string sallmanager = "-1";
                    dao.UpdateColValues(colName, sallmanager, info.AgreementId, roleType, ret);
                    if (ret.num != "1")
                    {
                        MessageBox.Show(ret.msg, "后台提示！");
                    }
                }
                dao.UpdateColValues(colName, info.SallManager, info.AgreementId, roleType, ret);
                if (ret.num != "1")
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                }
                return;
            }
            //销售代表
            if (e.ColumnIndex == 11 && e.RowIndex != -1)
            {
                string colName = "SALLER";
                int roleType = 1;
                if (StringUtils.IsNull(info.Saller))
                {
                    string saller = "-1";
                    dao.UpdateColValues(colName, saller, info.AgreementId, roleType, ret);
                    if (ret.num != "1")
                    {
                        MessageBox.Show(ret.msg, "后台提示！");
                    }
                }
                dao.UpdateColValues(colName, info.Saller, info.AgreementId, roleType, ret);
                if (ret.num != "1")
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                }
                return;
            }
            //备注
            if (e.ColumnIndex == 14 && e.RowIndex != -1)
            {
                string colName = "MARK";
                int roleType = 1;
                if (StringUtils.IsNull(info.Mark))
                {
                    string mark = "-1";
                    dao.UpdateColValues(colName, mark, info.AgreementId, roleType, ret);
                    if (ret.num != "1")
                    {
                        MessageBox.Show(ret.msg, "后台提示！");
                    }
                }
                else {
                    dao.UpdateColValues(colName, info.Mark, info.AgreementId, roleType, ret);
                    if (ret.num != "1")
                    {
                        MessageBox.Show(ret.msg, "后台提示！");
                    }
                }
               
                return;
            }
            //意向协议量
            if (e.ColumnIndex == 18 && e.RowIndex != -1)
            {
                string colName = "HOPEVALUES";
                int roleType = 1;
                if (StringUtils.IsNull(info.HopeValues))
                {
                    string HopeValues = "-1";
                    dao.UpdateColValues(colName, HopeValues, info.AgreementId, roleType, ret);
                    if (ret.num != "1")
                    {
                        MessageBox.Show(ret.msg, "后台提示！");
                    }
                }
                else {
                    dao.UpdateColValues(colName, info.HopeValues, info.AgreementId, roleType, ret);
                    if (ret.num != "1")
                    {
                        MessageBox.Show(ret.msg, "后台提示！");
                    }
                }
                return;
            }
        }

        #endregion
        #region excel导出
            private void BtnExcel_Click(object sender, EventArgs e)
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
            DataTable dt = GetDgvToTable(dgvSaleFeedBackInfo);
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
            DataTable t = dt.DefaultView.ToTable("dt", true, "年份", "协议ID", "供应商编号", "商务团队", "厂家性质", "采购员", "采购经理", "协议性质", "销售对接人", "销售副总", "销售经理", "销售代表", "协议级别", "客户编码", "客户名称", "去年销售额", "去年对应上游", "今年厂家预测量（万元）", "目标分级", "协议启动时间", "客户意向", "厂家意向","备注", "签约动态", "最终签约渠道", "今年产生销售额", "意向协议量（万元）", "是否盖章", "协议存档", "最终协议量（万元）", "采购最近修改时间");

            return t;
        }
        #endregion
        #region 下拉框处理
        //行变化 修改下拉框
        private void dgvSaleFeedBackInfo_CurrentCellChanged(object sender, EventArgs e)
        {
              try
                {
                if (this.dgvSaleFeedBackInfo.CurrentCell.ColumnIndex == 12)
                {
                    Rectangle rect = dgvSaleFeedBackInfo.GetCellDisplayRectangle(dgvSaleFeedBackInfo.CurrentCell.ColumnIndex, dgvSaleFeedBackInfo.CurrentCell.RowIndex, false);
                    string CstiValue = dgvSaleFeedBackInfo.CurrentCell.Value.ToString();
                    //for (int i = 0; i < Csti.Count; i++)
                    //{
                    //    if (CstiValue == StringUtils.GetKey(Csti[i], CstiDic))
                    //    {
                    //        CstIntention_ComboBox.Text = Csti[i];
                    //    }

                    //}
                    CstIntention_ComboBoxs.Left = rect.Left;
                    CstIntention_ComboBoxs.Top = rect.Top;
                    CstIntention_ComboBoxs.Width = rect.Width;
                    CstIntention_ComboBoxs.Height = rect.Height;
                    CstIntention_ComboBoxs.Visible = true;
                }
                else
                {
                    CstIntention_ComboBoxs.Visible = false;
                }

                if (this.dgvSaleFeedBackInfo.CurrentCell.ColumnIndex == 13 && this.dgvSaleFeedBackInfo.CurrentCell.RowIndex != -1)
                {
                    Rectangle rect = dgvSaleFeedBackInfo.GetCellDisplayRectangle(dgvSaleFeedBackInfo.CurrentCell.ColumnIndex, dgvSaleFeedBackInfo.CurrentCell.RowIndex, false);
                    string Value = dgvSaleFeedBackInfo.CurrentCell.Value.ToString();
                    this.ProdIntention_ComboBox.Text = Value;
                    this.ProdIntention_ComboBox.Left = rect.Left;
                    this.ProdIntention_ComboBox.Top = rect.Top;
                    this.ProdIntention_ComboBox.Width = rect.Width;
                    this.ProdIntention_ComboBox.Height = rect.Height;
                    this.ProdIntention_ComboBox.Visible = true;

                } else {
                    this.ProdIntention_ComboBox.Visible = false;
                }

                if (this.dgvSaleFeedBackInfo.CurrentCell.ColumnIndex == 15 && this.dgvSaleFeedBackInfo.CurrentCell.RowIndex != -1)
                {
                    Rectangle rect = dgvSaleFeedBackInfo.GetCellDisplayRectangle(dgvSaleFeedBackInfo.CurrentCell.ColumnIndex, dgvSaleFeedBackInfo.CurrentCell.RowIndex, false);
                    string Value = dgvSaleFeedBackInfo.CurrentCell.Value.ToString();
                    this.Dynas_ComboBox.Text = Value;
                    this.Dynas_ComboBox.Left = rect.Left;
                    this.Dynas_ComboBox.Top = rect.Top;
                    this.Dynas_ComboBox.Width = rect.Width;
                    this.Dynas_ComboBox.Height = rect.Height;
                    this.Dynas_ComboBox.Visible = true;

                }
                else
                {
                    this.Dynas_ComboBox.Visible = false;
                }

                if (this.dgvSaleFeedBackInfo.CurrentCell.ColumnIndex == 16 && this.dgvSaleFeedBackInfo.CurrentCell.RowIndex != -1)
                {
                    Rectangle rect = dgvSaleFeedBackInfo.GetCellDisplayRectangle(dgvSaleFeedBackInfo.CurrentCell.ColumnIndex, dgvSaleFeedBackInfo.CurrentCell.RowIndex, false);
                    string Value = dgvSaleFeedBackInfo.CurrentCell.Value.ToString();
                    this.Fina_ComboBox.Text = Value;
                    this.Fina_ComboBox.Left = rect.Left;
                    this.Fina_ComboBox.Top = rect.Top;
                    this.Fina_ComboBox.Width = rect.Width;
                    this.Fina_ComboBox.Height = rect.Height;
                    this.Fina_ComboBox.Visible = true;

                }
                else
                {
                    this.Fina_ComboBox.Visible = false;
                }

            }
                catch
                {

                }
            
        }


        //private void dgvSaleFeedBackInfo_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        //{
        //    for (int i = 0; i < this.dgvSaleFeedBackInfo.Rows.Count; i++)
        //    {
        //        if (dgvSaleFeedBackInfo.Rows[i].Cells["Column13"].Value != null && dgvSaleFeedBackInfo.Rows[i].Cells["Column13"].ColumnIndex == 12)
        //        {
        //            dgvSaleFeedBackInfo.Rows[i].Cells["Column13"].Tag = dgvSaleFeedBackInfo.Rows[i].Cells["Column13"].Value.ToString();
        //            for (int j = 0; j < Csti.Count; j++)
        //            {
        //                if (dgvSaleFeedBackInfo.Rows[j].Cells["Column13"].Value.ToString() == StringUtils.GetKey(Csti[j], CstiDic))
        //                {
        //                    dgvSaleFeedBackInfo.Rows[j].Cells["Column13"].Value = Csti[j];
        //                }

        //            }
        //        }

        //        if (dgvSaleFeedBackInfo.Rows[i].Cells["Column14"].Value != null && dgvSaleFeedBackInfo.Rows[i].Cells["Column14"].ColumnIndex == 13)
        //        {
        //            dgvSaleFeedBackInfo.Rows[i].Cells["Column14"].Tag = dgvSaleFeedBackInfo.Rows[i].Cells["Column14"].Value.ToString();
        //            for (int j = 0; j < Prod.Count; j++)
        //            {
        //                if (dgvSaleFeedBackInfo.Rows[j].Cells["Column14"].Value.ToString() == StringUtils.GetKey(Prod[j], ProdDic))
        //                {
        //                    dgvSaleFeedBackInfo.Rows[j].Cells["Column14"].Value = Prod[j];
        //                }

        //            }
        //        }

        //        if (dgvSaleFeedBackInfo.Rows[i].Cells["Column15"].Value != null && dgvSaleFeedBackInfo.Rows[i].Cells["Column15"].ColumnIndex == 15)
        //        {
        //            dgvSaleFeedBackInfo.Rows[i].Cells["Column15"].Tag = dgvSaleFeedBackInfo.Rows[i].Cells["Column15"].Value.ToString();
        //            for (int j = 0; j < Dyna.Count; j++)
        //            {
        //                if (dgvSaleFeedBackInfo.Rows[j].Cells["Column15"].Value.ToString() == StringUtils.GetKey(Dyna[j], DynaDic))
        //                {
        //                    dgvSaleFeedBackInfo.Rows[j].Cells["Column15"].Value = Dyna[j];
        //                }

        //            }
        //        }

        //        if (dgvSaleFeedBackInfo.Rows[i].Cells["Column16"].Value != null && dgvSaleFeedBackInfo.Rows[i].Cells["Column16"].ColumnIndex == 16)
        //        {
        //            dgvSaleFeedBackInfo.Rows[i].Cells["Column16"].Tag = dgvSaleFeedBackInfo.Rows[i].Cells["Column16"].Value.ToString();
        //            for (int j = 0; j < Dyna.Count; j++)
        //            {
        //                if (dgvSaleFeedBackInfo.Rows[j].Cells["Column16"].Value.ToString() == StringUtils.GetKey(Fina[j], FinaDic))
        //                {
        //                    dgvSaleFeedBackInfo.Rows[j].Cells["Column16"].Value = Fina[j];
        //                }

        //            }
        //        }

        //    }
        //}
        #endregion

         //外部查询
        private void BtnOutQuery_Click(object sender, EventArgs e)
        {

        }
    }
}

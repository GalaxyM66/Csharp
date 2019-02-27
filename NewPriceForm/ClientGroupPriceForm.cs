using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PriceManager
{
    public partial class ClientGroupPriceForm : DockContent
    {
        SortableBindingList<CstGroupHdr> CstGroupHdrList = new SortableBindingList<CstGroupHdr>();//主表
        SortableBindingList<CstGroupDtl> CstGroupDtlList = new SortableBindingList<CstGroupDtl>();//明细
        SortableBindingList<GoodPrc> CstGoodPrcList = new SortableBindingList<GoodPrc>();//价格
        SortableBindingList<SysCode> GrouptypeCodeList = new SortableBindingList<SysCode>();//客户组分类表
        SortableBindingList<SysCode> DetailtypeCodeList = new SortableBindingList<SysCode>();//客户组类型
        SortableBindingList<SysCode> SynctypeCodeList = new SortableBindingList<SysCode>();//客户组同步分类
        SortableBindingList<PubDept> PubDeptList = new SortableBindingList<PubDept>();//归属部门

        /// <summary>
        /// 新增客户信息列表用于暂存
        /// </summary>
        SortableBindingList<CstGroupDtl> SelNewList = new SortableBindingList<CstGroupDtl>();
        /// <summary>
        /// 选中主表信息列表用于操作
        /// </summary>
        SortableBindingList<CstGroupHdr> AddSelCstGroupHdrList = new SortableBindingList<CstGroupHdr>();
        /// <summary>
        /// 新增客户信息列表用于更新
        /// </summary>
        SortableBindingList<CstGroupDtl> AddSelNewList = new SortableBindingList<CstGroupDtl>();
        /// <summary>
        /// 价格明细数据用于更新操作
        /// </summary>
        SortableBindingList<GoodPrc> AddSelNewPrcList = new SortableBindingList<GoodPrc>();

        APDao_ClientGroup dao = new APDao_ClientGroup();
        PMSystemDao Pdao = new PMSystemDao();
        SortableBindingList<SysCode> stopflagList = new SortableBindingList<SysCode>();

        //构造窗体
        public ClientGroupPriceForm()
        {
            InitializeComponent();
        }

       
        //载入窗体
        private void ClientGroupPriceForm_Load(object sender, EventArgs e)
        {
            dgvClientGroup.AutoGenerateColumns = false;
            dgvClientDtl.AutoGenerateColumns = false;
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            sqlkeydict.Add("typeid", "1");
            stopflagList = Pdao.GetSysCode(sqlkeydict);

            SysCode info = new SysCode();
            info.Name = "-全部-";
            stopflagList.Add(info);

            cbStopflag.DataSource = stopflagList;
            cbStopflag.DisplayMember = "Name";
            cbStopflag.ValueMember = "Code";
            cbStopflag.Text = "-全部-";
            initUI(0);//查询模式

            if ("1" == PubOwnerConfigureDto.Saledepttype)
            {
                cbASaledeptname.Visible = true;
                label8.Visible = true;
            }
            else
            {
                cbASaledeptname.Visible = false;
                label8.Visible = false;
            }

            if ("1" == PubOwnerConfigureDto.Allowprctype)
            {
             chkModprc.Visible = true;
            }
            else
            {
                chkModprc.Checked = true;
                chkModprc.Visible = false;
            }
            //1默认全选品种；2可以自选商品；3默认全选商品,在已选界面有撤销功能
            if (PubOwnerConfigureDto.Goodchoose != "2")
            {
                btnSyncAdd.Enabled = false;
            }
        }

        //UI锁
        private void initUI(int astate)
        {
            int uiState = astate;
            switch (uiState)
            {
                case 0://查询
                    //txtAAttachcode.ReadOnly = true;
                    txtACode.ReadOnly = true;
                    txtAName.ReadOnly = true;
                    txtAMark.ReadOnly = true;
                    chkStopflag.Enabled = false;
                    cbADetailtypename.Enabled = false;
                    cbAGrouptypename.Enabled = false;
                    cbASaledeptname.Enabled = false;
                    cbASynctypename.Enabled = false;
                    
                    UDADeliveryfeerate.Enabled = false;
                    btnConfirm.Enabled = false;
                    btnReset.Enabled = false;
                    btnClientGroupSave.Enabled = false;
                    
                    break;
                case 1://新增
                    //txtAAttachcode.ReadOnly = false;
                    txtACode.ReadOnly = false;
                    txtAName.ReadOnly = false;
                    txtAMark.ReadOnly = false;
                    chkStopflag.Enabled = true;
                    cbADetailtypename.Enabled = true;
                    cbAGrouptypename.Enabled = true;
                    cbASaledeptname.Enabled = true;
                    cbASynctypename.Enabled = true;

                    
                    UDADeliveryfeerate.Enabled = true;
                    btnConfirm.Enabled = true;
                    btnReset.Enabled = true;

                    CstGroupHdrList.Clear();
                    CstGroupDtlList.Clear();
                    CstGoodPrcList.Clear();
                    dgvClientDtl.DataSource = CstGroupDtlList;
                    dgvClientDtl.Refresh();
                    dgvClientGroup.DataSource = CstGroupHdrList;
                    dgvClientGroup.Refresh();
                    dgvClientPrc.DataSource = CstGoodPrcList;
                    dgvClientPrc.Refresh();
                    txtCode.Text = "";
                    txtName.Text = "";
                    cbStopflag.Text = "-全部-";

                    break;
                case 2://修改

                    break;

                case 3://清空
                    //txtAAttachcode.Text = "";
                    txtACode.Text = "";
                    txtAMark.Text = "";
                    txtAName.Text = "";
                    UDADeliveryfeerate.Value = 0;
                    cbADetailtypename.Text = "-请选择-";
                    cbAGrouptypename.Text = "-请选择-";
                    cbASaledeptname.Text = "-请选择-";
                    cbASynctypename.Text = "-请选择-";
                    chkStopflag.Checked = false;
                    toolStrip2.Enabled = true;
                    CstGoodPrcList.Clear();
                    CstGroupDtlList.Clear();
                    CstGroupHdrList.Clear();
                    AddSelNewList.Clear();
                    AddSelNewPrcList.Clear();
                    break;

                case 4://新增结束
                    txtACode.Text = "";
                    txtAMark.Text = "";
                    txtAName.Text = "";
                    UDADeliveryfeerate.Value = 0;
                    cbADetailtypename.Text = "-请选择-";
                    cbAGrouptypename.Text = "-请选择-";
                    cbASaledeptname.Text = "-请选择-";
                    cbASynctypename.Text = "-请选择-";
                    chkStopflag.Checked = false;
                    toolStrip2.Enabled = true;
                    CstGoodPrcList.Clear();
                    CstGroupDtlList.Clear();
                    CstGroupHdrList.Clear();
                    dgvClientDtl.DataSource = CstGroupDtlList;
                    dgvClientDtl.Refresh();
                    dgvClientGroup.DataSource = CstGroupHdrList;
                    dgvClientGroup.Refresh();
                    dgvClientPrc.DataSource = CstGoodPrcList;
                    dgvClientPrc.Refresh();

                    AddSelNewList.Clear();
                    AddSelNewPrcList.Clear();
                    btnFilter.Enabled = true;
                    btnStartCstDtl.Enabled = true;
                    btnStopCstDtl.Enabled = true;
                    btnAddCstDtl.Enabled = true;
                    btnConfirmCstDtl.Enabled = true;
                    break;
            }
        }

        //查询按钮
        private void queryBtn_Click(object sender, EventArgs e)
        {
            initUI(3);
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                sqlkeydict.Add("code%", "%" + txtCode.Text.ToString() + "%");
            }
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                sqlkeydict.Add("name%", "%" + txtName.Text.ToString() + "%");
            }
            if (cbStopflag.Text != "-全部-" & cbStopflag.Text != "")
            {
                sqlkeydict.Add("stopflag", cbStopflag.SelectedValue.ToString());
            }
            try
            {
                CstGroupHdrList = dao.GetCstGroupHdrList(sqlkeydict);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            if (CstGroupHdrList.Count <= 0)
            {
                MessageBox.Show("未查询到数据！", "系统提示！");
                return;
            }
            else
            {
                dgvClientGroup.DataSource = CstGroupHdrList;
                dgvClientGroup.Refresh();
                //dgvClientGroup.CurrentCell = null;
            }
            initUI(0);
            addClientGroupBtn.Enabled = true;
            updateClientGroupBtn.Enabled = true;
            dgvClientGroup.Enabled = true;

        }

        //点击头表
        private void dgvClientGroup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewSelectedRowCollection row = dgvClientGroup.SelectedRows;
            if (e.ColumnIndex < 1 || e.RowIndex == -1)//第一列才触发事件
                return;
            //获取选中行的值

            //initUI(state);
            CstGroupHdr info = dgvClientGroup.CurrentRow.DataBoundItem as CstGroupHdr;
            CstGroupDtlList = dao.GetCstGroupDtlList(info.Hdrid);
            CstGoodPrcList = dao.GetGoodPrcDtlList(info.Hdrid);

            dgvClientDtl.DataSource = CstGroupDtlList;
            dgvClientDtl.Refresh();

            dgvClientPrc.DataSource = CstGoodPrcList;
            dgvClientPrc.Refresh();

            btnAddCstDtl.Enabled = true;
            btnConfirmCstDtl.Enabled = true;
            btnStartCstDtl.Enabled = true;
            btnStopCstDtl.Enabled = true;
            txtFCstcode.Text = "";
            txtFCSTNAME.Text = "";
            txtFregion.Text = "";
            AddSelNewList.Clear();
            AddSelNewPrcList.Clear();
            chkModprc.Enabled = true;
            btnFilter.Enabled = true;
            if (info.Detailtype == "10")
            {
                toolStrip2.Enabled = false;
            }
            else
            {
                toolStrip2.Enabled = true;
            }
            if (info.Synctype == "10")
            {
                chkModprc.Checked = false;
                chkModprc.Enabled = false;
            }
            else
            {
                chkModprc.Enabled = true;
            }
        }

        //明细表勾选
        private void dgvClientDtl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewSelectedRowCollection row = dgvClientDtl.SelectedRows;
            if (e.ColumnIndex != 24 || e.RowIndex == -1)//第一列才触发事件
                return;
            //获取选中行的值
            CstGroupDtl info = dgvClientDtl.CurrentRow.DataBoundItem as CstGroupDtl;
            if (info.SelectFlag == true)
            {
                info.SelectFlag = false;

            }
            else
            {

                info.SelectFlag = true;
            }
        }

        //价格表勾选
        private void dgvClientPrc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewSelectedRowCollection row = dgvClientPrc.SelectedRows;
            if (e.ColumnIndex !=17 || e.RowIndex == -1)//第一列才触发事件
                return;
            //获取选中行的值
            GoodPrc info = dgvClientPrc.CurrentRow.DataBoundItem as GoodPrc;
            if (info.SelectFlag == true)
            {
                info.SelectFlag = false;
            }
            else
            {
                info.SelectFlag = true;
            }
        }

        //下拉框查询方法
        private SortableBindingList<SysCode> SysCodeList(Dictionary<string, string> sqlkeydict)
        {
            SortableBindingList<SysCode> infolist = new SortableBindingList<SysCode>();
            infolist = Pdao.GetSysCode(sqlkeydict);
            if (infolist.Count > 0)
            {
                return infolist;
            }
            else
            {
                return null;
            }
        }

        //下拉框绑定数据
        private void cbSelectList()
        {
            SysCode Sdefaultinfo = new SysCode();
            Sdefaultinfo.Name = "-请选择-";

            Dictionary<string, string> Gsqlkeydict = new Dictionary<string, string>();
            Gsqlkeydict.Add("typecode", "grouptype");//客户组分类
            GrouptypeCodeList = SysCodeList(Gsqlkeydict);
            GrouptypeCodeList.Add(Sdefaultinfo);
            cbAGrouptypename.DataSource = GrouptypeCodeList;
            cbAGrouptypename.DisplayMember = "Name";
            cbAGrouptypename.ValueMember = "Code";
            cbAGrouptypename.Text = "-请选择-";


            Dictionary<string, string> Dsqlkeydict = new Dictionary<string, string>();
            Dsqlkeydict.Add("typecode", "detailtype");//客户组类型
            Dsqlkeydict.Add("detailtypemenu", "1");
            DetailtypeCodeList = SysCodeList(Dsqlkeydict);
            DetailtypeCodeList.Add(Sdefaultinfo);
            cbADetailtypename.DataSource = DetailtypeCodeList;
            cbADetailtypename.DisplayMember = "Name";
            cbADetailtypename.ValueMember = "Code";
            cbADetailtypename.Text = "-请选择-";

            Dictionary<string, string> Ssqlkeydict = new Dictionary<string, string>();
            Ssqlkeydict.Add("typecode", "synctype");//客户组同步分类
            SynctypeCodeList = SysCodeList(Ssqlkeydict);
            SynctypeCodeList.Add(Sdefaultinfo);
            cbASynctypename.DataSource = SynctypeCodeList;
            cbASynctypename.DisplayMember = "Name";
            cbASynctypename.ValueMember = "Code";
            cbASynctypename.Text = "-请选择-";

            PubDeptList = dao.GetPubDept();//归属部门
            PubDept Pinfo = new PubDept();
            PubDept Ninfo = new PubDept();
            Ninfo.Deptname = "";
            Pinfo.Deptname = "-请选择-";
            PubDeptList.Add(Pinfo);
            PubDeptList.Add(Ninfo);
            cbASaledeptname.DataSource = PubDeptList;
            cbASaledeptname.DisplayMember = "Deptname";
            cbASaledeptname.ValueMember = "Saledeptid";
            cbASaledeptname.Text = "-请选择-";

        }

        private void addClientGroupBtn_Click(object sender, EventArgs e)
        {
            if (addClientGroupBtn.Enabled == true & updateClientGroupBtn.Enabled == false) return;
            initUI(1);//新增模式

            cbSelectList();//下拉框绑定数据方法

            addClientGroupBtn.Enabled = false;
            toolStrip2.Enabled = false;
        }
        //筛选
        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (dgvClientGroup.RowCount <= 0) return;
            SortableBindingList<CstGroupDtl> TempCstGroupDtlList = new SortableBindingList<CstGroupDtl>();
            TempCstGroupDtlList = CstGroupDtlList;
            if(!string.IsNullOrEmpty(txtFCstcode.Text))
            {
                SortableBindingList<CstGroupDtl> m_list = new SortableBindingList<CstGroupDtl>();
                foreach (CstGroupDtl data in TempCstGroupDtlList)//传入list
                {
                    if (data.Cstcode.IndexOf(txtFCstcode.Text) != -1)
                    {
                        m_list.Add(data);
                    }
                }

                TempCstGroupDtlList = m_list;
            }
            if (!string.IsNullOrEmpty(txtFCSTNAME.Text))
            {
                SortableBindingList<CstGroupDtl> m_list = new SortableBindingList<CstGroupDtl>();
                foreach (CstGroupDtl data in TempCstGroupDtlList)//传入list
                {
                    if (data.CSTNAME.IndexOf(txtFCSTNAME.Text) != -1)
                    {
                        m_list.Add(data);
                    }
                }

                TempCstGroupDtlList = m_list;
            }
            if (!string.IsNullOrEmpty(txtFregion.Text))
            {
                SortableBindingList<CstGroupDtl> m_list = new SortableBindingList<CstGroupDtl>();
                foreach (CstGroupDtl data in TempCstGroupDtlList)//传入list
                {
                    if (data.Region.IndexOf(txtFregion.Text) != -1)
                    {
                        m_list.Add(data);
                    }
                }

                TempCstGroupDtlList = m_list;
            }
            if (!string.IsNullOrEmpty(txtClienttypename.Text))
            {
                SortableBindingList<CstGroupDtl> m_list = new SortableBindingList<CstGroupDtl>();
                foreach (CstGroupDtl data in TempCstGroupDtlList)//传入list
                {
                    if (data.Clienttypename.IndexOf(txtClienttypename.Text) != -1)
                    {
                        m_list.Add(data);
                    }
                }

                TempCstGroupDtlList = m_list;
            }

            dgvClientDtl.DataSource = TempCstGroupDtlList;
            dgvClientDtl.Refresh();
            if (btnAddCstDtl.Enabled == true & btnConfirmCstDtl.Enabled == true &
            btnStartCstDtl.Enabled == true & btnStopCstDtl.Enabled == true)
            {
                btnAddCstDtl.Enabled = false;
                btnConfirmCstDtl.Enabled = false;
                btnStartCstDtl.Enabled = false;
                btnStopCstDtl.Enabled = false;
            }
            else
            {

            }
        }

        //新增明细客户
        private void btnAddCstDtl_Click(object sender, EventArgs e)
        {
            //chkModprc_CheckedChanged(sender, e);
            if (dgvClientGroup.RowCount <= 0) return;
            CstGroupHdr info = dgvClientGroup.CurrentRow.DataBoundItem as CstGroupHdr;
            SelNewClient snc = new SelNewClient();
            snc.Hdrid = int.Parse(info.Hdrid);

            if (dgvClientDtlNew.RowCount <= 0)
            {
                snc.OldClientList = CstGroupDtlList;
            }
            else
            {
                foreach (CstGroupDtl AddOldinfo in CstGroupDtlList)
                {
                    snc.OldClientList.Add(AddOldinfo);
                }
                
                foreach (CstGroupDtl AddOldinfo in AddSelNewList)
                {
                    snc.OldClientList.Add(AddOldinfo);
                }
            }
            snc.ShowDialog();

            SortableBindingList<CstGroupDtl> TempList =new SortableBindingList<CstGroupDtl>();
            if (snc.DialogResult == DialogResult.OK)
            {
                SelNewList = snc.SelClientList;

                if (dgvClientDtl.RowCount == 0)
                {
                    foreach (CstGroupDtl AddNewinfo in SelNewList)
                    {
                        AddSelNewList.Add(AddNewinfo);
                    }
                    //CstGroupDtlList = SelNewList;
                    dgvClientDtlNew.DataSource = AddSelNewList;
                    dgvClientDtlNew.Refresh();
                }
                else
                {
                    foreach (CstGroupDtl AddNewinfo in SelNewList)
                    {
                        AddSelNewList.Add(AddNewinfo);
                    }
                    
                    dgvClientDtlNew.DataSource = AddSelNewList;
                    dgvClientDtlNew.Refresh();
                }

                btnFilter.Enabled = false;
                btnStartCstDtl.Enabled = false;
                btnStopCstDtl.Enabled = false;
            }
            if (txtACode.Text != "")
            {
                btnClientGroupSave.Enabled = true;
                dgvClientGroup.Enabled = false;
            }
        }

        //客户组确认按钮
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (updateClientGroupBtn.Enabled == true)
            {
                CstGroupHdr info = new CstGroupHdr();
                info.Ownerid = Properties.Settings.Default.OWNERID;
                info.Deptname = SessionDto.Ownername;


                if (string.IsNullOrEmpty(txtACode.Text))
                {
                    MessageBox.Show("客户组代码不能为空！", "系统提示！");
                    txtACode.Focus();
                    return;
                }
                else
                {
                    SortableBindingList<CstGroupHdr> CountList = new SortableBindingList<CstGroupHdr>();
                    Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
                    sqlkeydict.Add("code", txtACode.Text.ToString());
                    {
                        CountList = dao.GetCstGroupHdrList(sqlkeydict);
                    }
                    if (CountList.Count == 0)
                    {
                        info.Code = txtACode.Text;
                    }
                    else
                    {
                        MessageBox.Show("客户组代码已存在！请重新输入", "系统提示！");
                        txtACode.Focus();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(txtAName.Text))
                {
                    MessageBox.Show("客户组名称不能为空！", "系统提示！");
                    txtAName.Focus();
                    return;
                }

                else
                {
                    SortableBindingList<CstGroupHdr> CountList = new SortableBindingList<CstGroupHdr>();
                    Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
                    sqlkeydict.Add("name", txtAName.Text.ToString());
                    {
                        CountList = dao.GetCstGroupHdrList(sqlkeydict);
                    }
                    if (CountList.Count == 0)
                    {
                        info.Name = txtAName.Text;
                    }
                    else
                    {
                        MessageBox.Show("客户组名称已存在！请重新输入", "系统提示！");
                        txtAName.Focus();
                        return;
                    }
                }

                try
                {
                    info.Deliveryfeerate = UDADeliveryfeerate.Value.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("请检查输入的默认加点率是否有误！", "程序提示");
                    return;
                }

                if (!string.IsNullOrEmpty(txtAMark.Text))
                {
                    info.Mark = txtAMark.Text;
                }
                if (cbAGrouptypename.Text == "" || cbAGrouptypename.Text == "-请选择-")
                {
                    MessageBox.Show("请选择客户组分类！", "系统提示！");

                    cbAGrouptypename.Focus();
                    return;
                }
                else
                {
                    info.Grouptypename = cbAGrouptypename.Text.ToString();
                    info.Grouptype = cbAGrouptypename.SelectedValue.ToString();
                }
                if (cbADetailtypename.Text == "" || cbADetailtypename.Text == "-请选择-")
                {
                    MessageBox.Show("请选择客户组类型！", "系统提示！");

                    cbADetailtypename.Focus();
                    return;
                }
                else
                {
                    info.Detailtypename = cbADetailtypename.Text.ToString();
                    info.Detailtype = cbADetailtypename.SelectedValue.ToString();
                    if (info.Detailtype == "10")
                    { toolStrip2.Enabled = false; }
                    else { toolStrip2.Enabled = true; }

                }
                if (cbASynctypename.Text == "" || cbASynctypename.Text == "-请选择-")
                {
                    MessageBox.Show("请选择客户同步价格！", "系统提示！");

                    cbASynctypename.Focus();
                    return;
                }
                else
                {
                    info.Synctypename = cbASynctypename.Text.ToString();
                    info.Synctype = cbASynctypename.SelectedValue.ToString();
                }
                if (cbASaledeptname.Visible == true)
                {
                    if (cbASaledeptname.Text == "-请选择-")
                    {
                        MessageBox.Show("请选择归属部门！", "系统提示！");

                        cbASaledeptname.Focus();
                        return;
                    }
                    else
                    {
                        if (cbASaledeptname.Text != "")
                        {
                            info.Saledeptname = cbASaledeptname.Text.ToString();
                            info.Saledeptid = cbASaledeptname.SelectedValue.ToString();
                        }
                        else
                        {
                            info.Saledeptname = "";
                            info.Saledeptid = "";
                        }
                    }
                }
                else
                {
                    info.Saledeptname = "";
                    info.Saledeptid = "";
                }
                if (chkStopflag.Checked == true)
                {
                    info.STOPNAME = "停用";
                    info.Stopflag = "99";
                }
                else
                {
                    info.STOPNAME = "正常";
                    info.Stopflag = "00";
                }
                info.Createuser = SessionDto.Empcode;
                info.Createusername = SessionDto.Empname;
                info.Createdate = DateTime.Now.ToString();
                info.Modifyuser = SessionDto.Empcode;
                info.Modifyusername = SessionDto.Empname;
                info.Modifydate = DateTime.Now.ToString();
                info.Compid = Properties.Settings.Default.COMPID;
                info.Hdrid = dao.GetSeqNum(1).ToString();//获取客户组ID

                SortableBindingList<CstGroupHdr> TempList = new SortableBindingList<CstGroupHdr>();
                TempList.Add(info);
                CstGroupHdrList = TempList;
                if (CstGroupHdrList.Count > 0)
                {
                    initUI(0);
                    btnClientGroupSave.Enabled = true;
                }
                dgvClientGroup.DataSource = CstGroupHdrList;
                dgvClientGroup.Refresh();

                if (info.Detailtype == "10")
                {
                    toolStrip2.Enabled = false;
                }
                else
                {
                    toolStrip2.Enabled = true;
                }

            }
            else//判断为空
            {
                if (string.IsNullOrEmpty(txtACode.Text))
                {
                    MessageBox.Show("客户组代码不能为空！", "系统提示！");
                    txtACode.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtAName.Text))
                {
                    MessageBox.Show("客户组代码不能为空！", "系统提示！");
                    txtAName.Focus();
                    return;
                }

                txtACode.ReadOnly = true;
                txtAName.ReadOnly = true;
                txtAMark.ReadOnly = true;
                chkStopflag.Enabled = false;
                UDADeliveryfeerate.Enabled = false;
                //dgvClientGroup.Enabled = false;
                btnClientGroupSave.Enabled = true;

            }


        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            initUI(3);
        }

        //保存按钮
        private void btnClientGroupSave_Click(object sender, EventArgs e)
        {
            if (dgvClientGroup.RowCount <= 0) return;
            AddSelCstGroupHdrList.Clear();
            CstGroupHdr SelHdrinfo = dgvClientGroup.CurrentRow.DataBoundItem as CstGroupHdr;
            AddSelCstGroupHdrList.Add(SelHdrinfo);
            if (updateClientGroupBtn.Enabled == true)
            {
                if (dgvClientPrcSync.RowCount <= 0)
                {
                    if (txtACode.Text != "" & dgvClientGroup.RowCount > 0)
                    {
                        ConfirmSwitch(0);
                    }
                    else
                    {
                        if (dgvClientGroup.RowCount <= 0) return;
                        else if (btnFilter.Enabled == true & btnAddCstDtl.Enabled == true &
                            btnStopCstDtl.Enabled == true & btnStartCstDtl.Enabled == true) return;
                        else if (btnAddCstDtl.Enabled == true)
                        {
                            ConfirmSwitch(1);
                        }
                        else if (btnStopCstDtl.Enabled == true)
                        {
                            ConfirmSwitch(2);
                        }
                        else if (btnStartCstDtl.Enabled == true)
                        {
                            ConfirmSwitch(3);
                        }
                    }
                }
                else
                {
                    SortableBindingList<ScmPriceExe> ScmTempInfoList = new SortableBindingList<ScmPriceExe>();
                    foreach (CstGroupHdr HdrInfo in AddSelCstGroupHdrList)
                    {
                        foreach (CstGroupDtl DtlInfo in AddSelNewList)
                        {
                            foreach (GoodPrc GPinfo in AddSelNewPrcList)
                            {
                                ScmPriceExe ScmTempinfo = new ScmPriceExe();
                                ScmTempinfo.Compid = GPinfo.Compid;
                                ScmTempinfo.Ownerid = GPinfo.Ownerid;
                                ScmTempinfo.Saledeptid = GPinfo.Saledeptid;
                                ScmTempinfo.Cstid = DtlInfo.Cstid;
                                ScmTempinfo.Hdrid = HdrInfo.Hdrid;
                                ScmTempinfo.Goodid = GPinfo.Goodid;
                                ScmTempinfo.Prc = GPinfo.Prc;
                                ScmTempinfo.Price = GPinfo.Price;
                                ScmTempinfo.Bottomprc = GPinfo.Bottomprc;
                                ScmTempinfo.Bottomprice = GPinfo.Bottomprice;
                                ScmTempinfo.Costprc = GPinfo.Costprc;
                                ScmTempinfo.Costprice = GPinfo.Costprice;
                                ScmTempinfo.Begindate = GPinfo.Begindate;
                                ScmTempinfo.Enddate = GPinfo.Enddate;
                                ScmTempinfo.Goodtapid = GPinfo.Goodtapid;
                                if (addClientGroupBtn.Enabled == false)
                                {
                                    ScmTempinfo.Source = "15";//'11'新增明细;'12'启用明细;'13'停用明细  15  新增客户组
                                }
                                else if (addClientGroupBtn.Enabled == true & btnAddCstDtl.Enabled == true)
                                {
                                    ScmTempinfo.Source = "11";
                                }
                                else if (addClientGroupBtn.Enabled == true & btnStartCstDtl.Enabled == true)
                                {
                                    ScmTempinfo.Source = "12";
                                }
                                else if (addClientGroupBtn.Enabled == true & btnStopCstDtl.Enabled == true)
                                {
                                    ScmTempinfo.Source = "13";
                                }

                                if (addClientGroupBtn.Enabled == true & btnStopCstDtl.Enabled == true)
                                {
                                    ScmTempinfo.Type = "2";//0新增启用;  2停用
                                    ScmTempinfo.Stopflag = "99";//'00'新增启用;'99'停用
                                }
                                else
                                {
                                    ScmTempinfo.Type = "0";//0新增启用;  2停用
                                    ScmTempinfo.Stopflag = "00";//'00'新增启用;'99'停用
                                }
                                ScmTempinfo.Synctype = HdrInfo.Synctype;
                                ScmTempinfo.Grouptype = HdrInfo.Grouptype;
                                ScmTempinfo.Createdate = DateTime.Now.ToString();
                                ScmTempinfo.Costrate = GPinfo.Costrate;
                                ScmTempinfo.PrcPri = HdrInfo.Grouptype;

                                ScmTempInfoList.Add(ScmTempinfo);


                            }
                        }
                    }

                    SortableBindingList<ScmClientsGroupTemp> ScmGroupTempList = new SortableBindingList<ScmClientsGroupTemp>();
                    foreach (CstGroupHdr Hdrinfo in AddSelCstGroupHdrList)
                    {
                        foreach (CstGroupDtl Dtlinfo in AddSelNewList)
                        {
                            ScmClientsGroupTemp Tempinfo = new ScmClientsGroupTemp();
                            Tempinfo.Batchid = "";
                            Tempinfo.Hdrid = Hdrinfo.Hdrid.ToString();
                            Tempinfo.Compid = Properties.Settings.Default.COMPID;
                            Tempinfo.Ownerid = Properties.Settings.Default.OWNERID;
                            Tempinfo.Saledeptid = Hdrinfo.Saledeptid;
                            Tempinfo.Code = Hdrinfo.Code;
                            Tempinfo.Name = Hdrinfo.Name;
                            Tempinfo.Detailtype = Hdrinfo.Detailtype;
                            Tempinfo.Grouptype = Hdrinfo.Grouptype;
                            Tempinfo.Synctype = Hdrinfo.Synctype;
                            Tempinfo.Attachcode = "";
                            Tempinfo.Deliveryfeerate = Hdrinfo.Deliveryfeerate;
                            Tempinfo.Mark = Hdrinfo.Mark;
                            Tempinfo.Hdrstopflag = Hdrinfo.Stopflag;
                            Tempinfo.Dtlid = "";
                            Tempinfo.Cstid = Dtlinfo.Cstid;
                            Tempinfo.Modifyprctype = "00";
                            if (btnStopCstDtl.Enabled == true)
                            {
                                Tempinfo.Dtlstopflag = "99";//
                            }
                            else
                            {
                                Tempinfo.Dtlstopflag = "00";//
                            }
                            Tempinfo.Empid = SessionDto.Empid;
                            if (btnAddCstDtl.Enabled == true)
                            {
                                Tempinfo.Operatype = "2";//
                            }
                            else if (btnStartCstDtl.Enabled == true)
                            {
                                Tempinfo.Operatype = "4";//
                            }
                            else if (btnStopCstDtl.Enabled == true)
                            {
                                Tempinfo.Operatype = "3";//
                            }
                            Tempinfo.Createdate = DateTime.Now.ToString();
                            Tempinfo.HdrSaledeptid = Hdrinfo.Saledeptid;
                            Tempinfo.DtlSaledeptid = Hdrinfo.Saledeptid;

                            ScmGroupTempList.Add(Tempinfo);
                        }
                    }
                    SPRetInfo ret = new SPRetInfo();
                    dao.PCstGroupInfoDtl(ScmGroupTempList, ScmTempInfoList, ret);
                    if (ret.num == "1")
                    {
                        MessageBox.Show("提交成功！" + ret.msg + "|" + ret.result, "后台提示！");
                        toolStrip2.Enabled = true;
                        toolStrip1.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("提交失败" + ret.msg + "|" + ret.result, "后台提示！");
                        return;
                    }

                    initUI(3);

                }
            }
            else
            {
                CstGroupHdr info = dgvClientGroup.CurrentRow.DataBoundItem as CstGroupHdr;
                int Hdrid = int.Parse(info.Hdrid);
                string Code = txtACode.Text; 
                string Name = txtAName.Text;
                decimal Deliveryfeerate = decimal.Parse(UDADeliveryfeerate.Value.ToString());
                string Mark = txtAMark.Text;
                string Stopflag;
                if (chkStopflag.Checked == true)
                {
                    Stopflag = "99";
                }
                else
                {
                    Stopflag = "00";
                }
                SPRetInfo ret = new SPRetInfo();
                dao.PCstGroupUpdate(Hdrid, Code, Name, Deliveryfeerate, Mark, Stopflag, ret);
                if (ret.num == "1")
                {
                    MessageBox.Show("提交成功！" + ret.msg + "|" + ret.result, "后台提示！");
                    toolStrip2.Enabled = true;
                    toolStrip1.Enabled = true;
                }
                else
                {
                    MessageBox.Show("提交失败" + ret.msg + "|" + ret.result, "后台提示！");
                    return;
                }

                initUI(3);
            }
            updateClientGroupBtn.Enabled = true;
            addClientGroupBtn.Enabled = true;
            dgvClientGroup.Enabled = true;
        }

        //确认按钮事件
        private void ConfirmSwitch(int astate)
        {
            switch (astate)
            {
                case 0://新增客户组
                    if (cbADetailtypename.SelectedValue.ToString() != "10")
                    {
                        if (AddSelNewList.Count <= 0)
                        {
                            MessageBox.Show("客户信息未录入！保存失败！", "系统提示！");
                            return;
                        }
                        else//有明细
                        {
                            SortableBindingList<ScmClientsGroupTemp> TempList = new SortableBindingList<ScmClientsGroupTemp>();
                            foreach (CstGroupHdr Hdrinfo in AddSelCstGroupHdrList)
                            {
                                foreach (CstGroupDtl Dtlinfo in AddSelNewList)
                                {
                                    ScmClientsGroupTemp Tempinfo = new ScmClientsGroupTemp();
                                    Tempinfo.Batchid = "";
                                    Tempinfo.Hdrid = Hdrinfo.Hdrid.ToString();
                                    Tempinfo.Compid = Properties.Settings.Default.COMPID;
                                    Tempinfo.Ownerid = Properties.Settings.Default.OWNERID;
                                    Tempinfo.Saledeptid = Hdrinfo.Saledeptid;
                                    Tempinfo.Code = Hdrinfo.Code;
                                    Tempinfo.Name = Hdrinfo.Name;
                                    Tempinfo.Detailtype = Hdrinfo.Detailtype;
                                    Tempinfo.Grouptype = Hdrinfo.Grouptype;
                                    Tempinfo.Synctype = Hdrinfo.Synctype;
                                    Tempinfo.Attachcode = "";
                                    Tempinfo.Deliveryfeerate = Hdrinfo.Deliveryfeerate;
                                    Tempinfo.Mark = Hdrinfo.Mark;
                                    Tempinfo.Hdrstopflag = Hdrinfo.Stopflag;
                                    Tempinfo.Dtlid = "";
                                    Tempinfo.Cstid = Dtlinfo.Cstid;
                                    Tempinfo.Modifyprctype = "10";
                                    Tempinfo.Dtlstopflag = "00";
                                    Tempinfo.Empid = SessionDto.Empid;
                                    Tempinfo.Operatype = "1";
                                    Tempinfo.Createdate = DateTime.Now.ToString();
                                    Tempinfo.HdrSaledeptid = Hdrinfo.Saledeptid;
                                    Tempinfo.DtlSaledeptid = Hdrinfo.Saledeptid;

                                    TempList.Add(Tempinfo);
                                }
                            }

                            SPRetInfo ret = new SPRetInfo();
                            dao.PCstGroupInfo(TempList, ret);
                            if (ret.num == "1")
                            {
                                MessageBox.Show("提交成功！" + ret.msg + "|" + ret.result, "后台提示！");
                                toolStrip2.Enabled = true;
                                toolStrip1.Enabled = true;
                            }
                            else
                            {
                                MessageBox.Show("提交失败" + ret.msg + "|" + ret.result, "后台提示！");
                                return;
                            }

                            initUI(4);

                        }

                    }
                    else//无明细
                    {
                        if (AddSelNewList.Count > 0)
                        {
                            MessageBox.Show("无固定明细类型不允许录入客户信息！保存失败！", "系统提示！");
                            return;
                        }
                        else
                        {
                            SortableBindingList<ScmClientsGroupTemp> TempList = new SortableBindingList<ScmClientsGroupTemp>();
                            foreach (CstGroupHdr Hdrinfo in AddSelCstGroupHdrList)
                            {
                                //foreach (CstGroupDtl Dtlinfo in AddSelNewList)
                                //{
                                    ScmClientsGroupTemp Tempinfo = new ScmClientsGroupTemp();
                                    Tempinfo.Batchid = "";
                                    Tempinfo.Hdrid = Hdrinfo.Hdrid.ToString();
                                    Tempinfo.Compid = Properties.Settings.Default.COMPID;
                                    Tempinfo.Ownerid = Properties.Settings.Default.OWNERID;
                                    Tempinfo.Saledeptid = Hdrinfo.Saledeptid;
                                    Tempinfo.Code = Hdrinfo.Code;
                                    Tempinfo.Name = Hdrinfo.Name;
                                    Tempinfo.Detailtype = Hdrinfo.Detailtype;
                                    Tempinfo.Grouptype = Hdrinfo.Grouptype;
                                    Tempinfo.Synctype = Hdrinfo.Synctype;
                                    Tempinfo.Attachcode = "";
                                    Tempinfo.Deliveryfeerate = Hdrinfo.Deliveryfeerate;
                                    Tempinfo.Mark = Hdrinfo.Mark;
                                    Tempinfo.Hdrstopflag = Hdrinfo.Stopflag;
                                    Tempinfo.Dtlid = "";
                                    Tempinfo.Cstid = "";
                                    Tempinfo.Modifyprctype = "10";
                                    Tempinfo.Dtlstopflag = "";
                                    Tempinfo.Empid = SessionDto.Empid;
                                    Tempinfo.Operatype = "1";
                                    Tempinfo.Createdate = DateTime.Now.ToString();
                                    Tempinfo.HdrSaledeptid = Hdrinfo.Saledeptid;
                                    Tempinfo.DtlSaledeptid = Hdrinfo.Saledeptid;

                                    TempList.Add(Tempinfo);
                                //}
                            }

                            SPRetInfo ret = new SPRetInfo();
                            dao.PCstGroupInfo(TempList, ret);
                            if (ret.num == "1")
                            {
                                MessageBox.Show("提交成功！" + ret.msg + "|" + ret.result, "后台提示！");
                                toolStrip2.Enabled = true;
                                toolStrip1.Enabled = true;
                            }
                            else
                            {
                                MessageBox.Show("提交失败" + ret.msg + "|" + ret.result, "后台提示！");
                                return;
                            }

                            initUI(4);
                        }

                    }
                   
                    break;
                case 1://已有客户组添加客户
                    CstGroupHdr AddSelHdrinfo = dgvClientGroup.CurrentRow.DataBoundItem as CstGroupHdr;
                    SortableBindingList<CstGroupHdr> AddTempHdrList = new SortableBindingList<CstGroupHdr>();
                    AddTempHdrList.Add(AddSelHdrinfo);

                    if (AddSelHdrinfo.Detailtypename != "10")
                    {
                        if (CstGroupDtlList.Count <= 0)
                        {
                            MessageBox.Show("客户信息未录入！保存失败！", "系统提示！");
                            return;
                        }

                    }
                    else
                    {
                        if (CstGroupDtlList.Count > 0)
                        {
                            MessageBox.Show("无固定明细类型不允许录入客户信息！保存失败！", "系统提示！");
                            return;
                        }
                    }
                    SortableBindingList<ScmClientsGroupTemp> AddTempList = new SortableBindingList<ScmClientsGroupTemp>();
                    foreach (CstGroupHdr AddHdrinfo in AddTempHdrList)
                    {
                        foreach (CstGroupDtl AddDtlinfo in AddSelNewList)
                        {
                            ScmClientsGroupTemp Tempinfo = new ScmClientsGroupTemp();
                            Tempinfo.Batchid = "";
                            Tempinfo.Hdrid = AddHdrinfo.Hdrid;
                            Tempinfo.Compid = Properties.Settings.Default.COMPID;
                            Tempinfo.Ownerid = Properties.Settings.Default.OWNERID;
                            Tempinfo.Saledeptid = AddHdrinfo.Saledeptid;
                            Tempinfo.Code = AddHdrinfo.Code;
                            Tempinfo.Name = AddHdrinfo.Name;
                            Tempinfo.Detailtype = AddHdrinfo.Detailtype;
                            Tempinfo.Grouptype = AddHdrinfo.Grouptype;
                            Tempinfo.Synctype = AddHdrinfo.Synctype;
                            Tempinfo.Attachcode = "";
                            Tempinfo.Deliveryfeerate = AddHdrinfo.Deliveryfeerate;
                            Tempinfo.Mark = AddHdrinfo.Mark;
                            Tempinfo.Hdrstopflag = AddHdrinfo.Stopflag;
                            Tempinfo.Dtlid = "";
                            Tempinfo.Cstid = AddDtlinfo.Cstid;
                            Tempinfo.Modifyprctype = "10";
                            Tempinfo.Dtlstopflag = "00";
                            Tempinfo.Empid = SessionDto.Empid;
                            Tempinfo.Operatype = "2";
                            Tempinfo.Createdate = DateTime.Now.ToString();
                            Tempinfo.HdrSaledeptid = AddHdrinfo.Saledeptid;
                            Tempinfo.DtlSaledeptid = AddHdrinfo.Saledeptid;

                            AddTempList.Add(Tempinfo);
                        }
                    }

                    SPRetInfo Addret = new SPRetInfo();
                    dao.PCstGroupInfo(AddTempList, Addret);
                    if (Addret.num == "1")
                    {
                        MessageBox.Show("提交成功！" + Addret.msg + "|" + Addret.result, "后台提示！");
                        toolStrip2.Enabled = true;
                        toolStrip1.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("提交失败" + Addret.msg + "|" + Addret.result, "后台提示！");
                        return;
                    }

                    initUI(4);
                    dgvClientGroup.Enabled = true;

                    break;
                    //----------------------------------------------------------------------------
                case 2://停用客户
                    CstGroupHdr StopSelHdrinfo = dgvClientGroup.CurrentRow.DataBoundItem as CstGroupHdr;
                    SortableBindingList<CstGroupHdr> StopTempHdrList = new SortableBindingList<CstGroupHdr>();
                    StopTempHdrList.Add(StopSelHdrinfo);
                    
                    SortableBindingList<ScmClientsGroupTemp> StopTempList = new SortableBindingList<ScmClientsGroupTemp>();
                    foreach (CstGroupHdr StopHdrinfo in StopTempHdrList)
                    {
                        foreach (CstGroupDtl StopDtlinfo in AddSelNewList)
                        {
                            ScmClientsGroupTemp Tempinfo = new ScmClientsGroupTemp();
                            Tempinfo.Batchid = "";
                            Tempinfo.Hdrid = StopHdrinfo.Hdrid;
                            Tempinfo.Compid = Properties.Settings.Default.COMPID;
                            Tempinfo.Ownerid = Properties.Settings.Default.OWNERID;
                            Tempinfo.Saledeptid = StopHdrinfo.Saledeptid;
                            Tempinfo.Code = StopHdrinfo.Code;
                            Tempinfo.Name = StopHdrinfo.Name;
                            Tempinfo.Detailtype = StopHdrinfo.Detailtype;
                            Tempinfo.Grouptype = StopHdrinfo.Grouptype;
                            Tempinfo.Synctype = StopHdrinfo.Synctype;
                            Tempinfo.Attachcode = "";
                            Tempinfo.Deliveryfeerate = StopHdrinfo.Deliveryfeerate;
                            Tempinfo.Mark = StopHdrinfo.Mark;
                            Tempinfo.Hdrstopflag = StopHdrinfo.Stopflag;
                            Tempinfo.Dtlid = "";
                            Tempinfo.Cstid = StopDtlinfo.Cstid;
                            Tempinfo.Modifyprctype = "10";
                            Tempinfo.Dtlstopflag = "99";
                            Tempinfo.Empid = SessionDto.Empid;
                            Tempinfo.Operatype = "3";
                            Tempinfo.Createdate = DateTime.Now.ToString();
                            Tempinfo.HdrSaledeptid = StopHdrinfo.Saledeptid;
                            Tempinfo.DtlSaledeptid = StopHdrinfo.Saledeptid;

                            StopTempList.Add(Tempinfo);
                        }
                    }

                    SPRetInfo Stopret = new SPRetInfo();
                    dao.PCstGroupInfo(StopTempList,Stopret);
                    if (Stopret.num == "1")
                    {
                        MessageBox.Show("提交成功！" + Stopret.msg + "|" + Stopret.result, "后台提示！");
                        toolStrip2.Enabled = true;
                        toolStrip1.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("提交失败" + Stopret.msg + "|" + Stopret.result, "后台提示！");
                        return;
                    }

                    initUI(4);
                    dgvClientGroup.Enabled = true;

                    break;

                    //-----------------------------------------------------------------------
                case 3://启用客户

                    CstGroupHdr StartSelHdrinfo = dgvClientGroup.CurrentRow.DataBoundItem as CstGroupHdr;
                    SortableBindingList<CstGroupHdr> StartTempHdrList = new SortableBindingList<CstGroupHdr>();
                    StartTempHdrList.Add(StartSelHdrinfo);
                    
                    SortableBindingList<ScmClientsGroupTemp> StartTempList = new SortableBindingList<ScmClientsGroupTemp>();
                    foreach (CstGroupHdr StartHdrinfo in StartTempHdrList)
                    {
                        foreach (CstGroupDtl StartDtlinfo in AddSelNewList)
                        {
                            ScmClientsGroupTemp Tempinfo = new ScmClientsGroupTemp();
                            Tempinfo.Batchid = "";
                            Tempinfo.Hdrid = StartHdrinfo.Hdrid;
                            Tempinfo.Compid = Properties.Settings.Default.COMPID;
                            Tempinfo.Ownerid = Properties.Settings.Default.OWNERID;
                            Tempinfo.Saledeptid = StartHdrinfo.Saledeptid;
                            Tempinfo.Code = StartHdrinfo.Code;
                            Tempinfo.Name = StartHdrinfo.Name;
                            Tempinfo.Detailtype = StartHdrinfo.Detailtype;
                            Tempinfo.Grouptype = StartHdrinfo.Grouptype;
                            Tempinfo.Synctype = StartHdrinfo.Synctype;
                            Tempinfo.Attachcode = "";
                            Tempinfo.Deliveryfeerate = StartHdrinfo.Deliveryfeerate;
                            Tempinfo.Mark = StartHdrinfo.Mark;
                            Tempinfo.Hdrstopflag = StartHdrinfo.Stopflag;
                            Tempinfo.Dtlid = "";
                            Tempinfo.Cstid = StartDtlinfo.Cstid;
                            Tempinfo.Modifyprctype = "10";
                            Tempinfo.Dtlstopflag = "00";
                            Tempinfo.Empid = SessionDto.Empid;
                            Tempinfo.Operatype = "4";
                            Tempinfo.Createdate = DateTime.Now.ToString();
                            Tempinfo.HdrSaledeptid = StartHdrinfo.Saledeptid;
                            Tempinfo.DtlSaledeptid = StartHdrinfo.Saledeptid;

                            StartTempList.Add(Tempinfo);
                        }
                    }

                    SPRetInfo Startret = new SPRetInfo();
                    dao.PCstGroupInfo(StartTempList, Startret);
                    if (Startret.num == "1")
                    {
                        MessageBox.Show("提交成功！" + Startret.msg + "|" + Startret.result, "后台提示！");
                        toolStrip2.Enabled = true;
                        toolStrip1.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("提交失败" + Startret.msg + "|" + Startret.result, "后台提示！");
                        return;
                    }

                    initUI(4);
                    dgvClientGroup.Enabled = true;

                    break;
            }
        }

        //明细确认按钮
        private void btnConfirmCstDtl_Click(object sender, EventArgs e)
        {
           
            if (dgvClientGroup.RowCount <= 0) return;
            else if (btnFilter.Enabled == true & btnAddCstDtl.Enabled == true &
                btnStopCstDtl.Enabled == true & btnStartCstDtl.Enabled == true) return;
            else if (btnAddCstDtl.Enabled == true)//新增客户
            {
                btnClientGroupSave.Enabled = true;
                btnConfirmCstDtl.Enabled = false;
                btnConfirmCstDtl.Enabled = false;
            }
            else if (btnStopCstDtl.Enabled == true)//停用客户
            {
                object Stopobj2 = FormUtils.SelectRows(dgvClientDtl);
                if (Stopobj2 == null) return;
                SortableBindingList<CstGroupDtl> StopCstList = new SortableBindingList<CstGroupDtl>();
                DataGridViewSelectedRowCollection Stoprow = (DataGridViewSelectedRowCollection)Stopobj2;
                StopCstList.Clear();
                foreach (DataGridViewRow dgvr in Stoprow)
                {
                    StopCstList.Add((CstGroupDtl)dgvr.Cells[0].Value);
                }
                AddSelNewList = StopCstList;
                dgvClientDtlNew.DataSource = AddSelNewList;
                dgvClientDtlNew.Refresh();

                btnClientGroupSave.Enabled = true;
                btnConfirmCstDtl.Enabled = false;
                btnConfirmCstDtl.Enabled = false;
            }
            else if (btnStartCstDtl.Enabled == true)//启用客户
            {
                object Startobj2 = FormUtils.SelectRows(dgvClientDtl);
                if (Startobj2 == null) return;
                SortableBindingList<CstGroupDtl> StartCstList = new SortableBindingList<CstGroupDtl>();
                DataGridViewSelectedRowCollection Startrow = (DataGridViewSelectedRowCollection)Startobj2;
                StartCstList.Clear();
                foreach (DataGridViewRow dgvr in Startrow)
                {
                    StartCstList.Add((CstGroupDtl)dgvr.Cells[0].Value);
                }
                AddSelNewList = StartCstList;
                dgvClientDtlNew.DataSource = AddSelNewList;
                dgvClientDtlNew.Refresh();

                btnClientGroupSave.Enabled = true;
                btnConfirmCstDtl.Enabled = false;
                btnConfirmCstDtl.Enabled = false;
            }
            chkModprc_CheckedChanged(sender, e);
            chkModprc.Enabled = false;
        }

        //停用客户
        private void btnStopCstDtl_Click(object sender, EventArgs e)
        {
            if (dgvClientGroup.RowCount <= 0) return;
            //筛选出正常数据
            else
            {
                SortableBindingList<CstGroupDtl> m_list = new SortableBindingList<CstGroupDtl>();
                foreach (CstGroupDtl data in CstGroupDtlList)//传入list
                {
                    if (data.STOPNAME.IndexOf("正常") != -1)
                    {
                        m_list.Add(data);
                    }
                }

                CstGroupDtlList = m_list;
            }

            dgvClientDtl.DataSource = CstGroupDtlList;
            dgvClientDtl.Refresh();

            btnAddCstDtl.Enabled = false;
            btnConfirmCstDtl.Enabled = true;
            btnStartCstDtl.Enabled = false;
            btnStopCstDtl.Enabled = true;


        }

        //启用客户
        private void btnStartCstDtl_Click(object sender, EventArgs e)
        {
            if (dgvClientGroup.RowCount <= 0) return;
            //筛选出停用数据
            else
            {
                SortableBindingList<CstGroupDtl> m_list = new SortableBindingList<CstGroupDtl>();
                foreach (CstGroupDtl data in CstGroupDtlList)//传入list
                {
                    if (data.STOPNAME.IndexOf("停用") != -1)
                    {
                        m_list.Add(data);
                    }
                }

                CstGroupDtlList = m_list;
            }

            dgvClientDtl.DataSource = CstGroupDtlList;
            dgvClientDtl.Refresh();

            btnAddCstDtl.Enabled = false;
            btnConfirmCstDtl.Enabled = true;
            btnStartCstDtl.Enabled = true;
            btnStopCstDtl.Enabled = false;
        }


        //筛选商品
        private void btnGselect_Click(object sender, EventArgs e)
        {
            if (dgvClientPrc.RowCount <= 0) return;

            SortableBindingList<GoodPrc> TempGoodPrcList = new SortableBindingList<GoodPrc>();
            TempGoodPrcList = CstGoodPrcList;
            if (!string.IsNullOrEmpty(txtGGoods.Text))
            {
                SortableBindingList<GoodPrc> m_list = new SortableBindingList<GoodPrc>();
                foreach (GoodPrc data in TempGoodPrcList)//传入list
                {
                    if (data.Goods.IndexOf(txtGGoods.Text) != -1)
                    {
                        m_list.Add(data);
                    }
                }

                TempGoodPrcList = m_list;
            }
            if (!string.IsNullOrEmpty(txtGName.Text))
            {
                SortableBindingList<GoodPrc> m_list = new SortableBindingList<GoodPrc>();
                foreach (GoodPrc data in TempGoodPrcList)//传入list
                {
                    if (data.Name.IndexOf(txtGName.Text) != -1)
                    {
                        m_list.Add(data);
                    }
                }

                TempGoodPrcList = m_list;
            }
            if (!string.IsNullOrEmpty(txtGProducer.Text))
            {
                SortableBindingList<GoodPrc> m_list = new SortableBindingList<GoodPrc>();
                foreach (GoodPrc data in TempGoodPrcList)//传入list
                {
                    if (data.Producer.IndexOf(txtGProducer.Text) != -1)
                    {
                        m_list.Add(data);
                    }
                }

                TempGoodPrcList = m_list;
            }

            dgvClientPrc.DataSource = TempGoodPrcList;
            dgvClientPrc.Refresh();
        }

        //同步价格勾选可见
        private void chkModprc_CheckedChanged(object sender, EventArgs e)
        {
            if (dgvClientGroup.RowCount <= 0) return;
            if (chkModprc.Checked == true)
            {//1默认全选品种；2可以自选商品；3默认全选商品,在已选界面有撤销功能
                if (PubOwnerConfigureDto.Goodchoose == "1")
                {
                    if (CstGoodPrcList.Count > 0)
                    {
                        foreach (GoodPrc prcinfo in CstGoodPrcList)
                        {
                            AddSelNewPrcList.Add(prcinfo);
                        }
                        dgvClientPrcSync.DataSource = AddSelNewPrcList;
                        dgvClientPrcSync.Refresh();
                        dgvClientPrcSync.Enabled = false;
                    }

                }
                else if (PubOwnerConfigureDto.Goodchoose == "2")
                {
                    dgvClientPrcSync.DataSource = AddSelNewPrcList;
                    dgvClientPrcSync.Refresh();
                }
                else if (PubOwnerConfigureDto.Goodchoose == "3")
                {
                    if (CstGoodPrcList.Count > 0)
                    {
                        foreach (GoodPrc prcinfo in CstGoodPrcList)
                        {
                            AddSelNewPrcList.Add(prcinfo);
                        }
                        dgvClientPrcSync.DataSource = AddSelNewPrcList;
                        dgvClientPrcSync.Refresh();
                        dgvClientPrcSync.Enabled = true;
                    }
                }

            }
            else
            {
                AddSelNewPrcList.Clear();
                dgvClientPrcSync.DataSource = AddSelNewPrcList;
                dgvClientPrcSync.Refresh();
                dgvClientPrcSync.Enabled = true;
            }
        }

        //同步价格添加按钮,只针对Goodchoose="2"时的情况
        private void btnSyncAdd_Click(object sender, EventArgs e)
        {
            if (dgvClientPrcSync.Enabled == false) return;
            object Prcobj2 = FormUtils.SelectRows(dgvClientPrc);
            if (Prcobj2 == null) return;
            SortableBindingList<GoodPrc> TempPrcList = new SortableBindingList<GoodPrc>();
            DataGridViewSelectedRowCollection Prcrow = (DataGridViewSelectedRowCollection)Prcobj2;
            //临时表
            foreach (DataGridViewRow dgvr in Prcrow)
            {
                TempPrcList.Add((GoodPrc)dgvr.Cells[0].Value);
            }
            //对比是否已存在
            bool b = false;
            string Warming = "";
            foreach (GoodPrc OldInfo in AddSelNewPrcList)
            {
                foreach (GoodPrc SelInfo in TempPrcList)
                {
                    if (OldInfo.Goodid == SelInfo.Goodid)
                    {
                        Warming = Warming + SelInfo.Goodid + ",";
                        b = true;
                        break;
                    }
                    b = false;
                }
            }
            if (Warming != "")
            {
                MessageBox.Show("商品代码" + Warming + "已存在！新增失败！", "系统提示！");
                return;
            }
            else
            {
                //对比完成之后再添加
                foreach (GoodPrc info in TempPrcList)
                {
                    AddSelNewPrcList.Add(info);
                }
                
            }
            
            dgvClientDtlNew.DataSource = AddSelNewPrcList;
            dgvClientDtlNew.Refresh();
        }

        //双击剔除同步商品价格
        private void dgvClientPrcSync_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvClientPrcSync.RowCount <= 0) return;
            DataGridViewSelectedRowCollection row = dgvClientPrcSync.SelectedRows;
            if (e.ColumnIndex < 1 || e.RowIndex == -1)//第一列才触发事件
                return;
           
              //获取选中行的值
            GoodPrc info = dgvClientPrcSync.CurrentRow.DataBoundItem as GoodPrc;
            foreach (GoodPrc delinfo in AddSelNewPrcList)
            {
                if (delinfo == info)
                {
                    AddSelNewPrcList.Remove(delinfo);
                    break;
                }
            }
            dgvClientPrcSync.DataSource = AddSelNewPrcList;
            dgvClientPrcSync.Refresh();
           
        }

        //修改客户组
        private void updateClientGroupBtn_Click(object sender, EventArgs e)
        {
            if (dgvClientGroup.RowCount <= 0) return;
            if (addClientGroupBtn.Enabled == false & updateClientGroupBtn.Enabled == true) return;
            CstGroupHdr info = dgvClientGroup.CurrentRow.DataBoundItem as CstGroupHdr;
            txtACode.Text = info.Code;
            txtACode.ReadOnly = false;
            txtAName.Text = info.Name;
            txtAName.ReadOnly = false;
            txtAMark.Text = info.Mark;
            txtAMark.ReadOnly = false;
            UDADeliveryfeerate.Value = decimal.Parse(info.Deliveryfeerate);
            chkStopflag.Enabled = true;
            if (info.Stopflag == "00")
            {
                chkStopflag.Checked = false;
            }
            else
            {
                chkStopflag.Checked = true;
            }
            UDADeliveryfeerate.Enabled = true;
            cbSelectList();//下拉框绑定数据

            btnConfirm.Enabled = true;

            cbADetailtypename.Text = info.Detailtypename;
            cbAGrouptypename.Text = info.Grouptypename;
            cbASaledeptname.Text = info.Saledeptname;
            cbASynctypename.Text = info.Synctypename;

            cbADetailtypename.Enabled = false;
            cbAGrouptypename.Enabled = false;
            cbASaledeptname.Enabled = false;
            cbASynctypename.Enabled = false;

            updateClientGroupBtn.Enabled = false;
            dgvClientGroup.Enabled = false;
        }
    }
}

using PriceManager.WebReference;
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
    public partial class ContractForm : Form
    {      
        public SortableBindingList<InoutGenCmsbillXlstemp> dataList;
        public string batchid;
        public string cstid;
        APDao_B2BTools dao = new APDao_B2BTools();

        SortableBindingList<ContractInfo> infoList = new SortableBindingList<ContractInfo>();

        List<CheckInfo> ParaList = new List<CheckInfo>();
        SortableBindingList<ContractInfo> CheckedList = new SortableBindingList<ContractInfo>();

        List<CommitInfo> ParaCommitList = new List<CommitInfo>();
        SortableBindingList<ContractInfo> ImportedList = new SortableBindingList<ContractInfo>();

        SortableBindingList<ContractInfo> CommitedList = new SortableBindingList<ContractInfo>();

        SortableBindingList<ContractInfo> CommitList = new SortableBindingList<ContractInfo>();

        Dictionary<string, string> BillingTypeDic = new Dictionary<string, string>();
        List<string> billingType = new List<string>();

        Dictionary<string, string> payDic = new Dictionary<string, string>();
        List<string> pay = new List<string>();

        Dictionary<string, string> transDic = new Dictionary<string, string>();
        List<string> trans = new List<string>();

        Dictionary<string, string> sendAdrDic = new Dictionary<string, string>();
        List<string> sendAdr = new List<string>();



        SPRetInfo retinfo = new SPRetInfo();
        string excelsqlid = "";

        public ContractForm()
        {
            InitializeComponent();
            
        }
        private void ContractForm_Load(object sender, EventArgs e)
        {
            
            //加载开单方式下拉框
            BillingTypeDic = dao.getBillingType();
            // 遍历字典中的值
            foreach (var item in BillingTypeDic.Values)
            {
                billingType.Add(item);
            }
            this.cBBilingType.DataSource = billingType;
         
            //加载支付方式下拉框
            payDic = dao.getPayType();
            // 遍历字典中的值
            foreach (var item in payDic.Values)
            {
                pay.Add(item);

            }
            this.cBPay.DataSource = pay;

            //加载发运方式下拉框
            transDic = dao.getTransType();
            // 遍历字典中的值
            foreach (var item in transDic.Values)
            {
                trans.Add(item);

            }
            this.cBtrans.DataSource = trans;

            //加载送货地址下拉框
            sendAdrDic = dao.GetSendAdr(cstid);
            // 遍历字典中的值
            foreach (var item in sendAdrDic.Values)
            {
                sendAdr.Add(item);
            }
            this.cBSendAdr.DataSource = sendAdr;

            infoList.Clear();
            foreach (InoutGenCmsbillXlstemp ins in dataList)
            {
                ContractInfo info = new ContractInfo();
                batchid = ins.Batchid;
                excelsqlid = ins.ExcelSeqid;
                info.Batchid = ins.Batchid;                
                info.ExcelSeqid = ins.ExcelSeqid;
                info.Goods = ins.Goods;
                info.GoodName = ins.GoodName;
                info.Spec = ins.Spec;
                info.Producer = ins.Producer;
                info.Importprc = ins.Importprc;
                info.Plancount = ins.Plancount;
                info.Importmsg = ins.Importmsg;
                info.Importstate = ins.Importstate;
                info.Pmark = ins.Pmark;
                info.Amark = ins.Amark;
                infoList.Add(info);
            }
            dgvContractInfo.DataSource = infoList;
            dgvContractInfo.Refresh();

            CbId inf = new CbId();
            //调用储存过程  获取下拉框的默认值
            inf=dao.GetCbId(cstid,retinfo);
            if (retinfo.num != "1")
            {
                MessageBox.Show(retinfo.msg,"后台提示");
            }
            else {
                string keyTrans = inf.Transid;
                string keyKd = inf.Kdfs;
                string keyPay = inf.Payid;
                cBtrans.Text = transDic[keyTrans];
                cBBilingType.Text = BillingTypeDic[keyKd];
                cBPay.Text = payDic[keyPay];
            }
        }
        //检验事件
        private void BtnCheck_Click(object sender, EventArgs e)
        {
            SPRetInfo retinfos = new SPRetInfo();
            ParaList.Clear();
            CheckedList.Clear();
            int ret = -1234;
            string retmessage = "";
            string webService = "";
            string LinkType="WEBCHK001";
            string LinkComp = "CMSGD";
            //1.调用 生成字串入参
            ParaList=dao.GetInpara(batchid,retinfos);
            if (retinfos.num!="1") {
                MessageBox.Show(retinfos.msg,"后台提示");
                return;
            }
            //2.循环调WebServer 获取返回参数
            foreach (CheckInfo ins in ParaList) {
                string Inpara = ins.CheckPara;
                WebReference.WebService chk = new WebReference.WebService();
                DataTable s = new DataTable();
                try
                {
                    webService = Convert.ToString(chk.GetData(LinkType, LinkComp, Inpara, out ret, out retmessage));
                }
                catch (Exception ex)
                {
                    string a = ex.ToString();
                    MessageBox.Show(a);
                    return;
                }
                //3.将返回的参数分别更新到中间表中
                dao.UpdateCheckMsg(ins.Batchid,ins.ExcelSeqid,ret,retmessage);
            }
            //4.再次查询中间表 
            CheckedList=dao.GetCheckedInfo(batchid);
            dgvContractInfo.DataSource = CheckedList;
            dgvContractInfo.Refresh();
            BtnCommit.Enabled = true;
        }

        //提交事件
        private void BtnCommit_Click(object sender, EventArgs e)
        {
            int count = 0;//失败的个数
            ParaCommitList.Clear();
            ImportedList.Clear();
            CommitedList.Clear();
            int ret = -1234;
            string retmessage = "";
            string webService = "";
            string LinkType = "";
            string LinkComp = "CMSGD";
            WebReference.WebService chk = new WebReference.WebService();

            //1.调用存储过程 生成3个入参字符串
            ParaCommitList =dao.GetCommit(batchid, retinfo);
            ////2.调用一次WebServer 清除接口表数据
            CommitInfo ins = ParaCommitList[0];
            //string import_para1 = ins.ImportParas;
            //LinkType = "WEBSAL002";
            //try
            //{
            //    webService = Convert.ToString(chk.GetData(LinkType, LinkComp, import_para1, out ret, out retmessage));
            //}
            //catch (Exception ex)
            //{
            //    string a = ex.ToString();
            //    MessageBox.Show(a);
            //    return;
            //}
            ////3.判断 
            ////失败 直接终止
            //if (ret == -1)
            //{
            //    MessageBox.Show(retmessage, "后台提示");
            //    return;
            //}
            //else {
            //成功 进行下一步 循环调用Webserver
            foreach (CommitInfo info in ParaCommitList)
                {
                    string checkpara = info.CheckPara;
                    LinkType = "WEBSAL001";
                    try
                    {
                        webService = Convert.ToString(chk.GetData(LinkType, LinkComp, checkpara, out ret, out retmessage));
                    }
                    catch (Exception ex)
                    {
                        string a = ex.ToString();
                        MessageBox.Show(a);
                        return;
                    }
                    //4.失败计数 直接此条更新 importstate为失败
                    if (ret==-1) {
                        count++;
                        dao.UpImportstate(info.Batchid,info.ExcelSeqid);
                    }
                    //5.返回的参数分别更新到中间表中
                    dao.UpdateImpoprtMsg(info.Batchid, info.ExcelSeqid, ret, retmessage);
                }
                //6.判断 失败个数大于0  直接结束 重新load表
                if (count>0)
                {
                    MessageBox.Show("提交失败！", "后台提示");
                    //再次查中间表
                    ImportedList = dao.GetCheckedInfo(batchid);
                    dgvContractInfo.DataSource = ImportedList;
                    dgvContractInfo.Refresh();
                }
                else {
                    //7.失败个数为0  最终一次调用Webserver 写进CMS 最后生成合同
                    string import_para2 = ins.ImportParass;
                    LinkType = "WEBSAL002";
                    try
                    {
                        webService = Convert.ToString(chk.GetData(LinkType, LinkComp, import_para2, out ret, out retmessage));
                    }
                    catch (Exception ex)
                    {
                        string a = ex.ToString();
                        MessageBox.Show(a);
                        return;
                    }
                    MessageBox.Show(retmessage,"后台提示");
                    if (ret == -1)
                    {
                        //再次查中间表
                        CommitedList = dao.GetCheckedInfo(batchid);
                        dgvContractInfo.DataSource = CommitedList;
                        dgvContractInfo.Refresh();
                    }
                    else {

                        //更新 这一批次importstate 为成功
                        dao.updateBatchImport(batchid);
                        //再次查询中间表
                        CommitList= dao.GetCheckedInfo(batchid);
                        dgvContractInfo.DataSource = CommitList;
                        dgvContractInfo.Refresh();

                    }
                }

            //}
            
        }

        #region 其他修改触发事件
        //批卡备注和审批备注修改
        private void dgvContractInfo_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != 11 && e.ColumnIndex != 12 )
            {
                dgvContractInfo.Columns[e.ColumnIndex].ReadOnly = true;
                return;
            }
            //批卡备注
            if (e.ColumnIndex == 11 && e.RowIndex != -1)
            {
                dgvContractInfo.Columns[e.ColumnIndex].ReadOnly = false;
                DataGridViewCell cell = dgvContractInfo.Rows[e.RowIndex].Cells["Column5"];
                dgvContractInfo.CurrentCell = cell;
                dgvContractInfo.BeginEdit(true);                
            }
            //审批备注
            if (e.ColumnIndex == 12 && e.RowIndex != -1)
            {
                dgvContractInfo.Columns[e.ColumnIndex].ReadOnly = false;
                DataGridViewCell cell = dgvContractInfo.Rows[e.RowIndex].Cells["Column6"];
                dgvContractInfo.CurrentCell = cell;
                dgvContractInfo.BeginEdit(true);
           
            }
        }
        //完成修改触发
        private void dgvContractInfo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SPRetInfo ret = new SPRetInfo();
            ContractInfo info = dgvContractInfo.CurrentRow.DataBoundItem as ContractInfo;
            //批卡备注
            if (e.ColumnIndex == 11 && e.RowIndex != -1)
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
            if (e.ColumnIndex == 12 && e.RowIndex != -1)
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
        //修改发运方式
        private void cBtrans_SelectedValueChanged(object sender, EventArgs e)
        {
            string keys = "";
            string trans = cBtrans.SelectedItem.ToString().Trim();
            foreach (string key in transDic.Keys)
            {
                if (transDic[key].Equals(trans))
                {
                    keys = key;
                    break;
                }
            }

            dao.UpTrans(batchid, keys, retinfo);
            if (retinfo.num != "1")
            {

                MessageBox.Show(retinfo.msg, "后台提示！");
            }

        }
        //修改开单方式
        private void cBBilingType_SelectedValueChanged(object sender, EventArgs e)
        {
            string keys = "";
            string kd = cBBilingType.SelectedItem.ToString().Trim();
            foreach (string key in BillingTypeDic.Keys)
            {
                if (BillingTypeDic[key].Equals(kd))
                {
                    keys = key;
                    break;
                }
            }

            dao.UpBillings(batchid, keys, retinfo);
            if (retinfo.num != "1")
            {
                MessageBox.Show(retinfo.msg, "后台提示！");
            }
        }
        //修改支付方式
        private void cBPay_SelectedValueChanged(object sender, EventArgs e)
        {
            string keys = "";
            string pay = cBPay.SelectedItem.ToString().Trim();
            foreach (string key in payDic.Keys)
            {
                if (payDic[key].Equals(pay))
                {
                    keys = key;
                    break;
                }
            }

            dao.UpPayType(batchid, keys, retinfo);
            if (retinfo.num != "1")
            {
                MessageBox.Show(retinfo.msg, "后台提示！");
            }
        }
        //合同备注修改
        private void txtHdrMark_Leave(object sender, EventArgs e)
        {
            //SPRetInfo ret = new SPRetInfo();
            //string HdrMark = txtHdrMark.Text.ToString().Trim();
            //if (StringUtils.IsNull(HdrMark))
            //{
            //    HdrMark = "-1";
            //}
            //dao.UpdateHdrMark(int.Parse(batchid), HdrMark, ret);
            //if (ret.num != "1")
            //{
            //    MessageBox.Show(ret.msg, "后台提示！");
            //}
            //else
            //{
            //    MessageBox.Show(ret.msg, "后台提示！");
            //}
        }
        #endregion
        //监测子界面关闭事件
        private void ContractForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        ////enter键快捷触发  合同备注修改
        //private void ContractForm_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == 13)
        //    {
        //        SPRetInfo ret = new SPRetInfo();
        //        string HdrMark = txtHdrMark.Text.ToString().Trim();
        //        if (StringUtils.IsNull(HdrMark))
        //        {
        //            HdrMark = "-1";
        //        }
        //        dao.UpdateHdrMark(int.Parse(batchid), HdrMark, ret);
        //        if (ret.num != "1")
        //        {
        //            MessageBox.Show(ret.msg, "后台提示！");
        //        }
        //        else
        //        {
        //            MessageBox.Show(ret.msg, "后台提示！");
        //        }
        //    }
        //}
        //合同备注修改
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            SPRetInfo ret = new SPRetInfo();
            string HdrMark = txtHdrMark.Text.ToString().Trim();
            if (StringUtils.IsNull(HdrMark))
            {
                HdrMark = "-1";
            }
            dao.UpdateHdrMark(int.Parse(batchid), HdrMark, ret);
            if (ret.num != "1")
            {
                MessageBox.Show(ret.msg, "后台提示！");
            }
            else
            {
                MessageBox.Show(ret.msg, "后台提示！");
            }
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace PriceManager
{
    public partial class MatchForm : Form
    {
        //声明委托和事件
        public delegate void TransfDelegate(string quans,string key);
        public event TransfDelegate TrandfEvent;

        public MatchForm()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }
        SortableBindingList<CmsSku> CmsSkuList = new SortableBindingList<CmsSku>();
        SortableBindingList<CmsSku> matchedList = new SortableBindingList<CmsSku>();
        APDao_B2BTools dao = new APDao_B2BTools();
        public BillQuotationTemp MatchInfo = new BillQuotationTemp();
        public string CstCode = "";
        public string CstInfo = "";
        public string Cstid = "";

        public string KeyWord = "";
        public string Ratifier = "";
        public string Producer = "";
        public string Goodname = "";
        public string Goods = "";
        public string Saledeptid = "";
        public string GoodBar = "";
        public string Goodid = "";
        int MatchFlag = 0;

        //匹配按钮
        private void btnMatch_Click(object sender, EventArgs e)
        {
            //连接服务
            string server = AppConfigUtils.getConfig("server");
            string sPort = AppConfigUtils.getConfig("port");
            if (server == null || sPort == null)
            {
                MessageBox.Show("请检查配置文件!");
            }
            int port = int.Parse(sPort);
            if (dao.ConnectServer(server, port) == -1) return;
            //模糊匹配功能
            if (string.IsNullOrEmpty(txtKeyWord.Text.Trim()) || matchedList == null)
                return;
            //+ – && || ! ( ) { } [ ] ^ ” ~ * ? : /
            String text = txtKeyWord.Text;
            text = text.Replace('*', ' ');
            text = text.Replace('+', ' ');
            text = text.Replace('-', ' ');
            text = text.Replace('&', ' ');
            text = text.Replace('|', ' ');
            text = text.Replace('!', ' ');
            text = text.Replace('(', ' ');
            text = text.Replace(')', ' ');
            text = text.Replace('{', ' ');
            text = text.Replace('}', ' ');
            text = text.Replace('[', ' ');
            text = text.Replace(']', ' ');
            text = text.Replace('^', ' ');
            text = text.Replace('"', ' ');
            text = text.Replace('~', ' ');
            text = text.Replace('?', ' ');
            text = text.Replace(':', ' ');

            text = text.Replace('<', ' ');
            text = text.Replace('>', ' ');

            text = text.Replace('/', ' ');

            text = text.Replace('\\', ' ');
            text = text.Replace('\'', ' ');
            //List<CmsSku> vList = dao.MatchSku(text, Properties.Settings.Default.OWNERID);
            //if (vList != null && vList.Count > 0)
            //{
            //    CmsSkuList = new SortableBindingList<CmsSku>(vList);
            //    foreach (CmsSku sku in CmsSkuList)
            //    {
            //        IEnumerable<string> existQuery =
            //         from mSku in matchedList
            //         where mSku.CmsSkuID == sku.CmsSkuID
            //         select mSku.RelID;

            //        List<string> skuList = existQuery.ToList();
            //        if (skuList.Count > 0)
            //            sku.MatchFlag = "Y";}

            this.Cursor = Cursors.WaitCursor;
            try
            {
                List<CmsSku> vList = dao.QuerySkuMatch(txtGoods.Text, Cstid, Properties.Settings.Default.OWNERID, text);
                if (vList.Count > 0)
                {                 
                    var T = vList.OrderByDescending(i => i.RespBuyerFlag).ThenBy(i => i.SortFlag).ToList();
                    dataGridView1.DataSource = new SortableBindingList<CmsSku>(T);
                }
                else
                {
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dataGridView1.CurrentCell = null;
            dao.DisConnect();//断开服务
            this.Cursor = Cursors.Default;
        }

        //载入窗口
        private void MatchForm_Load(object sender, EventArgs e)
        {
            txtCstCode.Text = CstCode;
            txtCstInfo.Text = CstInfo;
            txtGoodname.Text = MatchInfo.Genspec;
            txtGoods.Text = MatchInfo.Gengoods;
            txtKeyWord.Text = MatchInfo.Genspec + MatchInfo.Genproducer + MatchInfo.Ratifier;
            txtProducer.Text = MatchInfo.Genproducer;
            txtRatifier.Text = MatchInfo.Ratifier;
            btnMatch_Click(sender, e);


        }

        //点击头表
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 1 || e.RowIndex == -1) return;
            SPRetInfo ret = new SPRetInfo();
            CmsSku info = dataGridView1.CurrentRow.DataBoundItem as CmsSku;
            dao.PDefaultDept(info.CmsSkuCode, ret);
            if (ret.num == "1")
            {
                txtSaledeptid.Text = ret.result;
                string deptId = txtSaledeptid.Text;
                dao.Qty(info.CmsSkuID, deptId, ret);
                txtBizqty.Text =ret.num;
                txtOwnchgqyt.Text =ret.msg;
            }
            //MessageBox.Show(info.RelID);
        }

        private void MatchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            dao.DisConnect();
            this.Close();
        }

        //已对码改变该行颜色
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex != 3) return;
            if (this.dataGridView1.DataSource == null) return;

            //CmsSku sku = (CmsSku)(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            //if (sku == null) return;
            //if ("Y".CompareTo(sku.MatchFlag) == 0)
            //{
            //    this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gray;
            //    //this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Green;
            //}
            //else
            //{
            //    this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            //}

            if (e.RowIndex != -1 && dataGridView1.Columns[e.ColumnIndex].Name == "Column2")
            {
                string stateName = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();//查询条件
                if (stateName == "Y")
                {
                    //e.CellStyle.BackColor = Color.Red;//单个单元格变色
                    this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                }
                else
                {
                    this.dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }
            
        }

        //取消对码
        private void button4_Click(object sender, EventArgs e)
        {
            CmsSku info = new CmsSku();
            for (int i=0;i<dataGridView1.Rows.Count;i++) {
                if (dataGridView1.Rows[i].Cells["Column2"].Value.ToString() =="Y") {
                     info = dataGridView1.Rows[i].DataBoundItem as CmsSku;
                }
            }
                string relid = info.RelID;
                SPRetInfo ret = new SPRetInfo();
                dao.PGenDel(relid, ret);
                if (ret.num == "1")
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                }
                else
                {
                    MessageBox.Show(ret.msg, "后台提示!");
                    return;
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells["Column2"].Value = "N";
                }
                //2.把对码数据刷回中间表
                dao.SetBillData(MatchInfo, ret);
                if (!(ret.num == "1"))
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                    return;
                }

            this.dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting);
            //this.DialogResult = DialogResult.No; 
            Saledeptid = txtSaledeptid.Text;
            MatchFlag = 2;
            dataGridView1.CurrentCell = null;
            dataGridView1.Refresh();

            string key = "1";
            string quans = txtQuotation.Text;
            //触发事件
            TrandfEvent(quans, key);
            //string quans = txtQuotation.Text;
            ////触发事件
            //TrandfEvent(quans);
        }

        //新增对码
        private void btnNewMatch_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount <= 0 || dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("请选择要新增对码的数据！", "系统提示！");
                return;
            }
            string cstcode ;
            string goods ;
            string gengoods;
            string genspec; 
            string genproducer; 
            string transrate; 
            string goodbar; 
            string ratifier;
            CmsSku info = dataGridView1.CurrentRow.DataBoundItem as CmsSku;
            SPRetInfo ret = new SPRetInfo();
            if (string.IsNullOrEmpty(txtSaledeptid.Text))
            {
                MessageBox.Show("部门id不能为空！", "系统提示！");
                txtSaledeptid.Focus();
                return;
            }
            else
            {
                Saledeptid = txtSaledeptid.Text;
                try
                {
                    int.Parse(Saledeptid);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex+"部门id格式错误！","系统提示！");
                    txtSaledeptid.Focus();
                }
            }
            cstcode = txtCstCode.Text;
            goods = info.CmsSkuCode;
            gengoods = txtGoods.Text;
            genspec = txtGoodname.Text;
            genproducer = txtProducer.Text;
            if (string.IsNullOrEmpty(txtTransrate.Text))
            {
                MessageBox.Show("转换比不能为空！", "系统提示！");
                txtTransrate.Focus();
                return;
            }
            else
            {
                transrate = txtTransrate.Text;
                try
                {
                    int.Parse(transrate);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex + "转换比格式错误！", "系统提示！");
                    txtTransrate.Focus();
                }
            }
            goodbar = GoodBar;
            ratifier = txtRatifier.Text;
            //1.
            dao.PGenAddmodify(Saledeptid, cstcode, goods, gengoods, genspec, genproducer, transrate, goodbar, ratifier, ret);
            if (!(ret.num == "1"))
            {
                MessageBox.Show(ret.msg, "后台提示！");
                return;
                //MessageBox.Show(ret.msg, "后台提示！");
                //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                //{
                //    dataGridView1.Rows[i].Cells[1].Value = "N";
                //}

                //dataGridView1.CurrentRow.Cells[1].Value = "Y";
                //this.dataGridView1.CellFormatting += 
                //    new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting);        
            }
            //--------------------------------------------------2018-10-15----
            //2.把对码数据刷回中间表
            dao.SetBillData(MatchInfo,ret);
            if (!(ret.num == "1")) {
                MessageBox.Show(ret.msg, "后台提示！");
                return;
            }
            AllPrcinfo zet = new AllPrcinfo();
            //调用mysql  获取价格
            zet=dao.GetB2BPrcs(MatchInfo,info.CmsSkuID, txtSaledeptid.Text);
            if (!string.IsNullOrEmpty(zet.Prcresultcode))//!=
            {
                if (string.IsNullOrEmpty(zet.Prc))
                {
                    zet.Prc = "-1";
                    zet.Bottomprc = "-1";
                    zet.Costprc = "-1";               
                }
            }
            //3.再次调用存储过程  可以更行单行价格数据
            dao.SetBillPrc(zet, MatchInfo,ret);
            if (!(ret.num == "1"))
            {
                MessageBox.Show(ret.msg, "后台提示！");
                return;
            }
            //4.再次调用存储过程 更新报价
            dao.SetBillQuotation(MatchInfo, ret);
            if (ret.num == "1")
            {
                MessageBox.Show(ret.msg, "后台提示！");
            }
            else
            {
                MessageBox.Show(ret.msg, "后台提示！");
                return;
            }
            //5.最后再次查询中间表
           zet=dao.getAllPrc(MatchInfo);
            txtBottomprc.Text =zet.Bottomprc;
            txtPrc.Text = zet.Prc;
            txtQuotation.Text = zet.Quotation;
            txtLastsoprc.Text = zet.Lastsoprc;
            txtLastsotime.Text = zet.Lastsotime;
            Goodid = info.CmsSkuID;
            Saledeptid = txtSaledeptid.Text;
            string key = "0";
            string quans = txtQuotation.Text;
            //触发事件
            TrandfEvent(quans, key);
            //this.DialogResult = DialogResult.OK;
            MatchFlag = 1;
            btnMatch_Click(sender, e);

        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (MatchFlag ==1 )
            {
                this.DialogResult = DialogResult.OK;
            }
            else if (MatchFlag == 2)
            {
                this.DialogResult = DialogResult.No;
            }
            else
            {
                this.Close();
            }
        }
        //部门改变 重新查询货主库存和可调库存    ----2018-10-15-----
        private void txtSaledeptid_Leave(object sender, EventArgs e)
        {
            SPRetInfo ret = new SPRetInfo();
            CmsSku info = dataGridView1.CurrentRow.DataBoundItem as CmsSku;
            string deptId = txtSaledeptid.Text;
            dao.Qty(info.CmsSkuID, deptId, ret);
            txtBizqty.Text = ret.num;
            txtOwnchgqyt.Text = ret.msg;
        }

        //enter键触发  更新人工报价     ------------2018-10-17---------
        private void txtQuotation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键  
            {
                SPRetInfo ret = new SPRetInfo();
                string Quotation = txtQuotation.Text;
                string key = "0";
                //触发事件
                TrandfEvent(Quotation, key);
                if (StringUtils.IsNull(Quotation)) {
                    Quotation = "-1";
                }
                dao.UpdateHPrc(MatchInfo, Quotation, ret);
                if (ret.num == "1")
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                }
                else
                {
                    MessageBox.Show(ret.msg, "后台提示！");
                    return;
                }
            }         
        }     
    }
}

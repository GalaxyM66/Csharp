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
    public partial class StockForm : Form
    {
        public InoutGenCmsbillXlstemp inoutGenCmsbillXlstemp;
        public OrderButtDto orderinfo;
        public int stateUI;
        APDao_B2BTools dao = new APDao_B2BTools();
        SortableBindingList<BizInfo> bizInfo = new SortableBindingList<BizInfo>();
        int AlloQty = 0;
        int unAlloQty= 0;
        public StockForm()
        {
            InitializeComponent();
        }
       private void clearUI() {
            bizInfo.Clear();
        }
        private void StockForm_Load(object sender, EventArgs e)
        {
            clearUI();
            SPRetInfo ret = new SPRetInfo();
            if (stateUI==1) {
                bizInfo = dao.getBizInfo(inoutGenCmsbillXlstemp, ret);
            }
            if (stateUI==0)
            {
                bizInfo = dao.getBizInfos(orderinfo, ret);
            }
            if (bizInfo.Count < 1)
            {
                MessageBox.Show("库存明细不存在！", "程序提示");
                dgvBizInfo.Refresh();
            }
            else {
                foreach (BizInfo bi in bizInfo) {
                    AlloQty += Convert.ToInt32(bi.AlloQty);
                    unAlloQty += Convert.ToInt32(bi.UnAlloQty);
                }
                txtAlloQty.Text = AlloQty.ToString();
                txtUnAlloQty.Text = unAlloQty.ToString();
                dgvBizInfo.DataSource = bizInfo;
                dgvBizInfo.Refresh();
            }

        }
        //监测此窗体关闭 
        private void StockForm_FormClosing(object sender, FormClosingEventArgs e)
        {
                this.DialogResult = DialogResult.OK;
        }
    }
}

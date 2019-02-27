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
    public partial class BtbPriceDetailForm : Form
    {
        APDao_GoodsClientPrice dao = new APDao_GoodsClientPrice();
        List<GoodsDetails> infolist = new List<GoodsDetails>();

        //声明委托和事件
        public delegate void TransfDelegate(GoodsDetails goodInfo);
        public event TransfDelegate TrandfEvent;
        public BtbPriceDetailForm()
        {
            InitializeComponent();
        }
        private void clearUI() {

            infolist.Clear();
        }
       
        //查询事件
        private void button1_Click(object sender, EventArgs e)
        {
            clearUI();
            this.Cursor = Cursors.WaitCursor;
            int i = 0;
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            if (StringUtils.IsNotNull(txtGoods.Text)) {
                sqlkeydict.Add("goods",txtGoods.Text.ToString().Trim());
                i++;
            }
            if (StringUtils.IsNotNull(txtGoodsName.Text)) {
                sqlkeydict.Add("name%","%"+txtGoodsName.Text.ToString().Trim()+"%");
                i++;
            }
            if (i <= 0)
            {
                MessageBox.Show("请至少输入一个查询条件！", "前台提示");
                this.Cursor = Cursors.Default;
                return;
            }
            else {
                infolist=dao.GetGoodsShield(sqlkeydict);
                if (infolist.Count <= 0)
                {
                    MessageBox.Show("没有查询到数据！", "后台提示");
                    dgvBtbPdinfo.DataSource = infolist;
                    dgvBtbPdinfo.Refresh();
                    this.Cursor = Cursors.Default;
                    return;
                }
                else {
                    dgvBtbPdinfo.DataSource = infolist;
                    dgvBtbPdinfo.CurrentCell = null;
                    dgvBtbPdinfo.Refresh();
                    this.Cursor = Cursors.Default;
                }

            }

        }

        //双击选中数据
        private void dgvPriceShieldInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //获取选中行的信息
            GoodsDetails info = dgvBtbPdinfo.CurrentRow.DataBoundItem as GoodsDetails;
            //触发事件
            TrandfEvent(info);
            this.Close();
        }

    }
}

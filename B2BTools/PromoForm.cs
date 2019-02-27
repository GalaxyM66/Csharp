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
    public partial class PromoForm : Form
    {
        APDao_B2BTools dao = new APDao_B2BTools();
        List<GoodInfo> list = new List<GoodInfo>();

        //声明委托和事件
        public delegate void TransfDelegate(GoodInfo goodInfo);
        public event TransfDelegate TransfEvent;
        public PromoForm()
        {
            InitializeComponent();
        }
        private void clearUI() {

            list.Clear();

        }
        
        //快捷键
        private void PromoForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                BtnQuery_Click(sender, e);
            }

        }
    //查询
    private void BtnQuery_Click(object sender, EventArgs e)
        {
            clearUI();
            int i = 0;
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            this.Cursor = Cursors.WaitCursor;
            if (StringUtils.IsNotNull(txtGoods.Text)) {
                sqlkeydict.Add("goods", txtGoods.Text.ToString().Trim());
                i++;
            }
            if (StringUtils.IsNotNull(txtGoodName.Text))
            {
                sqlkeydict.Add("name%","%"+ txtGoodName.Text.ToString().Trim()+"%");
                i++;
            }
            if (i <= 0)
            {
                MessageBox.Show("请至少输入一个查询条件！", "错误信息");
                this.Cursor = Cursors.Default;
                return;
            }
            else {

                list=dao.searchPromo(sqlkeydict);
                if (list.Count <= 0)
                {
                    MessageBox.Show("未查询到数据！", "程序提示");
                    dgvGoodInfo.DataSource = null;
                    this.Cursor = Cursors.Default;

                }
                else
                {
                    dgvGoodInfo.DataSource = list;
                    dgvGoodInfo.Refresh();
                    dgvGoodInfo.CurrentCell = null;
                    this.Cursor = Cursors.Default;
                }

            }
        }
        //选中
        private void dgvEmpInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GoodInfo info = dgvGoodInfo.CurrentRow.DataBoundItem as GoodInfo;
            //触发事件
            TransfEvent(info);
            this.Close();

        }

        private void PromoForm_Load(object sender, EventArgs e)
        {
            this.Text = "商品信息查询";
        }
    }
}

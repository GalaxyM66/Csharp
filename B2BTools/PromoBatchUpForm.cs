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
    public partial class PromoBatchUpForm : Form
    {
        public PromotionInfo batchUpInfo;
        public SortableBindingList<PromotionInfo> batchLists;
        APDao_B2BTools dao = new APDao_B2BTools();
        SPRetInfo retinfo = new SPRetInfo();
        public PromoBatchUpForm()
        {
            InitializeComponent();
        }

        private void PromoBatchUpForm_Load(object sender, EventArgs e)
        {
            txtGoods.Text = batchUpInfo.Goods;
            txtGoodName.Text = batchUpInfo.GoodName;
            this.Text = "批量修改";
        }
        //批量修改
        private void BtnBatchUp_Click(object sender, EventArgs e)
        {
            if (StringUtils.IsNull(txtPocName.Text)) {
                MessageBox.Show("活动政策名称不能为空！","前台提示");
                txtPocName.Focus();
                return;
            }
            if (StringUtils.IsNull(txtPolicy.Text))
            {
                MessageBox.Show("活动政策不能为空！", "前台提示");
                txtPolicy.Focus();
                return;
            }
            if (StringUtils.IsNull(txtRemark.Text))
            {
                MessageBox.Show("备注不能为空！", "前台提示");
                txtRemark.Focus();
                return;
            }
            string beginTime = DateBegin.Value.ToString("yyyyMMdd");
            string endTime = DateEnd.Value.ToString("yyyyMMdd");
            dao.BatchUpdate(batchLists,txtPocName.Text, beginTime,endTime,txtPolicy.Text,txtRemark.Text,retinfo);
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
    }
}

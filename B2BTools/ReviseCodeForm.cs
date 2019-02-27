using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PriceManager.B2BTools
{
    public partial class ReviseCodeForm : Form
    {
        APDao_B2BTools dao = new APDao_B2BTools();
        public  SortableBindingList<GenCstGood> UpdateGenCstGoodsList = new SortableBindingList<GenCstGood>();
        public ReviseCodeForm()
        {
            InitializeComponent();
        }

        //确定修改
        private void confirmUpdateBtn_Click(object sender, EventArgs e)
        {
            //获取修改后的cms商品码
            string newGoods = txtCmsGoodsCode.Text.Trim();
            SPRetInfo ret = new SPRetInfo();
          
            dao.UnifiedUpdateCode(UpdateGenCstGoodsList,newGoods,ret);
            if (ret.num == "1")
            {
                MessageBox.Show(ret.msg + "|" + ret.result, "后台提示！");
            }
            else
            {
                MessageBox.Show(ret.msg + "|" + ret.result, "后台提示！");
                return;
            }
            this.Close();
            UpdateGenCstGoodsList.Clear();
            


        }
  
    }
}

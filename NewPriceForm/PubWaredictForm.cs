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
    public partial class PubWaredictForm : Form
    {
        APDao_GoodsClientPrice dao = new APDao_GoodsClientPrice();

        private SortableBindingList<SelPubWaredict> PubWaredictList = new SortableBindingList<SelPubWaredict>();
        public SelPubWaredict waredictinfo = new SelPubWaredict();
        public PubWaredictForm()
        {
            InitializeComponent();
            dgvBill.AutoGenerateColumns = false;
        }

        private void PubWaredictForm_Load(object sender, EventArgs e)
        {
            txtGoodsCode.Focus();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            if (txtGoodsCode.Text.Length >= 3 || txtGoodsName.Text.Length >= 3)
            {

                if (!string.IsNullOrEmpty(txtGoodsCode.Text))
                {
                    sqlkeydict.Add("goods%", "%" + txtGoodsCode.Text.ToString() + "%");
                }
                if (!string.IsNullOrEmpty(txtGoodsName.Text))
                {
                    sqlkeydict.Add("name%", "%" + txtGoodsName.Text.ToString() + "%");
                }
                PubWaredictList = dao.GetPubWaredictAllInfoList(sqlkeydict);
                if (PubWaredictList.Count <= 0)
                {
                    MessageBox.Show("未查询到数据！", "程序提示");
                    return;
                }
                dgvBill.DataSource = PubWaredictList;
                dgvBill.Refresh();
            }
            else
            {
                MessageBox.Show("商品编码或商品名称至少一个不为空且需输入3个字以上！", "程序提示");
                return;
            }
        }

        private void dgvBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewSelectedRowCollection row = dgvBill.SelectedRows;
            

            if (e.ColumnIndex < 1)//第一列才触发事件
                return;
            waredictinfo = (SelPubWaredict)(row[0].Cells[0].Value);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtGoodsCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13) {
                btnSubmit_Click(sender, e);
            }
        }

        private void txtGoodsName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                btnSubmit_Click(sender, e);
            }
        }
    }
}

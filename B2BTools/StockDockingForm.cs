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
    public partial class StockDockingForm : DockContent
    {
        SortableBindingList<StockDockInfo> InfoList = new SortableBindingList<StockDockInfo>();
        APDao_B2BTools dao = new APDao_B2BTools();
        public StockDockingForm()
        {
            InitializeComponent();
            dgvStockDocking.AutoGenerateColumns = false;
        }

        private void clearUI() {
            InfoList.Clear();

        }
        //查询
        private void BtnSel_Click(object sender, EventArgs e)
        {
            clearUI();
            this.Cursor = Cursors.WaitCursor;
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            if (StringUtils.IsNotNull(txtGoods.Text))
            {
                sqlkeydict.Add("goods", txtGoods.Text.ToString().Trim());
            }
            if (StringUtils.IsNotNull(txtName.Text))
            {
                sqlkeydict.Add("name%", "%" + txtName.Text.ToString().Trim() + "%");
            }
            if (StringUtils.IsNotNull(txtProducer.Text))
            {
                sqlkeydict.Add("producer%", "%" + txtProducer.Text.ToString().Trim() + "%");
            }
            string accountCode = SessionDto.Empid;
            InfoList = dao.SelStockDockInfo(accountCode, sqlkeydict);
            if (InfoList.Count <= 0){

                  MessageBox.Show("未查询到数据！", "程序提示");
                  dgvStockDocking.DataSource = null;
                  dgvStockDocking.CurrentCell = null;
                  this.Cursor = Cursors.Default;
                }
                else
                {
                   dgvStockDocking.DataSource = InfoList;
                   dgvStockDocking.Refresh();
                   dgvStockDocking.CurrentCell = null;
                   this.Cursor = Cursors.Default;
                }            
        }





    }
}

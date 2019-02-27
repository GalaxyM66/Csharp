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
    public partial class SelectForm : DockContent

    {
        APDao_GoodsClientPrice dao = new APDao_GoodsClientPrice();
        private SortableBindingList<ScmPriceGoodunify> ScmPriceGoodunifyList = new SortableBindingList<ScmPriceGoodunify>();
        private SortableBindingList<ScmPriceExe> ScmPriceExeList = new SortableBindingList<ScmPriceExe>();
        private ScmPriceGoodunify SelScmPriceGoodunify = new ScmPriceGoodunify();
        private SortableBindingList<ScmPriceGoodtap> ScmPriceGoodtapList = new SortableBindingList<ScmPriceGoodtap>();
        private ScmPriceGoodtap SelScmPriceGoodtap = new ScmPriceGoodtap();
        public SelectForm()
        {
            InitializeComponent();
            dgvPriceGoodunify.AutoGenerateColumns = false;
            dgvPriceExe.AutoGenerateColumns = false;
            dgvPriceGoodtap.AutoGenerateColumns = false;
        }

        private void SelectForm_Load(object sender, EventArgs e)
        {

        }
        private void Clear()
        {
            ScmPriceGoodunifyList.Clear();
            dgvPriceGoodunify.DataSource = ScmPriceGoodunifyList;
            dgvPriceGoodunify.Refresh();
            ScmPriceGoodtapList.Clear();
            dgvPriceGoodtap.DataSource = ScmPriceGoodtapList;
            dgvPriceGoodtap.Refresh();
            

        }

        private void dgvPriceExeClear()
        {
            ScmPriceExeList.Clear();
            dgvPriceExe.DataSource = ScmPriceExeList;
            dgvPriceExe.Refresh();

        }
        private void queryBtn_Click(object sender, EventArgs e)
        {
            Clear();
            this.Cursor = Cursors.WaitCursor;
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            int i = 0;
            if (!StringUtils.IsNull(txtGoodsCode.Text))
            {
                sqlkeydict.Add("goods", txtGoodsCode.Text.ToString());
                i++;
            }
            if (!StringUtils.IsNull(txtGoodsName.Text))
            {
                sqlkeydict.Add("name%", "%" + txtGoodsName.Text.ToString() + "%");
            }
            if (!StringUtils.IsNull(txtSpec.Text))
            {
                sqlkeydict.Add("spec%", "%" + txtSpec.Text.ToString() + "%");
            }
            if (!StringUtils.IsNull(txtProducer.Text))
            {
                sqlkeydict.Add("producer%", "%" + txtProducer.Text.ToString() + "%");
            }
            if (i <=0)
            {
                MessageBox.Show("必须输入商品代码！", "程序提示");
                this.Cursor = Cursors.Default;
                return;
            }
            if (sqlkeydict.Count < 1) {
                MessageBox.Show("请输入一个查询条件！","程序提示");              
                this.Cursor = Cursors.Default;
                return;
            }
            ScmPriceGoodunifyList = dao.GetScmPriceGoodunifyList(sqlkeydict);
            dgvPriceGoodunify.DataSource = ScmPriceGoodunifyList;
            dgvPriceGoodunify.Refresh();
            dgvPriceGoodunify.CurrentCell = null;
            if (ScmPriceGoodunifyList.Count <= 0) {
                Dictionary<string, string> sqlkeydict1 = new Dictionary<string, string>();
                sqlkeydict1.Add("goods", txtGoodsCode.Text.ToString());
                ScmPriceGoodtapList = dao.GetScmPriceGoodtapList(sqlkeydict1);

                dgvPriceGoodtap.DataSource = ScmPriceGoodtapList;
                dgvPriceGoodtap.Refresh();
            }
            this.Cursor = Cursors.Default;
        }

        private void btnSel_Click(object sender, EventArgs e)
        {
            dgvPriceExeClear();
            this.Cursor = Cursors.WaitCursor;
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            if (!StringUtils.IsNull(txtClientCode.Text))
            {
                sqlkeydict.Add("cstcode",txtClientCode.Text.ToString());
            }
            if (!StringUtils.IsNull(txtGoods.Text))
            {
                sqlkeydict.Add("goods", txtGoods.Text.ToString());
            }
            if (!StringUtils.IsNull(txtClientName.Text))
            {
                sqlkeydict.Add("cstname%", "%" + txtClientName.Text.ToString() + "%");
            }
            if (!StringUtils.IsNull(txtRegion.Text))
            {
                sqlkeydict.Add("region%", "%" + txtRegion.Text.ToString() + "%");
            }
            if (!StringUtils.IsNull(txtClientGroupCode.Text))
            {
                sqlkeydict.Add("code%", "%"+txtClientGroupCode.Text.ToString()+"%");
            }
            if (!StringUtils.IsNull(txtClientGroupName.Text))
            {
                sqlkeydict.Add("name%", "%" + txtClientGroupName.Text.ToString() + "%");
            }
            if (!StringUtils.IsNull(txtBGoodsName.Text))
            {
                sqlkeydict.Add("goodsname%", "%" + txtBGoodsName.Text.ToString() + "%");
            }
            if (!StringUtils.IsNull(txtBProducer.Text))
            {
                sqlkeydict.Add("producer%", "%" + txtBProducer.Text.ToString() + "%");
            }
            if (sqlkeydict.Count < 1)
            {
                MessageBox.Show("请输入一个查询条件！", "程序提示");             
                this.Cursor = Cursors.Default;
                return;
            }

            ScmPriceExeList = dao.GetScmPriceExeList(sqlkeydict);

            if (ScmPriceExeList.Count <= 0)
            {
                MessageBox.Show("未查询到数据！", "程序提示");
                dgvPriceExe.DataSource = null;
                this.Cursor = Cursors.Default;
                return;
            }
            dgvPriceExe.DataSource = ScmPriceExeList;
            dgvPriceExe.Refresh();
            dgvPriceExe.CurrentCell = null;
            this.Cursor = Cursors.Default;
        }      

        private void dgvPriceGoodunify_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewSelectedRowCollection row = dgvPriceGoodunify.SelectedRows;
            SelScmPriceGoodunify = (ScmPriceGoodunify)(row[0].Cells[0].Value);

            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            sqlkeydict.Add("goodid", SelScmPriceGoodunify.Goodid);
            ScmPriceGoodtapList = dao.GetScmPriceGoodtapList(sqlkeydict);

            dgvPriceGoodtap.DataSource = ScmPriceGoodtapList;
            dgvPriceGoodtap.Refresh();
        }

        private void dgvPriceGoodtap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewSelectedRowCollection row = dgvPriceGoodtap.SelectedRows;
            SelScmPriceGoodtap = (ScmPriceGoodtap)(row[0].Cells[0].Value);

            txtGoods.Text = SelScmPriceGoodtap.Goods;
            txtClientGroupCode.Text=SelScmPriceGoodtap.Code;
        }
    }
}

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
    public partial class AddClientAndGoodsForm : Form
    {
        public AddClientAndGoodsForm()
        {
            InitializeComponent();
        }
        public int IntUI;
        APDao_ClientGroup dao = new APDao_ClientGroup();

        NameValue[] ContainList ={new NameValue("包含","00"),
                                    new NameValue("不包含","99")
                };

        //查询商品
        SortableBindingList<SelWaredict> SelWaredictList = new SortableBindingList<SelWaredict>();
        //已存在商品，用于对比
        public SortableBindingList<SelWaredict> OldSelWaredictList = new SortableBindingList<SelWaredict>();
        //选中商品，用于新增
        public SortableBindingList<SelWaredict> SelGoodsList = new SortableBindingList<SelWaredict>();//选中商品

        //查询客户
        SortableBindingList<SelClientsGroup> SelClientsGroupList = new SortableBindingList<SelClientsGroup>();
        //已存在客户，用于对比
        public SortableBindingList<SelClientsGroup> OldSelClientsGroupList = new SortableBindingList<SelClientsGroup>();
        //选中客户，用于新增
        public SortableBindingList<SelClientsGroup> SelClientsList = new SortableBindingList<SelClientsGroup>();

        //载入窗口
        private void AddClientAndGoodsForm_Load(object sender, EventArgs e)
        {
            dgvClients.AutoGenerateColumns = false;
            dgvGoods.AutoGenerateColumns = false;
            if (IntUI == 1)
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = true;
                cbContain.DataSource = ContainList;
                cbContain.DisplayMember = "Name";
                cbContain.ValueMember = "Value";
                cbContain.Text = "-包含-";
            }
        }

        //添加商品查询
        private void btnASelect_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> sqlkeydict = new Dictionary<string,string>();
            if (string.IsNullOrEmpty(txtAGoods.Text) & string.IsNullOrEmpty(txtAName.Text)
                & string.IsNullOrEmpty(txtAProducer.Text) & string.IsNullOrEmpty(txtASpec.Text)
                &string.IsNullOrEmpty(txtAWdrname.Text)&string.IsNullOrEmpty(txtAWdrcode.Text))
            {
                MessageBox.Show("请至少输入一个查询条件！", "系统提示！");
                return;
            }
            if (!string.IsNullOrEmpty(txtAGoods.Text))
            {
                sqlkeydict.Add("goods%", "%" + txtAGoods.Text + "%");
            }
            if (!string.IsNullOrEmpty(txtAName.Text))
            {
                sqlkeydict.Add("name%", "%" + txtAName.Text + "%");
            }
            if (!string.IsNullOrEmpty(txtAProducer.Text))
            {
                sqlkeydict.Add("producer%", "%" + txtAProducer.Text + "%");
            }
            if (!string.IsNullOrEmpty(txtASpec.Text))
            {
                sqlkeydict.Add("spec%", "%" + txtASpec.Text + "%");
            }
            if (!string.IsNullOrEmpty(txtAWdrname.Text))
            {
                sqlkeydict.Add("wdrname%", "%" + txtAWdrname.Text + "%");
            }
            if (!string.IsNullOrEmpty(txtAWdrcode.Text))
            {
                sqlkeydict.Add("wdrcode%", "%" + txtAWdrcode.Text + "%");
            }

            SelWaredictList = dao.GetSelWaredictList(sqlkeydict);
            if (SelWaredictList.Count <= 0)
            {
                MessageBox.Show("未查询到数据！", "系统提示！");
            }
            dgvGoods.DataSource = SelWaredictList;
            dgvGoods.Refresh();
            dgvGoods.CurrentCell = null;
        }

        //商品重置
        private void btnAReset_Click(object sender, EventArgs e)
        {
            txtAGoods.Text = "";
            txtAName.Text = "";
            txtAProducer.Text = "";
            txtASpec.Text = "";
            SelWaredictList.Clear();
            dgvGoods.DataSource = SelWaredictList;
            dgvGoods.Refresh();

        }

        //商品新增
        private void btnAAdd_Click(object sender, EventArgs e)
        {
            object obj2 = FormUtils.SelectRows(dgvGoods);
            if (obj2 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            SelGoodsList.Clear();
            foreach (DataGridViewRow dgvr in row)
            {
                SelGoodsList.Add((SelWaredict)dgvr.Cells[0].Value);
            }
            bool b = false;
            string Warming = "";
            foreach (SelWaredict OldInfo in OldSelWaredictList)
            {
                foreach (SelWaredict SelInfo in SelGoodsList)
                {
                    if (OldInfo.Goods == SelInfo.Goods)
                    {
                        Warming = Warming + SelInfo.Goods + ",";
                        b = true;
                        break;
                    }
                    b = false;
                }
            }
            if (Warming != "")
            {
                MessageBox.Show("商品代码" + Warming + "已存在！新增失败！", "系统提示！");
                return;
            }
            else
            {

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        //新增客户查询
        private void txtBSelect_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(txtBCstcode.Text) & string.IsNullOrEmpty(txtBCstname.Text)
                & string.IsNullOrEmpty(txtBRegion.Text) & string.IsNullOrEmpty(txtClienttypename.Text)
                & string.IsNullOrEmpty(txtBHdrcode.Text)&string.IsNullOrEmpty(txtBHdrname.Text))
            {
                MessageBox.Show("请至少输入一个查询条件！", "系统提示！");
                return;
            }
            if (!string.IsNullOrEmpty(txtBCstcode.Text))
            {
                sqlkeydict.Add("cstcode%", "%" + txtBCstcode.Text + "%");
            }
            if (!string.IsNullOrEmpty(txtBCstname.Text))
            {
                sqlkeydict.Add("cstname%", "%" + txtBCstname.Text + "%");
            }
            if (!string.IsNullOrEmpty(txtBRegion.Text))
            {
                if (cbContain.Text == "包含")
                {
                    sqlkeydict.Add("region%", "%" + txtBRegion.Text + "%");
                }
                else
                {
                    sqlkeydict.Add("region#", "%" + txtBRegion.Text + "%");
                }
            }
            if (!string.IsNullOrEmpty(txtClienttypename.Text))
            {
                sqlkeydict.Add("clienttypename%", "%" + txtClienttypename.Text + "%");
            }
            if (!string.IsNullOrEmpty(txtBHdrname.Text))
            {
                sqlkeydict.Add("hdrname%", "%" + txtBHdrname.Text + "%");
            }
            if (!string.IsNullOrEmpty(txtBHdrcode.Text))
            {
                sqlkeydict.Add("hdrcode%", "%" + txtBHdrcode.Text + "%");
            }

            SelClientsGroupList = dao.GetSelClientsGroupList(sqlkeydict);
            if (SelClientsGroupList.Count <= 0)
            {
                MessageBox.Show("未查询到数据！", "系统提示！");
            }
            dgvClients.DataSource = SelClientsGroupList;
            dgvClients.Refresh();
            dgvClients.CurrentCell = null;
        }

        //新增客户重置
        private void btnBReset_Click(object sender, EventArgs e)
        {
            txtBCstcode.Text = "";
            txtBCstname.Text = "";
            txtBRegion.Text = "";
            SelClientsGroupList.Clear();
            dgvClients.DataSource = SelClientsGroupList;
            dgvClients.Refresh();
        }

        //客户新增
        private void btnBAdd_Click(object sender, EventArgs e)
        {
            object obj2 = FormUtils.SelectRows(dgvClients);
            if (obj2 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            SelClientsList.Clear();
            foreach (DataGridViewRow dgvr in row)
            {
                SelClientsList.Add((SelClientsGroup)dgvr.Cells[0].Value);
            }
            bool b = false;
            string Warming = "";
            foreach (SelClientsGroup OldInfo in OldSelClientsGroupList)
            {
                foreach (SelClientsGroup SelInfo in SelClientsList)
                {
                    if (OldInfo.Cstcode == SelInfo.Cstcode)
                    {
                        Warming = Warming + SelInfo.Cstcode + ",";
                        b = true;
                        break;
                    }
                    b = false;
                }
            }
            if (Warming != "")
            {
                MessageBox.Show("客户代码" + Warming + "已存在！新增失败！", "系统提示！");
                return;
            }
            else
            {

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnASelectAll_Click(object sender, EventArgs e)
        {
            if (dgvGoods.RowCount <= 0) return;
            dgvGoods.SelectAll();
        }

        private void btnBSelectAll_Click(object sender, EventArgs e)
        {
            if (dgvClients.RowCount <= 0) return;
            dgvClients.SelectAll();
        }
    }
}

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
    public partial class AgreementForm : DockContent
    {
        APDao_Agreement dao = new APDao_Agreement();
        AgreeProducerInfo agreeProducerInfo = new AgreeProducerInfo();
        //协议下拉框List
        List<string> AgreeType = new List<string>();
        List<string> BuyerName = new List<string>();
        Dictionary<string, string> myDictionary = new Dictionary<string, string>();
        Dictionary<string, string> Dic = new Dictionary<string, string>();

        SortableBindingList<AgreeProducerInfo> infosList = new SortableBindingList<AgreeProducerInfo>();
        //操作临时表，用于存储过程
        SortableBindingList<DelTemp> DelTempAgrProdList = new SortableBindingList<DelTemp>();
        public AgreementForm()
        {
            InitializeComponent();
            dgvAgreementInfo.AutoGenerateColumns = false;
        }

        private void AgreementForm_Load(object sender, EventArgs e)
        {
            //协议性质下拉框加载
            string typecode = "AGREETYPE";
            myDictionary =dao.getCbContent(typecode);
            // 遍历字典中的值
            foreach (var item in myDictionary.Values)
            {
                AgreeType.Add(item);

            }
            this.cBAgreeType.DataSource = AgreeType;
            //采购员下拉框加载
            string type = "BUYERNAME";
            Dic = dao.getCbContent(type);
            // 遍历字典中的值
            foreach (var item in Dic.Values)
            {
                BuyerName.Add(item);

            }
            this.cBBuyer.DataSource = BuyerName;
        }

        private void clearUI() {
            infosList.Clear();

        }
        //查询事件
        private void BtnSel_Click(object sender, EventArgs e)
        {
            clearUI();
            this.Cursor = Cursors.WaitCursor;
            //获取下拉框的值
            string buyer = cBBuyer.SelectedItem.ToString().Trim();
            string buyerKey = "";
            string agreeType = cBAgreeType.SelectedItem.ToString().Trim();
            string agreeTypeKey = "";
            //获取对应的key
            foreach (string key in Dic.Keys)
            {
                if (Dic[key].Equals(buyer))
                {                 
                    buyerKey = key;
                    break;
                }
            }
            foreach (string keys in myDictionary.Keys)
            {
                if (myDictionary[keys].Equals(agreeType))
                {
                    agreeTypeKey = keys;
                    break;
                }
            }
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            //int i = 0;
            if (!StringUtils.IsNull(buyer)) {

                sqlkeydict.Add("buyername", buyerKey );

            }
            if (!StringUtils.IsNull(agreeType))
            {
                sqlkeydict.Add("agreetype",  agreeTypeKey);

            }
            if (!StringUtils.IsNull(txtProdCode.Text))
            {
                sqlkeydict.Add("prod_code%", "%" + txtProdCode.Text.ToString() + "%");

            }
            if (!StringUtils.IsNull(txtMiddleMan.Text))
            {
                sqlkeydict.Add("middleman%", "%" + txtMiddleMan.Text.ToString() + "%");

            }
            if (!StringUtils.IsNull(txtProdName.Text)) {
                sqlkeydict.Add("prod_name%", "%" + txtProdName.Text.ToString() + "%");

            }
                infosList = dao.GetAgreeProdInfo(sqlkeydict);
                if (infosList.Count <= 0)
                {
                    MessageBox.Show("未查询到数据！", "程序提示");
                    dgvAgreementInfo.DataSource = null;
                    //dgvAgreementInfo.Refresh();
                    this.Cursor = Cursors.Default;
                    return;
                }
                else
                {
                    dgvAgreementInfo.DataSource = infosList;
                    dgvAgreementInfo.Refresh();
                    dgvAgreementInfo.CurrentCell = null;
                    this.Cursor = Cursors.Default;
                }
            

        }
        //新增
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddOrUpdateAgrProdForm addAgrProd = new AddOrUpdateAgrProdForm();
            addAgrProd.Tag = dao;
            addAgrProd.stateUI = 0;//传值为0，界面为新增页面
            addAgrProd.ShowDialog();
            addAgrProd.agrProdInfo = agreeProducerInfo;
            if (addAgrProd.DialogResult == DialogResult.OK)
            {
                BtnSel_Click(sender, e);
            }
        }
        //双击修改
        private void dgvAgreementInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AgreeProducerInfo info = dgvAgreementInfo.CurrentRow.DataBoundItem as AgreeProducerInfo;

            AddOrUpdateAgrProdForm updateAgrProd = new AddOrUpdateAgrProdForm();
            updateAgrProd.agrProdInfo = info;
            updateAgrProd.Tag = dao;
            updateAgrProd.stateUI = 1;//传值为1，界面为修改页面
            updateAgrProd.ShowDialog();
            if (updateAgrProd.DialogResult == DialogResult.OK)
            {
                BtnSel_Click(sender, e);
            }
        }
        //删除事件
        private void BtnDel_Click(object sender, EventArgs e)
        {
            //取得选中的行
            object obj2 = FormUtils.SelectRows(dgvAgreementInfo);
            if (obj2 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            SortableBindingList<AgreeProducerInfo> agreeProducerList = new SortableBindingList<AgreeProducerInfo>();

            agreeProducerList.Clear();
            foreach (DataGridViewRow dgvr in row)
            {
                agreeProducerList.Add((AgreeProducerInfo)dgvr.Cells[0].Value);
            }
            //读取GencstgoodList集合中的Prodid
            foreach (AgreeProducerInfo info in agreeProducerList)
            {
                DelTemp deleteInfo = new DelTemp();
                deleteInfo.RelateId = info.ProdId;

                DelTempAgrProdList.Add(deleteInfo);
            }
            SPRetInfo ret = new SPRetInfo();
            //提示确认是否删除
            DialogResult result = MessageBox.Show("确认删除？", "温馨提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                dao.DeleteAgrProd(DelTempAgrProdList, ret);
                if (ret.num == "1")
                {
                    MessageBox.Show(ret.msg + "|" + ret.result, "后台提示！");
                }
                else
                {
                    MessageBox.Show(ret.msg + "|" + ret.result, "后台提示！");
                    return;
                }
                DelTempAgrProdList.Clear();
                BtnSel_Click(sender, e);
            }
            else
            {
                MessageBox.Show("您选择了取消删除！");
            }


        }
        //全选
        private void BtnAll_Click(object sender, EventArgs e)
        {
            if (dgvAgreementInfo.RowCount <= 0) return;
            this.dgvAgreementInfo.SelectAll();
        }

    }
}

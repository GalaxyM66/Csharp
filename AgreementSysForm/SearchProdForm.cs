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
    public partial class SearchProdForm : Form
    {
        APDao_Agreement dao = new APDao_Agreement();
        AgreeProducerInfo agreeProducerInfo = new AgreeProducerInfo();
        //协议下拉框List
        List<string> AgreeType = new List<string>();
        List<string> BuyerName = new List<string>();
        Dictionary<string, string> myDictionary = new Dictionary<string, string>();
        Dictionary<string, string> Dic = new Dictionary<string, string>();

        //声明委托和事件
        public delegate void TransfDelegate(AgreeProducerInfo agreeProducerInfo);
        public event TransfDelegate TransfEvent;

        SortableBindingList<AgreeProducerInfo> infosList = new SortableBindingList<AgreeProducerInfo>();
        public SearchProdForm()
        {
            InitializeComponent();
            dgvAgreementInfo.AutoGenerateColumns = false;
        }
        private void clearUI()
        {
            infosList.Clear();

        }
        private void SearchProdForm_Load(object sender, EventArgs e)
        {
            this.Text = "查询";
            //协议性质下拉框加载
            string typecode = "AGREETYPE";
            myDictionary = dao.getCbContent(typecode);
            AgreeType = StringUtils.GetValue(myDictionary);
            this.cBAgreeType.DataSource = AgreeType;
            //采购员下拉框加载
            string type = "BUYERNAME";
            Dic = dao.getCbContent(type);
            BuyerName = StringUtils.GetValue(Dic);
            this.cBBuyer.DataSource = BuyerName;
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
            buyerKey = StringUtils.GetKey(buyer, Dic);
            agreeTypeKey = StringUtils.GetKey(agreeType, myDictionary);
          
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            //int i = 0;
            if (!StringUtils.IsNull(buyer))
            {

                sqlkeydict.Add("buyername", buyerKey);

            }
            if (!StringUtils.IsNull(agreeType))
            {
                sqlkeydict.Add("agreetype", agreeTypeKey);

            }
            if (!StringUtils.IsNull(txtProdCode.Text))
            {
                sqlkeydict.Add("prod_code%", "%" + txtProdCode.Text.ToString() + "%");

            }
            if (!StringUtils.IsNull(txtMiddleMan.Text))
            {
                sqlkeydict.Add("middleman%", "%" + txtMiddleMan.Text.ToString() + "%");

            }
            if (!StringUtils.IsNull(txtProdName.Text))
            {
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
        //将选中的信息传送到上个界面
        private void dgvAgreementInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            AgreeProducerInfo info = dgvAgreementInfo.CurrentRow.DataBoundItem as AgreeProducerInfo;
            //触发事件
            TransfEvent(info);
            this.Close();
        }

    }
}

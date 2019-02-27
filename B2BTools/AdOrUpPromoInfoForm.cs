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
    public partial class AdOrUpPromoInfoForm : Form
    {
        public int stateUI = 0;
        public PromotionInfo promotionInfo;
        APDao_B2BTools dao = null;
        PMSystemDao Pdao = new PMSystemDao();
        SPRetInfo retinfo = new SPRetInfo();
        string goodId = "";
        string cstId = "";
        public AdOrUpPromoInfoForm()
        {
            InitializeComponent();
        }

        private void AdOrUpPromoInfoForm_Load(object sender, EventArgs e)
        {
            dao = (APDao_B2BTools)this.Tag;
            initUI(stateUI);
        }

        //判断是新增还是修改
        private void initUI(int astate)
        {
            switch (astate)
            {
                case 0://新增界面
                    BtnUpdate.Visible = false;
                    BtnAdd.Visible = true;
                    this.Text = "新增";
                    break;

                case 1://修改界面
                    BtnAdd.Visible = false;
                    BtnUpdate.Visible = true;
                    this.Text = "修改";
                    //修改界面需要将原始信息传送到文本框 
                    txtProName.Text = promotionInfo.PocName;
                    txtGoods.Text = promotionInfo.Goods;
                    txtGoodName.Text = promotionInfo.GoodName;
                    txtSpec.Text = promotionInfo.Spec;
                    txtProducer.Text = promotionInfo.Producer;
                    txtCstCode.Text = promotionInfo.CstCode;
                    txtCstName.Text = promotionInfo.CstName;
                    txtPolicy.Text = promotionInfo.Policy;
                    txtRemark.Text = promotionInfo.Remark;

                    DateBegin.Text = promotionInfo.BeginTime;
                    DateEnd.Text = promotionInfo.EndTime;
                    break;
            }
        }
        //新增
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (StringUtils.IsNull(txtProName.Text))
            {
                MessageBox.Show("活动政策名称不能为空！", "前台提示");
                txtProName.Focus();
                return;
            }
            if (StringUtils.IsNull(txtGoods.Text)) {
                MessageBox.Show("商品代码不能为空！","前台提示");
                txtGoods.Focus();
                return;
            }
            if (StringUtils.IsNull(txtCstCode.Text))
            {
                MessageBox.Show("活动对象代码不能为空！", "前台提示");
                txtCstCode.Focus();
                return;
            }
            if (StringUtils.IsNull(txtPolicy.Text))
            {
                MessageBox.Show("活动政策不能为空！", "前台提示");
                txtPolicy.Focus();
                return;
            }
            string beginTime = DateBegin.Value.ToString("yyyyMMdd");
            string endTime = DateEnd.Value.ToString("yyyyMMdd");
            string proName = txtProName.Text.ToString().Trim();
            string reMark = txtRemark.Text.ToString().Trim();
            if (StringUtils.IsNull(txtRemark.Text))
            {
                reMark = "-1";
            }
            dao.AdOrUpPromo(goodId,cstId,proName,txtGoods.Text.ToString().Trim(), txtGoodName.Text.ToString().Trim(), txtSpec.Text.ToString().Trim(), txtProducer.Text.ToString().Trim(), txtCstCode.Text.ToString().Trim(), txtCstName.Text.ToString().Trim(), beginTime,endTime,txtPolicy.Text, reMark,stateUI, retinfo);
            if (retinfo.num == "1")
            {
                MessageBox.Show(retinfo.msg+" ! ", "后台提示");
                return;

            }
            else
            {
                MessageBox.Show(retinfo.msg+" ! ", "后台提示");
                return;
            }

        }
        //修改
        private void BtnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void txtGoods_Click(object sender, EventArgs e)
        {
            PromoForm selForm = new PromoForm();
            //注册事件
            selForm.TransfEvent += selForm_TransfEvent;
            selForm.ShowDialog();
        }
        //事件处理方法
        void selForm_TransfEvent(GoodInfo infos)
        {
            //将子界面的值接受过来进行赋值
            txtGoods.Text = infos.Goods;
            txtGoodName.Text = infos.GoodName;
            txtSpec.Text = infos.Spec;
            txtProducer.Text = infos.Producer;
            goodId = infos.GoodId;
                       
        }
        //带出
        private void txtCstCode_Leave(object sender, EventArgs e)
        {
            CstInfo cstinfo = new CstInfo();
            if (StringUtils.IsNull(txtCstCode.Text))
            {
                MessageBox.Show("请输入活动对象代码", "前台提示");
                txtCstCode.Focus();
            }
            else {
                cstinfo=dao.searchCstInfo(txtCstCode.Text.ToString().Trim());
                if (StringUtils.IsNull(cstinfo.CstCode))
                {
                    MessageBox.Show("您输入的活动对象代码不存在，请重新输入!", "程序提示");
                    txtCstCode.Text = "";
                    txtCstCode.Focus();
                }
                else {
                    txtCstCode.Text = cstinfo.CstCode;
                    txtCstName.Text = cstinfo.CstName;
                    cstId = cstinfo.CstId;
                }

            }


        }

    }
}

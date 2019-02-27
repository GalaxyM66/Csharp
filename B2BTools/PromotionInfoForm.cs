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
    public partial class PromotionInfoForm : DockContent
    {
        APDao_B2BTools dao = new APDao_B2BTools();
        SortableBindingList<PromotionInfo> infoList = new SortableBindingList<PromotionInfo>();

        //操作临时表，用于存储过程
        SortableBindingList<DelTempGenCstGood> DelTempGenCstGoodList = new SortableBindingList<DelTempGenCstGood>();

        public PromotionInfoForm()
        {
            InitializeComponent();
            dgvPromotionInfo.AutoGenerateColumns = false;
        }
        private void clearUI() {

            infoList.Clear();

        }
        //查询
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            clearUI();
            int i = 0;
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            this.Cursor = Cursors.WaitCursor;
            if (StringUtils.IsNotNull(txtGoods.Text)) {
                sqlkeydict.Add("goods",txtGoods.Text.ToString().Trim());
                i++;
            }
            if (StringUtils.IsNotNull(txtProducer.Text))
            {
                sqlkeydict.Add("producer%", "%"+txtProducer.Text.ToString().Trim()+"%");
                i++;
            }
            if (StringUtils.IsNotNull(txtCstName.Text))
            {
                sqlkeydict.Add("cstname%", "%"+txtCstName.Text.ToString().Trim()+"%");
                i++;
            }
            if (StringUtils.IsNotNull(txtLModifyUser.Text))
            {
                sqlkeydict.Add("lmodifyuser%", "%" + txtLModifyUser.Text.ToString().Trim() + "%");
                i++;
            }
            if (StringUtils.IsNotNull(txtPocName.Text))
            {
                sqlkeydict.Add("pocname%", "%" + txtPocName.Text.ToString().Trim() + "%");
                i++;
            }
            if (i <= 0)
            {
                MessageBox.Show("请至少输入一个查询条件！", "前台提示");
                this.Cursor = Cursors.Default;
                return;
            }
            else {

               infoList=dao.GetPromotionInfo(sqlkeydict);
                if (infoList.Count <= 0)
                {
                    MessageBox.Show("未查询到数据！", "程序提示");
                    dgvPromotionInfo.DataSource = null;
                    this.Cursor = Cursors.Default;
                    
                }
                else {
                    dgvPromotionInfo.DataSource = infoList;
                    dgvPromotionInfo.Refresh();
                    dgvPromotionInfo.CurrentCell = null;
                    this.Cursor = Cursors.Default;
                }

            }

        }
        //全选
        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvPromotionInfo.RowCount <= 0) return;
            dgvPromotionInfo.SelectAll();
        }
        //新增
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AdOrUpPromoInfoForm add = new AdOrUpPromoInfoForm();
            add.Tag = dao;
            add.stateUI = 0;//传值为0，界面为新增页面
            add.ShowDialog();
            if (add.DialogResult == DialogResult.OK)
            {
                BtnQuery_Click(sender, e);
            }
        }
        //修改
        private void dgvPromotionInfo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //PromotionInfo info = dgvPromotionInfo.CurrentRow.DataBoundItem as PromotionInfo;

            //AdOrUpPromoInfoForm update = new AdOrUpPromoInfoForm();
            //update.promotionInfo = info;
            //update.Tag = dao;
            //update.stateUI = 1;//传值为1，界面为修改页面
            //update.ShowDialog();
            //if (update.DialogResult == DialogResult.OK)
            //{
            //    BtnQuery_Click(sender, e);
            //}
        }
        //删除
        private void BtnDel_Click(object sender, EventArgs e)
        {
            //取得选中的行
            object obj2 = FormUtils.SelectRows(dgvPromotionInfo);
            if (obj2 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            SortableBindingList<PromotionInfo> List = new SortableBindingList<PromotionInfo>();

            List.Clear();
            foreach (DataGridViewRow dgvr in row)
            {
                List.Add((PromotionInfo)dgvr.Cells[0].Value);
            }
            //读取GencstgoodList集合中的Relatid
            foreach (PromotionInfo info in List)
            {
                DelTempGenCstGood deleteInfo = new DelTempGenCstGood();
                deleteInfo.Relatid = int.Parse(info.Id);

                DelTempGenCstGoodList.Add(deleteInfo);
            }
            SPRetInfo ret = new SPRetInfo();
            //提示确认是否删除
            DialogResult result = MessageBox.Show("确认删除？", "温馨提示", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                //dao.DeleteGenCstGood(DelTempGenCstGoodList, ret);
                dao.DelInfo(DelTempGenCstGoodList,ret);
                if (ret.num == "1")
                {
                    MessageBox.Show(ret.msg + "|" + ret.result, "后台提示！");
                }
                else
                {
                    MessageBox.Show(ret.msg + "|" + ret.result, "后台提示！");
                    return;
                }
                DelTempGenCstGoodList.Clear();
                BtnQuery_Click(sender, e);
            }
            else
            {
                MessageBox.Show("您选择了取消删除！");
            }

        }

        private void PromotionInfoForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                BtnQuery_Click(sender, e);
            }
        }
        //批量修改
        private void BtnBatchUpdate_Click(object sender, EventArgs e)
        {
            Boolean flag = true;
            PromotionInfo batchUpInfo = new PromotionInfo();
            //取得选中的行
            object obj2 = FormUtils.SelectRows(dgvPromotionInfo);
            if (obj2 == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            SortableBindingList<PromotionInfo> batchList = new SortableBindingList<PromotionInfo>();

            batchList.Clear();
            foreach (DataGridViewRow dgvr in row)
            {
                batchList.Add((PromotionInfo)dgvr.Cells[0].Value);
            }
            if (batchList.Count <= 1)
            {
                MessageBox.Show("批量修改至少需要选择两项！", "前台提示");
                return;
            }
            //读取agreeClientList集合中的 GOODID和POCNAME是否一致 
            string symbol = batchList[0].GoodId;
            string symbol2 = batchList[0].PocName;
            for (int i = 0; i < batchList.Count; i++)
            {
                if (symbol != batchList[i].GoodId)
                {
                    MessageBox.Show("选择批量修改的数据--商品ID不一致！", "前台提示");
                    flag = false;
                    break;
                }
                else
                {
                    if (symbol2 != batchList[i].PocName)
                    {
                        MessageBox.Show("选择批量修改的数据--活动政策名称不一致！", "前台提示");
                        flag = false;
                        break;
                    }
                    else {
                        continue;
                    }                   
                }

            }
            if (flag == true)
            {
                //读取集合中的字段
                foreach (PromotionInfo info in batchList)
                {
                    batchUpInfo.Compid = info.Compid;
                    batchUpInfo.Ownerid = info.Ownerid;
                    batchUpInfo.GoodId = info.GoodId;
                    batchUpInfo.Goods = info.Goods;
                    batchUpInfo.GoodName = info.GoodName;

                }

                PromoBatchUpForm BatchupdateAgrProd = new PromoBatchUpForm();
                BatchupdateAgrProd.batchUpInfo = batchUpInfo;
                BatchupdateAgrProd.batchLists = batchList;
                BatchupdateAgrProd.Tag = dao;
                BatchupdateAgrProd.ShowDialog();
                if (BatchupdateAgrProd.DialogResult == DialogResult.OK)
                {
                    BtnQuery_Click(sender, e);
                }
            }

        }
        //Excel导入
        private void BtnExcel_Click(object sender, EventArgs e)
        {
            PromoInfoXlsForm importXslForm = new PromoInfoXlsForm();
            importXslForm.Tag = dao;
            importXslForm.Text = "Excel导入";
            importXslForm.ShowDialog();

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
 
 
 

namespace PriceManager
{
    public partial class GoodsEditForm : DockContent
    {
        PMBaseDao dao = new PMBaseDao();
        SortableBindingList<PubWaredict> goodsList = new SortableBindingList<PubWaredict>();
        SortableBindingList<GoodsSub> goodsAreaList = new SortableBindingList<GoodsSub>();
        SortableBindingList<GoodsSub> goodsForbitAreaList = new SortableBindingList<GoodsSub>();
        SortableBindingList<GoodsSub> goodsLimitList = new SortableBindingList<GoodsSub>();
        SortableBindingList<DictionarySub> areaList = new SortableBindingList<DictionarySub>();
        SortableBindingList<Dictionary> limitList = new SortableBindingList<Dictionary>();
        SortableBindingList<ImportGoodsSub> importList = new SortableBindingList<ImportGoodsSub>();
        SortableBindingList<ImportGoodsSub> importListTmp = new SortableBindingList<ImportGoodsSub>();
        ResultInfo resultMsg = new ResultInfo();
        public GoodsEditForm()
        {
            InitializeComponent();
        }

        private void GoodsEditForm_Load(object sender, EventArgs e)
        {
            SortableBindingList<Dictionary> stopFlagList = StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("1"));
            stopFlagList.Insert(0, new Dictionary { Name = "全部", Code = "" });
            FormUtils.SetComboBox(stopFlag_q, stopFlagList, "Name", "TagPtr");
            RefreshDataGridView5();
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            if (StringUtils.IsNull(goodsCode.Text) && StringUtils.IsNull(goodsName.Text) && StringUtils.IsNull(spec.Text) && StringUtils.IsNull(producer.Text))
            {
                MessageBox.Show("请填写搜索条件!!");
                goodsCode.Focus();
                return;
            }
            RefreshDataGridView1();
        }

        private void AreaQueryBtn_Click(object sender, EventArgs e)
        {
            RefreshDataGridView3(queryText.Text.Trim());
        }

        private void GoodsEditDgv1_SelectionChanged(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow(goodsEditDgv1);
            if (obj == null) return;
            PubWaredict goods = (PubWaredict)obj;
            RefreshDataGridView2(goods.Goodid);
            RefreshDataGridView4(goods.Goodid);
            RefreshDataGridView6(goods.Goodid);
        }

        private void InAreaBtn_Click(object sender, EventArgs e)
        {
            object obj1 = FormUtils.SelectRow(goodsEditDgv1);
            if (obj1 == null) return;
            object obj2 = FormUtils.SelectRows(goodsEditDgv3);
            if (obj2 == null) return;
            PubWaredict goods = (PubWaredict)obj1;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            foreach (DataGridViewRow dgvr in row)
            {
                string[] result = dao.AddGoodsSub(goods.Goodid, "12", ((DictionarySub)dgvr.Cells[0].Value).Code);
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            RefreshDataGridView2(goods.Goodid);
        }

        private void OutAreaBtn_Click(object sender, EventArgs e)
        {
            object obj1 = FormUtils.SelectRow(goodsEditDgv1);
            if (obj1 == null) return;
            object obj2 = FormUtils.SelectRows(goodsEditDgv2);
            if (obj2 == null) return;
            PubWaredict goods = (PubWaredict)obj1;

            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            foreach (DataGridViewRow dgvr in row)
            {
                string[] result = dao.RemoveGoodsSub(goods.Goodid, ((GoodsSub)dgvr.Cells[0].Value).Subtype, ((GoodsSub)dgvr.Cells[0].Value).Subid);
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            RefreshDataGridView2(goods.Goodid);
        }

        private void RefreshDataGridView1()
        {
            goodsList = StringUtils.TableToEntity<PubWaredict>(dao.GetAllGoods(goodsCode.Text.Trim(), goodsName.Text.Trim(), spec.Text.Trim(), producer.Text.Trim(), ((Dictionary)stopFlag_q.SelectedValue).Code));
            FormUtils.RefreshDataGridView(goodsEditDgv1, goodsList);
            count.Text = "共" + goodsList.Count.ToString() + "条数据。";
            goodsEditDgv1.Focus();
        }

        private void RefreshDataGridView2(string goodId)
        {
            goodsAreaList = StringUtils.TableToEntity<GoodsSub>(dao.GetGoodsSub(goodId,"12"));
            FormUtils.RefreshDataGridView(goodsEditDgv2, goodsAreaList);
        }

        private void RefreshDataGridView3(string value)
        {
            areaList = StringUtils.TableToEntity<DictionarySub>(dao.GetAreaDictionary(value,""));
            FormUtils.RefreshDataGridView(goodsEditDgv3, areaList);
        }

        private void RefreshDataGridView4(string goodId)
        {
            goodsLimitList = StringUtils.TableToEntity<GoodsSub>(dao.GetGoodsSub(goodId, "13"));
            FormUtils.RefreshDataGridView(goodsEditDgv4, goodsLimitList);
        }

        private void RefreshDataGridView5()
        {
            limitList = StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("13"));
            FormUtils.RefreshDataGridView(goodsEditDgv5, limitList);
        }
        private void RefreshDataGridView6(string goodId)
        {
            goodsForbitAreaList = StringUtils.TableToEntity<GoodsSub>(dao.GetGoodsSub(goodId, "29"));
            FormUtils.RefreshDataGridView(goodsEditDgv6, goodsForbitAreaList);
        }
        private void RefreshDataGridView7(string value)
        {
            areaList = StringUtils.TableToEntity<DictionarySub>(dao.GetAreaDictionary(value, ""));
            FormUtils.RefreshDataGridView(goodsEditDgv7, areaList);
        }

        private void InLimitBtn_Click(object sender, EventArgs e)
        {
            object obj1 = FormUtils.SelectRow(goodsEditDgv1);
            if (obj1 == null) return;
            object obj2 = FormUtils.SelectRows(goodsEditDgv5);
            if (obj2 == null) return;
            PubWaredict goods = (PubWaredict)obj1;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            foreach (DataGridViewRow dgvr in row)
            {
                string[] result = dao.AddGoodsSub(goods.Goodid, ((Dictionary)dgvr.Cells[0].Value).Typeid, ((Dictionary)dgvr.Cells[0].Value).Code);
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            RefreshDataGridView4(goods.Goodid);
        }

        private void OutLimitBtn_Click(object sender, EventArgs e)
        {
            object obj1 = FormUtils.SelectRow(goodsEditDgv1);
            if (obj1 == null) return;
            object obj2 = FormUtils.SelectRows(goodsEditDgv4);
            if (obj2 == null) return;
            PubWaredict goods = (PubWaredict)obj1;

            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            foreach (DataGridViewRow dgvr in row)
            {
                string[] result = dao.RemoveGoodsSub(goods.Goodid, ((GoodsSub)dgvr.Cells[0].Value).Subtype, ((GoodsSub)dgvr.Cells[0].Value).Subid);
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            RefreshDataGridView4(goods.Goodid);
        }

        private void InForbitAreaBtn_Click(object sender, EventArgs e)
        {
            object obj1 = FormUtils.SelectRow(goodsEditDgv1);
            if (obj1 == null) return;
            object obj2 = FormUtils.SelectRows(goodsEditDgv7);
            if (obj2 == null) return;
            PubWaredict goods = (PubWaredict)obj1;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            foreach (DataGridViewRow dgvr in row)
            {
                string[] result = dao.AddGoodsSub(goods.Goodid, "29", ((DictionarySub)dgvr.Cells[0].Value).Code);
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            RefreshDataGridView6(goods.Goodid);
        }

        private void OutForbitAreaBtn_Click(object sender, EventArgs e)
        {
            object obj1 = FormUtils.SelectRow(goodsEditDgv1);
            if (obj1 == null) return;
            object obj2 = FormUtils.SelectRows(goodsEditDgv6);
            if (obj2 == null) return;
            PubWaredict goods = (PubWaredict)obj1;

            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            foreach (DataGridViewRow dgvr in row)
            {
                string[] result = dao.RemoveGoodsSub(goods.Goodid, ((GoodsSub)dgvr.Cells[0].Value).Subtype, ((GoodsSub)dgvr.Cells[0].Value).Subid);
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            RefreshDataGridView6(goods.Goodid);
        }

        private void AreaQueryBtn2_Click(object sender, EventArgs e)
        {
            RefreshDataGridView7(queryText2.Text.Trim());
        }

        private void GoodsEditForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryBtn_Click(sender, e);
            }
        }

        private void SelectExcelBtn_Click(object sender, EventArgs e)
        {
            importList.Clear();
            openFileDialog1.Filter = "所有文件(*.*)|*.*";
            DataSet ds = null;
            string msg = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath.Text = openFileDialog1.FileName;
                try
                {
                    ds = ExcelHelper.GetDataSetByExcel(filePath.Text, "Sheet1", out msg);
                    importList = StringUtils.TableToEntity<ImportGoodsSub>(ds.Tables[0]);
                    importList.Remove(importList.First());
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                }
                MessageBox.Show(msg);
            }
            FormUtils.RefreshDataGridView(goodsEditDgv8, importList);
            checkBtn.Enabled = true;
            importBtn.Enabled = false;
            errorExport.Visible = false;
        }

        private void CheckBtn_Click(object sender, EventArgs e)
        {
            int i = 0;
            if (importList.Count == 0) return;
            foreach (var importData in importList)
            {
                importData.Needcheck = "1";
                importData.Checktext = "";
                string[] result = dao.CheckWaredictData(importData);
                importData.Needcheck = result[0];
                importData.Checktext += result[1];
                if ("0".Equals(importData.Needcheck)) i++;
            }
            FormUtils.RefreshDataGridView(goodsEditDgv8, importList);
            importBtn.Enabled = true;
            if (i > 0) errorExport.Visible = true;
        }

        private void ImportBtn_Click(object sender, EventArgs e)
        {
            importListTmp.Clear();
            foreach (var importValue in importList)
            {
                if (!"1".Equals(importValue.Needcheck))
                {
                    importListTmp.Add(importValue);
                    continue;
                }
                string[] result = dao.ImportWaredictSub(importValue);//数据导入方法
                result[1] += ",商品编码:" + importValue.Goods;
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            FormUtils.RefreshDataGridView(goodsEditDgv8, importListTmp);
        }

        private void TemplateDownload_Click(object sender, EventArgs e)
        {
            FormUtils.TmpDownload(saveFileDialog1, "商品属性模板", Properties.Settings.Default.WAREDICT_SUB_TMP_URL);
        }

        private void ErrorExport_Click(object sender, EventArgs e)
        {
            FormUtils.ExcelExport(goodsEditDgv8, saveFileDialog1, "错误数据");
        }
    }
}

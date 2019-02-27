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
    public partial class GoodsGroupForm : DockContent
    {
        PMBaseDao baseDao = new PMBaseDao();
        PMPriceDao dao = new PMPriceDao();
        SortableBindingList<GoodsGroups> groupList = new SortableBindingList<GoodsGroups>();
        SortableBindingList<PubWaredict> groupGoodsList = new SortableBindingList<PubWaredict>();
        SortableBindingList<PubWaredict> goodsList = new SortableBindingList<PubWaredict>();
        ResultInfo resultMsg = new ResultInfo();
        string option = null;
        public GoodsGroupForm()
        {
            InitializeComponent();
        }

        private void GoodsGroupForm_Load(object sender, EventArgs e)
        {
            SortableBindingList<Dictionary> stopFlagList = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("1"));
            SortableBindingList<Dictionary> stopFlagList_s = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("1"));
            stopFlagList.Insert(0, new Dictionary { Name = "全部", Code = "" });
            FormUtils.SetComboBox(stopFlag_s, stopFlagList_s, "Name", "TagPtr");
            FormUtils.SetComboBox(stopFlag_q, stopFlagList, "Name", "TagPtr");
            SortableBindingList<Dictionary> groupTypeList = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("18"));
            FormUtils.SetComboBox(groupType_s, groupTypeList, "Name", "TagPtr");
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            InitForm();
            RefreshDataGridView1();
        }

        private void InitForm()
        {
            option = "add";
            groupCode_s.Text = "";
            groupName_s.Text = "";
            groupId_s.Text = "";
            stopFlag_s.SelectedIndex = 0;
            mark_s.Text = "";
            groupType_s.SelectedIndex = 0;
            groupType_s.Enabled = true;
            groupGoodsList.Clear();
        }

        private void RefreshDataGridView1()
        {
            groupList = StringUtils.TableToEntity<GoodsGroups>(dao.GetGoodsGroups(groupCode_q.Text.Trim(), groupName_q.Text.Trim(), ((Dictionary)stopFlag_q.SelectedValue).Code));
            FormUtils.RefreshDataGridView(goodsGroupDgv1, groupList);
            goodsGroupDgv1.Focus();
        }

        private void RefreshDataGridView2(string groupId)
        {
            groupGoodsList = StringUtils.TableToEntity<PubWaredict>(baseDao.GetGoodsByGroupId("","",groupId, "10", ""));
            FormUtils.RefreshDataGridView(goodsGroupDgv2, groupGoodsList);
        }

        private void RefreshDataGridView3()
        {
            goodsList = StringUtils.TableToEntity<PubWaredict>(baseDao.GetGoods(goodsCode.Text.Trim(), goodsName.Text.Trim(), spec.Text.Trim(), producer.Text.Trim(),"00"));
            FormUtils.RefreshDataGridView(goodsGroupDgv3, goodsList);
        }

        private void GoodsGroupDgv1_SelectionChanged(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow(goodsGroupDgv1);
            if (obj == null) return;
            option = "edit";
            GoodsGroups groups = (GoodsGroups)obj;
            groupCode_s.Text = groups.Code;
            groupName_s.Text = groups.Groupname;
            groupId_s.Text = groups.Id;
            stopFlag_s.SelectedIndex = stopFlag_s.FindString(groups.Stopflagname);
            mark_s.Text = groups.Mark;
            groupType_s.SelectedIndex = groupType_s.FindString(groups.Typename);
            groupType_s.Enabled = false;
            if ("10".Equals(groups.Type))
            {
                inBtn.Enabled = true;
                outBtn.Enabled = true;
                goodsQueryBtn.Enabled = true;
                RefreshDataGridView2(groups.Id);
            }
            else if ("20".Equals(groups.Type))
            {
                groupGoodsList.Clear();
                inBtn.Enabled = false;
                outBtn.Enabled = false;
                goodsQueryBtn.Enabled = false;
            }
        }

        private void NewBtn_Click(object sender, EventArgs e)
        {
            InitForm();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string[] result = { "", "failed" };
            if ("add".Equals(option))
            {
                if (StringUtils.IsNull(groupCode_s.Text)) return;
                result = dao.NewGoodsGroup(groupCode_s.Text.Trim(), groupName_s.Text.Trim(), mark_s.Text.Trim(), ((Dictionary)groupType_s.SelectedValue).Code, ((Dictionary)stopFlag_s.SelectedValue).Code);
            }
            else if ("edit".Equals(option))
            {
                if (StringUtils.IsNull(groupId_s.Text)) return;
                result = dao.EditGoodsGroup(groupId_s.Text.Trim(),groupCode_s.Text.Trim(), groupName_s.Text.Trim(), mark_s.Text.Trim(), ((Dictionary)stopFlag_s.SelectedValue).Code);
            }

            if ("1".Equals(result[0]))
            {
                QueryBtn_Click(sender, e);
            }
            else
            {
                MessageBox.Show(result[1]);
            }
        }

        private void InBtn_Click(object sender, EventArgs e)
        {
            object obj1 = FormUtils.SelectRow(goodsGroupDgv1);
            if (obj1 == null) return;
            object obj2 = FormUtils.SelectRows(goodsGroupDgv3);
            if (obj2 == null) return;
            GoodsGroups group = (GoodsGroups)obj1;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            foreach (DataGridViewRow dgvr in row)
            {
                string[] result = dao.AddGoodsGroupDetails(group.Id, ((PubWaredict)dgvr.Cells[0].Value).Goodid);
                if ("1".Equals(result[0]))
                    groupGoodsList.Add((PubWaredict)dgvr.Cells[0].Value);
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
        }

        private void OutBtn_Click(object sender, EventArgs e)
        {
            object obj1 = FormUtils.SelectRow(goodsGroupDgv1);
            if (obj1 == null) return;
            object obj2 = FormUtils.SelectRows(goodsGroupDgv2);
            if (obj2 == null) return;
            GoodsGroups group = (GoodsGroups)obj1;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            foreach (DataGridViewRow dgvr in row)
            {
                string[] result = dao.RemoveGoodsGroupDetails(group.Id, ((PubWaredict)dgvr.Cells[0].Value).Goodid);
                if ("1".Equals(result[0]))
                    groupGoodsList.Remove((PubWaredict)dgvr.Cells[0].Value);
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
        }

        private void GoodsQueryBtn_Click(object sender, EventArgs e)
        {
            RefreshDataGridView3();
        }

        private void GoodsGroupForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryBtn_Click(sender, e);
            }
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow(goodsGroupDgv1);
            if (obj == null) return;
            GoodsGroups group = (GoodsGroups)obj;
            FormUtils.ExcelExport(goodsGroupDgv2, saveFileDialog1, "商品组_"+ group.Groupname);
        }
    }
}

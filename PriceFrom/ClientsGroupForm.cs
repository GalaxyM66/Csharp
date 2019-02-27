 
 
 
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
    public partial class ClientsGroupForm : DockContent
    {
        PMBaseDao baseDao = new PMBaseDao();
        PMPriceDao dao = new PMPriceDao();
        SortableBindingList<ClientsGroups> groupList = new SortableBindingList<ClientsGroups>();
        SortableBindingList<PubClients> groupClientsList = new SortableBindingList<PubClients>();
        SortableBindingList<PubClients> clientsList = new SortableBindingList<PubClients>();
        ResultInfo resultMsg = new ResultInfo();
        string option = null;
        public ClientsGroupForm()
        {
            InitializeComponent();
        }

        private void ClientsGroupForm_Load(object sender, EventArgs e)
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
            RefreshDataGridView1();
            queryBtn.Select();
        }

        private void RefreshDataGridView1()
        {
            groupList = StringUtils.TableToEntity<ClientsGroups>(dao.GetCliensGroups(groupCode_q.Text.Trim(), groupName_q.Text.Trim(), ((Dictionary)stopFlag_q.SelectedValue).Code));
            FormUtils.RefreshDataGridView(clientsGroupDgv1, groupList);
            clientsGroupDgv1.Focus();
        }

        private void RefreshDataGridView2(string groupId)
        {
            groupClientsList = StringUtils.TableToEntity<PubClients>(baseDao.GetClientsByGroupId("", "", groupId, "10", ""));
            FormUtils.RefreshDataGridView(clientsGroupDgv2, groupClientsList);
        }

        private void RefreshDataGridView3()
        {
            clientsList = StringUtils.TableToEntity<PubClients>(baseDao.GetClients(clientsCode.Text.Trim(), clientsName.Text.Trim(),
                areaCbo.Text.Trim(),"00","",""));
            FormUtils.RefreshDataGridView(clientsGroupDgv3, clientsList);
        }

        private void ClientsGroupDgv1_SelectionChanged(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow(clientsGroupDgv1);
            if (obj == null) return;
            option = "edit";
            ClientsGroups groups = (ClientsGroups)obj;
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
                clientsQueryBtn.Enabled = true;
                RefreshDataGridView2(groups.Id);
            }
            else if ("20".Equals(groups.Type))
            {
                groupClientsList.Clear();
                inBtn.Enabled = false;
                outBtn.Enabled = false;
                clientsQueryBtn.Enabled = false;
            }
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
            groupClientsList.Clear();
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
                result = dao.NewClientsGroup(groupCode_s.Text.Trim(), groupName_s.Text.Trim(), mark_s.Text.Trim(), ((Dictionary)groupType_s.SelectedValue).Code, ((Dictionary)stopFlag_s.SelectedValue).Code);
            }
            else if ("edit".Equals(option))
            {
                if (StringUtils.IsNull(groupId_s.Text)) return;
                result = dao.EditClientsGroup(groupId_s.Text.Trim(), groupCode_s.Text.Trim(), groupName_s.Text.Trim(), mark_s.Text.Trim(), ((Dictionary)stopFlag_s.SelectedValue).Code);
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

        private void ClientsQueryBtn_Click(object sender, EventArgs e)
        {
            RefreshDataGridView3();
        }

        private void InBtn_Click(object sender, EventArgs e)
        {
            object obj1 = FormUtils.SelectRow(clientsGroupDgv1);
            if (obj1 == null) return;
            object obj2 = FormUtils.SelectRows(clientsGroupDgv3);
            if (obj2 == null) return;
            ClientsGroups group = (ClientsGroups)obj1;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            foreach (DataGridViewRow dgvr in row)
            {
                string[] result = dao.AddClientsGroupDetails(group.Id, ((PubClients)dgvr.Cells[0].Value).Cstid);
                if ("1".Equals(result[0]))
                    groupClientsList.Add((PubClients)dgvr.Cells[0].Value);
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
        }

        private void OutBtn_Click(object sender, EventArgs e)
        {
            object obj1 = FormUtils.SelectRow(clientsGroupDgv1);
            if (obj1 == null) return;
            object obj2 = FormUtils.SelectRows(clientsGroupDgv2);
            if (obj2 == null) return;
            ClientsGroups group = (ClientsGroups)obj1;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            foreach (DataGridViewRow dgvr in row)
            {
                string[] result = dao.RemoveClientsGroupDetails(group.Id, ((PubClients)dgvr.Cells[0].Value).Cstid);
                if ("1".Equals(result[0]))
                    groupClientsList.Remove((PubClients)dgvr.Cells[0].Value);
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
        }

        private void AreaCbo_Click(object sender, EventArgs e)
        {
            FormUtils.AreasComboBoxSetting(sender);
        }

        private void ClientsGroupForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                    QueryBtn_Click(sender, e);
            }
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow(clientsGroupDgv1);
            if (obj == null) return;
            ClientsGroups group = (ClientsGroups)obj;
            FormUtils.ExcelExport(clientsGroupDgv2, saveFileDialog1, "客户组_" + group.Groupname);
        }
    }
}

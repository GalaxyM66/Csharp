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
    public partial class ClientsEditForm : DockContent
    {
        PMBaseDao dao = new PMBaseDao();
        PMSystemDao sysDao = new PMSystemDao();
        SortableBindingList<PubClients> clientsList = new SortableBindingList<PubClients>();
        SortableBindingList<ClientsSub> clientsDeptList = new SortableBindingList<ClientsSub>();
        SortableBindingList<Dictionary> typeList = new SortableBindingList<Dictionary>();
        SortableBindingList<ClientsSub> clientTypeList = new SortableBindingList<ClientsSub>();
        SortableBindingList <PubDept> deptList = new SortableBindingList<PubDept>();
        ResultInfo resultMsg = new ResultInfo();
        public ClientsEditForm()
        {
            InitializeComponent();
        }

        private void ClientsEditForm_Load(object sender, EventArgs e)
        {
            SortableBindingList<Dictionary> stopFlagList = StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("1"));
            SortableBindingList<DictionarySub> clientsTypeList = StringUtils.TableToEntity<DictionarySub>(dao.GetClientsTypeDictionary("", ""));
            SortableBindingList<Dictionary> payTypeList = StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("17"));
            stopFlagList.Insert(0, new Dictionary { Name = "全部", Code = "" });
            clientsTypeList.Insert(0, new DictionarySub { Name = "全部", Code = "" });
            FormUtils.SetComboBox(stopFlag_q, stopFlagList, "Name", "TagPtr");
            FormUtils.SetComboBox(clientsType, clientsTypeList, "Name", "TagPtr");
            FormUtils.SetComboBox(paytype, payTypeList, "Name", "TagPtr");
            RefreshDataGridView3();
            RefreshDataGridView5();
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            if (StringUtils.IsNull(clientsCode.Text) && StringUtils.IsNull(clientsName.Text) && areaCbo.DataSource == null)
            {
                MessageBox.Show("请填写搜索条件!!");
                clientsCode.Focus();
                return;
            }
            RefreshDataGridView1();
        }

        private void ClientsEditDgv1_SelectionChanged(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRow(clientsEditDgv1);
            if (obj == null) return;
            PubClients clients = (PubClients)obj;
            RefreshDataGridView2(clients.Cstid);
            RefreshDataGridView4(clients.Cstid);
        }

        private void InAreaTypeBtn_Click(object sender, EventArgs e)
        {
            object obj1 = FormUtils.SelectRow(clientsEditDgv1);
            if (obj1 == null) return;
            object obj2 = FormUtils.SelectRow(clientsEditDgv3);
            if (obj2 == null) return;
            if (clientsDeptList.Count > 0) return;
            PubClients clients = (PubClients)obj1;
            string[] result = dao.AddClientsSub(clients.Cstid, "30", ((PubDept)obj2).Saledeptid);
            if ("1".Equals(result[0]))
                RefreshDataGridView2(clients.Cstid);
            else
                MessageBox.Show(result[1]);
        }

        private void OutAreaTypeBtn_Click(object sender, EventArgs e)
        {
            object obj1 = FormUtils.SelectRow(clientsEditDgv1);
            if (obj1 == null) return;
            object obj2 = FormUtils.SelectRows(clientsEditDgv2);
            if (obj2 == null) return;
            PubClients clients = (PubClients)obj1;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            foreach (DataGridViewRow dgvr in row)
            {
                string[] result = dao.RemoveClientsSub(clients.Cstid, ((ClientsSub)dgvr.Cells[0].Value).Subtype, ((ClientsSub)dgvr.Cells[0].Value).Subid);
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            RefreshDataGridView2(clients.Cstid);
            RefreshDataGridView4(clients.Cstid);
        }

        private void RefreshDataGridView1()
        {
            clientsList = StringUtils.TableToEntity<PubClients>(dao.GetAllClients(clientsCode.Text.Trim(), clientsName.Text.Trim(),
                areaCbo.Text.Trim(), ((Dictionary)stopFlag_q.SelectedValue).Code,
                ((Dictionary)paytype.SelectedValue).Code, ((DictionarySub)clientsType.SelectedValue).Code));
            FormUtils.RefreshDataGridView(clientsEditDgv1, clientsList);
            clientsEditDgv1.Focus();
            count.Text = "共" + clientsList.Count.ToString() + "条数据。";
        }

        private void RefreshDataGridView2(string clientsId)
        {
            clientsDeptList = StringUtils.TableToEntity<ClientsSub>(dao.GetClientsSub(clientsId, "30"));
            FormUtils.RefreshDataGridView(clientsEditDgv2, clientsDeptList);
        }

        private void RefreshDataGridView3()
        {
            deptList = StringUtils.TableToEntity<PubDept>(sysDao.GetAllDept("flag"));
            FormUtils.RefreshDataGridView(clientsEditDgv3, deptList);
        }

        private void RefreshDataGridView4(string clientsId)
        {
            clientTypeList = StringUtils.TableToEntity<ClientsSub>(dao.GetClientsSub(clientsId, "25"));
            FormUtils.RefreshDataGridView(clientsEditDgv4, clientTypeList);
        }
        private void RefreshDataGridView5()
        {
            typeList = StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("25"));
            FormUtils.RefreshDataGridView(clientsEditDgv5, typeList);
        }

        private void AreaCbo_Click(object sender, EventArgs e)
        {
            FormUtils.AreasComboBoxSetting(sender);
        }

        private void ClientsEditForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryBtn_Click(sender, e);
            }
        }

        private void InClientTypeBtn_Click(object sender, EventArgs e)
        {
            object obj1 = FormUtils.SelectRow(clientsEditDgv1);
            if (obj1 == null) return;
            object obj2 = FormUtils.SelectRow(clientsEditDgv5);
            if (obj2 == null) return;
            if (clientTypeList.Count > 0) return;
            PubClients clients = (PubClients)obj1;
            string[] result = dao.AddClientsSub(clients.Cstid, "25", ((Dictionary)obj2).Code);
            if ("1".Equals(result[0]))
                RefreshDataGridView4(clients.Cstid);
            else
                MessageBox.Show(result[1]);
        }

        private void OutClientTypeBtn_Click(object sender, EventArgs e)
        {
            object obj1 = FormUtils.SelectRow(clientsEditDgv1);
            if (obj1 == null) return;
            object obj2 = FormUtils.SelectRows(clientsEditDgv4);
            if (obj2 == null) return;
            PubClients clients = (PubClients)obj1;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj2;
            foreach (DataGridViewRow dgvr in row)
            {
                string[] result = dao.RemoveClientsSub(clients.Cstid, ((ClientsSub)dgvr.Cells[0].Value).Subtype, ((ClientsSub)dgvr.Cells[0].Value).Subid);
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
            RefreshDataGridView4(clients.Cstid);
        }
    }
}

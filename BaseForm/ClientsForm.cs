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
    public partial class ClientsForm : DockContent
    {
        PMBaseDao dao = new PMBaseDao();
        SortableBindingList<PubClients> clientsList = new SortableBindingList<PubClients>();
        public ClientsForm()
        {
            InitializeComponent();
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            //if (StringUtils.IsNull(clientsCode.Text) && StringUtils.IsNull(clientsName.Text) && areaCbo.DataSource == null)
            //{
            //    MessageBox.Show("请填写搜索条件!!");
            //    clientsCode.Focus();
            //    return;
            //}
            //clientsList = StringUtils.TableToEntity<PubClients>(dao.GetClients(clientsCode.Text.Trim(),clientsName.Text.Trim(),
            //    areaCbo.Text.Trim(),((Dictionary)stopFlag_q.SelectedValue).Code,
            //    ((Dictionary)paytype.SelectedValue).Code,((DictionarySub)clientsType.SelectedValue).Code));
            //FormUtils.RefreshDataGridView(clientsDgv1, clientsList);
            //count.Text = "共" + clientsList.Count.ToString() + "条数据。";
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation. 
                backgroundWorker1.RunWorkerAsync(new PubClients()
                {
                    Cstcode = clientsCode.Text.Trim(),
                    Cstname = clientsName.Text.Trim(),
                    Area = areaCbo.Text.Trim(),
                    Stopflag = ((Dictionary)stopFlag_q.SelectedValue).Code,
                    Paytype = ((Dictionary)paytype.SelectedValue).Code,
                    Clienttype = ((DictionarySub)clientsType.SelectedValue).Code
                });
                queryBtn.Text = "查询中";
                queryBtn.Enabled = false;
            }
        }

        private void ClientsForm_Load(object sender, EventArgs e)
        {
            SortableBindingList<Dictionary> stopFlagList = StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("1"));
            SortableBindingList<DictionarySub> clientsTypeList = StringUtils.TableToEntity<DictionarySub>(dao.GetClientsTypeDictionary("",""));
            SortableBindingList<Dictionary> payTypeList = StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("17"));
            stopFlagList.Insert(0, new Dictionary { Name = "全部", Code = "" });
            clientsTypeList.Insert(0, new DictionarySub { Name = "全部", Code = "" });
            FormUtils.SetComboBox(stopFlag_q, stopFlagList, "Name", "TagPtr");
            FormUtils.SetComboBox(clientsType, clientsTypeList, "Name", "TagPtr");
            FormUtils.SetComboBox(paytype, payTypeList, "Name", "TagPtr");
        }

        private void AreaCbo_Click(object sender, EventArgs e)
        {
            FormUtils.AreasComboBoxSetting(sender);
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            FormUtils.ExcelExport(clientsDgv1, saveFileDialog1, "客户");
        }

        private void ClientsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryBtn_Click(sender, e);
            }
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            PubClients clinets = e.Argument as PubClients;
            clientsList = StringUtils.TableToEntity<PubClients>(dao.GetClients(clinets.Cstcode, clinets.Cstname, 
                clinets.Area, clinets.Stopflag, clinets.Paytype, clinets.Clienttype));
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FormUtils.RefreshDataGridView(clientsDgv1, clientsList);
            count.Text = "共" + clientsList.Count.ToString() + "条数据。";
            queryBtn.Text = "查询";
            queryBtn.Enabled = true;
        }
    }
}

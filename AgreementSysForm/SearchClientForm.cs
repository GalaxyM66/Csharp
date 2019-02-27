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
    public partial class SearchClientForm : Form
    {
        APDao_Agreement dao = new APDao_Agreement();
        SPRetInfo retinfo = new SPRetInfo();
        SortableBindingList<SearchClientInfo> infoList = new SortableBindingList<SearchClientInfo>();
        //声明委托和事件
        public delegate void TransfDelegate(SearchClientInfo clientInfo);
        public event TransfDelegate TrandfEvent;

        public SearchClientForm()
        {
            InitializeComponent();
            dgvSearchClient.AutoGenerateColumns = false;
            
        }
        private void clearUI() {
            infoList.Clear();
        }
        //查询事件
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            int i = 0;
            this.Cursor = Cursors.WaitCursor;
            Dictionary<string, string> sqlkeydict = new Dictionary<string, string>();
            if (!StringUtils.IsNull(txtCstCode.Text)) {
                sqlkeydict.Add("cstcode",txtCstCode.Text.ToString().Trim());
                i++;
            }
            if (!StringUtils.IsNull(txtCstName.Text))
            {
                sqlkeydict.Add("dname%", "%"+txtCstName.Text.ToString().Trim()+"%");
                i++;
            }
            if (i <= 0)
            {
                MessageBox.Show("请至少输入一个查询条件！", "前台提示");
                this.Cursor = Cursors.Default;
                return;
            }
            else {
                infoList=dao.GetSearchClientInfo(sqlkeydict,retinfo);
                if (infoList.Count<=0) {
                    MessageBox.Show("未查询到数据！","后台提示");
                    this.Cursor = Cursors.Default;
                    return;
                }
                else {
                    dgvSearchClient.DataSource = infoList;
                    dgvSearchClient.CurrentCell = null;
                    dgvSearchClient.Refresh();
                    this.Cursor = Cursors.Default;                   
                }


            }

        }
        //双击选择信息传给父窗口
        private void dgvSearchClient_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SearchClientInfo info = dgvSearchClient.CurrentRow.DataBoundItem as SearchClientInfo;
            SearchClientInfo infos = new SearchClientInfo();
            infos=dao.SelDetail(info.CstId);
            infos.CstId = info.CstId;
            infos.CstCode = info.CstCode;
            infos.CstName = info.CstName;
            //触发事件
            TrandfEvent(infos);
            this.Close();
        }

        private void SearchClientForm_Load(object sender, EventArgs e)
        {
            this.Text = "厂家查询";
        }
    }
}

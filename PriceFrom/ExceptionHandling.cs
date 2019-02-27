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
    public partial class ExceptionHandling : DockContent
    {
        PMPriceDao dao = new PMPriceDao();
        PMBaseDao baseDao = new PMBaseDao();
        PMSystemDao sysDao = new PMSystemDao();
        SortableBindingList<ExceptionPrice> PriceList = new SortableBindingList<ExceptionPrice>();

        ResultInfo resultMsg = new ResultInfo();

        public ExceptionHandling()
        {
            InitializeComponent();
        }

        private void ExceptionHandling_Load(object sender, EventArgs e)
        {
            SortableBindingList<Dictionary> exceptionTypeList = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("42"));
            SortableBindingList<Dictionary> handleFlagList = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("44"));
            exceptionTypeList.Insert(0, new Dictionary { Name = "全部", Code = "" });
            handleFlagList.Insert(0, new Dictionary { Name = "全部", Code = "" });
            FormUtils.SetComboBox(exceptionType, exceptionTypeList, "Name", "TagPtr");
            FormUtils.SetComboBox(handleFlag, handleFlagList, "Name", "TagPtr");
            handleFlag.SelectedIndex = 2;

            SortableBindingList<PubEmp> buyerList = StringUtils.TableToEntity<PubEmp>(sysDao.GetBuyer());
            buyerList.Insert(0, new PubEmp { Empname = "全部", Empcode = "" });
            FormUtils.SetComboBox(buyerCbo, buyerList, "Empname", "TagPtr");

            if ("108".Equals(SessionDto.Emproleid))
            {
                buyerCbo.Visible = false;
                label3.Visible = false;
                buyerCbo.DataSource = null;
            }
            if ("105".Equals(SessionDto.Emproleid) || "99".Equals(SessionDto.Emproleid))
            {
                toolStripSeparator3.Visible = true;
                stopBtn.Visible = true;
            }
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            PriceList = StringUtils.TableToEntity<ExceptionPrice>(
                dao.GetExceptionPrice(((Dictionary)exceptionType.SelectedValue).Code, ((Dictionary)handleFlag.SelectedValue).Code, 
                buyerCbo.DataSource == null ? SessionDto.Empcode : ((PubEmp)buyerCbo.SelectedValue).Empcode,
                goodsCode.Text.Trim(),clientsCode.Text.Trim()));

            FormUtils.RefreshDataGridView(exceptionPriceDgv, PriceList);
            count.Text = "共" + PriceList.Count.ToString() + "条数据。";
        }

        private void DealBtn_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRows(exceptionPriceDgv);
            if (obj == null) return;
            foreach (DataGridViewRow dgvr in (DataGridViewSelectedRowCollection)obj)
            {
                string[] result = dao.ExceptionHandle(((ExceptionPrice)dgvr.Cells[0].Value).Outlierid);
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);

            QueryBtn_Click(sender, e);
        }

        private void ExceptionPriceDgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            FormUtils.CellFormatting(sender, e);
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            StopController(exceptionPriceDgv, "99");
            QueryBtn_Click(sender, e);
        }

        private void StopController(DataGridView dgv, string stopFlag)
        {
            object obj = FormUtils.SelectRows(dgv);
            if (obj == null) return;
            foreach (DataGridViewRow dgvr in (DataGridViewSelectedRowCollection)obj)
            {
                ((ExceptionPrice)dgvr.Cells[0].Value).Stopflag = stopFlag;
                string value = ((ExceptionPrice)dgvr.Cells[0].Value).GetString("");
                string[] result = dao.SavePrice(value, "", "executed");
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
        }

        private void ExceptionPriceDgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                ExceptionPrice price = (ExceptionPrice)(dgv.Rows[i].Cells[0].Value);
                //if ("below".Equals(price.Belowbottomprc))
                //{
                //    dgv.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                //}
                if ("draft".Equals(price.Origin))
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            FormUtils.ExcelExport(exceptionPriceDgv, saveFileDialog1, "异常_渠道价");
        }

        private void ExceptionHandling_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryBtn_Click(sender, e);
            }
        }
    }
}

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
    public partial class CopyGoodsPriceForm : Form
    {
        PMBaseDao dao = new PMBaseDao();
        PMPriceDao priceDao = new PMPriceDao();
        SortableBindingList<PubWaredict> goodsList = new SortableBindingList<PubWaredict>();
        ResultInfo resultMsg = new ResultInfo();
        string status = "all_cover";
        public PubWaredict copyGoods;

        public CopyGoodsPriceForm()
        {
            InitializeComponent();
        }

        private void CopyGoodsPriceForm_Load(object sender, EventArgs e)
        {
            copyGoodsName.Text = copyGoods.Name;
        }

        private void GoodsGroup_Click(object sender, EventArgs e)
        {
            FormUtils.GroupComboBoxSetting(sender, "goods");
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            GoodsGroups cbo = new GoodsGroups();
            if (goodsGroup.SelectedValue != null)
                cbo = (GoodsGroups)(goodsGroup.SelectedValue);
            goodsList = StringUtils.TableToEntity<PubWaredict>(dao.GetGoodsByGroupId(goodsCode.Text.Trim(),goodsName.Text.Trim(), cbo.Id, cbo.Type,""));
            FormUtils.RefreshDataGridView(copyGoodsListDgv, goodsList);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            object obj = FormUtils.SelectRows(copyGoodsListDgv);
            if (obj == null) return;
            DataGridViewSelectedRowCollection row = (DataGridViewSelectedRowCollection)obj;
            foreach (DataGridViewRow dgvr in row)
            {
                string[] result = priceDao.CopyPrice(copyGoods.Goodid, ((PubWaredict)dgvr.Cells[0].Value).Goodid, status, "p_scm_exec_waredict_copy");
                FormUtils.SetResult(result, resultMsg);
            }
            FormUtils.SendMsg(resultMsg);
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MergeCover_Click(object sender, EventArgs e)
        {
            if (mergeCover.Checked)
            {
                allCover.Checked = false;
                status = "merge_cover";
            }
            else
            {
                allCover.Checked = true;
                status = "all_cover";
            }
        }

        private void AllCover_Click(object sender, EventArgs e)
        {
            if (allCover.Checked)
            {
                mergeCover.Checked = false;
                status = "all_cover";
            }
            else
            {
                mergeCover.Checked = true;
                status = "merge_cover";
            }
        }

        private void CopyGoodsPriceForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryBtn_Click(sender, e);
            }
        }
    }
}

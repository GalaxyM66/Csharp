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
    public partial class GoodsForm : DockContent
    {
        PMBaseDao dao = new PMBaseDao();
        SortableBindingList<PubWaredict> goodsList = new SortableBindingList<PubWaredict>();
        public GoodsForm()
        {
            InitializeComponent();
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            if (StringUtils.IsNull(goodsCode.Text) && StringUtils.IsNull(goodsName.Text) && StringUtils.IsNull(spec.Text) && StringUtils.IsNull(producer.Text)&&StringUtils.IsNull(txtArea.Text)&&StringUtils.IsNull(txtForbitarea.Text)&&StringUtils.IsNull(txtLimitcsttype.Text))
            {
                MessageBox.Show("请填写搜索条件!!");
                goodsCode.Focus();
                return;
            }
            goodsList = StringUtils.TableToEntity<PubWaredict>(dao.GetGoodss(goodsCode.Text.Trim(),goodsName.Text.Trim(), spec.Text.Trim(), producer.Text.Trim(), txtArea.Text.Trim(), txtForbitarea.Text.Trim(), txtLimitcsttype.Text.Trim(),((Dictionary)stopFlag_q.SelectedValue).Code));
            FormUtils.RefreshDataGridView(goodsDgv1, goodsList);
            count.Text = "共" + goodsList.Count.ToString() + "条数据。";
        }

        private void GoodsForm_Load(object sender, EventArgs e)
        {
            SortableBindingList<Dictionary> stopFlagList = StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("1"));
            stopFlagList.Insert(0, new Dictionary { Name = "全部", Code = "" });
            FormUtils.SetComboBox(stopFlag_q, stopFlagList, "Name", "TagPtr");
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            FormUtils.ExcelExport(goodsDgv1, saveFileDialog1, "商品");
        }

        private void GoodsForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryBtn_Click(sender, e);
            }
        }
    }
}

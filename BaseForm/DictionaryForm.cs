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
    public partial class DictionaryForm : DockContent
    {
        PMBaseDao dao = new PMBaseDao();
        SortableBindingList<Dictionary> dictionaryList = new SortableBindingList<Dictionary>();
        string option = null;
        public DictionaryForm()
        {
            InitializeComponent();
        }

        private void DictionaryForm_Load(object sender, EventArgs e)
        {
            SortableBindingList<Dictionary> stopFlagList = StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("1"));
            SortableBindingList<Dictionary> stopFlagList_s = StringUtils.TableToEntity<Dictionary>(dao.GetDictionaryByTypeId("1"));
            stopFlagList.Insert(0, new Dictionary { Name = "全部", Code = "" });
            FormUtils.SetComboBox(stopFlag_s, stopFlagList_s, "Name", "TagPtr");
            FormUtils.SetComboBox(stopFlag_q, stopFlagList, "Name", "TagPtr");
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            InitForm();
            RefreshDataGridView1();
        }

        private void RefreshDataGridView1()
        {
            dictionaryList = StringUtils.TableToEntity<Dictionary>(dao.GetDictionary(typeCode_q.Text.Trim(),typeName_q.Text.Trim(),dictCode_q.Text.Trim(),dictName_q.Text.Trim(), ((Dictionary)stopFlag_s.SelectedValue).Code));
            FormUtils.RefreshDataGridView(dictionaryDgv1, dictionaryList);
            dictionaryDgv1.Focus();
        }

        private void NewBtn_Click(object sender, EventArgs e)
        {
            InitForm();
        }

        private void InitForm()
        {
            option = "add";
            typeCode_s.Text = "";
            typeName_s.Text = "";
            typeId_s.Text = "";
            dictName_s.Text = "";
            dictCode_s.Text = "";
            dictId_s.Text = "";
            mark_s.Text = "";
            stopFlag_s.SelectedIndex = 0;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string[] result = { "", "failed" };
            if ("add".Equals(option))
            {
                if (StringUtils.IsNull(typeName_s.Text) || StringUtils.IsNull(typeCode_s.Text) || StringUtils.IsNull(dictCode_s.Text) || StringUtils.IsNull(dictName_s.Text)) return;
                result = dao.NewDictionary(typeCode_s.Text.Trim(),typeName_s.Text.Trim(), dictCode_s.Text.Trim(), dictName_s.Text.Trim(), mark_s.Text.Trim(), ((Dictionary)stopFlag_s.SelectedValue).Code);
            }
            else if ("edit".Equals(option))
            {
                if (StringUtils.IsNull(typeId_s.Text) || StringUtils.IsNull(dictId_s.Text)) return;
                result = dao.EditDictionary(dictId_s.Text.Trim(), typeId_s.Text.Trim(),typeCode_s.Text.Trim(), typeName_s.Text.Trim(),dictCode_s.Text.Trim(), dictName_s.Text.Trim(), mark_s.Text.Trim(), ((Dictionary)stopFlag_s.SelectedValue).Code);
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

        private void DictionaryDgv1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection row = dictionaryDgv1.SelectedRows;
            if (row.Count != 1) return;
            option = "edit";
            Dictionary dict = (Dictionary)(row[0].Cells[0].Value);
            typeCode_s.Text = dict.Typecode;
            typeName_s.Text = dict.Typename;
            typeId_s.Text = dict.Typeid;
            dictName_s.Text = dict.Name;
            dictCode_s.Text = dict.Code;
            dictId_s.Text = dict.Id;
            mark_s.Text = dict.Mark;
            stopFlag_s.SelectedIndex = stopFlag_s.FindString(dict.Stopflagname);
        }
    }
}

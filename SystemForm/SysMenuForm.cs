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
    public partial class SysMenuForm : DockContent
    {
        TreeViewUtils tvUtils = new TreeViewUtils();
        SortableBindingList<PubMenu> menuList = new SortableBindingList<PubMenu>();
        PMSystemDao dao = new PMSystemDao();
        PMBaseDao baseDao = new PMBaseDao();
        string option = "add";
        public SysMenuForm()
        {
            InitializeComponent();
        }

        private void SysMenuForm_Load(object sender, EventArgs e)
        {
            SortableBindingList<Dictionary> stopFlagList_s = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("1"));
            FormUtils.SetComboBox(stopFlag_s, stopFlagList_s, "Name", "TagPtr");

            SortableBindingList<Dictionary> levelList_s = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("40"));
            FormUtils.SetComboBox(menuLevel_s, levelList_s, "Name", "TagPtr");

            //父级菜单
            RefreshMenuParents();

            InitMenuTree();
        }

        private void RefreshMenuParents()
        {
            SortableBindingList<PubMenu> menuParentsList_s = StringUtils.TableToEntity<PubMenu>(dao.GetParentMenu("1"));
            FormUtils.SetComboBox(menuParents_s, menuParentsList_s, "Menuname", "Menuid");
        }

        private void InitMenuTree()
        {
            treeView1.Nodes.Clear();
            SortableBindingList<TreeNode> tnList = tvUtils.GetTreeNode(dao.GetAllMenu(""));
            foreach (TreeNode tn in tnList)
            {
                treeView1.Nodes.Add(tn);
            }
            treeView1.HideSelection = true;
            treeView1.ShowLines = true;
            treeView1.ExpandAll();
            if (treeView1.Nodes.Count != 0)
            {
                treeView1.SelectedNode = treeView1.Nodes[0];
                treeView1.Nodes[0].EnsureVisible();
            }
        }

        private void MenuLevel_s_SelectedValueChanged(object sender, EventArgs e)
        {
            if (menuLevel_s.ValueMember != "")
            {
                SetParents(((Dictionary)menuLevel_s.SelectedValue).Code);
            }
        }

        private void SetParents(string level)
        {
            menuParents_s.Enabled = "1".Equals(level) ? false : true;
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = treeView1.SelectedNode;
            RefreshDataGridView1(tn.Name);

        }

        private void InitForm()
        {
            option = "add";
            menuId_s.Text = "";
            menuCode_s.Text = "";
            menuName_s.Text = "";
            formName_s.Text = "";
            stopFlag_s.SelectedIndex = 0;
            menuLevel_s.SelectedIndex = 0;
            menuParents_s.SelectedIndex = 0;
        }

        private void RefreshDataGridView1(string name)
        {
            menuList = StringUtils.TableToEntity<PubMenu>(dao.GetMenuById(name));
            sysMenuDgv1.AutoGenerateColumns = false;
            sysMenuDgv1.DataSource = menuList;
        }

        private void SysMenuDgv1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection row = sysMenuDgv1.SelectedRows;
            if (row.Count != 1) return;
            option = "edit";
            PubMenu menu = (PubMenu)(row[0].Cells[0].Value);
            menuId_s.Text = menu.Menuid;
            menuCode_s.Text = menu.Menucode;
            menuName_s.Text = menu.Menuname;
            formName_s.Text = menu.Formname;
            stopFlag_s.SelectedIndex = stopFlag_s.FindString(menu.Stopflagname);
            menuLevel_s.SelectedIndex = menuLevel_s.FindString(menu.Levelname);
            menuParents_s.SelectedIndex = menuParents_s.FindString(menu.Parentsname);
        }

        private void NewBtn_Click(object sender, EventArgs e)
        {
            InitForm();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (StringUtils.IsNull(menuCode_s.Text))
            {
                MessageBox.Show("请填写菜单编码");
                menuCode_s.Focus();
                return;
            }
            if (StringUtils.IsNull(menuName_s.Text))
            {
                MessageBox.Show("请填写菜单名");
                menuName_s.Focus();
                return;
            }
            if (((Dictionary)menuLevel_s.SelectedValue).Code == "2" && StringUtils.IsNull(formName_s.Text))
            {
                MessageBox.Show("请填写表单名");
                formName_s.Focus();
                return;
            }
            string parentsId = "1";
            string[] result = { "", "" };
            if ("2".Equals(((Dictionary)menuLevel_s.SelectedValue).Code))
            {
                parentsId = menuParents_s.SelectedValue.ToString();
            }
            if ("add".Equals(option))
            {
                result = dao.SaveMenu(menuCode_s.Text.Trim(), menuName_s.Text.Trim(), parentsId, formName_s.Text.Trim(), ((Dictionary)stopFlag_s.SelectedValue).Code);
            }
            else if ("edit".Equals(option))
            {
                if (StringUtils.IsNull(menuId_s.Text)) return;
                if (parentsId.Equals(menuId_s.Text))
                {
                    MessageBox.Show("保存失败");
                    return;
                }
                result = dao.SaveMenu(menuId_s.Text.Trim(), menuCode_s.Text.Trim(), menuName_s.Text.Trim(), parentsId, formName_s.Text.Trim(), ((Dictionary)stopFlag_s.SelectedValue).Code);
            }

            if ("1".Equals(result[0]))
            {
                InitMenuTree();
                RefreshMenuParents();
            }
            else
            {
                MessageBox.Show(result[1]);
            }
        }
    }
}

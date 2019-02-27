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
    public partial class RoleForm : DockContent
    {
        PMSystemDao dao = new PMSystemDao();
        PMBaseDao baseDao = new PMBaseDao();
        TreeViewUtils tvUtils = new TreeViewUtils();
        SortableBindingList<PubRole> roleList = new SortableBindingList<PubRole>();
        SortableBindingList<PubMenu> menuList = new SortableBindingList<PubMenu>();
        List<TreeNode> selMenulist = null;
        string option = null;
        public RoleForm()
        {
            InitializeComponent();
        }

        private void RoleForm_Load(object sender, EventArgs e)
        {
            SortableBindingList<Dictionary> stopFlagList = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("1"));
            SortableBindingList<Dictionary> stopFlagList_s = StringUtils.TableToEntity<Dictionary>(baseDao.GetDictionaryByTypeId("1"));
            stopFlagList.Insert(0, new Dictionary { Name = "全部", Code = "" });
            FormUtils.SetComboBox(stopFlag_s, stopFlagList_s, "Name", "TagPtr");
            FormUtils.SetComboBox(stopFlag_q, stopFlagList, "Name", "TagPtr");
        }

        private void QueryBtn_Click(object sender, EventArgs e)
        {
            InitForm();
            RefreshDataGridView1();
        }

        private void InitForm()
        {
            option = "add";
            roleId_s.Text = "";
            roleName_s.Text = "";
            stopFlag_s.SelectedIndex = 0;
            mark_s.Text = "";
            treeView1.Nodes.Clear();
        }

        private void RefreshDataGridView1()
        {
            roleList = StringUtils.TableToEntity<PubRole>(dao.GetRole(roleName_q.Text.Trim(), ((Dictionary)stopFlag_q.SelectedValue).Code));
            roleDgv1.AutoGenerateColumns = false;
            roleDgv1.DataSource = roleList;
            roleDgv1.Focus();
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
                if (StringUtils.IsNull(roleName_s.Text)) return;
                result = dao.SaveRole(roleName_s.Text.Trim(), mark_s.Text.Trim(), ((Dictionary)stopFlag_s.SelectedValue).Code);
            }
            else if ("edit".Equals(option))
            {
                if (StringUtils.IsNull(roleId_s.Text)) return;
                result = dao.SaveRole(roleId_s.Text.Trim(), roleName_s.Text.Trim(), mark_s.Text.Trim(), ((Dictionary)stopFlag_s.SelectedValue).Code);
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

        private void RoleDgv1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection row = roleDgv1.SelectedRows;
            if (row.Count != 1) return;
            option = "edit";
            PubRole role = (PubRole)(row[0].Cells[0].Value);
            roleName_s.Text = role.Rolename;
            roleId_s.Text = role.Roleid;
            mark_s.Text = role.Mark;
            stopFlag_s.SelectedIndex = stopFlag_s.FindString(role.Stopflagname);
            //权限
            RoleMenuQuery_Click(sender, e);
        }

        private void InitMenuTree()
        {
            treeView1.Nodes.Clear();
            SortableBindingList<TreeNode> tnList = tvUtils.GetTreeNode(dao.GetAllMenu("00"));
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

        private void TreeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeViewCheck.CheckControl(e);

            if (e.Node.Checked == true)
            {
                if (selMenulist.FindIndex(m => m == e.Node) != -1) return;
                selMenulist.Add(e.Node);
            }
            else
            {
                selMenulist.Remove(e.Node);
            }
        }
        private void SetRoleMenu(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                foreach (PubMenu ml in menuList)
                {
                    if (node.Name.Equals(ml.Menuid))
                    {
                        node.Checked = true;
                    }
                }
                if (node.Nodes.Count > 0)
                {
                    SetRoleMenu(node.Nodes);
                }
            }
        }
        private void RoleMenuQuery_Click(object sender, EventArgs e)
        {
            if (StringUtils.IsNull(roleId_s.Text)) return;
            InitMenuTree();
            menuList = null;
            selMenulist = new List<TreeNode>();
            menuList = StringUtils.TableToEntity<PubMenu>(dao.GetMenuByRoleId(roleId_s.Text));
            SetRoleMenu(treeView1.Nodes);
        }

        private void RoleMenuSave_Click(object sender, EventArgs e)
        {
            if (StringUtils.IsNull(roleId_s.Text)) return;
            string menuIdList = "";
            foreach (TreeNode selNode in selMenulist)
            {
                menuIdList += selNode.Name + ",";
            }
            string[] result = dao.SaveRoleMenu(roleId_s.Text.Trim(), menuIdList);

            if ("1".Equals(result[0]))
            {
                QueryBtn_Click(sender, e);
            }
            else
            {
                MessageBox.Show(result[1]);
            }
        }
    }
}

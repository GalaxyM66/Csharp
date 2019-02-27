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
    public partial class MenuForm : DockContent
    {
        TreeViewUtils tvUtils = new TreeViewUtils();
        PMSystemDao dao = new PMSystemDao();
        public MenuForm()
        {
            InitializeComponent();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            SortableBindingList<TreeNode> tnList = tvUtils.GetTreeNode(dao.GetMenuByRoleId(SessionDto.Emproleid));
            foreach (TreeNode tn in tnList)
            {
                treeView1.Nodes.Add(tn);
            }
            treeView1.HideSelection = true;
            treeView1.ShowLines = true;
            treeView1.ExpandAll();
            if (treeView1.Nodes.Count != 0)
            {
                treeView1.Nodes[0].EnsureVisible();
            }
        }

        private void TreeView1_DoubleClick(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView1.SelectedNode;
            if (selectedNode.Nodes.Count == 0)
            {
                MainForm mainForm = (MainForm)(this.Tag);
                mainForm.SelectForm((DataRowView)selectedNode.Tag);
            }
        }
    }
}

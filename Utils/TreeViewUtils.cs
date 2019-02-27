using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace PriceManager
{
    class TreeViewUtils
    {
        static string strName = "menuname";     // 名字
        string strId = "menuid";                 // ID
        string strParentId = "parentsid";    // 父级ID
        string strFlag = "stopflag";
        SortableBindingList<TreeNode> treeNodeList = null;
        public SortableBindingList<TreeNode> GetTreeNode(DataTable dt)
        {
            treeNodeList = new SortableBindingList<TreeNode>();
            AddTreeNode(1, null, dt);
            return treeNodeList;
        }

        private void AddTreeNode(int parentID, TreeNode treeNode, DataTable menuTable)
        {
            DataView dvTree = new DataView(menuTable)
            {
                RowFilter = strParentId + " = " + parentID
            };
            foreach (DataRowView Row in dvTree)
            {
                TreeNode Node = new TreeNode();
                if (treeNode == null)
                {
                    if ("99".Equals(Row[strFlag]))
                    {
                        Node.ForeColor = System.Drawing.Color.Red;
                    }
                    Node.Text = Row[strName].ToString();
                    Node.Name = Row[strId].ToString();
                    Node.Tag = Row;
                    treeNodeList.Add(Node);
                    AddTreeNode(Int32.Parse(Row[strId].ToString()), Node, menuTable); //再次递归 
                }
                else
                {
                    if ("99".Equals(Row[strFlag]))
                    {
                        Node.ForeColor = System.Drawing.Color.Red;
                    }
                    Node.Text = Row[strName].ToString();
                    Node.Name = Row[strId].ToString();
                    Node.Tag = Row;
                    treeNode.Nodes.Add(Node);
                    AddTreeNode(Int32.Parse(Row[strId].ToString()), Node, menuTable); //再次递归 
                }
            }
        }
    }

    public static class TreeViewCheck
    {
        /// <summary>
        /// 系列节点 Checked 属性控制
        /// </summary>
        /// <param name="e"></param>
        public static void CheckControl(TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node != null && !Convert.IsDBNull(e.Node))
                {
                    CheckParentNode(e.Node);
                    if (e.Node.Nodes.Count > 0)
                    {
                        CheckAllChildNodes(e.Node, e.Node.Checked);
                    }
                }
            }
        }

        #region 私有方法

        //改变所有子节点的状态
        private static void CheckAllChildNodes(TreeNode pn, bool IsChecked)
        {
            foreach (TreeNode tn in pn.Nodes)
            {
                tn.Checked = IsChecked;

                if (tn.Nodes.Count > 0)
                {
                    CheckAllChildNodes(tn, IsChecked);
                }
            }
        }

        //改变父节点的选中状态，此处为所有子节点不选中时才取消父节点选中，可以根据需要修改
        private static void CheckParentNode(TreeNode curNode)
        {
            bool bChecked = false;

            if (curNode.Parent != null)
            {
                foreach (TreeNode node in curNode.Parent.Nodes)
                {
                    if (node.Checked)
                    {
                        bChecked = true;
                        break;
                    }
                }

                if (bChecked)
                {
                    curNode.Parent.Checked = true;
                    CheckParentNode(curNode.Parent);
                }
                else
                {
                    curNode.Parent.Checked = false;
                    CheckParentNode(curNode.Parent);
                }
            }
        }

        #endregion
    }
}

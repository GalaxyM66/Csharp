namespace PriceManager
{
    partial class MenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("节点1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("节点2");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("节点3");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("节点0", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("节点5");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("节点6");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("节点7");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("节点4", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7});
            this.treeView1 = new treeView.BaseTreeView();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.treeView1.FullRowSelect = true;
            this.treeView1.HotTracking = true;
            this.treeView1.ItemHeight = 23;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点1";
            treeNode1.Text = "节点1";
            treeNode2.Name = "节点2";
            treeNode2.Text = "节点2";
            treeNode3.Name = "节点3";
            treeNode3.Text = "节点3";
            treeNode4.Name = "节点0";
            treeNode4.Text = "节点0";
            treeNode5.Name = "节点5";
            treeNode5.Text = "节点5";
            treeNode6.Name = "节点6";
            treeNode6.Text = "节点6";
            treeNode7.Name = "节点7";
            treeNode7.Text = "节点7";
            treeNode8.Name = "节点4";
            treeNode8.Text = "节点4";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode8});
            this.treeView1.Size = new System.Drawing.Size(153, 406);
            this.treeView1.TabIndex = 0;
            this.treeView1.DoubleClick += new System.EventHandler(this.TreeView1_DoubleClick);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(153, 406);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.ControlBox = false;
            this.Controls.Add(this.treeView1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MenuForm";
            this.ShowIcon = false;
            this.Text = "菜单";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MenuForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private treeView.BaseTreeView treeView1;
    }
}
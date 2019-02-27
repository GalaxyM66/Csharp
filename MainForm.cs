using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Reflection;
 


namespace PriceManager
{
    public partial class MainForm : Form 
    {
        MenuForm menuForm = new MenuForm();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            menuForm.Tag = this;
            menuForm.Show(dockPanel1, DockState.DockLeft);
            empCode.Text = SessionDto.Empcode;
            empname.Text =   SessionDto.Empname;
            role.Text = SessionDto.Emprolename;
            dept.Text = SessionDto.Empdeptname;
            dataBase.Text = MySQLHelper.DBTYPE;
            owner.Text = SessionDto.Ownername;

            timer1.Interval = 60 * 1000;// a minute
            timer1.Start();
        }

      

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            MySQLHelper.CloseDB();
            MySQLHelper.closeAllDB();
            Application.Exit();
        }

        private void ReStartBtn_Click(object sender, EventArgs e)
        {
            MySQLHelper.CloseDB();
            MySQLHelper.closeAllDB();
            Application.Restart();
        }

        public void SelectForm(DataRowView form)
        {
            DockContentCollection ds =dockPanel1.Contents;
            Boolean isShowForm = false;
            string formname = form["formname"].ToString();
            if (formname == "")
            {
                MessageBox.Show("系统未定义菜单，或不相符，请核实！");
                return;
            }
            string dockContentName = formname.Substring(formname.LastIndexOf(".") + 1);
            foreach (DockContent dockContent in ds)
            {
                if (dockContent.Name.Equals(dockContentName))
                {
                    isShowForm = true;
                    if (dockContent.IsDockStateValid(DockState.Hidden))
                    {
                        dockContent.DockHandler.Activate();
                        dockContent.DockHandler.Show();
                        dockContent.Select();
                    }
                    else
                    {
                        dockContent.Select();
                    }
                    break;
                }
            }
            if (!isShowForm)
            {
                DockContent doc = null;
                doc = (DockContent)Assembly.Load("PriceManager").CreateInstance(formname);
                if (doc == null)
                {
                    MessageBox.Show("系统未定义菜单，或不相符，请核实！");
                }
                else
                {
                    doc.Text = form["menuname"].ToString();
                    doc.Show(this.dockPanel1);
                }
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)

        {
            timer1.Stop();
            MySQLHelper.PingDB();
            MySQLHelper.pingAllDB();
            timer1.Start();

        }

        private void RestSetpassword_Click(object sender, EventArgs e)
        {
            ReSetPasswordForm form = new ReSetPasswordForm()
            {
                StartPosition = FormStartPosition.CenterScreen
            };
            form.ShowDialog();
        }
    }
}

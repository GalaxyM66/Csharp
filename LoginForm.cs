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
    public partial class LoginForm : Form
    {
        //版本号---（每次更新需要配置）
        string version = "20190222";

        PMSystemDao dao = new PMSystemDao();
        public LoginForm()
        {
            InitializeComponent();
            userName.Focus();
            //--部门默认
            deptComboBox.DataSource = StringUtils.TableToEntity<PubDept>(dao.GetAllDept(""));
            deptComboBox.DisplayMember = "Deptname";
            deptComboBox.ValueMember = "TagPtr";
            int index = deptComboBox.FindString(Properties.Settings.Default.D_DEPT);
            deptComboBox.SelectedIndex = index;

            //--账号密码默认
            userName.Text = Properties.Settings.Default.D_USER;
            passWord.Text = Properties.Settings.Default.D_PWD;
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            //string a = SessionDto.Empdeptid;
            string user = userName.Text.Trim();
            if ("".Equals(user)) return;
            string passwords = passWord.Text.Trim();
            if ("".Equals(passWord) || StringUtils.IsNull(deptComboBox.Text)) return;
            string[] result =dao.CheckLoginUser(version, user, passwords,((PubDept)deptComboBox.SelectedValue).Saledeptid);
           
            if ("1".Equals(result[0]))
            {
                loginMsg.Text = result[1];
                string ab = SessionDto.Empdeptid;
                int retnumowner=dao.GetPubOwnerConfigureInfo(SessionDto.Empdeptid);
                int retnumdept = dao.GetPubDeptConfigureInfo(SessionDto.Empdeptid);
                if (retnumowner == 0)
                {
                    MessageBox.Show("没有配置货主规则表，请联系系统管理员！","程序提示");
                    return;
                }
                else if (retnumowner > 1)
                {
                    MessageBox.Show("货主规则表出现多行，请联系系统管理员！", "程序提示");
                    return;
                }              
                else if (retnumdept == 0)
                {
                    MessageBox.Show("没有配置货主部门规则表，请联系系统管理员！", "程序提示");
                    return;
                }
                else if (retnumdept > 1)
                {
                    MessageBox.Show("货主部门规则表出现多行，请联系系统管理员！", "程序提示");
                    return;
                }
                else
                {
                    DialogResult = DialogResult.Yes;
                }
            }
            else
            {
                //loginMsg.Text = result[1];
                string ss = result[1];
                MessageBox.Show(result[1]);
            }
        }

        private void UserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                passWord.Focus();
            }

        }

        private void PassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoginBtn_Click(sender, e);
            }
        }
    }
}

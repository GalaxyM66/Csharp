using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
 

namespace PriceManager
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            APDao_B2BTools dao = new APDao_B2BTools();
            if (dao.ConnectionTest() == -1)//打开数据库连接
            {
                MessageBox.Show("连接数据库失败!");
                Environment.Exit(0);
            }
            if (dao.ConnectionTests() == -1)//打开mysql从库连接
            {
                MessageBox.Show("连接数据库失败!");
                Environment.Exit(0);
            }
            if (dao.ConnectionTest_cmszh() == -1)
            {
                MessageBox.Show("连接数据库失败!");
                Environment.Exit(0);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginForm loginForm = new LoginForm();
            DialogResult result = loginForm.ShowDialog();
            if (result == DialogResult.Yes || result == DialogResult.Retry)
            {
                MainForm mainForm = new MainForm();
                Application.Run(mainForm);
            }
            else
            {
                Environment.Exit(0);
            }

        }
    }
}

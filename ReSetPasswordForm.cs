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
    public partial class ReSetPasswordForm : Form
    {
        PMSystemDao dao = new PMSystemDao();
        public ReSetPasswordForm()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string[] result = dao.ChangePassword(SessionDto.Empid, oldPwd.Text.Trim(), newPwd.Text.Trim());
            if("1".Equals(result[0]))
            {
                MySQLHelper.CloseDB();
                Application.Restart();
            }
            else
            {
                MessageBox.Show(result[1]);
            }
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ReSetPasswordForm_Load(object sender, EventArgs e)
        {
        }
    }
}

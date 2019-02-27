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
    public partial class ErrorForm : Form
    {
        public ResultInfo result = new ResultInfo();
        public ErrorForm()
        {
            InitializeComponent();
        }

        private void ErrorForm_Load(object sender, EventArgs e)
        {
        //    erroeText.Text = $"错误数据集:\r\n{result.ResultMsg}";
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CopyBtn_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(erroeText.Text);
        }
    }
}

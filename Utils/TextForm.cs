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
    public partial class TextForm : Form
    {
        public string resultText = null;
        public TextForm()
        {
            InitializeComponent();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EnterBtn_Click(object sender, EventArgs e)
        {
            resultText = FormUtils.StrConstruct(textBox1.Text.Trim());
            Close();
        }
    }
}

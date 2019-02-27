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
    public partial class TextForm1 : Form
    {
        public TextForm1()
        {
            InitializeComponent();
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
    }
}

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
    public partial class Form_confirm : Form
    {
        public string Message = null;
        public Form_confirm()
        {
            InitializeComponent();
        }

        private void Form_confirm_Load(object sender, EventArgs e)
        {
            txtMessage.Text = Message;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}

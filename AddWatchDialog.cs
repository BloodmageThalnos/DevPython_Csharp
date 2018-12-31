using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DevPython
{
    public partial class AddWatchDialog : Form
    {
        private Main _Main;
        public AddWatchDialog(Main pMain)
        {
            InitializeComponent();
            _Main = pMain;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Main.addWatch(textBox1.Text);
            this.Hide();
        }
    }
}

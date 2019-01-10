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
    public partial class ParameterDialog : Form
    {
        private Main _Main;
        public ParameterDialog(Main pMain)
        {
            InitializeComponent();
            _Main = pMain;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Main.runPara = textBox1.Text;
            _Main.debugPara = textBox2.Text;
            this.Hide();
        }
    }
}

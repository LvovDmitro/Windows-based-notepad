using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Лабораторная_работа__1
{
    public partial class GoToForm : Form
    {
        public GoToForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 main = this.Owner as Form1;
            if (main != null)
            {
                int lineNumber = Convert.ToInt32(numericUpDown1.Text);
                if (lineNumber > 0 && lineNumber <= main.textBox1.Lines.Count())
                {
                    main.textBox1.SelectionStart = main.textBox1.GetFirstCharIndexFromLine(Convert.ToInt32(numericUpDown1.Text) - 1);
                    main.textBox1.ScrollToCaret();
                    this.Close();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


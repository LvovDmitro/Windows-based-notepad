using System;
using System.IO;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool tbChange = false;
        string docPath = "";

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = Properties.Settings.Default.formWidth;
            this.Height = Properties.Settings.Default.formHeight;
            textBox1.Font = Properties.Settings.Default.textFont;
            if (Properties.Settings.Default.statusStripVisible == true)
            { statusStrip1.CheckState = CheckState.Checked; }
            else
            { statusStrip1.CheckState = CheckState.Unchecked; }
            if (Properties.Settings.Default.textTransfer == true)
            {.CheckState = CheckState.Checked; }
            else
            { mFormatTransfer.CheckState = CheckState.Unchecked; }
        }
        private void Form1_FormClosing(object sender, EventArgs e)
        {
            Properties.Settings.Default.formWidth = this.Width;
            Properties.Settings.Default.formHeight = this.Height;
            Properties.Settings.Default.textTransfer = textBox1.WordWrap;
            Properties.Settings.Default.textFont = textBox1.Font;
            Properties.Settings.Default.statusStripVisible = textBox1.Visible;
            Properties.Settings.Default.Save();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            tbChange = true;
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

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
            this.Text = Properties.Settings.Default.newDocName + " - " + Properties.Settings.Default.programmName;
        }
        public Form1(string fileName) // Открытие программы документом
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                try
                {
                    string programmName = Properties.Settings.Default.programmName;
                    FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    StreamReader reader = new StreamReader(file, Encoding.Default);
                    textBox1.Text = reader.ReadToEnd();
                    reader.Close();
                    docPath = fileName;
                    tbChange = false;
                    this.Text = Path.GetFileName(fileName) + " — " + programmName;
                    textBox1.Select(0, 0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        bool tbChange = false;
        string docPath = "";
        private CheckState statusStripVisible;
        private CheckState FormatTransfer;

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = Properties.Settings.Default.formWidth;
            this.Height = Properties.Settings.Default.formHeight;
            textBox1.Font = Properties.Settings.Default.textFont;
            if (Properties.Settings.Default.statusStripVisible == true)
            {statusStripVisible = CheckState.Checked; }
            else
            { statusStripVisible = CheckState.Unchecked; }
            if (Properties.Settings.Default.textTransfer == true)
            { FormatTransfer = CheckState.Checked; }
            else
            { FormatTransfer = CheckState.Unchecked; }
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.formWidth = this.Width;
            Properties.Settings.Default.formHeight = this.Height;
            Properties.Settings.Default.textTransfer = textBox1.WordWrap;
            Properties.Settings.Default.textFont = textBox1.Font;
            Properties.Settings.Default.statusStripVisible = textBox1.Visible;
            Properties.Settings.Default.Save();
            if (tbChange == true)
            {
                DialogResult message = MessageBox.Show("Сохранить текущий документ перед выходом?", "Выход из программы", MessageBoxButtons.YesNoCancel);
                if (message == DialogResult.Yes)
                {
                    if (docPath != "")
                    {
                        FileWorks.SaveFile(ref textBox1, ref tbChange, ref docPath);
                        Application.Exit();
                    }
                    else if (docPath == "")
                    {
                        FileWorks.SaveAsFile(ref textBox1, ref tbChange, ref docPath);
                        Application.Exit();
                    }
                }
                else if (message == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            tbChange = true;
            TextWork.StatusAnalize(ref textBox1, ref toolStripStatusLabel1, ref toolStripStatusLabel3, ref toolStripStatusLabel5, ref toolStripStatusLabel7);
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tbChange == true)
            {
                DialogResult message = MessageBox.Show("Сохранить текущий документ перед созданием нового?", "Создание документа", MessageBoxButtons.YesNoCancel);
                if (message == DialogResult.Yes)
                {
                    if (docPath != "")
                    {
                        FileWorks.SaveFile(ref textBox1, ref tbChange, ref docPath);
                        FileWorks.CreateFile(ref textBox1, ref tbChange, ref docPath);
                    }
                    else if (docPath == "")
                    {
                        FileWorks.SaveAsFile(ref textBox1, ref tbChange, ref docPath);
                        FileWorks.CreateFile(ref textBox1, ref tbChange, ref docPath);
                    }
                }
                else if (message == DialogResult.No)
                {
                    FileWorks.CreateFile(ref textBox1, ref tbChange, ref docPath);
                }
            }
            else
            {
                FileWorks.CreateFile(ref textBox1, ref tbChange, ref docPath);
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tbChange == true)
            {
                DialogResult message = MessageBox.Show("Сохранить текущий документ перед открытием нового?", "Открытие документа", MessageBoxButtons.YesNoCancel);
                if (message == DialogResult.Yes)
                {
                    if (docPath != "")
                    {
                        FileWorks.SaveFile(ref textBox1, ref tbChange, ref docPath);
                        FileWorks.OpenFile(ref textBox1, ref tbChange, ref docPath);
                    }
                    else if (docPath == "")
                    {
                        FileWorks.SaveAsFile(ref textBox1, ref tbChange, ref docPath);
                        FileWorks.OpenFile(ref textBox1, ref tbChange, ref docPath);
                    }
                }
                else if (message == DialogResult.No)
                {
                    FileWorks.OpenFile(ref textBox1, ref tbChange, ref docPath);
                }
                else
                {
                    return;
                }
            }
            else
            {
                FileWorks.OpenFile(ref textBox1, ref tbChange, ref docPath);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (docPath != "")
            {
                FileWorks.SaveFile(ref textBox1, ref tbChange, ref docPath);
            }
            else
            {
                FileWorks.SaveAsFile(ref textBox1, ref tbChange, ref docPath);
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileWorks.SaveAsFile(ref textBox1, ref tbChange, ref docPath);
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    printDocument1.Print();
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка параметров печати.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Undo();
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
            {
                textBox1.Cut();
            }
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
            {
                textBox1.Copy();
            }
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectionLength > 0)
            {
                textBox1.SelectedText = "";
            }
        }

        private void перейтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoToForm gotoform = new GoToForm();
            gotoform.Owner = this;
            gotoform.numericUpDown1.Minimum = 0 ;
            gotoform.numericUpDown1.Maximum = textBox1.Lines.Count();
            gotoform.ShowDialog();
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = textBox1.Font;
            DialogResult = fontDialog1.ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                textBox1.Font = fontDialog1.Font;
            }
        }

        private void переносПоСловамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FormatTransfer == CheckState.Checked)
            {
                textBox1.WordWrap = true;
                textBox1.ScrollBars = ScrollBars.Vertical;
                toolStripStatusLabel1.Visible = false;
                toolStripStatusLabel2.Visible = false;
            }
            else
            {
                textBox1.WordWrap = false;
                textBox1.ScrollBars = ScrollBars.Both;
                toolStripStatusLabel1.Visible = true;
                toolStripStatusLabel2.Visible = true;
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog();
        }

        private void строкаСостоянияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusStripVisible == CheckState.Checked)
            {
                Properties.Settings.Default.statusStripVisible = true;
            }
            else
            {
                Properties.Settings.Default.statusStripVisible = false;
            }
        }

        private void масштабToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void найтиИЗаменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchForm findText = new SearchForm();
            findText.Owner = this;
            findText.Show();
        }
    }
}

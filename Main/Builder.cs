using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KNA_Studio
{
    public partial class Builder: Form
    {

        public static string SelectedDB = File.ReadAllText(Environment.CurrentDirectory + @"\Data\bldr\SelectedDB.log");
        public void RefreshListBox()
        {
            try
            {
                listBox1.Items.Clear();
                DirectoryInfo dinfo = new DirectoryInfo(Environment.CurrentDirectory + @"\" + SelectedDB + @"\");
                FileInfo[] smFiles = dinfo.GetFiles("*.txt, *.kna, *.mp4, *.mp3, *.png, *.jpg, *.jpeg, *.exe, *.scr, *.wav, *.m4a, *.log");
                foreach (FileInfo fi in smFiles)
                {
                    listBox1.Items.Add(Path.GetFileName(fi.Name));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load selected LDB. File + '" + Environment.CurrentDirectory + "\\data\\bldr\\SelectedDB.log' studio is inaccesible. Make sure the file exists, if the issue persists, open a issue on GitHub. Information: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        public Builder()
        {
            InitializeComponent();
            MessageBox.Show("KNA LDB Studio 1.1 PTB: This our first public test build! Report bugs on our GitHub page.", "What's New", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshListBox();
        }

        private void Builder_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void executableToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {

        }

        private void tXTValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please check the GitHub wiki for supported file types, as any other will NOT show up in the studio.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateValueDB.Text = textBox1.Text;
            MessageBox.Show("Copied", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            updateValueDB.Text = textBox1.Text;
            MessageBox.Show("Copied", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

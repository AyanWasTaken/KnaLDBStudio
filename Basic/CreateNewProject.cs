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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KNA_Studio
{
    public partial class CreateNewProject: Form
    {
        public CreateNewProject()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                // Configure dialog
                folderDialog.Description = "Select a folder";
           //     folderDialog. = true; // This makes the description show in title bar

                // Show dialog and check result
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    // Set the selected path to the textbox
                    textBox2.Text = folderDialog.SelectedPath;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == null)
            {
                MessageBox.Show("Enter a name.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (textBox2.Text == null)
            {
                MessageBox.Show("Select a directory.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (Directory.Exists(textBox2.Text) == false)
            {
                MessageBox.Show("Select a valid directory.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, @"Data\bldr\SelectedDB.log"), textBox2.Text);
                    File.WriteAllText(Path.Combine(Environment.CurrentDirectory, @"Data\proj\mostrecentproject.txt"), textBox1.Text);
                    Builder builder = new Builder();
                    builder.Show();
                    this.Hide();
                }
                catch
                {
                    MessageBox.Show("Unable to use directory, try launching the studio with administrator privileges.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

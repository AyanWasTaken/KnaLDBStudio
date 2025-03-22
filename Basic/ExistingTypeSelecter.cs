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
using ErrorHelper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KNA_Studio.Basic
{
    public partial class ExistingTypeSelecter: Form
    {
        public ExistingTypeSelecter()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            ErrorHelper.Show Show = new Show();
            Show.Information("Project files are currently unsupported.", "Unsupported");
        }

        private void label13_Click(object sender, EventArgs e)
        {
            try
            {
                //     File.WriteAllText(Path.Combine(Environment.CurrentDirectory, @"Data\bldr\SelectedDB.log"), textBox2.Text);
                //   File.WriteAllText(Path.Combine(Environment.CurrentDirectory, @"Data\proj\mostrecentproject.txt"), textBox1.Text);
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    // Configure dialog
                    folderDialog.Description = "Select a folder";
                    //     folderDialog. = true; // This makes the description show in title bar

                    // Show dialog and check result
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Set the selected path to the textbox
                     File.WriteAllText(Path.Combine(Environment.CurrentDirectory, @"Data\proj\SelectedDB.log"), folderDialog.SelectedPath);
                    }
                }
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

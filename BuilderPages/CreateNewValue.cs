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

namespace KNA_Studio.Plugins.SubDir
{
    public partial class CreateNewValue: Form
    {
        private Builder _mainForm;
        public static string SelectedDB = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Data\bldr\SelectedDB.log")).Trim();
        public CreateNewValue(Builder mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the file name from textBox1
                string fileName = textBox1.Text.Trim();

                // Check if the file name is empty
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show("Please enter a file name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get the file content from textBox2
                string fileContent = textBox2.Text;

                // Construct the full file path (without extension)
                string filePathWithoutExtension = Path.Combine(Environment.CurrentDirectory, SelectedDB, fileName);

                // Construct the full file path with .kna extension
                string filePathWithExtension = filePathWithoutExtension + ".kna";

                // Check if the file already exists
                if (File.Exists(filePathWithExtension))
                {
                    DialogResult result = MessageBox.Show($"A file with the name '{fileName}.kna' already exists. Do you want to overwrite it?", "Overwrite Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                    {
                        return; // Exit if the user chooses not to overwrite
                    }
                }

                // Write the content to the file
                File.WriteAllText(filePathWithExtension, fileContent);

                MessageBox.Show($"File '{fileName}.kna' created successfully. If the value does not show up, please click Ref to refresh the explorer.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Builder builder = new Builder();
                // Optionally, refresh the ListBox to show the new file
                // builder.RefreshListBox();
                _mainForm.RefreshListBox();
            }
            catch (Exception ex)
            {
                // Handle any errors
                MessageBox.Show($"An error occurred while creating the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateNewValue_Load(object sender, EventArgs e)
        {

        }
    }
}

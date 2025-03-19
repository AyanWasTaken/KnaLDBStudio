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

        public static string SelectedDB = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Data\bldr\SelectedDB.log")).Trim();
        private Stack<string> deletedFilesStack = new Stack<string>();

        private void EmptyTempFolder()
        {
            try
            {
                // Define the path to the temp folder
                string tempFolderPath = Path.Combine(Environment.CurrentDirectory, "Temp");

                // Check if the temp folder exists
                if (Directory.Exists(tempFolderPath))
                {
                    // Get all files in the temp folder
                    string[] files = Directory.GetFiles(tempFolderPath);

                    // Delete each file
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }

                    // Optionally, delete the temp folder itself
                    Directory.Delete(tempFolderPath);
                }
            }
            catch (Exception ex)
            {
                // Handle any errors
                MessageBox.Show($"An error occurred while emptying the temp folder: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Empty the temp folder when the form is closing
            EmptyTempFolder();
        }

        public void RefreshListBox()
        {
            try
            {
                // clear the listbox
                listBox1.Items.Clear();

                // get the directory path
                string directoryPath = Path.Combine(Environment.CurrentDirectory, SelectedDB);

                // check if the directory exists
                if (!Directory.Exists(directoryPath))
                {
                    MessageBox.Show($"The directory '{directoryPath}' does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // get all files with the specified extensions
                DirectoryInfo dinfo = new DirectoryInfo(directoryPath);
                string[] allowedExtensions = { ".txt", ".kna", ".mp4", ".mp3", ".png", ".jpg", ".jpeg", ".exe", ".scr", ".wav", ".m4a", ".log" };
                FileInfo[] smFiles = dinfo.GetFiles("*.*")
                                          .Where(f => allowedExtensions.Contains(f.Extension.ToLower()))
                                          .ToArray();

                // add file names to list box
                foreach (FileInfo fi in smFiles)
                {
                    listBox1.Items.Add(Path.GetFileName(fi.Name));
                }
            }
            catch (Exception ex)
            {
                // errror
                MessageBox.Show($"Unable to load files from the selected LDB. Please ensure the directory exists and is accessible.\n\nError Details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Builder()
        {
            InitializeComponent();
            MessageBox.Show("KNA LDB Studio 1.1 PTB: This our first public test build! Report bugs on our GitHub page.", "What's New", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshListBox();
            this.FormClosing += MainForm_FormClosing;
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

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                // check if an item is selected in the listbox
                if (listBox1.SelectedItem == null)
                {
                    MessageBox.Show("Please select a file to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // get the selected file name
                string selectedFileName = listBox1.SelectedItem.ToString();

                // construct the full file path
                string filePath = Path.Combine(Environment.CurrentDirectory, SelectedDB, selectedFileName);

                // confirm deletion with the user
                DialogResult result = MessageBox.Show($"Are you sure you want to delete '{selectedFileName}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Delete the file
                    if (File.Exists(filePath))
                    {
                        // move the file to a temporary location 
                        string tempPath = Path.Combine(Environment.CurrentDirectory, "Temp", selectedFileName);
                        Directory.CreateDirectory(Path.GetDirectoryName(tempPath)); // Ensure the temp directory exists
                        File.Move(filePath, tempPath);

                        deletedFilesStack.Push(tempPath);

                        MessageBox.Show($"'{selectedFileName}' has been deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        RefreshListBox();
                    }
                    else
                    {
                        MessageBox.Show($"File '{selectedFileName}' does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
         
                MessageBox.Show($"An error occurred while deleting the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RefreshListBox();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                // check if there are any files to restore
                if (deletedFilesStack.Count == 0)
                {
                    MessageBox.Show("No files to undo.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // get the last deleted file path from the stack
                string lastDeletedFilePath = deletedFilesStack.Peek(); // peek instead of Pop to preview the file
                string fileName = Path.GetFileName(lastDeletedFilePath);

                // ask the user for confirmation
                DialogResult result = MessageBox.Show($"Are you sure you want to restore '{fileName}'?", "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    // pop the file path from the stack
                    lastDeletedFilePath = deletedFilesStack.Pop();

                    // restore the file to its original location
                    string originalPath = Path.Combine(Environment.CurrentDirectory, SelectedDB, fileName);

                    // ensure the original directory exists
                    Directory.CreateDirectory(Path.GetDirectoryName(originalPath));

                    // move the file back to its original location
                    File.Move(lastDeletedFilePath, originalPath);

                    MessageBox.Show($"'{fileName}' has been restored.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // refresh the listbox
                    RefreshListBox();
                }
            }
            catch (Exception ex)
            {
                // handle any errors
                MessageBox.Show($"An error occurred while restoring the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

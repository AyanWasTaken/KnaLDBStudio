using KNA_Studio.Plugins.SubDir;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        public static bool AutoSaveBool = false;
        private void AutoSave()
        {
            if (AutoSaveBool == false)
            {

            }
            else if (AutoSaveBool == true)
            {

            }
            else
            {
                MessageBox.Show("Your version of KNA LDB Studio is likely corrupt. Autosaving will be off since the void is not able to run.", "Autosave Void", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
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
                string[] allowedExtensions = { ".txt", ".kna", ".mp4", ".mp3", ".png", ".jpg", ".jpeg", ".exe", ".scr", ".wav", ".m4a", ".log", ".obj", ".mtl", ".psd", ".ae" };
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
           
            RefreshListBox();
            this.FormClosing += MainForm_FormClosing;
      //      btnCopyExisting.Click += btnCopyExisting_Click;
        }

        private void Builder_Load(object sender, EventArgs e)
        {
            MessageBox.Show("KNA LDB Studio V0.1 Alpha PTB: This our first public test build! Report bugs on our GitHub page.", "What's New", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Text = "Builder - Project " + File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Data\proj\mostrecentproject.txt"));
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
            updateValueTB.Text = textBox1.Text;
            MessageBox.Show("Copied", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the text from textBox1
                string textToCopy = textBox1.Text;

                // Set the text in updateValueTB
                updateValueTB.Text = textToCopy;

                // Optionally, enable updateValueTB temporarily (if needed)
                updateValueTB.Enabled = true;
                updateValueTB.Focus(); // Set focus to the TextBox
                MessageBox.Show("Copied", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Handle any errors
                MessageBox.Show($"An error occurred while copying the text: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                        MessageBox.Show($"'{selectedFileName}' has been moved to the trash folder (\\Temp).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Project files are not supported at the moment, check back in later versions!", "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Project files are not supported at the moment, check back in later versions! Dont worry, all your local database changes are done on live.", "Unsupported", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Check if an item is selected
                if (listBox1.SelectedItem == null)
                {
                    textBox1.Text = string.Empty; // Clear the TextBox if nothing is selected
                    return;
                }

                // Get the selected file name
                string selectedFileName = listBox1.SelectedItem.ToString();

                // Construct the full file path
                string filePath = Path.Combine(Environment.CurrentDirectory, SelectedDB, selectedFileName);

                // Check if the file exists
                if (File.Exists(filePath))
                {
                    // Read the file contents
                    string fileContents = File.ReadAllText(filePath);

                    // Display the contents in the TextBox
                    textBox1.Text = fileContents;
                }
                else
                {
                    MessageBox.Show($"File '{selectedFileName}' does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle any errors
            //    MessageBox.Show($"An error occurred while reading the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openCurrentDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            { 
        
                string dbDirectoryPath = Path.Combine(Environment.CurrentDirectory, SelectedDB);

               
                if (Directory.Exists(dbDirectoryPath))
                {

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = dbDirectoryPath,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show($"The directory '{dbDirectoryPath}' does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
     
                MessageBox.Show($"An error occurred while opening the directory: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lDBDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                string dbDirectoryPath = Path.Combine(Environment.CurrentDirectory, SelectedDB);


                if (Directory.Exists(dbDirectoryPath))
                {

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = dbDirectoryPath,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show($"The directory '{dbDirectoryPath}' does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"An error occurred while opening the directory: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void studioDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                string dbDirectoryPath = Path.Combine(Environment.CurrentDirectory);


                if (Directory.Exists(dbDirectoryPath))
                {

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = dbDirectoryPath,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show($"The directory '{dbDirectoryPath}' does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"An error occurred while opening the directory: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Define the allowed file extensions
                string[] allowedExtensions = { ".txt", ".kna", ".mp4", ".mp3", ".png", ".jpg", ".jpeg", ".exe", ".scr", ".wav", ".m4a", ".log", ".obj", ".mtl", ".psd", ".ae" };

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "All Files (*.*)|*.*";
                    openFileDialog.Title = "Select files to upload";
                    openFileDialog.Multiselect = true; // Allow multiple file selection

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
              
                        foreach (string selectedFilePath in openFileDialog.FileNames)
                        {
                            string fileName = Path.GetFileName(selectedFilePath);
                            string fileExtension = Path.GetExtension(selectedFilePath).ToLower();
                            string destinationPath = Path.Combine(Environment.CurrentDirectory, SelectedDB, fileName);

     
                            if (File.Exists(destinationPath))
                            {
                         
                                DialogResult overwriteResult = MessageBox.Show($"The file '{fileName}' already exists. Do you want to overwrite it?", "Overwrite Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (overwriteResult == DialogResult.No)
                                {
                                    continue;
                                }
                            }

                            File.Copy(selectedFilePath, destinationPath, overwrite: true);

                            if (!allowedExtensions.Contains(fileExtension))
                            {
                             
                                MessageBox.Show($"The file '{fileName}' has an unsupported extension ({fileExtension}). It has been uploaded but will not be visible in the studio explorer.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                 
                        RefreshListBox();

                        MessageBox.Show("All files uploaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
             
                MessageBox.Show($"An error occurred while uploading the files: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void label12_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if a file is selected in the ListBox
                if (listBox1.SelectedItem == null)
                {
                    MessageBox.Show("Please select a file to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Get the selected file name
                string selectedFileName = listBox1.SelectedItem.ToString();

                // Construct the full file path
                string filePath = Path.Combine(Environment.CurrentDirectory, SelectedDB, selectedFileName);

     //REFRESH THE TEXTBOX           // Check if the file exists
                if (File.Exists(filePath))
                {
                    // Get the new content from the TextBox
                    string newContent = updateValueTB.Text;

                    // Write the new content to the file
                    File.WriteAllText(filePath, newContent);

                    MessageBox.Show($"File '{selectedFileName}' has been updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    try
                    {
                        // Check if an item is selected
                        if (listBox1.SelectedItem == null)
                        {
                            textBox1.Text = string.Empty; // Clear the TextBox if nothing is selected
                            return;
                        }

                        // Get the selected file name
                   //     string selectedFileName = listBox1.SelectedItem.ToString();

                        // Construct the full file path
                    //    string filePath = Path.Combine(Environment.CurrentDirectory, SelectedDB, selectedFileName);

                        // Check if the file exists
                        if (File.Exists(filePath))
                        {
                            // Read the file contents
                            string fileContents = File.ReadAllText(filePath);

                            // Display the contents in the TextBox
                            textBox1.Text = fileContents;
                        }
                        else
                        {
                            MessageBox.Show($"File '{selectedFileName}' does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any errors
                        //    MessageBox.Show($"An error occurred while reading the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } //END OF REFRESHER
                else
                {
                    MessageBox.Show($"File '{selectedFileName}' does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Handle any errors
                MessageBox.Show($"An error occurred while updating the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void folderToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            CreateNewValue createNewValue = new CreateNewValue();
            createNewValue.ShowDialog();
        }

        private void autoSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

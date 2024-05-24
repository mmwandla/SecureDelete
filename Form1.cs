using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SecureDelete
{
    public partial class Form1 : Form
    {
        private FileSelector fileSelector;
        private FileOverwriter fileOverwriter;
        private SecureDeleter secureDeleter;
        private string selectedOverwriteMethod = "DoD 5220.22-M";

        public Form1()
        {
            InitializeComponent();
            fileSelector = new FileSelector();
            fileOverwriter = new FileOverwriter();
            secureDeleter = new SecureDeleter();
            UpdateDeleteButtonToolTip();
        }

        private void UpdateDeleteButtonToolTip()
        {
            deleteButtonToolTip.SetToolTip(deleteButton, $"Current Overwrite Method: {selectedOverwriteMethod}");
        }

        private void selectFilesButton_Click(object sender, EventArgs e)
        {
            var files = fileSelector.SelectFiles();
            filesListBox.Items.AddRange(files);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (filesListBox.Items.Count == 0)
            {
                MessageBox.Show("You need to add files before deleting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("This action is permanent! Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                foreach (string fileName in filesListBox.Items)
                {
                    try
                    {
                        fileOverwriter.OverwriteFile(fileName, selectedOverwriteMethod, UpdateProgressBar, ReportError);
                        secureDeleter.DeleteFile(fileName); // Deletion happens after overwriting
                        MessageBox.Show($"File '{Path.GetFileName(fileName)}' deleted securely using {selectedOverwriteMethod} method.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting file '{Path.GetFileName(fileName)}': {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                filesListBox.Items.Clear();
                progressBar.Value = 0;
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (filesListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("You need to select files to remove.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedItems = filesListBox.SelectedItems.Cast<string>().ToList();
            foreach (var item in selectedItems)
            {
                filesListBox.Items.Remove(item);
            }
        }

        private void filesListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void filesListBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                filesListBox.Items.AddRange(files);
            }
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            using (SettingsForm settingsForm = new SettingsForm(selectedOverwriteMethod))
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    selectedOverwriteMethod = settingsForm.SelectedMethod;
                    UpdateDeleteButtonToolTip();
                }
            }
        }

        private void UpdateProgressBar(int progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(UpdateProgressBar), progress);
            }
            else
            {
                progressBar.Value = progress;
            }
        }

        private void ReportError(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(ReportError), message);
            }
            else
            {
                MessageBox.Show(message, "Verification Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
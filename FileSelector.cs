using System.Windows.Forms;

namespace SecureDelete
{
    public class FileSelector
    {
        public string[] SelectFiles()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileNames;
            }

            return new string[] { };
        }
    }
}


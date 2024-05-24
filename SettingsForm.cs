using System;
using System.Windows.Forms;

namespace SecureDelete
{
    public partial class SettingsForm : Form
    {
        public event EventHandler<string> OverwriteMethodChanged;

        public string SelectedMethod { get; private set; }

        public SettingsForm(string selectedMethod)
        {
            InitializeComponent();
            overwriteMethodComboBox.SelectedItem = selectedMethod;
            UpdateDescription(selectedMethod);
        }

        private void overwriteMethodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedMethod = overwriteMethodComboBox.SelectedItem.ToString();
            UpdateDescription(SelectedMethod);
            OverwriteMethodChanged?.Invoke(this, SelectedMethod); // Raise event
        }

        private void UpdateDescription(string selectedMethod)
        {
            switch (selectedMethod)
            {
                case "DoD 5220.22-M":
                    methodDescriptionTextBox.Text = "The DoD 5220.22-M method performs a 3-pass overwrite: \n" +
                                                      "\n1. Zeros " +
                                                      "\n2. Ones " +
                                                      "\n3. Random pattern. " +
                                                      "This method is widely used for secure deletion.";
                    break;
                case "Guttman Method":
                    methodDescriptionTextBox.Text = "The Guttman method performs a 35-pass overwrite, using different patterns each pass. It's highly secure but may take longer than other methods.";
                    break;
                default:
                    methodDescriptionTextBox.Text = "";
                    break;
            }

            SelectedMethod = selectedMethod;
        }
    }
}

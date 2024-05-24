namespace SecureDelete
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.overwriteMethodComboBox = new System.Windows.Forms.ComboBox();
            this.methodDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // overwriteMethodComboBox
            // 
            this.overwriteMethodComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.overwriteMethodComboBox.FormattingEnabled = true;
            this.overwriteMethodComboBox.Items.AddRange(new object[] {
            "DoD 5220.22-M",
            "Guttman Method"});
            this.overwriteMethodComboBox.Location = new System.Drawing.Point(12, 12);
            this.overwriteMethodComboBox.Name = "overwriteMethodComboBox";
            this.overwriteMethodComboBox.Size = new System.Drawing.Size(300, 23);
            this.overwriteMethodComboBox.TabIndex = 0;
            this.overwriteMethodComboBox.SelectedIndexChanged += new System.EventHandler(this.overwriteMethodComboBox_SelectedIndexChanged);
            // 
            // methodDescriptionTextBox
            // 
            this.methodDescriptionTextBox.Location = new System.Drawing.Point(12, 41);
            this.methodDescriptionTextBox.Multiline = true;
            this.methodDescriptionTextBox.ReadOnly = true;
            this.methodDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.methodDescriptionTextBox.Size = new System.Drawing.Size(300, 100);
            this.methodDescriptionTextBox.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 147);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "Select";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(93, 147);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(324, 182);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.methodDescriptionTextBox);
            this.Controls.Add(this.overwriteMethodComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox overwriteMethodComboBox;
        private System.Windows.Forms.TextBox methodDescriptionTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}

namespace Twidlle.KeyboardTrainer.WinFormsApp.Forms
{
    partial class PreferencesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreferencesForm));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.openLastFileCheckBox = new System.Windows.Forms.CheckBox();
            this.modelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.voiceCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this._languageComboBox = new System.Windows.Forms.ComboBox();
            this.uiLanguagesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.modelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiLanguagesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // openLastFileCheckBox
            // 
            resources.ApplyResources(this.openLastFileCheckBox, "openLastFileCheckBox");
            this.openLastFileCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.modelBindingSource, "OpenLastFile", true));
            this.openLastFileCheckBox.Name = "openLastFileCheckBox";
            this.openLastFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // modelBindingSource
            // 
            this.modelBindingSource.DataSource = typeof(Twidlle.KeyboardTrainer.Forms.Models.PreferencesFormModel);
            // 
            // voiceCheckBox
            // 
            resources.ApplyResources(this.voiceCheckBox, "voiceCheckBox");
            this.voiceCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.modelBindingSource, "VoiceEnable", true));
            this.voiceCheckBox.Name = "voiceCheckBox";
            this.voiceCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // _languageComboBox
            // 
            this._languageComboBox.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.modelBindingSource, "UILanguageCode", true));
            this._languageComboBox.DataSource = this.uiLanguagesBindingSource;
            this._languageComboBox.DisplayMember = "Value";
            this._languageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._languageComboBox.FormattingEnabled = true;
            resources.ApplyResources(this._languageComboBox, "_languageComboBox");
            this._languageComboBox.Name = "_languageComboBox";
            this._languageComboBox.ValueMember = "Key";
            // 
            // uiLanguagesBindingSource
            // 
            this.uiLanguagesBindingSource.DataMember = "UILanguages";
            this.uiLanguagesBindingSource.DataSource = this.modelBindingSource;
            // 
            // PreferencesForm
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.Controls.Add(this._languageComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.voiceCheckBox);
            this.Controls.Add(this.openLastFileCheckBox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreferencesForm";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.modelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiLanguagesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox openLastFileCheckBox;
        private System.Windows.Forms.CheckBox voiceCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _languageComboBox;
        private BindingSource modelBindingSource;
        private BindingSource uiLanguagesBindingSource;
    }
}
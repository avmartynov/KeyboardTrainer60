namespace Twidlle.KeyboardTrainer.WinFormsApp.Forms
{
    partial class WorkoutForm
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
            System.Windows.Forms.Button cancelButton;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorkoutForm));
            System.Windows.Forms.GroupBox groupBox;
            this.textBox = new System.Windows.Forms.TextBox();
            this.workoutTypesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.modelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.okButton = new System.Windows.Forms.Button();
            this.workoutTypeComboBox = new System.Windows.Forms.ComboBox();
            this.localLanguageComboBox = new System.Windows.Forms.ComboBox();
            this.languagesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            cancelButton = new System.Windows.Forms.Button();
            groupBox = new System.Windows.Forms.GroupBox();
            groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.workoutTypesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.modelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.languagesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            resources.ApplyResources(cancelButton, "cancelButton");
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // groupBox
            // 
            resources.ApplyResources(groupBox, "groupBox");
            groupBox.Controls.Add(this.textBox);
            groupBox.Name = "groupBox";
            groupBox.TabStop = false;
            // 
            // textBox
            // 
            resources.ApplyResources(this.textBox, "textBox");
            this.textBox.BackColor = System.Drawing.SystemColors.Control;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.workoutTypesBindingSource, "Description", true));
            this.textBox.Name = "textBox";
            this.textBox.TabStop = false;
            // 
            // workoutTypesBindingSource
            // 
            this.workoutTypesBindingSource.DataMember = "WorkoutTypes";
            this.workoutTypesBindingSource.DataSource = this.modelBindingSource;
            // 
            // modelBindingSource
            // 
            this.modelBindingSource.AllowNew = false;
            this.modelBindingSource.DataSource = typeof(Twidlle.KeyboardTrainer.Forms.Models.WorkoutFormModel);
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // workoutTypeComboBox
            // 
            resources.ApplyResources(this.workoutTypeComboBox, "workoutTypeComboBox");
            this.workoutTypeComboBox.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.modelBindingSource, "SelectedWorkoutTypeCode", true));
            this.workoutTypeComboBox.DataSource = this.workoutTypesBindingSource;
            this.workoutTypeComboBox.DisplayMember = "Name";
            this.workoutTypeComboBox.FormattingEnabled = true;
            this.workoutTypeComboBox.Name = "workoutTypeComboBox";
            this.workoutTypeComboBox.ValueMember = "Code";
            // 
            // localLanguageComboBox
            // 
            resources.ApplyResources(this.localLanguageComboBox, "localLanguageComboBox");
            this.localLanguageComboBox.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.modelBindingSource, "SelectedLanguageCode", true));
            this.localLanguageComboBox.DataSource = this.languagesBindingSource;
            this.localLanguageComboBox.DisplayMember = "Name";
            this.localLanguageComboBox.FormattingEnabled = true;
            this.localLanguageComboBox.Name = "localLanguageComboBox";
            this.localLanguageComboBox.ValueMember = "Code";
            // 
            // languagesBindingSource
            // 
            this.languagesBindingSource.DataMember = "Languages";
            this.languagesBindingSource.DataSource = this.modelBindingSource;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // WorkoutForm
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = cancelButton;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.localLanguageComboBox);
            this.Controls.Add(this.workoutTypeComboBox);
            this.Controls.Add(groupBox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(cancelButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WorkoutForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            groupBox.ResumeLayout(false);
            groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.workoutTypesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.modelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.languagesBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox textBox;
        private BindingSource modelBindingSource;
        private BindingSource workoutTypesBindingSource;
        private ComboBox workoutTypeComboBox;
        private ComboBox localLanguageComboBox;
        private BindingSource languagesBindingSource;
        private Label label1;
        private Label label2;
    }
}
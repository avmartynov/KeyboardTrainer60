using Twidlle.Library.WinForms;

namespace Twidlle.KeyboardTrainer.WinFormsApp.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newWorkoutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWorkoutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveWorkoutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveWorkoutAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.workoutPropsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.workoutToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextExerciseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.resetWorkoutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.homePageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutProgramMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lastResultToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.bestResultToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.exerciseCountToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this._testPanel = new Twidlle.Library.WinForms.GraphicPanel();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.workoutToolsToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newWorkoutMenuItem,
            this.openWorkoutMenuItem,
            this.saveWorkoutMenuItem,
            this.saveWorkoutAsMenuItem,
            this.toolStripMenuItem5,
            this.workoutPropsMenuItem,
            this.toolStripMenuItem4,
            this.exitMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // newWorkoutMenuItem
            // 
            this.newWorkoutMenuItem.Name = "newWorkoutMenuItem";
            resources.ApplyResources(this.newWorkoutMenuItem, "newWorkoutMenuItem");
            // 
            // openWorkoutMenuItem
            // 
            this.openWorkoutMenuItem.Name = "openWorkoutMenuItem";
            resources.ApplyResources(this.openWorkoutMenuItem, "openWorkoutMenuItem");
            // 
            // saveWorkoutMenuItem
            // 
            this.saveWorkoutMenuItem.Name = "saveWorkoutMenuItem";
            resources.ApplyResources(this.saveWorkoutMenuItem, "saveWorkoutMenuItem");
            // 
            // saveWorkoutAsMenuItem
            // 
            this.saveWorkoutAsMenuItem.Name = "saveWorkoutAsMenuItem";
            resources.ApplyResources(this.saveWorkoutAsMenuItem, "saveWorkoutAsMenuItem");
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
            // 
            // workoutPropsMenuItem
            // 
            this.workoutPropsMenuItem.Name = "workoutPropsMenuItem";
            resources.ApplyResources(this.workoutPropsMenuItem, "workoutPropsMenuItem");
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            resources.ApplyResources(this.exitMenuItem, "exitMenuItem");
            // 
            // workoutToolsToolStripMenuItem
            // 
            this.workoutToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nextExerciseMenuItem,
            this.toolStripMenuItem1,
            this.resetWorkoutMenuItem});
            this.workoutToolsToolStripMenuItem.Name = "workoutToolsToolStripMenuItem";
            resources.ApplyResources(this.workoutToolsToolStripMenuItem, "workoutToolsToolStripMenuItem");
            // 
            // nextExerciseMenuItem
            // 
            this.nextExerciseMenuItem.Name = "nextExerciseMenuItem";
            resources.ApplyResources(this.nextExerciseMenuItem, "nextExerciseMenuItem");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // resetWorkoutMenuItem
            // 
            this.resetWorkoutMenuItem.Name = "resetWorkoutMenuItem";
            resources.ApplyResources(this.resetWorkoutMenuItem, "resetWorkoutMenuItem");
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            // 
            // preferencesMenuItem
            // 
            this.preferencesMenuItem.Name = "preferencesMenuItem";
            resources.ApplyResources(this.preferencesMenuItem, "preferencesMenuItem");
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homePageMenuItem,
            this.toolStripMenuItem2,
            this.aboutProgramMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // homePageMenuItem
            // 
            this.homePageMenuItem.Name = "homePageMenuItem";
            resources.ApplyResources(this.homePageMenuItem, "homePageMenuItem");
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // aboutProgramMenuItem
            // 
            this.aboutProgramMenuItem.Name = "aboutProgramMenuItem";
            resources.ApplyResources(this.aboutProgramMenuItem, "aboutProgramMenuItem");
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lastResultToolStripStatusLabel,
            this.toolStripStatusLabel8,
            this.toolStripStatusLabel2,
            this.bestResultToolStripStatusLabel,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel6,
            this.exerciseCountToolStripStatusLabel});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip.ShowItemToolTips = true;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust;
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // lastResultToolStripStatusLabel
            // 
            resources.ApplyResources(this.lastResultToolStripStatusLabel, "lastResultToolStripStatusLabel");
            this.lastResultToolStripStatusLabel.Name = "lastResultToolStripStatusLabel";
            // 
            // toolStripStatusLabel8
            // 
            this.toolStripStatusLabel8.Name = "toolStripStatusLabel8";
            resources.ApplyResources(this.toolStripStatusLabel8, "toolStripStatusLabel8");
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            resources.ApplyResources(this.toolStripStatusLabel2, "toolStripStatusLabel2");
            // 
            // bestResultToolStripStatusLabel
            // 
            this.bestResultToolStripStatusLabel.AccessibleRole = System.Windows.Forms.AccessibleRole.Grip;
            resources.ApplyResources(this.bestResultToolStripStatusLabel, "bestResultToolStripStatusLabel");
            this.bestResultToolStripStatusLabel.Name = "bestResultToolStripStatusLabel";
            // 
            // toolStripStatusLabel4
            // 
            resources.ApplyResources(this.toolStripStatusLabel4, "toolStripStatusLabel4");
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            resources.ApplyResources(this.toolStripStatusLabel6, "toolStripStatusLabel6");
            this.toolStripStatusLabel6.Spring = true;
            // 
            // exerciseCountToolStripStatusLabel
            // 
            resources.ApplyResources(this.exerciseCountToolStripStatusLabel, "exerciseCountToolStripStatusLabel");
            this.exerciseCountToolStripStatusLabel.Name = "exerciseCountToolStripStatusLabel";
            // 
            // _testPanel
            // 
            resources.ApplyResources(this._testPanel, "_testPanel");
            this._testPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this._testPanel.Name = "_testPanel";
            // 
            // saveFileDialog
            // 
            resources.ApplyResources(this.saveFileDialog, "saveFileDialog");
            // 
            // openFileDialog
            // 
            resources.ApplyResources(this.openFileDialog, "openFileDialog");
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this._testPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem workoutToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextExerciseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetWorkoutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutProgramMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem homePageMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lastResultToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel bestResultToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel exerciseCountToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel8;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private GraphicPanel _testPanel;
        private System.Windows.Forms.ToolStripMenuItem newWorkoutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openWorkoutMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem saveWorkoutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveWorkoutAsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem workoutPropsMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private SaveFileDialog saveFileDialog;
        private OpenFileDialog openFileDialog;
    }
}


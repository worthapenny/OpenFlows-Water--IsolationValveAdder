
namespace OFW.IsolationValveAdder.App.Forms
{
    partial class ValveAdderParentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ValveAdderParentForm));
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.valvePlacementOptionsControl = new OFW.IsolationValveAdder.App.UserControls.ValvePlacementOptionsControl();
            this.standardModelAccessToolbar = new OFW.IsolationValveAdder.App.Components.StandardModelAccessToolbar();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonElementSymbology = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRun = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.standardModelAccessToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 28);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.valvePlacementOptionsControl);
            this.splitContainerMain.Size = new System.Drawing.Size(800, 425);
            this.splitContainerMain.SplitterDistance = 232;
            this.splitContainerMain.TabIndex = 2;
            this.splitContainerMain.TabStop = false;
            // 
            // valvePlacementOptionsControl
            // 
            this.valvePlacementOptionsControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.valvePlacementOptionsControl.HaestadContainerControlModel = null;
            this.valvePlacementOptionsControl.Location = new System.Drawing.Point(0, 0);
            this.valvePlacementOptionsControl.Name = "valvePlacementOptionsControl";
            this.valvePlacementOptionsControl.Size = new System.Drawing.Size(232, 360);
            this.valvePlacementOptionsControl.TabIndex = 0;
            // 
            // standardModelAccessToolbar
            // 
            this.standardModelAccessToolbar.Dock = System.Windows.Forms.DockStyle.None;
            this.standardModelAccessToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripButtonElementSymbology,
            this.toolStripButtonRun});
            this.standardModelAccessToolbar.Location = new System.Drawing.Point(0, 0);
            this.standardModelAccessToolbar.Name = "standardModelAccessToolbar";
            this.standardModelAccessToolbar.Size = new System.Drawing.Size(162, 25);
            this.standardModelAccessToolbar.TabIndex = 0;
            this.standardModelAccessToolbar.Text = "standardModelAccessToolbar1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonElementSymbology
            // 
            this.toolStripButtonElementSymbology.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonElementSymbology.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonElementSymbology.Image")));
            this.toolStripButtonElementSymbology.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonElementSymbology.Name = "toolStripButtonElementSymbology";
            this.toolStripButtonElementSymbology.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonElementSymbology.Text = "Element Symbology";
            // 
            // toolStripButtonRun
            // 
            this.toolStripButtonRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRun.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRun.Image")));
            this.toolStripButtonRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRun.Name = "toolStripButtonRun";
            this.toolStripButtonRun.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRun.Text = "Run";
            // 
            // ValveAdderParentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.standardModelAccessToolbar);
            this.Controls.Add(this.splitContainerMain);
            this.Name = "ValveAdderParentForm";
            this.helpProviderHaestadForm.SetShowHelp(this, false);
            this.Text = "Form1";
            this.splitContainerMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.standardModelAccessToolbar.ResumeLayout(false);
            this.standardModelAccessToolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private UserControls.ValvePlacementOptionsControl valvePlacementOptionsControl;
        private Components.StandardModelAccessToolbar standardModelAccessToolbar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonRun;
        private System.Windows.Forms.ToolStripButton toolStripButtonElementSymbology;
    }
}


/**
 * @ Author: Akshaya Niraula
 * @ Create Time: 2021-12-22 4:42
 * @ Copyright: Copyright (c) 2021 Akshaya Niraula. See LICENSE for details
 */

using Haestad.Framework.Windows.Forms.Resources;
using Haestad.Support.Support;
using System.Drawing;
using System.Windows.Forms;

namespace OFW.IsolationValveAdder.App.Components
{
    public class StandardModelAccessToolbar : ToolStrip
    {
        #region Constructor
        public StandardModelAccessToolbar()
        {
            InitializeComponents();
            InitializeVisually();
        }
        #endregion

        #region Private Methods
        private void InitializeComponents()
        {
            this.ToolStripButtonOpen = new ToolStripButton();
            this.ToolStripButtonSelectScenario = new ToolStripButton();
            this.ToolStripSeparator = new ToolStripSeparator();
            this.ToolStripButtonSave = new ToolStripButton();
            this.ToolStripButtonSaveAs = new ToolStripButton();

            this.Items.AddRange(new ToolStripItem[] {
                this.ToolStripButtonOpen,
                this.ToolStripButtonSelectScenario,
                this.ToolStripSeparator,
                this.ToolStripButtonSave,
                this.ToolStripButtonSaveAs});
            this.Dock = DockStyle.None;
            this.Name = "ModelAccessToolbar";
            this.Text = "ModelAccessToolbar";

            // 
            // toolStripButtonOpen
            // 
            this.ToolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonOpen.Name = "toolStripButtonOpen";
            this.ToolStripButtonOpen.Size = new System.Drawing.Size(20, 20);
            this.ToolStripButtonOpen.Text = "Open";
            // 
            // toolStripButtonSelectScenario
            // 
            this.ToolStripButtonSelectScenario.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonSelectScenario.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonSelectScenario.Name = "toolStripButtonSelectScenario";
            this.ToolStripButtonSelectScenario.Size = new System.Drawing.Size(20, 20);
            this.ToolStripButtonSelectScenario.Text = "Select Scenario";
            // 
            // toolStripSeparator1
            // 
            this.ToolStripSeparator.Name = "toolStripSeparator";
            this.ToolStripSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButtonSave
            // 
            this.ToolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonSave.Name = "toolStripButtonSave";
            this.ToolStripButtonSave.Size = new System.Drawing.Size(20, 20);
            this.ToolStripButtonSave.Text = "Save As";
            // 
            // toolStripButtonSaveAs
            // 
            this.ToolStripButtonSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonSaveAs.Name = "toolStripButtonSaveAs";
            this.ToolStripButtonSaveAs.Size = new System.Drawing.Size(20, 20);
            this.ToolStripButtonSaveAs.Text = "Save As";
        }
        private void InitializeVisually()
        {
            this.ToolStripButtonOpen.Image = ((Icon)GraphicResourceManager.Current[StandardGraphicResourceNames.Open])?.ToBitmap();
            this.ToolStripButtonSelectScenario.Image = ((Icon)GraphicResourceManager.Current[StandardGraphicResourceNames.Scenario])?.ToBitmap();
            this.ToolStripButtonSave.Image = ((Icon)GraphicResourceManager.Current[StandardGraphicResourceNames.Save])?.ToBitmap();
            this.ToolStripButtonSaveAs.Image = ((Icon)GraphicResourceManager.Current[StandardGraphicResourceNames.SaveAs])?.ToBitmap();
        }
        #endregion

        #region Fields
        public ToolStripButton ToolStripButtonOpen;
        public ToolStripButton ToolStripButtonSelectScenario;
        public ToolStripSeparator ToolStripSeparator;
        public ToolStripButton ToolStripButtonSave;
        public ToolStripButton ToolStripButtonSaveAs;
        #endregion
    }
}

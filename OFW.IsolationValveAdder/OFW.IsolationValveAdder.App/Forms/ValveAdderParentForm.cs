/**
 * @ Author: Akshaya Niraula
 * @ Create Time: 2021-12-22 4:42
 * @ Copyright: Copyright (c) 2021 Akshaya Niraula. See LICENSE for details
 */

using Haestad.Drawing;
using Haestad.Drawing.Control.Application;
using Haestad.Drawing.Domain;
using Haestad.Drawing.Support;
using Haestad.Drawing.Windows.Forms.Components;
using Haestad.Framework.Application;
using Haestad.Framework.Windows.Forms.Forms;
using Haestad.Framework.Windows.Forms.Resources;
using Haestad.Framework.Windows.Forms.Support;
using Haestad.Support.Support;
using Haestad.WaterProduct.Application;
using OFW.IsolationValveAdder.App.FormModel;
using OFW.IsolationValveAdder.App.Support;
using OpenFlows.Application;
using OpenFlows.Water;
using OpenFlows.Water.Application;
using OpenFlows.Water.Domain;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace OFW.IsolationValveAdder.App.Forms
{
    public partial class ValveAdderParentForm : HaestadParentForm, IParentFormSurrogate
    {
        #region Constructor
        public ValveAdderParentForm(HaestadParentFormModel parentFormModel)
            : base(parentFormModel)
        {
            InitializeComponent();
        }
        #endregion

        #region Public Methods
        public void SetParentWindowHandle(long handle)
        {
            //no-op
        }
        #endregion

        #region Private Methods
        private ValveAdderFormModel NewValveAdderParentFormModel()
        {
            return new ValveAdderFormModel(AppManager.WaterApplicationModel);
        }

        private void DoLazyInitialization(bool lazyInitialize)
        {
            if (lazyInitialize && !IsLazyInitialized)
            {
                AnimationFormManager.Current.StartAnimation(TextManager.Current["InitializingUserInterface"], this);

                try
                {
                    WaterApplicationManager.GetInstance().ParentFormUIModel.Initialize();
                    InitializeDockingManagers();

                    WaterApplicationManager.GetInstance().ParentFormUIModel.DoLazyInitialization();
                }
                finally
                {
                    AnimationFormManager.Current.StopAnimation();
                }

                IsLazyInitialized = true;
            }
        }
        private void InitializeDockingManagers()
        {
            //var elementSymbologyProxy = WaterApplicationManager.GetInstance().ParentFormUIModel.ElementSymbologyProxy;
            //if (elementSymbologyProxy != null)
            //    elementSymbologyProxy.Dock = DockStyle.Fill;
            // add above control
        }
        private void OpenDrawingDocument(IProject aproject)
        {
            AddDocument(aproject);
            GLDrawingControl.Tag = ParentFormModel.CurrentProject;
            GLDrawingControl.ResumeLayout(true);
            (ParentFormModel.CurrentProject as IGraphicalProject).Drawing.AllowRefresh = true;
            GLDrawingControl.ResumeDrawing(true);
            Cursor = Cursors.WaitCursor;
        }
        private void AddDocument(IProject aproject)
        {
            GLDrawingControl = GetNewDocumentControl() as GLDrawingControl;
            GLDrawingControl.Dock = DockStyle.Fill;
            this.splitContainerMain.Panel2.Controls.Add(GLDrawingControl);
        }
        private Control GetNewDocumentControl()
        {
            GLDrawingControl documentControl = new GLDrawingControl(this);
            documentControl.AllowDrop = false;

            documentControl.SuspendLayout();
            (WaterApplicationManager.GetInstance().ParentFormModel.CurrentProject as IGraphicalProject).Drawing.AllowRefresh = true;
            documentControl.SuspendDrawing();
            documentControl.BackColor = Color.White;
            documentControl.Dock = DockStyle.Fill;
            documentControl.Location = new Point(0, 0);
            documentControl.Name = "GLDrawingControl";
            documentControl.Dock = DockStyle.Fill;

            MDIDrawingFormModelBase drawingFormModel = (ParentFormModel as WaterProductParentFormModel).GraphicalEditorFormModelFactory.NewMDIDrawingFormModel(
                WaterApplicationManager.GetInstance().ParentFormModel.CurrentProject as IDomainProject) as MDIDrawingFormModelBase;
            documentControl.LoadUserControl(drawingFormModel.GLDrawingControlModel);
            documentControl.GraphicalProject = ParentFormModel.CurrentProject as IGraphicalProject;
            documentControl.DrawingToolManager = WaterApplicationManager.GetInstance().ParentFormUIModel.LayoutController.DrawingToolManager;

            return documentControl;
        }

        private void SelectTool(IDrawingTool atool)
        {
            if (LayoutController != null
                && LayoutController.DrawingToolManager != null
                && LayoutController.DrawingToolManager.CurrentTool != null)
            {
                if (WaterModel != null && GLDrawingControl != null)
                {
                    GLDrawingControl.ResetToAppropriateCursor();
                }
                Cursor = Cursors.Default;
                Cursor.Current = Cursors.Default;
            }
        }
        private void EnableControls(bool enable)
        {
            standardModelAccessToolbar.ToolStripButtonSelectScenario.Enabled = enable;
            standardModelAccessToolbar.ToolStripButtonSave.Enabled = enable;
            standardModelAccessToolbar.ToolStripButtonSaveAs.Enabled = enable;
            toolStripButtonRun.Enabled = enable;
            toolStripButtonElementSymbology.Enabled = enable;

            valvePlacementOptionsControl.Enabled = enable;
        }
        #endregion

        #region Protected Overrides
        protected override void InitializeEvents()
        {
            this.standardModelAccessToolbar.ToolStripButtonOpen.Click += (o, e) => toolStripButtonOpen_Click();
            this.standardModelAccessToolbar.ToolStripButtonSelectScenario.Click += (o, e) => toolStripButtonSelectScenario_Click();
            this.standardModelAccessToolbar.ToolStripButtonSave.Click += (o, e) => toolStripButtonSave_Click();
            this.standardModelAccessToolbar.ToolStripButtonSaveAs.Click += (o, e) => toolStripButtonSaveAs_Click();

            toolStripButtonRun.Click += (o, e) => toolStripButtonRun_Click();
            toolStripButtonElementSymbology.Click += (o, e) => toolStripButtonElementSymbology_Click();
        }
        protected override void InitializeVisually()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Icon = (Icon)GraphicResourceManager.Current[StandardGraphicResourceNames.Isolate];
            this.Text = "Isolation Valve Added";

            this.toolStripButtonRun.Image = ((Icon)GraphicResourceManager.Current[StandardGraphicResourceNames.Compute]).ToBitmap();
            this.toolStripButtonElementSymbology.Image = ((Icon)GraphicResourceManager.Current[StandardGraphicResourceNames.ElementSymbology]).ToBitmap();

            EnableControls(false);
        }
        public override OpenFileDialog NewOpenFileDialog()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.CheckFileExists = true;
            open.CheckPathExists = true;
            open.DefaultExt = AppManager.ParentFormModel.ApplicationDescription.LeadFileExtension;
            open.Filter = ((WaterProductApplicationDescription)AppManager.ParentFormModel.ApplicationDescription).MultiExtensionOpenFileFilter;
            open.ShowReadOnly = false;
            return open;
        }
        #endregion

        #region Event Handlers        
        private void toolStripButtonOpen_Click()
        {
            CloseCurrentFile();

            var open = NewOpenFileDialog();
            if (open.ShowDialog(this) == DialogResult.OK)
            {
                DoLazyInitialization(true);
                try
                {
                    OpenFile(open.FileName);

                    if (ParentFormModel.CurrentProject != null)
                        AfterProjectOpened();
                }
                catch (Exception ex)
                {
                    var message = $"Oh no. \nFailed to open up the model. Sorry, other things might not work properly. \nFor geeks:\n{ex.Message}\n{ex.StackTrace}";
                    MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void toolStripButtonSelectScenario_Click()
        {
            var scenarioProxy = WaterApplicationManager.GetInstance().ParentFormUIModel.ScenarioManagerProxy;
            scenarioProxy.Dock = DockStyle.Fill;

            new CenterParentToolForm(
                title: "Scenario",
                FindForm(),
                scenarioProxy,
                new Size(350, 350)
                ).ShowDialog();

        }
        private void toolStripButtonSave_Click()
        {
            SaveCurrentFile();
        }
        private void toolStripButtonSaveAs_Click()
        {
            if (PromptSaveAs(ParentFormModel.CurrentProject) == DialogResult.OK)
            {
                OpenFlowsWater.SetMaxProjects(5);
                ValveAdderParentFormModel = NewValveAdderParentFormModel();

                Text = $"Isolation Valve Adder - {WaterModel.ModelInfo.Filename}";

                var newProjectFullPath = ParentFormModel.CurrentProject.FullPath;
                var mbox = MessageBox.Show(this,
                    "Would you like to open the newly saved file in the main application?",
                    "Open in Water[GEMS/CAD/OPS]",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (mbox == DialogResult.Yes)
                {
                    Process.Start(newProjectFullPath);
                }
            }
        }
        private void toolStripButtonRun_Click()
        {
            var piForm = new ProgressIndicatorForm(true, this);
            piForm.Show();

            try
            {
                ValveAdderParentFormModel.AddIsolationValves(piForm);

                if (ValveAdderParentFormModel.IsoValvesAdded.Count > 0)
                {
                    var message = ValveAdderParentFormModel.GetChangeSummary();
                    message += "\n\nNote:\nIf you don't see Isolation Valve in the model. Open up the element symbology (from the tool-bar) and make sure Isolation Valve is checked.";

                    MessageBox.Show(
                        this,
                        message,
                        "Valves Added",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show(this,
                        "No Isolation Valves are created. Please adjust the constrains and try again.",
                        "No Valves Added",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
            }
            finally
            {
                piForm.Done();
                piForm.Close();
            }
        }
        private void toolStripButtonElementSymbology_Click()
        {
            var elementSymbologpyProxy = AppManager.ParentFormUIModel.ElementSymbologyProxy;
            elementSymbologpyProxy.Dock = DockStyle.Fill;
            new CenterParentToolForm(
                "Element Symbology",
                FindForm(),
                elementSymbologpyProxy,
                new Size(350, (int)(this.Height * 0.8))
                ).ShowDialog();
        }



        private void AfterProjectOpened()
        {
            EnableControls(true);
            Text = $"Isolation Valve Adder  - {WaterModel.ModelInfo.Filename}";

            OpenDrawingDocument(ParentFormModel.CurrentProject);
            GLDrawingControl.ZoomExtents();
            SelectTool(LayoutController.DrawingToolManager.ToolNamed(PaletteNames.PaletteSelect));

            ValveAdderParentFormModel = NewValveAdderParentFormModel();
            this.valvePlacementOptionsControl.LoadUserControl(this.ValveAdderParentFormModel.ValvePlacementOptionsControlModel);

            Application.DoEvents();
        }
        #endregion

        #region Private Properties
        private IWaterModel WaterModel => AppManager.CurrentWaterModel;
        private GLDrawingControl GLDrawingControl { get; set; }
        private bool IsLazyInitialized { get; set; }
        private WaterApplicationManager AppManager => (WaterApplicationManager)WaterApplicationManager.GetInstance();
        private LayoutControllerBase LayoutController => AppManager.ParentFormUIModel.LayoutController;

        private ValveAdderFormModel ValveAdderParentFormModel { get; set; }
        #endregion

    }
}

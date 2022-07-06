using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gk_lab_final
{
    public partial class MainForm : Form
    {
        private ElementsFactory elementsFactory;

        public Button _PauseButton;
        public Button _RestartButton;

        public ComboBox Cameras { get; private set; }
        public ComboBox LightSources { get; private set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private void FormShown(object sender, EventArgs e) => this.Init();

        #region Init

        private void Init()
        {
            this._PauseButton = this.PauseButton;
            this._RestartButton = this.RestartButton;

            this.InitMoveBoxes();
            this.Cameras = this.CameraPicker;
            this.LightSources = this.LightSourcePicker;

            Designer.Init(new DesignerParams(this.mainPictureBox));
            this.AlphaChanged(null, null);
            this.ShadingChanged(null, null);

            this.PredefinedScene();

            this.LoadCameraPosition();
            this.LoadLightSourcePosition();
        }

        private void InitMoveBoxes()
        {
            List<NumericUpDown> XBoxes = new List<NumericUpDown>()
            {
                this.CameraX,
                this.TargetX,
                this.LightSourceX
            };

            List<NumericUpDown> YBoxes = new List<NumericUpDown>()
            {
                this.CameraY,
                this.TargetY,
                this.LightSourceY
            };

            List<NumericUpDown> ZBoxes = new List<NumericUpDown>()
            {
                this.CameraZ,
                this.TargetZ,
                this.LightSourceZ
            };

            foreach (var box in XBoxes)
            {
                box.Maximum = Limits.MAX_X;
                box.Minimum = Limits.MIN_X;
            }

            foreach (var box in YBoxes)
            {
                box.Maximum = Limits.MAX_Y;
                box.Minimum = Limits.MIN_Y;
            }

            foreach (var box in ZBoxes)
            {
                box.Maximum = Limits.MAX_Z;
                box.Minimum = Limits.MIN_Z;
            }
        }

        #endregion

        private void AlphaChanged(object sender, EventArgs e)
        {
            Designer.Instance.Alpha = this.trackBar.Value;
            Designer.Instance.Restart();
        }

        private void MainPictureBoxResize(object sender, EventArgs e) => Designer.Instance.Restart();

        private void MainPictureBoxPaint(object sender, PaintEventArgs e) => Designer.Instance.Repaint(e.Graphics);

        private void PredefinedScene()
        {
            this.elementsFactory = new ElementsFactory(this);
            this.elementsFactory.LoadPredefinedScene();
        }

        private bool systemChange = false;

        #region Camera

        #region CameraMovements

        private void MoveCamera(Vector<double> v) => Designer.Instance.Scene.ChosenCamera.MoveSelf(v);

        private void CameraPositionChanged(object sender, EventArgs e)
        {
            if (this.systemChange) return;

            Vector<double> pos = (this.Cameras.SelectedItem as Camera).Position.ToVector();

            if (sender == this.CameraX) pos[0] = (double)this.CameraX.Value - pos[0]; else pos[0] = 0;
            if (sender == this.CameraY) pos[1] = (double)this.CameraY.Value - pos[1]; else pos[1] = 0;
            if (sender == this.CameraZ) pos[2] = (double)this.CameraZ.Value - pos[2]; else pos[2] = 0;

            this.MoveCamera(pos);
            Designer.Instance.Invalidate(true);
        }

        #endregion

        #region TargetMovements

        private void MoveTarget(Vector<double> v) => Designer.Instance.Scene.ChosenCamera.MoveTarget(v);

        private void TargetPositionChanged(object sender, EventArgs e)
        {
            if (this.systemChange) return;

            Vector<double> pos = (this.Cameras.SelectedItem as Camera).Target.Position.ToVector();

            if (sender == this.TargetX) pos[0] = (double)this.TargetX.Value - pos[0]; else pos[0] = 0;
            if (sender == this.TargetY) pos[1] = (double)this.TargetY.Value - pos[1]; else pos[1] = 0;
            if (sender == this.TargetZ) pos[2] = (double)this.TargetZ.Value - pos[2]; else pos[2] = 0;

            this.MoveTarget(pos);
            Designer.Instance.Invalidate(true);
        }

        #endregion

        private void LoadCameraPosition()
        {
            if (this.Cameras.SelectedItem == null) return;

            Camera cam = this.Cameras.SelectedItem as Camera;
            ITarget t = cam.Target;

            this.systemChange = true;

            this.CameraX.Value = (decimal)cam.Position.X;
            this.CameraY.Value = (decimal)cam.Position.Y;
            this.CameraZ.Value = (decimal)cam.Position.Z;

            this.TargetX.Value = (decimal)t.Position.X;
            this.TargetY.Value = (decimal)t.Position.Y;
            this.TargetZ.Value = (decimal)t.Position.Z;

            this.systemChange = false;
        }

        private void CameraChanged(object sender, EventArgs e)
        {
            Designer.Instance.Scene.ChosenCamera = (Camera)this.Cameras.SelectedItem;
            this.LoadCameraPosition();
            this.SetEnableTarget(!Designer.Instance.Scene.ChosenCamera.Target.Locked);
            Designer.Instance.Restart();
        }

        private void SetEnableTarget(bool how)
        {
            this.TargetX.Enabled = how;
            this.TargetY.Enabled = how;
            this.TargetZ.Enabled = how;
        }

        public void RegisterCameras(Camera[] cameras) => this.Cameras.Items.AddRange(cameras);

        public void RegisterCamera(Camera camera) => this.Cameras.Items.Add(camera);

        #endregion

        #region LightSource

        private void LoadLightSourcePosition()
        {
            if (this.LightSources.SelectedItem == null) return;

            this.systemChange = true;

            LightSource src = this.LightSources.SelectedItem as LightSource;

            this.LightSourceX.Value = (decimal)src.Target.Position.X;
            this.LightSourceY.Value = (decimal)src.Target.Position.Y;
            this.LightSourceZ.Value = (decimal)src.Target.Position.Z;

            this.systemChange = false;
        }

        private void SetEnableLightSource(bool how)
        {
            this.LightSourceEnableButton.Enabled = !how;
            this.LightSourceDisableButton.Enabled = how;
            Designer.Instance.Invalidate(true);
        }

        private void LightSourceChanged(object sender, EventArgs e)
        {
            this.SetEnableLightSource((this.LightSources.SelectedItem as LightSource).Enabled);
            this.LoadLightSourcePosition();
        }

        private void EnableLightSource(object sender, EventArgs e)
        {
            (this.LightSources.SelectedItem as LightSource).Enabled = true;
            SetEnableLightSource(true);
        }

        private void DisableLightSource(object sender, EventArgs e)
        {
            (this.LightSources.SelectedItem as LightSource).Enabled = false;
            SetEnableLightSource(false);
        }

        private void MoveLightSource(LightSource src, Vector<double> v) => src.Move(v);

        private void LightSourcePositionChanged(object sender, EventArgs e)
        {
            if (this.systemChange) return;

            Vector<double> pos = (this.LightSources.SelectedItem as LightSource).Position.ToVector();

            pos[0] = (double)this.LightSourceX.Value;
            pos[1] = (double)this.LightSourceY.Value;
            pos[2] = (double)this.LightSourceZ.Value;

            this.MoveLightSource(this.LightSources.SelectedItem as LightSource, pos);
            Designer.Instance.Invalidate(true);
        }

        #endregion

        private void ShadingChanged(object sender, EventArgs e)
        {
            if (this.FlatShading.Checked)
                Designer.Instance.Scene.ShadingModel = Scene.ShadingModels.Flat;

            if (this.GourardShading.Checked)
                Designer.Instance.Scene.ShadingModel = Scene.ShadingModels.Gourard;

            if (this.PhongShading.Checked)
                Designer.Instance.Scene.ShadingModel = Scene.ShadingModels.Phong;

            Designer.Instance.Invalidate(true);
        }
    }
}

using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace gk_lab_final
{
    public partial class ElementsFactory
    {
        private MainForm mainForm;

        public ElementsFactory(MainForm f)
        {
            this.mainForm = f;

            this.Timers = new Dictionary<string, Timer>();
            this.Angles = new Dictionary<string, Counter>();
            
            this.MainDrawables = new List<Drawable>();
            this.LightSources = new List<LightSource>();
            this.Cameras = new List<Camera>();
        }

        private Dictionary<string, Timer> Timers;
        private Dictionary<string, Counter> Angles;
        
        private List<Drawable> MainDrawables;
        private List<LightSource> LightSources;
        private List<Camera> Cameras;

        private Camera ChosenCamera;
        private LightSource ChosenLightSource;

        public void InitiateDesigner()
        {
            // load cameras
            foreach (var cam in this.Cameras)
            {
                Designer.Instance.Scene.Cameras.Add(cam);
                this.mainForm.RegisterCamera(cam);
            }

            // load light sources
            foreach (var ls in this.LightSources)
            {
                Designer.Instance.Scene.LightSources.Add(ls);
                this.mainForm.LightSources.Items.Add(ls);
            }

            this.LoadObjects();

            Designer.Instance.Scene.ChosenCamera = this.ChosenCamera;

            this.mainForm.Cameras.SelectedItem = this.ChosenCamera;
            this.mainForm.LightSources.SelectedItem = this.ChosenLightSource;

            Designer.Instance.Restart();
        }

        private void LoadObjects()
        {
            Designer.Instance.Canvas.ClearObjects();

            // load objects
            foreach (var d in this.MainDrawables)
                Designer.Instance.Canvas.AddToMain(d);
        }

        public void LoadPredefinedScene()
        {
            this.MainDrawables.AddRange(this.MakeScene());
            this.InitiateDesigner();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;

namespace gk_lab_final
{
    public partial class ElementsFactory
    {
        public List<Drawable> _SceneObjects()
        {
            List<Drawable> ret = new List<Drawable>();

            Sphere s = new Sphere(new Point3D(4.0, -1.0, 1.0), 1, Color.Red, 10);

            Polyhedron t1 = this.MakeTetrahedron(
                    new List<Point3D>
                    {
                    new Point3D(-1.0, -10.0, 0.0),
                    new Point3D(0.0, -11.0, 0.0),
                    new Point3D(1.0, -10.0, 0.0),
                    new Point3D(-1.0, -9.0, 0.0),
                    new Point3D(0.0, -10.0, 2.0)
                    },
                    new Point3D(0.0, -10.0, 1.0)
                );
            Polyhedron t2 = this.MakeTetrahedron(
                    new List<Point3D>
                    {
                    new Point3D(1.0, -10.0, 0.0),
                    new Point3D(2.0, -11.0, 0.0),
                    new Point3D(3.0, -10.0, 0.0),
                    new Point3D(2.0, -9.0, 0.0),
                    new Point3D(2.0, -10.0, 2.0)
                    },
                    new Point3D(2.0, -10.0, 1.0)
                );
            Polyhedron t3 = this.MakeTetrahedron(
                    new List<Point3D>
                    {
                    new Point3D(3.0, -10.0, 0.0),
                    new Point3D(4.0, -11.0, 0.0),
                    new Point3D(5.0, -10.0, 0.0),
                    new Point3D(4.0, -9.0, 0.0),
                    new Point3D(4.0, -10.0, 2.0)
                    },
                    new Point3D(4.0, -10.0, 1.0)
                );
            Polyhedron t4 = this.MakeTetrahedron(
                    new List<Point3D>
                    {
                    new Point3D(5.0, -10.0, 0.0),
                    new Point3D(6.0, -11.0, 0.0),
                    new Point3D(7.0, -10.0, 0.0),
                    new Point3D(6.0, -9.0, 0.0),
                    new Point3D(6.0, -10.0, 2.0)
                    },
                    new Point3D(6.0, -10.0, 1.0)
                );
            Polyhedron t5 = this.MakeTetrahedron(
                    new List<Point3D>
                    {
                    new Point3D(7.0, -10.0, 0.0),
                    new Point3D(8.0, -11.0, 0.0),
                    new Point3D(9.0, -10.0, 0.0),
                    new Point3D(8.0, -9.0, 0.0),
                    new Point3D(8.0, -10.0, 2.0)
                    },
                    new Point3D(8.0, -10.0, 1.0)
                );

            ret.Add(s); ret.Add(t1); ret.Add(t2); ret.Add(t3); ret.Add(t4); ret.Add(t5);
            return ret;
        }

        public List<Drawable> MakeScene()
        {

            #region Drawables

            List<Drawable> ret = this._SceneObjects();
            Sphere s = (Sphere)ret[0]; 
            Polyhedron t1 = (Polyhedron)ret[1]; 
            Polyhedron t2 = (Polyhedron)ret[2]; 
            Polyhedron t3 = (Polyhedron)ret[3]; 
            Polyhedron t4 = (Polyhedron)ret[4];
            Polyhedron t5 = (Polyhedron)ret[5];

            #endregion

            #region Cameras

            this.ChooseCamera(this.MakeCamera(new Point3D(4.0, -5.0, 8.0), new DummyTarget(new Point3D(4.0, -5.0, 0.0)), "Ceiling Cam"));
            var followingCam = this.MakeCamera(new Point3D(4.0, 4.0, 3.0), s, "Following Cam");
            this.MakeCamera(new Point3D(-4.0, -8.0, 0.0), new DummyTarget(new Point3D(4.0, -9.0, 0.0)), "Pin Cam");

            #endregion

            this.Angles.Add("ScenewideAngle", new Counter());
            this.Angles.Add("SphereXCounter", new Counter());
            this.Angles.Add("SphereYCounter", new Counter());
            this.Angles.Add("SphereZCounter", new Counter());
            this.Angles.Add("txCounter", new Counter());
            this.Angles.Add("tzCounter", new Counter());

            this.Timers.Add("ScenewideClock", new Timer());

            this.MakeYRotation(s, this.Angles["ScenewideAngle"], (int x) => x * (-1) * Vector<double>.Build.DenseOfArray(new double[] { 4.0, -1.0, 1.0 }));
            this.MakeDrawableMovement(s, (int x) => Vector<double>.Build.DenseOfArray(new double[] { 0.0, (-1) * this.Angles["SphereXCounter"].Value, (-1) * this.Angles["SphereZCounter"].Value }));
            Func<ITransformator> sphereMovement2 = this.MakeDrawableMovement(s, (int x) => Vector<double>.Build.DenseOfArray(new double[] { 0.0, (-1) * this.Angles["SphereXCounter"].Value, (-1) * this.Angles["SphereZCounter"].Value }));
            Designer.Instance.Scene.RemoveTransformation(s, sphereMovement2);

            Func<ITransformator> txMovement1 = MakeDrawableMovement(t3, (int x) => Vector<double>.Build.DenseOfArray(new double[] { 0.0, -this.Angles["txCounter"].Value, 0.0 }));
            Func<ITransformator> txMovement2 = MakeDrawableMovement(t3, (int x) => Vector<double>.Build.DenseOfArray(new double[] { 0.0, 0.0, -this.Angles["tzCounter"].Value }));

            this.Timers["ScenewideClock"].Interval = 100;
            this.Timers["ScenewideClock"].Tick += (object sender, EventArgs e) =>
            {
                this.Angles["ScenewideAngle"].Value += 0.1;

                if (this.Angles["ScenewideAngle"].Value >= 6.2)
                    this.Angles["ScenewideAngle"].Value = 0;

                if (s.Position.Y >= -7.4)
                {
                    this.Angles["SphereXCounter"].Value += 0.1;
                    followingCam.MoveSelf(Vector<double>.Build.DenseOfArray(new double[] { 0.0, -0.1, 0.0 }));
                }
                else if (s.Position.Y >= -8.4)
                {
                    this.Angles["SphereXCounter"].Value += 0.1;
                    this.Angles["txCounter"].Value += 0.1;
                    followingCam.MoveSelf(Vector<double>.Build.DenseOfArray(new double[] { 0.0, -0.1, 0.0 }));
                }
                else if (s.Position.Y >= -10)
                {
                    this.Angles["tzCounter"].Value += 0.1;
                    this.Angles["SphereXCounter"].Value += 0.1;
                    followingCam.MoveSelf(Vector<double>.Build.DenseOfArray(new double[] { 0.0, -0.1, 0.0 }));
                }
                else if (s.Position.Y < -10)
                {
                    this.Angles["tzCounter"].Value += 0.1;
                    this.Angles["SphereZCounter"].Value += 0.1;
                    this.Angles["ScenewideAngle"].Value = 0;
                }

                if (s.Position.Z < -2)
                {
                    this.mainForm._RestartButton.PerformClick();
                    this.mainForm._PauseButton.PerformClick();
                }

                Designer.Instance.Invalidate(false);
            };
            this.Timers["ScenewideClock"].Start();

            #region Buttons

            this.mainForm._PauseButton.Click += (object sender, EventArgs e) =>
            {
                if (this.Timers["ScenewideClock"].Enabled)
                {
                    this.Timers["ScenewideClock"].Stop();
                    this.mainForm._PauseButton.Text = "play";
                }
                else
                {
                    this.Timers["ScenewideClock"].Start();
                    this.mainForm._PauseButton.Text = "pause";
                }
            };

            this.mainForm._RestartButton.Click += (object sender, EventArgs e) =>
            {
                Designer.Instance.Scene.RemoveTransformation(s, sphereMovement2);

                followingCam.Position = new Point3D(4.0, 4.0, 3.0);
                followingCam.Target = s;

                t3.Reset();
                t3.Position = new Point3D(4.0, -10.0, 1.0);
                s.Position.X = 4.0;
                s.Position.Y = -1.0;
                s.Position.Z = 1.0;

                this.Angles["txCounter"].Value = 0.0;
                this.Angles["tzCounter"].Value = 0.0;
                this.Angles["ScenewideAngle"].Value = 0;
                this.Angles["SphereXCounter"].Value = 0;
                this.Angles["SphereYCounter"].Value = 0;
                this.Angles["SphereZCounter"].Value = 0;

                Designer.Instance.Invalidate(true);
            };

            #endregion

            #region Light Sources

            this.MakeSphericalLightSource(1, 1, Color.White, new Point3D(4.0, -5.0, 3.0), "sun");
            var sphereReflector = this.MakeReflector(1, 1, Color.White, new Point3D(4.0, 10.0, 1.0), new DummyTarget(new Point3D(4.0, -1.0, 1.0)), "sphere reflector");
            var backlight = this.MakeReflector(1, 1, Color.White, new Point3D(-4.0, -15.0, 1.0), new DummyTarget(new Point3D(0.0, -10.0, 1.0)), "backlight reflector");
            backlight.Enabled = false;
            sphereReflector.Enabled = false;
            this.ChooseLightSource(sphereReflector);

            #endregion

            return ret;
        }
        
    }
}

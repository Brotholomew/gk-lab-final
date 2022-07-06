using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using MathNet.Numerics.LinearAlgebra;

namespace gk_lab_final
{
    public class Vertex : Drawable
    {
        public IPoint Center { get; set; }

        public Vertex(IPoint center)
        {
            Center = center;

            this.Vertices = new List<Vertex>();
            this.Drawables = new List<Drawable>();

            this.Vertices.Add(this);
        }

        public override void Print(Designer.PrintingMode pm)
        {
            if (this.Visible)
                Designer.Instance.PrintVertex(this.Transitional, this.TransformedCenter, pm);
        }

        #region Transformations

        public IPoint TransformedCenter { get; private set; }

        public bool TransformedView { get; private set; } = false;

        public override void ViewTransform(ITransformator transformator)
        {
            if (this.TransformedView) return;

            Vector<double> v = transformator.Transform(this.TransformedCenter.ToArray());

            this.TransformedCenter = new Point3D((int)v[0], (int)v[1], v[2], v[3]);
            this.TransformedView = true;

            this.InFrame();

            Vector<double> w = Functors.DeNormalize(this.TransformedCenter).ToVector();
            Designer.Instance.Scene.ChosenCamera.InverseCameraMatrix.Multiply(w, w);
        }

        public override void SpaceTransform(ITransformator transformator)
        {
            if (this.Transformed) return;

            Vector<double> v = transformator.Transform(this.Center.ToArray());
            this.TransformedCenter = new Point3D(v[0], v[1], v[2], v[3]);
            this.Transitional = this.TransformedCenter;
            this.Transformed = true;
        }

        private void InFrame()
        {
            this.Visible = true;
            
            if (this.TransformedCenter.X > Designer.Instance.Canvas.Main.Width || this.TransformedCenter.X < 0)
                this.Visible = false;

            if (this.TransformedCenter.Y > Designer.Instance.Canvas.Main.Height || this.TransformedCenter.Y < 0)
                this.Visible = false;
        }

        public IPoint Transitional { get; set; }

        public override void Validate()
        {
            this.Transformed = true;
            this.TransformedView = true;
        }

        public override void Invalidate()
        {
            this.Transformed = false;
            this.TransformedView = false;
        }

        public void Reset()
        {
            this.Invalidate();
            this.TransformedCenter = this.Center;
        }

        #endregion

        public override bool Equals(object obj)
        {
            Vertex v = (Vertex)obj;
            return v.Center == this.Center;
        }

        public override int GetHashCode()
        {
            return (int)this.Center.X ^ (int)this.Center.Y ^ (int)this.Center.Z;
        }
    }
}

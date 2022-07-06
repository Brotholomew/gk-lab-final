using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace gk_lab_final
{
    public class Triangle : Drawable
    {
        #region Shading

        public Color Color { get; set; }

        public int M { get; set; }

        #endregion

        public override List<Vertex> Vertices
        {
            get
            {
                List<Vertex> verts = new List<Vertex>();

                foreach (var d in this.Drawables)
                    foreach (var vd in d.Vertices)
                        if (!verts.Contains(vd)) verts.Add(vd);

                if (this.reverseVertexOrdering)
                    verts.Reverse();

                return verts;
            }
        }

        public Triangle(List<Drawable> Lines, int m = 1)
        {
            this.Init(Lines);
            this.M = m;
        }

        public Triangle(List<Drawable> Lines, Color c, int m = 1)
        {
            this.Init(Lines);
            this.Color = c;
            this.M = m;
        }

        private void Init(List<Drawable> Lines)
        {
            foreach (var line in Lines)
                line.Drawables.Add(this);

            this.Drawables = Lines;
        }

        public override void Fill(Designer.PrintingMode pm, Color c)
        {
            if (this.Visible)
                Designer.Instance.FillTriangle(this, this.Color, pm);
        }

        public override void Print(Designer.PrintingMode pm)
        {
            if (!this.Visible)
                return;

            foreach (var d in this.Drawables)
                d.Print(pm);

            foreach (var v in this.Vertices)
                v.Print(pm);
        }

        public (IPoint A, IPoint B, IPoint C) GetPoints()
        {
            return (this.Vertices[0].TransformedCenter, this.Vertices[1].TransformedCenter, this.Vertices[2].TransformedCenter);
        }

        #region Back-Face Culling

        private Vector<double> planeNormal;

        public Vector<double> CalculatePlaneNormal() => this.planeNormal = Functors.PlaneNormal(this.Vertices[0].TransformedCenter, this.Vertices[1].TransformedCenter, this.Vertices[2].TransformedCenter);

        public override bool Visible { get; protected set; } = false;

        public override void Cull()
        {
            this.planeNormal = Functors.PlaneNormal(this.Vertices[0].Transitional, this.Vertices[1].Transitional, this.Vertices[2].Transitional);

            Vector<double> A = this.Vertices[0].TransformedCenter.ToArray();
            Vector<double> V = Functors.Distance(Designer.Instance.Scene.ChosenCamera.Position.ToArray(), A);

            double dot = Functors.DotProduct(V, this.planeNormal);

            this.Visible = dot >= 0;
        }

        private void Debug(string additionalInfo = "")
        {
            Console.WriteLine("Triangle: " +
                "A = " + Math.Round(this.Vertices[0].Transitional.X, 2) + ", " + Math.Round(this.Vertices[0].Transitional.Y, 2) + ", " + Math.Round(this.Vertices[0].Transitional.Z, 2) + "; " +
                "B = " + Math.Round(this.Vertices[1].Transitional.X, 2) + ", " + Math.Round(this.Vertices[1].Transitional.Y, 2) + ", " + Math.Round(this.Vertices[1].Transitional.Z, 2) + "; " +
                "C = " + Math.Round(this.Vertices[2].Transitional.X, 2) + ", " + Math.Round(this.Vertices[2].Transitional.Y, 2) + ", " + Math.Round(this.Vertices[2].Transitional.Z, 2) + "; "
                + " " + additionalInfo);
        }

        public override void SpaceTransform(ITransformator t)
        {
            if (this.Transformed) return;

            foreach (var v in this.Vertices)
                v.SpaceTransform(t.SpaceTransformations());

            this.Transformed = true;
        }

        public override void ViewTransform(ITransformator t)
        {
            if (!this.Visible) return;
            if (ViewTransformed) return;

            foreach (var v in this.Vertices)
                v.ViewTransform(t.ViewTransformations());

            this.InFrame();

            this.Shader.Init();
            this.ViewTransformed = true;
        }

        private void InFrame()
        {
            this.Visible = false;

            foreach (var v in this.Vertices)
                if (v.Visible) this.Visible = true;
        }

        public override void Invalidate()
        {
            base.Invalidate();
            this.ViewTransformed = false;
        }

        #endregion

        #region Vertex Ordering

        private IPoint CenterPoint()
        {
            return new Point3D(
                (this.Vertices[0].Center.X + this.Vertices[1].Center.X + this.Vertices[2].Center.X) / 3,
                (this.Vertices[0].Center.Y + this.Vertices[1].Center.Y + this.Vertices[2].Center.Y) / 3,
                (this.Vertices[0].Center.Z + this.Vertices[1].Center.Z + this.Vertices[2].Center.Z) / 3
                );
        }

        private bool reverseVertexOrdering = false;
        public void ReverseVertexOrdering() => this.reverseVertexOrdering = !this.reverseVertexOrdering;

        public void FixNormal(IPoint referencePoint, double segmentLength)
        {
            this.planeNormal = Functors.PlaneNormal(this.Vertices[0].Center, this.Vertices[1].Center, this.Vertices[2].Center);

            IPoint c = this.CenterPoint();
            IPoint a = Functors.SegmentFromVector(c, this.planeNormal, segmentLength);

            Vector<double> centerPlane = Functors.PlaneEquation(this.planeNormal, c);

            if (Functors.Sign(Functors.Plane(a, centerPlane)) != Functors.Plane(c, centerPlane))
                this.ReverseVertexOrdering();
        }

        #endregion

        #region Shaders

        public Shader Shader { get; set; }

        #endregion
    }
}

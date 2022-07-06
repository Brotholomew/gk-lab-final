using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;

using System.Text;
using System.Threading.Tasks;

namespace gk_lab_final
{
    public class Polyhedron : Drawable, ITarget
    {
        public IPoint Position { get; set; }
        public bool Locked { get; set; } = true;

        public override List<Vertex> Vertices
        {
            get
            {
                List<Vertex> verts = new List<Vertex>();

                foreach (var d in this.Drawables)
                    foreach (var v in d.Vertices)
                        if (!verts.Contains(v)) verts.Add(v);

                return verts;
            }
        }

        public Polyhedron(List<Triangle> Triangles, Point3D position, double h)
        {
            foreach (var t in Triangles)
            {
                t.P = this;
                t.Shader = new PlanarShader(t);
                t.FixNormal(this.Position, 2 * h);
            }

            this.Drawables = Triangles.ConvertAll((Triangle t) => (Drawable)t);
            this.Position = position;
            this.Triangles = Triangles;
        }

        public override void Fill(Designer.PrintingMode pm, Color c)
        {
            Parallel.ForEach(this.Drawables, (Drawable t) => t.Fill(pm, c));
        }

        public override void SpaceTransform(ITransformator transformator)
        {
            base.SpaceTransform(transformator);

            var tp = transformator.SpaceTransformations().Transform(this.Position.ToVector());
            this.Position.X = tp[0];
            this.Position.Y = tp[1];
            this.Position.Z = tp[2];
        }

        public void Reset()
        {
            foreach (var v in this.Vertices)
                v.Reset();
        }

        public override void Print(Designer.PrintingMode pm)
        {

            foreach (var t in this.Drawables)
                t.Print(pm);
        }

        public IPoint CenterOfBasis()
        {
            if (this.Vertices[0].Transitional == null)
                this.Vertices[0].Transitional = this.Vertices[0].Center;

            if (this.Vertices[1].Transitional == null)
                this.Vertices[1].Transitional = this.Vertices[1].Center;

            if (this.Vertices[2].Transitional == null)
                this.Vertices[2].Transitional = this.Vertices[2].Center;

            if (this.Vertices[3].Transitional == null)
                this.Vertices[3].Transitional = this.Vertices[3].Center;

            Vector<double> l1 = Functors.LineEquation(this.Vertices[0].Transitional, this.Vertices[3].Transitional);
            Vector<double> l2 = Functors.LineEquation(this.Vertices[1].Transitional, this.Vertices[2].Transitional);
            Vector<double> normal = Functors.PlaneNormal(this.Vertices[0].Center, this.Vertices[1].Center, this.Vertices[2].Center);
            Vector<double> plane = Functors.PlaneEquation(normal, this.Vertices[0].Center);
            
            return Functors.LineIntersection(l1, l2, plane);
        }
    }
}

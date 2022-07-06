using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace gk_lab_final
{
    public class Sphere : Drawable, ITarget 
    {
        public IPoint Position { get; set; }
        public bool Locked { get; set; } = true;

        private IPoint initialPosition;

        private Color color;

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

        private double radius;

        public Sphere(IPoint position, double radius, Color c, int triangulationDegree = 500)
        {
            this.color = c;
            this.Position = position;
            this.radius = radius;
            this.Triangulate(triangulationDegree);
            this.Drawables = this.Triangles.ConvertAll((Triangle t) => (Drawable)t);
            this.initialPosition = position.Clone();

            int count = 0;
            foreach (var d in this.Triangles)
            {
                count++;
                if (count >= 20)
                {
                    d.Color = Color.Red;
                    break;
                }
            }
        }

        public override void Fill(Designer.PrintingMode pm, Color c)
        {
            Parallel.ForEach(this.Drawables, (Drawable t) => t.Fill(pm, c));
        }

        public override void Print(Designer.PrintingMode pm)
        {
            foreach (var t in this.Drawables)
                t.Print(pm);
        }

        public override void SpaceTransform(ITransformator transformator)
        {
            if (transformator is CombinedTransformator)
            {
                CombinedTransformator t = (CombinedTransformator)transformator;
                foreach (var tt in t.spaceTransformations)
                {
                    if (tt is Move)
                    {
                        var tp = tt.SpaceTransformations().Transform(this.initialPosition.ToVector());
                        this.Position.X = tp[0];
                        this.Position.Y = tp[1];
                        this.Position.Z = tp[2];
                    }
                }
            }

            base.SpaceTransform(transformator);
        }

        // source: https://www.codeproject.com/Articles/8238/Polygon-Triangulation-in-C
        private void Triangulate(double accuracy)
        {
            int count = (int)accuracy * 2;
            double steps = (double)Math.PI / (double)accuracy;

            Vertex[,] verticesList = new Vertex[(int)accuracy + 1, count + 1];
            Dictionary<(int, int), Vertex> repeatedVertices = new Dictionary<(int, int), Vertex>();

            for (int tita = 0; tita <= accuracy; tita++)
            {
                double vtita = (double)tita * steps;

                for (int nphi = (-1) * (int)accuracy; nphi <= accuracy; nphi++)
                {
                    double vphi = (double)nphi * steps;

                    double x = Math.Sin(vtita) * Math.Cos(vphi);
                    double y = Math.Sin(vtita) * Math.Sin(vphi);
                    double z = Math.Cos(vtita);

                    IPoint p = this.Translate(new Point3D(x, y, z), this.Position, this.radius);
                    Vertex v = new Vertex(p);

                    verticesList[tita, nphi + (int)accuracy] = v;
                }
            }

            for (int n_tita = 1; n_tita <= verticesList.GetLength(0) - 1; n_tita++)
            {
                for (int n_phi = 0; n_phi <= verticesList.GetLength(1) - 2; n_phi++)
                {
                    Vertex v11 = verticesList[n_tita, n_phi], v12 = verticesList[n_tita, n_phi + 1], v13 = verticesList[n_tita - 1, n_phi];
                    Vertex v21 = verticesList[n_tita, n_phi + 1], v22 = verticesList[n_tita - 1, n_phi + 1], v23 = verticesList[n_tita - 1, n_phi];

                    Triangle t1 = new Triangle(new List<Drawable>() { new Line(v11, v12), new Line(v12, v13), new Line(v13, v11) }, this.color);
                    Triangle t2 = new Triangle(new List<Drawable>() { new Line(v21, v22), new Line(v22, v23), new Line(v23, v21) }, this.color);

                    t1.Shader = new SphericalShader(t1, this.radius, this.Position);
                    t2.Shader = new SphericalShader(t2, this.radius, this.Position);

                    if (this.CheckTriangleSanity(t1))
                    {
                        t1.FixNormal(this.Position, this.radius * 2);
                        this.Triangles.Add(t1);
                    }

                    if (this.CheckTriangleSanity(t2))
                    {
                        t2.FixNormal(this.Position, this.radius * 2);
                        this.Triangles.Add(t2);
                    }
                }
            }
        }

        private bool CheckTriangleSanity(Triangle t)
        {
            foreach (var v in t.Vertices)
            {
                int count = 0;
                foreach (var v2 in t.Vertices)
                {
                    if (v.Center.EQ(v2.Center))
                        count++;
                }

                if (count >= 2)
                    return false;
            }
        
            return true;
        }

        private IPoint Translate(IPoint p, IPoint distance, double radius)
        {
            p.X *= radius;
            p.Y *= radius;
            p.Z *= radius;

            p.X += distance.X;
            p.Y += distance.Y;
            p.Z += distance.Z;

            return p;
        }
    }
}

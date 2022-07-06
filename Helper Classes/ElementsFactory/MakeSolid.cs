using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace gk_lab_final
{
    public partial class ElementsFactory
    {
        public Polyhedron MakeTetrahedron(List<Point3D> points, Point3D center)
        {
            var basePlane = Functors.PlaneEquation(Functors.PlaneNormal(points[0], points[1], points[2]), points[0]);
            double h = Math.Abs((points[4].X * basePlane[0] + points[1].X * basePlane[1] + points[2].X * basePlane[2] + basePlane[3])) / Math.Sqrt(Math.Pow(basePlane[0], 2) + Math.Pow(basePlane[1], 2) + Math.Pow(basePlane[2], 2));

            Vertex v1 = new Vertex(points[0]);
            Vertex v2 = new Vertex(points[1]);
            Vertex v3 = new Vertex(points[2]);
            Vertex v4 = new Vertex(points[3]);
            Vertex v5 = new Vertex(points[4]);

            Line l1 = new Line(v1, v2);
            Line l2 = new Line(v2, v3);
            Line l3 = new Line(v3, v4);
            Line l4 = new Line(v1, v4);

            Line l13 = new Line(v1, v3);

            Line l5 = new Line(v1, v5);
            Line l6 = new Line(v2, v5);
            Line l7 = new Line(v3, v5);
            Line l8 = new Line(v4, v5);

            Triangle t1 = new Triangle(new List<Drawable> { l13, l2, l1 }, Color.Red);
            Triangle t2 = new Triangle(new List<Drawable> { l4, l3, l13 }, Color.Red);

            Triangle t3 = new Triangle(new List<Drawable> { l6, l5, l1 }, Color.Blue);
            Triangle t4 = new Triangle(new List<Drawable> { l5, l8, l4 }, Color.Blue);
            Triangle t5 = new Triangle(new List<Drawable> { l7, l6, l2 }, Color.Green);
            Triangle t6 = new Triangle(new List<Drawable> { l8, l7, l3 }, Color.Green);

            return new Polyhedron(new List<Triangle> { t1, t2, t3, t4, t5, t6 }, center, h);
        }

        public Sphere MakeSphere(IPoint position, double radius, Color c, int triangulationDegree = 500) 
            => new Sphere(position, radius, c, triangulationDegree);
    }
}

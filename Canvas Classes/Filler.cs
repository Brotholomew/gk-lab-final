using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace gk_lab_final
{
    public class Filler
    {
        private FillerParams Params;

        public Filler(FillerParams p) { this.Params = p; }

        public void ScanLine(Triangle t, Color c, Designer.PrintingMode pm)
        {
            int yCounter = 1;

            (double ymin, double ymax, List<Vertex> vertices, var yvertices) = SortVertices(t);
            List<node> aet = new List<node>();

            for (double i = ymin + 1; i <= ymax; i++)
            {
                if (yvertices.ContainsKey(i - 1))
                {
                    List<(Vertex vertex, int index)> VertList = yvertices[i - 1];
                    foreach (var v in VertList)
                    {
                        this.UpdateAET(v.vertex, this.GetPrevious(v.index, vertices), ref aet);
                        this.UpdateAET(v.vertex, this.GetNext(v.index, vertices), ref aet);
                    }
                }

                this.HandleAET(ref aet, (int)i, t, c, pm);

                yCounter++;
            }
        }

        private void HandleAET(ref List<node> aet, int y, Triangle t, Color c, Designer.PrintingMode pm)
        {
            // sort aet edges based on their x coordinates
            aet.Sort((node n1, node n2) => n1.X.CompareTo(n2.X));

            // color points between edges from aet
            for (int i = 0; i < aet.Count - 1; i++)
                this.HorizontalLine(y, (int)aet[i].X, (int)aet[i + 1].X, t, c, pm);

            // update the inverse of the slope of each edge in the aet
            for (int i = 0; i < aet.Count; i++)
                aet[i].X += aet[i].SlopeInverse;
        }

        public void HorizontalLine(int y, int x1, int x2, Triangle t, Color c, Designer.PrintingMode pm)
        {
            for (int i = x1; i < x2; i++)
            {
                if (!Designer.Instance.Canvas.InBounds(new Point2D(i, y)))
                    continue;

                Point2D p = new Point2D(i, y);
                var (z, zb, alpha, beta, gamma) = this.InterpolateZ(p, t);
                p.Z = z;
                p.ZBuffer = zb;

                if (z < Designer.Instance.Buffer.ZBuffer[(int)p.X, (int)p.Y])
                {
                    Designer.Instance.Buffer.ZBuffer[(int)p.X, (int)p.Y] = z;
                    Designer.Instance.PutPixel(p, pm, t.Shader.CalculateColor(p, alpha, beta, gamma));
                }
            }
        }

        private (double z, double zb, double alpha, double beta, double gamma) InterpolateZ(IPoint current, Triangle t)
        {
            #region variables

            var (A, B, C) = t.GetPoints();

            double x = current.X;
            double y = current.Y;

            double denominator = (B.Y - C.Y) * (A.X - C.X) + (C.X - B.X) * (A.Y - C.Y);

            double alpha = (double)((B.Y - C.Y) * (x - C.X) + (C.X - B.X) * (y - C.Y)) / (double)denominator;
            double beta = (double)((C.Y - A.Y) * (x - C.X) + (A.X - C.X) * (y - C.Y)) / (double)denominator;
            double gamma = 1 - alpha - beta;

            #endregion

            return (alpha * A.Z + beta * B.Z + gamma * C.Z, alpha * A.ZBuffer + beta * B.ZBuffer + gamma * C.ZBuffer, alpha, beta, gamma) ;
        }

        private void UpdateAET(Vertex v, Vertex vx, ref List<node> aet)
        {
            node n = new node(vx.TransformedCenter, v.TransformedCenter, (int)v.TransformedCenter.X);

            // no horizontal lines
            if (double.IsInfinity(n.SlopeInverse))
                return;

            if (vx.TransformedCenter.Y >= v.TransformedCenter.Y)
                aet.Add(n);
            else
                aet.RemoveAll((node nx) => { return nx.SlopeInverse == n.SlopeInverse && nx.YMax == n.YMax; });
        }

        private (double ymin, double ymax, List<Vertex> vertices, Dictionary<double, List<(Vertex vertex, int index)>> yvertices) SortVertices(Triangle t)
        {
            List<Vertex> vertices = t.Vertices;

            // keys - y coordinates, values - vertices and their indices in the vertices list
            Dictionary<double, List<(Vertex vertex, int index)>> yvertices = new Dictionary<double, List<(Vertex vertex, int index)>>();

            vertices.Sort((Vertex v1, Vertex v2) => v1.TransformedCenter.Y.CompareTo(v2.TransformedCenter.Y));

            for (int i = 0; i < vertices.Count; i++)
            {
                Vertex v = vertices[i];
                if (yvertices.ContainsKey(v.TransformedCenter.Y))
                    yvertices[v.TransformedCenter.Y].Add((v, i));
                else
                    yvertices[v.TransformedCenter.Y] = new List<(Vertex vertex, int index)> { (v, i) };
            }

            return (vertices[0].TransformedCenter.Y, vertices[vertices.Count - 1].TransformedCenter.Y, vertices, yvertices);
        }

        private Vertex GetPrevious(int idx, List<Vertex> vertices)
        {
            int previdx = idx == 0 ? vertices.Count - 1 : idx - 1;
            return vertices[previdx];
        }

        private Vertex GetNext(int idx, List<Vertex> vertices)
        {
            int nextidx = idx == vertices.Count - 1 ? 0 : idx + 1;
            return vertices[nextidx];
        }

        class node
        {
            public int YMax { get; private set; }
            public double X { get; set; }
            public double SlopeInverse { get; private set; }
            public double Slope { get; private set; }

            public node(IPoint p1, IPoint p2, int x)
            {
                this.YMax = p1.Y >= p2.Y ? (int)p1.Y : (int)p2.Y;
                this.X = x;

                (this.Slope, _) = LineSlopeEquation.CalculateLineSlopeEquation(p1, p2);
                this.SlopeInverse = 1.0 / (double) this.Slope;
            }
        }
    }
}

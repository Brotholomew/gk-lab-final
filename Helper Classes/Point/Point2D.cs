using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using System.Text;

namespace gk_lab_final
{
    public class Point2D : IPoint
    {        
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double ZBuffer { get; set; }

        public Point2D(double X, double Y) => this.Init(X, Y);

        public Point2D(int X, int Y) => this.Init(X, Y);

        public Point2D(Vector<double> p) => this.Init(p[0], p[1]);

        private void Init(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
            this.Z = 0;
            this.ZBuffer = 0;
        }

        public System.Drawing.Point SPoint() => new System.Drawing.Point((int)this.X, (int)this.Y);

        public Vector<double> ToArray()
        {
            var v = Vector<double>.Build;
            return v.Dense(new double[] { this.X, this.Y, this.Z, 1 });
        }

        public Vector<double> ToVector()
        {
            var v = Vector<double>.Build;
            return v.Dense(new double[] { this.X, this.Y, this.Z, this.ZBuffer });
        }

        public IPoint Clone()
        {
            return new Point2D(this.ToArray());
        }
    }
}

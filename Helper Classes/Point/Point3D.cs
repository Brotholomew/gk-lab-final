using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using System.Text;

namespace gk_lab_final
{
    public class Point3D : IPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double ZBuffer { get; set; }

        public Point3D(double X, double Y, double Z, double zBuffer = 0) => this.Init(X, Y, Z, zBuffer);

        public Point3D(int X, int Y, int Z, int zBuffer = 0) => this.Init(X, Y, Z, zBuffer);

        public Point3D(Vector<double> p) => this.Init(p[0], p[1], p[2], p[3]);

        private void Init(double X, double Y, double Z, double zBuffer = 0)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.ZBuffer = zBuffer;
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
            return new Point3D(this.X, this.Y, this.Z, this.ZBuffer);
        }
    }
}

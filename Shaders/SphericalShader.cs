using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace gk_lab_final
{
    public class SphericalShader : Shader
    {
        private double r;
        private IPoint center;

        public SphericalShader(Triangle t, double r, IPoint center) : base(t)
        {
            this.r = r;
            this.center = center;
        }

        protected override Vector<double> GetN(IPoint p) => Functors.Versorize(Functors.Distance(Vector<double>.Build.DenseOfArray(new double[] { p.X, p.Y, this.GetZ(p, new Point3D(0.0, 0.0, 0.0)) }), this.center.ToArray()));
        protected override Vector<double> GetN(IPoint p, double z) => Functors.Versorize(Functors.Distance(Vector<double>.Build.DenseOfArray(new double[] { p.X, p.Y, z }), this.center.ToArray()));

        private double lastCalculated = 0.0;

        protected override double GetZ(IPoint p, IPoint interpolated)
        {
            List<double> xlist = new List<double>() { this.t.Vertices[0].Transitional.X, this.t.Vertices[1].Transitional.X, this.t.Vertices[2].Transitional.X };
            List<double> ylist = new List<double>() { this.t.Vertices[0].Transitional.Y, this.t.Vertices[1].Transitional.Y, this.t.Vertices[2].Transitional.Y };

            double xMax = xlist.Max();
            double xMin = xlist.Min();
            double yMax = ylist.Max();
            double yMin = ylist.Min();

            double x = p.X;
            double y = p.Y;

            if (x > xMax) x = xMax;
            if (x < xMin) x = xMin;
            if (y > yMax) y = yMax;
            if (y < yMin) y = yMin;

            double ret = this.center.Z + Math.Sqrt(Math.Pow(this.r, 2) - Math.Pow(x - this.center.X, 2) - Math.Pow(y - this.center.Y, 2));
            if (double.IsNaN(ret))
                ret = this.lastCalculated;
            else
                this.lastCalculated = ret;

            return ret;
        }
    }
}

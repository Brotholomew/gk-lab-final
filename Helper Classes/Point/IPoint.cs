using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using System.Text;

namespace gk_lab_final
{
    public interface IPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double ZBuffer { get; set; }

        public System.Drawing.Point SPoint();

        public Vector<double> ToArray();
        public Vector<double> ToVector();

        public IPoint Clone();

        public bool EQ(IPoint p)
        {
            double epsilon = 0.000001;
            
            return
                Math.Abs(this.X - p.X) < epsilon &&
                Math.Abs(this.Y - p.Y) < epsilon &&
                Math.Abs(this.Z - p.Z) < epsilon;
        }
    }
}

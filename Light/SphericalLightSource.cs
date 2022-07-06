using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace gk_lab_final
{
    public class SphericalLightSource : LightSource
    {
        public SphericalLightSource(double kd, double ks, Color c, IPoint pos, string name) : base(kd, ks, c, pos, name) 
        {
            this.Target = new DummyTarget(pos);
        }
        public override void Move(Vector<double> distance)
        {
            this.Position.X = distance[0];
            this.Position.Y = distance[1];
            this.Position.Z = distance[2];
        }
    }
}

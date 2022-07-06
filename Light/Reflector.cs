using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace gk_lab_final
{
    public class Reflector : LightSource
    {
        public Reflector(double kd, double ks, Color c, IPoint pos, ITarget target, string name) : base(kd, ks, c, pos, name)
        {
            this.Target = new DummyTarget(target.Position);
        }

        protected override Color ColorCompositional(Vector<double> L, ShineParams parameters)
        {
            var (_, _, m, _, shader) = parameters;
            
            Vector<double> V = Functors.Versorize(Functors.Distance(this.Position.ToVector(), this.Target.Position.ToVector()));
            double L_V_cos = Math.Pow(Math.Max(0, Functors.DotProduct(L, V)), m);

            return Color.FromArgb(
                (int)((1) * this.Color.R * L_V_cos),
                (int)((1) * this.Color.G * L_V_cos),
                (int)((1) * this.Color.B * L_V_cos)
            );
        }

        public override void Move(Vector<double> distance)
        {
            this.Target.Position.X = distance[0];
            this.Target.Position.Y = distance[1];
            this.Target.Position.Z = distance[2];
        }
    }
}

using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace gk_lab_final
{
    public abstract class LightSource
    {
        private string name;

        public ITarget Target { get; set; }

        public bool Enabled { get; set; } = true;

        public double kd { get; set; }
        public double ks { get; set; }

        public Color Color { get; set; }

        public IPoint Position { get; protected set; }
        public IPoint TransformedPosition { get; private set; }
        public abstract void Move(Vector<double> distance);

        public LightSource(double kd, double ks, Color c, IPoint pos, string name) 
        { 
            this.kd = kd; 
            this.ks = ks; 
            this.Color = c; 
            this.Position = pos;
            this.name = name;
        }

        public virtual Vector<double> Shine(ShineParams parameters)
        {
            var (p, N, m, c, _) = parameters;

            N = Functors.Versorize(N);

            Vector<double> L = (-1) * Functors.Versorize(Functors.Distance(p.ToArray(), this.Position.ToArray()));
            Vector<double> V = this.V(parameters);

            double NL_dp = Functors.DotProduct(N, L);

            Vector<double> R = Functors.Versorize(2 * NL_dp * Functors.Distance(L, N));

            double NL_cos = Math.Max(NL_dp, 0);
            double VR_cos = Math.Max(Functors.DotProduct(V, R), 0);

            Color CC = this.ColorCompositional(L, parameters);

            double r = CC.R * c.R;
            double g = CC.G * c.G;
            double b = CC.B * c.B;

            return Vector<double>.Build.DenseOfArray(new double[]
            {
                this.kd * r * NL_cos + this.ks * r * Math.Pow(VR_cos, m),
                this.kd * g * NL_cos + this.ks * g * Math.Pow(VR_cos, m),
                this.kd * b * NL_cos + this.ks * b * Math.Pow(VR_cos, m)
            });
        }

        public override string ToString() => this.name;
        protected virtual Vector<double> V(ShineParams parameters) => Vector<double>.Build.DenseOfArray(new double[] { 0.0, 0.0, 1.0 });
        protected virtual Color ColorCompositional(Vector<double> L, ShineParams parameters) => this.Color;

        #region Transform

        public void Transform(ITransformator transformator)
        {
            Vector<double> v = transformator.Transform(this.Position.ToArray());
            this.TransformedPosition = new Point3D(v[0], v[1], v[2], v[3]);
        }

        #endregion
    }
}

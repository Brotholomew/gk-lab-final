using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace gk_lab_final
{
    public abstract class Shader
    {
        protected Triangle t;

        protected IPoint A { get => this.t.Vertices[0].Transitional; }
        private IPoint B { get => this.t.Vertices[1].Transitional; }
        private IPoint C { get => this.t.Vertices[2].Transitional; }
        private int m { get => this.t.M; }
        Color c { get => this.t.Color; }

        public Shader(Triangle t) {  this.t = t; }

        private Color AColor;
        private Color BColor;
        private Color CColor;

        private IPoint Center = new Point3D(0.0, 0.0, 0.0);
        private Color CenterColor;
        protected Vector<double> CenterNormal;

        #region Init

        public void Init()
        {
            switch (Designer.Instance.Scene.ShadingModel)
            {
                case Scene.ShadingModels.Flat:
                    this.InitFlat();
                    break;
                case Scene.ShadingModels.Gourard:
                    this.InitGourard();
                    break;
                default:
                    this.InitPhong();
                    break;
            }
        }

        private void InitFlat()
        {
            this.Center.X = Functors.Mean(this.A.X, this.B.X, this.C.X);
            this.Center.Y = Functors.Mean(this.A.Y, this.B.Y, this.C.Y);
            this.Center.Z = Functors.Mean(this.A.Z, this.B.Z, this.C.Z);

            this.CalculateCenterNormal();
            this.CenterColor = this._CalculateColor(this.Center, this.CenterNormal);
        }

        private void InitGourard()
        {
            this.CalculateCenterNormal();

            this.AColor = this._CalculateColor(this.A, this.CenterNormal);
            this.BColor = this._CalculateColor(this.B, this.CenterNormal);
            this.CColor = this._CalculateColor(this.C, this.CenterNormal);
        }

        protected virtual void InitPhong() { }

        #endregion

        #region _CalculateColor

        public Color CalculateColor(IPoint p, double alpha = double.PositiveInfinity, double beta = double.PositiveInfinity, double gamma = double.PositiveInfinity)
        {

            switch(Designer.Instance.Scene.ShadingModel)
            {
                case Scene.ShadingModels.Flat:
                    return this.CalculateColorFlat(p);
                case Scene.ShadingModels.Gourard:
                    return this.CalculateColorGourard(p, alpha, beta, gamma);
                default:
                    IPoint pp = this.RecalculatePoint(p);
                    return this.CalculateColorPhong(pp, p, alpha, beta, gamma);
            }
        }

        private Color CalculateColorFlat(IPoint p) => this.CenterColor;

        private Color CalculateColorGourard(IPoint p, double alpha, double beta, double gamma) => this.Interpolate(p, alpha, beta, gamma);

        private Color CalculateColorPhong(IPoint p, IPoint old, double alpha, double beta, double gamma)
        {
            IPoint interpolated = new Point3D(p.X, p.Y, this.t.Vertices[0].Transitional.Z * alpha + this.t.Vertices[1].Transitional.Z * beta + this.t.Vertices[2].Transitional.Z * gamma);
            p.Z = this.GetZ(p, interpolated);
            Vector<double> N = this.GetN(p, p.Z);

            return this._CalculateColor(p, N);
        }

        private Color _CalculateColor(IPoint p, Vector<double> N)
        {
            ShineParams parameters = new ShineParams(p, N, this.m, this.c, this);
            Vector<double> c = Vector<double>.Build.DenseOfArray(new double[] { 0.0, 0.0, 0.0 });

            foreach (var source in Designer.Instance.Scene.LightSources)
                if (source.Enabled) c += source.Shine(parameters);

            this.NormalizeColor(ref c);
            return Color.FromArgb((int)c[0], (int)c[1], (int)c[2]);
        }

        private void NormalizeColor(ref Vector<double> c)
        {
            c[0] = Math.Max(0, Math.Min(255, (c[0] / 255.0)));
            c[1] = Math.Max(0, Math.Min(255, (c[1] / 255.0)));
            c[2] = Math.Max(0, Math.Min(255, (c[2] / 255.0)));

            if (double.IsNaN(c[0])) c[0] = 255;
            if (double.IsNaN(c[1])) c[1] = 255;
            if (double.IsNaN(c[2])) c[2] = 255;
        }

        protected void CalculateCenterNormal() => this.CenterNormal = Functors.PlaneNormal(this.A, this.B, this.C);

        private Color Interpolate(IPoint p, double alpha, double beta, double gamma)
        {
            #region variables

            double x = p.X - C.X;
            double y = p.Y - C.Y;

            if (alpha == double.PositiveInfinity || beta == double.PositiveInfinity || gamma == double.PositiveInfinity)
            {
                double denominator = (B.Y - C.Y) * (A.X - C.X) + (C.X - B.X) * (A.Y - C.Y); if (denominator == 0) denominator = 1;
                alpha = (double)((B.Y - C.Y) * x + (C.X - B.X) * y) / (double)denominator;
                beta = (double)((C.Y - A.Y) * x + (A.X - C.X) * y) / (double)denominator;
                gamma = 1 - alpha - beta;

            }

            #endregion

            int cR = (int)Math.Min(255, Math.Max(0, (alpha * this.AColor.R + beta * this.BColor.R + gamma * this.CColor.R)));
            int cG = (int)Math.Min(255, Math.Max(0, (alpha * this.AColor.G + beta * this.BColor.G + gamma * this.CColor.G)));
            int cB = (int)Math.Min(255, Math.Max(0, (alpha * this.AColor.B + beta * this.BColor.B + gamma * this.CColor.B)));

            return Color.FromArgb(
                cR,
                cG,
                cB
                );
        }

        protected abstract double GetZ(IPoint p, IPoint old);

        protected abstract Vector<double> GetN(IPoint p);

        protected virtual Vector<double> GetN(IPoint p, double z) => this.GetN(p);

        #endregion

        protected IPoint RecalculatePoint(IPoint p)
        {
            IPoint px = Functors.DeNormalize(p);

            Vector<double> v = px.ToVector();
            Designer.Instance.Scene.ChosenCamera.InverseCameraMatrix.Multiply(v, v);

            return new Point3D(v[0], v[1], v[2], v[3]);
        }
    }
}

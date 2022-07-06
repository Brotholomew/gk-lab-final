using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Text;

namespace gk_lab_final
{
    public class PlanarShader : Shader
    {
        private double a;
        private double b;
        private double c;
        private double d;

        private void GeneratePlaneEquation()
        {
            this.CalculateCenterNormal();
            this.a = this.CenterNormal[0];
            this.b = this.CenterNormal[1];
            this.c = this.CenterNormal[2];
            this.d = (-1) * (this.a * this.A.X + this.b * this.A.Y + this.c * this.A.Z);
        }

        public PlanarShader(Triangle t) : base(t) { }

        protected override void InitPhong() =>  this.GeneratePlaneEquation();

        protected override Vector<double> GetN(IPoint p) => this.CenterNormal;

        protected override double GetZ(IPoint p, IPoint old) => this.a * p.X + this.b * p.Y + this.c * p.Z + this.d;
    }
}

using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace gk_lab_final
{
    public struct ShineParams
    {
        public IPoint p;
        public Vector<double> N;
        public int m;
        public Color c;
        public Shader shader;

        public ShineParams(IPoint p, Vector<double> N, int m, Color c, Shader shader)
        {
            this.p = p;
            this.N = N;
            this.m = m;
            this.c = c;
            this.shader = shader;
        }

        public void Deconstruct(out IPoint p, out Vector<double> N, out int m, out Color c, out Shader shader)
        {
            p = this.p;
            N = this.N;
            m = this.m;
            c = this.c;
            shader = this.shader;
        }
    }
}

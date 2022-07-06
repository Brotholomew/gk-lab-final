using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Text;

namespace gk_lab_final
{
    public class Normalizer : ITransformator
    {
        public Vector<double> Transform(Vector<double> p)
        {
            double width = Designer.Instance.Params.MainPictureBox.Width;
            double height = Designer.Instance.Params.MainPictureBox.Height;
            
            return Vector<double>.Build.Dense(new double[] { Functors.Transform(p[0] / p[3], width), height - Functors.Transform(p[1] / p[3], height), p[2] / p[3], p[2] });
        }

        public ITransformator SpaceTransformations() => new CombinedTransformator(new List<ITransformator> { this });
        public ITransformator ViewTransformations() => new CombinedTransformator(new List<ITransformator>(), false);

        public override int GetHashCode()
        {
            return 'n' ^ 'o' ^ 'r' ^ 'm' ^ 'a' ^ 'l' ^ 'i' ^ 'z' ^ 'e' ^ 'r' ^ Designer.Instance.Width ^ Designer.Instance.Height ^ Designer.Instance.Alpha;
        }
        public override bool Equals(object obj)
        {
            return this.GetHashCode() == obj.GetHashCode();
        }
    }
}

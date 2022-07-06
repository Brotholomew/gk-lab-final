using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Text;

namespace gk_lab_final
{
    public class EmptyTransformer : ITransformator
    {
        public Vector<double> Transform(Vector<double> p) => p;
        public ITransformator SpaceTransformations() => new CombinedTransformator(new List<ITransformator> { this });
        public ITransformator ViewTransformations() => new CombinedTransformator(new List<ITransformator>(), false);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace gk_lab_final
{
    public interface ITransformator
    {
        Vector<double> Transform(Vector<double> p);
        ITransformator SpaceTransformations();
        ITransformator ViewTransformations();
    }
}

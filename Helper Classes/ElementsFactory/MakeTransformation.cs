using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Text;

namespace gk_lab_final
{
    public partial class ElementsFactory
    {
        public Func<ITransformator> MakeDrawableMovement(Drawable d, Func<int, Vector<double>> distance)
        {
            Func<ITransformator> t = () => new Move(() => distance.Invoke(1));
            Designer.Instance.Scene.AddTransformation(d, t);

            return t;
        }

    }
}

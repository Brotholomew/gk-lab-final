using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Text;

namespace gk_lab_final
{
    public class Move : ITransformator
    {
        private Vector<double> distance;

        public Move(Func<Vector<double>> distance)
        {
            this.distance = distance.Invoke();
        }

        public Vector<double> Transform(Vector<double> p)
        {
            p[0] += this.distance[0];
            p[1] += this.distance[1];
            p[2] += this.distance[2];

            return p;
        }

        public ITransformator SpaceTransformations() => new CombinedTransformator(new List<ITransformator> { this });
        public ITransformator ViewTransformations() => new CombinedTransformator(new List<ITransformator>(), false);
    }
}

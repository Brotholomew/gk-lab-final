using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Text;

namespace gk_lab_final
{
    public class Transformator: ITransformator
    {
        private Matrix<double> matrix;

        public Transformator(Matrix<double> m, Func<int, Vector<double>> distance = null)
        {
            this.matrix = m;
            this.InitDistance(distance);
        }

        private ITransformator before;
        private ITransformator after;

        private void InitDistance(Func<int, Vector<double>> distance)
        {
            if (distance == null)
            {
                this.before = new EmptyTransformer();
                this.after = new EmptyTransformer();

                return;
            }

            this.before = new Move(() => distance.Invoke(1));
            this.after = new Move(() => distance.Invoke(-1));
        }

        public Vector<double> Transform(Vector<double> p)
        {
            this.before.Transform(p);
            this.matrix.Multiply(p, p);
            this.after.Transform(p);
            return p;
        }

        public override int GetHashCode()
        {
            return this.matrix.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.GetHashCode() == obj.GetHashCode();
        }

        public ITransformator SpaceTransformations() => new CombinedTransformator(new List<ITransformator> { this });
        public ITransformator ViewTransformations() => new CombinedTransformator(new List<ITransformator>(), false);
    }
}

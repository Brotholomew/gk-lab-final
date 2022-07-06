using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Text;

namespace gk_lab_final
{
    public partial class ElementsFactory
    {
        public Func<ITransformator> MakeZRotation(Drawable d, Counter c, Func<int, Vector<double>> distance = null)
        {
            Func<ITransformator> t = () =>
            {
                return new Transformator(Matrix<double>.Build.DenseOfArray(new double[,]
                {
                    { Math.Cos(c.Value), -Math.Sin(c.Value), 0.0, 0.0 },
                    { Math.Sin(c.Value), Math.Cos(c.Value), 0.0, 0.0 },
                    { 0.0, 0.0, 1.0, 0.0 },
                    { 0.0, 0.0, 0.0, 1.0 }
                }), distance);
            };

            Designer.Instance.Scene.AddTransformation(d, t);
            return t;
        }

        public Func<ITransformator> MakeXRotation(Drawable d, Counter c, Func<int, Vector<double>> distance = null)
        {
            Func<ITransformator> t = () => new Transformator(Matrix<double>.Build.DenseOfArray(new double[,]
            {
                { Math.Cos(2 * c.Value), 0.0, -Math.Sin(2 * c.Value), 0.0 },
                { 0.0, 1.0, 0.0, 0.0 },
                { Math.Sin(2 * c.Value), 0, Math.Cos(2 * c.Value), 0.0 },
                { 0.0, 0.0, 0.0, 1.0 }
            }), distance);

            Designer.Instance.Scene.AddTransformation(d, t);
            return t;
        }

        public Func<ITransformator> MakeYRotation(Drawable d, Counter c, Func<int, Vector<double>> distance = null)
        {
            Func<ITransformator> t = () => new Transformator(Matrix<double>.Build.DenseOfArray(new double[,]
            {
                { 1.0, 0.0, 0.0, 0.0 },
                { 0.0, Math.Cos(2 * c.Value), -Math.Sin(2 * c.Value), 0.0 },
                { 0.0, Math.Sin(2 * c.Value), Math.Cos(2 * c.Value), 0.0 },
                { 0.0, 0.0, 0.0, 1.0 }
            }), distance);

            Designer.Instance.Scene.AddTransformation(d, t);
            return t;
        }
    }
}

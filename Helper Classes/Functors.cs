using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Text;

namespace gk_lab_final
{
    public static class Functors
    {
        #region Movement

        public enum Movement { Up, Down, Left, Right };

        public static Vector<double> Mover(Movement direction, double distance)
        {
            switch(direction)
            {
                case Movement.Up:
                    return Vector<double>.Build.DenseOfArray(new double[] { distance, 0.0, 0.0, 0.0 });
                case Movement.Right:
                    return Vector<double>.Build.DenseOfArray(new double[] { 0.0, distance, 0.0, 0.0 });
                case Movement.Down:
                    return Vector<double>.Build.DenseOfArray(new double[] { 0.0, 0.0, distance, 0.0 });
                case Movement.Left:
                    return Vector<double>.Build.DenseOfArray(new double[] { 0.0, 0.0, 0.0, distance });
            }

            return Vector<double>.Build.DenseOfArray(new double[] { });
        }

        #endregion

        public static int Sign(double a)
        {
            if (a < 0) return -1;
            if (a == 0) return 0;
            
            return 1;
        }

        public static Vector<double> PlaneEquation(Vector<double> planeNormal, IPoint p)
        {
            return Vector<double>.Build.DenseOfArray(new double[]
            {
                planeNormal[0],
                planeNormal[1],
                planeNormal[2],
                (-1) * (planeNormal[0] * p.X + planeNormal[1] * p.Y + planeNormal[2] * p.Z)
            });
        }

        public static double Plane(IPoint p, Vector<double> plane)
        {
            return plane[0] * p.X + plane[1] * p.Y + plane[2] * p.Z;
        }

        public static IPoint SegmentFromVector(IPoint origin, Vector<double> v, double length)
        {
            double scale = Functors.Vabs(v) / length;
            v = v * scale;

            return new Point3D(
                origin.X + v[0],
                origin.Y + v[1],
                origin.Z + v[2]
                );
        } 

        public static double Vabs(Vector<double> v)
        {
            double r = 0;

            foreach (var c in v)
                r += Math.Pow(c, 2);

            return Math.Sqrt(r);
        }

        public static double abs3D (Vector<double> v)
        {
            return Math.Sqrt(Math.Pow(v[0], 2) + Math.Pow(v[1], 2) + Math.Pow(v[2], 2));
        }

        public static Vector<double> Normalize(Vector<double> vx) 
        {
            double abs = Math.Sqrt(Math.Pow(vx[0], 2) + Math.Pow(vx[1], 2) + Math.Pow(vx[2], 2));
            if (abs == 0) return vx;

            var v = Vector<double>.Build;
            return v.Dense(new double[] { Math.Abs(vx[0] / abs), Math.Abs(vx[1] / abs), Math.Abs(vx[2] / abs) });
        }

        public static Vector<double> Versorize(Vector<double> vx)
        {
            double abs = Math.Sqrt(Math.Pow(vx[0], 2) + Math.Pow(vx[1], 2) + Math.Pow(vx[2], 2));
            if (abs == 0) return vx;

            var v = Vector<double>.Build;
            return v.Dense(new double[] {vx[0] / abs, vx[1] / abs, vx[2] / abs });
        }

        public static Vector<double> CrossProduct(Vector<double> v1, Vector<double> v2)
        {
            var v = Vector<double>.Build;
            return v.Dense(new double[] { 
                v1[1] * v2[2] - v2[1] * v1[2],
                v1[2] * v2[0] - v2[2] * v1[0],
                v1[0] * v2[1] - v2[0] * v1[1]
            });
        }

        public static double DotProduct(Vector<double> v1, Vector<double> v2)
        {
            return v1[0]*v2[0] + v1[1]*v2[1] + v1[2]*v2[2];
        }

        public static Vector<double> Distance(Vector<double> v1, Vector<double> v2)
        {
            var v = Vector<double>.Build;
            return v.Dense(new double[]
            {
                v2[0] - v1[0],
                v2[1] - v1[1],
                v2[2] - v1[2]
            });
        }

        public static double Transform(double v, double scale) => (v + 1) * 0.5 * scale;
        public static double DeTransform(double v, double scale) => (v / scale / 0.5) - 1;
        public static IPoint DeNormalize(IPoint p)
        {
            IPoint px = p.Clone();

            double d = p.Z == 0 ? 1 : p.ZBuffer / p.Z; if (d == 0) d = 1;
            px.X = Functors.DeTransform(p.X, Designer.Instance.Width) * d; // 0 / 3
            px.Y = Functors.DeTransform(Designer.Instance.Height - p.Y, Designer.Instance.Height) * d; // 1 / 3
            px.Z = p.Z * d;
            px.ZBuffer = d;

            return px;
        }

        public static IPoint ReCalculate(IPoint p)
        {
            IPoint px = Functors.DeNormalize(p);
            Vector<double> v = px.ToVector();
            Designer.Instance.Scene.ChosenCamera.InverseProjectionMatrix.Multiply(v, v);

            return new Point3D(v[0], v[1], v[2], v[3]);
        }

        public static Vector<double> PlaneNormal(IPoint A, IPoint B, IPoint C)
        {
            return Vector<double>.Build.DenseOfArray(new double[] {
                (B.Y - A.Y) * (C.Z - A.Z) - (C.Y - A.Y) * (B.Z - A.Z),
                (B.Z - A.Z) * (C.X - A.X) - (C.Z - A.Z) * (B.X - A.X),
                (B.X - A.X) * (C.Y - A.Y) - (C.X - A.X) * (B.Y - A.Y)
            });
        }

        public static Vector<double> LineEquation(IPoint a, IPoint b)
        {
            if (a.X == b.X) return Vector<double>.Build.DenseOfArray(new double[] { double.PositiveInfinity, a.X });
            if (a.Y == b.Y) return Vector<double>.Build.DenseOfArray(new double[] { 0, a.Y });

            double A = (a.Y - b.Y) / (a.X - b.X);
            double B = A * a.X - a.Y;

            return Vector<double>.Build.DenseOfArray(new double[] { A, B });
        }

        public static IPoint LineIntersection(Vector<double> l1, Vector<double> l2, Vector<double> plane)
        {
            double x = 0;
            double y = 0;
            double z;


            if (l1[0] == double.PositiveInfinity)
            {
                x = l1[1];
                y = l2[1];

            }
            else if (l2[0] == double.PositiveInfinity)
            {
                x = l2[1];
                y = l1[1];
            }
            else
            {
                x = (l1[1] - l2[1]) / (l2[0] - l1[0]);
                y = x * l1[0] + l1[1];
            }

            z = (-1) * (plane[0] * x + plane[1] * y + plane[3]) / plane[2];
            
            return new Point3D(x, y, z);
        }

        public static double Mean(params double[] args)
        {
            double sum = 0;

            foreach (var arg in args)
                sum += arg;

            return sum / args.Length;
        }
    }
}

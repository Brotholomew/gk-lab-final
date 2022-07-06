using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace gk_lab_final
{
    public class Buffer
    {
        private Dictionary<double, Dictionary<double, Dictionary<double, Dictionary<ITransformator, Dictionary<Camera, Vector<double>>>>>> cache;
        public ConcurrentDictionary<(Matrix<double>, Matrix<double>), Matrix<double>> MatrixMultiplicationCache; 

        public Buffer()
        {
            this.zBufferMtx = new Mutex();

            this.cache = new Dictionary<double, Dictionary<double, Dictionary<double, Dictionary<ITransformator, Dictionary<Camera, Vector<double>>>>>>();
            this.MatrixMultiplicationCache = new ConcurrentDictionary<(Matrix<double>,Matrix<double>), Matrix<double>>();
        }

        public void Clear()
        {
            this.ResetZBuffer();
        }

        public void MultiplyMatrices(ref Matrix<double> M1, ref Matrix<double> M2)
        {
            if (this.MatrixMultiplicationCache.ContainsKey((M1, M2)))
                M2 = this.MatrixMultiplicationCache[(M1, M2)];

            Matrix<double> old = M2.Clone();

            M1.Multiply(M2, M2);
            this.MatrixMultiplicationCache[(M1, old)] = M2;
        }

        #region ZBuffer

        private double[,] zBuffer;
        private Mutex zBufferMtx;

        public double[,] ZBuffer { get => this.zBuffer; }
        public Mutex ZBufferMtx { get => this.zBufferMtx; }

        private void ResetZBuffer()
        {
            this.zBuffer = new double[Designer.Instance.Width, Designer.Instance.Height];

            for (int i = 0; i < Designer.Instance.Width; i++)
                for (int j = 0; j < Designer.Instance.Height; j++)
                    this.zBuffer[i, j] = Double.PositiveInfinity;
        }

        #endregion

        public bool CacheContains(Vector<double> v, ITransformator t)
        {
            if (this.cache.ContainsKey(v[0]))
                if (this.cache[v[0]].ContainsKey(v[1]))
                    if (this.cache[v[0]][v[1]].ContainsKey(v[2]))
                        if (this.cache[v[0]][v[1]][v[2]].ContainsKey(t))
                            if (this.cache[v[0]][v[1]][v[2]][t].ContainsKey(Designer.Instance.Scene.ChosenCamera))
                                return true;

            return false;
        }

        public Vector<double> Cache(Vector<double> v, ITransformator t)
        {
            if (this.cache.ContainsKey(v[0]))
                if (this.cache[v[0]].ContainsKey(v[1]))
                    if (this.cache[v[0]][v[1]].ContainsKey(v[2]))
                        if (this.cache[v[0]][v[1]][v[2]].ContainsKey(t))
                            if (this.cache[v[0]][v[1]][v[2]][t].ContainsKey(Designer.Instance.Scene.ChosenCamera))
                                return this.cache[v[0]][v[1]][v[2]][t][Designer.Instance.Scene.ChosenCamera];

            return null;
        }

        public void PutInCache(Vector<double> key, ITransformator t, Vector<double> value)
        {
            if (!this.cache.ContainsKey(key[0]))
                this.cache[key[0]] = new Dictionary<double, Dictionary<double, Dictionary<ITransformator, Dictionary<Camera, Vector<double>>>>>();

            if (!this.cache[key[0]].ContainsKey(key[1]))
                this.cache[key[0]][key[1]] = new Dictionary<double, Dictionary<ITransformator, Dictionary<Camera, Vector<double>>>>();

            if (!this.cache[key[0]][key[1]].ContainsKey(key[2]))
                this.cache[key[0]][key[1]][key[2]] = new Dictionary<ITransformator, Dictionary<Camera, Vector<double>>>();

            if (!this.cache[key[0]][key[1]][key[2]].ContainsKey(t))
                this.cache[key[0]][key[1]][key[2]][t] = new Dictionary<Camera, Vector<double>>();

            this.cache[key[0]][key[1]][key[2]][t][Designer.Instance.Scene.ChosenCamera] = value;
        }
    }
}

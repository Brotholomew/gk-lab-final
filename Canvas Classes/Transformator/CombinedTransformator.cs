using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Text;

namespace gk_lab_final
{
    public class CombinedTransformator : ITransformator
    {
        private List<ITransformator> transformators
        {
            get
            {
                List<ITransformator> ret = new List<ITransformator>();
                
                ret.AddRange(this.spaceTransformations);
                ret.AddRange(this.viewTransformations);

                return ret;
            }
        }

        public List<ITransformator> spaceTransformations;
        private List<ITransformator> viewTransformations;

        public CombinedTransformator()
        {
            this.Init();
        }

        public CombinedTransformator(List<ITransformator> lt, bool space = true)
        {
            this.Init();

            if (space)
                this.spaceTransformations.AddRange(lt);
            else
                this.viewTransformations.AddRange(lt);
        }

        private void Init()
        {
            this.spaceTransformations = new List<ITransformator>();
            this.viewTransformations = new List<ITransformator>();
        } 

        public void AddSpaceTransformation(ITransformator t) => this.spaceTransformations.Add(t);
        public void AddViewTransformation(ITransformator t) => this.viewTransformations.Add(t);

        public ITransformator SpaceTransformations() => new CombinedTransformator(this.spaceTransformations);
        public ITransformator ViewTransformations() => new CombinedTransformator(this.viewTransformations, false);

        public Vector<double> Transform(Vector<double> p)
        {
           if (Designer.Instance.Buffer.CacheContains(p, this))
                return Designer.Instance.Buffer.Cache(p, this);

            Vector<double> op = p.Clone();

            foreach (var t in this.transformators)
                p = t.Transform(p);

            Designer.Instance.Buffer.PutInCache(op, this, p);
            return p;
        }

        public override int GetHashCode()
        {
            int ret = 0;
            foreach (var t in this.transformators)
                ret ^= t.GetHashCode();

            return ret;
        }

        public override bool Equals(object obj)
        {
            return this.GetHashCode() == obj.GetHashCode();
        }
    }
}

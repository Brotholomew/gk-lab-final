using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace gk_lab_final
{
    public class Scene
    {
        public Scene()
        {
            this.transformations = new ConcurrentDictionary<Drawable, List<Func<ITransformator>>>();
            this.Cameras = new List<Camera>();
        }

        #region Camera

        public List<Camera> Cameras { get; private set; }

        private Camera chosenCamera;
        public Camera ChosenCamera 
        { 
            get => this.chosenCamera;
            set
            {
                this.chosenCamera = value;
                this.chosenCamera.Invalidate();
            } 
        }

        #endregion

        public void Invalidate()
        {
            if (this.chosenCamera == null)
                return;

            this.ChosenCamera.Invalidate();
            this.InvalidateLight();
        }

        #region Transformations

        private ConcurrentDictionary<Drawable, List<Func<ITransformator>>> transformations;

        public void AddTransformation(Drawable d, Func<ITransformator> t)
        {
            this.transformations.AddOrUpdate(d, new List<Func<ITransformator>> { t }, (Drawable d, List<Func<ITransformator>> lt) => { lt.Add(t); return lt; });
        }

        public void SafeAddTransformation(Drawable d, Func<ITransformator> t)
        {
            if (this.transformations.ContainsKey(d))
                if (this.transformations[d].Contains(t))
                    return;

            this.AddTransformation(d, t);
        }

        public void RemoveTransformation(Drawable d, Func<ITransformator> t)
        {
            if (this.transformations.ContainsKey(d))
                this.transformations[d].Remove(t);
        }

        public void SpaceTransform(Drawable d)
        {
            if (d.Transformed) return;

            CombinedTransformator combined = new CombinedTransformator();

            if (this.transformations.ContainsKey(d))
                foreach (var t in this.transformations[d])
                    combined.AddSpaceTransformation(t.Invoke()); ;

            d.SpaceTransform(combined);
        }

        public void ViewTransform(Drawable d)
        {
            if (d.ViewTransformed) return;

            CombinedTransformator combined = new CombinedTransformator();

            combined.AddViewTransformation(this.cameraMatrix());
            combined.AddViewTransformation(this.normalizer);

            d.ViewTransform(combined);
        }

        #endregion

        #region Matrices

        private ITransformator viewMatrix()
        {
            if (this.ChosenCamera == null)
                return new EmptyTransformer();

            return new Transformator(this.ChosenCamera.ViewMatrix);
        }
        private ITransformator projectionMatrix()
        {
            if (this.ChosenCamera == null)
                return new EmptyTransformer();

            return new Transformator(this.ChosenCamera.ProjectionMatrix);
        }
        private ITransformator cameraMatrix()
        {
            if (this.ChosenCamera == null)
                return new EmptyTransformer();

            return new Transformator(this.ChosenCamera.CameraMatrix);
        }
        
        private ITransformator normalizer { get; } = new Normalizer();

        #endregion

        #region Light

        public enum ShadingModels { Flat, Gourard, Phong };
        public ShadingModels ShadingModel { get; set; }

        public List<LightSource> LightSources { get; private set; } = new List<LightSource>();

        public void InvalidateLight()
        {
            foreach (var source in this.LightSources)
                if (source.Enabled) source.Transform(new CombinedTransformator(new List<ITransformator>()
                {
                    // this.viewMatrix(),
                }, true));
        }

        #endregion
    }
}

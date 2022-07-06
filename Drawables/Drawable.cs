using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace gk_lab_final
{
    public abstract class Drawable
    {
        public Dictionary<IPoint, IPoint> PointMappings = new Dictionary<IPoint, IPoint>();
        public Polyhedron P { get; set; } = null;

        public List<Drawable> Drawables { get; set; }
        public virtual List<Vertex> Vertices { get; set; }
        public virtual List<Triangle> Triangles { get; protected set; } = new List<Triangle>();

        public abstract void Print(Designer.PrintingMode pm);
        public virtual void Fill(Designer.PrintingMode pm, Color c) { }

        #region Transformations

        public bool Transformed { get; protected set; } = false;
        public bool ViewTransformed { get; protected set; } = false;

        public virtual void Invalidate()
        {
            foreach (var d in this.Drawables)
                d.Invalidate();

            this.Transformed = false;
            this.ViewTransformed = false;
        }
        public virtual void Validate() => this.Transformed = true;

        public virtual void SpaceTransform(ITransformator transformator)
        {
            if (this.Transformed) return;

            foreach (var t in this.Triangles)
            {
                t.SpaceTransform(transformator.SpaceTransformations());
                t.Cull();
            }

            this.Validate();
        }

        public virtual void ViewTransform(ITransformator transformator)
        {
            if (this.ViewTransformed) return;

            foreach (var t in this.Triangles)
                if (t.Visible)
                    t.ViewTransform(transformator.ViewTransformations());

            this.ViewTransformed = true;
        }

        #endregion

        #region Back-face Culling

        public virtual bool Visible { get; protected set; } = true;
        public virtual void Cull() { }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace gk_lab_final
{
    public class Line : Drawable
    {
        public IPoint A { get => this.Vertices[0].TransformedCenter; }
        public IPoint B { get => this.Vertices[1].TransformedCenter; }

        public Line(Vertex a, Vertex b)
        {
            a.Drawables.Add(this);
            b.Drawables.Add(this);

            this.Drawables = new List<Drawable>();
            this.Vertices = new List<Vertex>();

            this.Vertices.Add(a);
            this.Vertices.Add(b);
        }

        public override void Print(Designer.PrintingMode pm)
        {
            Designer.Instance.PrintLine(this.A, this.B, pm);

            foreach (var v in this.Vertices)
                v.Print(pm);
        }

        public override void Invalidate()
        {
            foreach (var v in this.Vertices)
                v.Invalidate();

            this.Transformed = false;
            this.ViewTransformed = false;
        }

        public override void SpaceTransform(ITransformator transformator)
        {
            if (this.Transformed) return;

            foreach (var t in this.Vertices)
            {
                t.SpaceTransform(transformator.SpaceTransformations());
            }

            this.Validate();
        }

        public override void ViewTransform(ITransformator transformator)
        {
            if (this.ViewTransformed) return;

            foreach (var t in this.Vertices)
                t.ViewTransform(transformator.ViewTransformations());

            this.ViewTransformed = true;
        }
    }
}

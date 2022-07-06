using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace gk_lab_final
{
    public class Polygon : Drawable
    {
        public override List<Vertex> Vertices
        {
            get
            {
                List<Vertex> verts = new List<Vertex>();
                
                foreach (var d in this.Drawables)
                    foreach (var v in d.Vertices)
                        if (!verts.Contains(v)) verts.Add(v);

                return verts;
            }
        }

        public Polygon(List<Drawable> Triangles)
        {
            foreach (var line in Triangles)
                line.Drawables.Add(this);

            this.Drawables = Triangles;
        }

        public override void Fill(Designer.PrintingMode pm, Color c) => Designer.Instance.FillPolygon(this, c, pm);

        public override void Print(Designer.PrintingMode pm)
        {
            foreach (var d in this.Drawables)
                d.Print(pm);

            foreach (var v in this.Vertices)
                v.Print(pm);
        }
    }
}

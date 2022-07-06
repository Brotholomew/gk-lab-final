using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace gk_lab_final
{
    public class Canvas
    {
        private HashSet<Drawable> MainDrawables;
        private HashSet<Drawable> PreviewDrawables;

        public DirectBitmap Main { get; private set; }
        public DirectBitmap Preview { get; private set; }

        private CanvasParams Params;

        public Canvas(CanvasParams p)
        {
            this.Params = p;

            this.MainDrawables = new HashSet<Drawable>();
            this.PreviewDrawables = new HashSet<Drawable>();

            this.Init();
        }

        private void Init()
        {
            this.Main = new DirectBitmap(this.Params.MainPictureBox.Width, this.Params.MainPictureBox.Height);
            this.Preview = new DirectBitmap(this.Params.MainPictureBox.Width, this.Params.MainPictureBox.Height);
        }

        public void Refresh() => this.Params.MainPictureBox.Refresh();
        public void Erase()
        {
            this.Main.Dispose();
            this.Preview.Dispose();

            this.Init();
        }
        public void Reprint()
        {
            this.Erase();

            foreach (var d in this.MainDrawables)
            {
                Designer.Instance.Scene.SpaceTransform(d);
            }

            foreach (var d in this.PreviewDrawables)
            {
                Designer.Instance.Scene.SpaceTransform(d);
            }


            foreach (var d in this.MainDrawables)
            {
                Designer.Instance.Scene.ViewTransform(d);
            }

            foreach (var d in this.PreviewDrawables)
            {
                Designer.Instance.Scene.ViewTransform(d);
            }

            Parallel.ForEach(this.MainDrawables, (Drawable d) => d.Fill(Designer.PrintingMode.Main, Color.Empty));
            Parallel.ForEach(this.PreviewDrawables, (Drawable d) => d.Fill(Designer.PrintingMode.Preview, Color.Empty));

        }

        public void Repaint(Graphics gx)
        {
            gx.DrawImage(this.Main.Bitmap, 0, 0);
            gx.DrawImage(this.Preview.Bitmap, 0, 0);
        }

        public void Invalidate()
        {
            foreach (var d in this.MainDrawables)
                d.Invalidate();

            foreach (var d in this.PreviewDrawables)
                d.Invalidate();
        }

        public void RestartScene()
        {
            this.Invalidate();
            this.Reprint();
            this.Refresh();
        }

        public DirectBitmap Bitmap(Designer.PrintingMode pm)
        {
            switch(pm)
            {
                case Designer.PrintingMode.Main:
                    return this.Main;
                case Designer.PrintingMode.Preview:
                    return this.Preview;
            }

            return null;
        }

        public bool InBounds(IPoint p)
        {
            return p.X >= 0 && p.Y >= 0 && p.X < this.Main.Width && p.Y < this.Main.Height;
        }

        public void ClearObjects()
        {
            this.MainDrawables.Clear();
            this.PreviewDrawables.Clear();
        }

        #region Switch Canvas

        public void AddToPreview(Drawable d) => this.PreviewDrawables.Add(d);

        public void AddToMain(Drawable d) => this.MainDrawables.Add(d);

        public void SwitchToPreview(Drawable d) => this.SwitchCanvas(this.MainDrawables, this.PreviewDrawables, d);

        public void SwitchToMain(Drawable d) => this.SwitchCanvas(this.PreviewDrawables, this.MainDrawables, d);

        private void SwitchCanvas(HashSet<Drawable> from, HashSet<Drawable> to, Drawable d)
        {
            if (from.Contains(d))
                from.Remove(d);

            to.Add(d);
        }

        #endregion
    }
}

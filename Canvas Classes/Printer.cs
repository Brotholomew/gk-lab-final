using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace gk_lab_final
{
    public class Printer
    {
        private PrinterParams Params;

        public Printer(PrinterParams p)
        {
            this.Params = p;
        }

        public void PrintLine(IPoint a, IPoint b, Bitmap bmp) => this.PrintLine_Internal(a, b, bmp, Embellisher.PrintingColor);
        public void PrintLine_Internal(IPoint a, IPoint b, Bitmap bmp, Color c) => this.PerformOn(bmp, (Graphics gx) => gx.DrawLine(new Pen(c), a.SPoint(), b.SPoint()));

        public void PrintVertex(IPoint a, IPoint where, Bitmap bmp) => this.PrintVertex_Internal(a, where, bmp, Embellisher.PrintingColor, Embellisher.VertexRadius);
        public void PrintVertex_Internal(IPoint a, IPoint where, Bitmap bmp, Color c, int r) => this.PerformOn(bmp, (Graphics gx) => this._PrintVertex(a, where, gx, c, r));

        public void PutPixel(IPoint a, DirectBitmap bmp, Color c) => bmp.SetPixel((int)a.X, (int)a.Y, c);

        private void PerformOn(Bitmap bmp, Action<Graphics> a)
        {
            Graphics gx = Graphics.FromImage(bmp);

            a(gx);
            gx.Flush();
        }

        private void _PrintVertex(IPoint p, IPoint where, Graphics gx, Color c, int r)
        {
            gx.DrawString("[ " + Math.Round(p.X, 2) + "; " + Math.Round(p.Y, 2) + "; " + Math.Round(p.Z, 2) + " ]", SystemFonts.DefaultFont, Brushes.Red, new Point((int)where.X, (int)where.Y + 10));
            gx.FillEllipse(new SolidBrush(c), (int)where.X - r, (int)where.Y - r, 2 * r + 1, 2 * r + 1);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace gk_lab_final
{
    public class Designer
    {
        #region Singleton Fields and Methods

        public static Designer Instance { get => Designer._Instance; }
        
        public static void Init(DesignerParams p)
        {
            Designer._Instance = new Designer(p);
        }

        private static Designer _Instance = null;

        private Designer(DesignerParams p)
        {
            this.Params = p;

            this.canvas = new Canvas(new CanvasParams(this.Params.MainPictureBox));
            this.Printer = new Printer(new PrinterParams());
            this.Filler = new Filler(new FillerParams());
            this.Observer = new Observer(new ObserverParams());
            this.buffer = new Buffer();
            this.scene = new Scene();
        }

        #endregion

        public DesignerParams Params;

        private Canvas canvas;
        private Printer Printer;
        private Filler Filler;
        private Observer Observer;
        private Buffer buffer;
        private Scene scene;

        public Buffer Buffer { get => this.buffer; }
        public Canvas Canvas { get => this.canvas; }
        public Scene Scene { get => this.scene; }

        public void Refresh() => this.Canvas.Refresh();
        public void Erase() => this.Canvas.Erase();
        public void Reprint() => this.Canvas.Reprint();
        public void Restart() => this.Invalidate(true);

        public int Width { get => this.Canvas.Main.Width; }
        public int Height { get => this.Canvas.Main.Height; }

        public void Invalidate(bool invalidateScene = false)
        {
            if (invalidateScene) this.Scene.Invalidate();
            this.Buffer.Clear(); 
            this.Canvas.RestartScene();
        }

        public int Alpha { get; set; }
      
        public enum PrintingMode { Main, Preview };

        public void PrintLine(IPoint a, IPoint b, PrintingMode pm) => this.Printer.PrintLine(a, b, this.Canvas.Bitmap(pm).Bitmap);
        public void PrintVertex(IPoint a, IPoint where, PrintingMode pm) => this.Printer.PrintVertex(a, where, this.Canvas.Bitmap(pm).Bitmap);

        public void PutPixel(IPoint a, PrintingMode pm, Color c) => this.Printer.PutPixel(a, this.Canvas.Bitmap(pm), c);

        public void FillPolygon(Drawable d, Color c, PrintingMode pm)
        {
            foreach (var t in d.Triangles)
                this.Filler.ScanLine(t, c, pm);
        }

        public void FillTriangle(Triangle t, Color c, PrintingMode pm)
        {
            this.Filler.ScanLine(t, c, pm);
        }

        public void Repaint(Graphics gx) => this.Canvas.Repaint(gx);
    }
}

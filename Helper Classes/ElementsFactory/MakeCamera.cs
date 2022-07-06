using System;
using System.Collections.Generic;
using System.Text;

namespace gk_lab_final
{
    public partial class ElementsFactory
    {
        public Camera MakeCamera(IPoint position, ITarget target, string name)
        {
            Camera cam = new Camera(position, target, name);
            this.Cameras.Add(cam);

            return cam;
        }

        public void ChooseCamera(Camera cam) => this.ChosenCamera = cam;
    }
}

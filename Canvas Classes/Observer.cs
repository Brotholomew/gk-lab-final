using System;
using System.Collections.Generic;
using System.Text;

namespace gk_lab_final
{
    public class Observer
    {
        private ObserverParams Params;

        public Observer(ObserverParams p)
        {
            this.Params = p;
        }

        public IPoint Position()
        {
            return new Point3D(0, 0, 0);
        }
    }
}

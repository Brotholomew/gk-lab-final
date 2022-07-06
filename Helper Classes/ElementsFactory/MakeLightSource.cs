using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace gk_lab_final
{
    public partial class ElementsFactory
    {
        public SphericalLightSource MakeSphericalLightSource(double kd, double ks, Color c, IPoint pos, string name)
        {
            var ls = new SphericalLightSource(kd, ks, c, pos, name);
            this.LightSources.Add(ls);

            return ls;
        }

        public Reflector MakeReflector(double kd, double ks, Color c, IPoint pos, ITarget target, string name)
        {
            var r = new Reflector(kd, ks, c, pos, target, name);
            this.LightSources.Add(r);

            return r;
        }
    
        public void ChooseLightSource(LightSource ls) => this.ChosenLightSource = ls;
    }
}

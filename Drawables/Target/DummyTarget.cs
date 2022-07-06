using System;
using System.Collections.Generic;
using System.Text;

namespace gk_lab_final
{
    public class DummyTarget : ITarget
    {
        public IPoint Position { get; set; }
        public bool Locked { get; set; } = false;

        public DummyTarget(IPoint position) => this.Position = position;
    }
}

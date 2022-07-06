using System;
using System.Collections.Generic;
using System.Text;

namespace gk_lab_final
{
    public interface ITarget
    {
        IPoint Position { get; set; }
        bool Locked { get; set; }
    }
}

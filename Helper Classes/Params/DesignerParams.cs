using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace gk_lab_final
{
    public struct DesignerParams
    {
        public PictureBox MainPictureBox;

        public DesignerParams(
            PictureBox mainPictureBox
            )
        {
            this.MainPictureBox = mainPictureBox;
        }
    }
}

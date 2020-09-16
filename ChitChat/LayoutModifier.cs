using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChitChat
{
    static public class LayoutModifier
    {
        static public void centerControlHorizontally(Control ctrlToCenter)
        {
            ctrlToCenter.Left = (ctrlToCenter.Parent.Width - ctrlToCenter.Width) / 2;
            
        }
        static public void centerControlVertically(Control ctrlToCenter)
        {
            ctrlToCenter.Top = (ctrlToCenter.Parent.Height - ctrlToCenter.Height) / 2;
        }
        static public void centerControl(Control ctrlToCenter)
        {
            LayoutModifier.centerControlHorizontally(ctrlToCenter);
            LayoutModifier.centerControlVertically(ctrlToCenter);
        }
        
    }
}

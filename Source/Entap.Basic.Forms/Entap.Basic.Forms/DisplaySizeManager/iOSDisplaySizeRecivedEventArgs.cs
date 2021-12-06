using System;
namespace Entap.Basic.Forms
{
    public class iOSDisplaySizeRecivedEventArgs : EventArgs
    {
        public double NavigationBarHeight { get; set; }
        public double PageHeight { get; set; }
    }
}

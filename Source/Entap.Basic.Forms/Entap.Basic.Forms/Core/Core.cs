using System;
using Xamarin.Forms;

namespace Entap.Basic.Forms
{
    public static class Core
    {
        static Application _application;

        public static void Init(Application application)
        {
            _application = application;
        }

        public static Application Application
        {
            get
            {
                if (_application is null)
                    throw new Exception("Please call Entap.Basic.Forms.Core.Init() method.");

                return _application;
            }
        }
    }
}

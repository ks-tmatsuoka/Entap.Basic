using System;
using System.Collections.Generic;

namespace Entap.Basic.Launch.Guide
{
    public class GuidePageContent
    {
        public GuidePageContent()
        {
        }

        public string PageTitle { get; set; }

        public IEnumerable<GuideContent> Contents { get; set; }
    }
}

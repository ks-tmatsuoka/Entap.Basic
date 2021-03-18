using System;
using System.Threading.Tasks;

namespace Entap.Basic
{
    public interface IPageNavigator
    {
        void SetHomePage();

        Task SetGuidePageAsync();
    }
}

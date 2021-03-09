using System;
using System.Net.Http;

namespace SHIRO.CO
{
    public interface IRefitApi
    {
        HttpClient Client { get; }
    }
}

using System;
using Entap.Basic.Refit;

namespace SHIRO.CO
{
    /// <summary>
    /// RestService生成用に定義を集約
    /// </summary>
    public interface IBasicClinet : IHttpClient, IBasicAuthApi
    {
    }
}

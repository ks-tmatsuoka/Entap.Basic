using System;
using System.Net.Http;

namespace Entap.Basic.Refit
{
    /// <summary>
    /// HttpClientインターフェース
    /// RestServiceからHttpClientにアクセス可能にする
    /// </summary>
    public interface IHttpClient
    {
        HttpClient Client { get; }
    }
}
using System;
using Entap.Basic.Core;
using Refit;

namespace Entap.Basic.Refit
{
    /// <summary>
    /// APIのRestServiceを管理する
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiManager<T> : ApiService where T : IHttpClient
    {
#nullable enable
        public ApiManager(string hostUrl, RefitSettings? refitSettings = null)
#nullable disable
        {
            Api = GetRestService<T>(hostUrl, refitSettings);
        }

        /// <summary>
        /// RestService
        /// </summary>
        public T Api { get; }

        /// <summary>
        /// RestServiceを取得する
        /// </summary>
        /// <typeparam name="T">RestService</typeparam>
        /// <param name="hostUrl">ホストUrl</param>
        /// <param name="refitSettings">RefitSettings</param>
        /// <returns></returns>
#nullable enable
        public virtual T GetRestService<T>(string hostUrl, RefitSettings? refitSettings = null) where T : IHttpClient
#nullable disable
        {
            return RestService.For<T>(hostUrl, refitSettings);
        }

        /// <summary>
        /// 認証情報を設定する
        /// </summary>
        /// <param name="httpClient">RestService</param>
        /// <param name="type">認証方法</param>
        /// <param name="token">トークン</param>
        public virtual void SetAuthorization(string type, string token)
        {
            Api.Client.SetAuthorization(type, token);
        }

        /// <summary>
        /// 認証情報を削除する
        /// </summary>
        /// <param name="httpClient">RestService</param>
        public virtual void ClearAuthorization()
        {
            Api.Client.ClearAuthorization();
        }
    }
}
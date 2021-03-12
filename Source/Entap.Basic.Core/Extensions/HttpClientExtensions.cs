using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Entap.Basic.Core
{
    public static class HttpClientExtensions
    {
        /// <summary>
        /// JsonAcceptヘッダを設定する
        /// </summary>
        /// <param name="httpClient">HttpClient</param>
        public static void SetJsonAcceptHeader(this HttpClient httpClient)
        {
            SetAcceptHeader(httpClient, "application/json");
        }

        /// <summary>
        /// Acceptヘッダを設定する
        /// </summary>
        /// <param name="httpClient">HttpClient</param>
        /// <param name="mediaType"></param>
        public static void SetAcceptHeader(this HttpClient httpClient, string mediaType)
        {
            ;
            httpClient.DefaultRequestHeaders.Add("Accept", System.Net.Mime.MediaTypeNames.Application.Json);
        }

        /// <summary>
        /// 認証情報を設定する
        /// </summary>
        /// <param name="httpClient">HttpClient</param>
        /// <param name="type">認証方法</param>
        /// <param name="token">トークン</param>
        public static void SetAuthorization(this HttpClient httpClient, string type, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(type, token);
        }
    }
}

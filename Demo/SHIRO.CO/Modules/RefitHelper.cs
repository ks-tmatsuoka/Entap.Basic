using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;

namespace SHIRO.CO
{
    public class RefitHelper
    {
        /// <summary>
        /// SnakeCase用のシリアライザを取得する
        /// </summary>
        public static NewtonsoftJsonContentSerializer SnakeCaseSerializer
            => new NewtonsoftJsonContentSerializer(
                new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    }
                });

        /// <summary>
        /// CamelCase用のシリアライザを取得する
        /// </summary>
        public static NewtonsoftJsonContentSerializer CamelCaseSerializer
            => new NewtonsoftJsonContentSerializer(
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });

        /// <summary>
        /// APIをコールする
        /// </summary>
        /// <typeparam name="T">レスポンスの型</typeparam>
        /// <param name="funcTask">API処理</param>
        /// <param name="timeoutSeconds">タイムアウト値（秒）</param>
        /// <returns>HttpStatusCode?, APIレスポンス</returns>
        public static async Task<(HttpStatusCode?, T)> CallAsync<T>(Func<Task<T>> funcTask)
        {
            try
            {
                var result = await funcTask().ConfigureAwait(false);
                return (HttpStatusCode.OK, result);
            }
            catch (ApiException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return (ex.StatusCode, default);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return (null, default);
            }
        }

        /// <summary>
        /// APIをコールする
        /// </summary>
        /// <param name="funcTask">API処理</param>
        /// <returns>HttpStatusCode?</returns>
        public static async Task<HttpStatusCode?> CallAsync(Func<Task> funcTask)
        {
            var (status, _) = await CallAsync<object>(async () =>
            {
                await funcTask();
                return null;
            });
            return status;
        }

    }
}

using System;
using System.Net;
using System.Threading.Tasks;
using Refit;

namespace SHIRO.CO
{
    public class RefitHelper
    {
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

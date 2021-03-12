using System;
using System.Threading.Tasks;
using Refit;

namespace Entap.Basic.Refit
{
    /// <summary>
    /// APIの実行処理
    /// </summary>
    public class ApiService
    {
        public ApiService()
        {
        }

        /// <summary>
        /// APIをコールする（APIレスポンスあり）
        /// </summary>
        /// <typeparam name="T">レスポンスの型</typeparam>
        /// <param name="funcTask">API処理</param>
        /// <param name="ignoreError">エラーを無視するか</param>
#nullable enable
        public virtual async Task<ApiResponse<T>?> CallAsync<T>(Func<Task<ApiResponse<T>>> funcTask, bool ignoreError = false)
#nullable disable
        {
            try
            {
                var result = await funcTask().ConfigureAwait(false);
                if (result.IsSuccessStatusCode) return result;
                if (ignoreError) return result;

                HandleApiError(result.Error, ignoreError);
                return result;
            }
            catch (Exception ex)
            {
                HandleException(ex, ignoreError);
                return null;
            }
        }

        /// <summary>
        /// APIをコールする（APIレスポンスなし）
        /// </summary>
        /// <param name="funcTask">API処理</param>
        /// <param name="ignoreError">エラーを無視するか</param>
#nullable enable
        public virtual Task<ApiResponse<Task>?> CallAsync(Func<ApiResponse<Task>> funcTask, bool ignoreError = false)
#nullable disable
        {
            return CallAsync(funcTask, ignoreError);
        }

        /// <summary>
        /// ApiExceptionハンドリング処理
        /// </summary>
        /// <param name="apiException">ApiException</param>
        public virtual void HandleApiError(ApiException apiException, bool ignoreError)
        {
        }

        /// <summary>
        /// 例外ハンドリング処理
        /// </summary>
        /// <param name="exception">Exception</param>
        public virtual void HandleException(Exception exception, bool ignoreError)
        {
        }
    }
}

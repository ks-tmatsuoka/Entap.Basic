using System;
using System.Net;
using System.Threading.Tasks;
using Entap.Basic.Api;
using Entap.Basic.Refit;
using Newtonsoft.Json;
using Refit;
using Xamarin.Forms;

namespace SHIRO.CO
{
    public class BasicApiManager
    {
        static readonly Lazy<BasicApiManager> _instance = new Lazy<BasicApiManager>(() => new BasicApiManager());
        public static BasicApiManager Current => _instance.Value;

        static string HostUrl => Urls.AppApi;
        const string DefaultErrorMessage = "インターネットに接続されていません。通信環境をご確認ください。";

        public IBasicAuthApi AuthApi;

        private BasicApiManager()
        {
            AuthApi = GetInstance<IBasicAuthApi>(HostUrl);
        }

        static T GetInstance<T>(string hostUrl) where T : IBasicAuthApi
        {
            var instance = RestService.For<T>(
                hostUrl,
                new RefitSettings
                {
                    ContentSerializer = RefitSettingsService.SnakeCaseSerializer
                });
            instance.Client.DefaultRequestHeaders.Add("Accept", "application/json");
            return instance;
        }

        /// <summary>
        /// APIをコールする（APIレスポンスあり）
        /// </summary>
        /// <typeparam name="T">レスポンスの型</typeparam>
        /// <param name="funcTask">API処理</param>
        /// <param name="ignoreError">エラーを無視するか</param>
        /// <returns>API実行結果：例外発生時はNull</returns>
#nullable enable
        public static async Task<ApiResponse<T>?> CallAsync<T>(Func<Task<ApiResponse<T>>> funcTask, bool ignoreError = false)
#nullable disable
        {
            try
            {
                var result = await funcTask().ConfigureAwait(false);
                if (result.IsSuccessStatusCode) return result;
                if (ignoreError) return result;

                await HandleApiErrorAsunc(result.Error);
                return result;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await DisplayErrorDialogAsync();
                return null;
            }
        }

        /// <summary>
        /// APIをコールする（APIレスポンスなし）
        /// </summary>
        /// <param name="funcTask">API処理</param>
        /// <param name="ignoreError">エラーを無視するか</param>
        /// <returns>API実行結果：例外発生時はNull</returns>
#nullable enable
        public static Task<ApiResponse<Task>?> CallAsync(Func<ApiResponse<Task>> funcTask, bool ignoreError = false)
#nullable disable
        {
            return CallAsync(funcTask, ignoreError);
        }

        /// <summary>
        /// エラーハンドリング処理
        /// </summary>
        /// <param name="ex">ApiException</param>
        /// <returns>Task</returns>
        static async Task HandleApiErrorAsunc(ApiException ex)
        {
            // ToDo エラー処理

            var message = GetErrorMessage(ex);
            await DisplayErrorDialogAsync(message);
        }

        /// <summary>
        /// エラーメッセージを取得する
        /// </summary>
        /// <param name="apiException">ApiException</param>
        /// <returns>エラーメッセージ</returns>
#nullable enable
        static string? GetErrorMessage(ApiException apiException)
#nullable disable
        {
            if (apiException?.Content is null) return null;

            try
            {
                var errorInfo = JsonConvert.DeserializeObject<ErrorResponse>(apiException.Content);
                return apiException.StatusCode == ExHttpStatusCode.UnprocessableEntity ?
                    errorInfo?.Errors?.First?.First?.First?.ToString() :
                    errorInfo?.Message;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return null;
            }
        }

        /// <summary>
        /// エラーダイアログ表示
        /// </summary>
        /// <param name="errorMessage">エラーメッセージ</param>
        /// <returns>Task</returns>
        static async Task DisplayErrorDialogAsync(string errorMessage = DefaultErrorMessage)
        {
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                await App.Current.MainPage.DisplayAlert("通信エラー", errorMessage, "OK");
            });
        }
    }
}

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
                    ContentSerializer = RefitHelper.SnakeCaseSerializer
                });
            instance.Client.DefaultRequestHeaders.Add("Accept", "application/json");
            return instance;
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

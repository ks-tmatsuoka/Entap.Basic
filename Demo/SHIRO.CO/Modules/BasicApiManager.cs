using System;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Entap.Basic.Core;
using Entap.Basic.Api;
using Entap.Basic.Refit;
using Newtonsoft.Json;
using Refit;
using Xamarin.Forms;
using System.Net.Http;

namespace SHIRO.CO
{
    public class BasicApiManager : ApiManager<IBasicClinet>
    {
        static readonly Lazy<BasicApiManager> _instance = new Lazy<BasicApiManager>(() => new BasicApiManager());
        public static BasicApiManager Current => _instance.Value;

        static string HostUrl => Urls.AppApi;

        const string DefaultErrorMessage = "インターネットに接続されていません。通信環境をご確認ください。";

        private BasicApiManager() : base(HostUrl)
        {
        }

#nullable enable
        public override T GetRestService<T>(string hostUrl, RefitSettings? refitSettings = null)
#nullable disable
        {
            var settings =new RefitSettings
            {
                ContentSerializer = RefitSettingsService.SnakeCaseSerializer
            };
            // Androidでサーバー証明書の検証エラーが発生するため一時無効化
            //var restService = base.GetRestService<T>(hostUrl, settings);
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslErrors) => true
            };
            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(hostUrl)
            };

            var restService = RestService.For<T>(httpClient, settings);
            restService.Client.SetJsonAcceptHeader();
            return restService;
        }

        public void SetAuthorization(ServerAccessToken accessToken)
        {
            base.SetAuthorization(accessToken.TokenType, accessToken.AccessToken);
        }

        /// <summary>
        /// ApiExceptionハンドリング処理
        /// </summary>
        /// <param name="apiException">ApiException</param>
        public override void HandleApiError(ApiException apiException, bool ignoreError)
        {
            System.Diagnostics.Debug.WriteLine(apiException.Message);
            if (ignoreError) return;

            // ToDo エラー処理
            var errorMessage = GetErrorMessage(apiException);
            DisplayErrorDialog(errorMessage);

        }

        /// <summary>
        /// 例外ハンドリング処理
        /// </summary>
        /// <param name="exception">Exception</param>
        public override void HandleException(Exception exception, bool ignoreError)
        {
            System.Diagnostics.Debug.WriteLine(exception.Message);
            if (ignoreError) return;

            DisplayErrorDialog();
        }

        /// <summary>
        /// エラーメッセージを取得する
        /// </summary>
        /// <param name="apiException">ApiException</param>
        /// <returns>エラーメッセージ</returns>
#nullable enable
        string? GetErrorMessage(ApiException apiException)
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
        void DisplayErrorDialog(string errorMessage = DefaultErrorMessage)
        {
            Device.InvokeOnMainThreadAsync(async () =>
            {
                await App.Current.MainPage.DisplayAlert("通信エラー", errorMessage, "OK");
            }).ConfigureAwait(false);
        }
    }
}

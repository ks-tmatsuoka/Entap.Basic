using System;
using System.Net;
using System.Threading.Tasks;
using Refit;

namespace SHIRO.CO
{
    public class BasicApiManager
    {
        static readonly Lazy<BasicApiManager> _instance = new Lazy<BasicApiManager>(() => new BasicApiManager());
        public static BasicApiManager Current => _instance.Value;

        static string HostUrl => Urls.AppApi;

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
    }
}

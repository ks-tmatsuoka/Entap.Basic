using System;
using Refit;

namespace SHIRO.CO.Modules
{
    public class BasicApiManager
    {
        static readonly Lazy<BasicApiManager> _instance = new Lazy<BasicApiManager>(() => new BasicApiManager());
        public static BasicApiManager Current => _instance.Value;

        // ToDo
        const string HostUrl = "";

        public IBasicAuthApi AuthApi;

        private BasicApiManager()
        {
            AuthApi = GetInstance<IBasicAuthApi>(HostUrl);
        }

        static T GetInstance<T>(string hostUrl) where T : IBasicAuthApi
        {
            var instance = RestService.For<T>(hostUrl);
            instance.Client.DefaultRequestHeaders.Add("Accept", "application/json");
            return instance;
        }
    }
}

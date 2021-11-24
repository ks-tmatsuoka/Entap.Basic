using System;
using System.Threading.Tasks;
using Entap.Basic.Api;
using Entap.Basic.Firebase.Auth;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace SHIRO.CO
{
    public class SecureStorageManager : IAccessTokenPreferencesService
    {
        static readonly Lazy<SecureStorageManager> _instance = new Lazy<SecureStorageManager>(() => new SecureStorageManager());
        public static SecureStorageManager Current => _instance.Value;

        struct Keys
        {
            /// <summary>
            /// 認証情報
            /// </summary>
            public const string AccessToken = "AccessToken";
        }

        public Task<ServerAccessToken> GetAccessTokenAsync()
            => GetAsync<ServerAccessToken>(Keys.AccessToken);

        public Task SetAccessTokenAsync(ServerAccessToken accessToken)
            => SetAsync(Keys.AccessToken, accessToken);

        public void RemoveAccessToken()
            => Remove(Keys.AccessToken);

        static async Task SetAsync(string key, object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            await SecureStorage.SetAsync(key, json);
        }

        static async Task<T> GetAsync<T>(string key)
        {
            var json = await SecureStorage.GetAsync(key);
            if (string.IsNullOrWhiteSpace(json))
                return default;
            return JsonConvert.DeserializeObject<T>(json);
        }

        static bool Remove(string key)
        {
            return SecureStorage.Remove(key);
        }
    }
}

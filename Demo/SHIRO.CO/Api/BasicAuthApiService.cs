using System;
using System.Threading;
using System.Threading.Tasks;
using Entap.Basic.Api;

namespace SHIRO.CO
{
    public class BasicAuthApiService : IAuthApi
    {
        public async Task PostAuthFirebaseUser(FirebaseIdToken request, CancellationToken token = default)
        {
            await BasicApiManager.Current.CallAsync(() =>
            {
                return BasicApiManager.Current.Api.PostAuthFirebaseUser(request, token);
            });
        }

        public async Task DeleteAuthFirebaseUser(CancellationToken token = default)
        {
            await BasicApiManager.Current.CallAsync(() =>
            {
                return BasicApiManager.Current.Api.DeleteAuthFirebaseUser(token);
            });
        }

        public async Task<ServerAccessToken> PostAuthFirebaseToken(FirebaseIdToken request, CancellationToken token = default)
        {
            var result = await BasicApiManager.Current.CallAsync(() =>
            {
                return BasicApiManager.Current.Api.PostAuthFirebaseToken(request, token);
            });
            return result?.Content;
        }

        public async Task DeleteAuthLineUser(CancellationToken token = default)
        {
            await BasicApiManager.Current.CallAsync(() =>
            {
                return BasicApiManager.Current.Api.DeleteAuthLineUser(token);
            });
        }

        public async Task PostAuthLineUser(CustomAuthToken request, CancellationToken token = default)
        {
            await BasicApiManager.Current.CallAsync(() =>
            {
                return BasicApiManager.Current.Api.PostAuthLineUser(request, token);
            });
        }

        public async Task<ServerAccessToken> PostAuthLineToken(CustomAuthToken request, CancellationToken token = default)
        {
            var result = await BasicApiManager.Current.CallAsync(() =>
            {
                return BasicApiManager.Current.Api.PostAuthLineToken(request, token);
            });
            return result?.Content;
        }

        public async Task<FirebaseCustomToken> PostAuthFirebaseCustomToken(CancellationToken token = default)
        {
            var result = await BasicApiManager.Current.CallAsync(() =>
            {
                return BasicApiManager.Current.Api.PostAuthFirebaseCustomToken(token);
            });
            return result?.Content;
        }

        public async Task<User> GetUser(CancellationToken token = default)
        {
            var result = await BasicApiManager.Current.CallAsync(() =>
            {
                return BasicApiManager.Current.Api.GetUser(token);
            });
            return result?.Content;
        }

        public async Task DelerteUser(CancellationToken token = default)
        {
            await BasicApiManager.Current.CallAsync(() =>
            {
                return BasicApiManager.Current.Api.DeleteAuthFirebaseUser(token);
            });
        }
    }
}

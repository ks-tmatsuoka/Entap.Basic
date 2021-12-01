using System.Threading.Tasks;
using Entap.Basic.Api;

namespace Entap.Basic.Firebase.Auth
{
    public interface IUserDataRepository
    {
        Task<ServerAccessToken> GetAccessToken();
        Task SetAccessTokenAsync(ServerAccessToken accessToken);
        void RemoveAccessToken();
    }
}
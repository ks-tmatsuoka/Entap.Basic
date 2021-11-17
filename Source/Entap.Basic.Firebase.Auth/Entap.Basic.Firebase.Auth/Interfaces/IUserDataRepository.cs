using System.Threading.Tasks;

namespace Entap.Basic.Firebase.Auth
{
    public interface IUserDataRepository
    {
        Task<string> GetAccessToken();
        Task SetAccessTokenAsync(string accessToken);
        void RemoveAccessToken();
    }
}
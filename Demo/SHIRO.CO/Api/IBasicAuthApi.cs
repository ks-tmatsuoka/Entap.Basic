using System;
using System.Threading;
using System.Threading.Tasks;
using Entap.Basic.Api;
using Refit;

namespace SHIRO.CO
{
    public interface IBasicAuthApi
    {
        /// <summary>
        /// Firebase連携を登録する
        /// ログインしているユーザーにFirebaseアカウントを紐付けます。すでに紐づいている場合は何もしません。
        /// </summary>
        /// <param name="request">リクエストパラメータ</param>
        [Post("/auth/firebase/user")]
        ApiResponse<Task> PostAuthFirebaseUser(FirebaseIdToken request, CancellationToken token = default);

        /// <summary>
        /// [Delete("/auth/firebase/user")]
        /// Firebase連携を解除する
        /// ログインしているユーザーに紐づいているFirebaseの情報を削除します。（サーバー上のみ）
        /// </summary>
        [Delete("/auth/firebase/user")]
        ApiResponse<Task> DeleteAuthFirebaseUser(CancellationToken token = default);

        /// <summary>
        /// アクセストークンを発行する
        /// FirebaseのIDトークンから、サーバーのアクセストークンを発行します。
        /// 該当するユーザーが見つからない場合は、新たに作成します。
        /// </summary>
        /// <param name="request">FirebaseIdToken</param>
        /// <returns>ServerAccessToken</returns>
        [Post("/auth/firebase/token")]
        Task<ApiResponse<ServerAccessToken>> PostAuthFirebaseToken([Body] FirebaseIdToken request, CancellationToken token = default);

        /// <summary>
        /// LINE連携を解除する
        /// ログインしているユーザーに紐づいているLINEの情報を削除します。（サーバー上のみ）
        /// </summary>
        [Delete("/auth/line/user")]
        ApiResponse<Task> DeleteAuthLineUser(CancellationToken token = default);

        /// <summary>
        /// LINE連携を登録する
        /// ログインしているユーザーにLINEのアカウントを紐付けます。
        /// すでに紐づいている場合は何もしません。
        /// </summary>
        /// <param name="request">CustomAuthToken</param>
        [Post("/auth/line/user")]
        ApiResponse<Task> PostAuthLineUser([Body] CustomAuthToken request, CancellationToken token = default);

        /// <summary>
        /// アクセストークンを発行する
        /// LINEのIDトークンまたはアクセストークンから、サーバーのアクセストークンを発行します。
        /// 該当するユーザーが見つからない場合は、新たに作成します。
        /// </summary>
        /// <param name="request"></param>
        /// <returns>ServerAccessToken</returns>
        [Post("/auth/line/token")]
        Task<ApiResponse<ServerAccessToken>> PostAuthLineToken([Body] CustomAuthToken request, CancellationToken token = default);

        /// <summary>
        /// [post]
        /// カスタムトークンを発行する
        /// Firebaseの認証に必要なカスタムトークンを発行します。
        /// </summary>
        /// <returns>FirebaseCustomToken</returns>
        [Post("/auth/firebase/custom-token")]
        Task<ApiResponse<FirebaseCustomToken>> PostAuthFirebaseCustomToken(CancellationToken token = default);

        /// <summary>
        /// ユーザー情報を取得する
        /// ログインしているユーザーの情報を取得します。
        /// </summary>
        /// <returns>User</returns>
        [Get("/user")]
        Task<ApiResponse<User>> GetUser(CancellationToken token = default);
    }
}

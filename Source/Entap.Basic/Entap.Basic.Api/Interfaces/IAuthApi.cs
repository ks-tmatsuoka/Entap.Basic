using System;
using System.Threading;
using System.Threading.Tasks;

namespace Entap.Basic.Api
{
    /// <summary>
    /// 認証周りのAPI
    /// https://github.com/entap/laravel-template/blob/main/reference/Auth.v1.yaml
    /// </summary>
    public interface IAuthApi
    {
        /// <summary>
        /// Firebase連携を登録する
        /// ログインしているユーザーにFirebaseアカウントを紐付けます。すでに紐づいている場合は何もしません。
        /// </summary>
        /// <param name="request">リクエストパラメータ</param>
        /// [Post("/auth/firebase/user")]
        Task PostAuthFirebaseUser(FirebaseIdToken request, CancellationToken token = default);

        /// <summary>
        /// [Delete("/auth/firebase/user")]
        /// Firebase連携を解除する
        /// ログインしているユーザーに紐づいているFirebaseの情報を削除します。（サーバー上のみ）
        /// </summary>
        /// [Delete("/auth/firebase/user")]
        Task DeleteAuthFirebaseUser(CancellationToken token = default);

        /// <summary>
        /// アクセストークンを発行する
        /// FirebaseのIDトークンから、サーバーのアクセストークンを発行します。
        /// 該当するユーザーが見つからない場合は、新たに作成します。
        /// </summary>
        /// <param name="request">FirebaseIdToken</param>
        /// <returns>ServerAccessToken</returns>
        /// [Post("/auth/firebase/token")]
        Task<ServerAccessToken> PostAuthFirebaseToken(FirebaseIdToken request, CancellationToken token = default);

        /// <summary>
        /// LINE連携を解除する
        /// ログインしているユーザーに紐づいているLINEの情報を削除します。（サーバー上のみ）
        /// </summary>
        /// [Delete("/auth/line/user")]
        Task DeleteAuthLineUser(CancellationToken token = default);

        /// <summary>
        /// LINE連携を登録する
        /// ログインしているユーザーにLINEのアカウントを紐付けます。
        /// すでに紐づいている場合は何もしません。
        /// </summary>
        /// <param name="request">CustomAuthToken</param>
        /// [Post("/auth/line/user")]
        Task PostAuthLineUser(CustomAuthToken request, CancellationToken token = default);

        /// <summary>
        /// アクセストークンを発行する
        /// LINEのIDトークンまたはアクセストークンから、サーバーのアクセストークンを発行します。
        /// 該当するユーザーが見つからない場合は、新たに作成します。
        /// </summary>
        /// <param name="request"></param>
        /// <returns>ServerAccessToken</returns>
        /// [Post("/auth/line/token")]
        Task<ServerAccessToken> PostAuthLineToken(CustomAuthToken request, CancellationToken token = default);

        /// <summary>
        /// [post]
        /// カスタムトークンを発行する
        /// Firebaseの認証に必要なカスタムトークンを発行します。
        /// </summary>
        /// <returns>FirebaseCustomToken</returns>
        /// [Post("/auth/firebase/custom-token")]
        Task<FirebaseCustomToken> PostAuthFirebaseCustomToken(CancellationToken token = default);

        /// <summary>
        /// ユーザー情報を取得する
        /// ログインしているユーザーの情報を取得します。
        /// </summary>
        /// <returns>User</returns>
        /// [Get("/user")]
        Task<User> GetUser(CancellationToken token = default);

        /// <summary>
        /// ユーザー情報を削除する
        /// ログインしているユーザーの情報を削除します。
        /// </summary>
        /// [Delete("/user")]
        Task DeleteUser(CancellationToken token = default);
    }
}

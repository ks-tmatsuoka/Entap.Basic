using System;
using System.Threading;
using System.Threading.Tasks;
using Entap.Basic.Api;
using Refit;

namespace SHIRO.CO
{
    public interface IBasicAuthApi : IAuthApi
    {
        /// <summary>
        /// Firebase連携を登録する
        /// ログインしているユーザーにFirebaseアカウントを紐付けます。すでに紐づいている場合は何もしません。
        /// </summary>
        /// <param name="request">リクエストパラメータ</param>
        [Post("/auth/firebase/user")]
        new Task PostAuthFirebaseUser(FirebaseIdToken request, CancellationToken token = default);

        /// <summary>
        /// [Delete("/auth/firebase/user")]
        /// Firebase連携を解除する
        /// ログインしているユーザーに紐づいているFirebaseの情報を削除します。（サーバー上のみ）
        /// </summary>
        [Delete("/auth/firebase/user")]
        new Task DeleteAuthFirebaseUser(CancellationToken token = default);

        /// <summary>
        /// アクセストークンを発行する
        /// FirebaseのIDトークンから、サーバーのアクセストークンを発行します。
        /// 該当するユーザーが見つからない場合は、新たに作成します。
        /// </summary>
        /// <param name="request">FirebaseIdToken</param>
        /// <returns>ServerAccessToken</returns>
        [Post("/auth/firebase/token")]
        new Task<ServerAccessToken> PostAuthFirebaseToken(FirebaseIdToken request, CancellationToken token = default);

        /// <summary>
        /// LINE連携を解除する
        /// ログインしているユーザーに紐づいているLINEの情報を削除します。（サーバー上のみ）
        /// </summary>
        [Delete("/auth/line/user")]
        new Task DeleteAuthLineUser(CancellationToken token = default);

        /// <summary>
        /// LINE連携を登録する
        /// ログインしているユーザーにLINEのアカウントを紐付けます。
        /// すでに紐づいている場合は何もしません。
        /// </summary>
        /// <param name="request">CustomAuthToken</param>
        [Post("/auth/line/user")]
        new Task PostAuthLineUser(CustomAuthToken request, CancellationToken token = default);

        /// <summary>
        /// アクセストークンを発行する
        /// LINEのIDトークンまたはアクセストークンから、サーバーのアクセストークンを発行します。
        /// 該当するユーザーが見つからない場合は、新たに作成します。
        /// </summary>
        /// <param name="request"></param>
        /// <returns>ServerAccessToken</returns>
        [Post("/auth/line/token")]
        new Task<ServerAccessToken> PostAuthLineToken(CustomAuthToken request, CancellationToken token = default);

        /// <summary>
        /// [post]
        /// カスタムトークンを発行する
        /// Firebaseの認証に必要なカスタムトークンを発行します。
        /// </summary>
        /// <returns>FirebaseCustomToken</returns>
        [Post("/auth/firebase/custom-token")]
        new Task<FirebaseCustomToken> PostAuthFirebaseCustomToken(CancellationToken token = default);

        /// <summary>
        /// ユーザー情報を取得する
        /// ログインしているユーザーの情報を取得します。
        /// </summary>
        /// <returns>User</returns>
        [Get("/user")]
        new Task<User> GetUser(CancellationToken token = default);
    }
}

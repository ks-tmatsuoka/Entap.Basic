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
        /// [post]/auth/firebase/user
        /// Firebase連携を登録する
        /// ログインしているユーザーにFirebaseアカウントを紐付けます。すでに紐づいている場合は何もしません。
        /// </summary>
        /// <param name="request">リクエストパラメータ</param>
        Task PostAuthFirebaseUser(FirebaseIdToken request, CancellationToken token = default);

        /// <summary>
        /// [delete]/auth/firebase/user
        /// Firebase連携を解除する
        /// ログインしているユーザーに紐づいているFirebaseの情報を削除します。（サーバー上のみ）
        /// </summary>
        Task DeleteAuthFirebaseUser(CancellationToken token = default);

        /// <summary>
        /// [post]/auth/firebase/token
        /// アクセストークンを発行する
        /// FirebaseのIDトークンから、サーバーのアクセストークンを発行します。
        /// 該当するユーザーが見つからない場合は、新たに作成します。
        /// </summary>
        /// <param name="request">FirebaseIdToken</param>
        /// <returns>ServerAccessToken</returns>
        Task<ServerAccessToken> PostAuthFirebaseToken(FirebaseIdToken request, CancellationToken token = default);

        /// <summary>
        /// [delete]/auth/line/user
        /// LINE連携を解除する
        /// ログインしているユーザーに紐づいているLINEの情報を削除します。（サーバー上のみ）
        /// </summary>
        Task DeleteAuthLineUser(CancellationToken token = default);

        /// <summary>
        /// [post]/auth/line/user
        /// LINE連携を登録する
        /// ログインしているユーザーにLINEのアカウントを紐付けます。
        /// すでに紐づいている場合は何もしません。
        /// </summary>
        /// <param name="request">CustomAuthToken</param>
        Task PostAuthLineUser(CustomAuthToken request, CancellationToken token = default);

        /// <summary>
        /// [post]/auth/line/token
        /// アクセストークンを発行する
        /// LINEのIDトークンまたはアクセストークンから、サーバーのアクセストークンを発行します。
        /// 該当するユーザーが見つからない場合は、新たに作成します。
        /// </summary>
        /// <param name="request"></param>
        /// <returns>ServerAccessToken</returns>
        Task<ServerAccessToken> PostAuthLineToken(CustomAuthToken request, CancellationToken token = default);

        /// <summary>
        /// [post]/auth/firebase/custom-token
        /// カスタムトークンを発行する
        /// Firebaseの認証に必要なカスタムトークンを発行します。
        /// </summary>
        /// <returns>FirebaseCustomToken</returns>
        Task<FirebaseCustomToken> PostAuthFirebaseCustomToken(CancellationToken token = default);

        /// <summary>
        /// [get]/user
        /// ユーザー情報を取得する
        /// ログインしているユーザーの情報を取得します。
        /// </summary>
        /// <returns>User</returns>
        Task<User> GetUser(CancellationToken token = default);
    }
}

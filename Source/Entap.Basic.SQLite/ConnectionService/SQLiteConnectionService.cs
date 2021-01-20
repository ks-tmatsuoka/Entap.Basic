using System;
using SQLite;

namespace Entap.Basic.SQLite
{
    /// <summary>
    /// SQLiteのConnectionを管理する
    /// </summary>
    public static class SQLiteConnectionManager
    {
        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="connectionService">SQLiteConnectionService</param>
        static ISQLiteConnectionService _connectionService;
        public static void Init(ISQLiteConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        /// <summary>
        /// データベースへの同期接続
        /// </summary>
        public static SQLiteConnection Connection => _connectionLazyInitializer.Value;
        static Lazy<SQLiteConnection> _connectionLazyInitializer =
            new Lazy<SQLiteConnection>(() => _connectionService.GetConnection());

        /// <summary>
        /// データベースへの非同期接続
        /// </summary>
        public static SQLiteAsyncConnection AsyncConnection => _asyncConnectionLazyInitializer.Value;
        static Lazy<SQLiteAsyncConnection> _asyncConnectionLazyInitializer =
            new Lazy<SQLiteAsyncConnection>(() => _connectionService.GetAsyncConnection());
    }
}

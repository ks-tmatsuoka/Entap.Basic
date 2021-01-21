using System;
using SQLite;

namespace Entap.Basic.SQLite
{
    /// <summary>
    /// SQLiteのコネクション取得インターフェース
    /// </summary>
    public interface ISQLiteConnectionService
    {
        SQLiteConnection GetConnection();
        SQLiteAsyncConnection GetAsyncConnection();
    }
}

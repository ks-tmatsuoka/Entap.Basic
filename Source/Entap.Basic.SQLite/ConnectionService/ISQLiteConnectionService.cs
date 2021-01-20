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
    public class TableBase
    {
        public TableBase()
        {
        }

        /// <summary>
        /// Id
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int AppId { get; set; }

        /// <summary>
        /// 作成日時
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}

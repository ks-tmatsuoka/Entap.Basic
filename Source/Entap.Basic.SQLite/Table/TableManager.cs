using System;
using SQLite;
namespace Entap.Basic.SQLite
{
    /// <summary>
    /// テーブルの同期制御処理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TableManager<T> where T : ITableBase, new ()
    {
        public static TableManager<T> Current => LazyTableManagerInitializer.Value;
        static readonly Lazy<TableManager<T>> LazyTableManagerInitializer = new Lazy<TableManager<T>>(() => new TableManager<T>(SQLiteConnectionManager.AsyncConnection));

        public TableManager(SQLiteAsyncConnection asyncConnection)
        {
            AsyncConnection = asyncConnection;
            Connection.CreateTable<T>();
            Connection.TableChanged += OnTableChanged;
        }

        private void OnTableChanged(object sender, NotifyTableChangedEventArgs e)
        {
            if (e.Table.TableName != typeof(T).Name) return;
            TableChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// データベースへの非同期接続
        /// </summary>
        public SQLiteAsyncConnection AsyncConnection { get; private set; }

        /// <summary>
        /// データベースへの同期接続
        /// </summary>
        public SQLiteConnection Connection => AsyncConnection.GetConnection();

        /// <summary>
        /// テーブルの変更イベント
        /// </summary>
        public event EventHandler<NotifyTableChangedEventArgs> TableChanged;
    }
}

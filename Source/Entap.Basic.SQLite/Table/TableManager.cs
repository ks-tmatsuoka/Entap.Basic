using System;
using System.Collections.Generic;
using System.Linq;
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
        static readonly Lazy<TableManager<T>> LazyTableManagerInitializer = new Lazy<TableManager<T>>(() => new TableManager<T>(SQLiteConnectionManager.Connection));

        public TableManager(SQLiteConnection connection)
        {
            Connection = connection;
            Connection.CreateTable<T>();
            Connection.TableChanged += OnTableChanged;
        }

        private void OnTableChanged(object sender, NotifyTableChangedEventArgs e)
        {
            if (e.Table.TableName != typeof(T).Name) return;
            TableChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// データベースへの同期接続
        /// </summary>
        public SQLiteConnection Connection { get; private set; }

        /// <summary>
        /// テーブルの変更イベント
        /// </summary>
        public event EventHandler<NotifyTableChangedEventArgs> TableChanged;

        /// <summary>
        /// アイテムが存在するか判断する
        /// </summary>
        /// <returns>要素が含まれている場合は true。それ以外の場合は false</returns>
        public bool Any() => Connection.Table<T>().FirstOrDefault() is not null;

        /// <summary>
        /// 条件を満たすアイテムが存在するか判断する
        /// </summary>
        /// <param name="predicate">条件を満たしているかどうかをテストする関数</param>
        /// <returns>要素が含まれている場合は true。それ以外の場合は false</returns>
        public bool Any(Func<T, bool> predicate) => Connection.Table<T>().FirstOrDefault(predicate) is not null;

        /// <summary>
        /// アイテム数を返す
        /// </summary>
        public int Count() => Connection.Table<T>().Count();

        /// <summary>
        /// 条件を満たすアイテム数を返す
        /// </summary>
        public int Count(Func<T, bool> predicate) => Connection.Table<T>().Count(predicate);

        #nullable enable
        /// <summary>
        /// 指定したIDのアイテムを取得する
        /// </summary>
        /// <param name="appId">Id</param>
        /// <returns>指定したIdのアイテム（アイテムが存在しない場合はNull）</returns>
        public T? Get(int appId) => Connection.Find<T>(appId);
        #nullable disable

        /// <summary>
        /// 全てのアイテムを返す
        /// </summary>
        /// <returns>全てのアイテム</returns>
        public IEnumerable<T> GetAll() => Connection.Table<T>();

        /// <summary>
        /// Idの昇順でソートしたアイテムを取得する
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <returns>AppIdでソートしたアイテム</returns>
        public IEnumerable<T> OrderBy()
            => Connection.Table<T>().OrderBy(o => o.AppId);

        /// <summary>
        /// 任意のキーの昇順でソートしたアイテムを取得する
        /// </summary>
        /// <typeparam name="U">keySelector によって返されるキーの型</typeparam>
        /// <param name="keySelector">アイテムからキーを抽出する関数</param>
        /// <returns>任意のキーでソートしたアイテム</returns>
        public IEnumerable<T> OrderBy<U>(Func<T, U> keySelector)
            => Connection.Table<T>().OrderBy(keySelector);

        /// <summary>
        /// Idの降順でソートしたアイテムを取得する
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <returns>AppIdでソートしたアイテム</returns>
        public IEnumerable<T> OrderByDescending()
            => Connection.Table<T>().OrderByDescending(o => o.AppId);

        /// <summary>
        /// 任意のキーの降順でソートしたアイテムを取得する
        /// </summary>
        /// <typeparam name="U">keySelector によって返されるキーの型</typeparam>
        /// <param name="keySelector">アイテムからキーを抽出する関数</param>
        /// <returns>任意のキーでソートしたアイテム</returns>
        public IEnumerable<T> OrderByDescending<U>(Func<T, U> keySelector)
            => Connection.Table<T>().OrderByDescending(keySelector);

        /// <summary>
        /// 指定したアイテムを保存する
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <returns>保存に成功時は true。それ以外の場合は false</returns>
        public bool Save(T item)
        {
            if (item.AppId != 0)
                return Update(item);
            else
                return Insert(item);
        }

        /// <summary>
        /// 指定したアイテムを追加する
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <returns>追加に成功時は true。それ以外の場合は false</returns>
        public bool Insert(T item)
        {
            var date = DateTime.Now;
            item.CreateAt = date;
            item.UpdatedAt = date;
            var rowsAffected = Connection.Insert(item);
            return rowsAffected > 0;
        }

        /// <summary>
        /// 指定したアイテムを更新する
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <returns>更新に成功時は true。それ以外の場合は false</returns>
        public bool Update(T item)
        {
            item.UpdatedAt = DateTime.Now;
            var rowsAffected = Connection.Update(item);
            return rowsAffected > 0;
        }

        /// <summary>
        /// 指定したアイテムを削除する
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <returns>削除に成功時は true。それ以外の場合は false</returns>
        public bool Delete(T item)
        {
            var rowsAffected = Connection.Delete(item);
            return rowsAffected > 0;
        }

        /// <summary>
        /// 指定したIdのアイテムを削除する
        /// </summary>
        /// <param name="appId">Id</param>
        /// <returns>削除に成功時は true。それ以外の場合は false</returns>
        public bool Delete(int appId)
        {
            var rowsAffected = Connection.Delete(appId);
            return rowsAffected > 0;
        }

        /// <summary>
        /// 指定した複数のアイテムを一括追加する
        /// </summary>
        /// <param name="items">複数のアイテム</param>
        /// <param name="runInTransaction">トランザクションとして実行する場合は true。それ以外の場合は false</param>
        /// <returns>全てのアイテムの追加に成功時は true。それ以外の場合は false</returns>
        public bool InsertAll(IEnumerable<T> items, bool runInTransaction = true)
        {
            var date = DateTime.Now;
            var datedItems = items.Select((arg) =>
            {
                arg.CreateAt = date;
                arg.UpdatedAt = date;
                return arg;
            });
            var rowsAffected = Connection.InsertAll(datedItems, runInTransaction);
            return rowsAffected == items.Count();
        }

        /// <summary>
        /// 指定した複数のアイテムを一括更新する
        /// </summary>
        /// <param name="items">複数のアイテム</param>
        /// <param name="runInTransaction">トランザクションとして実行する場合は true。それ以外の場合は false</param>
        /// <returns>全てのアイテムの更新に成功時は true。それ以外の場合は false</returns>
        public bool UpdateAll(IEnumerable<T> items, bool runInTransaction = true)
        {
            var date = DateTime.Now;
            var datedItems = items.Select((arg) =>
            {
                arg.UpdatedAt = date;
                return arg;
            });
            var rowsAffected = Connection.UpdateAll(datedItems, runInTransaction);
            return rowsAffected == items.Count();
        }

        /// <summary>
        /// アイテムを一括削除する
        /// </summary>
        /// <returns>一括削除に成功時は true。それ以外の場合は false</returns>
        public bool DeleteAll()
        {
            var rowsAffected = Connection.DeleteAll<T>();
            return rowsAffected > 0;
        }

        /// <summary>
        /// テーブルを削除する
        /// </summary>
        /// <returns>テーブルの削除に成功時は true。それ以外の場合は false</returns>
        public bool DropTable()
        {
            var rowsAffected = Connection.DropTable<T>();
            return rowsAffected > 0;
        }
    }
}

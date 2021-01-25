using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace Entap.Basic.SQLite
{
    /// <summary>
    /// SQLiteConnectionの拡張メソッド
    /// 既存メソッドをオーバーライドできないため、メソッドの重複回避のためにメソッドの接頭語として"Ex"を付加する。
    /// </summary>
    public static class SQLiteConnectionExtensions
    {
        /// <summary>
        /// アイテムが存在するか判断する
        /// </summary>
        /// <returns>要素が含まれている場合は true。それ以外の場合は false</returns>
        public static bool ExAny<T>(this SQLiteConnection connection) where T : ITableBase, new()
            => connection.Table<T>().FirstOrDefault() is not null;

        /// <summary>
        /// 条件を満たすアイテムが存在するか判断する
        /// </summary>
        /// <param name="predicate">条件を満たしているかどうかをテストする関数</param>
        /// <returns>要素が含まれている場合は true。それ以外の場合は false</returns>
        public static bool ExAny<T>(this SQLiteConnection connection, Func<T, bool> predicate) where T : ITableBase, new()
            => connection.Table<T>().FirstOrDefault(predicate) is not null;

        /// <summary>
        /// アイテム数を返す
        /// </summary>
        public static int ExCount<T>(this SQLiteConnection connection) where T : ITableBase, new()
            => connection.Table<T>().Count();

        /// <summary>
        /// 条件を満たすアイテム数を返す
        /// </summary>
        public static int ExCount<T>(this SQLiteConnection connection, Func<T, bool> predicate) where T : ITableBase, new()
            => connection.Table<T>().Count(predicate);

#nullable enable
        /// <summary>
        /// 指定したIDのアイテムを取得する
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>指定したIdのアイテム（アイテムが存在しない場合はNull）</returns>
        public static T? ExGet<T>(this SQLiteConnection connection, int id) where T : ITableBase, new()
            => connection.Find<T>(id);
#nullable disable

        /// <summary>
        /// 全てのアイテムを返す
        /// </summary>
        /// <returns>全てのアイテム</returns>
        public static IEnumerable<T> ExGetAll<T>(this SQLiteConnection connection) where T : ITableBase, new()
            => connection.Table<T>();

        /// <summary>
        /// Idの昇順でソートしたアイテムを取得する
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <returns>AppIdでソートしたアイテム</returns>
        public static IOrderedEnumerable<T> ExOrderBy<T>(this SQLiteConnection connection) where T : ITableBase, new()
            => connection.ExOrderBy<T, int>(o => o.Id);

        /// <summary>
        /// 任意のキーの昇順でソートしたアイテムを取得する
        /// </summary>
        /// <typeparam name="U">keySelector によって返されるキーの型</typeparam>
        /// <param name="keySelector">アイテムからキーを抽出する関数</param>
        /// <returns>任意のキーでソートしたアイテム</returns>
        public static IOrderedEnumerable<T> ExOrderBy<T, U>(this SQLiteConnection connection, Func<T, U> keySelector) where T : ITableBase, new()
            => connection.Table<T>().OrderBy(keySelector);

        /// <summary>
        /// 任意のキーの昇順でソートしたアイテムを取得する
        /// </summary>
        /// <typeparam name="U">keySelector によって返されるキーの型</typeparam>
        /// <param name="keySelector">アイテムからキーを抽出する関数</param>
        /// <param name="comparer">キーを比較する IComparer<T></param>
        /// <returns>任意のキーでソートしたアイテム</returns>
        public static IOrderedEnumerable<T> ExOrderBy<T, U>(this SQLiteConnection connection, Func<T, U> keySelector, IComparer<U> comparer) where T : ITableBase, new()
            => connection.Table<T>().OrderBy(keySelector, comparer);

        /// <summary>
        /// Idの降順でソートしたアイテムを取得する
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <returns>AppIdでソートしたアイテム</returns>
        public static IOrderedEnumerable<T> ExOrderByDescending<T>(this SQLiteConnection connection) where T : ITableBase, new()
            => connection.ExOrderByDescending<T, int>(o => o.Id);

        /// <summary>
        /// 任意のキーの降順でソートしたアイテムを取得する
        /// </summary>
        /// <typeparam name="U">keySelector によって返されるキーの型</typeparam>
        /// <param name="keySelector">アイテムからキーを抽出する関数</param>
        /// <returns>任意のキーでソートしたアイテム</returns>
        public static IOrderedEnumerable<T> ExOrderByDescending<T, U>(this SQLiteConnection connection, Func<T, U> keySelector) where T : ITableBase, new()
            => connection.Table<T>().OrderByDescending(keySelector);

        /// <summary>
        /// 任意のキーの降順でソートしたアイテムを取得する
        /// </summary>
        /// <typeparam name="U">keySelector によって返されるキーの型</typeparam>
        /// <param name="keySelector">アイテムからキーを抽出する関数</param>
        /// <param name="comparer">キーを比較する IComparer<T></param>
        /// <returns>任意のキーでソートしたアイテム</returns>
        public static IOrderedEnumerable<T> ExOrderByDescending<T, U>(this SQLiteConnection connection, Func<T, U> keySelector, IComparer<U> comparer) where T : ITableBase, new()
            => connection.Table<T>().OrderByDescending(keySelector, comparer);

        /// <summary>
        /// 指定したアイテムを保存する
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <returns>保存に成功時は true。それ以外の場合は false</returns>
        public static bool ExSave<T>(this SQLiteConnection connection, T item) where T : ITableBase, new()
        {
            if (item.Id != 0)
                return connection.ExUpdate<T>(item);
            else
                return connection.ExInsert<T>(item);
        }

        /// <summary>
        /// 指定したアイテムを追加する
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <returns>追加に成功時は true。それ以外の場合は false</returns>
        public static bool ExInsert<T>(this SQLiteConnection connection, T item) where T : ITableBase, new()
        {
            var date = DateTime.Now;
            item.CreateAt = date;
            item.UpdatedAt = date;
            var rowsAffected = connection.Insert(item);
            return rowsAffected > 0;
        }

        /// <summary>
        /// 指定したアイテムを更新する
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <returns>更新に成功時は true。それ以外の場合は false</returns>
        public static bool ExUpdate<T>(this SQLiteConnection connection, T item) where T : ITableBase, new()
        {
            item.UpdatedAt = DateTime.Now;
            var rowsAffected = connection.Update(item);
            return rowsAffected > 0;
        }

        /// <summary>
        /// 指定したアイテムを削除する
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <returns>削除に成功時は true。それ以外の場合は false</returns>
        public static bool ExDelete<T>(this SQLiteConnection connection, T item) where T : ITableBase, new()
        {
            var rowsAffected = connection.Delete(item);
            return rowsAffected > 0;
        }

        /// <summary>
        /// 指定したIdのアイテムを削除する
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>削除に成功時は true。それ以外の場合は false</returns>
        public static bool ExDelete<T>(this SQLiteConnection connection, int id) where T : ITableBase, new()
        {
            var rowsAffected = connection.Delete<T>(id);
            return rowsAffected > 0;
        }

        /// <summary>
        /// 指定した複数のアイテムを一括追加する
        /// </summary>
        /// <param name="items">複数のアイテム</param>
        /// <param name="runInTransaction">トランザクションとして実行する場合は true。それ以外の場合は false</param>
        /// <returns>全てのアイテムの追加に成功時は true。それ以外の場合は false</returns>
        public static bool ExInsertAll<T>(this SQLiteConnection connection, IEnumerable<T> items, bool runInTransaction = true) where T : ITableBase, new()
        {
            var date = DateTime.Now;
            var datedItems = items.Select((arg) =>
            {
                arg.CreateAt = date;
                arg.UpdatedAt = date;
                return arg;
            });
            var rowsAffected = connection.InsertAll(datedItems, runInTransaction);
            return rowsAffected == items.Count();
        }

        /// <summary>
        /// 指定した複数のアイテムを一括更新する
        /// </summary>
        /// <param name="items">複数のアイテム</param>
        /// <param name="runInTransaction">トランザクションとして実行する場合は true。それ以外の場合は false</param>
        /// <returns>全てのアイテムの更新に成功時は true。それ以外の場合は false</returns>
        public static bool ExUpdateAll<T>(this SQLiteConnection connection, IEnumerable<T> items, bool runInTransaction = true) where T : ITableBase, new()
        {
            var date = DateTime.Now;
            var datedItems = items.Select((arg) =>
            {
                arg.UpdatedAt = date;
                return arg;
            });
            var rowsAffected = connection.UpdateAll(datedItems, runInTransaction);
            return rowsAffected == items.Count();
        }

        /// <summary>
        /// アイテムを一括削除する
        /// </summary>
        /// <returns>一括削除に成功時は true。それ以外の場合は false</returns>
        public static bool ExDeleteAll<T>(this SQLiteConnection connection) where T : ITableBase, new()
        {
            var rowsAffected = connection.DeleteAll<T>();
            return rowsAffected > 0;
        }

        /// <summary>
        /// テーブルを削除する
        /// </summary>
        /// <returns>テーブルの削除に成功時は true。それ以外の場合は false</returns>
        public static bool ExDropTable<T>(this SQLiteConnection connection) where T : ITableBase, new()
        {
            var rowsAffected = connection.DropTable<T>();
            return rowsAffected > 0;
        }

        /// <summary>
        /// 指定したクエリを実行する
        /// </summary>
        /// <param name="query">SQLクエリ</param>
        /// <param name="args">'?'を置換する引数</param>
        /// <returns>実行結果</returns>
        public static List<T> ExExecuteQuery<T>(this SQLiteConnection connection, string query, params object[] args) where T : ITableBase, new()
            => connection.Query<T>(query, args);
    }
}

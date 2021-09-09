using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SQLite;

namespace Entap.Basic.SQLite
{
    /// <summary>
    /// SQLiteAsyncconnectionの拡張メソッド
    /// 既存メソッドをオーバーライドできないため、メソッドの重複回避のためにメソッドの接頭語として"Ex"を付加する。
    /// </summary>
    public static class SQLiteAsyncconnectionExtensions
    {
        /// <summary>
        /// アイテムが存在するか判断する
        /// </summary>
        /// <returns>要素が含まれている場合は true。それ以外の場合は false</returns>
        public static async Task<bool> ExAnyAsync<T>(this SQLiteAsyncConnection connection) where T : ITableBase, new()
        {
            var item = await connection.Table<T>().FirstOrDefaultAsync();
            return item is not null;
        }

        /// <summary>
        /// 条件を満たすアイテムが存在するか判断する
        /// </summary>
        /// <param name="predicate">条件を満たしているかどうかをテストする関数</param>
        /// <returns>要素が含まれている場合は true。それ以外の場合は false</returns>
        public static async Task<bool> ExAnyAsync<T>(this SQLiteAsyncConnection connection, Expression<Func<T, bool>> predicate) where T : ITableBase, new()
        {
            var item = await connection.Table<T>().FirstOrDefaultAsync(predicate);
            return item is not null;
        }

        /// <summary>
        /// アイテム数を返す
        /// </summary>
        public static Task<int> ExCountAsync<T>(this SQLiteAsyncConnection connection) where T : ITableBase, new()
            => connection.Table<T>().CountAsync();

        /// <summary>
        /// 条件を満たすアイテム数を返す
        /// </summary>
        public static Task<int> ExCountAsync<T>(this SQLiteAsyncConnection connection, Expression<Func<T, bool>> predicate) where T : ITableBase, new()
            => connection.Table<T>().CountAsync(predicate);

        /// <summary>
        /// 指定したIDのアイテムを取得する
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>指定したIdのアイテム（アイテムが存在しない場合はNull）</returns>
        public static Task<T> ExGetAsync<T>(this SQLiteAsyncConnection connection, int id) where T : ITableBase, new()
            => connection.FindAsync<T>(id);

        /// <summary>
        /// 全てのアイテムを返す
        /// </summary>
        /// <returns>全てのアイテム</returns>
        public static Task<List<T>> ExGetAllAsync<T>(this SQLiteAsyncConnection connection) where T : ITableBase, new()
            => connection.Table<T>().ToListAsync();

        /// <summary>
        /// 任意のキーの昇順でソートしたアイテムを取得する
        /// </summary>
        /// <typeparam name="U">keySelector によって返されるキーの型</typeparam>
        /// <param name="keySelector">アイテムからキーを抽出する関数</param>
        /// <returns>任意のキーでソートしたアイテム</returns>
        public static Task<List<T>> ExOrderByAsync<T, U>(this SQLiteAsyncConnection connection, Expression<Func<T, U>> keySelector) where T : ITableBase, new()
            => connection.Table<T>().OrderBy(keySelector).ToListAsync();

        /// <summary>
        /// 任意のキーの降順でソートしたアイテムを取得する
        /// </summary>
        /// <typeparam name="U">keySelector によって返されるキーの型</typeparam>
        /// <param name="keySelector">アイテムからキーを抽出する関数</param>
        /// <returns>任意のキーでソートしたアイテム</returns>
        public static Task<List<T>> ExOrderByDescending<T, U>(this SQLiteAsyncConnection connection, Expression<Func<T, U>> keySelector) where T : ITableBase, new()
            => connection.Table<T>().OrderByDescending(keySelector).ToListAsync();

        /// <summary>
        /// 指定したアイテムを保存する
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <returns>保存に成功時は true。それ以外の場合は false</returns>
        public static Task<bool> ExSaveAsync<T>(this SQLiteAsyncConnection connection, T item) where T : ITableBase, new()
        {
            if (item.Id != 0)
                return connection.ExUpdateAsync(item);
            else
                return connection.ExInsertAsync(item);
        }

        /// <summary>
        /// 指定したアイテムを追加する
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <returns>追加に成功時は true。それ以外の場合は false</returns>
        public static async Task<bool> ExInsertAsync<T>(this SQLiteAsyncConnection connection, T item) where T : ITableBase, new()
        {
            var date = DateTime.Now;
            item.CreateAt = date;
            item.UpdatedAt = date;
            var rowsAffected = await connection.InsertAsync(item);
            return rowsAffected > 0;
        }

        /// <summary>
        /// 指定したアイテムを更新する
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <returns>更新に成功時は true。それ以外の場合は false</returns>
        public static async Task<bool> ExUpdateAsync<T>(this SQLiteAsyncConnection connection, T item) where T : ITableBase, new()
        {
            item.UpdatedAt = DateTime.Now;
            var rowsAffected = await connection.UpdateAsync(item);
            return rowsAffected > 0;
        }

        /// <summary>
        /// 指定したアイテムを削除する
        /// </summary>
        /// <param name="item">アイテム</param>
        /// <returns>削除に成功時は true。それ以外の場合は false</returns>
        public static async Task<bool> ExDeleteAsync<T>(this SQLiteAsyncConnection connection, T item) where T : ITableBase, new()
        {
            var rowsAffected = await connection.DeleteAsync(item);
            return rowsAffected > 0;
        }

        /// <summary>
        /// 指定したIdのアイテムを削除する
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>削除に成功時は true。それ以外の場合は false</returns>
        public static async Task<bool> ExDeleteAsync<T>(this SQLiteAsyncConnection connection, int id) where T : ITableBase, new()
        {
            var rowsAffected = await connection.DeleteAsync<T>(id);
            return rowsAffected > 0;
        }

        /// <summary>
        /// 指定した複数のアイテムを一括追加する
        /// </summary>
        /// <param name="items">複数のアイテム</param>
        /// <param name="runInTransaction">トランザクションとして実行する場合は true。それ以外の場合は false</param>
        /// <returns>全てのアイテムの追加に成功時は true。それ以外の場合は false</returns>
        public static async Task<bool> ExInsertAllAsync<T>(this SQLiteAsyncConnection connection, IEnumerable<T> items, bool runInTransaction = true) where T : ITableBase, new()
        {
            var date = DateTime.Now;
            var datedItems = items.Select((arg) =>
            {
                arg.CreateAt = date;
                arg.UpdatedAt = date;
                return arg;
            });
            var rowsAffected = await connection.InsertAllAsync(datedItems, runInTransaction);
            return rowsAffected == items.Count();
        }

        /// <summary>
        /// 指定した複数のアイテムを一括更新する
        /// </summary>
        /// <param name="items">複数のアイテム</param>
        /// <param name="runInTransaction">トランザクションとして実行する場合は true。それ以外の場合は false</param>
        /// <returns>全てのアイテムの更新に成功時は true。それ以外の場合は false</returns>
        public static async Task<bool> ExUpdateAllAsync<T>(this SQLiteAsyncConnection connection, IEnumerable<T> items, bool runInTransaction = true) where T : ITableBase, new()
        {
            var date = DateTime.Now;
            var datedItems = items.Select((arg) =>
            {
                arg.UpdatedAt = date;
                return arg;
            });
            var rowsAffected = await connection.UpdateAllAsync(datedItems, runInTransaction);
            return rowsAffected == items.Count();
        }

        /// <summary>
        /// アイテムを一括削除する
        /// </summary>
        /// <returns>一括削除に成功時は true。それ以外の場合は false</returns>
        public static async Task<bool> ExDeleteAllAsync<T>(this SQLiteAsyncConnection connection) where T : ITableBase, new()
        {
            var rowsAffected = await connection.DeleteAllAsync<T>();
            return rowsAffected > 0;
        }

        /// <summary>
        /// テーブルを削除する
        /// </summary>
        /// <returns>テーブルの削除に成功時は true。それ以外の場合は false</returns>
        public static async Task<bool> ExDropTableAsync<T>(this SQLiteAsyncConnection connection) where T : ITableBase, new()
        {
            var rowsAffected = await connection.DropTableAsync<T>();
            return rowsAffected > 0;
        }

        /// <summary>
        /// 指定したクエリを実行する
        /// </summary>
        /// <param name="query">SQLクエリ</param>
        /// <param name="args">'?'を置換する引数</param>
        /// <returns>実行結果</returns>
        public static Task<List<T>> ExExecuteQueryAsync<T>(this SQLiteAsyncConnection connection, string query, params object[] args) where T : ITableBase, new()
            => connection.QueryAsync<T>(query, args);

    }
}
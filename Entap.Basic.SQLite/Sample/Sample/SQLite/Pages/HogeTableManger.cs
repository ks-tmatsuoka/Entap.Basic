using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entap.Basic.SQLite;
using SQLite;

namespace Sample
{
    public class HogeTableManger : TableManager<HogeTable>
    {
        public static new HogeTableManger Current => LazyTableManagerInitializer.Value;
        static readonly Lazy<HogeTableManger> LazyTableManagerInitializer = new Lazy<HogeTableManger>(() => new HogeTableManger(SQLiteConnectionManager.AsyncConnection));

        public HogeTableManger(SQLiteAsyncConnection asyncConnection) : base(asyncConnection)
        {
        }

        public async Task TestAsync()
        {
            await AsyncConnection.ExAnyAsync<HogeTable>();
            await AsyncConnection.ExAnyAsync<HogeTable>(hoge => hoge.Id > 2);

            await AsyncConnection.ExCountAsync<HogeTable>();
            await AsyncConnection.ExCountAsync<HogeTable>(hoge => hoge.Id > 2);

            await AsyncConnection.ExGetAsync<HogeTable>(1);
            await AsyncConnection.ExGetAllAsync<HogeTable>();

            await AsyncConnection.ExOrderByAsync<HogeTable, DateTime>(hoge => hoge.UpdatedAt);
            await AsyncConnection.ExOrderByDescending<HogeTable, DateTime>(hoge => hoge.UpdatedAt);

            var hoge1 = new HogeTable();
            await AsyncConnection.ExSaveAsync(hoge1);
            var hoge2 = new HogeTable();
            await AsyncConnection.ExInsertAsync(hoge2);
            await AsyncConnection.ExUpdateAsync(hoge2);
            await AsyncConnection.ExDeleteAsync<HogeTable>(hoge1.Id);
            await AsyncConnection.ExDeleteAsync(hoge2);

            var items = Enumerable.Range(0, 10).Select((arg) => new HogeTable());
            await AsyncConnection.ExInsertAllAsync(items);
            await AsyncConnection.ExUpdateAllAsync(items);
            await AsyncConnection.ExDeleteAllAsync<HogeTable>();
        }

        public Task<List<HogeTable>> GetAllAsync()
        {
            try
            {
                return AsyncConnection.ExGetAllAsync<HogeTable>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{nameof(DeleteAsync)} : {ex.Message}");
                return Task.FromResult<List<HogeTable>>(null);
            }
        }

        public Task<bool> InsertAllAsync(IEnumerable<HogeTable> items)
        {
            try
            {
                return AsyncConnection.ExInsertAllAsync(items);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{nameof(InsertAllAsync)} : {ex.Message}");
                return Task.FromResult(false);
            }
        }

        public Task<bool> UpdateAllAsync(IEnumerable<HogeTable> items)
        {
            try
            {
                return AsyncConnection.ExUpdateAllAsync(items);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{nameof(UpdateAllAsync)} : {ex.Message}");
                return Task.FromResult(false);
            }
        }

        public Task<bool> DeleteAllAsync()
        {
            try
            {
                return AsyncConnection.ExDeleteAllAsync<HogeTable>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{nameof(DeleteAllAsync)} : {ex.Message}");
                return Task.FromResult(false);
            }
        }

        public Task<bool> InsertAsync(HogeTable item)
        {
            try
            {
                return AsyncConnection.ExInsertAsync(item);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{nameof(InsertAsync)} : {ex.Message}");
                return Task.FromResult(false);
            }
        }

        public Task<bool> UpdateAsync(HogeTable item)
        {
            try
            {
                return AsyncConnection.ExUpdateAsync(item);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{nameof(UpdateAsync)} : {ex.Message}");
                return Task.FromResult(false);
            }
        }

        public Task<bool> DeleteAsync(HogeTable item)
        {
            try
            {
                return AsyncConnection.ExDeleteAsync<HogeTable>(item.Id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{nameof(DeleteAsync)} : {ex.Message}");
                return Task.FromResult(false);
            }
        }

    }
}

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Entap.Basic.SQLite;
using Xamarin.Forms;

namespace Sample
{
    public class SQLitePageViewModel : PageViewModelBase
    {
        public SQLitePageViewModel()
        {
            SQLiteConnectionManager.Connection.TableChanged += OnHogeTableChanged;
            LoadItems();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            SQLiteConnectionManager.Connection.TableChanged -= OnHogeTableChanged;
        }

        private void OnHogeTableChanged(object sender, SQLite.NotifyTableChangedEventArgs e)
        {
            if (e.Table.TableName != typeof(HogeTable).Name) return;
            LoadItems();
        }

        public Command InsertAllCommand => new Command(() =>
        {
            var items = Enumerable.Range(0, 10).Select((arg) => new HogeTable());
            var result = SQLiteConnectionManager.Connection.ExInsertAll<HogeTable>(items);
            System.Diagnostics.Debug.WriteLine($"InsertAll : {result}");
        });

        public Command UpdateAllCommand => new Command(() =>
        {
            var result = SQLiteConnectionManager.Connection.ExUpdateAll<HogeTable>(Items);
            System.Diagnostics.Debug.WriteLine($"UpdateAll : {result}");
        });

        public Command DeleteAllCommand => new Command(() =>
        {
            var result = SQLiteConnectionManager.Connection.ExDeleteAll<HogeTable>();
            System.Diagnostics.Debug.WriteLine($"DeleteAll : {result}");
        });

        public ObservableCollection<HogeTable> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
        ObservableCollection<HogeTable> _items = new ObservableCollection<HogeTable>();

        public Command AddCommand => new Command(() =>
        {
            var result = SQLiteConnectionManager.Connection.Insert(new HogeTable());
            System.Diagnostics.Debug.WriteLine($"Insert : {result}");
        });


        public ProcessCommand<HogeTable> SelectedCommand => new ProcessCommand<HogeTable>(async (arg) =>
        {
            var menu = await App.Current.MainPage.DisplayActionSheet(null, "キャンセル", null, "更新", "削除");
            switch (menu)
            {
                case "更新":
                    var result = SQLiteConnectionManager.Connection.ExUpdate(arg);
                    System.Diagnostics.Debug.WriteLine($"Update : {result}");
                    break;
                case "削除":
                    result = SQLiteConnectionManager.Connection.ExDelete<HogeTable>(arg.Id);
                    System.Diagnostics.Debug.WriteLine($"Delete : {result}");
                    break;
            }
        });

        void LoadItems()
        {
            Items = new ObservableCollection<HogeTable>(SQLiteConnectionManager.Connection.ExOrderByDescending<HogeTable>());
            //Items = new ObservableCollection<HogeTable>(SQLiteConnectionManager.Connection.OrderBy<HogeTable, DateTime>((arg) => arg.UpdatedAt));
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Entap.Basic.Forms;
using Xamarin.Forms;

namespace Sample
{
    public class SQLitePageViewModelAsync : PageViewModelBase
    {
        public SQLitePageViewModelAsync()
        {
            HogeTableManger.Current.TableChanged += OnHogeTableChanged;
            LoadItems();

            Task.Run(async () =>
            {
                await HogeTableManger.Current.TestAsync();
            }).ContinueWith((arg) =>
            {
                if (arg.IsFaulted)
                    System.Diagnostics.Debug.WriteLine(arg.Exception.Message);
            });
            
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            HogeTableManger.Current.TableChanged -= OnHogeTableChanged;
        }

        private void OnHogeTableChanged(object sender, SQLite.NotifyTableChangedEventArgs e)
        {
            LoadItems();
        }

        public ProcessCommand InsertAllCommand => new ProcessCommand(async () =>
        {
            var items = Enumerable.Range(0, 10).Select((arg) => new HogeTable());
            var result = await HogeTableManger.Current.InsertAllAsync(items);
            System.Diagnostics.Debug.WriteLine($"InsertAll : {result}");
        });

        public ProcessCommand UpdateAllCommand => new ProcessCommand(async () =>
        {
            var result = await HogeTableManger.Current.UpdateAllAsync(Items);
            System.Diagnostics.Debug.WriteLine($"UpdateAll : {result}");
        });

        public ProcessCommand DeleteAllCommand => new ProcessCommand(async () =>
        {
            var result = await HogeTableManger.Current.DeleteAllAsync();
            System.Diagnostics.Debug.WriteLine($"DeleteAll : {result}");
        });

        public ObservableCollection<HogeTable> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
        ObservableCollection<HogeTable> _items = new ObservableCollection<HogeTable>();

        public ProcessCommand AddCommand => new ProcessCommand(async () =>
        {
            var result = await HogeTableManger.Current.InsertAsync(new HogeTable());
            System.Diagnostics.Debug.WriteLine($"Insert : {result}");
        });


        public ProcessCommand<HogeTable> SelectedCommand => new ProcessCommand<HogeTable>(async (arg) =>
        {
            var menu = await App.Current.MainPage.DisplayActionSheet(null, "キャンセル", null, "更新", "削除");
            switch (menu)
            {
                case "更新":
                    var result = await HogeTableManger.Current.UpdateAsync(arg);
                    System.Diagnostics.Debug.WriteLine($"Update : {result}");
                    break;
                case "削除":
                    try
                    {
                        result = await HogeTableManger.Current.DeleteAsync(arg);
                        System.Diagnostics.Debug.WriteLine($"Delete : {result}");
                    }
                    catch(Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"ex : {ex.Message}");
                    }
                    break;
            }
        });

        void LoadItems()
        {
            var items = HogeTableManger.Current.GetAllAsync().Result;
            Items = new ObservableCollection<HogeTable>(items);
        }
    }
}

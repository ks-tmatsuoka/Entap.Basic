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
            TableManager<HogeTable>.Current.TableChanged += OnHogeTableChanged;
            LoadItems();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            TableManager<HogeTable>.Current.TableChanged -= OnHogeTableChanged;
        }

        private void OnHogeTableChanged(object sender, SQLite.NotifyTableChangedEventArgs e)
        {
            LoadItems();
        }

        public Command InsertAllCommand => new Command(() =>
        {
            var items = Enumerable.Range(0, 10).Select((arg) => new HogeTable());
            TableManager<HogeTable>.Current.InsertAll(items);
        });

        public Command DeleteAllCommand => new Command(() =>
        {
            TableManager<HogeTable>.Current.DeleteAll();
        });

        public ObservableCollection<HogeTable> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
        ObservableCollection<HogeTable> _items = new ObservableCollection<HogeTable>();

        public Command AddCommand => new Command(() =>
        {
            TableManager<HogeTable>.Current.Insert(new HogeTable());
        });


        public ProcessCommand<HogeTable> SelectedCommand => new ProcessCommand<HogeTable>(async (arg) =>
        {
            var menu = await App.Current.MainPage.DisplayActionSheet(null, "キャンセル", null, "更新", "削除");
            switch (menu)
            {
                case "更新":
                    TableManager<HogeTable>.Current.Update(arg);
                    break;
                case "削除":
                    TableManager<HogeTable>.Current.Delete(arg);
                    break;
            }
        });

        void LoadItems()
        {
            //Items = new ObservableCollection<HogeTable>(TableManager<HogeTable>.Current.GetAll());
            Items = new ObservableCollection<HogeTable>(TableManager<HogeTable>.Current.OrderBy((arg) => arg.UpdatedAt));
        }
    }
}

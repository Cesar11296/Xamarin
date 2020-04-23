using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ASOCLaViga
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageActivityBus : ContentPage
    {
        public PageActivityBus()
        {
            InitializeComponent();
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bbddASOC.db");
            var db = new SQLiteConnection(databasePath);
            List<Actividad> act = db.Query<Actividad>("SELECT * FROM Actividad where bus = true ");
            listView.ItemsSource = act;
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                PageActivityDetail pBus = new PageActivityDetail();
                pBus.BindingContext = e.SelectedItem;
                Navigation.PushModalAsync(pBus);
            }
        }
    }
}
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
    public partial class PageActivity : ContentPage
    {
        public PageActivity()
        {
            InitializeComponent();
            /*var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bbddASOC.db");
            var db = new SQLiteConnection(databasePath);
            List<Actividad> act = db.Query<Actividad>("SELECT * FROM Actividad where bus = ? ", "No");
            listView.ItemsSource = act;*/
            loadListAsync();
        }

        private async Task loadListAsync()
        {
            List<Actividad> list = await FirebaseHelper.GetActivities();
            var listFilter = from act in list
                             where (act.bus == "No")
                             select act;
            listView.ItemsSource = listFilter;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            listView.SelectedItem = null;
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                PageActivityDetail p = new PageActivityDetail();
                p.BindingContext = e.SelectedItem;
                Navigation.PushModalAsync(p);
            }
        }
    }
}
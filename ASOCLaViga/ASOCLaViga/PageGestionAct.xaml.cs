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
    public partial class PageGestionAct : ContentPage
    {
        public PageGestionAct()
        {
            InitializeComponent();
            LoadList();
            lw_Act.SelectedItem = null;
        }

        private void LoadList()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bbddASOC.db");
            var db = new SQLiteConnection(databasePath);
            Actividad act = (Actividad)this.BindingContext;
            List<Actividad> list = db.Query<Actividad>("SELECT * FROM actividad");
            lw_Act.ItemsSource = list;
        }

        private void lw_Act_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
            }
        }
    }
}
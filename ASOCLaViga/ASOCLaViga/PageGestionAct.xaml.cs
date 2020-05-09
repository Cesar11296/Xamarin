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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadList();
            lw_Act.SelectedItem = null;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            OnAppearing();
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
                OnAlert(e);
            }
        }

        async void OnAlert(SelectedItemChangedEventArgs e)
        {
            try
            {
                string answer = await DisplayActionSheet("¿Qué desea hacer?", "Cancel", null, "Modificar", "Eliminar");
                if (answer.Equals("Modificar"))
                {
                    PageChangeAct p = new PageChangeAct((Actividad)e.SelectedItem);
                    Navigation.PushModalAsync(p);
                }
                else if (answer.Equals("Eliminar"))
                {
                    var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bbddASOC.db");
                    var db = new SQLiteConnection(databasePath);
                    db.Delete(e.SelectedItem);
                    LoadList();
                }
            }
            catch (SystemException ex)
            {
                LoadList();
            }
        }

        private void bAdd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PageCreateAct());
        }
    }
}
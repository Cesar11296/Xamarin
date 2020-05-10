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
            LoadListAsync();
            lw_Act.SelectedItem = null;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadListAsync();
            lw_Act.SelectedItem = null;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            OnAppearing();
        }

        private async Task LoadListAsync()
        {
            /*var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bbddASOC.db");
            var db = new SQLiteConnection(databasePath);
            Actividad act = (Actividad)this.BindingContext;
            List<Actividad> list = db.Query<Actividad>("SELECT * FROM actividad");
            lw_Act.ItemsSource = list;*/
            Actividad act = (Actividad)this.BindingContext;
            List<Actividad> list = await FirebaseHelper.GetActivities();
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
                    int id = ((Actividad)e.SelectedItem).ID;
                    doDeleteAsync(id);
                    /*var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bbddASOC.db");
                    var db = new SQLiteConnection(databasePath);
                    db.Delete(e.SelectedItem);
                    LoadListAsync();*/
                }
            }
            catch (SystemException ex)
            {
                LoadListAsync();
            }
        }

        private async Task doDeleteAsync(int id)
        {
            await FirebaseHelper.DeleteActividad(id);
            LoadListAsync();
        }

        private void bAdd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PageCreateAct());
        }
    }
}
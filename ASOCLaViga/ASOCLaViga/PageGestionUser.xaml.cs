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
    public partial class PageGestionUser : ContentPage
    {
        public PageGestionUser()
        {
            InitializeComponent();
            LoadList();
        }

        private void LoadList()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bbddASOC.db");
            var db = new SQLiteConnection(databasePath);
            List<User> users = db.Query<User>("SELECT DISTINCT * FROM user where DNI != ? and type != 1", App.u.DNI);
            lw_AdminUser.ItemsSource = users;
        }

        private void lw_AdminUser_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            OnAlert(e);
        }

        async void OnAlert(SelectedItemChangedEventArgs e)
        {
            try { 
            string answer = await DisplayActionSheet("¿Qué desea hacer?", "Cancel", null, "Modificar", "Eliminar");
            if (answer.Equals("Modificar"))
            {
                PageChangeUser p = new PageChangeUser((User)e.SelectedItem);
                Navigation.PushModalAsync(p);
            }
            else if (answer.Equals("Eliminar"))
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bbddASOC.db");
                var db = new SQLiteConnection(databasePath);
                db.Delete(e.SelectedItem);
                LoadList();
            }
            }catch (SystemException ex)
            {
                LoadList();
            }
        }

        private void bAdd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PageCreateUser());
        }
    }
}
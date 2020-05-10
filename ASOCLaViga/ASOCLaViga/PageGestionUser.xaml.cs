using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
            LoadListAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadListAsync();
            lw_AdminUser.SelectedItem = null;
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
            List<User> users = db.Query<User>("SELECT DISTINCT * FROM user where DNI != ? and type != 1", App.u.DNI);*/
            List<User> users = await FirebaseHelper.GetAllUsers();
            var queryUser = from u in users
                            where (u.DNI != App.u.DNI && u.type != 1)
                            select u;
            lw_AdminUser.ItemsSource = queryUser;
        }

        private void lw_AdminUser_ItemSelected(object sender, SelectedItemChangedEventArgs e)
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
                    PageChangeUser p = new PageChangeUser((User)e.SelectedItem);
                    Navigation.PushModalAsync(p);
                }
                else if (answer.Equals("Eliminar"))
                {
                    /*var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bbddASOC.db");
                    var db = new SQLiteConnection(databasePath);
                    db.Delete(e.SelectedItem);*/
                    int id = ((User)e.SelectedItem).ID;
                    doDeleteAsync(id);
                    LoadListAsync();
                }
            }
            catch (SystemException ex)
            {
                LoadListAsync();
            }
        }

        private async Task doDeleteAsync(int id)
        {
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;
            try
            {
                await FirebaseHelper.DeleteUser(id);
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {e.Message}");
            }
            finally
            {
                tokenSource2.Dispose();
                LoadListAsync();
            }

        }

        private void bAdd_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PageCreateUser());
        }
    }
}
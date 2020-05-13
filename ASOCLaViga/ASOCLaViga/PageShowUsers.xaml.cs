using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ASOCLaViga
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageShowUsers : ContentPage
    {
        Actividad act;
        public PageShowUsers()
        {
            InitializeComponent();
        }

        public PageShowUsers(Actividad a)
        {
            InitializeComponent();
            act = a;
            LoadListAsync();
        }

        private async Task LoadListAsync()
        {
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;
            try
            {
                List<User> listUser = await FirebaseHelper.GetAllUsers();
                List<Apuntado> listAp = await FirebaseHelper.GetApuntados();
                List<Actividad> list = await FirebaseHelper.GetActivities();
                var listFilter = from ac in list
                                 join ap in listAp
                                 on ac.ID equals ap.IDAct
                                 join user in listUser
                                 on ap.IDUser equals user.ID
                                 where (ac.ID == act.ID)
                                 select new { Name = user.Name, Apellido = user.Apellido, Estado = ap.Estado, ID = ap.ID };
                lw_Users.ItemsSource = listFilter;
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {e.Message}");
            }
            finally
            {
                tokenSource2.Dispose();
            }
        }

        private void lw_Users_ItemSelected(object sender, SelectedItemChangedEventArgs e)
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
                int ID = 0;
                var stringProps = e.SelectedItem.GetType().GetProperties().Where(p => p.PropertyType == typeof(int));
                foreach (var prop in stringProps)
                {
                    ID = (int)prop.GetValue(e.SelectedItem);
                }
                string answer = await DisplayActionSheet("¿Qué desea hacer?", "Cancel", null, "Pago", "Eliminar de actividad");
                if (answer.Equals("Pago"))
                {
                    doPagoAsync(ID);
                    LoadListAsync();
                }
                else if (answer.Equals("Eliminar de actividad"))
                {
                    doDeleteAsync(ID);
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
                await FirebaseHelper.DeleteApuntado(id);
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

        private async Task doPagoAsync(int id)
        {
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;
            try
            {
                await FirebaseHelper.UpdateApuntado(id);
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
    }
}
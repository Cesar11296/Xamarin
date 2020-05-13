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
    public partial class ShowActivities : ContentPage
    {
        public ShowActivities()
        {
            InitializeComponent();
            loadListAsync();
            
        }

        private async Task loadListAsync()
        {
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;
            try
            {
                List<User> listUser = await FirebaseHelper.GetAllUsers();
                List<Apuntado> listAp = await FirebaseHelper.GetApuntados();
                List<Actividad> list = await FirebaseHelper.GetActivities();
                var listFilter = from act in list
                                 join ap in listAp
                                 on act.ID equals ap.IDAct
                                 join user in listUser
                                 on ap.IDUser equals user.ID
                                 where (user.DNI == App.u.DNI)
                                 select new { Titulo = act.Titulo, Fecha = act.Fecha, Estado = ap.Estado }; ;
                lw_Act.ItemsSource = listFilter;
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
    }
}
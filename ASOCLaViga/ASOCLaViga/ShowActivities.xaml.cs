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
    public partial class ShowActivities : ContentPage
    {
        public ShowActivities()
        {
            InitializeComponent();
            /*var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bbddASOC.db");
            var db = new SQLiteConnection(databasePath);
            List<Actividad> list = db.Query<Actividad>("SELECT * FROM actividad INNER JOIN apuntado ON apuntado.IDAct = actividad.ID INNER JOIN user ON user.ID = apuntado.IDUser WHERE user.DNI = ? ", App.u.DNI);

            lw_Act.ItemsSource = list;*/
            // dateEntrada.Date.ToString("ddd, dd MMMM"),

            loadListAsync();
            
        }

        private async Task loadListAsync()
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
                                         select act;
            lw_Act.ItemsSource = listFilter;
        }
    }
}
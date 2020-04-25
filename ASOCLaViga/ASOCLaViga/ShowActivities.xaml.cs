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
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bbddASOC.db");
            var db = new SQLiteConnection(databasePath);
            List<Actividad> list = db.Query<Actividad>("SELECT * FROM actividad INNER JOIN apuntado ON apuntado.IDAct = actividad.ID INNER JOIN user ON user.ID = apuntado.IDUser WHERE user.DNI = ? ", App.u.DNI);
            lw_Act.ItemsSource = list;
            // dateEntrada.Date.ToString("ddd, dd MMMM"),

        }
    }
}
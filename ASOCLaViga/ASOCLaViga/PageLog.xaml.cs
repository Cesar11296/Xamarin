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
    public partial class PageLog : ContentPage
    {
        public List<User> u;

        public PageLog()
        {
            InitializeComponent();
        }

        public PageLog(List<User> u)
        {
            InitializeComponent();
            this.u = u;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bbddASOC.db");
            var db = new SQLiteConnection(databasePath);
            string[] values = new string[] { entryDNI.Text, entryPass.Text };
            var list = db.Query<User>("SELECT DISTINCT * FROM user where DNI = ? and pass = ?", values);
            foreach (User us in list)
            {
                if (us.DNI == entryDNI.Text)
                {
                    App.u = us;
                    Navigation.PushModalAsync(new MainPage());
                }
                else
                {
                    DisplayAlert("Error", "Usuario no encontrado", "OK");
                }
            }
        }

    }
}
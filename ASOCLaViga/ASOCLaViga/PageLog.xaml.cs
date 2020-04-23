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
        bool bDNI, bPass;

        public PageLog()
        {
            InitializeComponent();
        }

        public PageLog(List<User> u)
        {
            InitializeComponent();
            this.u = u;
            Validar();
        }

        private void entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry texto = (Entry)sender;
            if (texto.FindByName("entryDNI").Equals(sender))
                bDNI = (entryDNI.Text.Length > 0);
            else if (texto.FindByName("entryPass").Equals(sender))
                bPass = (entryPass.Text != string.Empty) && (entryPass.Text.Length > 4);
            Validar();
        }

        private void Validar()
        {
            string visualState = (bDNI && bPass) ? "Valido" : "NoValido";
            VisualStateManager.GoToState(bLogin, visualState);
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
                else if (us.DNI != entryDNI.Text)
                {
                    var message = "Usuario incorrecto";
                    DependencyService.Get<IMessage>().LongTime(message);
                    entryDNI.Text = "";
                    entryPass.Text = "";
                }
            }
        }

        private void swPass_Toggled(object sender, ToggledEventArgs e)
        {
            entryPass.IsPassword = !swPass.IsToggled;
        }

    }
}
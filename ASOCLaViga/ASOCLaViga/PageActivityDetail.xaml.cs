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
    public partial class PageActivityDetail : ContentPage
    {
        public PageActivityDetail()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (App.u == null)
            {
                var message = "No permitido usted no esta registrado";
                DependencyService.Get<IMessage>().LongTime(message);
            }
            else
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bbddASOC.db");
                var db = new SQLiteConnection(databasePath);
                Actividad act = (Actividad)this.BindingContext;
                List<Actividad> list = db.Query<Actividad>("SELECT * FROM actividad INNER JOIN apuntado ON apuntado.IDAct = actividad.ID INNER JOIN user ON user.ID = apuntado.IDUser WHERE user.DNI = ? AND actividad.Titulo = ? ", App.u.DNI, act.Titulo);
                if (list.Count > 0)
                {
                    var message = "Ya estas apuntado";
                    DependencyService.Get<IMessage>().LongTime(message);
                }
                else if (act.Plazas == 0)
                {
                    var message = "No quedan mas plazas";
                    DependencyService.Get<IMessage>().LongTime(message);
                }
                else
                {
                    db.Insert(new Apuntado
                    {
                        IDAct = act.ID,
                        IDUser = App.u.ID
                    });
                    Navigation.PushModalAsync(new MainPage());
                }
            }

        }
    }
}
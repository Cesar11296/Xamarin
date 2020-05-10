﻿using SQLite;
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
            queriesAsync();
        }

        private async Task queriesAsync()
        {
            if (App.u == null)
            {
                var message = "No permitido usted no esta registrado";
                DependencyService.Get<IMessage>().LongTime(message);
            }
            else
            {
                /*var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bbddASOC.db");
                var db = new SQLiteConnection(databasePath);
                Actividad act = (Actividad)this.BindingContext;
                List<Actividad> list = db.Query<Actividad>("SELECT * FROM actividad INNER JOIN apuntado ON apuntado.IDAct = actividad.ID INNER JOIN user ON user.ID = apuntado.IDUser WHERE user.DNI = ? AND actividad.Titulo = ? ", App.u.DNI, act.Titulo);*/
                Actividad activity = (Actividad)this.BindingContext;
                List<User> listUser = await FirebaseHelper.GetAllUsers();
                List<Apuntado> listAp = await FirebaseHelper.GetApuntados();
                List<Actividad> list = await FirebaseHelper.GetActivities();
                var listFilter = from act in list
                                 join ap in listAp
                                 on act.ID equals ap.IDAct
                                 join user in listUser
                                 on ap.IDUser equals user.ID
                                 where (user.DNI == App.u.DNI && act.Titulo == activity.Titulo)
                                 select act;
                if (listFilter.Count() > 0)
                {
                    var message = "Ya estas apuntado";
                    DependencyService.Get<IMessage>().LongTime(message);
                }
                else if (activity.Plazas == 0)
                {
                    var message = "No quedan mas plazas";
                    DependencyService.Get<IMessage>().LongTime(message);
                }
                else
                {
                    /*db.Insert(new Apuntado
                    {
                        IDAct = act.ID,
                        IDUser = App.u.ID
                    });*/
                    await FirebaseHelper.AddApuntado(activity.ID, App.u.ID);
                    Navigation.PushModalAsync(new MainPage());
                }
            }
        }

    }
}
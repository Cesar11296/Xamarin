using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;

namespace ASOCLaViga
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        public SQLiteConnection db;
        private User u;


        public MasterPage()
        {
            InitializeComponent();
            this.u = App.u;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (this.u != null)
            {
                labelNombre.Text = u.Name;
                labelApellido.Text = u.Apellido;
                if (this.u.type == 1)
                {
                    labelTipoUser.Text = "Usuario Administrador";
                }else if (this.u.type == 0)
                {
                    labelTipoUser.Text = "Usuario básico";
                }
                else
                {
                    labelTipoUser.Text = "";
                }
                    MasterPageItem m = new MasterPageItem();
                m.Title = "Cerrar sesion";
                m.IconSource = "icon_logout.png";
                ArrayBarra.SetValue(m, 3);
            }
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            string op = item.Title;

            switch (op)
            {
                case "Actas":
                    Navigation.PushModalAsync(new PageActas());
                    break;
                case "Actividades":
                    Navigation.PushModalAsync(new PageAct());
                    break;
                case "Contacto":
                    Navigation.PushModalAsync(new PageContact());
                    break;
                case "Iniciar sesion":
                    Navigation.PushModalAsync(new PageLog());
                    break;
                case "Cerrar sesion":
                    OnAlert();
                    break;
                default:
                    break;
            }
        }

        async void OnAlert()
        {
            bool answer = await DisplayAlert("Cerrar sesión", "¿Deseas salir?", "Si", "No");
            if (answer)
            {
                labelNombre.Text = "";
                labelApellido.Text = "";
                labelTipoUser.Text = "";
                u = null;
            }
            else
            {
                Navigation.PushModalAsync(new MainPage());
            }
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            if (this.u != null)
            {
                Navigation.PushAsync(new PageUser());
            }
        }
    }
}
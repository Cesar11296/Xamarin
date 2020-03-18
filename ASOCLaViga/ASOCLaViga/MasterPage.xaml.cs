using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ASOCLaViga
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        private User u;
        public MasterPage()
        {
            InitializeComponent();
            carga();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetPeopleAsync();
        }

        private void carga()
        {
            u = new User { nombre = "Cesar", apellido = "Redondo", email = "redondogomezcesar", pais = "España", telefono = "601220994" };
            labelNombre.Text = u.nombre;
            labelApellido.Text = u.apellido;
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            string op = item.Title;
            switch (op)
            {
                case "Bus":
                    Navigation.PushModalAsync(new PageBus());
                    break;
                case "Actividades":
                    Navigation.PushModalAsync(new PageAct());
                    break;
                case "Contacto":
                    Navigation.PushModalAsync(new PageContact());
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
                u = null;
            }
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            PageOut p = new PageOut();
            Navigation.PushModalAsync(p);
        }
    }
}
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
    public partial class PageUser : ContentPage
    {
        private User u;

        public PageUser()
        {
            InitializeComponent();
            this.u = App.u;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (u != null)
            {
                labelNombre.Text = "Nombre: " + this.u.Name;
                labelApellido.Text = "Apellidos: " + this.u.Apellido;
                labelDNI.Text = "DNI: " + this.u.DNI;
                if(this.u.type == 1)
                {
                    labelTipo.Text = "Tipo de usuario: Administrador";
                }
                else if (this.u.type == 0)
                {
                    labelTipo.Text = "Tipo de usuario: Básico";
                }
            }
        }
    }
}
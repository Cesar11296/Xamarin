﻿using SQLite;
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
                if (this.u.type == 1)
                {
                    labelTipo.Text = "Tipo de usuario: Administrador";
                }
                else if (this.u.type == 0)
                {
                    labelTipo.Text = "Tipo de usuario: Básico";
                }
            }
        }

        private void bShowAct_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ShowActivities());
        }

        private async void bChangePass_Clicked(object sender, EventArgs e)
        {
            try
            {
                string result = "";
                while (result.Length < 5)
                {
                    result = await DisplayPromptAsync("Introduce la nueva contraseña", "Debe ser igual o superior a 5 caracteres");
                    if (result.Length < 5)
                    {
                        var message = "Contraseña Incorrecta";
                        DependencyService.Get<IMessage>().Up(message);
                    }
                }
                if (result.Length >= 5)
                {
                    doUpdateAsync(result);
                }
            }
            catch (System.NullReferenceException)
            {
            }
        }

        private async Task doUpdateAsync(string result)
        {
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;
            try
            {
                await FirebaseHelper.UpdatePass(App.u.DNI, result);
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
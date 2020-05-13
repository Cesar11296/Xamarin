using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data;
using System.Threading;

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

        private void bLogin_Clicked(object sender, EventArgs e)
        {
            queryUser();
            //https://www.c-sharpcorner.com/article/xamarin-forms-working-with-firebase-realtime-database-crud-operations/
            //https://xamarinmonkeys.blogspot.com/2019/01/xamarinforms-working-with-firebase.html
        }

        private async Task queryUser()
        {
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;
            try
            {
                var person = await FirebaseHelper.GetUser(entryDNI.Text, entryPass.Text);
                if (person != null)
                {
                    if (entryDNI.Text == person.DNI)
                    {
                        App.u = person;
                    }
                }
                else
                {
                    var message = "Usuario incorrecto";
                    DependencyService.Get<IMessage>().LongTime(message);
                    entryDNI.Text = "";
                    entryPass.Text = "";
                }
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {e.Message}");
            }
            finally
            {
                tokenSource2.Dispose();
                Navigation.PushModalAsync(new MainPage());
            }
        }

        private void swPass_Toggled(object sender, ToggledEventArgs e)
        {
            entryPass.IsPassword = !swPass.IsToggled;
        }
    }
}
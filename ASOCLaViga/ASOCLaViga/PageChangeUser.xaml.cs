using SQLite;
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
    public partial class PageChangeUser : ContentPage
    {
        User usuario;
        String tipo;
        public PageChangeUser()
        {
            InitializeComponent();
        }

        public PageChangeUser(User us)
        {
            InitializeComponent();
            usuario = us;
            if (us.type == 1)
            {
                tipo = "Administrador";
            }
            else if (us.type == 0)
            {
                tipo = "Básico";
            }
            entryName.Text = usuario.Name;
            entryApellidos.Text = usuario.Apellido;
            entryDNI.Text = usuario.DNI;
            pickerType.Title = tipo;
        }

        private void pickerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                tipo = (string)picker.ItemsSource[selectedIndex];
            }
        }

        private void bChange_Clicked(object sender, EventArgs e)
        {
            doUpdateAsync();

        }

        private async Task doUpdateAsync()
        {
            int opt = 2;
            if (tipo == "Administrador")
            {
                opt = 1;
            }
            else if (tipo == "Básico")
            {
                opt = 0;
            }
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;
            try
            {
                await FirebaseHelper.UpdateUser(Convert.ToInt32(usuario.ID), entryName.Text, entryApellidos.Text, entryDNI.Text, opt, usuario.DNI);
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {e.Message}");
            }
            finally
            {
                tokenSource2.Dispose();
                Navigation.PopModalAsync();
            }
        }
    }
}
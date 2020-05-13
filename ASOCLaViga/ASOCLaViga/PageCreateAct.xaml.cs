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
    public partial class PageCreateAct : ContentPage
    {
        bool eTitulo, eLugar, eDescripcion, eFoto, eBus, ePrecio, ePlazas;

        public PageCreateAct()
        {
            InitializeComponent();
            pickerBus.Title = "";
        }

        private void entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry texto = (Entry)sender;
            if (texto.FindByName("entryTitulo").Equals(sender))
                eTitulo = (entryTitulo.Text.Length > 0);
            else if (texto.FindByName("entryLugar").Equals(sender))
                eLugar = (entryLugar.Text.Length > 0);
            else if (texto.FindByName("entryFoto").Equals(sender))
                eFoto = (entryFoto.Text.Length > 0);
            else if (texto.FindByName("entryPrecio").Equals(sender))
                ePrecio = (entryFoto.Text.Length > 0);
            else if (texto.FindByName("entryPlazas").Equals(sender))
                ePlazas = (entryPlazas.Text.Length > 0);
            Validar();
        }

        private void editorDescripcion_TextChanged(object sender, TextChangedEventArgs e)
        {
            Editor texto = (Editor)sender;
            if (texto.FindByName("editorDescripcion").Equals(sender))
                eDescripcion = (editorDescripcion.Text.Length > 0);
            Validar();
        }

        private void pickerBus_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                pickerBus.Title = (string)picker.ItemsSource[selectedIndex];
            }
            Validar();
        }

        private void Validar()
        {
            if (pickerBus.Title.Length > 1)
            {
                eBus = true;
                string visualState = (eTitulo && eLugar && eDescripcion && eFoto && eBus && ePrecio && ePlazas) ? "Valido" : "NoValido";
                VisualStateManager.GoToState(bAdd, visualState);
            }
        }

        private void bAdd_Clicked(object sender, EventArgs e)
        {
            addValueAsync();
        }

        private async Task addValueAsync()
        {
            decimal price = Convert.ToDecimal(entryPrecio.Text, System.Globalization.CultureInfo.CurrentCulture);
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;
            try
            {
                await FirebaseHelper.AddActividad(entryTitulo.Text, entryLugar.Text, editorDescripcion.Text, entryFoto.Text, pickerBus.Title, price, fechaAct.Date, Int32.Parse(entryPlazas.Text));
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
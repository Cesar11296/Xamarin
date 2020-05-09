using SQLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ASOCLaViga
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageChangeAct : ContentPage
    {
        Actividad act;

        public PageChangeAct()
        {
            InitializeComponent();
        }

        public PageChangeAct(Actividad a)
        {
            InitializeComponent();
            act = a;
            entryTitulo.Text = act.Titulo;
            entryLugar.Text = act.Lugar;
            editorDescripcion.Text = act.Descripccion;
            entryFoto.Text = act.Foto;
            entryPrecio.Text = act.Precio.ToString();
            fechaAct.Date = act.Fecha;
            entryPlazas.Text = act.Plazas.ToString();
            pickerBus.Title = act.bus;
        }

        private void pickerBus_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                pickerBus.Title = (string)picker.ItemsSource[selectedIndex];
            }
        }

        private void bChange_Clicked(object sender, EventArgs e)
        {
            decimal price = Convert.ToDecimal(entryPrecio.Text, System.Globalization.CultureInfo.CurrentCulture);
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bbddASOC.db");
            var db = new SQLiteConnection(databasePath);
            db.Query<Actividad>("UPDATE Actividad SET Titulo = ?, Lugar = ?, Descripccion = ?, Foto = ?, bus= ?, Precio = ?, Fecha = ?, Plazas = ? where ID = ?", entryTitulo.Text,
            entryLugar.Text,
            editorDescripcion.Text,
            entryFoto.Text,
            pickerBus.Title,
            price,
            fechaAct.Date,
            entryPlazas.Text,
            act.ID);
            Navigation.PopModalAsync();
        }
    }
}
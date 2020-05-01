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
            else if (us.type == 1)
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
                pickerType.Title = (string)picker.ItemsSource[selectedIndex];
            }
        }

        private void bChange_Clicked(object sender, EventArgs e)
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bbddASOC.db");
            var db = new SQLiteConnection(databasePath);
            db.Query<User>("UPDATE User SET Name = ?, Apellido = ?, DNI = ?, type = ? where DNI = ?", entryName.Text,
            entryApellidos.Text,
            entryDNI.Text,
            pickerType.Title,
                usuario.DNI);
            Navigation.PushModalAsync(new MainPage());
        }
    }
}
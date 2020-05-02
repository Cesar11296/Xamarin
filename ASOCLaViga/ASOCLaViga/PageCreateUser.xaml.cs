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
    public partial class PageCreateUser : ContentPage
    {
        string tipo ="";
        bool eName, eApellido, eDNI, ePicker;
        int opt;

        public PageCreateUser()
        {
            InitializeComponent();
        }

        private void bAdd_Clicked(object sender, EventArgs e)
        {
            if (tipo.Length > 1)
            {
                if (tipo.Equals("Administrador"))
                {
                    opt = 1;
                }
                else if (tipo.Equals("Básico"))
                {
                    opt = 0;
                }
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bbddASOC.db");
                var db = new SQLiteConnection(databasePath);
                db.Insert(new User
                {
                    Name = entryName.Text,
                    Apellido = entryApellidos.Text,
                    DNI = entryDNI.Text,
                    pass = "12345",
                    type = opt
                });
                Navigation.PopModalAsync();
            }
        }

        private void entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry texto = (Entry)sender;
            if (texto.FindByName("entryName").Equals(sender))
                eName = (entryName.Text.Length > 0);
            else if (texto.FindByName("entryApellidos").Equals(sender))
                eApellido = (entryApellidos.Text.Length > 0);
            else if (texto.FindByName("entryDNI").Equals(sender))
                eDNI = (entryDNI.Text.Length > 8) && (entryDNI.Text.Length < 10);
            Validar();
        }

        private void pickerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                tipo = (string)picker.ItemsSource[selectedIndex];
            }
            Validar();
        }

        private void Validar()
        {
            if (tipo.Length > 1)
            {
                ePicker = true;
                string visualState = (eName && eApellido && eDNI && ePicker) ? "Valido" : "NoValido";
                VisualStateManager.GoToState(bAdd, visualState);
            }
        }
    }
}
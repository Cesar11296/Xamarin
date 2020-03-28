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

namespace ASOCLaViga
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        
        public MasterPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            //https://es.ourcodeworld.com/articulos/leer/70/como-conectarse-a-una-base-de-datos-mysql-con-c-en-winforms-y-xampp
            base.OnAppearing();
            string connectionString = "datasource=127.0.0.1;user id=root;password=;database=asoc;";
            
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            Console.WriteLine("No se encontro nada");
            MySqlDataReader reader = null; ;
            String data = null;
            try
            {
                string query = "SELECT * FROM `user` WHERE `Name`='Cesar'";
                MySqlCommand command = new MySqlCommand(query);
                command.Connection = databaseConnection;
                databaseConnection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data += reader.GetString(1);
                    labelNombre.Text = data;
                }
            }
            catch (MySqlException ex)
            {
                labelNombre.Text = "error";
            }
            finally
            {
                databaseConnection.Close();
            }
            /*
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MySQLite.db3");
            var db = new SQLiteConnection(databasePath);
            var list = db.Query<User>("SELECT DISTINCT * FROM User where Name = ?", "Cesar");
            foreach (User us in list)
            {
                labelNombre.Text = us.Name;
                labelApellido.Text = us.Apellido;
            }*/
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
            }
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            PageOut p = new PageOut();
            Navigation.PushModalAsync(p);
        }
    }
}
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ASOCLaViga
{
    public partial class App : Application
    {
        static User usLog;

        public static User u
        {
            get
            {
                return usLog;
            }
            set
            {
                usLog = value;
            }
        }

        public App()
        {
            InitializeComponent();
           /* 
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "bbddASOC.db");
            var db = new SQLiteConnection(databasePath);
            db.CreateTable<Actividad>();
            db.Insert(new Actividad
            {
                Titulo = "Ruta del segador",
                Lugar = "Montemayor de Pililla",
                Descripccion = "Tradicional ruta del segador a través del valle de Valcorba.",
                Foto = "segador_ruta.jpg",
                bus = false,
                Precio = 15.5,
                Fecha = new DateTime(2020, 5, 12),
                Plazas = 60
            }) ;
            db.Insert(new Actividad
            {
                Titulo = "Gymkana en La Hontana",
                Lugar = "Montemayor de Pililla",
                Descripccion = "Gymkana en nuestro area recreativa La Hontana",
                Foto = "hontana_act.jpg",
                bus = false,
                Precio = 0,
                Fecha = new DateTime(2020, 6, 12),
                Plazas = 60
            });
            db.Insert(new Actividad
            {
                Titulo = "Esquí en Xanadu",
                Lugar = "Madrid",
                Descripccion = "Diviertete en esta escapada al Xanadu de Madrid",
                Foto = "xanadu.jpg",
                bus = true,
                Precio = 20,
                Fecha = new DateTime(1939, 6, 12),
                Plazas = 55
            });
            db.Insert(new Actividad
            {
                Titulo = "Kachetazo",
                Lugar = "Arrabal de Portillo",
                Descripccion = "Liada en el Kache con motivo del final del Covid-19",
                Foto = "kache.jpg",
                bus = true,
                Precio = 3,
                Fecha = new DateTime(2020, 6, 12),
                Plazas = 55
            });

            db.CreateTable<User>();
            db.Insert(new User
            {
                Name = "Javier",
                Apellido = "Garcia Criado",
                DNI = "71178102X",
                pass = "12345",
                type = 1
            });
            db.Insert(new User
            {
                Name = "Cesar",
                Apellido = "Redondo Gomez",
                DNI = "71178102W",
                pass = "12345",
                type = 0
            });
            db.Insert(new User
            {
                Name = "Paco",
                Apellido = "Osuna",
                DNI = "1234567H",
                pass = "12345",
                type = 0
            });
            db.CreateTable<Apuntado>();
            db.Insert(new Apuntado
            {
                IDUser = 1,
                IDAct = 2
            });
            db.Insert(new Apuntado
            {
                IDUser = 1,
                IDAct = 3
            });
            db.Insert(new Apuntado
            {
                IDUser = 2,
                IDAct = 4
            });
            db.Insert(new Apuntado
            {
                IDUser = 2,
                IDAct = 3
            });*/
            //Javier - Gymkana
            //Javier - Xanadu
            //Cesar - Kache
            //Cesar - Xanadu
            //INSERT INTO `apuntado` (`ID`, `IDUser`, `IDAct`) VALUES (NULL, '2', '1'); 
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

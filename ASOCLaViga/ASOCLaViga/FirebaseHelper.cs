using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOCLaViga
{
    public class FirebaseHelper
    {
        //https://dzone.com/articles/xamarinforms-working-with-firebase-storage   --- Almacenamiento de archivos
        //https://www.c-sharpcorner.com/article/xamarin-forms-working-with-firebase-storage/
        static FirebaseClient firebase = new FirebaseClient("https://asocviga.firebaseio.com/");

        public static async Task<List<User>> GetAllUsers()
        {

            return (await firebase
              .Child("User")
              .OnceAsync<User>()).Select(item => new User
              {
                  ID = item.Object.ID,
                  Name = item.Object.Name,
                  Apellido = item.Object.Apellido,
                  phone = item.Object.phone,
                  type = item.Object.type,
                  DNI = item.Object.DNI,
                  pass = item.Object.pass
              }).ToList();
        }

        public static async Task<User> GetUser(string userDNI, string pass)
        {
            var allPersons = await GetAllUsers();
            await firebase
              .Child("User")
              .OnceAsync<User>();
            return allPersons.Where(a => a.DNI == userDNI && a.pass == pass).FirstOrDefault();
        }

        public static async Task<User> GetId()
        {
            var allPersons = await GetAllUsers();
            await firebase
              .Child("User")
              .OnceAsync<User>();
            return allPersons.Last();
        }

        public static async Task UpdateUser(int userId, string Name, string Apellido, string DNI, int type, string DNIOld)
        {
            var toUpdateUser = (await firebase
              .Child("User")
              .OnceAsync<User>()).Where(a => a.Object.DNI == DNIOld).FirstOrDefault();

            await firebase
              .Child("User")
              .Child(toUpdateUser.Key)
              .PutAsync(new User()
              {
                  ID = userId,
                  Name = Name,
                  Apellido = Apellido,
                  DNI = DNI,
                  type = type,
                  pass = toUpdateUser.Object.pass,
                  phone = toUpdateUser.Object.phone
              });
        }

        public static async Task UpdatePass(string DNIOld, string pass)
        {
            var toUpdateUser = (await firebase
              .Child("User")
              .OnceAsync<User>()).Where(a => a.Object.DNI == DNIOld).FirstOrDefault();

            await firebase
              .Child("User")
              .Child(toUpdateUser.Key)
              .PutAsync(new User()
              {
                  ID = toUpdateUser.Object.ID,
                  Name = toUpdateUser.Object.Name,
                  Apellido = toUpdateUser.Object.Apellido,
                  DNI = toUpdateUser.Object.DNI,
                  type = toUpdateUser.Object.type,
                  pass = pass,
                  phone = toUpdateUser.Object.phone
              });
        }

        public static async Task AddUser(string name, string apellido, string DNI, string pass, int type)
        {
            var person = await GetId();
            await firebase
              .Child("User")
              .PostAsync(new User()
              {
                  ID = (person.ID + 2),
                  Name = name,
                  Apellido = apellido,
                  phone = "",
                  type = type,
                  DNI = DNI,
                  pass = pass
              });
        }

        public static async Task DeleteUser(int userID)
        {
            var toDeleteUser = (await firebase
              .Child("User")
              .OnceAsync<User>()).Where(a => a.Object.ID == userID).FirstOrDefault();
            await firebase.Child("User").Child(toDeleteUser.Key).DeleteAsync();

        }

        public static async Task<List<Actividad>> GetActivities()
        {
            return (await firebase
              .Child("Actividad")
              .OnceAsync<Actividad>()).Select(item => new Actividad
              {
                  ID = item.Object.ID,
                  Titulo = item.Object.Titulo,
                  Lugar = item.Object.Lugar,
                  Descripccion = item.Object.Descripccion,
                  Foto = item.Object.Foto,
                  bus = item.Object.bus,
                  Precio = item.Object.Precio,
                  Fecha = item.Object.Fecha,
                  Plazas = item.Object.Plazas
              }).ToList();
        }

        public static async Task<Actividad> GetActivity()
        {
            var act = await GetActivities();
            await firebase
              .Child("Actividad")
              .OnceAsync<Actividad>();
            return act.FirstOrDefault();
        }

        public static async Task<Actividad> GetIdAct()
        {
            var allActs = await GetActivities();
            await firebase
              .Child("Actividad")
              .OnceAsync<Actividad>();
            return allActs.Last();
        }

        public static async Task UpdateActividad(int actId, string Titulo, string Lugar, string Descripccion, string Foto, string bus, Decimal Precio, DateTime Fecha, int Plazas)
        {
            var toUpdateActividad = (await firebase
              .Child("Actividad")
              .OnceAsync<Actividad>()).Where(a => a.Object.ID == actId).FirstOrDefault();

            await firebase
              .Child("Actividad")
              .Child(toUpdateActividad.Key)
              .PutAsync(new Actividad()
              {
                  ID = actId,
                  Titulo = Titulo,
                  Lugar = Lugar,
                  Descripccion = Descripccion,
                  Foto = Foto,
                  bus = bus,
                  Precio = Precio,
                  Fecha = Fecha,
                  Plazas = Plazas
              });
        }

        public static async Task UpdateActividadPlazas(int Id)
        {
            var toUpdateActividad = (await firebase
              .Child("Actividad")
              .OnceAsync<Actividad>()).Where(a => a.Object.ID == Id).FirstOrDefault();

            await firebase
              .Child("Actividad")
              .Child(toUpdateActividad.Key)
              .PutAsync(new Actividad()
              {
                  ID = Id,
                  Titulo = toUpdateActividad.Object.Titulo,
                  Lugar = toUpdateActividad.Object.Lugar,
                  Descripccion = toUpdateActividad.Object.Descripccion,
                  Foto = toUpdateActividad.Object.Foto,
                  bus = toUpdateActividad.Object.bus,
                  Precio = toUpdateActividad.Object.Precio,
                  Fecha = toUpdateActividad.Object.Fecha,
                  Plazas = toUpdateActividad.Object.Plazas - 1
              });
        }

        public static async Task AddActividad(string Titulo, string Lugar, string Descripccion, string Foto, string bus, Decimal Precio, DateTime Fecha, int Plazas)
        {
            var actID = await GetIdAct();
            await firebase
              .Child("Actividad")
              .PostAsync(new Actividad()
              {
                  ID = (actID.ID + 2),
                  Titulo = Titulo,
                  Lugar = Lugar,
                  Descripccion = Descripccion,
                  Foto = Foto,
                  bus = bus,
                  Precio = Precio,
                  Fecha = Fecha,
                  Plazas = Plazas
              });
        }

        public static async Task DeleteActividad(int actID)
        {
            var toDeleteActividad = (await firebase
              .Child("Actividad")
              .OnceAsync<Actividad>()).Where(a => a.Object.ID == actID).FirstOrDefault();
            await firebase.Child("Actividad").Child(toDeleteActividad.Key).DeleteAsync();

        }

        public static async Task<List<Apuntado>> GetApuntados()
        {
            return (await firebase
              .Child("Apuntado")
              .OnceAsync<Apuntado>()).Select(item => new Apuntado
              {
                  ID = item.Object.ID,
                  IDUser = item.Object.IDUser,
                  IDAct = item.Object.IDAct,
                  Estado = item.Object.Estado
              }).ToList();
        }

        public static async Task<Apuntado> GetIdApuntado()
        {
            var idAp = await GetApuntados();
            await firebase
              .Child("Apuntado")
              .OnceAsync<Apuntado>();
            return idAp.Last();
        }

        public static async Task AddApuntado(int IDAct, int IDUser)
        {
            var apuntadoID = await GetIdApuntado();
            await firebase
              .Child("Apuntado")
              .PostAsync(new Apuntado()
              {
                  ID = (apuntadoID.ID + 2),
                  IDAct = IDAct,
                  IDUser = IDUser,
                  Estado = "No Pagado"
              });
        }

        public static async Task UpdateApuntado(int ID)
        {
            var toUpdateAp = (await firebase
              .Child("Apuntado")
              .OnceAsync<Apuntado>()).Where(a => a.Object.ID == ID).FirstOrDefault();

            await firebase
              .Child("Apuntado")
              .Child(toUpdateAp.Key)
              .PutAsync(new Apuntado()
              {
                  ID = toUpdateAp.Object.ID,
                  IDAct = toUpdateAp.Object.IDAct,
                  IDUser = toUpdateAp.Object.IDUser,
                  Estado = "Pagado"
              });
        }

        public static async Task DeleteApuntado(int ID)
        {
            var toDeleteActividad = (await firebase
              .Child("Apuntado")
              .OnceAsync<Apuntado>()).Where(a => a.Object.ID == ID).FirstOrDefault();
            await firebase.Child("Apuntado").Child(toDeleteActividad.Key).DeleteAsync();

        }
    }
}
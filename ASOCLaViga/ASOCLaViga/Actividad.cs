using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASOCLaViga
{
    public class Actividad
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Lugar { get; set; }
        public string Descripccion { get; set; }
        public string Foto { get; set; }
        public bool bus { get; set; }
        public double Precio { get; set; }
        public DateTime Fecha { get; set; }
        public int Plazas { get; set; }
    }
}

using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASOCLaViga
{
    public class Apuntado
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [ForeignKey(typeof(User))]
        public int IDUser { get; set; }
        [ForeignKey(typeof(Actividad))]
        public int IDAct { get; set; }
        public string Estado { get; set; }
    }
}

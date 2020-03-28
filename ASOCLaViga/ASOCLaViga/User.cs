using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASOCLaViga
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }

    }
}

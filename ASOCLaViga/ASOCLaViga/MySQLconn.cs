using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace ASOCLaViga
{
    public class MySQLconn
    {
        string usr;
        string pass;

        public MySQLconn(string User, string Password)
        {
            usr = User;
            pass = Password;
        }
        MySqlBaseConnectionStringBuilder Builder = new MySqlConnectionStringBuilder();

        public bool TryConnection(out string Error)
        {
            Builder.Server = "127.0.0.1";
            Builder.Database = "asoc";
            Builder.UserID =usr;
            Builder.Password = pass;
            try
            {
                MySqlConnection ms = new MySqlConnection(Builder.ToString());
                ms.Open();
                Error = "";
                return true;
            }catch  ( Exception ex){
                Error = ex.ToString();
                return false;
            }
        }
    }
}

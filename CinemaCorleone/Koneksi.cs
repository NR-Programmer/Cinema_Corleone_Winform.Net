using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaCorleone
{
    internal class Koneksi
    {
        public static MySqlConnection GetConn()
        {
            string connectionString = "server=127.0.0.1;port=3306;user=root;password=Dens999201;database=laravel_cinema_corleone;";
            return new MySqlConnection(connectionString);
        }
    }
}
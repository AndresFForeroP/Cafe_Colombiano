using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

namespace Liga_Futbol.src.Shared.Helpers
{
    public class MySqlVersionResolver
    {
        public static Version DetectVersion(string connectionString)
        {
            using var comunicacion = new MySqlConnection(connectionString);
            comunicacion.Open();
            var raw = comunicacion.ServerVersion;
            var clean = raw.Split('-')[0];
            return Version.Parse(clean);
        }
    }
}
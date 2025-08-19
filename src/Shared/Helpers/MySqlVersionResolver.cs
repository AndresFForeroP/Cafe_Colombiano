using System;
using MySqlConnector;

namespace Cafe_Colombiano.src.Shared.Helpers
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
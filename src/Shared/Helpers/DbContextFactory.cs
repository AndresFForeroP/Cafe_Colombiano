using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Liga_futbol.Src.Shared.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Liga_Futbol.src.Shared.Helpers
{
    public class DbContextFactory
    {
        public static AppDbContext Create()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
            string? connectionString = Environment.GetEnvironmentVariable("MysqlConnection")
                ?? configuration.GetConnectionString("MysqlConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'MysqlConnection' is not configured.");
            }
            var detectedVersion = MySqlVersionResolver.DetectVersion(connectionString);
            var minversion = new Version(8, 0, 0);
            if (detectedVersion < minversion)
            {
                throw new InvalidOperationException($"MySQL version {detectedVersion} is not supported. Minimum required version is {minversion}.");
            }
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, new MySqlServerVersion(detectedVersion))
                .Options;
            return new AppDbContext(options);
        }
    }

}
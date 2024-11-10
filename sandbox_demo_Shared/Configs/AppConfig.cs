using Microsoft.Extensions.Configuration;
using System;

namespace sandbox_demo_Shared.Configs
{
    public static class AppConfig
    {
        private static readonly Lazy<IConfiguration> _configuration = new(() => new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build());

        public static string? GetConnectionString()
        {
            return _configuration.Value.GetConnectionString("HsqldbConnection");
        }

        public static string? GetUser()
        {
            return Environment.GetEnvironmentVariable("HSQLDB_USER") ?? _configuration.Value["HsqldbSettings:User"];
        }


        public static string? GetPassword()
        {
            var password = Environment.GetEnvironmentVariable("HSQLDB_PASSWORD");

            if (password == "empty")
                password = string.Empty;

            if (password == null)
                password = _configuration.Value["HsqldbSettings:Password"];

            return password;
        }

        public static string? GetBaseUrl()
        {
            return _configuration.Value["ApiSettings:BaseUrl"];
        }

        public static string? GetWebAppUrl()
        {
            return _configuration.Value["ApiSettings:WebAppUrl"];
        }

        public static IConfiguration GetConfiguration()
        {
            return _configuration.Value;
        }
    }
}

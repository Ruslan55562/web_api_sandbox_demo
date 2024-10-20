using Microsoft.Extensions.Configuration;

namespace sandbox_demo_API_Configs
{
    public static class ConfigurationLoader
    {
        private static readonly Lazy<IConfiguration> _configuration = new(() => new ConfigurationBuilder()
                             .SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("appsettings.json")
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
        public static IConfiguration GetConfiguration()
        {
            return _configuration.Value;
        }
    }
}

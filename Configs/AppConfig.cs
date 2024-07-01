using Microsoft.Extensions.Configuration;

namespace SpecFlowProject
{
    public static class AppConfig
    {
        private static readonly Lazy<IConfiguration> _configuration = new(() => new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build());

        public static string? GetConnectionString()
        {
            return _configuration.Value.GetConnectionString("HsqldbConnection");
        }

        public static string? GetUser()
        {
            return _configuration.Value["HsqldbSettings:User"];
        }

        public static string? GetPassword()
        {
            return _configuration.Value["HsqldbSettings:Password"];
        }
    }
}

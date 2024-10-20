using java.sql;
using NUnit.Framework;
using sandbox_demo_API_Configs;

namespace sandbox_demo_API.Hooks
{
    public static class DBHook
    {
        private static Connection? _connection;

        public static void OpenConnection()
        {
            try
            {
                string? connectionString = ConfigurationLoader.GetConnectionString();
                string? user = ConfigurationLoader.GetUser();
                string? password = ConfigurationLoader.GetPassword();

                DriverManager.registerDriver(new org.hsqldb.jdbcDriver());
                _connection = DriverManager.getConnection(connectionString, user, password);
                _connection.setAutoCommit(true);
            }
            catch (SQLException ex)
            {
                TestContext.WriteLine("Exception was thrown when connecting to DB " + ex.Message);
                throw;
            }
        }

        public static void CloseConnection()
        {
            try
            {
                if (_connection != null && !_connection.isClosed())
                {
                    _connection.close();
                }
            }
            catch (SQLException ex)
            {
                TestContext.WriteLine("There is an exception when trying to close connection " + ex.Message);
                throw;
            }
        }

        public static bool IsConnectionOpen()
        {
            return _connection != null && !_connection.isClosed();
        }

        public static bool IsConnectionClosed()
        {
            return _connection.isClosed();
        }

        public static Connection GetConnection()
        {
            return _connection;
        }

    }
}

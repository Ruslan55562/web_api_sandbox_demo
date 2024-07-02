using java.sql;
using SpecFlowProject;

namespace web_api_sandbox_demo_UI.Hooks
{
    public static class DBHook
    {
        private static Connection? _connection;

        public static void OpenConnection()
        {
            try
            {
                string? connectionString = AppConfig.GetConnectionString();
                string? user = AppConfig.GetUser();
                string? password = AppConfig.GetPassword();

                DriverManager.registerDriver(new org.hsqldb.jdbcDriver());
                _connection = DriverManager.getConnection(connectionString, user, password);
                _connection.setAutoCommit(true);
            }
            catch (SQLException ex)
            {
                Console.WriteLine("Exception was thrown when connecting to DB " + ex.Message);
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
                Console.WriteLine("There is an exception when trying to close connection " + ex.Message);
                throw;
            }
        }

        public static bool IsConnectionOpen()
        {
            return _connection != null && !_connection.isClosed();
        }
    }
}

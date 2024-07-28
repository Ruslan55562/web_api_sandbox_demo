using java.sql;
using NUnit.Framework;
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

        public static void CleanupUsers()
        {
            try
            {
                if (!ScenarioContext.Current.ContainsKey("usernames"))
                    return;

                var usernames = ScenarioContext.Current["usernames"] as List<string>;
                if (usernames == null || usernames.Count == 0)
                    return;

                foreach (var username in usernames)
                {
                    string query = "SELECT ID FROM PUBLIC.CUSTOMER WHERE USERNAME = ?";
                    PreparedStatement stmt = GetConnection().prepareStatement(query);
                    stmt.setString(1, username);
                    ResultSet rs = stmt.executeQuery();

                    if (rs.next())
                    {
                        int userId = rs.getInt("ID");

                        string deleteAccountQuery = "DELETE FROM PUBLIC.ACCOUNT WHERE CUSTOMER_ID = ?";
                        PreparedStatement deleteAccountStmt = GetConnection().prepareStatement(deleteAccountQuery);
                        deleteAccountStmt.setInt(1, userId);
                        deleteAccountStmt.executeUpdate();

                        string deleteCustomerQuery = "DELETE FROM PUBLIC.CUSTOMER WHERE ID = ?";
                        PreparedStatement deleteCustomerStmt = GetConnection().prepareStatement(deleteCustomerQuery);
                        deleteCustomerStmt.setInt(1, userId);
                        deleteCustomerStmt.executeUpdate();
                    }

                    rs.close();
                    stmt.close();
                }
            }
            catch (SQLException ex)
            {
                TestContext.WriteLine("Exception during cleanup: " + ex.Message);
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

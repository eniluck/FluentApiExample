using System.Data.SqlClient;

namespace FluentApiExample
{
    internal class SimpleFuletnSqlConnection
    {
        private string _server;
        private string _database;
        private string _user;
        private string _password;

        public SimpleFuletnSqlConnection ForServer(string server)
        {
            _server = server;
            return this;
        }

        public SimpleFuletnSqlConnection AndDatabase(string database)
        {
            _database = database;
            return this;
        }

        public SimpleFuletnSqlConnection AsUser(string user)
        {
            _user = user;
            return this;
        }

        public SimpleFuletnSqlConnection AndPassword(string password)
        {
            _password = password;
            return this;
        }

        public SqlConnection Connect()
        {
            var connection = new SqlConnection($"Server={_server};Database={_database};User id={_user};Password={_password}");
            connection.Open();
            return connection;
        }
    }
}
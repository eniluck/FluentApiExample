using System;
using System.Data;
using System.Data.SqlClient;

namespace FluentApiExample
{
    class FluentSqlConnection: IServerSelectionStage,
        IDatabaseSelectionStage,
        IUserSelectionStage,
        IPasswordSelectionStage,
        IConnectionInitializerStage
    {
        private string _server;
        private string _database;
        private string _user;
        private string _password;
        private bool _integratedSecurity = false;

        private FluentSqlConnection(){}

        public static IServerSelectionStage CreateConnection(Action<ConnectionConfiguration> config)
        {
            var configuration = new ConnectionConfiguration();
            config?.Invoke(configuration);
            return new FluentSqlConnection();
        }
        public static IServerSelectionStage CreateConnection()
        {
            return new FluentSqlConnection();
        }

        public IUserSelectionStage AndDatabase(string database)
        {
            _database = database;
            return this;
        }

        public IPasswordSelectionStage AsUser(string user)
        {
            _user = user;
            return this;
        }


        public IDatabaseSelectionStage ForServer(string server)
        {
            _server = server;
            return this;
        }

        public IConnectionInitializerStage WithPassword(string password)
        {
            _password = password;
            return this;
        }

        // Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        public IDbConnection Connect()
        {
            SqlConnection connection;
            if (_integratedSecurity)
                connection = new SqlConnection($"Server={_server};Database={_database};Integrated Security=True;");
            else
                connection = new SqlConnection($"Server={_server};Database={_database};User id={_user};Password={_password}");
            connection.Open();
            return connection;
        }

        public IConnectionInitializerStage LoginWithWindowsCredentials()
        {
            throw new System.NotImplementedException();
        }
    }


    public class ConnectionConfiguration
    {
        public string ConnectionName { get; set; }
    }

    public interface IServerSelectionStage
    {
        public IDatabaseSelectionStage ForServer(string server);
    }

    public interface IDatabaseSelectionStage
    {
        public IUserSelectionStage AndDatabase(string database);
    }

    public interface IUserSelectionStage
    {
        public IPasswordSelectionStage AsUser(string user);
        public IConnectionInitializerStage LoginWithWindowsCredentials();
    }

    public interface IPasswordSelectionStage
    {
        public IConnectionInitializerStage WithPassword(string password);
    }
    
    public interface IConnectionInitializerStage
    {
        public IDbConnection Connect();
    }
}

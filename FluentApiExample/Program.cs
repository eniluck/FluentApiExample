using System;

namespace FluentApiExample
{
    class Program
    {
        static void Main(string[] args)
        {
            /*User user = new UserBuilder()
                 .WithLogin("testLogin")
                 .WithName("user")
                 .Build();
            */

            var connection = new SimpleFuletnSqlConnection()
                .ForServer("localhost")
                .AndDatabase("mydb")
                .AsUser("SqlServerUser")
                .AndPassword("Password")
                .Connect();

            var connection2 = FluentSqlConnection
                .CreateConnection(config =>
                {
                    config.ConnectionName = "MyConnection";
                })
                .ForServer("localhost")
                .AndDatabase("mydb")
                .AsUser("user")
                .WithPassword("password")
                .Connect();

            var connection3 = FluentSqlConnection
                .CreateConnection()
                .ForServer("(localdb)")
                .AndDatabase("MSSQLLocalDB")
                .LoginWithWindowsCredentials()
                .Connect();
        }
    }
}

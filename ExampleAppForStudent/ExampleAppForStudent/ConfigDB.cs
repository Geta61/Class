using MySql.Data.MySqlClient;

namespace ExampleAppForStudent
{
    class ConfigDB
    {
        public MySqlConnectionStringBuilder connectionStringBuilder = null;
        public ConfigDB()
        {
            connectionStringBuilder = new MySqlConnectionStringBuilder();
            connectionStringBuilder.Server = "localhost";
            connectionStringBuilder.UserID = "root";
            connectionStringBuilder.Password = "";
            connectionStringBuilder.Database = "example.db";
        }

        public string GetConnectionString()
        {
            return connectionStringBuilder.ConnectionString.ToString();
        }
    }
}

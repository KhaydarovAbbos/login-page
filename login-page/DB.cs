using MySql.Data.MySqlClient;

namespace login_page
{
    public class DB
    {
        MySqlConnection connection = new MySqlConnection("server = localhost; username = root; database=login_page");

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}

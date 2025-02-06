using System.Data.Common;
namespace TicketLib.Repository;

public class ConnectionHelper<T> : IConnectionHelper<T> where T : DbConnection, new()
{
    string _connectionString = "";
    T? connection = null;
    public ConnectionHelper(string connectionString)
    {
        _connectionString = connectionString;
    }
    public T GetConnection()
    {
        T conn = new T();
        conn.ConnectionString = _connectionString;
        conn.Open();
        return conn;
    }
}
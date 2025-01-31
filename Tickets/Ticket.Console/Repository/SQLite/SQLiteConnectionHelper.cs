using TicketLib.Repository;
using System.Data.SQLite;
namespace Ticket.Console.Repository.SQLite;

class SQLiteConnectionHelper : IConnectionHelper<SQLiteConnection>
{
    string _dataSource;
    public SQLiteConnectionHelper(string dataSource)
    {
        _dataSource = dataSource;
    }
    public SQLiteConnection GetConnection() {
        var conn = new SQLiteConnection($"Data Source={_dataSource}");
        conn.Open();
        return conn;
    }
}
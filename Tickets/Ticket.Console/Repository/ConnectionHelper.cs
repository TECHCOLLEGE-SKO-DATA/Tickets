using System.Data.Common;
using System.Data.SQLite;
namespace Ticket.Console.Repository;

public class ConnectionHelper
{
    public DbConnection GetConnection()
    {
        return new SQLiteConnection("Data Context=test.db");
    }
}
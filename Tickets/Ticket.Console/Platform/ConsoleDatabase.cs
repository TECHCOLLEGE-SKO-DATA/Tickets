using System.Data.SQLite;
using Ticket.Console.Repository.SQLite;
using TicketLib.Models;
using TicketLib.Platform;
using TicketLib.Repository;

namespace Ticket.Console.Platform;

public class ConsoleDatabase : IDatabase
{
    IConnectionHelper<SQLiteConnection> _connectionHelper;
    public IRepository<Person> Persons { get; private set; }
    public ConsoleDatabase(string connection_string)
    {
        _connectionHelper = new ConnectionHelper<SQLiteConnection>(connection_string);
        Persons = new PersonRepository(_connectionHelper);
    }
}
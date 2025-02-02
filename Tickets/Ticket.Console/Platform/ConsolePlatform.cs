using TicketLib.Platform;

namespace Ticket.Console.Platform;

public class ConsolePlatform : IPlatform
{
    ConsoleDatabase _database;
    public IDatabase Database => _database;
    public ConsolePlatform(string connection_string) 
    {
        _database = new(connection_string);
    }

}
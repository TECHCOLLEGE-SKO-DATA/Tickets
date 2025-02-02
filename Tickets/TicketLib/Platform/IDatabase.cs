using TicketLib.Models;
using TicketLib.Repository;

namespace TicketLib.Platform;

public interface IDatabase
{
    IRepository<Person> Persons { get; }
}
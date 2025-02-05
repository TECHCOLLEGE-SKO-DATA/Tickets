using TicketLib.Models;
using TicketLib.Repository;

namespace TicketLib.Platform;

public interface IDatabase
{
    IRepository<Person> Persons { get; }
    IRepository<Incident> Incidents { get; }
    IRepository<Staff> Staffs { get; }
    IRepository<IncidentLog> IncidentLogs { get; }
    IRepository<ContactMethod> ContactMethods { get; }

    IRepository<City> Citys { get; }
}
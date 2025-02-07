using TicketLib.Models;
using TicketLib.Repository;

namespace TicketLib.Services;
/// <summary>
/// Service abstraction for managing more complex tasks with the data access layer
/// </summary>
public interface IIncidentsService
{
    /// <summary>
    /// Creates a new incident and person with adress and contact method if these doesn't exist yet
    /// </summary>
    /// <param name="incident"></param>
    /// <param name="person"></param>
    public void CreateNewIncident(Incident incident, Person person);

    public IEnumerable<Incident> GetOpenIncidents();
    public IEnumerable<Incident> GetAllIncidents(); 
}
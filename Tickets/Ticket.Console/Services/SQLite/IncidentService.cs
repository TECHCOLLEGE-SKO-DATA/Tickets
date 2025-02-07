// using System.Data.SQLite;
// using Ticket.Console.Repository.SQLite;
// using TicketLib;
// using TicketLib.Models;
// using TicketLib.Platform;
// using TicketLib.Repository;
// using TicketLib.Services;
// namespace Ticket.Console.Services.SQLite;

// public class IncidentsService : IIncidentsService
// {
//     //IConnectionHelper<SQLiteConnection> _connection;
//     PersonRepository _personRepository;
//     public IncidentsService(IConnectionHelper<SQLiteConnection> connection)
//     {
//         // _connection = connection;
//         _personRepository = new PersonRepository(connection);
        
//     }
//     public void CreateNewIncident(Incident incident, Person person)
//     {
//         Person? existingPerson = _personRepository.GetById(person.PersonId);
//         if (existingPerson == null)
//         {
//             _personRepository.Add(person);
//         }
        
//     }

//     public IEnumerable<Incident> GetAllIncidents()
//     {
//         throw new NotImplementedException();
//     }

//     // public IEnumerable<Incident> GetOpenIncidents()
//     // {
//     //     string baseSql = "SELECT Id";
//     // }
// }
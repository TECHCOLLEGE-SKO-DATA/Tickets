using System.Data;
using System.Data.SQLite;
using Ticket.Console.Repository.SQLite;
using TicketLib;
using TicketLib.Models;
using TicketLib.Platform;
using TicketLib.Repository;
using TicketLib.Services;
namespace Ticket.Console.Services.SQLite;

public class IncidentsService : IIncidentsService
{
    IncidentRepository _incidentRepository;
    PersonRepository _personRepository;
    AddressRepository _addressRepository;
    CityRepository _cityRepository;
    IConnectionHelper<SQLiteConnection> _connection;
    public IncidentsService(IConnectionHelper<SQLiteConnection> connection)
    {
        _connection = connection;
        _personRepository = new PersonRepository(connection);
        _incidentRepository = new IncidentRepository(connection);
        _addressRepository = new AddressRepository(connection);
        _cityRepository = new CityRepository(connection);
        
    }
    public void CreateNewIncident(Incident incident, Person person)
    {
        // if (address == null && person.Address != 0)
        // {
        //     address = _addressRepository.GetById(person.Address);
        // }
        
        Person? existingPerson = _personRepository.GetById(person.PersonId);
        if (existingPerson == null)
        {
            _personRepository.Add(person);
        }
        
    }

    IEnumerable<Incident> GetIncidents(bool onlyOpen)
    {
        List<Incident> results = new();
        using SQLiteConnection conn = _connection.GetConnection();

        SQLiteCommand command = conn.CreateCommand();
        string sql = @"
            SELECT 
                i.IncidentId, i.Status, i.IssueDate, i.IssueDescription, i.CreatedBy, 
                i.ResolutionDate, i.ResolutionDescription, a.AddressId, a.Street, a.Number, 
                a.CityId, c.ZipCode, c.Name, cust.FirstName, cust.MiddleName, 
                cust.LastName, cust.AddressId, cust.RegisteredDate, cust.PreferredContactMethodId,

                i.AddressId, i.CustomerId
                FROM Incident AS i
                    LEFT JOIN Address AS a 
                        ON i.AddressId = a.AddressId
                    JOIN City as c
                        ON c.CityId = a.CityId
                    LEFT JOIN Person as cust
                        ON i.CustomerId = cust.PersonId
                    ";
        if (onlyOpen)
        {
            sql += $"WHERE i.ResolutionDate is null";
        }
        
        command.CommandText = sql;
        SQLiteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Incident incident = new();
            Address address = new();
            City city = new();
            Customer customer = new();

            incident.IncidentId = reader.GetInt32(0);
            incident.Status = reader.GetByte(1);
            incident.IssueDate = reader.GetDateTime(2);
            incident.IssueDescription = reader.GetString(3);
            incident.CreatedBy = reader.GetInt32(4);

            incident.ResolutionDate = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5);
            incident.ResolutionDescription = reader.IsDBNull(6) ? "" : reader.GetString(6);          
            address.AddressId = reader.GetInt32(7);
            address.Street = reader.GetString(8);
            address.Number = reader.GetString(9);

            address.CityId = reader.GetInt16(10);           
            city.CityId = reader.GetInt16(10);
            city.ZipCode = reader.GetString(11);
            city.Name = reader.GetString(12);
            customer.FirstName = reader.GetString(13);
            customer.MiddleName = reader.GetString(14);

            customer.LastName = reader.GetString(15);
            customer.AddressId = reader.GetInt32(16); 
            customer.RegisterdDate = reader.GetDateTime(17);
            customer.PreferredContactMethod = reader.GetInt16(18);

            results.Add(incident);
        }
        return results;
    }

    public IEnumerable<Incident> GetAllIncidents() => GetIncidents(false);
    public IEnumerable<Incident> GetOpenIncidents() => GetIncidents(true);
}
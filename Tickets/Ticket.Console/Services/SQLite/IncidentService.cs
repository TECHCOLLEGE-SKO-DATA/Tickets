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
    public void CreateNewIncident(Incident incident)
    {
              
        Person? existingPerson = _personRepository.GetById(incident?.Customer?.PersonId ?? 0);
        if (incident?.Customer != null && existingPerson == null)
        {
            _personRepository.Add(incident.Customer);
        }
    }

    IEnumerable<Incident> GetIncidents(int limit, int offset, bool onlyOpen)
    {
        List<Incident> results = new();
        using SQLiteConnection conn = _connection.GetConnection();

        SQLiteCommand command = conn.CreateCommand();
    //     ContactMethodId INTEGER PRIMARY KEY AUTOINCREMENT,
    // PersonId INT NOT NULL,
    // ContactInfoType INT NOT NULL,
    // Value VARCHAR(255) NOT NULL,
        string sql = @"
            SELECT 
                i.IncidentId, i.Status, i.IssueDate, i.IssueDescription, i.CreatedBy, 
                i.ResolutionDate, i.ResolutionDescription, a.AddressId, a.Street, a.Number, 
                a.CityId, c.ZipCode, c.Name, cust.FirstName, cust.MiddleName, 
                cust.LastName, cust.AddressId, cust.RegisteredDate, cust.PreferredContactMethodId,

                i.AddressId, i.CustomerId, con.ContactInfoType, con.Value
                FROM Incident AS i
                    LEFT JOIN Address AS a 
                        ON i.AddressId = a.AddressId
                    JOIN City as c
                        ON c.CityId = a.CityId
                    LEFT JOIN Person as cust
                        ON i.CustomerId = cust.PersonId
                    LEFT JOIN ContactMethod as con
                        ON i.CustomerId = con.PersonId
                    ";
        if (onlyOpen)
        {
            sql += $"WHERE i.ResolutionDate is null";
        }
        sql += "LIMIT @offset, @limit";

        command.Parameters.AddWithValue("@offset", offset);
        command.Parameters.AddWithValue("@limit", limit);
        
        command.CommandText = sql;
        SQLiteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Incident incident = new();
            incident.Address = new();
            incident.Address.City = new();
            incident.Customer = new();
            incident.Customer.PreferredContactMethod = new();

            incident.IncidentId = reader.GetInt32(0);
            incident.Status = reader.GetByte(1);
            incident.IssueDate = reader.GetDateTime(2);
            incident.IssueDescription = reader.GetString(3);
            //incident.CreatedBy = reader.GetInt32(4);

            incident.ResolutionDate = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5);
            incident.ResolutionDescription = reader.IsDBNull(6) ? "" : reader.GetString(6);          
            incident.Address.AddressId = reader.GetInt32(7);
            incident.Address.Street = reader.GetString(8);
            incident.Address.Number = reader.GetString(9);

            //incident.Address.CityId = reader.GetInt16(10);           
            incident.Address.City.CityId = reader.GetInt16(10);
            incident.Address.City.ZipCode = reader.GetString(11);
            incident.Address.City.Name = reader.GetString(12);
            incident.Customer.FirstName = reader.GetString(13);
            incident.Customer.MiddleName = reader.GetString(14);

            incident.Customer.LastName = reader.GetString(15);
            //incident.Customer.AddressId = reader.GetInt32(16); 
            incident.Customer.RegisterdDate = reader.GetDateTime(17);
            incident.Customer.PreferredContactMethod.ContactMethodId = reader.GetInt16(18);
            incident.Customer.PreferredContactMethod.ContactInfoType = reader.GetInt16(19);
            incident.Customer.PreferredContactMethod.Value = reader.GetString(20);


            results.Add(incident);
        }
        return results;
    }

    public IEnumerable<Incident> GetAllIncidents(int limit = 30, int offset = 0) => GetIncidents(limit, offset, false);
    public IEnumerable<Incident> GetOpenIncidents(int limit = 30, int offset = 0) => GetIncidents(limit, offset, true);
}
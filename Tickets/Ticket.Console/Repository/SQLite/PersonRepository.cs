using TicketLib.Repository;
using TicketLib.Models;
using System.Data.SQLite;
using System.Data.Common;

namespace Ticket.Console.Repository.SQLite;

public class PersonRepository : IRepository<Person>
{
    IConnectionHelper<SQLiteConnection> _connectionHelper;
    const string TABLE = "Person";
    public PersonRepository(IConnectionHelper<SQLiteConnection> connectionHelper)
    {
        _connectionHelper = connectionHelper;
    }

    public IEnumerable<Person> GetAll()
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"SELECT PersonId, FirstName, MiddleName, LastName, RegisteredDate, AddressId, PreferredContactMethodId FROM {TABLE}";

        List<Person> result = new();
        SQLiteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Person p = new()
            {
                PersonId = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                MiddleName = reader.GetString(2),
                LastName = reader.GetString(3),
                RegisteredDate  = reader.GetDateTime(4),
                AddressId = reader.GetInt32(5),
                PreferredContactMethod = reader.GetInt32(6),
            };
            result.Add(p);
        }
        return result;
    }
    public Person? GetById(int id)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"SELECT personId, firstName,middleName,lastName, registeredDate, addressId, preferredContactMethodId FROM {TABLE} WHERE personId=@id";
        command.Parameters.AddWithValue("@id", id);
        SQLiteDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            Person p = new()
            {
                PersonId = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                MiddleName = reader.GetString(2),
                LastName = reader.GetString(3),
                RegisteredDate  = reader.GetDateTime(4),
                AddressId = reader.GetInt32(5),
                PreferredContactMethod = reader.GetInt32(6),
            };
            return p;
        }

        return null;
    }
    public void Add(Person model)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"INSERT INTO {TABLE} (firstName, middleName, lastName, registeredDate, addressId, preferredContactMethodId) VALUES (@firstName, @middleName, @lastName, @registeredDate, @addressId, @preferredContactMethodId)";

        command.Parameters.AddWithValue("@firstName", model.FirstName);
        command.Parameters.AddWithValue("@middleName", model.MiddleName);
        command.Parameters.AddWithValue("@lastName", model.LastName);

        // Sørg for at formatere RegisteredDate korrekt
        command.Parameters.AddWithValue("@registeredDate", model.RegisteredDate.ToString("yyyy-MM-dd HH:mm:ss"));

        command.Parameters.AddWithValue("@addressId", model.AddressId);
        command.Parameters.AddWithValue("@preferredContactMethodId", model.PreferredContactMethod);
        command.ExecuteNonQuery();
    }
    public void Update(Person model)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = @$"UPDATE {TABLE} SET 
                    firstName = @firstName,
                    middleName = @middleName, 
                    lastName = @lastName, 
                    registeredDate = @registeredDate,
                    addressId = @addressId,
                    preferredContactMethodId = @preferredContactMethodId
                WHERE personId=@id";
        command.Parameters.AddWithValue("@firstName", model.FirstName);
        command.Parameters.AddWithValue("@middleName", model.MiddleName);
        command.Parameters.AddWithValue("@lastName", model.LastName);
        command.Parameters.AddWithValue("@registeredDate", model.RegisteredDate);
        command.Parameters.AddWithValue("@addressId", model.AddressId);
        command.Parameters.AddWithValue("@preferredContactMethodId", model.PreferredContactMethod);
        command.Parameters.AddWithValue("@id", model.PersonId);
        command.ExecuteNonQuery();
    }
    public void Delete(int id)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"DELETE FROM {TABLE} WHERE personId=@id";
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }
}
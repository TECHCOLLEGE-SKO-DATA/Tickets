using TicketLib.Repository;
using TicketLib.Models;
using System.Data.SQLite;
using System.Data.Common;
using TicketLib;

namespace Ticket.Console.Repository.Sqlite;

public class ContactMethodRepository : IRepository<ContactMethod>
{
    IConnectionHelper<SQLiteConnection> _connectionHelper;

    const string TABLE = "ContactMethod";

    public ContactMethodRepository(IConnectionHelper<SQLiteConnection> connectionHelper)
    {
        _connectionHelper = connectionHelper;
    }

    public IEnumerable<ContactMethod> GetAll()
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"SELECT ContactMethodId, PersonId, ContactInfoType, Value FROM {TABLE}";

        List<ContactMethod> result = new();
        SQLiteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            ContactMethod cm = new()
            {
                ContactMethodId = reader.GetInt32(0),
                PersonId = reader.GetInt32(1),
                ContactInfoType = reader.GetInt32(2),
                Value = reader.GetString(3)
            };
            result.Add(cm);
        }
        return result;
    }

    public ContactMethod? GetById(int id)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"SELECT ContactMethodId, PersonId, ContactInfoType, Value,  FROM {TABLE} WHERE ContactMethodId=@id";
        command.Parameters.AddWithValue("@id", id);
        SQLiteDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            ContactMethod cm = new()
            {
                ContactMethodId = reader.GetInt32(0),
                PersonId = reader.GetInt32(1),
                ContactInfoType = reader.GetInt32(2),
                Value = reader.GetString(3),
            };
            return cm;
        }
        return null;
    }

    public void Add(ContactMethod model)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"INSERT INTO {TABLE} (ContactInfoType, Value) VALUES (@ContactInfoType, @Value)";


        command.Parameters.AddWithValue("@ContactInfoType", model.ContactInfoType);
        command.Parameters.AddWithValue("@Value", model.Value);
        command.ExecuteNonQuery();
    }

    public void Update(ContactMethod model)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = @$"UPDATE {TABLE} SET
                            ContactInfoType = @ContactInfoType,
                            Value = @Value
                            WHERE ContactMethodId=@id";
        command.Parameters.AddWithValue("@ContactInfoType", model.ContactInfoType);
        command.Parameters.AddWithValue("@Value", model.Value);
        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"DELETE FROM {TABLE} WHERE ContactMethodId=@id";
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }
}
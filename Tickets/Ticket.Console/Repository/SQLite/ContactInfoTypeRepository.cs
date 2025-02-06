using TicketLib.Repository;
using TicketLib.Models;
using System.Data.SQLite;
using System.Data.Common;
using TicketLib;

namespace Ticket.Console.Repository.Sqlite;
public class ContactInfoTypeRepository : IRepository<ContactInfoType>
{
    IConnectionHelper<SQLiteConnection> _connectionHelper;

    const string TABLE = "ContactMethod";

    public ContactInfoTypeRepository(IConnectionHelper<SQLiteConnection> connectionHelper)
    {
        _connectionHelper = connectionHelper;
    }

    public IEnumerable<ContactInfoType> GetAll()
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"SELECT ContactInfoTypeId, Name, Icon FROM {TABLE}";

        List<ContactInfoType> result = new();
        SQLiteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            ContactInfoType cm = new()
            {
                ContactInfoTypeId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Icon = reader.GetChar(2),
            };
            result.Add(cm);
        }
        return result;
    }

    public ContactInfoType? GetById(int id)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"SELECT ContactInfoTypeId, Name, Icon FROM {TABLE} WHERE ContactInfoTypeId=@id";
        command.Parameters.AddWithValue("@id", id);
        SQLiteDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            ContactInfoType cm = new()
            {
                ContactInfoTypeId = reader.GetInt32(0),
                Name = reader.GetString(1),
                Icon = reader.GetChar(2),
            };
            return cm;
        }
        return null;
    }

//   public int ContactInfoTypeId { get; set; } // (PK)
//   public string Name { get; set; }
//   public char Icon { get; set; }
    public void Add(ContactInfoType model)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"INSERT INTO {TABLE} (Name, Icon) VALUES (@Name, @Icon)";

        command.Parameters.AddWithValue("@Name", model.Name);
        command.Parameters.AddWithValue("@Icon", model.Icon);
        command.ExecuteNonQuery();
    }

    public void Update(ContactInfoType model)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = @$"UPDATE {TABLE} SET
                            Name = @Name,
                            Icon = @Icon
                            WHERE ContactInfoTypeId=@id";
        command.Parameters.AddWithValue("@Name", model.Name);
        command.Parameters.AddWithValue("@Icon", model.Icon);
        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"DELETE FROM {TABLE} WHERE ContactInfoTypeId=@id";
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }
}
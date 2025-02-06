using TicketLib.Repository;
using TicketLib.Models;
using System.Data.SQLite;
using System.Data.Common;
using TicketLib;
using System.Data;

namespace Ticket.Console.Repository.SQLite;

public class CityRepository : IRepository<City>
{
    IConnectionHelper<SQLiteConnection> _connectionHelper;
    const string TABLE = "city";
    public CityRepository(IConnectionHelper<SQLiteConnection> connectionHelper)
    {
        _connectionHelper = connectionHelper;
    }

    public IEnumerable<City> GetAll()
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"SELECT CityId, ZipCode, Name FROM {TABLE}";

        List<City> result = new();
        SQLiteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            City p = new()
            {
                CityId = (short)reader.GetInt32(0),
                ZipCode = reader.GetString(1),
                Name = reader.GetString(2)
            };
            result.Add(p);
        }
        return result;
    }
    public City? GetById(int id)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"SELECT CityId, ZipCode, Name FROM {TABLE} WHERE CityId=@id";
        command.Parameters.AddWithValue("@id", id);
        SQLiteDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            City p = new()
            {
                CityId = (short)reader.GetInt32(0),
                ZipCode = reader.GetString(1),
                Name = reader.GetString(2)
            };
            return p;
        }

        return null;
    }
    public void Add(City model)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"INSERT INTO {TABLE} (ZipCode, Name) VALUES ( @ZipCode, @Name)";

        command.Parameters.AddWithValue("@ZipCode", model.ZipCode);
        command.Parameters.AddWithValue("@Name", model.Name);
        command.ExecuteNonQuery();

    }
    public void Update(City model)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = @$"UPDATE {TABLE} SET 
                    CityId = @CityId,
                    ZipCode = @ZipCode, 
                    Name = @Name";
        command.Parameters.AddWithValue("@ZipCode", model.ZipCode);
        command.Parameters.AddWithValue("@Name", model.Name);
        command.ExecuteNonQuery();
    }
    public void Delete(int id)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"DELETE FROM {TABLE} WHERE CityId=@id";
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }
}
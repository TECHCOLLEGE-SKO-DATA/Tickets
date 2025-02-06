using TicketLib.Repository;
using TicketLib.Models;
using System.Data.SQLite;
using System.Data.Common;
using TicketLib;

namespace Ticket.Console.Repository.SQLite;
public class AddressRepository : IRepository<Address>
{
    IConnectionHelper<SQLiteConnection> _connectionHelper;
    const string TABLE = "address";
    public AddressRepository(IConnectionHelper<SQLiteConnection> connectionHelper) 
    {
        _connectionHelper = connectionHelper;
    }

    public IEnumerable<Address> GetAll() 
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"SELECT AddressId, Street, Number,CityId FROM {TABLE}";
        
        List<Address> result = new();
        SQLiteDataReader reader = command.ExecuteReader();
        while (reader.Read()) {
            Address p = new() {
                AddressId = reader.GetInt32(0),
                Street = reader.GetString(1),
                Number = reader.GetString(2),
                CityId = (short)reader.GetInt32(3),
            };
            result.Add(p);
        }
        return result;
    }
    public Address? GetById(int id) 
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"SELECT Street, Number,CityId FROM {TABLE} WHERE AddressId=@id";
        command.Parameters.AddWithValue("@id", id);
        SQLiteDataReader reader = command.ExecuteReader();
        if (reader.Read()) {
            Address p = new() {
                AddressId = reader.GetInt32(0),
                Street = reader.GetString(1),
                Number = reader.GetString(2),
                CityId = (short)reader.GetInt32(3),
            };
            return p;
        }
        
        return null;
    }
    public void Add(Address model)
    {
        
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"INSERT INTO {TABLE} (Street,Number, CityId) VALUES (@Street, @Number, @CityId)";

        command.Parameters.AddWithValue("@Street", model.Street);
        command.Parameters.AddWithValue("@Number", model.Number);
        command.Parameters.AddWithValue("@CityId", model.CityId);
        command.ExecuteNonQuery();

    }
    public void Update(Address model) 
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = @$"UPDATE {TABLE} SET 
                    Street = @Street, 
                    Number = @Number, 
                    CityId = @CityId
                WHERE AddressId=@id";
        command.Parameters.AddWithValue("@Street", model.Street);
        command.Parameters.AddWithValue("@Number", model.Number);
        command.Parameters.AddWithValue("@CityId", model.CityId);
        command.ExecuteNonQuery();
    }
    public void Delete(int id) 
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"DELETE FROM {TABLE} WHERE AddressId=@id";
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }
}
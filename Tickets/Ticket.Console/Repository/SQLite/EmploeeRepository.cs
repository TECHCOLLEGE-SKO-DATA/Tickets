using TicketLib.Repository;
using TicketLib.Models;
using System.Data.SQLite;
using System.Data.Common;
using TicketLib;

namespace Ticket.Console.Repository.SQLite;

public class EmploeeRepository : IRepository<Employee>
{
    IConnectionHelper<SQLiteConnection> _connectionHelper;

    const string TABLE = "Employee";

    public EmploeeRepository(IConnectionHelper<SQLiteConnection> connectionHelper)
    {
        _connectionHelper = connectionHelper;
    }

    public IEnumerable<Employee> GetAll()
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"SELECT PersonId, Username, IsDriversLicenceValid, ProfilePicture FROM {TABLE}";

        List<Employee> result = new();
        SQLiteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Employee s = new()
            {
                PersonId = reader.GetInt32(0),
                Username = reader.GetString(1),
                IsDriversLicenceValid = reader.GetBoolean(2),
                ProfilePicture = reader.GetChar(3)
            };
            result.Add(s);
        }
        return result;
    }

    public Employee? GetById(int id)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"SELECT PersonId, Username, IsDriversLicenceValid, ProfilePicture FROM {TABLE} WHERE PersonId=@id";
        command.Parameters.AddWithValue("@id", id);
        SQLiteDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            Employee s = new()
            {
                PersonId = reader.GetInt32(0),
                Username = reader.GetString(1),
                IsDriversLicenceValid = reader.GetBoolean(2),
                ProfilePicture = reader.GetChar(3)
            };
            return s;
        }
        return null;
    }

    public void Add(Employee model)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"INSERT INTO {TABLE} (Username, Password, IsDriversLicenceValid, ProfilePicture) VALUES (@Username, @IsDriversLicenceValid, @ProfilePicture)";

        command.Parameters.AddWithValue("@Username", model.Username);
        command.Parameters.AddWithValue("@Password", model.Password);
        command.Parameters.AddWithValue("@IssueDate", model.IsDriversLicenceValid);
        command.Parameters.AddWithValue("@IssueDescription", model.ProfilePicture);
        command.ExecuteNonQuery();
    }

    public void Update(Employee model)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = @$"UPDATE {TABLE} SET
                            Username = @Username,
                            Password = @Password,
                            IsDriversLicenceValid = @IsDriversLicenceValid,
                            ProfilePicture = @ProfilePicture
                            WHERE PersonId=@id";
        command.Parameters.AddWithValue("@Username", model.Username);
        command.Parameters.AddWithValue("@Password", model.Password);
        command.Parameters.AddWithValue("@IssueDate", model.IsDriversLicenceValid);
        command.Parameters.AddWithValue("@IssueDescription", model.ProfilePicture);
        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"DELETE FROM {TABLE} WHERE PersonId=@id";
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }
}
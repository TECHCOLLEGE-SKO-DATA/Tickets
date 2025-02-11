using TicketLib.Repository;
using TicketLib.Models;
using System.Data.SQLite;
using System.Data.Common;
using TicketLib;
using MySqlX.XDevAPI.Relational;

namespace Ticket.Console.Repository.SQLite;

public class IncidentRepository : IRepository<Incident> 
{
    IConnectionHelper<SQLiteConnection> _connectionHelper;

    const string TABLE = "Incident";

    public IncidentRepository(IConnectionHelper<SQLiteConnection> connectionHelper)
    {
        _connectionHelper = connectionHelper;
    }

    public IEnumerable<Incident> GetAll()
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"SELECT IncidentId, Status, IssueDate, IssueDescription, CreatedBy, ResolutionDate, ResolutionDescription FROM {TABLE}";

        List<Incident> result = new();
        SQLiteDataReader reader = command.ExecuteReader();
        while (reader.Read()) {
            Incident i = new() {
                IncidentId = reader.GetInt32(0),
                Status = reader.GetByte(1),
                IssueDate = reader.GetDateTime(2),
                IssueDescription = reader.GetString(3),
                CreatedBy = reader.GetInt32(4),
                ResolutionDate = reader.GetDateTime(5),
                ResolutionDescription = reader.GetString(6),
            };
            result.Add(i);
        }
        return result;
    }

    public Incident? GetById(int id)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"SELECT IncidentId, Status, IssueDate, IssueDescription, CreatedBy, ResolutionDate, ResolutionDescription FROM {TABLE} WHERE IncidentId=@id";
        command.Parameters.AddWithValue("@id", id);
        SQLiteDataReader reader = command.ExecuteReader();
        if (reader.Read()) {
            Incident i = new() {
                IncidentId = reader.GetInt32(0),
                Status = reader.GetByte(1),
                IssueDate = reader.GetDateTime(2),
                IssueDescription = reader.GetString(3),
                CreatedBy = reader.GetInt32(4),
                ResolutionDate = reader.GetDateTime(5),
                ResolutionDescription = reader.GetString(6),
            };
            return i;
        }
        return null;
    }
    public void Add(Incident model) 
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"INSERT INTO {TABLE} (Status, IssueDate, IssueDescription, CreatedBy, ResolutionDate, ResolutionDescription) VALUES (@Status, @IssueDate, @IssueDescription, @CreatedBy, @ResolutionDate, @ResolutionDescription)";

        command.Parameters.AddWithValue("@Status", model.Status);
        command.Parameters.AddWithValue("@IssueDate", model.IssueDate);
        command.Parameters.AddWithValue("@IssueDescription", model.IssueDescription);
        command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
        command.Parameters.AddWithValue("@ResolutionDate", model.ResolutionDate);
        command.Parameters.AddWithValue("@ResolutionDescription", model.ResolutionDescription);
        command.ExecuteNonQuery();
    }

    public void Update(Incident model) 
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = @$"UPDATE {TABLE} SET
                            Status = @Status,
                            IssueDate = @IssueDate
                            IssueDescription = @IssueDescription
                            CreatedBy = @CreatedBy
                            ResolutionDate = @ResolutionDate
                            ResolutionDescription = @ResolutionDescription 
                            WHERE IncidentId=@id";
        command.Parameters.AddWithValue("@Status", model.Status);
        command.Parameters.AddWithValue("@IssueDate", model.IssueDate);
        command.Parameters.AddWithValue("@IssueDescription", model.IssueDescription);
        command.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
        command.Parameters.AddWithValue("@ResolutionDate", model.ResolutionDate);
        command.Parameters.AddWithValue("@ResolutionDescription", model.ResolutionDescription);
        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"DELETE FROM {TABLE} WHERE IncidentId=@id";
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery(); 
    }
}
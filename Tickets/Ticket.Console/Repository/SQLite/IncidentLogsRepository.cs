using TicketLib.Repository;
using TicketLib.Models;
using System.Data.SQLite;
using System.Data.Common;
using TicketLib;

namespace Ticket.Console.Repository.Sqlite;

public class IncidentLogsRepository : IRepository<IncidentLog>
{
    IConnectionHelper<SQLiteConnection> _connectionHelper;

    const string TABLE = "IncidentLog";

    public IncidentLogsRepository(IConnectionHelper<SQLiteConnection> connectionHelper)
    {
        _connectionHelper = connectionHelper;
    }

    public IEnumerable<IncidentLog> GetAll()
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"SELECT IncidentLogId, ChangedBy, LogDescription FROM {TABLE}";

        List<IncidentLog> result = new();
        SQLiteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            IncidentLog il = new()
            {
                IncidentLogId = reader.GetInt32(0),
                ChangedBy = reader.GetInt32(1),
                LogDescription = reader.GetString(2)
            };
            result.Add(il);
        }
        return result;
    }

    public IncidentLog? GetById(int id)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"SELECT IncidentLogId, ChangedBy, LogDescription,  FROM {TABLE} WHERE IncidentLogId=@id";
        command.Parameters.AddWithValue("@id", id);
        SQLiteDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            IncidentLog il = new()
            {
                IncidentLogId = reader.GetInt32(0),
                ChangedBy = reader.GetInt32(1),
                LogDescription = reader.GetString(2)
            };
            return il;
        }
        return null;
    }

    public void Add(IncidentLog model)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"INSERT INTO {TABLE} (ChangedBy, LogDescription) VALUES (@ChangedBy, @LogDescription)";

        command.Parameters.AddWithValue("@ChangedBy", model.ChangedBy);
        command.Parameters.AddWithValue("@LogDescription", model.LogDescription);
        command.ExecuteNonQuery();
    }

    public void Update(IncidentLog model)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = @$"UPDATE {TABLE} SET
                            ChangedBy = @Username,
                            LogDescription = @Password,
                            WHERE IncidentLogId=@id";
        command.Parameters.AddWithValue("@ChangedBy", model.ChangedBy);
        command.Parameters.AddWithValue("@LogDescription", model.LogDescription);
        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using SQLiteConnection conn = _connectionHelper.GetConnection();
        SQLiteCommand command = conn.CreateCommand();
        command.CommandText = $"DELETE FROM {TABLE} WHERE IncidentLogId=@id";
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }
}
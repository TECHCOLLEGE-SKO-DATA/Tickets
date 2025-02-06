namespace TicketLib;

public class IncidentLog : IModel
{
    public int IncidentLogId { get; set; } // (PK)

    public int ChangedBy { get; set; } // (FK person)

    public string LogDescription { get; set; }

    public string Validate()
    {
        throw new NotImplementedException();
    }
}
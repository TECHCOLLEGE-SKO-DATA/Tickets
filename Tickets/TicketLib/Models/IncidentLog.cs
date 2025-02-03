namespace TicketLib;

public class IncidentLog
{
    public int IncidentLogId { get; set; } // (PK)

    public int ChangedBy { get; set; } // (FK person)

    public string LogDescription { get; set; }

}
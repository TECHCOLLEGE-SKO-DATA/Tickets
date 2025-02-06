using TicketLib.Models;

namespace TicketLib;

public class IncidentLog : BaseModel, IModel
{
    public int IncidentLogId { get; set; } // (PK)

    public int ChangedBy { get; set; } // (FK person)

    public string LogDescription { get; set; }

    public string Validate()
    {
        throw new NotImplementedException();
    }

}
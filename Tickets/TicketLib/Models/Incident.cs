using TicketLib.Models;

namespace TicketLib;

public class Incident : BaseModel, IModel
{
    public int IncidentId { get; set; } //(PK)

    public byte Status { get; set; }

    public DateTime IssueDate { get; set; }

    public string IssueDescription { get; set; } = " ";

    public int CreatedBy { get; set; } //(FK Staff)

    public DateTime ResolutionDate { get; set; }
    public string ResolutionDescription { get; set; } = " ";

    public string Validate()
    {
        throw new NotImplementedException();
    }
}
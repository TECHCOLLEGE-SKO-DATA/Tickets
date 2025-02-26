using TicketLib.Models;

namespace TicketLib;

public class Incident : BaseModel, IModel
{
    public int IncidentId { get; set; } //(PK)
    public int? AddressId => Address?.AddressId ?? 0;
    public Address? Address { get; set; }
    public Person? Customer { get; set; }
    public int CustomerId => Customer?.PersonId ?? 0;
    
    public byte Status { get; set; }
    public DateTime IssueDate { get; set; }
    public string IssueDescription { get; set; } = " ";
    public Person? CreatedBy { get; set; }
    public int CreatedById => CreatedBy?.PersonId ?? 0; //(FK Staff)
    public DateTime? ResolutionDate { get; set; }
    public string ResolutionDescription { get; set; } = " ";

    public string Validate()
    {
        throw new NotImplementedException();
    }
}
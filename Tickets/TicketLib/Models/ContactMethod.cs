namespace TicketLib.Models;

public class ContactMethod : BaseModel
{
    public int ContactMethodId { get; set; } //(PK)

    public int PersonId { get; set; } // (FK)

    public int ContactInfoType { get; set; } //(FK)

    public string Value { get; set; }
}
namespace TicketLib;

public class ContactMethod : IModel
{
    public int ContactMethodId { get; set; } //(PK)

    public int PersonId { get; set; } // (FK)

    public int ContactInfoType { get; set; } //(FK)

    public string Value { get; set; }

    public string Validate()
    {
        throw new NotImplementedException();
    }
}
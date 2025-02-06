namespace TicketLib;

public class ContactInfoType : IModel
{
    public int ContactInfoTypeId { get; set; } // (PK)
    public string Name { get; set; }
    public char Icon { get; set; }

    public string Validate()
    {
        throw new NotImplementedException();
    }
}
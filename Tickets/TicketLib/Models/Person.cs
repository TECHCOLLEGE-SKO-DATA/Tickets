namespace TicketLib.Models;

public class Person : BaseModel, IModel
{
    public int PersonId { get; set; } //(PK)

    public string FirstName { get; set; } = " ";

    public string MiddleName { get; set; } = " ";

    public string LastName { get; set; } = " ";

    public int AddressId { get; set; } //(FK)

    public DateTime RegisteredDate  { get; set; }

    public int PreferredContactMethod { get; set; } //(PK)

    public string Validate()
    {
        throw new NotImplementedException();
    }
}
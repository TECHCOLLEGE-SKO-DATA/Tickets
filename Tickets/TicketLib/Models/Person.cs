namespace TicketLib.Models;

public class Person : BaseModel, IModel
{
    public int PersonId { get; set; } //(PK)

    public string FirstName { get; set; } = " ";

    public string MiddleName { get; set; } = " ";

    public string LastName { get; set; } = " ";

    public Address? Address { get; set; }
    public int AddressId => Address?.AddressId ?? 0; //(FK)

    public DateTime RegisterdDate { get; set; }

    public ContactMethod? PreferredContactMethod { get; set; }
    public int PreferredContactMethodId => PreferredContactMethod?.ContactMethodId ?? 0; //(FK)


    public string Validate()
    {
        throw new NotImplementedException();
    }
}
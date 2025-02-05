namespace TicketLib;

public class Person 
{
    public int PersonId { get; set; } //(PK)

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

    public int Address { get; set; } //(FK)

    public DateTime RegisterdDate { get; set; }

    public int PreferredContactMethod { get; set; } //(PK)
}
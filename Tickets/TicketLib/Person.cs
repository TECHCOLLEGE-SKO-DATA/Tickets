namespace TicketLib;

public class Person 
{
    public int PersonId { get; set; } //(PK)

    public string Firstname { get; set; }

    public string Middlename { get; set; }

    public string Lastname { get; set; }

    public int Address { get; set; } //(FK)

    public DateTime RegisterdDate { get; set; }

    public int PreferredContactMethod { get; set; } //(PK)
}
namespace TicketLib.Models;

public class Person : BaseModel
{
    public int PersonId { get; set; } //(PK)

    public string Firstname { get; set; }

    public string Middlename { get; set; }

    public string Lastname { get; set; }

    public int Address { get; set; } //(FK)

    public DateTime RegisterdDate { get; set; }

    public int PreferredContactMethod { get; set; } //(PK)
}
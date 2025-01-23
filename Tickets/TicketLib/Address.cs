namespace TicketLib;

public class Address
{
    public int AddressId { get; set; } //(PK)

    public string Street { get; set; }

    public string Number { get; set; }

    public short CityId { get; set; } //(FK)
}
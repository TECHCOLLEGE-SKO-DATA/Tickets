using TicketLib.Models;

namespace TicketLib;

public class Address : BaseModel, IModel
{
    public int AddressId { get; set; } //(PK)

    public string Street { get; set; } = " ";

    public string Number { get; set; } = " ";

    public short CityId => City?.CityId ?? 0; //(FK)
    public City? City { get; set; }

    public string Validate()
    {
        throw new NotImplementedException();
    }
}
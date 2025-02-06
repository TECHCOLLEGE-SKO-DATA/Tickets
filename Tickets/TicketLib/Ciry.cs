using System.ComponentModel.DataAnnotations;

namespace TicketLib;

public class City : IModel
{
    public short CityId { get; set; } //(PK)

    public string ZipCode { get; set; }

    public string Name { get; set; }

    public string Validate()
    {
        throw new NotImplementedException();
    }
}
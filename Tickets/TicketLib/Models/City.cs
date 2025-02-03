using System.ComponentModel.DataAnnotations;

namespace TicketLib;

public class City 
{
    public short CityId { get; set; } //(PK)

    public string ZipCode { get; set; }

    public string Name { get; set; }
}
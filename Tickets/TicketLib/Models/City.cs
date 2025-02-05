using System.ComponentModel.DataAnnotations;
using TicketLib.Models;

namespace TicketLib;

public class City : BaseModel
{
    public short CityId { get; set; } //(PK)

    public string ZipCode { get; set; }

    public string Name { get; set; }
}
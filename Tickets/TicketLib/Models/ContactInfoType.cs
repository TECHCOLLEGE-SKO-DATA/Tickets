using TicketLib.Models;

namespace TicketLib;

public class ContactInfoType : BaseModel
{
    public int ContactInfoTypeId { get; set; } // (PK)
    public string Name { get; set; }
    public char Icon { get; set; }
}
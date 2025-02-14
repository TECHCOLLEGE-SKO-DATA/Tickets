namespace TicketLib.Models;

public class Customer : Person, IModel
{
    public string CustomerNumber { get; set; } = "";
    public int PersonId { get; set; }  //(FK)
    public string Validate()
    {
        throw new NotImplementedException();
    }
}
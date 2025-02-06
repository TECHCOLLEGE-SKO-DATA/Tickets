namespace TicketLib.Models;

public class Customer : Person, IModel
{
    public int PersonId { get; set; } //(PK, FK)

    public string Validate()
    {
        throw new NotImplementedException();
    }
}
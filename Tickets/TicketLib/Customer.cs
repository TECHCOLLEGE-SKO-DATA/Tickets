namespace TicketLib;

public class Customer : IModel
{
    public int PersonId { get; set; } //(PK, FK)

    public string Validate()
    {
        throw new NotImplementedException();
    }
}
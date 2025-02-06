namespace TicketLib;

public interface IModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns>Empty string if validation is succes or error message if it fails</returns>
    public string Validate();
}
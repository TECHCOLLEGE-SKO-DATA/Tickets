namespace TicketLib;

public class Employee
{
    public int PersonId { get; set; } //(PK, FK)
    public string Username { get; set; }
    public char Password { get; set; }
    public bool IsDriversLicenceValid { get; set; }
    public char ProfilePicture { get; set; }
}
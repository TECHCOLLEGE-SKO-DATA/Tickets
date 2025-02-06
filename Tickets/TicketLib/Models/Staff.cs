using TicketLib.Models;

namespace TicketLib;

<<<<<<<< HEAD:Tickets/TicketLib/Models/Staff.cs
public class Staff : BaseModel
========
public class Employee
>>>>>>>> upstream/main:Tickets/TicketLib/Employee.cs
{
    public int PersonId { get; set; } //(PK, FK)
    public string Username { get; set; }
    public char Password { get; set; }
    public bool IsDriversLicenceValid { get; set; }
    public char ProfilePicture { get; set; }
}
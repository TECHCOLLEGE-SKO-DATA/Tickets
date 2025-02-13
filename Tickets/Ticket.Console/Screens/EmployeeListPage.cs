using TECHCOOL.UI;
using Ticket.Console.Screens;
using TicketLib;
using TicketLib.Models;
using TicketLib.Platform;

namespace Ticket.Console.Screens;

public class EmployeeListPage : TicketScreen 
{
    public override string Title {get; set;} = "Medarbejder";

    public EmployeeListPage(IPlatform platform) : base(platform) 
    {

    }

    protected override void Draw() 
    {
        ListPage<Employee> list = new();
        list.AddColumn("PersonId", "PersonId");
        list.AddColumn("Username", "Username");
        list.AddColumn("Password", "Password");
        list.AddColumn("IsDriversLicenceValid", "Valid Driverlicense");

        var employees = _platform.Database.Employees.GetAll();
        foreach (Employee e in employees) 
        {
            list.Add(e);
        }

        list.Select();
    }
}
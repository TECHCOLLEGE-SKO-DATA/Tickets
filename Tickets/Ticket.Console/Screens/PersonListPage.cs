using TECHCOOL.UI;
using Ticket.Console.Screens;
using TicketLib;
using TicketLib.Models;
using TicketLib.Platform;

namespace Ticket.Console.Screens;

public class PersonListPage : TicketScreen
{
    public override string Title {get; set;} = "Personer";
    public PersonListPage(IPlatform platform) : base(platform)
    {
        
    }

    protected override void Draw()
    {
        ListPage<Person> list = new();
        list.AddColumn("PersonId", "PersonId");
        list.AddColumn("FirstName", "FirstName");
        list.AddColumn("MiddleName", "MiddleName");
        list.AddColumn("LastName", "LastName");
        list.AddColumn("RegisterdDate", "RegisterdDate");
        list.AddColumn("PreferredContactMethod", "PreferredContactMethod");
        list.AddColumn("Address", "Address");  // this needs to be made with the service layer... Because its a FK

        var persons = _platform.Database.Persons.GetAll();
        foreach (Person p in persons) 
        {
            // var address = _platform.Database.Addresss.GetById(p.Address);
            // p.Addr = address ?? new Address();
            list.Add(p);
        }

        list.Select();
    }
}
using TECHCOOL.UI;
using Ticket.Console.Screens;
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
        list.AddColumn("Firstname", "Firstname");

        var persons = _platform.Database.Persons.GetAll();
        foreach (Person p in persons) 
        {
            list.Add(p);
        }
        
    }
}
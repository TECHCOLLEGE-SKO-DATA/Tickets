using TECHCOOL.UI;
using Ticket.Console.Screens;
using TicketLib.Models;
using TicketLib.Platform;

namespace Ticket.Console.Screens;

public class ContactMethodListPage : TicketScreen
{
    public override string Title {get; set;} = "Personer";

    public ContactMethodListPage(IPlatform platform) : base(platform)
    {
        
    }

    protected override void Draw() 
    {
        ListPage<ContactMethod> list = new();
        list.AddColumn("ContactMethodId", "ContactMethodId");
        //list.AddColumn("PersonId", "PersonId");
        //list.AddColumn("ContactInfoType", "ContactInfoType");
        list.AddColumn("Value", "Value");

        var contactMethods = _platform.Database.ContactMethods.GetAll();
        foreach (ContactMethod p in contactMethods) 
        {
            list.Add(p);
        }
    }
}
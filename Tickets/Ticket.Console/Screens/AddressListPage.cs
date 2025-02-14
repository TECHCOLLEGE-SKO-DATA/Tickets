using TECHCOOL.UI;
using Ticket.Console.Screens;
using TicketLib;
using TicketLib.Models;
using TicketLib.Platform;

namespace Ticket.Console.Screens;


public class AddressListPage : TicketScreen 
{
    public override string Title {get; set;} = "Adresser";

    public AddressListPage(IPlatform platform) : base(platform) 
    {
        
    }

    protected override void Draw() 
    {
        ListPage<Address> list = new();
        list.AddColumn("AddressId", "AddressId");
        list.AddColumn("Street", "Street");
        list.AddColumn("Number", "Number");
        list.AddColumn("City", "CityId");

        var addresses = _platform.Database.Addresss.GetAll();
        foreach (Address a in addresses) 
        {
            list.Add(a);
        }
        list.Select();
    }
}
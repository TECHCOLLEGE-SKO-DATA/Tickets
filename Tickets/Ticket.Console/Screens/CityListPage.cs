using TECHCOOL.UI;
using Ticket.Console.Screens;
using TicketLib;
using TicketLib.Models;
using TicketLib.Platform;

namespace Ticket.Console.Screens;

public class CityListPage : TicketScreen
{
    public override string Title {get; set;} = "Cities";
    public CityListPage(IPlatform platform) : base(platform)
    {
        
    }

    protected override void Draw()
    {
        ListPage<City> list = new();
        list.AddColumn("CityId", "CityId");
        list.AddColumn("Zipcode", "Zipcode");
        list.AddColumn("Name", "Name");

        var citys = _platform.Database.Citys.GetAll();
        foreach (City p in citys) 
        {
            list.Add(p);
        }
        
        list.Select();
    }

}
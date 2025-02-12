using TECHCOOL.UI;
using TicketLib;
using TicketLib.Platform;

namespace Ticket.Console.Screens;

public class MainScreen : TicketScreen
{
    public override string Title { get; set; } = "Tickets";

    public MainScreen(IPlatform platform) : base(platform)
    {

    }

    protected override void Draw()
    {
        Menu menu = new Menu();
        menu.Add( new EditPersonScreen(_platform) );
        menu.Add( new PersonListPage(_platform) );
        menu.Add( new IncidentListPage(_platform) );
        Screen.Display(menu);
    }
}
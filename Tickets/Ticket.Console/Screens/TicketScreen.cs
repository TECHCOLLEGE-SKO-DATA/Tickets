using TECHCOOL.UI;
using TicketLib.Platform;
using TicketLib.Repository;

namespace Ticket.Console.Screens;

public abstract class TicketScreen : Screen
{
    protected IPlatform _platform { get; private set; }
    public TicketScreen(IPlatform platform) 
    {
        _platform = platform;
    }
}
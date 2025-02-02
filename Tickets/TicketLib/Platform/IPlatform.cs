namespace TicketLib.Platform;

public interface IPlatform
{
    IDatabase Database { get; }
}
using Ticket.Console.Services.SQLite;

namespace TicketTests.Repository.SQLite;

public class ConsoleIncidentServiceTests
{
    [Fact]
    public void CanGetAllIncidents()
    {
        using MockSQLiteConnectionHelper helper = new();
        IncidentsService inc = new(helper);

        var all = inc.GetAllIncidents();
        Assert.Equal(2, all.Count());

        var open = inc.GetOpenIncidents();
        Assert.Equal(1, open.Count());
    }
}
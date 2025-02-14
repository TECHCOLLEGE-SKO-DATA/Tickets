using TECHCOOL.UI;
using Ticket.Console.Screens;
using TicketLib;
using TicketLib.Models;
using TicketLib.Platform;

namespace Ticket.Console.Screens;


public class IncidentListPage : TicketScreen 
{
    public override string Title {get; set;} = "Sager";

    public IncidentListPage(IPlatform platform) : base(platform) 
    {

    }

    protected override void Draw() 
    {
        ListPage<Incident> list = new();
        list.AddColumn("IncidentId", "IncidentId");
        list.AddColumn("Status", "Status");
        list.AddColumn("IssueDescription", "IssueDescription");
        list.AddColumn("IssueDate", "IssueDate");
        list.AddColumn("CreatedBy", "CreatedBy");
        list.AddColumn("ResolutionDate", "ResolutionDate");
        list.AddColumn("ResolutionDescription", "ResolutionDescription");
        var incidents = _platform.Database.Incidents.GetAll();
        foreach (Incident i in incidents) 
        {
            list.Add(i);
        }
        list.Select();
    }
}
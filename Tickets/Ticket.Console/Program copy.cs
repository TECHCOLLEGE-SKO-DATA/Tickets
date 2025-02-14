// See https://aka.ms/new-console-template for more information

using System.Data.SQLite;
using TicketLib.Models;
using Ticket.Console.Repository.SQLite;
using Ticket.Console.Screens;
using Ticket.Console.Platform;
using TECHCOOL.UI;
using TicketLib.Platform;
using Ticket.Console.Services.SQLite;
using TicketLib.Repository;

ConsolePlatform platform = new("Data Source=ticketSystem.db");
string sqlFilePath = System.IO.File.ReadAllText("../TicketLib/DbScripts/testTables.sql");

ConsoleDatabase database = (ConsoleDatabase) platform.Database;
if (!File.Exists("ticketSystem.db"))
{
    database.ExecuteNonQuery(sqlFilePath);
}
// MainScreen screen = new(platform);
PersonListPage screen = new(platform);

ConnectionHelper<SQLiteConnection> helper = new("Data Source=ticketSystem.db");
IncidentsService service = new(helper);
service.GetOpenIncidents();
Screen.Display(screen);
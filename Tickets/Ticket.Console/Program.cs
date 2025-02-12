// See https://aka.ms/new-console-template for more information

using System.Data.SQLite;
using TicketLib.Models;
using Ticket.Console.Repository.SQLite;
using Ticket.Console.Screens;
using Ticket.Console.Platform;
using TECHCOOL.UI;
using TicketLib.Platform;

ConsolePlatform platform = new("Data Source=ticketSystem.db");
string sqlFilePath = System.IO.File.ReadAllText("../TicketLib/DbScripts/testTables.sql");

ConsoleDatabase database = (ConsoleDatabase) platform.Database;
if (!File.Exists("ticketSystem.db"))
{
    database.ExecuteNonQuery(sqlFilePath);
}
// MainScreen screen = new(platform);
MainScreen screen = new(platform);
Screen.Display(screen);
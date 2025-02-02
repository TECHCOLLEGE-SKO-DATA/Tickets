// See https://aka.ms/new-console-template for more information

using System.Data.SQLite;
using TicketLib.Models;
using Ticket.Console.Repository.SQLite;
using Ticket.Console.Screens;
using Ticket.Console.Platform;
using TECHCOOL.UI;

// var connection_string = "Data Source=:memory";
// using (var conn = new SQLiteConnection(connection_string))
// {
//     var cmd = conn.CreateCommand();
//     cmd.CommandText = @"CREATE TABLE IF NOT EXISTS customer (ID INT AUTO_INCREMENT PRIMARY KEY, firstname VARCHAR(16))";
// }

//CustomerRepository repo = new(new ConnectionHelper<SQLiteConnection>("test.db"));
//repo.Add(new Customer() { Firstname = "Konrad"});

ConsolePlatform platform = new("Data Source=:memory:");
MainScreen screen = new(platform);
Screen.Display(screen);
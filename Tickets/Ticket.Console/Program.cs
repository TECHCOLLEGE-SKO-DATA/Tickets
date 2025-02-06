// See https://aka.ms/new-console-template for more information

using System.Data.SQLite;
using TicketLib.Models;
using Ticket.Console.Repository.SQLite;
using Ticket.Console.Screens;
using Ticket.Console.Platform;
using TECHCOOL.UI;

ConsolePlatform platform = new("Data Source=:memory:");
MainScreen screen = new(platform);
Screen.Display(screen);
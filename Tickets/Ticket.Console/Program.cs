// See https://aka.ms/new-console-template for more information

using System.Data.SQLite;
using TicketLib.Models;
using Ticket.Console.Repository.SQLite;
using (SQLiteConnection conn = new("Data Source=test.db")) 
{
    
    //var cmd = conn.CreateCommand();
    //cmd.CommandText = @"CREATE TABLE IF NOT EXISTS customer (ID INT AUTO_INCREMENT PRIMARY KEY, firstname VARCHAR(16))";

    CustomerRepository repo = new(conn);
    repo.Add(new Customer() { Firstname = "Konrad"});

}


Console.WriteLine("Hello, World!");

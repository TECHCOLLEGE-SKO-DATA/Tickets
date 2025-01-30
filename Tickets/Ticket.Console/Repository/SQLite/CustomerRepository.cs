using TicketLib.Repository;
using TicketLib.Models;
using System.Data.SQLite;

namespace Ticket.Console.Repository.SQLite;

class CustomerRepository : IRepository<Customer>
{
    SQLiteConnection _conn;
    public CustomerRepository(SQLiteConnection conn) 
    {
        _conn = new();
    }

    public IEnumerable<Customer> GetAll() 
    {
        //SELECT * FROM customer
        return null;
    }
    public Customer GetById(int id) 
    {
        //SELECT * FROM customer WHERR customerId = id
        return null;
    }
    public void Add(Customer model)
    {
        _conn.Open();
        SQLiteCommand command = _conn.CreateCommand();
        command.CommandText = "INSERT INTO customer (firstname) VALUES (@firstname)";
        command.Parameters.AddWithValue("@firstname", model.Firstname);
        command.ExecuteNonQuery();
        //INSERT INTO customer () VALUES ();
    }
    public void Update(Customer model) 
    {
        SQLiteCommand command = _conn.CreateCommand();
    }
    public void Delete(int id) 
    {

    }
}
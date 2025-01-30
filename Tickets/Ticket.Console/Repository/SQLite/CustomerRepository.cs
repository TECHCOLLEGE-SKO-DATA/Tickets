using TicketLib.Repository;
using TicketLib.Models;
using System.Data.SQLite;
using System.Data.Common;

namespace Ticket.Console.Repository.SQLite;

class CustomerRepository : IRepository<Customer>
{
    ConnectionHelper _connectionHelper;
    public CustomerRepository(ConnectionHelper connectionHelper) 
    {
        _connectionHelper = connectionHelper;
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
        using (DbConnection conn = _connectionHelper.GetConnection()) 
        {
            DbCommand command = conn.CreateCommand();
            command.CommandText = "INSERT INTO customer (firstname) VALUES (@firstname)";
            command.Parameters.Add("@firstname", DbType.VarChar, 30).Value = model.Firstname;
            command.ExecuteNonQuery();
        }
        
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
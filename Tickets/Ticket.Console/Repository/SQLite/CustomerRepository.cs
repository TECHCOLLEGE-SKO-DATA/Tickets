// using TicketLib.Repository;
// using TicketLib.Models;
// using System.Data.SQLite;
// using System.Data.Common;

// namespace Ticket.Console.Repository.SQLite;

// class CustomerRepository : IRepository<Customer>
// {
//     SQLiteConnectionHelper _connectionHelper;
//     public CustomerRepository(SQLiteConnectionHelper connectionHelper) 
//     {
//         _connectionHelper = connectionHelper;
//     }

//     public IEnumerable<Customer> GetAll() 
//     {
//         //SELECT * FROM customer
//         return null;
//     }
//     public Customer GetById(int id) 
//     {
//         using (SQLiteConnection conn = _connectionHelper.GetConnection()) 
//         {
//             SQLiteCommand command = conn.CreateCommand();
//             command.CommandText = "SELECT * FROM model WHERE @id";
//             command.Parameters.AddWithValue("@id", id);
//             command.ExecuteNonQuery();
//         }
//         //SELECT * FROM customer WHERR customerId = id
//         return null;
//     }
//     public void Add(Customer model)
//     {
//         using (SQLiteConnection conn = _connectionHelper.GetConnection()) 
//         {
//             SQLiteCommand command = conn.CreateCommand();
//             command.CommandText = "INSERT INTO customer (firstname) VALUES (@firstname)";
//             command.Parameters.AddWithValue("@firstname", model.Firstname);
//             command.ExecuteNonQuery();
//         }
        
//     }
//     public void Update(Customer model) 
//     {
//         using (SQLiteConnection conn = _connectionHelper.GetConnection() as SQLiteConnection) 
//         {
//             SQLiteCommand command = conn.CreateCommand();
//             command.CommandText = "UPDATE customer SET ... WHERE ...";
//             command.Parameters.AddWithValue("@firstname", model.Firstname);
//             command.ExecuteNonQuery();
//         }
//     }
//     public void Delete(int id) 
//     {
//         using (SQLiteConnection conn = _connectionHelper.GetConnection() as SQLiteConnection) 
//         {
//             SQLiteCommand command = conn.CreateCommand();
//             command.CommandText = "DELETE FROM ....";
//             command.Parameters.AddWithValue("@id", id);
//             command.ExecuteNonQuery();
//         }
//     }
// }
namespace TicketTests.Repository;
using System.Data.SQLite;

public class SQLiteMockDatabase
{
//     Person
// - PersonId INT (PK)
// - Firstname VARCHAR(24)
// - Middlename VARCHAR(32)
// - Lastname VARCHAR(24)
// - Address INT (FK)
// - RegisteredDate DATETIME
// - PreferredContactMethod INT (FK)
    const string _sql = @"
    CREATE TABLE person (
        personId INT,
        firstName VARCHAR(24) NOT NULL,
        middleName VARCHAR(24) NOT NULL DEFAULT '',
        lastName VARCHAR(24) NOT NULL,
        registeredDate TEXT NOT NULL,
        addressId INT,
        preferredContactMethodId INT,
        PRIMARY KEY(personId AUTO_INCREMENT)
    );
    INSERT INTO person (firstName, middleName, lastName, registeredDate, addressId, preferredContactMethodId) VALUES 
        ('Konrad', '', 'Sommer', '2010-03-29 12:09:12', 0, 0),
        ('Steen', 'Sachs', 'Pappy', '2010-03-29 12:09:12', 0, 0),
        ('Anne', '', 'Dam', '2010-06-01 08:54:36', 0, 0);
    ";
    public static void MockConnection(SQLiteConnection connection)
    {
        var cmd = connection.CreateCommand();
        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS customer (ID INT AUTO_INCREMENT PRIMARY KEY, firstname VARCHAR(16))";
        cmd.ExecuteNonQuery();
    }
}
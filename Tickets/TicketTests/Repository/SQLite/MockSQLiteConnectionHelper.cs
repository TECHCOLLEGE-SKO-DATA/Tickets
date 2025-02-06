namespace TicketTests.Repository.SQLite;
using System.Data.SQLite;
using System.Runtime.InteropServices;
using TicketLib.Repository;

public class MockSQLiteConnectionHelper : IConnectionHelper<SQLiteConnection>, IDisposable
{
    const string _sql = @"
    CREATE TABLE person (
        personId INTEGER PRIMARY KEY AUTOINCREMENT,
        firstName VARCHAR(24) NOT NULL,
        middleName VARCHAR(24) NOT NULL DEFAULT '',
        lastName VARCHAR(24) NOT NULL,
        registeredDate TEXT NOT NULL,
        addressId INT,
        preferredContactMethodId INT
    );
    INSERT INTO person (firstName, middleName, lastName, registeredDate, addressId, preferredContactMethodId) VALUES 
        ('Konrad', '', 'Sommer', '2010-03-29 12:09:12', 2, 0),
        ('Steen', 'Sachs', 'Pappy', '2010-03-29 12:09:12', 1, 0),
        ('Anne', '', 'Dam', '2010-06-01 08:54:36', 2, 0)
    ;
    
    CREATE TABLE city (
        cityId INTEGER PRIMARY KEY AUTOINCREMENT,
        zipCode VARCHAR(6),
        name VARCHAR(24)
    );
    INSERT INTO city (zipCode, name) VALUES
        (9000, 'Aalborg'),
        (9220, 'Aalborg Øst')
    ;

    CREATE TABLE address (
        addressId INTEGER PRIMARY KEY AUTOINCREMENT,
        street VARCHAR(32),
        number VARCHAR(8),
        cityId SMALLINT,
        FOREIGN KEY (cityId) REFERENCES city(cityId)
    );
    INSERT INTO address (street, number, cityId) VALUES
        ('Øster Uttrupvej', '5', 1),
        ('Struervej', '70', 2)
    ;
    ";

    
    string _connectionString = "";
    string _tempFile = "";
    public MockSQLiteConnectionHelper(string connectionString)
    {
        _connectionString = connectionString;
        
    }
    public MockSQLiteConnectionHelper()
    {
        _tempFile = Path.GetTempFileName();
        _connectionString = $"Data Source={_tempFile}";
        
        using SQLiteConnection conn = GetConnection();
        var cmd = conn.CreateCommand();
        cmd.CommandText = _sql;
        cmd.ExecuteNonQuery();    
    }
    public SQLiteConnection GetConnection()
    {
        SQLiteConnection conn = new SQLiteConnection(_connectionString);
        conn.Open();
        
        return conn;

    }
    public void Dispose()
    {
        // Delete the temporary file
        if (!string.IsNullOrEmpty(_tempFile) && File.Exists(_tempFile))
        {
            File.Delete(_tempFile);
        }
    }
}
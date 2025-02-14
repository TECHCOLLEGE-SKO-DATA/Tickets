namespace TicketTests.Repository.SQLite;
using System.Data.SQLite;
using System.Runtime.InteropServices;
using TicketLib.Repository;

public class MockSQLiteConnectionHelper : IConnectionHelper<SQLiteConnection>, IDisposable
{
    const string _sql = @$"
    CREATE TABLE IF NOT EXISTS City (
    CityId INTEGER PRIMARY KEY AUTOINCREMENT,
    ZipCode VARCHAR(6) NOT NULL,
    Name VARCHAR(24) NOT NULL
);

CREATE TABLE IF NOT EXISTS Address (
    AddressId INTEGER PRIMARY KEY AUTOINCREMENT,
    Street VARCHAR(32) NOT NULL,
    Number VARCHAR(8) NOT NULL,
    CityId SMALLINT NOT NULL,
    FOREIGN KEY (CityId) REFERENCES City(CityId)
);

CREATE TABLE IF NOT EXISTS Person (
    PersonId INTEGER PRIMARY KEY AUTOINCREMENT,
    Firstname VARCHAR(24) NOT NULL,
    Middlename VARCHAR(32),
    Lastname VARCHAR(24) NOT NULL,
    AddressId INT NOT NULL DEFAULT 0,
    RegisteredDate DATETIME NOT NULL,
    PreferredContactMethodId INT NOT NULL,
    FOREIGN KEY (AddressId) REFERENCES Address(AddressId) ON DELETE SET DEFAULT
);

CREATE TABLE IF NOT EXISTS Customer (
    PersonId INTEGER PRIMARY KEY,
    FOREIGN KEY (PersonId) REFERENCES Person(PersonId)
);

CREATE TABLE IF NOT EXISTS Employee (
    PersonId INTEGER PRIMARY KEY,
    Username VARCHAR(16) NOT NULL UNIQUE,
    Password CHAR(64) NOT NULL,
    FOREIGN KEY (PersonId) REFERENCES Person(PersonId)
);

CREATE TABLE IF NOT EXISTS ContactInfoType (
    ContactInfoTypeId INTEGER PRIMARY KEY AUTOINCREMENT,
    Name VARCHAR(16) NOT NULL,
    Icon CHAR(1)
);

CREATE TABLE IF NOT EXISTS ContactMethod (
    ContactMethodId INTEGER PRIMARY KEY AUTOINCREMENT,
    PersonId INT NOT NULL,
    ContactInfoType INT NOT NULL,
    Value VARCHAR(255) NOT NULL,
    FOREIGN KEY (PersonId) REFERENCES Person(PersonId),
    FOREIGN KEY (ContactInfoType) REFERENCES ContactInfoType(ContactInfoTypeId)
);

CREATE TABLE IF NOT EXISTS Incident (
    IncidentId INTEGER PRIMARY KEY AUTOINCREMENT,
    CustomerId INT NOT NULL, 
    AddressId INT,
    Status TINYINT NOT NULL,
    IssueDate DATETIME NOT NULL,
    IssueDescription TEXT NOT NULL,
    CreatedBy INT NOT NULL,
    ResolutionDate DATETIME,
    ResolutionDescription TEXT,
    FOREIGN KEY (CreatedBy) REFERENCES Employee(PersonId),
    FOREIGN KEY (CustomerId) REFERENCES Customer(PersonId) 
);

CREATE TABLE IF NOT EXISTS IncidentStatus (
    StatusId INTEGER PRIMARY KEY AUTOINCREMENT,
    Name VARCHAR(32) NOT NULL UNIQUE
);

INSERT OR IGNORE INTO IncidentStatus (StatusId, Name) VALUES
(1, 'Open'),
(2, 'InProgress'),
(3, 'OnHold'),
(4, 'Closed'),
(5, 'Afventer_SKO_HjemmeBesog'),
(6, 'Afventer_Bruger'),
(7, 'Genaabnet');

CREATE TABLE IF NOT EXISTS IncidentLog (
    IncidentLogId INTEGER PRIMARY KEY AUTOINCREMENT,
    ChangedBy INT NOT NULL,
    LogDescription TEXT NOT NULL,
    FOREIGN KEY (ChangedBy) REFERENCES Person(PersonId)
);

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
            for (int i = 0; i < 5; i++) 
            { 
                try
                {
                    File.Delete(_tempFile);
                } catch (IOException)
                {
                    Thread.Sleep(200);
                }
            }
        }
    }
}
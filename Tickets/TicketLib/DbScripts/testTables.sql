CREATE TABLE Address (
    AddressId INT PRIMARY KEY,
    Street VARCHAR(32) NOT NULL,
    Number VARCHAR(8) NOT NULL,
    CityId SMALLINT NOT NULL,
    FOREIGN KEY (CityId) REFERENCES City(CityId)
);

CREATE TABLE City (
    CityId SMALLINT PRIMARY KEY,
    ZipCode VARCHAR(6) NOT NULL,
    Name VARCHAR(24) NOT NULL
);

CREATE TABLE Person (
    PersonId INT PRIMARY KEY,
    Firstname VARCHAR(24) NOT NULL,
    Middlename VARCHAR(32),
    Lastname VARCHAR(24) NOT NULL,
    Address INT NOT NULL,
    RegisteredDate DATETIME NOT NULL,
    PreferredContactMethod INT NOT NULL,
    FOREIGN KEY (Address) REFERENCES Address(AddressId)
);

CREATE TABLE Customer(
    PersonId INT PRIMARY KEY,
    FOREIGN KEY (PersonId) REFERENCES Person(PersonId)
);

CREATE TABLE Staff (
    PersonId INT PRIMARY KEY,
    Username VARCHAR(16) NOT NULL UNIQUE,
    Password CHAR(64) NOT NULL,
    FOREIGN KEY (PersonId) REFERENCES Person(PersonId)
);

CREATE TABLE ContactInfoType (
    ContactInfoTypeId INT PRIMARY KEY,
    Name VARCHAR(16) NOT NULL,
    Icon CHAR(1)
);

CREATE TABLE ContactMethod (
    ContactMethodId INT PRIMARY KEY,
    PersonId INT NOT NULL,
    ContactInfoType INT NOT NULL,
    Value VARCHAR(255) NOT NULL,
    FOREIGN KEY (PersonId) REFERENCES Person(PersonId),
    FOREIGN KEY (ContactInfoType) REFERENCES ContactInfoType(ContactInfoTypeId)
);

CREATE TABLE Incident (
    IncidentId INT PRIMARY KEY,
    Status TINYINT NOT NULL,
    IssueDate DATETIME NOT NULL,
    IssueDescription TEXT NOT NULL,
    CreatedBy INT NOT NULL,
    ResolutionDate DATETIME,
    ResolutionDescription TEXT,
    FOREIGN KEY (CreatedBy) REFERENCES Staff(PersonId)
);

CREATE TABLE IncidentStatus (
    StatusId TINYINT PRIMARY KEY,
    Name VARCHAR(32) NOT NULL UNIQUE
);

INSERT INTO IncidentStatus (StatusId, Name) VALUES
(1, 'Open'),
(2, 'InProgress'),
(3, 'OnHold'),
(4, 'Closed'),
(5, 'Afventer_SKO_HjemmeBesog'),
(6, 'Afventer_Bruger'),
(7, 'Genaabnet');

CREATE TABLE IncidentLog (
    IncidentLogId INT PRIMARY KEY,
    ChangedBy INT NOT NULL,
    LogDescription TEXT NOT NULL,
    FOREIGN KEY (ChangedBy) REFERENCES Person(PersonId)
);

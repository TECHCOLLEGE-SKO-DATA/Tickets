using TicketLib.Repository;
using System.Data.SQLite;
using TicketLib.Models;
using Ticket.Console.Repository.SQLite;
namespace TicketTests.Repository.SQLite;

public class PersonRepositoryTests
{
    [Fact]
    public void PersonRepository_GetByIdTests()
    {
        using MockSQLiteConnectionHelper helper = new();

        //Create a mocked repository
        PersonRepository repo = new(helper);

        //Get Konrad Sommer (id 1)
        Person? konrad = repo.GetById(1);

        Assert.NotNull(konrad);
        Assert.Equal(1, konrad.PersonId);
        Assert.Equal("Konrad", konrad.Firstname);
        Assert.Equal("Sommer", konrad.Lastname);

        //Get Steen Sachs Pappy (id 2)
        Person? steen = repo.GetById(2);
        Assert.NotNull(steen);
        Assert.Equal(2, steen.PersonId);
        Assert.Equal("Steen", steen.Firstname);
        Assert.Equal("Sachs", steen.Middlename);
        Assert.Equal("Pappy", steen.Lastname);

        //Check GetById with invalid id returns null
        Assert.Null(repo.GetById(0));
    }

    [Fact]
    public void PersonRepository_AddTests()
    {
        using MockSQLiteConnectionHelper helper = new();
        PersonRepository repo = new(helper);
        Person remo = new () {
            Firstname = "Remo",
            Middlename = "",
            Lastname = "Lademann",
            Address = 1,
            RegisterdDate = DateTime.Now,
            PreferredContactMethod = 0
        };
        repo.Add(remo);
        Person? remoAgain = repo.GetById(4);
        Assert.NotNull(remoAgain);
        Assert.Equal(remo.Firstname, remoAgain.Firstname);
        Assert.Equal(remo.Lastname, remoAgain.Lastname);

        //Cannot add person with nonexisting address
        Person ella = new () {
            Firstname = "Ella",
            Middlename = "",
            Lastname = "Stick",
            Address = 0, //Does not exist
            RegisterdDate = DateTime.Now,
            PreferredContactMethod = 0
        };

        try {
            repo.Add(ella);
            Assert.Fail(); //An exception should have occurred
        } 
        catch(SQLiteException)
        {
            //Success! A SQLiteException was thrown. Lovely!
        }
        catch(Exception e)
        {
            Assert.IsType<SQLiteException>(e); //Should have been an SQLiteException
        }
    }

    [Fact]
    public void PersonRepository_GetAllTests()
    {
        using MockSQLiteConnectionHelper helper = new();
        PersonRepository repo = new(helper);

        List<Person> allPersons = (List<Person>) repo.GetAll();

        Assert.Equal(3, allPersons.Count);

        //Add another person
        Person remo = new () {
            Firstname = "Remo",
            Middlename = "",
            Lastname = "Lademann",
            Address = 1,
            RegisterdDate = DateTime.Now,
            PreferredContactMethod = 0
        };
        
        repo.Add(remo);
        allPersons = (List<Person>) repo.GetAll();
        Assert.Equal(4, allPersons.Count);
    }

    [Fact]
    public void PersonRepository_DeleteTests()
    {
        using (MockSQLiteConnectionHelper conn = new()) {
            PersonRepository repo = new(conn);

            List<Person> allPersons = (List<Person>) repo.GetAll();
            Assert.Equal(3, allPersons.Count);

            //Delete konrad
            repo.Delete(1);
            
            allPersons = (List<Person>) repo.GetAll();
            Assert.Equal(2,allPersons.Count);

            //Delete the rest
            repo.Delete(2);
            repo.Delete(3);
            
            allPersons = (List<Person>) repo.GetAll();
            Assert.Empty(allPersons);
        }
    }

    [Fact]
    public void PersonRepository_UpdateTests()
    {
        Person? konrad = null;
        using (MockSQLiteConnectionHelper conn = new()) {
            PersonRepository repo = new(conn);

            //Update Konrad Sommers address
            konrad = repo.GetById(1);
            Assert.NotNull(konrad);
            Assert.NotEqual(1, konrad.Address);
        }

        string firstname = "Mary", middlename = "Anne", lastname = "Hasmark";
        DateTime now = DateTime.Now;

        using (MockSQLiteConnectionHelper conn = new()) {
            PersonRepository repo = new(conn);

            //Update Konrad Sommers address
            konrad.Firstname = firstname;
            konrad.Middlename = middlename;
            konrad.Lastname = lastname;
            konrad.RegisterdDate = now;
            konrad.Address = 1;
            repo.Update(konrad);

            //Get Konrad and see the model was updated
            konrad = repo.GetById(1);
            Assert.NotNull(konrad);
            Assert.Equal(firstname, konrad.Firstname);
            Assert.Equal(middlename, konrad.Middlename);
            Assert.Equal(lastname, konrad.Lastname);
            Assert.Equal(now, konrad.RegisterdDate);
        }
    }
}
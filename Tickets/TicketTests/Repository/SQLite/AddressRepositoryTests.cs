using TicketLib.Repository;
using System.Data.SQLite;
using TicketLib.Models;
using Ticket.Console.Repository.SQLite;
using TicketLib;
namespace TicketTests.Repository.SQLite;

public class AddressRepositoryTests
{
    [Fact]
    public void AddressRepository_GetByIdTests()
    {
        using MockSQLiteConnectionHelper helper = new();


        AddressRepository repo = new(helper);

        
        Address? techcollege = repo.GetById(1);

        Assert.NotNull(techcollege);
        Assert.Equal(1, techcollege.CityId);
        Assert.Equal("Øster Uttrupvej", techcollege.Street);
        Assert.Equal("5", techcollege.Number);
        Assert.Equal(1, techcollege.CityId);

       
        Address? aalborgøst = repo.GetById(2);
        Assert.NotNull(aalborgøst);
        Assert.Equal(2, aalborgøst.CityId);
        Assert.Equal("Struervej", aalborgøst.Street);
        Assert.Equal("70", aalborgøst.Number);
        Assert.Equal(2, aalborgøst.CityId);



        Assert.Null(repo.GetById(0));
    }

    [Fact]
    public void AddressRepository_AddTests()
    {
        using MockSQLiteConnectionHelper helper = new();
        AddressRepository repo = new(helper);
        Address cambodian = new()
        {
            Street = "Cambodian",
            Number = "9999",
            AddressId = 1,
            CityId = 1,

        };
        repo.Add(cambodian);
        
        
        Assert.Equal("Cambodian", cambodian.Street);
        Assert.Equal("9999", cambodian.Number);
        Assert.Equal(1, cambodian.CityId);

        
        Address newjercy = new()
        {
            Street = "New Jercy",
            Number = "12",
            AddressId = 1,
            CityId = 0, 
            
        };

        try
        {
            repo.Add(newjercy);
            Assert.Fail(); 
        }
        catch (SQLiteException)
        {
            
        }
        catch (Exception e)
        {
            Assert.IsType<SQLiteException>(e); 
        }
    }

    [Fact]
    public void AddressRepository_GetAllTests()
    {
        using MockSQLiteConnectionHelper helper = new();
        AddressRepository repo = new(helper);

        List<Address> allCitys = (List<Address>)repo.GetAll();

        Assert.Equal(2, allCitys.Count);

        
        Address remo = new()
        {
            Street = "Cambodian",
            Number = "9999",
            AddressId = 1,
            CityId = 2,
        };

        repo.Add(remo);
        allCitys = (List<Address>)repo.GetAll();
        Assert.Equal(3, allCitys.Count);
    }

    [Fact]
    public void AddressRepository_DeleteTests()
    {
        using (MockSQLiteConnectionHelper conn = new())
        {
            AddressRepository repo = new(conn);

            List<Address> allAdresses = (List<Address>)repo.GetAll();
            Assert.Equal(2, allAdresses.Count);

           
            repo.Delete(1);

            allAdresses = (List<Address>)repo.GetAll();
            Assert.Equal(1, allAdresses.Count);

           
            repo.Delete(1);
            repo.Delete(2);

            allAdresses = (List<Address>)repo.GetAll();
            Assert.Empty(allAdresses);
        }
    }

    [Fact]
    public void AddressRepository_UpdateTests()
    {
        Address? aalborg = null;
        using (MockSQLiteConnectionHelper conn = new())
        {
            AddressRepository repo = new(conn);

            
            aalborg = repo.GetById(1);
            Assert.NotNull(aalborg);
            Assert.NotEqual(2, aalborg.CityId);
        }

        string street = "Aalborg Øst", number = "9000";
        
        using (MockSQLiteConnectionHelper conn = new())
        {
            AddressRepository repo = new(conn);

           
            aalborg.Street = street;
            aalborg.Number = number;
            aalborg.CityId = 1;
            aalborg.AddressId = 1;
            repo.Update(aalborg);

            
            aalborg = repo.GetById(1);
            Assert.NotNull(aalborg);
            Assert.Equal(street, aalborg.Street);
            Assert.Equal(number, aalborg.Number);
            
        }
    }
}
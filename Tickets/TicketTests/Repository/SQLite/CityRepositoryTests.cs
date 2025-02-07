using TicketLib.Repository;
using System.Data.SQLite;
using TicketLib.Models;
using Ticket.Console.Repository.SQLite;
using TicketLib;
namespace TicketTests.Repository.SQLite;

public class CityRepositoryTests
{
    [Fact]
    public void CityRepository_GetByIdTests()
    {
        using MockSQLiteConnectionHelper helper = new();

        
        CityRepository repo = new(helper);

        
        City? aalborg = repo.GetById(1);

        Assert.NotNull(aalborg);
        Assert.Equal(1, aalborg.CityId);
        Assert.Equal("Aalborg", aalborg.Name);
        Assert.Equal("9000", aalborg.ZipCode);

       
        City? aalborgøst = repo.GetById(2);
        Assert.NotNull(aalborgøst);
        Assert.Equal(2, aalborgøst.CityId);
        Assert.Equal("Aalborg Øst", aalborgøst.Name);
        Assert.Equal("9000", aalborg.ZipCode);


        
        Assert.Null(repo.GetById(0));
    }

    [Fact]
    public void CityRepository_AddTests()
    {
        using MockSQLiteConnectionHelper helper = new();
        CityRepository repo = new(helper);
        City cambodian = new()
        {
            Name = "Cambodian",
            ZipCode = "9999",
            CityId = 1,

        };
        repo.Add(cambodian);
        
        
        Assert.Equal("Cambodian", cambodian.Name);
        Assert.Equal("9999", cambodian.ZipCode);
        Assert.Equal(1, cambodian.CityId);

        
        City newjercy = new()
        {
            Name = "New Jercy",
            ZipCode = "12",            
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
    public void CityRepository_GetAllTests()
    {
        using MockSQLiteConnectionHelper helper = new();
        CityRepository repo = new(helper);

        List<City> allCitys = (List<City>)repo.GetAll();

        Assert.Equal(2, allCitys.Count);

        
        City remo = new()
        {
            Name = "Cambodian",
            ZipCode = "9999",
            CityId = 1,
        };

        repo.Add(remo);
        allCitys = (List<City>)repo.GetAll();
        Assert.Equal(3, allCitys.Count);
    }

    [Fact]
    public void CityRepository_DeleteTests()
    {
        using (MockSQLiteConnectionHelper conn = new())
        {
            CityRepository repo = new(conn);

            List<City> allCitys = (List<City>)repo.GetAll();
            Assert.Equal(2, allCitys.Count);

           
            repo.Delete(1);

            allCitys = (List<City>)repo.GetAll();
            Assert.Equal(1, allCitys.Count);

           
            repo.Delete(1);
            repo.Delete(2);

            allCitys = (List<City>)repo.GetAll();
            Assert.Empty(allCitys);
        }
    }

    [Fact]
    public void CityRepository_UpdateTests()
    {
        City? aalborg = null;
        using (MockSQLiteConnectionHelper conn = new())
        {
            CityRepository repo = new(conn);

            
            aalborg = repo.GetById(1);
            Assert.NotNull(aalborg);
            Assert.NotEqual(2, aalborg.CityId);
        }

        string name = "Aalborg Øst", zipcode = "9000";
        
        using (MockSQLiteConnectionHelper conn = new())
        {
            CityRepository repo = new(conn);

           
            aalborg.Name = name;
            aalborg.ZipCode = zipcode;
            aalborg.CityId = 1;
            repo.Update(aalborg);

            
            aalborg = repo.GetById(1);
            Assert.NotNull(aalborg);
            Assert.Equal(name, aalborg.Name);
            Assert.Equal(zipcode, aalborg.ZipCode);
            
        }
    }
}
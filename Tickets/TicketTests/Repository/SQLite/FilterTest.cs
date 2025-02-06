using Ticket.Console.Repository.SQLite;
using TicketLib.Models;
namespace FilterUnitTest;

public class FilterTest
{
    [Fact]
    public void FilterTests()
    {
        /*var test = Filter.Where("personId").Is(1);
        Assert.Equal("personId=1", test.ToString());

        test = Filter.Where("name").Is(1)
            .And("test").Is("hm");

        Assert.Equal("name=1 AND test=\"hm\"", test.ToString());*/

        Person p = new Person {
            FirstName = "Konrad"
        };

        var filter = Filter.Where(() => p.FirstName == "Konrad");
        Assert.Equal("FirstName=\"Konrad\"", filter.ToString());

    }
}
public class Node
{
    public object Value = "";
    public Node? Next = null;
    public Node(object key)
    {
        Value = key;
    }
}
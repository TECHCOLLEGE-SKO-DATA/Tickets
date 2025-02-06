using TECHCOOL.UI;
using TicketLib.Models;
using TicketLib.Platform;

namespace Ticket.Console.Screens;

public class EditPersonScreen : TicketScreen
{
    public override string Title { get; set; } = "Edit ";
    Person _person = new();

    public EditPersonScreen(IPlatform platform) : base(platform) 
    {

    }
    public EditPersonScreen(IPlatform platform, Person person) : base(platform)
    {
        _person = person;
    }

    protected override void Draw()
    {
        Form<Person> form = new();
        
        form.TextBox("First name", nameof(_person.FirstName));
        form.TextBox("Middle name", nameof(_person.MiddleName));
        form.TextBox("Last name", nameof(_person.LastName));
        
        if (!form.Edit(_person)) 
        {
            _platform.Database.Persons.Add(_person);
            Quit();
        } 
        else 
        {

        }
    }
}
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
        
        form.TextBox("First name", nameof(_person.Firstname));
        form.TextBox("Middle name", nameof(_person.Middlename));
        form.TextBox("Last name", nameof(_person.Lastname));
        
        if (!form.Edit(_person)) 
        {
            Quit();
        } 
        else 
        {

        }
    }
}
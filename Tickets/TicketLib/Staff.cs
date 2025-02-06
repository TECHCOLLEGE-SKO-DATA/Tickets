namespace TicketLib;

public class Staff : IModel
{
    public int PersonId { get; set; } //(PK, FK)
    public string Username { get; set; }

    public char Password { get; set; }

    //(Dette er til når vi opretter en medarbeder / User for og se om de har kørekort eller ej)
    public bool IsDriversLicenceValid { get; set; }

    // Profil billede til (sidennav) så man kan se man er logget ind.
    public char ProfilePicture { get; set; }

    public string Validate()
    {
        throw new NotImplementedException();
    }
}
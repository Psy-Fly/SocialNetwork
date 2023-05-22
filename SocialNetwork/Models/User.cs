using Microsoft.AspNetCore.Identity;

namespace SocialNetwork.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
}
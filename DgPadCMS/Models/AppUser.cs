using Microsoft.AspNetCore.Identity;

namespace DgPadCMS.Models
{
    public class AppUser : IdentityUser

    {
        public string Occupation { get; set; }

    }
}

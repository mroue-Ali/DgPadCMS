using System.ComponentModel.DataAnnotations;

namespace DgPadCMS.Models
{
    public class User
    {
        [Required ,MinLength(2,ErrorMessage ="Minimum is 2")]
        public string UserName{ get; set; }
        [Required,EmailAddress]
        public string Email{ get; set; }
        [DataType(DataType.Password),Required]
        public string Password{ get; set; }
        public User() { }
        public User(AppUser appUser)
        {
            UserName = appUser.UserName;
            Email = appUser.Email;
            Password = appUser.PasswordHash;
        }
    }
}

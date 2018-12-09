using Microsoft.AspNetCore.Identity;

namespace EZChat.Master.Identity
{
    public class AppUser : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

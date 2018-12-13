using EZChat.Master.Identity;
using EZChat.Master.Identity.Models;

namespace EZChat.Master.Database.DTO
{
    public class AppUserDto
    {
        public string UserName { get; }
        public string NormalizedUserName { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string PasswordHash { get; }

        public AppUserDto(AppUser source)
        {
            UserName = source.UserName;
            NormalizedUserName = source.NormalizedUserName;
            FirstName = source.FirstName;
            LastName = source.LastName;
            PasswordHash = source.PasswordHash;
        }
    }
}

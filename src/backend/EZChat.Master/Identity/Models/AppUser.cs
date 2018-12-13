namespace EZChat.Master.Identity.Models
{
    public class AppUser
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
    }
}

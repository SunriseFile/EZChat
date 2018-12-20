namespace EZChat.Master.Identity.Models
{
    public class PublicAppUser
    {
        public int Id { get; }
        public string UserName { get; }
        public string DisplayName { get; }

        public PublicAppUser(AppUser user)
        {
            Id = user.Id;
            UserName = user.UserName;
            DisplayName = user.DisplayName;
        }
    }
}

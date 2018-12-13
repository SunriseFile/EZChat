using EZChat.Master.Identity.Models;

namespace EZChat.Master.Identity.Services
{
    public interface IJsonWebTokenGenerator
    {
        JsonWebToken Generate(AppUser user);
    }
}

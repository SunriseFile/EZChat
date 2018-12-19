using System.Threading.Tasks;

using EZChat.Master.Identity.Models;

namespace EZChat.Master.Database.Repositories
{
    public interface IUserRepository
    {
        Task<int> InsertAsync(AppUser user);
        Task UpdateAsync(AppUser user);
        Task DeleteAsync(AppUser user);
        Task<AppUser> GetByIdAsync(int id);
        Task<AppUser> GetByNormalizedUserNameAsync(string normalizedUserName);
    }
}

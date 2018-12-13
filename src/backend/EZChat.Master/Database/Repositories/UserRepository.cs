using System.Threading.Tasks;

using Dapper;

using EZChat.Master.Database.QueryObject;
using EZChat.Master.Identity;
using EZChat.Master.Identity.Models;

namespace EZChat.Master.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _factory;

        public UserRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<long> InsertAsync(AppUser user)
        {
            using (var connection = await _factory.OpenAsync())
            {
                var sql = AppUserQueryObject.Insert(user);
                return await connection.QuerySingleAsync<long>(sql);
            }
        }

        public async Task UpdateAsync(AppUser user)
        {
            using (var connection = await _factory.OpenAsync())
            {
                var sql = AppUserQueryObject.Update(user);
                await connection.ExecuteAsync(sql);
            }
        }

        public async Task DeleteAsync(AppUser user)
        {
            using (var connection = await _factory.OpenAsync())
            {
                var sql = AppUserQueryObject.Delete(user);
                await connection.ExecuteAsync(sql);
            }
        }

        public async Task<AppUser> GetByIdAsync(long id)
        {
            using (var connection = await _factory.OpenAsync())
            {
                var sql = AppUserQueryObject.ById(id);
                return await connection.QuerySingleOrDefaultAsync<AppUser>(sql);
            }
        }

        public async Task<AppUser> GetByNormalizedUserNameAsync(string normalizedUserName)
        {
            using (var connection = await _factory.OpenAsync())
            {
                var sql = AppUserQueryObject.ByNormalizedUserName(normalizedUserName);
                return await connection.QuerySingleOrDefaultAsync<AppUser>(sql);
            }
        }
    }
}

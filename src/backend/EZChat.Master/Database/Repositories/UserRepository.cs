using System.Threading.Tasks;

using Dapper;

using EZChat.Master.Database.QueryObject;
using EZChat.Master.Identity.Models;

namespace EZChat.Master.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _factory;
        private readonly AppUserQueryObject _queryObject;

        public UserRepository(IDbConnectionFactory factory, AppUserQueryObject queryObject)
        {
            _factory = factory;
            _queryObject = queryObject;
        }

        public async Task<int> InsertAsync(AppUser user)
        {
            using (var connection = await _factory.OpenAsync())
            {
                var sql = _queryObject.Insert(user);
                return await connection.QuerySingleAsync<int>(sql);
            }
        }

        public async Task UpdateAsync(AppUser user)
        {
            using (var connection = await _factory.OpenAsync())
            {
                var sql = _queryObject.Update(user);
                await connection.ExecuteAsync(sql);
            }
        }

        public async Task DeleteAsync(AppUser user)
        {
            using (var connection = await _factory.OpenAsync())
            {
                var sql = _queryObject.Delete(user);
                await connection.ExecuteAsync(sql);
            }
        }

        public async Task<AppUser> GetByIdAsync(int id)
        {
            using (var connection = await _factory.OpenAsync())
            {
                var sql = _queryObject.ById(id);
                return await connection.QuerySingleOrDefaultAsync<AppUser>(sql);
            }
        }

        public async Task<AppUser> GetByNormalizedUserNameAsync(string normalizedUserName)
        {
            using (var connection = await _factory.OpenAsync())
            {
                var sql = _queryObject.ByNormalizedUserName(normalizedUserName);
                return await connection.QuerySingleOrDefaultAsync<AppUser>(sql);
            }
        }
    }
}

using System.Threading.Tasks;

using DbExtensions;

using EZChat.Master.Identity;

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
                var sql = SQL.INSERT_INTO($"users (" +
                                          $"{nameof(user.UserName)}," +
                                          $"{nameof(user.NormalizedUserName)}," +
                                          $"{nameof(user.FirstName)}," +
                                          $"{nameof(user.LastName)}," +
                                          $"{nameof(user.PasswordHash)})")
                             .VALUES(user.UserName,
                                     user.NormalizedUserName,
                                     user.FirstName,
                                     user.LastName,
                                     user.PasswordHash)
                    .SELECT("CAST(SCOPE_IDENTITY() as int)");
            }

            return 1;
        }
    }
}

using System.Threading.Tasks;

using Dapper;

using EZChat.Master.Database.Sql;
using EZChat.Master.Identity;

namespace EZChat.Master.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const string TableName = "users";
        private readonly IDbConnectionFactory _factory;

        private string Id => nameof(AppUser.Id);
        private string UserName => nameof(AppUser.UserName);
        private string NormalizedUserName => nameof(AppUser.NormalizedUserName);
        private string FirstName => nameof(AppUser.FirstName);
        private string LastName => nameof(AppUser.LastName);
        private string PasswordHash => nameof(AppUser.PasswordHash);

        private string IdParam => $"@{Id}";
        private string UserNameParam => $"@{UserName}";
        private string NormalizedUserNameParam => $"@{NormalizedUserName}";
        private string FirstNameParam => $"@{FirstName}";
        private string LastNameParam => $"@{LastName}";
        private string PasswordHashParam => $"@{PasswordHash}";

        public UserRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<long> InsertAsync(AppUser user)
        {
            using (var connection = await _factory.OpenAsync())
            {
                var insert = new SqlInsertBuilder(TableName)
                             .Values(UserName, UserNameParam)
                             .Values(NormalizedUserName, NormalizedUserNameParam)
                             .Values(FirstName, FirstNameParam)
                             .Values(LastName, LastNameParam)
                             .Values(PasswordHash, PasswordHashParam);

                var join = new SqlJoinBuilder()
                           .Append(insert)
                           .Append(new SqlCustomBuilder("SELECT CAST(SCOPE_IDENTITY() AS BIGINT)"));

                return await connection.QuerySingleAsync<long>(join.ToSql(), user);
            }
        }

        public async Task UpdateAsync(AppUser user)
        {
            using (var connection = await _factory.OpenAsync())
            {
                var update = new SqlUpdateBuilder(TableName)
                             .Set(UserName, UserNameParam)
                             .Set(NormalizedUserName, NormalizedUserNameParam)
                             .Set(FirstName, FirstNameParam)
                             .Set(LastName, LastNameParam)
                             .Set(PasswordHash, PasswordHashParam)
                             .Where(Id, SqlBuilder.Eq, IdParam);

                await connection.ExecuteAsync(update.ToSql(), user);
            }
        }

        public async Task DeleteAsync(AppUser user)
        {
            using (var connection = await _factory.OpenAsync())
            {
                var delete = new SqlDeleteBuilder(TableName).Where(Id, SqlBuilder.Eq, IdParam);
                await connection.ExecuteAsync(delete.ToSql(), user);
            }
        }

        public async Task<AppUser> GetByIdAsync(long id)
        {
            using (var connection = await _factory.OpenAsync())
            {
                var select = new SqlSelectBuilder(TableName).Where(Id, SqlBuilder.Eq, $"@{nameof(id)}");
                return await connection.QuerySingleOrDefaultAsync<AppUser>(select.ToSql(), new { id });
            }
        }

        public async Task<AppUser> GetByNormalizedUserNameAsync(string normalizedUserName)
        {
            using (var connection = await _factory.OpenAsync())
            {
                var select = new SqlSelectBuilder(TableName).Where(NormalizedUserName, SqlBuilder.Eq, $"@{nameof(normalizedUserName)}");
                return await connection.QuerySingleOrDefaultAsync<AppUser>(select.ToSql(), new { normalizedUserName });
            }
        }
    }
}

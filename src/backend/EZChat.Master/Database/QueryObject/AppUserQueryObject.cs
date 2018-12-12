using EZChat.Master.Identity;

using SqlKata;

namespace EZChat.Master.Database.QueryObject
{
    public static class AppUserQueryObject
    {
        private const string TableName = "users";

        public static string Insert(AppUser user)
        {
            return new Query(TableName)
                   // TODO: [Id] field in INSERT, hmmm, maybe DTO?
                   .AsInsert(user, true)
                   .CompileQuery();
        }

        public static string Update(AppUser user)
        {
            return new Query(TableName)
                   .AsUpdate(user)
                   .Where(nameof(AppUser.Id), "=", user.Id)
                   .CompileQuery();
        }

        public static string Delete(AppUser user)
        {
            return new Query(TableName)
                   .AsDelete()
                   .Where(nameof(AppUser.Id), "=", user.Id)
                   .CompileQuery();
        }

        public static string ById(long id)
        {
            return new Query(TableName)
                   .Where(nameof(AppUser.Id), "=", id)
                   .CompileQuery();
        }

        public static string ByNormalizedUserName(string normalizedUserName)
        {
            return new Query(TableName)
                   .Where(nameof(AppUser.NormalizedUserName), "=", normalizedUserName)
                   .CompileQuery();
        }
    }
}

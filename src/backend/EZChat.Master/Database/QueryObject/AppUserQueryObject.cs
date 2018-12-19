using System.Collections.Generic;

using EZChat.Master.Identity.Models;

using SqlKata;

namespace EZChat.Master.Database.QueryObject
{
    // TODO: Until I found a good way to set it all up correctly and beautifully
    public class AppUserQueryObject
    {
        private const string TableName = "users";

        private readonly Dictionary<string, string> _columnsMapByModel = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _columnsMapByRemote = new Dictionary<string, string>();

        private string IdProp => _columnsMapByModel[nameof(AppUser.Id)];
        private string UserNameProp => _columnsMapByModel[nameof(AppUser.UserName)];
        private string NormalizedUserNameProp => _columnsMapByModel[nameof(AppUser.NormalizedUserName)];
        private string DisplayNameProp => _columnsMapByModel[nameof(AppUser.DisplayName)];
        private string PasswordHashProp => _columnsMapByModel[nameof(AppUser.PasswordHash)];

        public AppUserQueryObject()
        {
            MapDapperColumn(nameof(AppUser.Id), "id");
            MapDapperColumn(nameof(AppUser.UserName), "username");
            MapDapperColumn(nameof(AppUser.NormalizedUserName), "normalized_username");
            MapDapperColumn(nameof(AppUser.DisplayName), "display_name");
            MapDapperColumn(nameof(AppUser.PasswordHash), "password_hash");
            SqlExtensions.MapDapperColumns<AppUser>(_columnsMapByRemote);
        }

        public string Insert(AppUser user)
        {
            return new Query(TableName)
                   .AsInsert(GetInsertUpdateData(user), true)
                   .CompileQuery();
        }

        public string Update(AppUser user)
        {
            return new Query(TableName)
                   .AsUpdate(GetInsertUpdateData(user))
                   .Where(IdProp, "=", user.Id)
                   .CompileQuery();
        }

        public string Delete(AppUser user)
        {
            return new Query(TableName)
                   .AsDelete()
                   .Where(IdProp, "=", user.Id)
                   .CompileQuery();
        }

        public string ById(int id)
        {
            return new Query(TableName)
                   .Where(IdProp, "=", id)
                   .CompileQuery();
        }

        public string ByNormalizedUserName(string normalizedUserName)
        {
            return new Query(TableName)
                   .Where(NormalizedUserNameProp, "=", normalizedUserName)
                   .CompileQuery();
        }

        private void MapDapperColumn(string modelKey, string remoteKey)
        {
            _columnsMapByModel.Add(modelKey, remoteKey);
            _columnsMapByRemote.Add(remoteKey, modelKey);
        }

        private Dictionary<string, object> GetInsertUpdateData(AppUser user)
        {
            return new Dictionary<string, object>
            {
                [UserNameProp] = user.UserName,
                [NormalizedUserNameProp] = user.NormalizedUserName,
                [DisplayNameProp] = user.DisplayName,
                [PasswordHashProp] = user.PasswordHash
            };
        }
    }
}

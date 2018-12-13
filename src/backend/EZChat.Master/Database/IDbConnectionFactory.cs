using System.Data;
using System.Threading.Tasks;

namespace EZChat.Master.Database
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> OpenAsync();
    }
}

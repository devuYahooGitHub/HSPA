using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IUserRepository
    {
         Task<user> Authenticate(string userName, string password);
    }
}
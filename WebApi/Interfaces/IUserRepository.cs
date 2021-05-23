using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Interfaces
{
    public interface IUserRepository
    {
         Task<user> Authenticate(string userName, string password);
         void Register(string userName, string password);
         Task<bool> UserAlreadyExists(string userName);

    }
}
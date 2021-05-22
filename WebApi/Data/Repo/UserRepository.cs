using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dc;

        public UserRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public async Task<user> Authenticate(string userName, string password)
        {
            //throw new System.NotImplementedException();
            return await dc.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
        }
    }
}
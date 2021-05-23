using System;
using System.Security.Cryptography;
using System.Text;
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
        public async Task<user> Authenticate(string userName, string passwordText)
        {                       
            //throw new System.NotImplementedException();
            var user =  await dc.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if(user==null || user.PasswordKey==null)
                return null;

            if (!MatchPassswordHash(passwordText, user.Password, user.PasswordKey))
                return null;
            
            return user;
        }

        private bool MatchPassswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {
            using(var hmac= new HMACSHA512(passwordKey))
            {
               var passwordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordText));
                        
                for(int i=0; i<passwordHash.Length; i++)
                {
                    if(passwordHash[i] != password[i])
                    return false;
                }                
            }
            return true;
        }

        public void Register(string userName, string password)
        {
            byte[] passwordHash, passwordKey;
            using (var hmac=new HMACSHA512()){
                passwordKey=hmac.Key;
                passwordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            user userNew = new user();
            userNew.UserName=userName;
            userNew.Password= passwordHash;
            userNew.PasswordKey=passwordKey;

            dc.Users.Add(userNew);
        }

        public async Task<bool> UserAlreadyExists(string userName)
        {
           // throw new System.NotImplementedException();
           return await dc.Users.AnyAsync(x=> x.UserName==userName);
        }

    }
}
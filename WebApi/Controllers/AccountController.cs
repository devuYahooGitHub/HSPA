using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Data.Repo;
using WebApi.Dtos;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IConfiguration configuration;

        public AccountController(IUnitOfWork uow, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.uow = uow;
        }

        //api/account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto loginReq)
        {
            var user = await uow.UserRepository.Authenticate(loginReq.UserName, loginReq.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var loginRes = new LoginResDto();
            loginRes.UserName = user.UserName;
            loginRes.Token = CreateJWT(user);
            return Ok(loginRes);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginReqDto loginReq)
        {
            if(await uow.UserRepository.UserAlreadyExists(loginReq.UserName)) 
                return BadRequest("User already exists, Please try something else");
            
            uow.UserRepository.Register(loginReq.UserName,loginReq.Password);
            await uow.SaveAsync();
            return StatusCode(201);

        }
        private string CreateJWT(user user)
        {
            var secretKey = configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var claims = new Claim[]{
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var signingCredentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
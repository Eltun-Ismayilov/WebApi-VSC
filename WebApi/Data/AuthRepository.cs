using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;

namespace WebApi.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;

        public AuthRepository(DataContext context,IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var respons=new ServiceResponse<string>();
            var user=await context.Users.FirstOrDefaultAsync(x=>x.Username.ToLower().Equals(username.ToLower()));
            if (user==null)
            {
                respons.Success=false;
                respons.Message="User not found. ";
            }
            else if(!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt))
            {
                respons.Success=false;
                respons.Message="Wrong password. ";
            }
            else
            {
                respons.Data=CreateToken(user);
            }
            return respons;

        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
             ServiceResponse<int> response=new ServiceResponse<int>();
             if(await UserExists(user.Username))
             {
                 response.Success=false;
                 response.Message="User already exists";
                 return response;
             }


            CreatePasswordHash(password,out byte[]passwordHash,out byte[]passwordSalt);

            user.PasswordHash=passwordHash;
            user.PasswordSalt=passwordSalt;

            context.Add(user);
            await context.SaveChangesAsync();
            response.Data=user.Id;
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await context.Users.AnyAsync(x=>x.Username.ToLower().Equals(username.ToLower())))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash,out byte[] passwordSalt)


        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt=hmac.Key;
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }

        }

        private bool VerifyPasswordHash(string password,  byte[] passwordHash, byte[] passwordSalt)

        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computerHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computerHash.Length; i++)
                {
                    if(computerHash[i]!=computerHash[i])
                    {
                        return false;
                    }
                }
               return true;

            }
        }

        private string CreateToken(User user)
        {
            var claims=new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Username)
            };
            var key= new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("Appsettings:Token").Value));

            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescription=new SecurityTokenDescriptor
            {
                Subject=new ClaimsIdentity(claims),
                Expires=System.DateTime.Now.AddDays(1),
                SigningCredentials=creds
            };
            var tokenHandler=new JwtSecurityTokenHandler();
            var token=tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}
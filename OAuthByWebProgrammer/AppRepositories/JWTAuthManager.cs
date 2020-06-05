using Microsoft.IdentityModel.Tokens;
using OAuthByWebProgrammer.AppInterfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OAuthByWebProgrammer.AppRepositories
{
    public class JWTAuthManager : IJWTAuthManager
    {
        IDictionary<string, string> _users = new Dictionary<string, string>
        {
             {"Sample1","password1"},
             {"Sample2","password2"}
         };

        private readonly string key;
        public JWTAuthManager(string key)
        {
            this.key = key;
        }
        public string Authentication(string username, string password)
        {
            if (!_users.Any(u => u.Key == username && u.Value == password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name,username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
using OAuthByWebProgrammer.AppInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAuthByWebProgrammer.AppRepositories
{
    public class CustomAuthenticationManaher : ICustomAuthenticationManaher
    {
         List<User> _users = new List<User>
         {
            new User {UserName ="Sample1", Password = "password1", Role = "Adminstrator"},
            new User {UserName = "Sample2", Password = "password2",Role = "User"}
         };
        private readonly IDictionary<string, Tuple<string, string>> tokens =
            new Dictionary<string, Tuple<string, string>>();
        public IDictionary<string, Tuple<string,string>> Tokens => tokens;
       



        public string Authenticate(string username, string password)
        {
            if (!_users.Any(u => u.UserName == username && u.Password == password))
            {
                return null;
            }
            var token = Guid.NewGuid().ToString();
            tokens.Add(token, new Tuple<string,string>(username, _users.FirstOrDefault(u => u.UserName == username && u.Password == password).Role));

            return token;
        }
    }
}

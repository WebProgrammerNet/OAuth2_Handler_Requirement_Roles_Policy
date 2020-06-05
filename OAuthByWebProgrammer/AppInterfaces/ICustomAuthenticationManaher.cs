using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAuthByWebProgrammer.AppInterfaces
{
    public interface ICustomAuthenticationManaher
    {
        string Authenticate(string username, string password);
        IDictionary<string, Tuple<string, string>> Tokens { get; }
    }
}

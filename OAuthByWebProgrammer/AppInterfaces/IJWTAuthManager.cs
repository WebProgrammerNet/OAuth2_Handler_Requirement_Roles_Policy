using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAuthByWebProgrammer.AppInterfaces
{
   public interface IJWTAuthManager
    {
        string Authentication(string username, string password);
    }
}

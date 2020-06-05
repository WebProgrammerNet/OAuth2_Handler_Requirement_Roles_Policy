using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAuthByWebProgrammer.JwtRequirement
{
    public class EmployeeWithMoreYearsRequirement : IAuthorizationRequirement
    {
        public int Years { get; set; }
        public EmployeeWithMoreYearsRequirement(int years)
        {
            this.Years = years;
        }
    }
}

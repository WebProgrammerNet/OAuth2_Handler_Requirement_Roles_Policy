using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OAuthByWebProgrammer.JwtRequirement
{
    //AQuthorization Requirement clasindan Mid=ras almis bir class lazimdir bize 
    //biz custom HandlerRequirement classinda AuthorizationHandler<T> T burda IAuthorizationRequirement classindan ,iras almis bir deyer olmali

    public class EmployeWithMoreYearsHandler : AuthorizationHandler<EmployeeWithMoreYearsRequirement>
    {
        private readonly IEmployeNumbersOfYears yearsNumber;
        public EmployeWithMoreYearsHandler(IEmployeNumbersOfYears employeNumbersOfYears)
        {
            this.yearsNumber = employeNumbersOfYears;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            EmployeeWithMoreYearsRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Name))
            {
                return Task.CompletedTask;
            }
            var name = context.User.FindFirst(c => c.Type == ClaimTypes.Name);
            var yearsofexperience = yearsNumber.GetName(name.Value);
            if (yearsofexperience >= requirement.Years)
            {
                context.Succeed(requirement); 
            }
            return Task.CompletedTask;
        }
    }
}


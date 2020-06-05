using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OAuthByWebProgrammer.AppInterfaces;

namespace OAuthByWebProgrammer.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        //private IJWTAuthManager _jWTAuthManager;
        private readonly ICustomAuthenticationManaher _customAuthenticationManaher;

        public NameController(ICustomAuthenticationManaher customAuthenticationManaher)//IJWTAuthManager jWTAuthManager
        {
            _customAuthenticationManaher = customAuthenticationManaher;
        }
        [Authorize]
        // GET: api/Name
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [AllowAnonymous]
        // GET: api/Name/5
        [HttpGet("{id}", Name = "Getnum")]
        public string Getnum()
        {
            return "value";
        }

        //[AllowAnonymous]
        // [HttpPost("authenticate")]
        // public IActionResult Authennticate([FromBody] UserCred userCred)
        // {
        //     var token = _jWTAuthManager.Authentication(userCred.UserName, userCred.Password);
        //     if (token == null)
        //     {
        //         return Unauthorized();
        //     }
        //     return Ok(token);
        // }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authennticate([FromBody] UserCred userCred)
        {
            var token = _customAuthenticationManaher.Authenticate(userCred.UserName, userCred.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

    }
}

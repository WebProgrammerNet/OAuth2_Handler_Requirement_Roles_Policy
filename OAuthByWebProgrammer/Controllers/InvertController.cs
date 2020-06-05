using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OAuthByWebProgrammer.Controllers
{
    [Authorize(Policy = "AdminAndPowerUser")]
    [Authorize(Policy = "EmployeeWithMore20Years")]
    [Route("api/[controller]")]
    [ApiController]
    public class InvertController : ControllerBase
    {
        // GET: api/Invert
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Invert/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

       //default Policy
        [HttpPost]
        public string Post([FromBody] Inventory value)
        {
            return "I am get Role";
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("poster")]
        public string Poster(Inventory value)
        {
            return "I am get Roleonly";
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("posterreq")]
        public string Posterreq(Inventory value)
        {
            return "I am get Requirement";
        }

    }
}

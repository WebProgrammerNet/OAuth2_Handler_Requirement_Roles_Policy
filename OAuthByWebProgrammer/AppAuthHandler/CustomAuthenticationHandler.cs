using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OAuthByWebProgrammer.AppInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace OAuthByWebProgrammer.AppAuthHandler
{
    public class BasicAuthenticationOptions : AuthenticationSchemeOptions
    {
    }
    public class CustomAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        private readonly ICustomAuthenticationManaher _authManager;
        public CustomAuthenticationHandler(
            IOptionsMonitor<BasicAuthenticationOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock, ICustomAuthenticationManaher customAuthenticationManaher) : base(options, logger, encoder, clock)
        {
            _authManager = customAuthenticationManaher;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }
            string authHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authHeader))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }
            if (!authHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            string token = authHeader.Substring("bearer".Length).Trim();
            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail("Unahtorized"); 
            }
           // return await Task.FromResult(AuthenticateResult.Fail("Unahtorized"));

            try
            {
               
                return ValidateToken(token);
            }
            catch (Exception ex)
            {
                // Ex Loglama
                return AuthenticateResult.Fail("Unahtorized");
            }    
        }

        private AuthenticateResult ValidateToken(string token)
        {
            var validatetoken = _authManager.Tokens.FirstOrDefault(r => r.Key == token);
            if (validatetoken.Value == null) 
            {
                return AuthenticateResult.Fail("Unahtorized");
            }
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,validatetoken.Value.Item1),
                    new Claim(ClaimTypes.Role, validatetoken.Value.Item2)
                };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principial = new GenericPrincipal(identity, new[] { validatetoken.Value.Item2});
            var ticket = new AuthenticationTicket(principial, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }

    }
}

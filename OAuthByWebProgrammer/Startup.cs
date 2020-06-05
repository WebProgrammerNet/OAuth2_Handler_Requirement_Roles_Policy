using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OAuthByWebProgrammer.AppAuthHandler;
using OAuthByWebProgrammer.AppInterfaces;
using OAuthByWebProgrammer.AppRepositories;
using OAuthByWebProgrammer.JwtRequirement;

namespace OAuthByWebProgrammer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            string key = "This my Secret Key";

            services.AddAuthentication("Basic")
                .AddScheme<BasicAuthenticationOptions, CustomAuthenticationHandler>("Basic", null);
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminAndPowerUser", policy =>
                {
                    policy.RequireRole("Adminstrator", "PoweUser");
                });
                options.AddPolicy("EmployeeWithMore20Years", policy => {
                    policy.Requirements.Add(new EmployeeWithMoreYearsRequirement(20));
                });
            });
            services.AddSingleton<IAuthorizationHandler, EmployeWithMoreYearsHandler>();
            services.AddSingleton<IEmployeNumbersOfYears, EmployeNumbersOfYears>();
            services.AddSingleton<IJWTAuthManager>(new JWTAuthManager(key));
            services.AddSingleton<ICustomAuthenticationManaher, CustomAuthenticationManaher>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

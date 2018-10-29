using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.QQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace QuickstartIdentityServer
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
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddTestUsers(Config.GetUsers());

            //services.AddAuthentication()
            //    .AddQQ(a =>
            //    {
            //        a.AppId = "";
            //        a.AppKey = "";
            //    });

            //使用OpenID Connect进行外部登录集成
            //services.AddAuthentication()
            //    .AddOpenIdConnect("oidc", "OpenID Connect", a =>
            //      {
            //          a.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
            //          a.SignOutScheme = IdentityServerConstants.SignoutScheme;

            //          a.Authority = "https://demo.identityserver.io/";
            //          a.ClientId = "implicit";

            //          a.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //          {
            //              NameClaimType = "name",
            //              RoleClaimType = "role"
            //          };

            //      });


            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}

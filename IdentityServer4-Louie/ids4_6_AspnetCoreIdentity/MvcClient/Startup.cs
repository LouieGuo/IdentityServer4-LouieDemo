using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MvcClient
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
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(a =>
            {
                a.DefaultScheme = "Cookies";
                a.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", a =>
             {
                 //用于在OpenID Connect协议完成后使用cookie处理程序发出cookie
                 a.SignInScheme = "Cookies";

                 a.Authority = "http://localhost:5000";
                 a.RequireHttpsMetadata = false;

                 a.ClientId = "mvc";
                 //用于在Cookie中保存IdentityServer中的令牌
                 a.SaveTokens = true;

                 a.ClientSecret = "secret";
                 a.ResponseType = "code id_token";//ResponseType设置为代码id_token（基本意思是“使用混合流”)
                 a.Scope.Add("api1");//授权范围为api1
                 a.Scope.Add("offline_access");
                 a.GetClaimsFromUserInfoEndpoint = true;

             });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}

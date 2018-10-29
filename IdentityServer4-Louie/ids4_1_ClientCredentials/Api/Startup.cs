using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api
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
            //需要去认证中心中认证token的有效性
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(a =>
                {
                    a.Authority = "http://localhost:5000";//认证中心
                    a.RequireHttpsMetadata = false;//指定发现端点是否需要HTTPS
                    a.ApiName = "api2";//api资源名称，与认证中心里面的APIResource需要配对
                });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //每次调用主机时自动执行身份验证
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Events.Web.Core;
using Events.Web.Adapters;
using Events.Data;
using Events.Data.Entities;

namespace Events.Web
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Config = config;
        }

        public IConfiguration Config { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EventsDbContext>(options => options.UseInMemoryDatabase("InMemory"));

            services.AddIdentity<Account, IdentityRole>()
                .AddEntityFrameworkStores<EventsDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureIdentity();

            services.AddScoped<ISecurityAdapter, SecurityAdapter>();

            services.AddSession();

            services.AddMvc();

            services.AddScoped<IDbInitializer, DbInitializer>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseFileServer();

            app.UseNodeModulesFolder(env.ContentRootPath);

            app.UseKnockoutBindings(env.ContentRootPath);

            app.UseAuthentication();

            app.UseSession();

            app.UseMvcWithDefaultRoute();

            dbInitializer.Initialize();
        }
    }
}

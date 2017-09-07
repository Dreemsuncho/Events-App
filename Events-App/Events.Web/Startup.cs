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
using Events.Data.Contracts;
using Events.Data.Factories;
using Events.Data.Repositories;

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
            services.AddTransient<IDataRepositoryFactory, DataRepositoryFactory>();
            services.AddTransient<EventsRepository>();

            services.AddDbContext<EventsDbContext>(options => options.UseSqlServer(Config.GetConnectionString("Events")));

            services.AddIdentity<Account, IdentityRole>()
                .AddEntityFrameworkStores<EventsDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureIdentity();

            services.AddScoped<ISecurityAdapter, SecurityAdapter>();

            services.AddSession();

            services.AddMvc();
        }

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            await app.InitialzieDatabase(app.ApplicationServices);
        }   
    }
}

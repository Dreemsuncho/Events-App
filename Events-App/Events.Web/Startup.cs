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
using System.Threading.Tasks;
using Events.Web.Controllers;

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
            services.AddTransient<IEventsRepository, EventsRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<EventsDbContext>(options => options.UseSqlServer(Config.GetConnectionString("Events")));
            //services.AddDbContext<EventsDbContext>(options => options.UseInMemoryDatabase("InMemory"));

            services.AddIdentity<Account, ApplicationRole>()
                .AddEntityFrameworkStores<EventsDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureIdentity();

            services.AddAuthorization(o =>
            {
                o.AddPolicy("Managers", policy => policy.RequireClaim("Admins", "Administrator"));
                o.AddPolicy("Users", policy => policy.RequireClaim("Users", "User"));
            });

            services.ConfigureApplicationCookie(config => config.LoginPath = "/account/login");

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

            app.UseDeveloperExceptionPage();

            app.UseFileServer();

            app.UseNodeModulesFolder(env.ContentRootPath);

            app.UseKnockoutBindings(env.ContentRootPath);

            app.UseSession();

            app.UseAuthentication();

            await app.InitialzieDatabase(app.ApplicationServices);

            app.UseMvcWithDefaultRoute();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

using Events.Data;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Events.Data.Entities;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseNodeModulesFolder(this IApplicationBuilder app, string rootPath)
        {
            var node_modules = "/node_modules";
            var fileProvider = new PhysicalFileProvider(rootPath + node_modules);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = fileProvider,
                RequestPath = node_modules
            });
        }

        public static void UseKnockoutBindings(this IApplicationBuilder app, string rootPath)
        {
            var node_modules = "/Bindings";
            var fileProvider = new PhysicalFileProvider(rootPath + node_modules);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = fileProvider,
                RequestPath = node_modules
            });
        }

        public static async Task InitialzieDatabase(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var scopeService = scope.ServiceProvider;

                var userManager = scopeService.GetRequiredService<UserManager<Account>>();
                var context = scopeService.GetRequiredService<EventsDbContext>();

               await DbInitializer.Initialize(userManager, context);
            }
        }
    }
}

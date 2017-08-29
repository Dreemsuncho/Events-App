using Microsoft.Extensions.FileProviders;

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
    }
}

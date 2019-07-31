using Lingva.WebAPI;
using Lingva.WebAPI.Extensions;
using Lingva.WebAPI.Infrastructure.Binders;
using Lingva.WebAPI.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Lingva.WebAPI
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureOptions(Configuration);                      
            services.ConfigureAuthentication(Configuration);
            services.ConfigureAutoMapper();
            services.ConfigureSwagger();

            services.ConfigureDbProvider(Configuration);
            services.ConfigureManagers();
            services.ConfigureDataAdapters();

            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new ModelBinderProvider());
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            var options = new RewriteOptions()
                .AddRedirect("(.*)/$", "$1")
                .AddRedirect("[h,H]ome[/]?$", "home/index"); 
            app.UseRewriter(options);

            app.UseCors("AllowAll");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lingva API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            DbInitializer.InitializeAsync(Configuration).Wait();
        }
    }
}

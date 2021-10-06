using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Rhetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp
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
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.ToString()); // Allows multiple entities with the same name in different modules.
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestApp", Version = "v1" });
                // Adding Rhetos REST API to Swagger with document name "rhetos".
                c.SwaggerDoc("rhetos", new OpenApiInfo { Title = "Rhetos REST API", Version = "v1" });
            });

            // Adding Rhetos.
            services.AddRhetosHost(ConfigureRhetosHostBuilder)
                .AddAspNetCoreIdentityUser()
                .AddRestApi(o =>
                {
                    o.BaseRoute = "rest";
                    o.GroupNameMapper = (conceptInfo, controller, oldName) => "rhetos"; // OpenAPI document name.
                });
        }

        private void ConfigureRhetosHostBuilder(IServiceProvider serviceProvider, IRhetosHostBuilder rhetosHostBuilder)
        {
            rhetosHostBuilder
                .ConfigureRhetosAppDefaults()
                .UseBuilderLogProviderFromHost(serviceProvider)
                .ConfigureConfiguration(cfg => cfg
                    .AddJsonFile("rhetos-app.local.settings.json")
                    .MapNetCoreConfiguration(Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestApp v1");
                    // Add Swagger endpoint for Rhetos REST API.
                    c.SwaggerEndpoint("/swagger/rhetos/swagger.json", "Rhetos REST API");
                });
            }

            app.UseRhetosRestApi();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

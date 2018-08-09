using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.PlatformAbstractions;
using MicroServiceExampleAPI.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using AutoMapper;

namespace MicroServiceExampleAPI
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
            services.AddDbContext<ExampleApiContext>(cfg => {
                // Can use in memory db.
                cfg.UseSqlServer(Configuration.GetConnectionString("ExampleApiConnectionString"));
            });

            services.AddAutoMapper();
            services.AddTransient<ExampleApiSeeder>();
            services.AddScoped<IExampleApiRepository, ExampleApiRepository>();

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            // API and Swagger configuration
            services.AddApiVersioning();
            services.AddMvcCore().AddVersionedApiExplorer(o => {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(options =>
            {
                var assemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, new Info { 
                        Title = $"{assemblyName} {description.ApiVersion}",
                        Description = description.IsDeprecated 
                                                 ? description.ApiVersion + ". This API version has been deprecated."
                                                 : description.ApiVersion.ToString()
                    });
                }

                // Integrate xml comments
                var xmlComments = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, assemblyName, ".xml");
                if (File.Exists(xmlComments))
                {
                    options.IncludeXmlComments(xmlComments);
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            // Generate swagger and auto create UI for each API version.
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });

            if (env.IsDevelopment()){
                using(var scope = app.ApplicationServices.CreateScope()){
                    var seeder = scope.ServiceProvider.GetService<ExampleApiSeeder>();
                    seeder.Seed();
                }
            }
        }
    }
}

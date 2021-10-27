using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SmartSchool.WebAPI.Data;

namespace SmartSchool.WebAPI
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
            // Adding database context from SmartContext class that contains all the tables of the application, using SQLite
            services.AddDbContext<SmartContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))
            );


            services.AddControllers()
                    .AddNewtonsoftJson(
                        opt => opt.SerializerSettings.ReferenceLoopHandling = 
                            Newtonsoft.Json.ReferenceLoopHandling.Ignore);
                            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddScoped<IRepository, Repository>();

            services.AddVersionedApiExplorer(options => 
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            })
            .AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            var apiProviderDescription = services.BuildServiceProvider()
                                                 .GetService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(options => 
            {
                foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo(){
                        Title = "SmartSchool API",
                        Version = description.ApiVersion.ToString(),
                        // TermsOfService = new Uri("http://SeusTermosDeUso.com"),
                        Description = "A descrição da WebApi do SmartSchool",
                        License = new OpenApiLicense
                        {
                            Name = "SmartSchool License",
                            // Url = new Uri("http://mit.com")
                        },
                        Contact = new OpenApiContact
                        {
                            Name = "Carlo Maschi Sulzbeck",
                            Email = "cmsulzbeck@hotmail.com",
                            Url = new Uri("https://github.com/cmsulzbeck")
                        }
                    });
                }
                
                options.SwaggerDoc("smartschoolapi", new OpenApiInfo(){
                    Title = "SmartSchool API",
                    Version = "1.0",
                    // TermsOfService = new Uri("http://SeusTermosDeUso.com"),
                    Description = "A descrição da WebApi do SmartSchool",
                    License = new OpenApiLicense
                    {
                        Name = "SmartSchool License",
                        // Url = new Uri("http://mit.com")
                    },
                    Contact = new OpenApiContact
                    {
                        Name = "Carlo Maschi Sulzbeck",
                        Email = "cmsulzbeck@hotmail.com",
                        Url = new Uri("https://github.com/cmsulzbeck")
                    }
                });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                options.IncludeXmlComments(xmlFullPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env,
                              IApiVersionDescriptionProvider apiProviderDescription)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger()
                    .UseSwaggerUI(options => 
                    {
                        foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                        {
                            options.SwaggerEndpoint(
                                $"/swagger/{description.GroupName}/swagger.json",
                                 description.GroupName.ToUpperInvariant());
                        }

                        options.RoutePrefix = "";
                    });
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartSchool.WebAPI v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

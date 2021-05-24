using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCore.Authentication.ApiKey;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ReitanDistribution.Infrastructure;

namespace ReitanDistribution.API
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
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ReitanDistribution.API", Version = "v1"});
                //TODO add authentication in a real world scenario
                ////Swagger security configuration: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/blob/v5.0.0-rc2/README-v5.md#add-security-definitions-and-requirements
                //c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                //{
                //    Type = SecuritySchemeType.ApiKey,
                //    In = ParameterLocation.Header,
                //    Name = "ApiKey",
                //    Description =
                //        "Secures access to the API controllers with a key, that can be used to control customer access or limit fraudulent access."
                //});
                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "ApiKey"}
                //        },
                //        new string[] { }
                //    }
                //});
            });

            //TODO add authentication in a real world scenario
            //services.AddAuthentication(ApiKeyDefaults.AuthenticationScheme)
            //    .AddApiKeyInHeader<ApiKeyProvider>(options =>
            //    {
            //        options.Realm = "Reitan Distribution API";
            //        options.KeyName = "ApiKey";
            //    });

            services.AddControllers();


            services.AddDbContext<ReitanDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReitanDistribution.API v1"));
                //Seeds the in memory database
                var context = provider.GetService<ReitanDbContext>();
                //OnModelCreating only ever gets called during a migration or when the following method gets called
                context?.Database.EnsureCreated();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //TODO add authentication in a real world scenario
            //app.UseAuthentication();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        }
    }
}

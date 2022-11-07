using System;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using QNomy.Api.Data;
using QNomy.Application;
using QNomy.Application.Clients.Command.AddClient;
using QNomy.Infrastructure;
using QNomy.Persistence;

namespace QNomy.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddFluentValidationAutoValidation();
	        services.AddValidatorsFromAssemblyContaining<AddClientCommandValidator>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Q-Nomy API", Version = "v1" });
            });
            services.AddAutoMapper(typeof(AutoMapperProfile));

            var assembly = typeof(AddClientCommandHandler).Assembly;
            services.AddMediatR(assembly);

            

            ConfigureDatabaseServices(services);

            // configure DI for application services
            services.AddInfraServices();

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOriginPolicy", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build());
            });
        }

        protected virtual void ConfigureDatabaseServices(IServiceCollection services)
        {
            services.AddPersistenceServices(Configuration.GetConnectionString("DefaultConnection"), "QNomy.Api");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Q-Nomy API V1");
            });

            using (var scope = app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetService<DataContext>())
                EnsureDatabaseCreated(context);
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowOriginPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        protected virtual void EnsureDatabaseCreated(DataContext context)
        {
            // run Migrations
            context.Database.Migrate();
        }
    }
}

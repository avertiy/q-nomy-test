using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using QNomy.Api.Data;
using QNomy.Application;
using QNomy.Application.Clients.Command.AddClient;
using QNomy.Infrastructure;
using QNomy.Persistence;

namespace QNomy.Api;

public static class ApplicationBuilderExtensions
{
	public static void AddAppServices(this WebApplicationBuilder builder)
	{
		builder.Services.AddControllers();
		builder.Services.AddFluentValidationAutoValidation();
		builder.Services.AddValidatorsFromAssemblyContaining<AddClientCommandValidator>();
		builder.Services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo { Title = "Q-Nomy API", Version = "v1" });
		});
		builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

		var assembly = typeof(AddClientCommandHandler).Assembly;
		builder.Services.AddMediatR(assembly);

		builder.AddPersistenceServices();

		// configure DI for application services
		builder.Services.AddInfraServices();

		

	}

	public static void AddCorsAllowAnyOrigin(this WebApplicationBuilder builder, string name = "AllowOriginPolicy")
	{
		builder.Services.AddCors(c =>
		{
			c.AddPolicy(name, x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build());
		});
	}

	private static void AddPersistenceServices(this WebApplicationBuilder builder)
	{
		var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
		builder.Services.AddPersistenceServices(connStr, "QNomy.Api");
	}

}


public static class WebApplicationExtensions
{
	public static void UseSwaggerWithUI(this WebApplication app)
	{
		app.UseSwagger();
		app.UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint("/swagger/v1/swagger.json", "Q-Nomy API V1");
		});
	}

	public static void UseMigrations(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		using var context = scope.ServiceProvider.GetService<DataContext>();
		EnsureDatabaseCreated(context);
	}

	static void EnsureDatabaseCreated(DataContext context)
	{
		// run Migrations
		context.Database.Migrate();
	}
}
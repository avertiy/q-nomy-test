using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QNomy.Api;
using QNomy.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddAppServices();
builder.AddCorsAllowAnyOrigin("AllowOriginPolicy");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}
else
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseSwaggerWithUI();
app.UseMigrations();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowOriginPolicy");

//app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//	endpoints.MapControllers();
//});

app.MapControllerRoute(
	name: "default",
	pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();

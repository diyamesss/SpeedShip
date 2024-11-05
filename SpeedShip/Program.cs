using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.KeyVault;
using Microsoft.EntityFrameworkCore;
using SpeedShip.DataAccess.Database;
using SpeedShip.DataAccess.DbInitializer;
using SpeedShip.DataAccess.Repository;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDbInitializer,DbInitializer>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();

//if (builder.Environment.IsDevelopment())
//{
//	builder.Services.AddDbContext<DbSpeedShipContext>(options => options.UseSqlServer(
//	builder.Configuration.GetConnectionString("DefaultConnection")));
//}
//if (builder.Environment.IsProduction())
//{
//	var client = new SecretClient(new Uri(builder.Configuration.GetSection("KeyVault:KeyVaultUrl").Value), new DefaultAzureCredential());
//	var secret = await client.GetSecretAsync(builder.Configuration.GetSection("KeyVault:SecretName").Value);

//	builder.Services.AddDbContext<DbSpeedShipContext>(options => options.UseSqlServer(
//	secret.Value.ToString()));
//}

builder.Services.AddDbContext<DbSpeedShipContext>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

MigrateDatabase();

app.MapControllerRoute(
	name: "default",
	pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();

void MigrateDatabase()
{
	using (var scope = app.Services.CreateScope())
	{
		var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
		dbInitializer.Initialize();
	}
}

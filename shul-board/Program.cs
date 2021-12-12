using Microsoft.EntityFrameworkCore;
using shul_board.Data;
using shul_board.Models;
using shul_board.Services;
using shul_board.Settings;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ShulBoardContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<SettingsService>();
builder.Services.AddScoped<LocationSettings>();
builder.Services.AddScoped<ZmanimService>();
builder.Services.AddScoped<ScheduleItemService>();
builder.Services.AddScoped<ScheduleGroupService>();
builder.Services.AddScoped<AnnouncementService>();

builder.Services.AddControllers().AddNewtonsoftJson(x =>
    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );



builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "withdate",
    pattern: "{controller}/{action=Index}/{dateTime?}");
app.MapRazorPages();

app.MapFallbackToFile("index.html"); ;

app.Run();

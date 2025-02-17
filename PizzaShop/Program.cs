using BLL.Services;
using DAL.Models;
using DAL.Repositories;
using MailKit;
using Microsoft.EntityFrameworkCore;
using PizzaShop.ViewModels;


var builder = WebApplication.CreateBuilder(args);

var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();
builder.Services.AddSingleton(emailSettings);
// Add services to the container.
var conn = builder.Configuration.GetConnectionString("PizzashopDBConnection");
builder.Services.AddDbContext<PizzashopContext>(q => q.UseNpgsql(conn));


builder.Services.AddScoped<LoginRepository>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<EmailService>();

builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

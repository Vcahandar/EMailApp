using EMailApp.Business.Implementations;
using EMailApp.Business.Interfaces;
using EMailApp.Core.Concrete;
using EMailApp.DataAccess.Context;
using EMailApp.DataAccess.Repositories.Implementations;
using EMailApp.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddDbContext<EMailDbContext>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IDraftRepository, DraftRepository>();
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<EMailDbContext>();



builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IDraftService, DraftService>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Login/Index";

});

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = "/Login/Index";
});




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

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();

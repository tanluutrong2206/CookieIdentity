using CookieIdentity.AppCode;
using CookieIdentity.Authorization;

using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddRazorPages();
services.AddAuthentication(Constant.COOKIE_NAME)
    .AddCookie(Constant.COOKIE_NAME, options =>
    {
        options.Cookie.Name = Constant.COOKIE_NAME;
        options.ExpireTimeSpan = TimeSpan.FromSeconds(20);
        options.SlidingExpiration = true;
        options.LoginPath = "/Account/Login";   // Default
        options.LogoutPath = "/Account/Logout";  // Default
    });
services.AddAuthorization(configure =>
{
    configure.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
    configure.AddPolicy("HRManager", policy => policy.Requirements.Add(new HRManagerProbationRequirement(3)));
});
services.AddSingleton<IAuthorizationHandler, HRManagerProbationRequirementHandler>();

services.AddHttpClient("WebAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7225/");
});
services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapRazorPages();

app.Run();

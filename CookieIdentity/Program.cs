using CookieIdentity.AppCode;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddRazorPages();
services.AddAuthentication(Constant.COOKIE_NAME)
    .AddCookie(Constant.COOKIE_NAME, options =>
    {
        options.Cookie.Name = Constant.COOKIE_NAME;
    });
services.AddAuthorization(configure =>
{
    configure.AddPolicy("AdminOnly", policy => policy.RequireClaim("Admin"));
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

app.MapRazorPages();

app.Run();

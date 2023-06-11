using BLL.Injections;
using DAL.Data;
using DAL.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Dependency injections
builder.Services.AddScoped<ICourseControllerHelper, CourseControllerHelper>();
builder.Services.AddScoped<IHashCodeGenerator, HashCodeGenerator>();
// Db Context
builder.Services.AddDbContext<DbContextProject>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDbConnection"))
);
// Додаю аутентифікацію, на основі cookie, задаю шлях для входу та виходу
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Account/LogIn");
        options.LoginPath = new PathString("/Account/LogOut");
    });

builder.Logging.AddConsole();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", context =>
{
    var isAuthenticated = context.User.Identity?.IsAuthenticated ?? false;
    if (isAuthenticated)
    {
        context.Response.Redirect("/Courses/Index");
    }
    else
    {
        context.Response.Redirect("/Home/Index");
    }
    return Task.CompletedTask;
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
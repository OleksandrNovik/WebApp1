using BLL.Injections;
using DAL.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ICourseControllerHelper, CourseControllerHelper>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

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
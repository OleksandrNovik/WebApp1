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

//TODO: Вибрати представлення для показу головної сторінки зареєстрованим користувачам
#region Authenticated-User-Return-View
//app.MapGet("/", context =>
//{
//    var isAuthenticated = context.User.Identity?.IsAuthenticated ?? false;
//    if (!isAuthenticated)
//    {
//        context.Response.Redirect("/Home/Index");
//	}
//    else
//    {
//		context.Response.Redirect("/***/Home");
//	}
//	return Task.CompletedTask;
//});
#endregion

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
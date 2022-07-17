using DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", opt =>
{
    opt.Cookie.Name = "MyCookieAuth";
    opt.LoginPath = "/Account/Login";
    opt.ExpireTimeSpan= TimeSpan.FromDays(30);
});

//builder.Services.AddDistributedMemoryCache();

builder.Services.AddDbContext<MyContext>(opt => opt.UseSqlServer(@"Data Source=localhost\SQLEXPRESS;Database=SummerProject;Trusted_Connection=True;"));


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

app.UseCookiePolicy();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();

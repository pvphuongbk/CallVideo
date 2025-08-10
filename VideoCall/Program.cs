using VideoCall.Api.Configurations;
using Microsoft.EntityFrameworkCore;
using Q101.ServiceCollectionExtensions.ServiceCollectionExtensions;
using System.Net.WebSockets;
using VideoCall.Common.Configuration;
using VideoCall.DataAccess.DBContext;
using VideoCall.DataAccess.Interface;
using VideoCall.DataAccess.Repositories;
using VideoCall.DataAccess.UnitOfWork;
using VideoCall.Service.Users;
using VideoCall.CommonCode;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();
AppConfigs.LoadAll(config);
builder.Services.AddDbContext<CommonDBContext>(options =>
            options.UseSqlServer(AppConfigs.SqlConnection, options => { }),
            ServiceLifetime.Scoped
            );
builder.Services.AddTransient(typeof(ICommonRepository<>), typeof(CommonRepository<>));
builder.Services.AddTransient(typeof(ICommonUoW), typeof(CommonUoW));
builder.Services.RegisterAssemblyTypesByName(typeof(IUserService).Assembly,
     name => name.EndsWith("Service")) // Condition for name of type
.AsScoped()
.AsImplementedInterfaces()
     .Bind();
builder.Services.AddCommonServices();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(365);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


// Add services to the container.
builder.Services.AddControllersWithViews();
// Configure the HTTP request pipeline.
builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();
builder.Services.AddSingleton<ISessionManager, SessionManager>();

var app = builder.Build();
// Sử dụng session
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "home",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<VideoCallHub>("/videoCallHub");
app.UseWebSockets();

app.UseHttpsRedirection();
app.UseAuthorization();

app.Run();

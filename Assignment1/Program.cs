using Microsoft.EntityFrameworkCore;
using Assignment1.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace Assignment1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";   // Trang đăng nhập
        options.LogoutPath = "/Account/Logout"; // Trang đăng xuất
        options.AccessDeniedPath = "/Account/AccessDenied"; // Trang từ chối truy cập
    });

            // Thêm dịch vụ Authorization (phân quyền)
            builder.Services.AddAuthorization();
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<FunewsManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FUNewsManagement")));
            builder.Services.AddScoped(typeof(FunewsManagementContext));
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}

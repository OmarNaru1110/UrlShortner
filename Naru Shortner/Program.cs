using Naru_Shortner.BLL;
using Naru_Shortner.Helpers;
using Naru_Shortner.Repository.IRepository;
using Naru_Shortner.Services.IServices;

namespace Naru_Shortner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //so i can inject it in constructor
            builder.Services.AddScoped<IUrlRepository, UrlRepository>();
            builder.Services.AddScoped<IUrlService, UrlService>();

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
                pattern: "{controller=Url}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
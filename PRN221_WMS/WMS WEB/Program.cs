using Microsoft.EntityFrameworkCore;
using WMS_BLL.Models;
using WMS_DAL.Implement;
using WMS_DAL.Interface;
using WMS_WEB.Hubs;

namespace WMS_WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            //SessionAccessor
            builder.Services.AddHttpContextAccessor();

            //DI
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ILotRepository, LotRepository>();
            builder.Services.AddScoped<IPartnerRepository, PartnerRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IStockOutRepository, StockOutRepository>();
            builder.Services.AddScoped<IStorageRepository, StorageAreaRepository>();

            //DBContext
            builder.Services.AddDbContext<PRN221_WMSContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //Service
            builder.Services.AddSession();
            builder.Services.AddSignalR();

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

            app.UseAuthorization();

            app.MapRazorPages();
            app.MapHub<ChatHub>("/chatHub");
            app.UseSession();

            app.Run();
        }
    }
}
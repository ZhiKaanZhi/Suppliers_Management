using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Repositories;
using WebApplication1.Repositories.RepositoryInterfaces;
using WebApplication1.Services;
using WebApplication1.Services.ServiceInterfaces;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllersWithViews();

            //add services into IoC container
            builder.Services.AddScoped<ISupplierService, SupplierService>();
            builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
            builder.Services.AddScoped<ISupplierCategoryService, SupplierCategoryService>();
            builder.Services.AddScoped<ISupplierCategoryRepository, SupplierCategoryRepository>();
            builder.Services.AddScoped<ICountryService, CountryService>();
            builder.Services.AddScoped<ICountryRepository, CountryRepository>();


            var constr = builder.Configuration.GetConnectionString("conn");
            builder.Services.AddDbContext<DatabaseContext>(opts => opts.UseSqlServer(constr));
            

            var app = builder.Build();



            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}
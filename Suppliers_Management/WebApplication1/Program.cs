using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();
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
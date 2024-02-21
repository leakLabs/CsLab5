using Lab4.MedicineDB;
using Microsoft.EntityFrameworkCore;

using (ApplicationDbContext db = new ApplicationDbContext())
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllersWithViews();

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer((@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=lab4;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")));

    var app = builder.Build();

    app.MapControllerRoute(
        name: "default",
    //pattern: "{controller=Medicine}/{action=Index}/{id?}");
    //pattern: "{controller=Manufacturer}/{action=Index}/{id?}");
    //pattern: "{controller=Ingredient}/{action=Index}/{id?}");
    pattern: "{controller=Home}/{action=Index}/{id?}");


    app.Run();
}
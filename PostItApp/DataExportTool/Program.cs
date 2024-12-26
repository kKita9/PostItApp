using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer("Server=sqlserver,1433;Database=PostItAppDb;User=sa;Password=Password_123#;TrustServerCertificate=True;"))
            .BuildServiceProvider();

        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            DataAccess.DataExporter.ExportData(context);
            Console.WriteLine("Export is finished!");
        }
    }
}

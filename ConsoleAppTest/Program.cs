using ConsoleAppTest.DbContexts;
using ConsoleAppTest.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ConsoleAppTest
{
    internal class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(app =>
                {
                    app.AddJsonFile("appsettings.json");
                })
                .ConfigureServices((hostContext, services) =>
                {
                    string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                    string? connectionString = hostContext.Configuration.GetConnectionString("Default");

                    //entity framework
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseSqlServer(connectionString, option => option.MigrationsAssembly(assemblyName));
                    });
                });

        public class Compare : IEqualityComparer<int>
        {
            public bool Equals(int x, int y)
            {
                return x == y;
            }

            public int GetHashCode([DisallowNull] int obj)
            {
                return obj.GetHashCode();
            }
        }

        static void Main(string[] args)
        {
            var test = new Classroom();
            Console.WriteLine(test.Name?.ToString());

            IHost host = CreateHostBuilder(args).Build();
            var dbContext = host.Services.GetService<ApplicationDbContext>()!;

            var studentClassrooms = dbContext.StudentsClassrooms.ToList();
            var students = dbContext.Students.ToList();

            List<int> list = new() { 1, 2, 3 };
            List<int> list2 = new() { 2, 3 };

            list.Except(list2, new Compare());
        }

    }
}
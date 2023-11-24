using ConsoleAppTest.DbContexts;
using ConsoleAppTest.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.RegularExpressions;

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
            var regex = new Regex(@"{{Text_(\d+)_(\d+)}}");
            string inputString1 = "abc def {{Text_123_456}}";
            var result1 = regex.Matches(inputString1);
            var result = result1.Count;

            string inputString2 = "abc def ";
            var result2 = regex.Matches(inputString2);
        }

    }
}
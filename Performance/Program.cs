using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Performance.DbContexts;
using Performance.Entities;
using System.Collections.Concurrent;
using System.Reflection;

namespace Performance
{
    [MemoryDiagnoser]
    internal class Program
    {
        static async Task SeedData(ApplicationDbContext dbContext)
        {
            dbContext.Database.ExecuteSqlRaw($"Delete from {nameof(Book)}");
            dbContext.Database.ExecuteSqlRaw($"Delete from {nameof(Author)}");
            int authorCount = 1000000;
            for (int i = 0; i < authorCount; i++)
            {
                var author = dbContext.Authors.Add(new Author
                {
                    Name = Faker.Name.FullName(),
                }).Entity;
                await dbContext.SaveChangesAsync();

                for (int j = 0; j < Faker.RandomNumber.Next(1, 20); j++)
                {
                    dbContext.Books.Add(new Book
                    {
                        Title = Faker.Lorem.Sentence(),
                        Status = Faker.RandomNumber.Next(1, 2),
                        Description = Faker.Lorem.Sentence(),
                        AuthorId = author.Id,
                    });
                }
                await dbContext.SaveChangesAsync();
            }
        }


        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var dbContext = host.Services.GetRequiredService<ApplicationDbContext>();
            await SeedData(dbContext);
            //host.Run();

            var summary = BenchmarkRunner.Run<Program>();
        }

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
    }
}

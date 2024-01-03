using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Performance.DbContexts;
using Performance.Entities;
using System.Reflection;

namespace Performance
{
    [MemoryDiagnoser]
    public class Program
    {
        protected Program()
        {
        }

        static async Task SeedData(ApplicationDbContext dbContext)
        {
            //dbContext.Database.ExecuteSqlRaw($"Delete from {nameof(Classroom)}");
            //dbContext.Database.ExecuteSqlRaw($"Delete from {nameof(Student)}");
            int numberStudent = 1000000;
            //for (int i = 0; i < numberStudent; i++)
            //{
            //    dbContext.Students.Add(new Student
            //    {
            //        Name = Faker.Name.FullName(),
            //    });
            //}
            //await dbContext.SaveChangesAsync();

            int numberClassroom = 1000000;
            //for (int i = 0; i < numberClassroom; i++)
            //{
            //    dbContext.Classrooms.Add(new Classroom
            //    {
            //        Name = Faker.Lorem.Sentence(),
            //        Status = Faker.RandomNumber.Next(1, 2),
            //        MaxStudent = Faker.RandomNumber.Next(20, 30),
            //    });
            //}
            //await dbContext.SaveChangesAsync();

            //int numberRegister = 200000;
            //for (int i = 0; i < numberRegister; i++)
            //{
            //    int studentId = 0;
            //    int classroomId = 0;
            //    while (true)
            //    {
            //        studentId = Faker.RandomNumber.Next(1, numberStudent);
            //        classroomId = Faker.RandomNumber.Next(1, numberClassroom);
            //        bool checkExist = await dbContext.StudentClassrooms.AnyAsync(s => s.StudentId == studentId && s.ClassroomId == classroomId);
            //        int countStudent = await dbContext.StudentClassrooms.Where(s => s.ClassroomId == classroomId).CountAsync();
            //        int maxStudentInClass = dbContext.Classrooms.Where(s => s.Id == classroomId).FirstOrDefault()!.MaxStudent;
            //        if (!checkExist && countStudent <= maxStudentInClass)
            //        {
            //            break;
            //        }
            //    }
            //    dbContext.StudentClassrooms.Add(new StudentClassroom
            //    {
            //        StudentId = studentId,
            //        ClassroomId = classroomId,
            //    });
            //    dbContext.SaveChanges();
            //}
        }

        static async Task UpdateData(ApplicationDbContext dbContext)
        {
            Random random = new Random();
            foreach(var classroom in dbContext.Classrooms.ToList())
            {
                classroom.MaxStudent = random.Next(20, 60);
            }
            await dbContext.SaveChangesAsync();
        }

        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var dbContext = host.Services.GetRequiredService<ApplicationDbContext>();
            //await SeedData(dbContext);
            await UpdateData(dbContext);
            //host.Run();

            BenchmarkRunner.Run<Program>();
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

                    services.AddLogging(builder => builder.AddConsole());
                });
    }
}

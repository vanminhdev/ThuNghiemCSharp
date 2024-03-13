using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Performance.Benchmarks;
using Performance.Common;
using Performance.DbContexts;
using Performance.Dtos.Student;
using Performance.Entities;

namespace Performance
{
    public static class Program
    {
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

        static string GenerateRandomPhoneNumber()
        {
            Random random = new Random();
            // Lấy ngẫu nhiên 10 chữ số để tạo số điện thoại
            StringBuilder phoneNumberBuilder = new StringBuilder("0");
            for (int i = 0; i < 9; i++)
            {
                phoneNumberBuilder.Append(random.Next(10));
            }
            return phoneNumberBuilder.ToString();
        }

        static string GenerateRandomEmail()
        {
            Random random = new Random();
            StringBuilder emailBuilder = new StringBuilder();
            emailBuilder.Append("user");
            emailBuilder.Append(random.Next(1, 99999999));
            emailBuilder.Append("@edu.vn");
            return emailBuilder.ToString();
        }

        // Danh sách 10 mã ngành
        static readonly List<string?> IndustryCodes =
            new()
            {
                "111",
                null,
                "222",
                "333",
                null,
                "444",
                "555",
                null,
                "666",
                "777",
                null,
                "888",
                "999",
                null
            };

        // Danh sách mã chuyên ngành dài 7 ký tự
        static readonly List<string> MajorCodes = new List<string>
        {
            "1234567",
            null,
            "2345678",
            "3456789",
            null,
            "4567890",
            "5678901",
            null,
            "6789012",
            "7890123",
            null,
            "8901234",
            "9012345",
            null,
        };

        static string GetRandomIndustryCode()
        {
            // Sử dụng thời gian hiện tại làm giá trị seed
            Random random = new Random(DateTime.Now.Millisecond);
            // Lấy ngẫu nhiên một mã ngành từ danh sách
            int randomIndex = random.Next(IndustryCodes.Count);
            return IndustryCodes[randomIndex];
        }

        static string GetRandomMajorCode()
        {
            // Sử dụng thời gian hiện tại làm giá trị seed
            Random random = new Random(DateTime.Now.Millisecond);

            // Lấy ngẫu nhiên một mã chuyên ngành từ danh sách
            int randomIndex = random.Next(MajorCodes.Count);
            return MajorCodes[randomIndex];
        }

        static DateTime GenerateRandomBirthDate()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            DateTime maxDate = DateTime.Now.AddYears(-18);
            DateTime minDate = DateTime.Now.AddYears(-30);
            int range = (int)(maxDate - minDate).TotalDays;
            int randomDays = random.Next(range);
            DateTime randomBirthDate = minDate.AddDays(randomDays);
            return randomBirthDate;
        }

        static async Task UpdateData(ApplicationDbContext dbContext)
        {
            //Random random = new Random();
            //foreach(var classroom in dbContext.Classrooms.ToList())
            //{
            //    classroom.MaxStudent = random.Next(20, 60);
            //}
            Random random1 = new Random(Guid.NewGuid().GetHashCode());
            Random random2 = new Random(Guid.NewGuid().GetHashCode());
            foreach (var student in dbContext.Students.ToList())
            {
                student.StudentCode =
                    random1.Next(300, 999999).ToString() + random2.Next(50, 68).ToString();
                student.Phone = GenerateRandomPhoneNumber();
                student.Email = GenerateRandomEmail();
                student.IndustryCode = GetRandomIndustryCode();
                student.MajorCode = GetRandomMajorCode();
                student.CreatedDate = DateTime.Now.AddDays(-100).AddSeconds(student.Id);
                student.DateOfBirth = GenerateRandomBirthDate();
                Console.WriteLine(student);
            }

            await dbContext.SaveChangesAsync();
        }

        class Filter1
        {
            public int Status { get; set; }
        }

        static void QueryParam(ApplicationDbContext dbContext)
        {
            dbContext.Classrooms.Where(c => c.Status == ClassroomStatus.Active).ToList();

            int status = ClassroomStatus.Active;
            dbContext.Classrooms.Where(c => c.Status == status).ToList();

            var filter = new Filter1() { Status = ClassroomStatus.Active };
            dbContext.Classrooms.Where(c => c.Status == filter.Status).ToList();

            //var status2 =
            dbContext
                .Classrooms.Where(c =>
                    new int[] { ClassroomStatus.Active, ClassroomStatus.Deactive }.Contains(
                        c.Status
                    )
                )
                .ToList();
        }

        static void QueryParam2(ApplicationDbContext dbContext)
        {
            var timer = new Stopwatch();
            timer.Start();
            for (int i = 0; i < 300; i++)
            {
                var test1 = dbContext.Classrooms.FirstOrDefault(c =>
                    c.MaxStudent == 20 && c.Status == 1
                );
            }
            //B: Run stuff you want timed
            timer.Stop();

            var timer2 = new Stopwatch();
            timer2.Start();
            for (int i = 0; i < 300; i++)
            {
                int maxStudent = 20;
                int status = 1;
                var test2 = dbContext.Classrooms.FirstOrDefault(c =>
                    c.MaxStudent == maxStudent && c.Status == status
                );
            }
            timer2.Stop();

            var timer3 = new Stopwatch();
            timer3.Start();
            var test3 = dbContext
                .Classrooms.Where(c => c.MaxStudent == 20 && c.Status == 1)
                .ToList();
            timer2.Stop();

            Console.WriteLine($"{nameof(QueryParam2)}: {timer.Elapsed}");
            Console.WriteLine($"{nameof(QueryParam2)}: {timer2.Elapsed}");
            Console.WriteLine($"{nameof(QueryParam2)}: {timer3.Elapsed}");
        }

        static IQueryable<StudentClassroom> QueryReuse(this ApplicationDbContext dbContext)
        {
            //Chỗ này có thể xử lý join bla bla
            return dbContext.StudentClassrooms;
        }

        static void QueryParam3(ApplicationDbContext dbContext)
        {
            var test =
                from student in dbContext.Students
                join sc in dbContext.QueryReuse() on student.Id equals sc.StudentId
                select sc;

            var test2 = test.ToList();
        }

        static void QueryParam4(ApplicationDbContext dbContext)
        {
            var timer = new Stopwatch();
            timer.Start();
            for (int i = 0; i < 100; i++)
            {
                dbContext.Classrooms.FirstOrDefault(c => c.Status == 1);
            }
            timer.Stop();
            Console.WriteLine($"{nameof(QueryParam4)}: {timer.Elapsed}");
        }

        static void QueryParam5(ApplicationDbContext dbContext)
        {
            var timer = new Stopwatch();
            timer.Start();
            for (int i = 0; i < 100; i++)
            {
                dbContext.Classrooms.AsNoTracking().FirstOrDefault(c => c.Status == 1);
            }
            timer.Stop();

            //var timer2 = new Stopwatch();
            //timer2.Start();
            //for (int i = 0; i < 100; i++)
            //{
            //    dbContext.Classrooms.AsNoTrackingWithIdentityResolution().FirstOrDefault(c => c.Status == 1);
            //}
            //timer2.Stop();

            Console.WriteLine($"{nameof(QueryParam5)}: {timer.Elapsed}");
            //Console.WriteLine($"{nameof(QueryParam5)}: {timer2.Elapsed}");
        }

        static async Task QueryParam6(ApplicationDbContext dbContext)
        {
            var timer = new Stopwatch();
            timer.Start();
            for (int i = 0; i < 100; i++)
            {
                await dbContext.Classrooms.FirstOrDefaultAsync(c => c.Status == 1);
            }
            timer.Stop();

            Console.WriteLine($"{nameof(QueryParam6)}: {timer.Elapsed}");
        }

        static void QueryParam7(ApplicationDbContext dbContext)
        {
            var timer = new Stopwatch();
            timer.Start();
            for (int i = 0; i < 100; i++)
            {
                dbContext.Classrooms.FirstOrDefault(c => c.Status == 1);
            }
            timer.Stop();

            Console.WriteLine($"{nameof(QueryParam7)}: {timer.Elapsed}");
        }

        static void QuerySame(ApplicationDbContext dbContext, IQueryable<IEntity> query)
        {
            var test =
                from entity in query
                join other in dbContext.StudentClassrooms on entity.Id equals other.StudentId
                select entity;
            var test2 = test.FirstOrDefault();
        }

        static void QueryIncludeWithWhere(ApplicationDbContext dbContext)
        {
            var test = dbContext
                .Students.Include(s => s.Classrooms.Where(c => !c.Deleted))
                .Where(s => s.Classrooms.Any())
                .Select(s => s)
                .FirstOrDefault();

            var test2 = dbContext
                .Students.Include(s => s.Classrooms.Where(c => !c.Deleted).Take(1))
                .Where(s => s.Classrooms.Any())
                .Select(s => s)
                .AsSplitQuery()
                .AsNoTracking() //filted trong include sẽ có tác dụng
                .FirstOrDefault();

            //dbContext.Attach(test2!);
            var test3 = dbContext.Students.Find(test.Id);

            test3.Email = GenerateRandomEmail();

            dbContext.SaveChanges();

            var test4 = dbContext
                .StudentClassrooms.Include(sc => sc.Student)
                .Where(sc => !sc.Student.Deleted && sc.Id == 1)
                .Select(sc => sc)
                .ToList();
        }

        static void QueryFulltext(ApplicationDbContext dbContext)
        {
            var test5 = dbContext
                .Classrooms.Where(c => EF.Functions.Contains(c.Name, "a"))
                .ToList();
            var test6 = dbContext.Classrooms.Where(c => c.Name.Contains("a")).ToList();
        }

        /// <summary>
        /// Thực hiện raw sql <br/>
        /// Chú ý tham số hoá query
        /// The FromSql and FromSqlInterpolated methods are safe against SQL injection,
        /// and always integrate parameter data as a separate SQL parameter.
        /// However, the FromSqlRaw method can be vulnerable to SQL injection attacks,
        /// if improperly used.
        /// </summary>
        static void QueryScalar(ApplicationDbContext dbContext)
        {
            var test = dbContext
                .Database.SqlQueryRaw<StudentDto>("Select [Id], [Name] from Student")
                .ToList();
            var test2 = dbContext
                .Database.SqlQueryRaw<StudentDto>("Select * from Student")
                .ToList();

            var test3 = dbContext
                .Database.SqlQuery<StudentDto>($"Select * from Student")
                .Where(s => s.Id < 10)
                .ToList();

            string param1 = "10";
            var test4 = dbContext
                .Database.SqlQuery<StudentDto>($"Select * from Student where Id < {param1}")
                .ToList();

            string param2 = "10; insert into Student(Name) values('Abc')";
            //var test5 = dbContext
            //    .Database.SqlQueryRaw<StudentDto>($"Select * from Student where Id < {param1}")
            //    .ToList();

            // against SQL injection
            var columnValue = new SqlParameter("param1", param2);

            var test6 = dbContext
                .Database.SqlQueryRaw<StudentDto>(
                    $"Select * from Student where Id < @param1",
                    columnValue
                )
                .ToList();


        }

        static async Task Main(string[] args)
        {
            //BenchmarkRunner.Run<Program>();
            var host = CreateHostBuilder(args).Build();
            var dbContext = host.Services.GetRequiredService<ApplicationDbContext>();
            //await SeedData(dbContext);
            //await UpdateData(dbContext);
            //host.Run();

            //BenchmarkRunner.Run<TestQuery>();

            //QueryParam2(dbContext);

            //QueryParam3(dbContext);
            //QueryParam4(dbContext);
            //QueryParam5(dbContext);
            //await QueryParam6(dbContext);
            //QueryParam7(dbContext);

            //QuerySame(dbContext, dbContext.Students);
            //QuerySame(dbContext, dbContext.Classrooms);

            //QueryIncludeWithWhere(dbContext);
            //QueryFulltext(dbContext);

            QueryScalar(dbContext);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(app =>
                {
                    app.AddJsonFile("appsettings.json");
                })
                .ConfigureServices(
                    (hostContext, services) =>
                    {
                        string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                        string? connectionString = hostContext.Configuration.GetConnectionString(
                            "Default"
                        );

                        //entity framework
                        services.AddDbContext<ApplicationDbContext>(options =>
                        {
                            options.UseSqlServer(
                                connectionString,
                                option => option.MigrationsAssembly(assemblyName)
                            );
                        });

                        services.AddLogging(builder => builder.AddConsole());
                    }
                );
    }
}

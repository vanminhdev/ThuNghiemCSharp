using System.Diagnostics;

namespace TaskAndThread
{
    static class Program
    {
        static async Task Main()
        {
            //await Time(async () =>
            //{
            //    Console.WriteLine("Enter");
            //    await Task.Delay(TimeSpan.FromSeconds(16));
            //    Console.WriteLine("Exit");
            //});

            var test = GetDate();
        }

        public static DateTime GetDate()
        {
            return DateTime.UtcNow.AddHours(7);
        }

        public static async Task MyMethod()
        {
            List<Task> tasks = new List<Task>();

            tasks.Add(Task.Run(async () =>
            {
                // Đợi 1 giây
                await Task.Delay(5000);
                Console.WriteLine("After delay");
            }));

            // Các lệnh sau không phụ thuộc vào Task.Delay ở trên
            tasks.Add(SomeOtherMethod()); // Công việc khác không phụ thuộc vào Task.Delay ở trên
            Console.WriteLine("After SomeOtherMethod");

            // Các lệnh sau đây sẽ được thực hiện ngay lập tức, không chờ đợi các công việc trước đó hoàn tất
            Console.WriteLine("End of MyMethod");

            //https://learn.microsoft.com/en-us/dotnet/standard/parallel-programming/exception-handling-task-parallel-library
            try
            {
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task SomeOtherMethod()
        {
            await Task.Delay(2000); // Đợi 1 giây
            Console.WriteLine("SomeOtherMethod done");
            throw new Exception("ex 1");
        }

        static async Task Time(Func<Task> action)
        {
            Console.WriteLine("Timing...");
            Stopwatch sw = Stopwatch.StartNew();
            await action();
            Console.WriteLine($"...done timing: {sw.Elapsed}");
        }

        public static IEnumerable<int> GenerateNumbers()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return i;
            }
        }
    }
}
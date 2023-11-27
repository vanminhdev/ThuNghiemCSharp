using System.Diagnostics;

namespace TaskAndThread
{
    static class Program
    {
        static async Task Main()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationTokenSource source2 = new CancellationTokenSource();
            Console.WriteLine($"Main - {Environment.CurrentManagedThreadId}: start job");
            
            var task1 = SlowTaskWithCancelToken("Task1", 1000, source.Token);
            var task2 = SlowTaskWithCancelToken("Task2", 5000, source2.Token);
            var task3 = SlowTask("Task3", 2000);
            var task4 = SlowTask("Task4", 5000);

            await Task.WhenAll(task1, task2, task3, task4);
            Console.WriteLine($"Main - {Environment.CurrentManagedThreadId}: done job");
        }

        #region demo handle exception in multiple task
        public static async Task SlowTaskWithCancelToken(string taskName, int delay, CancellationToken cancellationToken)
        {
            Console.WriteLine($"{taskName} - {Environment.CurrentManagedThreadId}: start");
            await Task.Delay(delay, cancellationToken);
            if (taskName == "Task1")
            {
                await Task.Delay(delay, cancellationToken);
            }
            Console.WriteLine($"{taskName} - {Environment.CurrentManagedThreadId}: done");
        }

        public static async Task SlowTask(string taskName, int delay)
        {
            Console.WriteLine($"{taskName} - {Environment.CurrentManagedThreadId}: start");
            await Task.Delay(delay);
            Console.WriteLine($"{taskName} - {Environment.CurrentManagedThreadId}: done");
        }
        #endregion

        #region demo multiple task
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
        #endregion


        public static IEnumerable<int> GenerateNumbers()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return i;
            }
        }
    }
}
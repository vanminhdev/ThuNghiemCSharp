using System.Diagnostics;

namespace PerformanceCompare;

public class Program
{
    static void Main()
    {
        const int iterations = 100000;

        var stopwatch = new Stopwatch();

        // Test Result Pattern
        stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            var result = new Test2().Layer3();
            if (!result.IsSuccess)
            {
                var error = result.ErrorCode; // Handle error
                Console.WriteLine($"Process failed: {result.StackTrace}");
            }
        }
        stopwatch.Stop();
        Console.WriteLine($"Result Pattern: {stopwatch.ElapsedMilliseconds} ms");

        //var result = new Test2().Layer3();

        //if (result.IsSuccess)
        //{
        //    Console.WriteLine($"Process completed successfully. Data3: {result.Value.Data3}");
        //}
        //else
        //{
        //    Console.WriteLine($"Process failed: {result.ErrorCode}");
        //    Console.WriteLine($"Stack Trace: {result.StackTrace}");
        //}
    }
}


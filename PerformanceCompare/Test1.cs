using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceCompare
{
    public class Test1
    {
        // Result pattern implementation
        static ResultPattern PerformOperationUsingResultPattern(int value)
        {
            if (value % 2 == 0) // Simulate an error condition
            {
                return ResultPattern.Fail("An error occurred.");
            }
            return ResultPattern.Ok();
        }

        // Exception pattern implementation
        static void PerformOperationUsingExceptionPattern(int value)
        {
            if (value % 2 == 0) // Simulate an error condition
            {
                throw new InvalidOperationException("An error occurred.");
            }
        }

        public static void Test()
        {
            const int iterations = 1000000;

            var stopwatch = new Stopwatch();

            // Test Result Pattern
            stopwatch.Start();
            for (int i = 0; i < iterations; i++)
            {
                var result = PerformOperationUsingResultPattern(i);
                if (!result.IsSuccess)
                {
                    var error = result.ErrorMessage; // Handle error
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"Result Pattern: {stopwatch.ElapsedMilliseconds} ms");

            // Test Exception Pattern
            stopwatch.Restart();
            for (int i = 0; i < iterations; i++)
            {
                try
                {
                    PerformOperationUsingExceptionPattern(i);
                }
                catch (Exception ex)
                {
                    var error = ex.Message; // Handle error
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"Exception Pattern: {stopwatch.ElapsedMilliseconds} ms");
        }
    }

    // Result class to represent success or failure
    public class ResultPattern
    {
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }

        protected ResultPattern(bool isSuccess, string errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static ResultPattern Ok()
        {
            return new ResultPattern(true, string.Empty);
        }

        public static ResultPattern Fail(string errorMessage)
        {
            return new ResultPattern(false, errorMessage);
        }
    }

}

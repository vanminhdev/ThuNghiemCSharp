using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace PerformanceCompare
{
    public static class StackTraceExtension
    {
        public static string GetCurrentMethodInfo(
            this object obj,
            [CallerLineNumber] int lineNumber = 0,
            [CallerFilePath] string filePath = null!,
            [CallerMemberName] string memberName = null!
        )
        {
            return $"\nTrace: {filePath} {memberName} {lineNumber}\n";
        }
    }

    public class Test2
    {
        public Result<Layer1Dto> Layer1()
        {
            // Thực hiện bước 1
            // ...
            var radom = new Random();
            if (true)
            {
                return Result<Layer1Dto>.Failure(400, this.GetCurrentMethodInfo());
            }

            return Result<Layer1Dto>.Success(new Layer1Dto { Data1 = "Value from Layer1" });
        }

        public Result<Layer2Dto> Layer2()
        {
            var result1 = Layer1();
            if (!result1.IsSuccess)
            {
                return Result<Layer2Dto>.Failure(result1);
            }

            if (string.IsNullOrEmpty(result1.Value.Data1))
            {
                return Result<Layer2Dto>.Failure(result1);
            }

            return Result<Layer2Dto>.Success(new Layer2Dto { Data2 = 42 });
        }

        public Result<Layer3Dto> Layer3()
        {
            var result2 = Layer2();
            if (!result2.IsSuccess)
            {
                return Result<Layer3Dto>.Failure(result2);
            }

            if (result2.Value.Data2 <= 0)
            {
                return Result<Layer3Dto>.Failure(result2);
            }

            return Result<Layer3Dto>.Success(new Layer3Dto { Data3 = true });
        }
    }

    public class Layer1Dto
    {
        public string Data1 { get; set; }
    }

    public class Layer2Dto
    {
        public int Data2 { get; set; }
    }

    public class Layer3Dto
    {
        public bool Data3 { get; set; }
    }
}

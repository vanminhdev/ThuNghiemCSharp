using System.Diagnostics;

namespace PerformanceCompare
{
    public class Result
    {
        public bool IsSuccess { get; protected set; }
        public int ErrorCode { get; protected set; }
        public string? StackTrace { get; protected set; }
    }

    public class Result<T> : Result
    {
        public T Value { get; private set; }

        /// <summary>
        /// Constructor cho kết quả thành công
        /// </summary>
        /// <param name="value"></param>
        private Result(T value)
        {
            IsSuccess = true;
            Value = value;
            ErrorCode = 0;
        }

        /// <summary>
        /// Constructor cho kết quả thất bại
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="stackTrace"></param>
        private Result(int errorCode, string stackTrace)
        {
            IsSuccess = false;
            Value = default(T);
            ErrorCode = errorCode;
            StackTrace = stackTrace;
        }

        /// <summary>
        /// Constructor cho kết quả thất bại
        /// </summary>
        private Result(Result result)
        {
            IsSuccess = false;
            Value = default(T);
            ErrorCode = result.ErrorCode;
            StackTrace = result.StackTrace;
        }

        /// <summary>
        /// Factory method cho thành công
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }

        /// <summary>
        /// Method để báo lỗi đầu tiên
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="stackTrace"></param>
        /// <returns></returns>
        public static Result<T> Failure(int errorCode, string stackTrace)
        {
            return new Result<T>(errorCode, stackTrace);
        }

        /// <summary>
        /// Method để nối thêm stack trace
        /// </summary>
        public static Result<T> Failure(Result result)
        {
            return new Result<T>(result);
        }

        private static string GetCallerInfo()
        {
            var stackTrace = new StackTrace(true); // true để bao gồm thông tin về file và số dòng
            var frame = stackTrace.GetFrame(2); // Lấy khung ngăn xếp của hàm gọi đến hàm hiện tại
            if (frame != null)
            {
                return $"{frame.GetMethod().DeclaringType.FullName}.{frame.GetMethod().Name} (Line {frame.GetFileLineNumber()})";
            }
            return "Unknown";
        }
    }
}

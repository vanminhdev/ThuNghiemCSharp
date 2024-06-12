namespace WebAPICacheDemo.Hosted
{
    public class AutoFetchTokenHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<AutoFetchTokenHostedService> _logger;
        private Timer? _timer = null;
        private int intervalSeconds = 5;
        private const int MIN = 20;
        private const int MAX = 50;
        private readonly object _lock = new object();

        public AutoFetchTokenHostedService(ILogger<AutoFetchTokenHostedService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(AutoFetchTokenHostedService)} running.");

            _timer = new Timer(DoWork, int.MaxValue, TimeSpan.Zero, TimeSpan.FromSeconds(intervalSeconds));

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            var count = Interlocked.Increment(ref executionCount);
            _logger.LogInformation($"{nameof(AutoFetchTokenHostedService)} Count: {count}");
            //số lượng 
            int numRemainUser = state as int? ?? int.MaxValue;
            if (numRemainUser > 100)
            {
                //lấy dữ liệu cập nhật cache
            }

            lock (_lock)
            {
                // Kiểm tra lại biến đếm sau khi có khóa
                if (executionCount == 1)
                {
                    // Thay đổi khoảng thời gian giữa các lần thực thi thành 10 giây
                    intervalSeconds = 10;
                    // Thiết lập lại timer với khoảng thời gian mới
                    _timer?.Change(TimeSpan.Zero, TimeSpan.FromSeconds(intervalSeconds));
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(AutoFetchTokenHostedService)} is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

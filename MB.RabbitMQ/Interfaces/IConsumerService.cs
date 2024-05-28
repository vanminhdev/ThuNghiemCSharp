namespace MB.RabbitMQ.Interfaces
{
    public interface IConsumerService : IDisposable
    {
        /// <summary>
        /// Đọc message
        /// </summary>
        /// <param name="exclusive">Có cho phép chỉ một consumer đọc từ queue</param>
        /// <returns></returns>
        Task ReadMessages(bool exclusive = false);
    }
}

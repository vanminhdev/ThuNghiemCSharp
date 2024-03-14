namespace DemoMinIO
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await FileUpload.RunUpload();
        }
    }
}

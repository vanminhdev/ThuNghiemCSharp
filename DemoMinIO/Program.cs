namespace DemoMinIO
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //await FileUpload.RunUpload();
            await S3Manager.Test();
        }
    }
}

namespace HandleExceptions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine($"Nội dung vừa nhập: {Console.ReadLine()}");


                // Các câu lệnh trong khối try
                // Thực hiện theo thứ tự từ trên xuống dưới
                Console.WriteLine("Bắt đầu thực hiện khối try");

                // Một số câu lệnh khác
                int a = 10;
                int b = 0;
                int result = a / b; // Tạo một ngoại lệ DivideByZeroException
                Console.WriteLine(result);
            }
            catch (DivideByZeroException ex)
            {
                // Các câu lệnh trong khối catch
                // Được thực hiện khi xảy ra ngoại lệ DivideByZeroException
                Console.WriteLine("Xử lý ngoại lệ DivideByZeroException: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Các câu lệnh trong khối catch
                // Được thực hiện nếu xảy ra ngoại lệ khác ngoài DivideByZeroException
                Console.WriteLine("Xử lý ngoại lệ khác: " + ex.Message);
            }
            finally
            {
                // Các câu lệnh trong khối finally
                // Được thực hiện sau khi kết thúc khối try hoặc catch, dù có ngoại lệ hay không
                Console.WriteLine("Kết thúc khối try-catch-finally");
            }

        }
    }
}

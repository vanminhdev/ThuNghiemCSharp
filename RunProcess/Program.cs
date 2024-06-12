using System;
using System.Diagnostics;
using System.Text;
using static System.Runtime.CompilerServices.RuntimeHelpers;


namespace RunProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string command = "run D:\\Demo\\ThuNghiemCSharp\\HandleExceptions\\Program.cs --project D:\\Demo\\ThuNghiemCSharp\\HandleExceptions";

            string inputString = "xin chào";

            // Tạo một quy trình để thực thi
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = @"C:\Program Files\dotnet\dotnet.exe",
                Arguments = $"{command}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                RedirectStandardInput = true,
            };

            // Thực thi lệnh
            using Process process = new Process
            {
                StartInfo = startInfo
            };
            process.Start();

            using (StreamWriter writer = process.StandardInput)
            {
                if (writer.BaseStream.CanWrite)
                {
                    writer.Write(inputString);
                }
            }

            //byte[] buffer = System.Text.Encoding.UTF8.GetBytes(inputString);
            //process.StandardInput.BaseStream.Write(buffer, 0, buffer.Length);
            //buffer = System.Text.Encoding.UTF8.GetBytes("\n");
            //process.StandardInput.BaseStream.Write(buffer, 0, buffer.Length);
            //process.StandardInput.Write("\n");

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            // In kết quả
            Console.WriteLine("Output:");
            Console.WriteLine(output);
            Console.WriteLine("Error:");
            Console.WriteLine(error);
        }
    }

}

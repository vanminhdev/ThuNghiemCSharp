using ConsoleAppTest.DbContexts;
using ConsoleAppTest.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ConsoleAppTest
{
    internal class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(app =>
                {
                    app.AddJsonFile("appsettings.json");
                })
                .ConfigureServices((hostContext, services) =>
                {
                    string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                    string? connectionString = hostContext.Configuration.GetConnectionString("Default");

                    //entity framework
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseSqlServer(connectionString, option => option.MigrationsAssembly(assemblyName));
                    });
                });

        public class Compare : IEqualityComparer<int>
        {
            public bool Equals(int x, int y)
            {
                return x == y;
            }

            public int GetHashCode([DisallowNull] int obj)
            {
                return obj.GetHashCode();
            }
        }

        public class ByteArrayConverter : JsonConverter<byte[]>
        {
            public override byte[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.Null)
                    return null;

                return reader.GetBytesFromBase64();
            }

            public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
            {
                writer.WriteStartArray();

                foreach (var val in value)
                {
                    writer.WriteNumberValue(val);
                }

                writer.WriteEndArray();
            }
        }

        public class Data
        {
            public byte[] Bytes { get; set; }
        }

        public class ExceptionCustom : Exception
        {
            public ExceptionCustom(string? message) : base(message)
            {
            }
        }

        public class ExceptionCustom2 : Exception
        {
            public ExceptionCustom2(string? message) : base(message)
            {
            }
        }

        public static void OkException(ExceptionCustom ex)
        {

        }

        public static void OkException(ExceptionCustom2 ex)
        {

        }

        public static void OkException(Exception ex)
        {

        }

        public class A1
        {
            public string Str { get; set; }
        }

        public record R1(int A, int B, A1 Obj);

        public static void Method1(R1 r)
        {
            r.Obj.Str = "2";
        }

        /// <summary>
        /// Xoá dấu
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        static void Main(string[] args)
        {
            //try
            //{
            //    throw new ExceptionCustom2("abc");
            //}
            //catch (Exception ex)
            //{
            //    OkException(ex);
            //}

            //var regex = new Regex(@"{{Text_(\d+)_(\d+)}}");
            //string inputString1 = "abc def {{Text_123_456}}";
            //var result1 = regex.Matches(inputString1);
            //var result = result1.Count;

            //string inputString2 = "abc def ";
            //var result2 = regex.Matches(inputString2);

            //var jsonSerializerOptions = new JsonSerializerOptions
            //{
            //    Converters = { new ByteArrayConverter() },
            //    PropertyNameCaseInsensitive = true, // (Tùy chọn) Tự động chữ hoa/thường của tên thuộc tính
            //    //WriteIndented = true // (Tùy chọn) Định dạng đẹp khi xuất JSON
            //};
            //var test = JsonSerializer.Serialize(new Data
            //{
            //    Bytes = File.ReadAllBytes(@"D:\Docs\Hợp đồng.pdf"),
            //}, jsonSerializerOptions);

            //var test2 = JsonSerializer.Deserialize<Data>(test, jsonSerializerOptions);

            //var r1 = new R1(1, 1, new A1
            //{
            //    Str = "1"
            //});

            //Method1(r1);


            string input = "Für die Feier heute Abend brauchen wir noch Bier, Limonade und Wasser; Salzgebäck, Kekse und Chips; Papierteller und Plastikbesteck.";
            string output = RemoveDiacritics(input);
            Console.WriteLine(output);

        }
    }
}
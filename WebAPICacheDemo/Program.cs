using Microsoft.AspNetCore.ResponseCompression;
using WebAPICacheDemo.Hosted;

namespace WebAPICacheDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Services.AddResponseCaching();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHostedService<AutoFetchTokenHostedService>();

            builder.Services.AddResponseCaching(options =>
            {
                options.MaximumBodySize = 1024;
                options.UseCaseSensitivePaths = true; //đường dẫn phân biệt chữ hoa chữ thường
                options.SizeLimit = 1024 * 1024 * 1024; //1GB
            });

            //builder.Services.AddResponseCompression(options =>
            //{
            //    options.EnableForHttps = true;
            //    options.Providers.Add<BrotliCompressionProvider>();
            //    options.Providers.Add<GzipCompressionProvider>();

            //    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
            //        new[] { "video/mp4" }
            //    );
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseResponseCaching();
            //app.UseResponseCompression();
            app.UseAuthorization();

            app.MapControllers();
            app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}

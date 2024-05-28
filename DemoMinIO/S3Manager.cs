using Amazon.S3.Model;
using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMinIO
{
    public class S3Manager
    {
        private static readonly string bucketName = "bucket-1";
        private static readonly string accessKey = "user1"; //tạo tài khoản mật khẩu
        private static readonly string secretKey = "123@123a";
        private static readonly string serviceURL = "http://localhost:9000"; // URL của MinIO server

        public static async Task Test()
        {
            var s3Client = new AmazonS3Client(accessKey, secretKey, new AmazonS3Config
            {
                ServiceURL = serviceURL,
                ForcePathStyle = true, // Yêu cầu để làm việc với MinIO
            });

            var filePath = @"C:\Users\minhlv\Pictures\Screenshot 2024-02-26 173515.png";

            // Tải lên tệp
            string s3Key = await UploadFileAsync(s3Client, filePath);

            // Tải xuống tệp
            await DownloadFileAsync(s3Client, s3Key);

            // Liệt kê các đối tượng trong bucket
            await ListObjectsAsync(s3Client);
        }

        private static async Task<string> UploadFileAsync(IAmazonS3 client, string filePath)
        {
            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            string s3Key = $"{DateTime.Now.ToFileTime()}{Path.GetExtension(filePath)}";

            var putRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = s3Key,
                InputStream = fileStream,
                AutoCloseStream = true,

            };

            var response = await client.PutObjectAsync(putRequest);
            Console.WriteLine("Tệp đã được tải lên thành công, ETag: " + response.ETag);

            return s3Key;
        }

        private static async Task DownloadFileAsync(IAmazonS3 client, string s3Key)
        {
            try
            {
                var getRequest = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = s3Key
                };

                using var response = await client.GetObjectAsync(getRequest);
                using var responseStream = response.ResponseStream;
                using var reader = new StreamReader(responseStream);
                string title = response.Metadata["x-amz-meta-title"];
                string contentType = response.Headers["Content-Type"];
                Console.WriteLine("Tệp tải xuống, Tiêu đề: {0}, Loại nội dung: {1}", title, contentType);

                string dest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), s3Key);
                if (!File.Exists(dest))
                {
                    await using (var fileStream = File.Create(dest))
                    {
                        responseStream.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                }
                Console.WriteLine("Tệp đã được lưu vào: " + dest);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Lỗi gặp phải khi tải xuống tệp: " + e.Message);
            }
        }

        private static async Task ListObjectsAsync(IAmazonS3 client)
        {
            try
            {
                var listRequest = new ListObjectsV2Request
                {
                    BucketName = bucketName
                };

                var listResponse = await client.ListObjectsV2Async(listRequest);
                foreach (S3Object entry in listResponse.S3Objects)
                {
                    Console.WriteLine("Tìm thấy đối tượng với key: {0}, kích thước: {1}", entry.Key, entry.Size);
                }
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Lỗi gặp phải khi liệt kê các đối tượng: " + e.Message);
            }
        }
    }
}

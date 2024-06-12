using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace DemoMinIO
{
    public class FileUpload
    {
        public static async Task RunUpload()
        {
            var endpoint = "localhost:9000";
            var accessKey = "user1";
            var secretKey = "123@123a";
            try
            {
                var minio = new MinioClient()
                    .WithEndpoint(endpoint)
                    .WithCredentials(accessKey, secretKey)
                    .WithSSL(false)
                    .Build();

                // Create an async task for listing buckets.
                var getListBucketsTask = await minio.ListBucketsAsync();

                // Iterate over the list of buckets.
                foreach (var bucket in getListBucketsTask.Buckets)
                {
                    Console.WriteLine(bucket.Name + " " + bucket.CreationDateDateTime);
                }

                await Upload(minio);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        // File uploader task.
        private async static Task Upload(IMinioClient minio)
        {
            var bucketName = "bucket-1";
            //var location = "us-east-1";
            var objectName = $"Screenshot {DateTime.Now.ToFileTime()}.png";
            var filePath = @"C:\Users\minhlv\Pictures\Screenshot 2024-02-26 173515.png";
            //var contentType = "application/zip";

            try
            {
                // Make a bucket on the server, if not already present.
                var beArgs = new BucketExistsArgs().WithBucket(bucketName);
                bool found = await minio.BucketExistsAsync(beArgs);
                if (!found)
                {
                    var mbArgs = new MakeBucketArgs().WithBucket(bucketName);
                    await minio.MakeBucketAsync(mbArgs);
                }
                // Upload a file to bucket.
                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithFileName(filePath);
                    //.WithContentType(contentType);
                var result = await minio.PutObjectAsync(putObjectArgs);
                Console.WriteLine("Successfully uploaded " + objectName);
            }
            catch (MinioException e)
            {
                Console.WriteLine("File Upload Error: {0}", e.Message);
            }
        }
    }
}

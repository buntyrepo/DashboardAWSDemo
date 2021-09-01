using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.IO;

namespace AWS
{
    public class AmazonS3Helper
    {
        private const string AwsSecretAccessKey = "+s26IaA0d2VaCRGA7RsvrHN2679J+VbFNH6M0aD3";
        public static string AwsAccessKeyId = "AKIA2GSUQAMJRJ7KNM4G";
        public static string DefaultBucketName = "test23720214";
        public static Amazon.RegionEndpoint EndPoint = Amazon.RegionEndpoint.USEast1;

        private static TransferUtility GetFileTransferUtility()
        {
            if (string.IsNullOrEmpty(AwsAccessKeyId))
                throw new Exception("Missing Aws Access Key ID");

            if (string.IsNullOrEmpty(AwsSecretAccessKey))
                throw new Exception("Missing Aws Secret Access Key");

            return new
                                TransferUtility(AwsAccessKeyId, AwsSecretAccessKey, EndPoint);
        }

        public static void Upload(Stream TheStream, string Filename)
        {
            try
            {
                TransferUtility fileTransferUtility = GetFileTransferUtility();
                TransferUtilityUploadRequest fileTransferUtilityRequest = new TransferUtilityUploadRequest();
                fileTransferUtilityRequest.BucketName = ValueManagers.BucketName;
                fileTransferUtilityRequest.InputStream = TheStream;
                fileTransferUtilityRequest.StorageClass = S3StorageClass.ReducedRedundancy;
                fileTransferUtilityRequest.Key = Filename;
                fileTransferUtilityRequest.CannedACL = S3CannedACL.PublicRead;
                // 1. Upload a file, file name is used as the object key name.
                fileTransferUtility.Upload(fileTransferUtilityRequest);
                //Console.WriteLine("Upload 1 completed");

                //// 2. Specify object key name explicitly.
                //fileTransferUtility.Upload(filePath,
                //                          existingBucketName, keyName);
                //Console.WriteLine("Upload 2 completed");

                // 3. Upload data from a type of System.IO.Stream.
                //using (FileStream fileToUpload =
                //    new FileStream(filePath, FileMode.Open, FileAccess.Read))
                //{
                //    fileTransferUtility.Upload(fileToUpload,
                //                               existingBucketName, keyName);
                //}
                //Console.WriteLine("Upload 3 completed");


            }
            catch (AmazonS3Exception s3Exception)
            {
                Console.WriteLine(s3Exception.Message,
                                  s3Exception.InnerException);
            }
        }
        public static void AdvanceUpload(Stream TheStream, string Filename, string BucketName, long PartSize, Dictionary<string, string> Metadata, S3CannedACL S3ACL)
        {
            TransferUtility fileTransferUtility = GetFileTransferUtility();
            TransferUtilityUploadRequest fileTransferUtilityRequest = new TransferUtilityUploadRequest();
            fileTransferUtilityRequest.BucketName = ValueManagers.BucketName;
            fileTransferUtilityRequest.InputStream = TheStream;
            fileTransferUtilityRequest.StorageClass = S3StorageClass.ReducedRedundancy;
            fileTransferUtilityRequest.PartSize = PartSize;

            fileTransferUtilityRequest.Key = Filename;
            fileTransferUtilityRequest.CannedACL = S3ACL;
            foreach (KeyValuePair<string, string> keyValuePairString in Metadata)
            {
                fileTransferUtilityRequest.Metadata.Add(keyValuePairString.Key, keyValuePairString.Value);
            }

            fileTransferUtility.Upload(fileTransferUtilityRequest);

        }
        private static AmazonS3Client GetClient()
        {
            return new AmazonS3Client(AwsAccessKeyId, AwsSecretAccessKey, Amazon.RegionEndpoint.USEast1);
        }
        public static string UploadedFileUrl(string bucketName, string fullFileName)
        {
            GetPreSignedUrlRequest request = new GetPreSignedUrlRequest();
            request.BucketName = bucketName;
            request.Key = fullFileName;
            request.Expires = DateTime.Now.AddYears(5);
            request.Protocol = Protocol.HTTPS;
            string url = GetClient().GetPreSignedURL(request);
            return url;
        }
        public static void DownloadFile(string FileNameKey, Stream Stream)
        {
            using (AmazonS3Client client = GetClient())
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = ValueManagers.BucketName,
                    Key = FileNameKey
                };

                using (GetObjectResponse response = client.GetObject(request))
                {
                    response.ResponseStream.CopyTo(Stream);
                }
            }
        }
        public static void DeleteFile(string FileNameKey)
        {
            using (AmazonS3Client client = GetClient())
            {
                DeleteObjectRequest request = new DeleteObjectRequest
                {
                    BucketName = ValueManagers.BucketName,
                    Key = FileNameKey
                };

                client.DeleteObjectAsync(request);
            }
        }
    }
}

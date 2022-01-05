using Amazon.S3;

namespace file_handler;

public class S3FileSystemIo : IFilesystemIo
{
    private readonly Settings _settings;
    private readonly AmazonS3Client _s3Client;

    public S3FileSystemIo(Settings settings)
    {
        _settings = settings;
        _s3Client = new(
            _settings.GetS3AccessKey(),
            _settings.GetS3SecretKey(),
            Amazon.RegionEndpoint.GetBySystemName(_settings.GetS3Region()));
    }

    public async Task<FileStoreResponse> Write(FileHeaders fileHeaders, FilesystemData fileData)
    {
        return new FileStoreResponse(await _s3Client.PutObjectAsync(
            new()
            {
                BucketName = _settings.GetS3BucketName(),
                ContentType = "image/*",
                InputStream = fileData.Data,
                Key = $"{_settings.GetS3Folder()}/{fileData.Name}",
                CannedACL = S3CannedACL.PublicRead,
                StorageClass = S3StorageClass.Standard,
                Headers =
                {
                    ContentType = fileHeaders.ContentType()
                }
            }
        ));
    }
}
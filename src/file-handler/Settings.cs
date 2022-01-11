namespace file_handler;

public class Settings : Properties
{
    private const string MAX_FILE_SIZE = "max_file_size";
    private const string ATTRIBUTE_NAME = "file_attribute_name";
    private const string S3_ACCESS_KEY = "S3_ACCESS_KEY";
    private const string S3_SECRET_KEY = "S3_SECRET_KEY";
    private const string S3_REGION = "S3_REGION";
    private const string S3_BUCKET_NAME = "S3_BUCKET_NAME";
    private const string S3_FOLDER = "S3_FOLDER";

    public int GetMaxFileSize()
    {
        return Convert.ToInt32(Props[MAX_FILE_SIZE]);
    }

    public string GetAttributeName()
    {
        return Props[ATTRIBUTE_NAME];
    }

    public string GetS3AccessKey()
    {
        return Props[S3_ACCESS_KEY];
    }

    public string GetS3SecretKey()
    {
        return Props[S3_SECRET_KEY];
    }

    public string GetS3Region()
    {
        return Props[S3_REGION];
    }

    public string GetS3BucketName()
    {
        return Props[S3_BUCKET_NAME];
    }

    public string GetS3Folder()
    {
        return Props[S3_FOLDER];
    }
}
namespace file_handler;

public class FileHeaders : Properties
{
    private const string CONTENT_TYPE = "content-type";

    public string ContentType()
    {
        return Props[CONTENT_TYPE];
    }
}
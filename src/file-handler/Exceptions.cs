using HttpMultipartParser;

namespace file_handler;

public class Error
{
    public Error(string code, string description)
    {
        Code = code;
        Description = description;
    }

    public string Code { get; set; }
    public string Description { get; set; }
}

public abstract class AbstractException : Exception
{
    protected const string Prefix = "FILE";
    public abstract Error Get();
    public abstract string GetMessage();
}

public class OperationFailedException : AbstractException
{
    private readonly string _errorMessage;

    public OperationFailedException(string errorMessage)
    {
        _errorMessage = errorMessage;
    }

    public override Error Get()
    {
        return new Error(Prefix + "000005", GetMessage());
    }

    public override string GetMessage()
    {
        return "Failure storing logo";
    }
}

public class FileSizeTooLargeException : AbstractException
{
    public override Error Get()
    {
        return new Error(Prefix + "000004", GetMessage());
    }

    public override string GetMessage()
    {
        return "File too large";
    }
}

public class ContentTypeNotSupportedException : AbstractException
{
    public override Error Get()
    {
        return new Error(Prefix + "000003", GetMessage());
    }

    public override string GetMessage()
    {
        return "Content type not supported";
    }
}

public class FileNotPresentException : AbstractException
{
    public override Error Get()
    {
        return new Error(Prefix + "000002", GetMessage());
    }

    public override string GetMessage()
    {
        return "Missing logo file";
    }
}

public class NoDataFoundException : AbstractException
{
    private MultipartParseException _multipartParseException;
    public NoDataFoundException(MultipartParseException multipartParseException)
    {
        _multipartParseException = multipartParseException;
    }

    public override Error Get()
    {
        return new Error(Prefix + "000006", GetMessage());
    }

    public override string GetMessage()
    {
        return "No data found in request";
    }
}
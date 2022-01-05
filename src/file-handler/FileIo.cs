using HttpMultipartParser;

namespace file_handler;

public class FileIo : IFileIo
{
    private readonly IFilesystemIo _filesystemIo;
    private readonly Settings _settings;

    public FileIo(IFilesystemIo filesystemIo, Settings settings)
    {
        _filesystemIo = filesystemIo;
        _settings = settings;
    }

    public async Task Write(FileHeaders fileHeaders, FileData fileData)
    {
        Task<FileStoreResponse> response = _filesystemIo.Write(fileHeaders, await GetFilesystemData(fileData));
        if (!response.Result.Succes)
        {
            throw new OperationFailedException(response.Result.Error);
        }
    }

    private async Task<FilesystemData> GetFilesystemData(FileData fileData)
    {
        MultipartFormDataParser bodyParser = await Parse(fileData);
        FilePart? file = bodyParser.Files.FirstOrDefault(x => x.Name == _settings.GetAttributeName());
        Validate(file);
        return new FilesystemDataBuilder()
            .Name(bodyParser.GetParameterValue("id"))
            .Stream(file?.Data)
            .Build();
    }

    private static async Task<MultipartFormDataParser> Parse(FileData fileData)
    {
        try
        {
            return await MultipartFormDataParser.ParseAsync(fileData.FileStream);
        }
        catch (MultipartParseException e)
        {
            throw new NoDataFoundException(e);
        }
    }

    private void Validate(FilePart? file)
    {
        if (file == null)
        {
            throw new FileNotPresentException();
        }

        if (!file.ContentType.Contains("image"))
        {
            throw new ContentTypeNotSupportedException();
        }

        if (file.Data.Length > _settings.GetMaxFileSize())
        {
            throw new FileSizeTooLargeException();
        }
    }
}

public class FilesystemData
{
    public string? Name { get; }
    public Stream? Data { get; }

    public FilesystemData(string? name, Stream? data)
    {
        Name = name;
        Data = data;
    }
}

public class FilesystemDataBuilder
{
    private string? _name;
    private Stream? _stream;

    public FilesystemDataBuilder Name(string? name)
    {
        _name = name;
        return this;
    }

    public FilesystemDataBuilder Stream(Stream? stream)
    {
        _stream = stream;
        return this;
    }

    public FilesystemData Build()
    {
        if (_name == null)
        {
            throw new NameNotInitializedException();
        }

        if (_stream == null)
        {
            throw new StreamNotInitializedException();
        }

        return new FilesystemData(_name, _stream);
    }
}

public class StreamNotInitializedException : Exception
{
}

public class NameNotInitializedException : Exception
{
}
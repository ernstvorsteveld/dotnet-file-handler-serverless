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

public class StreamNotInitializedException : Exception
{
}

public class NameNotInitializedException : Exception
{
}
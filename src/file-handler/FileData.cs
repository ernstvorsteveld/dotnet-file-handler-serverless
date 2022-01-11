namespace file_handler;

public class FileData
{
    public Stream FileStream { get; }

    public FileData(Stream stream) => FileStream = stream;
}
namespace file_handler;

public interface IFileIo
{
    public Task Write(FileHeaders fileHeaders, FileData fileData);
}
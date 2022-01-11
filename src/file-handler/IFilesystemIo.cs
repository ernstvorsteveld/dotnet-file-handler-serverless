namespace file_handler;

public interface IFilesystemIo
{
    Task<FileStoreResponse> Write(FileHeaders fileHeaders, FilesystemData fileData);
}
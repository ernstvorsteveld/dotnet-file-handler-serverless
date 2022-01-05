using System.Net;
using Amazon.S3.Model;

namespace file_handler;

public interface IFilesystemIo
{
    Task<FileStoreResponse> Write(FileHeaders fileHeaders, FilesystemData fileData);
}

public class FileStoreResponse
{
    public bool Succes { get; set; }
    public string Error { get; set; }

    public FileStoreResponse(PutObjectResponse putObjectResponse)
    {
        Succes = putObjectResponse.HttpStatusCode == HttpStatusCode.OK;
        Error = "to do";
    }
}
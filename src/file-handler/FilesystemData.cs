namespace file_handler;

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
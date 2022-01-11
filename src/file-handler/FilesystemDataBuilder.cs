namespace file_handler;

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
namespace file_handler;

public abstract class Properties
{
    protected Dictionary<string, string> Props;

    protected Properties() => Props = new Dictionary<string, string>();

    protected Properties(Dictionary<string, string> properties) => Props = properties;
}
namespace week9.Model.Exception
{
    public class NotFoundException(string message) : IOException(message);
}

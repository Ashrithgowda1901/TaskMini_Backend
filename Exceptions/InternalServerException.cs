using TaskMini.Exceptions;

public class InternalServerException : AppException
{
    public InternalServerException(string message)
        : base(message, 500)
    {
    }
}
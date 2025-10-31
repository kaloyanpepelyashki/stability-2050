namespace WorldOfZuul.Exceptions;

public class FileReadException : Exception
{
    
    // Default constructor
    public FileReadException()
    {
    }

    // Constructor with custom message
    public FileReadException(string message)
        : base(message)
    {
    }

    // Constructor with message and inner exception
    public FileReadException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

}
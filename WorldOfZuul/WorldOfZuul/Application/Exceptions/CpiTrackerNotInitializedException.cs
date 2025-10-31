namespace WorldOfZuul.Exceptions;

public class CpiTrackerNotInitializedException : Exception
{
    public CpiTrackerNotInitializedException() : base("Cpi tracker not initialized.") { }

    public CpiTrackerNotInitializedException(string message) : base(message) { }
    
    public CpiTrackerNotInitializedException(string message, Exception inner) : base(message, inner) { }
}
namespace Quiz.Exceptions;

public class RecordExistsException : Exception
{
    public RecordExistsException(string message) : base(message) { }
}

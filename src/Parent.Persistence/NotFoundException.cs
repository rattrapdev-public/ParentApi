namespace Parent.Persistence;

public class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message)
    {
        
    }
}
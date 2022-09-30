namespace Parent.Domain;

public record EmailAddress
{
    public string Email { get; }

    public EmailAddress(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("The email cannot be an empty string");
        }
        
        Email = email;
    }
}
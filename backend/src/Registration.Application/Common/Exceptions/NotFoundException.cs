namespace Registration.Application.Common.Exceptions;

/// <summary>Raised when a requested entity does not exist. Mapped to 404 by the API.</summary>
public sealed class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string name, object key)
        : base($"\"{name}\" ({key}) was not found.")
    {
    }
}

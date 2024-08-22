using System.Runtime.Serialization;

namespace Axidel.Service.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException() { }
    public ForbiddenException(string message) : base(message) { }
    public ForbiddenException(string message, Exception innerException) : base(message) { }
    public ForbiddenException(SerializationException exception, StreamingContext context) { }
    public int StatusCode => 403;
}
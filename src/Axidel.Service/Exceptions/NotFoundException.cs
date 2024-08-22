using System.Runtime.Serialization;

namespace Axidel.Service.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() { }
    public NotFoundException(string message) : base(message) { }
    public NotFoundException(string message, Exception innerException) : base(message) { }
    public NotFoundException(SerializationException exception, StreamingContext context) { }
    public int StatusCode => 404;
}
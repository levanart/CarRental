namespace CarRental.Application.Common.Exceptions;

public class BadRequestException : Exception
{
    public string[] Errors { get; set; }

    public BadRequestException(string[] errors) : base("Multiple errors occurred. See error details")
    {
        Errors = errors;
    }

    public BadRequestException(string message) : base(message) { }
}
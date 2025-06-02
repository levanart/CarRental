namespace CarRental.Application.Features.UserFeatures.GetByPhone;

public record GetUserByPhoneResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
}
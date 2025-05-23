namespace CarRental.Application.Features.UserFeatures.GetAllUsers;

public record GetAllUserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
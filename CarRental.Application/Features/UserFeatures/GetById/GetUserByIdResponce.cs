﻿namespace CarRental.Application.Features.UserFeatures.GetById;

public record GetUserByIdResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
}
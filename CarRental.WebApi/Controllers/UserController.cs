using CarRental.Application.Features.UserFeatures.CreateUser;
using CarRental.Application.Features.UserFeatures.DeleteUser;
using CarRental.Application.Features.UserFeatures.GetAllUsers;
using CarRental.Application.Features.UserFeatures.GetByEmail;
using CarRental.Application.Features.UserFeatures.GetById;
using CarRental.Application.Features.UserFeatures.GetByPhone;
using CarRental.Application.Features.UserFeatures.GetByUsername;
using CarRental.Application.Features.UserFeatures.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var request = new GetAllUserRequest();
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        
        [HttpGet("id/{id}")]
        public async Task<ActionResult<GetUserByIdResponse>> GetUserById(Guid id)
        {
            var request = new GetUserByIdRequest(id);
            var response = await _mediator.Send(request);
            return response == null ? NotFound() : Ok(response);
        }
        
        [HttpGet("email/{email}")]
        public async Task<ActionResult<GetUserByEmailResponse>> GetUserByEmail(string email)
        {
            var request = new GetUserByEmailRequest(email);
            var response = await _mediator.Send(request);
            return response == null ? NotFound() : Ok(response);
        }
        
        [HttpGet("phone/{phone}")]
        public async Task<ActionResult<GetUserByPhoneResponse>> GetUserByPhone(string phone)
        {
            var request = new GetUserByPhoneRequest(phone);
            var response = await _mediator.Send(request);
            return response == null ? NotFound() : Ok(response);
        }
        
        [HttpGet("username/{username}")]
        public async Task<ActionResult<GetUserByUsernameResponse>> GetUserByUsername(string username)
        {
            var request = new GetUserByUsernameRequest(username);
            var response = await _mediator.Send(request);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteUserResponse>> DeleteUser(Guid id)
        {
            var request = new DeleteUserRequest(id);
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateUserResponse>> UpdateUser(
            Guid id, [FromBody] UpdateUserRequest updateRequest, CancellationToken cancellationToken)
        {
            var updatedRequest = updateRequest with { UserId = id };

            var response = await _mediator.Send(updatedRequest, cancellationToken);
            return Ok(response);
        }
    }
}
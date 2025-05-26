using CarRental.Application.Features.CarFeatures.DeleteCar;
using CarRental.Application.Features.CarFeatures.GetById;
using CarRental.Application.Features.CarFeatures.UpdateCar;
using CarRental.Application.Features.UserFeatures.CreateUser;
using CarRental.Application.Features.UserFeatures.DeleteUser;
using CarRental.Application.Features.UserFeatures.GetAllUsers;
using CarRental.Application.Features.UserFeatures.GetById;
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
        
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserByIdResponse>> GetUserById(Guid id)
        {
            var request = new GetUserByIdRequest(id);
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
        public async Task<ActionResult<UpdateUserResponse>> UpdateApartment(
            Guid id, [FromBody] UpdateUserRequest updateRequest, CancellationToken cancellationToken)
        {
            var updatedRequest = updateRequest with { UserId = id };

            var response = await _mediator.Send(updatedRequest, cancellationToken);
            return Ok(response);
        }
    }
}
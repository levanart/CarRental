using CarRental.Application.Features.UserFeatures.CreateUser;
using CarRental.Application.Features.UserFeatures.GetAllUsers;
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
    }
}
using CarRental.Application.Features.CarFeatures.CreateCar;
using CarRental.Application.Features.CarFeatures.DeleteCar;
using CarRental.Application.Features.CarFeatures.GetAllCars;
using CarRental.Application.Features.CarFeatures.GetById;
using CarRental.Application.Features.CarFeatures.UpdateCar;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateCarResponse>> CreateCar([FromBody] CreateCarRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllCarResponse>>> GetCars()
        {
            var request = new GetAllCarRequest();
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCarByIdResponse>> GetCarById(Guid id)
        {
            var request = new GetCarByIdRequest(id);
            var response = await _mediator.Send(request);
            return response == null ? NotFound() : Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteCarResponse>> DeleteCar(Guid id)
        {
            var request = new DeleteCarRequest(id);
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateCarResponse>> UpdateCar(
            Guid id, [FromBody] UpdateCarRequest updateRequest, CancellationToken cancellationToken)
        {
            var updatedRequest = updateRequest with { CarId = id };

            var response = await _mediator.Send(updatedRequest, cancellationToken);
            return Ok(response);
        }
    }
}
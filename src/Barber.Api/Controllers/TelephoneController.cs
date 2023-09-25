using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Barber.Api.Features.Addresses.Queries.GetTelephoneById;
using Barber.Api.Features.Addresses.Commands.CreateTelephone;
using Barber.Api.Features.Addresses.Commands.DeleteTelephone;
using Barber.Api.Features.Addresses.Commands.UpdateTelephone;

namespace Barber.Api.Controllers;

[ApiController]
[Route("api/customers/{customerId}/telephones")]
public class TelephoneController : MainController{
  private readonly IMediator _mediator;

  public TelephoneController(IMediator mediator){
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }


  // Gets:
  [HttpGet("{telephoneId}", Name = "GetTelephoneById")]
  public async Task<ActionResult<GetTelephoneByIdDetailDto>> GetTelephoneById(int customerId, int telephoneId){
    var getTelephoneByIdDetailQuery = new GetTelephoneByIdDetailQuery { CustomerId = customerId, TelephoneId = telephoneId };

    var telephoneToReturn = await _mediator.Send(getTelephoneByIdDetailQuery);

    if(telephoneToReturn == null) return NotFound();

    return Ok(telephoneToReturn);
  }


  // Posts:
  [HttpPost]
  public async Task<ActionResult<CreateTelephoneCommandDto>> CreateTelephone(int customerId, CreateTelephoneCommand telephoneToCreateDto){
    if(customerId != telephoneToCreateDto.CustomerId) return BadRequest();

    var response = await _mediator.Send(telephoneToCreateDto);

    if(!response.IsSuccessful){
      if (response.Errors.Count == 0) {
        return NotFound();
      }

      return Validate(response.Errors);
    }

    return CreatedAtRoute(
      "GetTelephoneById",
      new { customerId, telephoneId = response.Telephone.Id },
      response.Telephone
    );
  }


  // Deletes:
  [HttpDelete("{telephoneId}")]
  public async Task<ActionResult> DeleteTelephone(int customerId, int telephoneId){
    var deleteTelephoneCommand = new DeleteTelephoneCommand{ CustomerId = customerId, TelephoneId = telephoneId };

    var response = await _mediator.Send(deleteTelephoneCommand);

    if(!response) return NotFound();

    return NoContent();
  }


  // Puts:
  [HttpPut("{telephoneId}")]
  public async Task<ActionResult> UpdateTelephone(int customerId, int telephoneId, UpdateTelephoneCommand telephoneToUpdateDto){
    if(telephoneId != telephoneToUpdateDto.Id || customerId != telephoneToUpdateDto.CustomerId) return BadRequest();

    var response = await _mediator.Send(telephoneToUpdateDto);

    if(!response.IsSuccessful){
      if(response.Errors.Count == 0) {
        return NotFound();
      }

      return Validate(response.Errors);
    }

    return NoContent();
  }
}
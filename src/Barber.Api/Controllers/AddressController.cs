using Barber.Api.Models;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Barber.Api.Features.Addresses.Queries.GetAddressById;
using Barber.Api.Features.Addresses.Commands.CreateAddress;
using Barber.Api.Features.Addresses.Commands.DeleteAddress;
using Barber.Api.Features.Addresses.Commands.UpdateAddress;

namespace Barber.Api.Controllers;

[ApiController]
[Route("api/customers/{customerId}/addresses")]
public class AddressController : MainController{
  private readonly IMediator _mediator;

  public AddressController(IMediator mediator){
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }


  // Gets:
  [HttpGet("{addressId}", Name = "GetAddressById")]
  public async Task<ActionResult<GetAddressByIdDetailDto>> GetAddressById(int customerId, int addressId){
    var getAddressByIdDetailQuery = new GetAddressByIdDetailQuery{CustomerId = customerId, AddressId = addressId};

    var addressToReturn = await _mediator.Send(getAddressByIdDetailQuery);

    if(addressToReturn == null) return NotFound();

    return Ok(addressToReturn);
  }

  
  // Posts:
  [HttpPost]
  public async Task<ActionResult<CreateAddressCommandDto>> CreateAddress(int customerId, CreateAddressCommand addressToCreateDto){
    if(addressToCreateDto.CustomerId != customerId) return BadRequest();

    var response = await _mediator.Send(addressToCreateDto);

    if(response.IsSuccessful == false) {
      if (response.Errors.Count == 0) {
        return NotFound();
      }

      return Validate(response.Errors);
    }

    return CreatedAtRoute(
      "GetAddressById",
      new { customerId, addressId = response.Address.Id },
      response.Address
    );
  }


  // Deletes:
  [HttpDelete("{addressId}")]
  public async Task<ActionResult> DeleteAddress(int customerId, int addressId){
    var deleteAddressCommand = new DeleteAddressCommand{ CustomerId = customerId, AddressId = addressId};

    var response = await _mediator.Send(deleteAddressCommand);

    if(!response) return NotFound();

    return NoContent();
  }
  

  // Puts:
  [HttpPut("{addressId}")]
  public async Task<ActionResult> UpdateAddress(int customerId, int addressId, UpdateAddressCommand addressToUpdateDto){
    if(addressId != addressToUpdateDto.Id || customerId != addressToUpdateDto.CustomerId) return BadRequest();

    var response = await _mediator.Send(addressToUpdateDto);

    if(!response.IsSuccessful){
      if(response.Errors.Count == 0) {
        return NotFound();
      }

      return Validate(response.Errors);
    }

    return NoContent();
  }
}
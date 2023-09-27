using Microsoft.AspNetCore.Mvc;
using MediatR;
using Barber.Api.Features.Customers.Queries.GetAllCustomers;
using Barber.Api.Features.Customers.Queries.GetCustomerById;
using Barber.Api.Features.Customers.Queries.GetCustomersWithTelephones;
using Barber.Api.Features.Customers.Queries.GetCustomerWithAddressesAndTelephonesById;
using Barber.Api.Features.Customers.Commands.CreateNewCustomer;
using Barber.Api.Features.Customers.Commands.DeleteCustomer;
using Barber.Api.Features.Customers.Commands.UpdateCustomer;

namespace Barber.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : MainController{
  private readonly IMediator _mediator;

  public CustomerController(IMediator mediator){
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }


  // Gets:
  [HttpGet]
  public async Task<ActionResult<IEnumerable<GetAllCustomersDetailDto>>> GetAllCustomers(){
    var getAllCustomersDetailQuery = new GetAllCustomersDetailQuery{ };

    var customersToReturn = await _mediator.Send(getAllCustomersDetailQuery);

    if(!customersToReturn.Any()) return NotFound();

    return Ok(customersToReturn);
  }

  [HttpGet("{customerId}")]
  public async Task<ActionResult<GetCustomerByIdDetailDto>> GetCustomerById(int customerId){
    var getCustomerByIdDetailQuery = new GetCustomerByIdDetailQuery{ CustomerId = customerId };

    var customerToReturn = await _mediator.Send(getCustomerByIdDetailQuery);

    if(customerToReturn == null) return NotFound();

    return Ok(customerToReturn);
  }

  [HttpGet("with-telephones")]
  public async Task<ActionResult<IEnumerable<GetCustomersWithTelephonesDetailDto>>> GetCustomersWithTelephones(){
    var getCustomersWithTelephonesDetailQuery = new GetCustomersWithTelephonesDetailQuery{};

    var customerToReturn = await _mediator.Send(getCustomersWithTelephonesDetailQuery);

    if(customerToReturn == null) return NotFound();

    return Ok(customerToReturn);
  }

  [HttpGet("with-addresses-and-telephones/{customerId}", Name = "GetCustomerWithAddressesAndTelephonesById")]
  public async Task<ActionResult<GetCustomerWithAddressesAndTelephonesByIdDetailDto>> GetCustomerWithAddressesAndTelephonesById(int customerId){
    var getCustomerWithAddressesAndTelephonesByIdDetailQuery = new GetCustomerWithAddressesAndTelephonesByIdDetailQuery { CustomerId = customerId};

    var customerToReturn = await _mediator.Send(getCustomerWithAddressesAndTelephonesByIdDetailQuery);

    if(customerToReturn == null) return NotFound();

    return Ok(customerToReturn);
  }


  // Posts:
  [HttpPost]
  public async Task<ActionResult<CreateNewCustomerCommandResponse>> CreateNewCustomer(CreateNewCustomerCommand customerToCreateDto){
    var response = await _mediator.Send(customerToCreateDto);

    if (response.IsSuccessful == false) {
      if (response.Errors.Count == 0) {
        return NotFound();
      }

      return Validate(response.Errors);
    }

    return CreatedAtRoute(
      "GetCustomerWithAddressesAndTelephonesById",
      new { customerId = response.Customer.Id },
      response.Customer
    );
  }


  // Deletes:
  [HttpDelete("{customerId}")]
  public async Task<ActionResult> DeleteCustomer(int customerId){
    var deleteCustomerCommand = new DeleteCustomerCommand{ CustomerId = customerId };

    var response = await _mediator.Send(deleteCustomerCommand);

    if(!response) return NotFound();

    return NoContent();
  }


  // Updates:
  [HttpPut("{customerId}")]
  public async Task<ActionResult> UpdateCustomer(int customerId, UpdateCustomerCommand customerToUpdate){
    if(customerId != customerToUpdate.Id) return BadRequest();

    var response = await _mediator.Send(customerToUpdate);

    if(!response.IsSuccessful){
      if(response.Errors.Count == 0) {
        return NotFound();
      }

      return Validate(response.Errors);
    }

    return NoContent();
  }
}

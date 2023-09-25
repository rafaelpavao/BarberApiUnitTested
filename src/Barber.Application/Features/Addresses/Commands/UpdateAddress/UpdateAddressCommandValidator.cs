using System.Text.RegularExpressions;
using Barber.Api.Models;
using FluentValidation;

namespace Barber.Api.Features.Addresses.Commands.UpdateAddress;

public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>{
  public UpdateAddressCommandValidator(){
    RuleFor(a => a.Id)
      .NotEmpty()
      .WithMessage("Id tem que ser preenchido...");

    RuleFor(a => a.CustomerId)
      .NotEmpty()
      .WithMessage("CustomerId tem que ser preenchido...");

    RuleFor(a => a.CEP)
      .Cascade(CascadeMode.Stop)
      .Length(9)
      .WithMessage("CEP deve ter 9 caracteres...")
      .Must(ValidateCEP)
      .When(a => !string.IsNullOrEmpty(a.CEP))
      .WithMessage("CEP inválido...");

    RuleFor(a => a.Street)
      .NotEmpty()
      .WithMessage("Street tem que ser preenchido...")
      .MaximumLength(80)
      .WithMessage("Street possui um máximo de 80 caracteres...");
    
    RuleFor(a => a.Number)
      .NotEmpty()
      .WithMessage("Number tem que ser preenchido...");

    RuleFor(a => a.District)
      .NotEmpty()
      .WithMessage("District tem que ser preenchido...")
      .MaximumLength(60)
      .WithMessage("District possui um máximo de 60 caracteres...");

    RuleFor(a => a.City)
      .NotEmpty()
      .WithMessage("City tem que ser preenchido...")
      .MaximumLength(60)
      .WithMessage("City possui um máximo de 60 caracteres...");

    RuleFor(a => a.State)
      .NotEmpty()
      .WithMessage("State tem que ser preenchido...")
      .Length(2)
      .WithMessage("State deve ter 2 caracteres...");
  }

  private bool ValidateCEP(string cep){
    string pattern = @"^\d{5}-\d{3}$";

    return Regex.IsMatch(cep, pattern);
  }
}

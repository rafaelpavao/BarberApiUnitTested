using System.Text.RegularExpressions;
using FluentValidation;

namespace Barber.Api.Features.Addresses.Commands.UpdateTelephone;

public class UpdateTelephoneCommandValidator : AbstractValidator<UpdateTelephoneCommand>{
  public UpdateTelephoneCommandValidator(){
    RuleFor(t => t.CustomerId)
      .NotEmpty()
      .WithMessage("CustomerId deve ser preenchido...");

    RuleFor(t => t.Id)
      .NotEmpty()
      .WithMessage("Id deve ser preenchido...");

    RuleFor(t => t.Number)
      .Cascade(CascadeMode.Stop)
      .NotEmpty()
      .WithMessage("Number tem que ser preenchido...")
      .MaximumLength(80)
      .WithMessage("Number possui um máximo de 80 caracteres...")
      .Must(IsValidPhoneNumber);

    RuleFor(a => a.Type)
      .IsInEnum()
      .WithMessage("Type inválido...");
  }

  private bool IsValidPhoneNumber(string numero){
    string pattern = @"^\+?\d{1,4}?\s?\d{2,5}\s?\d{4,5}-?\d{4}$";

    return Regex.IsMatch(numero, pattern);
  }
}
